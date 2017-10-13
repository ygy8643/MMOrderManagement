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
    Function DeleteOrderDto(orderId As String) As ProcessResult

    <OperationContract>
    Function OrderDtoExists(orderId As String) As Boolean

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
    Function DeleteCustomerDto(customerId As String) As ProcessResult

    <OperationContract>
    Function CustomerDtoExists(customerId As String) As Boolean

#End Region
End Interface

