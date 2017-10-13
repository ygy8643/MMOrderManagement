Imports System.Globalization
Imports OrderManagement.WpfClient.Service

Namespace Converters.ToName

    <ValueConversion(GetType(Integer), GetType(String))>
    Public Class CustomerCodeToNameConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert

            If value IsNot Nothing Then

                Dim customerCode = CType(value, Integer)
                Dim customerServiceAgent As New CustomerServiceAgent

                Return customerServiceAgent.GetCustomerName(customerCode)
            End If

            Return String.Empty
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace