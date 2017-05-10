Imports OrderManagement.Entities
Imports OrderManagement.WcfService.Dto

<ServiceContract()>
Public Interface IOrderManagementService

    <OperationContract>
    Function GetOrderDtoes() As List(Of OrderDto)

End Interface

