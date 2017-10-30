Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class CustomerServiceAgent
        Implements ICustomerServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

#Region "取得"

        ''' <summary>
        '''     取得用户信息
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Public Function GetCustomer(customerId As String) As CustomerClient Implements ICustomerServiceAgent.GetCustomer
            Return Mapper.Map(Of CustomerClient)(_service.GetCustomerDto(customerId))
        End Function

        ''' <summary>
        ''' 根据条件检索数据
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Public Function GetCustomerByCondition(condition As CustomerClient) As IEnumerable(Of CustomerClient) Implements ICustomerServiceAgent.GetCustomerByCondition
            Return Mapper.Map(Of List(Of CustomerClient))(_service.GetCustomerDtoByCondition(Mapper.Map(Of CustomerDto)(condition)))
        End Function

        ''' <summary>
        '''     取得用户列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair) _
            Implements ICustomerServiceAgent.GetCustomerComboBoxList
            Dim result As New List(Of ValueNamePair)

            Dim lstCustomer = _service.GetCustomerDtoes()

            Dim query = From ctx In lstCustomer
                        Select New ValueNamePair With {.Value = ctx.CustomerId, .DisplayName = ctx.Name}

            result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = String.Empty, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     取得用户名称
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Public Function GetCustomerName(customerId As Integer) As String _
            Implements ICustomerServiceAgent.GetCustomerName

            Dim customer = _service.GetCustomerDto(customerId)

            If customer IsNot Nothing Then
                Return customer.Name
            Else
                Return String.Empty
            End If
        End Function

#End Region

#Region "追加"

        Public Function CreateCustomer(customer As CustomerClient) As ProcessResult Implements ICustomerServiceAgent.CreateCustomer
            Return _service.AddCustomerDto(Mapper.Map(Of CustomerDto)(customer))
        End Function

#End Region

#Region "更新"

        Public Function UpdateCustomer(customer As CustomerClient) As ProcessResult Implements ICustomerServiceAgent.UpdateCustomer
            Return _service.UpdateCustomerDto(Mapper.Map(Of CustomerDto)(customer))
        End Function

#End Region

#Region "削除"

        Public Function DeleteCustomer(customerId As String) As ProcessResult Implements ICustomerServiceAgent.DeleteCustomer
            Return _service.DeleteCustomerDto(customerId)
        End Function

#End Region

#Region "存在チェック"

        Public Function CustomerExists(customerId As String) As Boolean Implements ICustomerServiceAgent.CustomerExists
            Return _service.CustomerDtoExists(customerId)
        End Function

#End Region

    End Class
End Namespace