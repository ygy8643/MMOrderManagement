Imports System.Globalization

Namespace Converters.ToVisibility
    <ValueConversion(GetType(Object), GetType(Boolean))>
    Public Class TrueToHiddenConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) _
            As Object Implements IValueConverter.Convert

            If CBool(value) Then
                Return Visibility.Hidden
            Else
                Return Visibility.Visible
            End If
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) _
            As Object Implements IValueConverter.ConvertBack

            Return New NotImplementedException
        End Function
    End Class
End Namespace