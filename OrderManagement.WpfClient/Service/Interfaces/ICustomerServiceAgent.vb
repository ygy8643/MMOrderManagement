Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Common

Namespace Service
    Public Interface ICustomerServiceAgent

#Region "取得"

        ''' <summary>
        '''     取得顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetCustomerComboBoxList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     取得顾客名称
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Function GetCustomerName(customerId As Integer) As String

        ''' <summary>
        '''     取得客户信息
        ''' </summary>
        ''' <param name="customerId"></param>
        ''' <returns></returns>
        Function GetCustomer(customerId As String) As CustomerClient

        ''' <summary>
        ''' 根据条件取得顾客信息
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Function GetCustomerByCondition(condition As CustomerClient) As IEnumerable(Of CustomerClient)

#End Region

#Region "追加"

        Function CreateCustomer(customer As CustomerClient) As ProcessResult

#End Region

#Region "更新"

        Function UpdateCustomer(customer As CustomerClient) As ProcessResult

#End Region

#Region "削除"

        Function DeleteCustomer(customerId As String) As ProcessResult

#End Region

#Region "存在チェック"

        Function CustomerExists(customerId As String) As Boolean

#End Region

    End Interface
End Namespace