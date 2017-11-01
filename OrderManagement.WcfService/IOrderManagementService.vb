Imports OrderManagement.Common
Imports OrderManagement.WcfService.Dto

<ServiceContract>
Public Interface IOrderManagementService

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
End Interface

