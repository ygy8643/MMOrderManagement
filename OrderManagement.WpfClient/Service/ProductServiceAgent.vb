Imports AutoMapper
Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class ProductServiceAgent
        Implements IProductServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

#Region "取得"

        ''' <summary>
        '''     取得用户信息
        ''' </summary>
        ''' <param name="productId"></param>
        ''' <returns></returns>
        Public Function GetProduct(productId As Integer) As ProductClient Implements IProductServiceAgent.GetProduct
            Return Mapper.Map(Of ProductClient)(_service.GetProductDto(productId))
        End Function

        ''' <summary>
        ''' 根据条件检索数据
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Public Function GetProductByCondition(condition As ProductClient) As IEnumerable(Of ProductClient) Implements IProductServiceAgent.GetProductByCondition
            Return Mapper.Map(Of List(Of ProductClient))(_service.GetProductDtoByCondition(Mapper.Map(Of ProductDto)(condition)))
        End Function

        ''' <summary>
        '''     取得用户列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetProductComboBoxList() As IEnumerable(Of ValueNamePair) _
            Implements IProductServiceAgent.GetProductComboBoxList

            Dim lstProduct = _service.GetProductDtoes()

            Dim query = From ctx In lstProduct
                        Select New ValueNamePair With {.Value = ctx.ProductId, .DisplayName = ctx.ProductName}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     取得用户名称
        ''' </summary>
        ''' <param name="productId"></param>
        ''' <returns></returns>
        Public Function GetProductName(productId As Integer) As String _
            Implements IProductServiceAgent.GetProductName

            Dim product = _service.GetProductDto(productId)

            If product IsNot Nothing Then
                Return product.ProductName
            Else
                Return String.Empty
            End If
        End Function

#End Region

#Region "追加"

        Public Function CreateProduct(product As ProductClient) As ProcessResult Implements IProductServiceAgent.CreateProduct
            Return _service.AddProductDto(Mapper.Map(Of ProductDto)(product))
        End Function

#End Region

#Region "更新"

        Public Function UpdateProduct(product As ProductClient) As ProcessResult Implements IProductServiceAgent.UpdateProduct
            Return _service.UpdateProductDto(Mapper.Map(Of ProductDto)(product))
        End Function

#End Region

#Region "削除"

        Public Function DeleteProduct(productId As Integer) As ProcessResult Implements IProductServiceAgent.DeleteProduct
            Return _service.DeleteProductDto(productId)
        End Function

#End Region

#Region "存在チェック"

        Public Function ProductExists(productId As Integer) As Boolean Implements IProductServiceAgent.ProductExists
            Return _service.ProductDtoExists(productId)
        End Function

#End Region

    End Class
End Namespace