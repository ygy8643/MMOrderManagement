Imports AutoMapper
Imports OrderManagement.Client.Entities.Models
Imports OrderManagement.Common
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class SpeciesServiceAgent
        Implements ISpeciesServiceAgent

        Private ReadOnly _service As New OrderManagementServiceClient

#Region "取得"

        ''' <summary>
        '''     取得用户信息
        ''' </summary>
        ''' <param name="speciesId"></param>
        ''' <returns></returns>
        Public Function GetSpecies(speciesId As Integer) As SpeciesClient Implements ISpeciesServiceAgent.GetSpecies
            Return Mapper.Map(Of SpeciesClient)(_service.GetSpeciesDto(speciesId))
        End Function

        ''' <summary>
        ''' 根据条件检索数据
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Public Function GetSpeciesByCondition(condition As SpeciesClient) As IEnumerable(Of SpeciesClient) Implements ISpeciesServiceAgent.GetSpeciesByCondition
            Return Mapper.Map(Of List(Of SpeciesClient))(_service.GetSpeciesDtoByCondition(Mapper.Map(Of SpeciesDto)(condition)))
        End Function

        ''' <summary>
        '''     取得用户列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetSpeciesComboBoxList() As IEnumerable(Of ValueNamePair) _
            Implements ISpeciesServiceAgent.GetSpeciesComboBoxList

            Dim lstSpecies = _service.GetSpeciesDtoes()

            Dim query = From ctx In lstSpecies
                        Select New ValueNamePair With {.Value = ctx.SpeciesId, .DisplayName = ctx.SpeciesName}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     取得用户名称
        ''' </summary>
        ''' <param name="speciesId"></param>
        ''' <returns></returns>
        Public Function GetSpeciesName(speciesId As Integer) As String _
            Implements ISpeciesServiceAgent.GetSpeciesName

            Dim species = _service.GetSpeciesDto(speciesId)

            If species IsNot Nothing Then
                Return species.SpeciesName
            Else
                Return String.Empty
            End If
        End Function

#End Region

#Region "追加"

        Public Function CreateSpecies(species As SpeciesClient) As ProcessResult Implements ISpeciesServiceAgent.CreateSpecies
            Return _service.AddSpeciesDto(Mapper.Map(Of SpeciesDto)(species))
        End Function

#End Region

#Region "更新"

        Public Function UpdateSpecies(species As SpeciesClient) As ProcessResult Implements ISpeciesServiceAgent.UpdateSpecies
            Return _service.UpdateSpeciesDto(Mapper.Map(Of SpeciesDto)(species))
        End Function

#End Region

#Region "削除"

        Public Function DeleteSpecies(speciesId As Integer) As ProcessResult Implements ISpeciesServiceAgent.DeleteSpecies
            Return _service.DeleteSpeciesDto(speciesId)
        End Function

#End Region

#Region "存在チェック"

        Public Function SpeciesExists(speciesId As Integer) As Boolean Implements ISpeciesServiceAgent.SpeciesExists
            Return _service.SpeciesDtoExists(speciesId)
        End Function

#End Region

    End Class
End Namespace