Imports OrderManagement.Client.Entities.Models.OrderManagement
Imports OrderManagement.Common

Namespace Service
    Public Interface ISpeciesServiceAgent

#Region "取得"

        ''' <summary>
        '''     取得顾客信息
        ''' </summary>
        ''' <returns></returns>
        Function GetSpeciesComboBoxList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     取得顾客名称
        ''' </summary>
        ''' <param name="speciesId"></param>
        ''' <returns></returns>
        Function GetSpeciesName(speciesId As Integer) As String

        ''' <summary>
        '''     取得客户信息
        ''' </summary>
        ''' <param name="speciesId"></param>
        ''' <returns></returns>
        Function GetSpecies(speciesId As Integer) As SpeciesClient

        ''' <summary>
        ''' 根据条件取得顾客信息
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <returns></returns>
        Function GetSpeciesByCondition(condition As SpeciesClient) As IEnumerable(Of SpeciesClient)

#End Region

#Region "追加"

        Function CreateSpecies(species As SpeciesClient) As ProcessResult

#End Region

#Region "更新"

        Function UpdateSpecies(species As SpeciesClient) As ProcessResult

#End Region

#Region "削除"

        Function DeleteSpecies(speciesId As Integer) As ProcessResult

#End Region

#Region "存在チェック"

        Function SpeciesExists(speciesId As Integer) As Boolean

#End Region

    End Interface
End Namespace