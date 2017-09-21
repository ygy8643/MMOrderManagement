Imports OrderManagement.WcfService.Dto
Imports OrderManagement.WcfService.Results

<ServiceContract()>
Public Interface IOrderManagementService

#Region "Order"

    <OperationContract>
    Function GetOrderDto(orderId As Integer) As OrderDto

    <OperationContract>
    Function GetOrderDtoes() As List(Of OrderDto)

    <OperationContract>
    Function GetOrderDtoesByConditions(conditions As OrderSearchConditionsDto) As List(Of OrderDto)

#End Region

#Region "Customer"

    <OperationContract>
    Function GetCustomerDtoes() As IEnumerable(Of CustomerDto)

    <OperationContract>
    Function GetCustomerDto(customerId As String) As CustomerDto

    <OperationContract>
    Function GetCustomerDtoByCondition(condition As CustomerDto) As IEnumerable(Of CustomerDto)

    <OperationContract>
    Function AddCustomerDto(leadTimeDto As CustomerDto) As DbProcessResult

    <OperationContract>
    Function UpdateCustomerDto(leadTimeDto As CustomerDto) As DbProcessResult

    <OperationContract>
    Function DeleteCustomerDto(customerId As String) As DbProcessResult

    <OperationContract>
    Function CustomerDtoExists(customerId As String) As Boolean

#End Region

End Interface

