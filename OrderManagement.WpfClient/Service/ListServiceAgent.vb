Imports OrderManagement.Common
Imports OrderManagement.WpfClient.Service.Interfaces
Imports OrderManagement.WpfClient.WcfService

Namespace Service
    Public Class ListServiceAgent
        Implements IListServiceAgent

        Private ReadOnly _orderManagementService As New OrderManagementServiceClient

        ''' <summary>
        '''     品牌列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetBrandList() As IEnumerable(Of ValueNamePair) Implements IListServiceAgent.GetBrandList
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        '''     用户列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCustomerList() As IEnumerable(Of ValueNamePair) Implements IListServiceAgent.GetCustomerList

            Dim lstCustomer = _orderManagementService.GetCustomerDtoes()

            Dim query = From ctx In lstCustomer
                        Select New ValueNamePair With {.Value = ctx.CustomerId, .DisplayName = ctx.Name}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     产品列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetProductList() As IEnumerable(Of ValueNamePair) Implements IListServiceAgent.GetProductList
            Dim productDtos = _orderManagementService.GetProductDtoes()

            Dim query = From ctx In productDtos
                        Select New ValueNamePair With {.Value = ctx.ProductId, .DisplayName = ctx.ProductName}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function

        ''' <summary>
        '''     种类列表
        ''' </summary>
        ''' <returns></returns>
        Public Function GetSpeciesList() As IEnumerable(Of ValueNamePair) Implements IListServiceAgent.GetSpeciesList
            Dim speciesDtos = _orderManagementService.GetSpeciesDtoes()

            Dim query = From ctx In speciesDtos
                        Select New ValueNamePair With {.Value = ctx.SpeciesId, .DisplayName = ctx.SpeciesName}

            Dim result = query.ToList()

            result.Insert(0, New ValueNamePair With {.Value = 0, .DisplayName = String.Empty})

            Return result
        End Function
    End Class
End Namespace

