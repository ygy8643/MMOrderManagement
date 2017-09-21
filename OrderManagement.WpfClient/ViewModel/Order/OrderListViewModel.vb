Imports System.Data
Imports GalaSoft.MvvmLight.CommandWpf
Imports Microsoft.Win32
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Client.Entities.SearchConditions
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.Service

Namespace ViewModel.Order

    Public Class OrderListViewModel
        Inherits MyViewModelBase

#Region "Fields"

        ''' <summary>
        ''' 订单service
        ''' </summary>
        Private ReadOnly _orderService As IOrderService

#End Region

#Region "Properties"

        ''' <summary>
        ''' 标题
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Title As String
            Get
                Return "订单管理"
            End Get
        End Property

        ''' <summary>
        ''' 订单信息
        ''' </summary>
        Private _orders As IEnumerable(Of OrderClient)

        Public Property Orders() As IEnumerable(Of OrderClient)
            Get
                Return _orders
            End Get
            Set(ByVal value As IEnumerable(Of OrderClient))
                [Set]("Orders", _orders, value)
            End Set
        End Property

        ''' <summary>
        ''' 检索条件
        ''' </summary>
        Private _searchCondition As OrderSearchConditionsClient

        Public Property SearchCondition As OrderSearchConditionsClient
            Get
                Return _searchCondition
            End Get
            Set(value As OrderSearchConditionsClient)
                [Set]("SearchCondition", _searchCondition, value)
            End Set
        End Property

        ''' <summary>
        ''' 选择的订单
        ''' </summary>
        Private _selectedOrder As OrderClient

        Public Property SelectedOrder() As OrderClient
            Get
                Return _selectedOrder
            End Get
            Set(ByVal value As OrderClient)
                [Set]("SelectedOrder", _selectedOrder, value)
            End Set
        End Property

#Region "Lists"

        ''' <summary>
        ''' 顧客リスト
        ''' </summary>
        Public Property CustomerList As List(Of ValueNamePair)

#End Region

#End Region

#Region "Commands"

        ''' <summary>
        ''' 查询订单命令
        ''' </summary>
        ''' <returns></returns>
        Public Property SearchOrderCommand As RelayCommand

        ''' <summary>
        ''' 读取订单信息
        ''' </summary>
        ''' <returns></returns>
        Public Property LoadOrderCommand As RelayCommand

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New(orderService As IOrderService)

            _orderService = orderService

            SearchCondition = New OrderSearchConditionsClient()

            With "Lists"
                CustomerList = _orderService.GetCustomerComboBoxList()
            End With

            With "Commands"

                SearchOrderCommand = New RelayCommand(AddressOf SearchOrder)

                LoadOrderCommand = New RelayCommand(AddressOf LoadOrder)
            End With
        End Sub
#End Region

#Region "Methods"

        ''' <summary>
        ''' 查询订单
        ''' </summary>
        Private Sub SearchOrder()

            Orders = _orderService.GetOrdersByConditions(SearchCondition)

        End Sub

        ''' <summary>
        ''' 读取订单
        ''' </summary>
        Private Sub LoadOrder()
            Dim openFileSelector As New OpenFileDialog

            openFileSelector.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            If openFileSelector.ShowDialog() = True Then

                Dim filePath As String = openFileSelector.FileName

                'Load Excel file
                Dim dsExcel As DataSet = Import(filePath)

                'Convert Excel data to Entities
                Dim lstOrder As List(Of OrderClient) = ConvertExcelData(dsExcel)

            End If
        End Sub

        ''' <summary>
        ''' Load Excel file
        ''' </summary>
        ''' <param name="strImportFile">File Name</param>
        ''' <returns></returns>
        Public Function Import(ByVal strImportFile As String) As DataSet

            Dim dsResult As New DataSet
            Dim strConn As String = String.Empty

            'EXCEL接続文字列
            If IO.Path.GetExtension(strImportFile).ToLower.Equals(".xls") Then
                strConn = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=NO;IMEX=1;'", strImportFile)
            Else
                strConn = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1;'", strImportFile)
            End If

            'ファイルを開く
            Dim connExcel As New OleDb.OleDbConnection(strConn)
            connExcel.Open()

            'ファイルを読取
            Dim schemaTable As DataTable = connExcel.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)

            '各シートを取得
            For Each schemaRow As DataRow In schemaTable.Rows
                Dim dtExcel As New DataTable
                Dim strSheetName As String = schemaRow.Item("TABLE_NAME")

                If strSheetName.EndsWith("$") Then
                    Dim query As String = "SELECT * FROM [" & strSheetName & "]"
                    Dim daExcel As New OleDb.OleDbDataAdapter(query, connExcel)

                    dtExcel.Locale = Globalization.CultureInfo.CurrentCulture
                    dtExcel.TableName = strSheetName.Replace("$", "")
                    daExcel.Fill(dtExcel)

                    If dtExcel.Rows.Count > 1 Then
                        '第一行を列名に変換する
                        Dim dr As DataRow = dtExcel.Rows(0)

                        For Each dc As DataColumn In dtExcel.Columns
                            Dim strhead As String = String.Empty

                            strhead = IIf(dr.Item(dc.ColumnName).Equals(DBNull.Value) _
                                          Or dr.Item(dc.ColumnName) Is Nothing, String.Empty, dr.Item(dc.ColumnName).ToString.Trim)
                            If Not strhead.Equals(String.Empty) Then
                                dc.ColumnName = strhead
                            End If
                        Next

                        dtExcel.Rows.RemoveAt(0)

                        '情報を追加
                        dsResult.Tables.Add(dtExcel)

                    End If

                End If
            Next

            connExcel.Close()

            Return dsResult
        End Function

        ''' <summary>
        ''' Convert Excel data to Entities
        ''' </summary>
        ''' <param name="dsExcel">Excel Data</param>
        ''' <returns></returns>
        Private Function ConvertExcelData(dsExcel As DataSet) As List(Of OrderClient)

            Try
                Dim result As New List(Of OrderClient)

                'Excel Data
                Dim dtExcel As DataTable = dsExcel.Tables(0)

                '订单分割
                Dim lstDevide As New List(Of Integer)

                For index = 0 To dtExcel.Rows.Count - 1

                    Dim dr As DataRow = dtExcel.Rows(index)

                    If dr.Item("C1").ToString = "分类" Then

                        '记录分类行用以分割订单
                        lstDevide.Add(index)

                    End If

                Next

                '添加订单信息
                For indexDevide = 0 To lstDevide.Count - 1

                    Dim devide = lstDevide(indexDevide)
                    Dim nextDevide = lstDevide(indexDevide + 1)
                    Dim order As New OrderClient With {.ShipDate = "2016/" & dtExcel.Rows(devide - 1).Item("C1").ToString.Split(Space(1))(0)}

                    '用户信息
                    Dim customer As New CustomerClient With {.Name = dtExcel.Rows(devide - 1).Item("C1").ToString.Split(Space(1))(1)}
                    order.CustomerClient = customer

                    For index = devide + 1 To nextDevide - 2
                        '订单信息
                        Dim orderDetail As New OrderDetailClient With {
                            .ProductClient = New ProductClient With {.ProductName = dtExcel.Rows(index).Item("C1")},
                            .Quantity = dtExcel.Rows(index).Item("C2"),
                            .PurchasePrice = dtExcel.Rows(index).Item("C3"),
                            .SoldPrice = dtExcel.Rows(index).Item("C4")
                        }

                        order.OrderDetailClients.Add(orderDetail)
                    Next

                    result.Add(order)

                Next

                Return result
            Catch ex As Exception
                Dim strError = ex.Message


            End Try
        End Function

#End Region

    End Class
End Namespace
