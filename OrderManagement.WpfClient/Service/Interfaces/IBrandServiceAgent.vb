Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Common

Namespace Service
    Public Interface IBrandServiceAgent

#Region "取得"

        ''' <summary>
        '''     取得顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetBrandComboBoxList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     取得顾客名称
        ''' </summary>
        ''' <param name="brandId"></param>
        ''' <returns></returns>
        Function GetBrandName(brandId As Integer) As String

        ''' <summary>
        '''     取得客户信息
        ''' </summary>
        ''' <param name="brandId"></param>
        ''' <returns></returns>
        Function GetBrand(brandId As Integer) As BrandClient

        ''' <summary>
        ''' 根据条件取得顾客信息
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Function GetBrandByCondition(condition As BrandClient) As IEnumerable(Of BrandClient)

#End Region

#Region "追加"

        Function CreateBrand(brand As BrandClient) As ProcessResult

#End Region

#Region "更新"

        Function UpdateBrand(brand As BrandClient) As ProcessResult

#End Region

#Region "削除"

        Function DeleteBrand(brandId As Integer) As ProcessResult

#End Region

#Region "存在チェック"

        Function BrandExists(brandId As Integer) As Boolean

#End Region

    End Interface
End Namespace