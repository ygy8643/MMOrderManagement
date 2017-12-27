Imports AutoMapper
Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class BrandServiceAgent
        Implements IBrandServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

#Region "取得"

        ''' <summary>
        '''     取得用户信息
        ''' </summary>
        ''' <param name="brandId"></param>
        ''' <returns></returns>
        Public Function GetBrand(brandId As Integer) As BrandClient Implements IBrandServiceAgent.GetBrand
            Return Mapper.Map(Of BrandClient)(_service.GetBrandDto(brandId))
        End Function

        ''' <summary>
        ''' 根据条件检索数据
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Public Function GetBrandByCondition(condition As BrandClient) As IEnumerable(Of BrandClient) Implements IBrandServiceAgent.GetBrandByCondition
            Return Mapper.Map(Of List(Of BrandClient))(_service.GetBrandDtoByCondition(Mapper.Map(Of BrandDto)(condition)))
        End Function

        ''' <summary>
        '''     取得用户列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetBrandComboBoxList() As IEnumerable(Of ValueNamePair) _
            Implements IBrandServiceAgent.GetBrandComboBoxList

            Dim lstBrand = _service.GetBrandDtoes()

            Dim query = From ctx In lstBrand
                        Select New ValueNamePair With {.Value = ctx.BrandId, .DisplayName = ctx.BrandName}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     取得用户名称
        ''' </summary>
        ''' <param name="brandId"></param>
        ''' <returns></returns>
        Public Function GetBrandName(brandId As Integer) As String _
            Implements IBrandServiceAgent.GetBrandName

            Dim brand = _service.GetBrandDto(brandId)

            If brand IsNot Nothing Then
                Return brand.BrandName
            Else
                Return String.Empty
            End If
        End Function

#End Region

#Region "追加"

        Public Function CreateBrand(brand As BrandClient) As ProcessResult Implements IBrandServiceAgent.CreateBrand
            Return _service.AddBrandDto(Mapper.Map(Of BrandDto)(brand))
        End Function

#End Region

#Region "更新"

        Public Function UpdateBrand(brand As BrandClient) As ProcessResult Implements IBrandServiceAgent.UpdateBrand
            Return _service.UpdateBrandDto(Mapper.Map(Of BrandDto)(brand))
        End Function

#End Region

#Region "削除"

        Public Function DeleteBrand(brandId As Integer) As ProcessResult Implements IBrandServiceAgent.DeleteBrand
            Return _service.DeleteBrandDto(brandId)
        End Function

#End Region

#Region "存在チェック"

        Public Function BrandExists(brandId As Integer) As Boolean Implements IBrandServiceAgent.BrandExists
            Return _service.BrandDtoExists(brandId)
        End Function

#End Region

    End Class
End Namespace