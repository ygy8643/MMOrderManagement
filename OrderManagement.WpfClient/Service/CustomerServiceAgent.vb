Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class CustomerServiceAgent
        Implements ICustomerServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair) Implements ICustomerServiceAgent.GetCustomerComboBoxList
            Dim result As New List(Of ValueNamePair)

            Dim lstCustomer = _service.GetCustomerDtoes()

            Dim query = From ctx In lstCustomer
                        Select New ValueNamePair With {.Value = ctx.CustomerId, .DisplayName = ctx.Name}

            result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = String.Empty, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Public Function GetCustomerName(customerId As Integer) As String Implements ICustomerServiceAgent.GetCustomerName

            Dim customer = _service.GetCustomerDto(customerId)

            If customer IsNot Nothing Then
                Return customer.Name
            Else
                Return String.Empty
            End If
        End Function
    End Class
End Namespace