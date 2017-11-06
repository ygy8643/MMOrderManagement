Imports OrderManagement.Common
Imports OrderManagement.Common.Define
Imports OrderManagement.WpfClient.Service.Interfaces

Namespace ViewModel
    ''' <summary>
    ''' 列表集合
    ''' </summary>
    Public Class ConstantListsViewModel

        ''' <summary>
        '''     リストservice
        ''' </summary>
        Private ReadOnly _listServiceAgent As IListServiceAgent

#Region "Lists"

        ''' <summary>
        '''     用户列表
        ''' </summary>
        Public Property CustomerList As List(Of ValueNamePair)

        ''' <summary>
        '''     订单类型列表
        ''' </summary>
        Public Property OrderTypeList As List(Of ValueNamePair)

        ''' <summary>
        '''     产品列表
        ''' </summary>
        Public Property ProductList As List(Of ValueNamePair)

#End Region

#Region "Constructor"

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="listServiceAgent"></param>
        Public Sub New(listServiceAgent As IListServiceAgent)
            _listServiceAgent = listServiceAgent
            
            With "Lists"

                '用户列表
                CustomerList = _listServiceAgent.GetCustomerList()

                '订单类型列表
                OrderTypeList = ConstantLists.OrderTypeList

                '产品列表
                ProductList = _listServiceAgent.GetProductList()

            End With

        End Sub
#End Region

    End Class
End Namespace