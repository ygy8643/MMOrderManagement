Imports System.Globalization
Imports OrderManagement.Client.Entities.Models.OrderManagement

Namespace Converters.ToName
    <ValueConversion(GetType(ICollection(Of OrderDetailClient)), GetType(ICollection(Of OrderDetailClient)))>
    Public Class OrderDetailNothingToEmptyConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) _
            As Object Implements IValueConverter.Convert

            If value IsNot Nothing Then

                Dim orderDetails = CType(value, ICollection(Of OrderDetailClient))
                Return orderDetails

            Else
                Return New List(Of OrderDetailClient)
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) _
            As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace