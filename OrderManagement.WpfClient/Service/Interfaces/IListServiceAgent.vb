Imports OrderManagement.Common

Namespace Service.Interfaces
    Public Interface IListServiceAgent
        ''' <summary>
        '''     产品列表
        ''' </summary>
        ''' <returns></returns>
        Function GetProductList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     种类列表
        ''' </summary>
        ''' <returns></returns>
        Function GetSpeciesList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     用户列表
        ''' </summary>
        ''' <returns></returns>
        Function GetCustomerList() As IEnumerable(Of ValueNamePair)

        ''' <summary>
        '''     品牌列表
        ''' </summary>
        ''' <returns></returns>
        Function GetBrandList() As IEnumerable(Of ValueNamePair)
    End Interface
End Namespace

