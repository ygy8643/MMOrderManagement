Imports OrderManagement.Common
Imports OrderManagement.WcfService.Dto.OrderBlog
Imports OrderManagement.WcfService.Dto.OrderManagement

<ServiceContract>
Public Interface IOrderManagementService

#Region "OrderManagement"

#Region "Order"

    <OperationContract>
    Function GetOrderDto(orderId As Integer) As OrderDto

    <OperationContract>
    Function GetOrderDtoes() As List(Of OrderDto)

    <OperationContract>
    Function GetOrderDtoesByConditions(conditions As OrderSearchConditionsDto) As List(Of OrderDto)

    <OperationContract>
    Function AddOrderDto(orderDto As OrderDto) As ProcessResult

    <OperationContract>
    Function UpdateOrderDto(orderDto As OrderDto) As ProcessResult

    <OperationContract>
    Function AddOrUpdateOrderDto(orderDto As OrderDto) As ProcessResult

    <OperationContract>
    Function DeleteOrderDto(orderId As Integer) As ProcessResult

    <OperationContract>
    Function OrderDtoExists(orderId As Integer) As Boolean

#End Region

#Region "Customer"

    <OperationContract>
    Function GetCustomerDtoes() As IEnumerable(Of CustomerDto)

    <OperationContract>
    Function GetCustomerDto(customerId As Integer) As CustomerDto

    <OperationContract>
    Function GetCustomerDtoByCondition(condition As CustomerDto) As IEnumerable(Of CustomerDto)

    <OperationContract>
    Function AddCustomerDto(customerDto As CustomerDto) As ProcessResult

    <OperationContract>
    Function UpdateCustomerDto(customerDto As CustomerDto) As ProcessResult

    <OperationContract>
    Function DeleteCustomerDto(customerId As Integer) As ProcessResult

    <OperationContract>
    Function CustomerDtoExists(customerId As Integer) As Boolean

#End Region

#Region "Product"

    <OperationContract>
    Function GetProductDtoes() As IEnumerable(Of ProductDto)

    <OperationContract>
    Function GetProductDto(productId As Integer) As ProductDto

    <OperationContract>
    Function GetProductDtoByCondition(condition As ProductDto) As IEnumerable(Of ProductDto)

    <OperationContract>
    Function AddProductDto(productDto As ProductDto) As ProcessResult

    <OperationContract>
    Function UpdateProductDto(productDto As ProductDto) As ProcessResult

    <OperationContract>
    Function DeleteProductDto(productId As Integer) As ProcessResult

    <OperationContract>
    Function ProductDtoExists(productId As Integer) As Boolean

#End Region

#Region "Species"

    <OperationContract>
    Function GetSpeciesDtoes() As IEnumerable(Of SpeciesDto)

    <OperationContract>
    Function GetSpeciesDto(speciesId As Integer) As SpeciesDto

    <OperationContract>
    Function GetSpeciesDtoByCondition(condition As SpeciesDto) As IEnumerable(Of SpeciesDto)

    <OperationContract>
    Function AddSpeciesDto(speciesDto As SpeciesDto) As ProcessResult

    <OperationContract>
    Function UpdateSpeciesDto(speciesDto As SpeciesDto) As ProcessResult

    <OperationContract>
    Function DeleteSpeciesDto(speciesId As Integer) As ProcessResult

    <OperationContract>
    Function SpeciesDtoExists(speciesId As Integer) As Boolean

#End Region

#Region "Brand"

    <OperationContract>
    Function GetBrandDtoes() As IEnumerable(Of BrandDto)

    <OperationContract>
    Function GetBrandDto(brandId As Integer) As BrandDto

    <OperationContract>
    Function GetBrandDtoByCondition(condition As BrandDto) As IEnumerable(Of BrandDto)

    <OperationContract>
    Function AddBrandDto(brandDto As BrandDto) As ProcessResult

    <OperationContract>
    Function UpdateBrandDto(brandDto As BrandDto) As ProcessResult

    <OperationContract>
    Function DeleteBrandDto(brandId As Integer) As ProcessResult

    <OperationContract>
    Function BrandDtoExists(brandId As Integer) As Boolean

#End Region

#End Region

#Region "OrderBlog"

#Region "Category"

    <OperationContract>
    Function GetCategoryDtoes() As IEnumerable(Of CategoryDto)

    <OperationContract>
    Function GetCategoryDto(categoryId As Integer) As CategoryDto

    <OperationContract>
    Function GetCategoryDtoByCondition(condition As CategoryDto) As IEnumerable(Of CategoryDto)

    <OperationContract>
    Function AddCategoryDto(categoryDto As CategoryDto) As ProcessResult

    <OperationContract>
    Function UpdateCategoryDto(categoryDto As CategoryDto) As ProcessResult

    <OperationContract>
    Function DeleteCategoryDto(categoryId As Integer) As ProcessResult

    <OperationContract>
    Function CategoryDtoExists(categoryId As Integer) As Boolean

#End Region

#Region "Post"

    <OperationContract>
    Function GetPostDtoes() As IEnumerable(Of PostDto)

    <OperationContract>
    Function GetPostDto(postId As Integer) As PostDto

    <OperationContract>
    Function GetPostDtoByCondition(condition As PostDto) As IEnumerable(Of PostDto)

    <OperationContract>
    Function GetLatestPostDtoes(size As Integer) As IEnumerable(Of PostDto)

    <OperationContract>
    Function GetPostDtoesByCategory(categoryId As Integer) As IEnumerable(Of PostDto)
    
    <OperationContract>
    Function GetPostDtoesByTitle(searchString As string) As IEnumerable(Of PostDto)

    <OperationContract>
    Function AddPostDto(postDto As PostDto) As ProcessResult

    <OperationContract>
    Function UpdatePostDto(postDto As PostDto) As ProcessResult

    <OperationContract>
    Function DeletePostDto(postId As Integer) As ProcessResult

    <OperationContract>
    Function PostDtoExists(postId As Integer) As Boolean

#End Region

#Region "Tag"

    <OperationContract>
    Function GetTagDtoes() As IEnumerable(Of TagDto)

    <OperationContract>
    Function GetTagDto(tagId As Integer) As TagDto

    <OperationContract>
    Function GetTagDtoByCondition(condition As TagDto) As IEnumerable(Of TagDto)

    <OperationContract>
    Function AddTagDto(tagDto As TagDto) As ProcessResult

    <OperationContract>
    Function UpdateTagDto(tagDto As TagDto) As ProcessResult

    <OperationContract>
    Function DeleteTagDto(tagId As Integer) As ProcessResult

    <OperationContract>
    Function TagDtoExists(tagId As Integer) As Boolean

#End Region

#End Region
End Interface

