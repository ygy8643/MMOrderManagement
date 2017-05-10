Imports OrderManagement.Client.Entities

Namespace Service

    Public Interface IOrderService

        Function GetOrders() As IEnumerable(Of OrderClient)

    End Interface
End Namespace

