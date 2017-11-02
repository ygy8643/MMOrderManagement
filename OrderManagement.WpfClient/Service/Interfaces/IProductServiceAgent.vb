Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common

Namespace Service
    Public Interface IProductServiceAgent

#Region "取得"

        ''' <summary>
        '''     取得顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetProductComboBoxList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     取得顾客名称
        ''' </summary>
        ''' <param name="productId"></param>
        ''' <returns></returns>
        Function GetProductName(productId As Integer) As String

        ''' <summary>
        '''     取得客户信息
        ''' </summary>
        ''' <param name="productId"></param>
        ''' <returns></returns>
        Function GetProduct(productId As Integer) As ProductClient

        ''' <summary>
        ''' 根据条件取得顾客信息
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Function GetProductByCondition(condition As ProductClient) As IEnumerable(Of ProductClient)

#End Region

#Region "追加"

        Function CreateProduct(product As ProductClient) As ProcessResult

#End Region

#Region "更新"

        Function UpdateProduct(product As ProductClient) As ProcessResult

#End Region

#Region "削除"

        Function DeleteProduct(productId As Integer) As ProcessResult

#End Region

#Region "存在チェック"

        Function ProductExists(productId As Integer) As Boolean

#End Region

    End Interface
End Namespace