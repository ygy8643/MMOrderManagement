Imports System.ComponentModel
Imports PropertyChanged

Namespace Models.OrderManagement

    <ImplementPropertyChanged>
    Public Class CustomerClient
        <DisplayName("客户编号")>
        Public Property CustomerId As Integer

        <DisplayName("客户姓名")>
        Public Property Name As String

        <DisplayName("收货地址")>
        Public Property Address As String

        <DisplayName("邮编")>
        Public Property PostCode As String

        <DisplayName("电话号码")>
        Public Property Phone As String

        <DisplayName("微信昵称")>
        Public Property WechatName As String

        <DisplayName("淘宝昵称")>
        Public Property TaobaoName As String
    End Class
End Namespace