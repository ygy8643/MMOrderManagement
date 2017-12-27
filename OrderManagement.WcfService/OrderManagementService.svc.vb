Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Migrations
Imports System.Data.Entity.Validation
Imports AutoMapper
Imports OrderManagement.Common
Imports OrderManagement.WcfService.Dto.OrderBlog
Imports OrderManagement.WcfService.Dto.OrderManagement

Public Class OrderManagementService
    Implements IOrderManagementService

#Region "OrderManagement"

#Region "Order"

    ''' <summary>
    '''     返回订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDto(orderId As Integer) As OrderDto Implements IOrderManagementService.GetOrderDto
        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of OrderDto)(
                db.Orders.Include("Customer").Include("OrderDetails").
                                               SingleOrDefault(Function(o) o.OrderId = orderId))
        End Using
    End Function

    ''' <summary>
    '''     返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoes() As List(Of OrderDto) Implements IOrderManagementService.GetOrderDtoes

        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of List(Of OrderDto))(db.Orders.Include("Customer").Include("OrderDetails"))
        End Using
    End Function

    ''' <summary>
    '''     按检索条件返回所有订单信息
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOrderDtoesByConditions(conditions As OrderSearchConditionsDto) As List(Of OrderDto) _
        Implements IOrderManagementService.GetOrderDtoesByConditions

        '开始日
        If conditions.OrderDateFrom Is Nothing Then
            conditions.OrderDateFrom = Now.Date.AddYears(-10).Date
        End If

        '终止日
        If conditions.OrderDateTo Is Nothing Then
            conditions.OrderDateTo = Now.Date.AddYears(10).Date
        Else
            conditions.OrderDateTo = conditions.OrderDateTo.Value.AddDays(1).Date
        End If

        Using db As New OrderManagementDbEntities

            Dim result = Mapper.Map(Of List(Of OrderDto))(
                db.Orders.Include("Customer").Include("OrderDetails").Where(
                    Function(order) ((conditions.CustomerId = 0 OrElse
                                      order.CustomerId = conditions.CustomerId) AndAlso
                                     order.OrderDate >= conditions.OrderDateFrom AndAlso
                                     order.OrderDate.Value < conditions.OrderDateTo)).ToList())

            Return result
        End Using
    End Function

    ''' <summary>
    ''' タイトルの名称により検索
    ''' </summary>
    ''' <param name="searchString"></param>
    ''' <returns></returns>
    Public Function GetPostDtoesByTitle(searchString As String) As IEnumerable(Of PostDto) _
        Implements IOrderManagementService.GetPostDtoesByTitle

        Using db As New OrderBlogDbEntities
            Return Mapper.Map(Of List(Of PostDto))(db.Posts.Where(Function(p) p.Title.Contains(searchString)).ToList())
        End Using
    End Function

    ''' <summary>
    '''     添加订单
    ''' </summary>
    ''' <param name="orderDto"></param>
    ''' <returns></returns>
    Public Function AddOrderDto(orderDto As OrderDto) As ProcessResult _
        Implements IOrderManagementService.AddOrderDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            Dim customer = db.Customers.Find(orderDto.CustomerId)
            If customer IsNot Nothing Then
                db.Customers.Attach(customer)
            End If

            db.Orders.AddOrUpdate(Mapper.Map(Of Order)(orderDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If CustomerDtoExists(orderDto.OrderId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     更新订单
    ''' </summary>
    ''' <param name="orderDto"></param>
    ''' <returns></returns>
    Public Function UpdateOrderDto(orderDto As OrderDto) As ProcessResult _
        Implements IOrderManagementService.UpdateOrderDto

        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            db.Entry(Mapper.Map(Of Order)(orderDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

    ''' <summary>
    '''     添加或更新订单
    ''' </summary>
    ''' <param name="orderDto"></param>
    ''' <returns></returns>
    Public Function AddOrUpdateOrderDto(orderDto As OrderDto) As ProcessResult _
        Implements IOrderManagementService.AddOrUpdateOrderDto

        Dim result As New ProcessResult

        Dim order = Mapper.Map(Of Order)(orderDto)

        Using db As New OrderManagementDbEntities

            Dim orderExist = db.Orders.Find(order.OrderId)

            If orderExist Is Nothing Then
                'Add Order if its not exist.
                'db.entry(order).State = EntityState.Added 
                db.Orders.Add(order)
            Else
                'Current Order Info
                Dim orderInDb = db.Orders.Include("OrderDetails").Single(Function(c) c.OrderId = order.OrderId)
                Dim orderDetailInDb = orderInDb.OrderDetails.ToList()

                'Update Order Info
                db.Orders.AddOrUpdate(order)
                'db.Entry(orderInDb).CurrentValues.SetValues(order)

                'Update or delete detail info
                For Each detailInDb In orderDetailInDb

                    Dim detail =
                            order.OrderDetails.SingleOrDefault(
                                Function(d) _
                                                                  d.OrderId = detailInDb.OrderId AndAlso
                                                                  d.OrderDetailId = detailInDb.OrderDetailId)

                    If detail Is Nothing Then
                        'Delete Detail Info
                        db.OrderDetails.Remove(detailInDb)
                    Else
                        'Udpate Detail Info
                        db.OrderDetails.AddOrUpdate(detail)
                        'db.Entry(detailInDb).CurrentValues.SetValues(detail)
                    End If
                Next

                'Add new detail info
                For Each detail In order.OrderDetails

                    If _
                        Not _
                        orderDetailInDb.Any(
                            Function(d) d.OrderId = detail.OrderId AndAlso d.OrderDetailId = detail.OrderDetailId) Then
                        db.OrderDetails.Add(detail)
                    End If
                Next
            End If

            Try
                db.SaveChanges()
            Catch ex As DbEntityValidationException
                For Each verr In ex.EntityValidationErrors
                    Debug.WriteLine("Entity of type ""{0}"" in state ""{1}"" has the following validation errors:",
                                    verr.Entry.Entity.[GetType]().Name, verr.Entry.State)
                    For Each ve In verr.ValidationErrors
                        Debug.WriteLine("- Property: ""{0}"", Error: ""{1}""", ve.PropertyName, ve.ErrorMessage)
                    Next
                Next
                Throw
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

    ''' <summary>
    '''     删除订单
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function DeleteOrderDto(orderId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteOrderDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            Dim order = db.Orders.Find(orderId)

            If IsNothing(order) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Orders.Remove(order)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     订单存在
    ''' </summary>
    ''' <param name="orderId"></param>
    ''' <returns></returns>
    Public Function OrderDtoExists(orderId As Integer) As Boolean Implements IOrderManagementService.OrderDtoExists
        Using db As New OrderManagementDbEntities
            Return db.Orders.Count(Function(order) order.OrderId = orderId) > 0
        End Using
    End Function

#End Region

#Region "Customer"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="customerDto"></param>
    ''' <returns></returns>
    Public Function AddCustomerDto(customerDto As CustomerDto) As ProcessResult _
        Implements IOrderManagementService.AddCustomerDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            db.Customers.Add(Mapper.Map(Of Customer)(customerDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If CustomerDtoExists(customerDto.CustomerId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function CustomerDtoExists(customerId As Integer) As Boolean _
        Implements IOrderManagementService.CustomerDtoExists
        Using db As New OrderManagementDbEntities
            Return db.Customers.Count(Function(customer) customer.CustomerId = customerId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function DeleteCustomerDto(customerId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteCustomerDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            Dim customer = db.Customers.Find(customerId)

            If IsNothing(customer) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Customers.Remove(customer)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <returns></returns>
    Public Function GetCustomerDto(customerId As Integer) As CustomerDto _
        Implements IOrderManagementService.GetCustomerDto
        Using db As New OrderManagementDbEntities
            Dim customer = db.Customers.Find(customerId)
            Return Mapper.Map(Of CustomerDto)(customer)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetCustomerDtoByCondition(condition As CustomerDto) As IEnumerable(Of CustomerDto) _
        Implements IOrderManagementService.GetCustomerDtoByCondition

        Using db As New OrderManagementDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of CustomerDto))(
                    db.Customers.Where(
                        Function(c) (.CustomerId = 0 OrElse c.CustomerId = .CustomerId) And
                                    (String.IsNullOrEmpty(.Name) OrElse c.Name = .Name) And
                                    (String.IsNullOrEmpty(.WechatName) OrElse c.Name = .WechatName) And
                                    (String.IsNullOrEmpty(.TaobaoName) OrElse c.Name = .TaobaoName) And
                                    (String.IsNullOrEmpty(.Address) OrElse c.Name = .Address) And
                                    (String.IsNullOrEmpty(.PostCode) OrElse c.Name = .PostCode) And
                                    (String.IsNullOrEmpty(.Phone) OrElse c.Name = .Phone)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    Public Function GetCustomerDtoes() As IEnumerable(Of CustomerDto) _
        Implements IOrderManagementService.GetCustomerDtoes

        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of List(Of CustomerDto))(db.Customers.ToList)
        End Using
    End Function

    Public Function UpdateCustomerDto(customerDto As CustomerDto) As ProcessResult _
        Implements IOrderManagementService.UpdateCustomerDto

        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            db.Entry(Mapper.Map(Of Customer)(customerDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#Region "Product"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="productDto"></param>
    ''' <returns></returns>
    Public Function AddProductDto(productDto As ProductDto) As ProcessResult _
        Implements IOrderManagementService.AddProductDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            db.Products.Add(Mapper.Map(Of Product)(productDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If ProductDtoExists(productDto.ProductId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function ProductDtoExists(productId As Integer) As Boolean _
        Implements IOrderManagementService.ProductDtoExists
        Using db As New OrderManagementDbEntities
            Return db.Products.Count(Function(product) product.ProductId = productId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function DeleteProductDto(productId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteProductDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            Dim product = db.Products.Find(productId)

            If IsNothing(product) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Products.Remove(product)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Function GetProductDto(productId As Integer) As ProductDto _
        Implements IOrderManagementService.GetProductDto
        Using db As New OrderManagementDbEntities
            Dim product = db.Products.Find(productId)
            Return Mapper.Map(Of ProductDto)(product)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetProductDtoByCondition(condition As ProductDto) As IEnumerable(Of ProductDto) _
        Implements IOrderManagementService.GetProductDtoByCondition

        Using db As New OrderManagementDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of ProductDto))(
                    db.Products.Where(
                        Function(c) (.ProductId = 0 OrElse c.ProductId = .ProductId) AndAlso
                                    (.SpeciesId = 0 OrElse c.SpeciesId = .SpeciesId) AndAlso
                                    (.BrandId = 0 OrElse c.BrandId = .BrandId) AndAlso
                                    (String.IsNullOrEmpty(.ProductName) OrElse c.ProductName = .ProductName) AndAlso
                                    (String.IsNullOrEmpty(.ProductNameJp) OrElse c.ProductNameJp = .ProductNameJp)).
                                                                    ToList()
                    )
                Return result
            End With
        End Using
    End Function

    Public Function GetProductDtoes() As IEnumerable(Of ProductDto) _
        Implements IOrderManagementService.GetProductDtoes

        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of List(Of ProductDto))(db.Products.ToList)
        End Using
    End Function

    Public Function UpdateProductDto(productDto As ProductDto) As ProcessResult _
        Implements IOrderManagementService.UpdateProductDto

        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            db.Entry(Mapper.Map(Of Product)(productDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#Region "Species"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="speciesDto"></param>
    ''' <returns></returns>
    Public Function AddSpeciesDto(speciesDto As SpeciesDto) As ProcessResult _
        Implements IOrderManagementService.AddSpeciesDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            db.Species.Add(Mapper.Map(Of Species)(speciesDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If SpeciesDtoExists(speciesDto.SpeciesId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="speciesId"></param>
    ''' <returns></returns>
    Public Function SpeciesDtoExists(speciesId As Integer) As Boolean _
        Implements IOrderManagementService.SpeciesDtoExists
        Using db As New OrderManagementDbEntities
            Return db.Species.Count(Function(species) species.SpeciesId = speciesId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="speciesId"></param>
    ''' <returns></returns>
    Public Function DeleteSpeciesDto(speciesId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteSpeciesDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            Dim species = db.Species.Find(speciesId)

            If IsNothing(species) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Species.Remove(species)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="speciesId"></param>
    ''' <returns></returns>
    Public Function GetSpeciesDto(speciesId As Integer) As SpeciesDto _
        Implements IOrderManagementService.GetSpeciesDto
        Using db As New OrderManagementDbEntities
            Dim species = db.Species.Find(speciesId)
            Return Mapper.Map(Of SpeciesDto)(species)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetSpeciesDtoByCondition(condition As SpeciesDto) As IEnumerable(Of SpeciesDto) _
        Implements IOrderManagementService.GetSpeciesDtoByCondition

        Using db As New OrderManagementDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of SpeciesDto))(
                    db.Species.Where(
                        Function(c) (.SpeciesId = 0 OrElse c.SpeciesId = .SpeciesId) AndAlso
                                    (String.IsNullOrEmpty(.SpeciesName) OrElse c.SpeciesName = .SpeciesName)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    Public Function GetSpeciesDtoes() As IEnumerable(Of SpeciesDto) _
        Implements IOrderManagementService.GetSpeciesDtoes

        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of List(Of SpeciesDto))(db.Species.ToList)
        End Using
    End Function

    Public Function UpdateSpeciesDto(speciesDto As SpeciesDto) As ProcessResult _
        Implements IOrderManagementService.UpdateSpeciesDto

        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            db.Entry(Mapper.Map(Of Species)(speciesDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#Region "Brand"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="brandDto"></param>
    ''' <returns></returns>
    Public Function AddBrandDto(brandDto As BrandDto) As ProcessResult _
        Implements IOrderManagementService.AddBrandDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            db.Brands.Add(Mapper.Map(Of Brand)(brandDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If BrandDtoExists(brandDto.BrandId) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="brandId"></param>
    ''' <returns></returns>
    Public Function BrandDtoExists(brandId As Integer) As Boolean _
        Implements IOrderManagementService.BrandDtoExists
        Using db As New OrderManagementDbEntities
            Return db.Brands.Count(Function(brand) brand.BrandId = brandId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="brandId"></param>
    ''' <returns></returns>
    Public Function DeleteBrandDto(brandId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteBrandDto
        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities
            Dim brand = db.Brands.Find(brandId)

            If IsNothing(brand) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Brands.Remove(brand)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="brandId"></param>
    ''' <returns></returns>
    Public Function GetBrandDto(brandId As Integer) As BrandDto _
        Implements IOrderManagementService.GetBrandDto
        Using db As New OrderManagementDbEntities
            Dim brand = db.Brands.Find(brandId)
            Return Mapper.Map(Of BrandDto)(brand)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetBrandDtoByCondition(condition As BrandDto) As IEnumerable(Of BrandDto) _
        Implements IOrderManagementService.GetBrandDtoByCondition

        Using db As New OrderManagementDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of BrandDto))(
                    db.Brands.Where(
                        Function(c) (.BrandId = 0 OrElse c.BrandId = .BrandId) AndAlso
                                    (String.IsNullOrEmpty(.BrandName) OrElse c.BrandName = .BrandName)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    Public Function GetBrandDtoes() As IEnumerable(Of BrandDto) _
        Implements IOrderManagementService.GetBrandDtoes

        Using db As New OrderManagementDbEntities
            Return Mapper.Map(Of List(Of BrandDto))(db.Brands.ToList)
        End Using
    End Function

    Public Function UpdateBrandDto(brandDto As BrandDto) As ProcessResult _
        Implements IOrderManagementService.UpdateBrandDto

        Dim result As New ProcessResult

        Using db As New OrderManagementDbEntities

            db.Entry(Mapper.Map(Of Brand)(brandDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#End Region

#Region "OrderBlog"

#Region "Category"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="categoryDto"></param>
    ''' <returns></returns>
    Public Function AddCategoryDto(categoryDto As CategoryDto) As ProcessResult _
        Implements IOrderManagementService.AddCategoryDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            db.Categories.Add(Mapper.Map(Of Category)(categoryDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If CategoryDtoExists(categoryDto.Id) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <returns></returns>
    Public Function CategoryDtoExists(categoryId As Integer) As Boolean _
        Implements IOrderManagementService.CategoryDtoExists
        Using db As New OrderBlogDbEntities
            Return db.Categories.Count(Function(category) category.Id = categoryId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <returns></returns>
    Public Function DeleteCategoryDto(categoryId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteCategoryDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            Dim category = db.Categories.Find(categoryId)

            If IsNothing(category) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Categories.Remove(category)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <returns></returns>
    Public Function GetCategoryDto(categoryId As Integer) As CategoryDto _
        Implements IOrderManagementService.GetCategoryDto
        Using db As New OrderBlogDbEntities
            Dim category = db.Categories.Find(categoryId)
            Return Mapper.Map(Of CategoryDto)(category)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetCategoryDtoByCondition(condition As CategoryDto) As IEnumerable(Of CategoryDto) _
        Implements IOrderManagementService.GetCategoryDtoByCondition

        Using db As New OrderBlogDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of CategoryDto))(
                    db.Categories.Where(
                        Function(c) (.Id = 0 OrElse c.Id = .Id) And
                                    (String.IsNullOrEmpty(.Name) OrElse c.Name = .Name) And
                                    (String.IsNullOrEmpty(.Description) OrElse c.Name = .Description) And
                                    (String.IsNullOrEmpty(.UrlSlug) OrElse c.Name = .UrlSlug)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    ''' <summary>
    '''     全件取得
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCategoryDtoes() As IEnumerable(Of CategoryDto) _
        Implements IOrderManagementService.GetCategoryDtoes

        Using db As New OrderBlogDbEntities
            Return Mapper.Map(Of List(Of CategoryDto))(db.Categories.ToList)
        End Using
    End Function

    ''' <summary>
    '''     更新
    ''' </summary>
    ''' <param name="categoryDto"></param>
    ''' <returns></returns>
    Public Function UpdateCategoryDto(categoryDto As CategoryDto) As ProcessResult _
        Implements IOrderManagementService.UpdateCategoryDto

        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities

            db.Entry(Mapper.Map(Of Category)(categoryDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#Region "Post"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="postDto"></param>
    ''' <returns></returns>
    Public Function AddPostDto(postDto As PostDto) As ProcessResult _
        Implements IOrderManagementService.AddPostDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            db.Posts.Add(Mapper.Map(Of Post)(postDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If PostDtoExists(postDto.Id) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="postId"></param>
    ''' <returns></returns>
    Public Function PostDtoExists(postId As Integer) As Boolean _
        Implements IOrderManagementService.PostDtoExists
        Using db As New OrderBlogDbEntities
            Return db.Posts.Count(Function(post) post.Id = postId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="postId"></param>
    ''' <returns></returns>
    Public Function DeletePostDto(postId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeletePostDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            Dim post = db.Posts.Find(postId)

            If IsNothing(post) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Posts.Remove(post)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="postId"></param>
    ''' <returns></returns>
    Public Function GetPostDto(postId As Integer) As PostDto _
        Implements IOrderManagementService.GetPostDto
        Using db As New OrderBlogDbEntities
            Dim post = db.Posts.Find(postId)
            Return Mapper.Map(Of PostDto)(post)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetPostDtoByCondition(condition As PostDto) As IEnumerable(Of PostDto) _
        Implements IOrderManagementService.GetPostDtoByCondition

        Using db As New OrderBlogDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of PostDto))(
                    db.Posts.Where(
                        Function(p) (.Id = 0 OrElse p.Id = .Id) And
                                    (String.IsNullOrEmpty(.Title) OrElse p.Title = .Title) And
                                    (String.IsNullOrEmpty(.ShortDescription) OrElse
                                     p.ShortDescription = .ShortDescription) And
                                    (String.IsNullOrEmpty(.Description) OrElse p.Description = .Description) And
                                    (String.IsNullOrEmpty(.Meta) OrElse p.Meta = .Meta) And
                                    (String.IsNullOrEmpty(.UrlSlug) OrElse p.UrlSlug = .UrlSlug) And
                                    (String.IsNullOrEmpty(.Published) OrElse p.Published = .Published) And
                                    (String.IsNullOrEmpty(.PostedOn) OrElse p.PostedOn = .PostedOn) And
                                    (String.IsNullOrEmpty(.Modified) OrElse p.Modified = .Modified) And
                                    (.CategoryId = 0 OrElse p.CategoryId = .CategoryId) And
                                    (String.IsNullOrEmpty(.Author) OrElse p.Author = .Author)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    ''' <summary>
    '''     最新を取得
    ''' </summary>
    ''' <param name="size"></param>
    ''' <returns></returns>
    Public Function GetLatestPostDtoes(size As Integer) As IEnumerable(Of PostDto) _
        Implements IOrderManagementService.GetLatestPostDtoes

        Using db As New OrderBlogDbEntities
            Return Mapper.Map(Of List(Of PostDto))(db.Posts.OrderByDescending(Function(p) p.PostedOn).Take(size))
        End Using
    End Function

    ''' <summary>
    '''     カテゴリによりデータの取得
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <returns></returns>
    Public Function GetPostDtoesByCategory(categoryId As Integer) As IEnumerable(Of PostDto) _
        Implements IOrderManagementService.GetPostDtoesByCategory

        Using db As New OrderBlogDbEntities
            Return _
                Mapper.Map(Of List(Of PostDto))(
                    db.Posts.Where(Function(p) p.CategoryId = categoryId).OrderByDescending(Function(p) p.PostedOn))
        End Using
    End Function

    ''' <summary>
    '''     全件取得
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPostDtoes() As IEnumerable(Of PostDto) _
        Implements IOrderManagementService.GetPostDtoes

        Using db As New OrderBlogDbEntities
            Return Mapper.Map(Of List(Of PostDto))(db.Posts.ToList)
        End Using
    End Function

    ''' <summary>
    '''     更新
    ''' </summary>
    ''' <param name="postDto"></param>
    ''' <returns></returns>
    Public Function UpdatePostDto(postDto As PostDto) As ProcessResult _
        Implements IOrderManagementService.UpdatePostDto

        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities

            db.Entry(Mapper.Map(Of Post)(postDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#Region "Tag"

    ''' <summary>
    '''     追加
    ''' </summary>
    ''' <param name="tagDto"></param>
    ''' <returns></returns>
    Public Function AddTagDto(tagDto As TagDto) As ProcessResult _
        Implements IOrderManagementService.AddTagDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            db.Tags.Add(Mapper.Map(Of Tag)(tagDto))

            Try
                db.SaveChanges()
            Catch ex As DbUpdateException
                result.IsSuccess = False

                If TagDtoExists(tagDto.Id) Then
                    result.ErrorMessage = "データ重複"
                Else
                    result.ErrorMessage = ex.Message
                End If
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     存在チェック
    ''' </summary>
    ''' <param name="tagId"></param>
    ''' <returns></returns>
    Public Function TagDtoExists(tagId As Integer) As Boolean _
        Implements IOrderManagementService.TagDtoExists
        Using db As New OrderBlogDbEntities
            Return db.Tags.Count(Function(tag) tag.Id = tagId) > 0
        End Using
    End Function

    ''' <summary>
    '''     削除
    ''' </summary>
    ''' <param name="tagId"></param>
    ''' <returns></returns>
    Public Function DeleteTagDto(tagId As Integer) As ProcessResult _
        Implements IOrderManagementService.DeleteTagDto
        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities
            Dim tag = db.Tags.Find(tagId)

            If IsNothing(tag) Then
                result.IsSuccess = False
                result.ErrorMessage = "无此数据"
                Return result
            End If

            db.Tags.Remove(tag)

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.Message
            End Try
        End Using
        Return result
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="tagId"></param>
    ''' <returns></returns>
    Public Function GetTagDto(tagId As Integer) As TagDto _
        Implements IOrderManagementService.GetTagDto
        Using db As New OrderBlogDbEntities
            Dim tag = db.Tags.Find(tagId)
            Return Mapper.Map(Of TagDto)(tag)
        End Using
    End Function

    ''' <summary>
    '''     取得
    ''' </summary>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    Public Function GetTagDtoByCondition(condition As TagDto) As IEnumerable(Of TagDto) _
        Implements IOrderManagementService.GetTagDtoByCondition

        Using db As New OrderBlogDbEntities
            With condition
                Dim result = Mapper.Map(Of List(Of TagDto))(
                    db.Tags.Where(
                        Function(tag) (.Id = 0 OrElse tag.Id = .Id) And
                                      (String.IsNullOrEmpty(.Name) OrElse tag.Name = .Name) And
                                      (String.IsNullOrEmpty(.Description) OrElse tag.Name = .Description) And
                                      (String.IsNullOrEmpty(.UrlSlug) OrElse tag.Name = .UrlSlug)).ToList()
                    )
                Return result
            End With
        End Using
    End Function

    ''' <summary>
    '''     全件取得
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTagDtoes() As IEnumerable(Of TagDto) _
        Implements IOrderManagementService.GetTagDtoes

        Using db As New OrderBlogDbEntities
            Return Mapper.Map(Of List(Of TagDto))(db.Tags.ToList)
        End Using
    End Function

    ''' <summary>
    '''     更新
    ''' </summary>
    ''' <param name="tagDto"></param>
    ''' <returns></returns>
    Public Function UpdateTagDto(tagDto As TagDto) As ProcessResult _
        Implements IOrderManagementService.UpdateTagDto

        Dim result As New ProcessResult

        Using db As New OrderBlogDbEntities

            db.Entry(Mapper.Map(Of Tag)(tagDto)).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As Exception
                result.IsSuccess = False
                result.ErrorMessage = ex.ToString()
            End Try
        End Using

        Return result
    End Function

#End Region

#End Region
End Class
