Imports OrderManagement.Common

Namespace Service
    Public Interface ICustomerServiceAgent
        ''' <summary>
        '''     顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     顾客名称
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Function GetCustomerName(customerId As Integer) As String
    End Interface
End Namespace