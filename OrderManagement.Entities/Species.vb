'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Species
    Public Property SpeciesId As Integer
    Public Property SpeciesName As String

    Public Overridable Property Products As ICollection(Of Product) = New HashSet(Of Product)

End Class
