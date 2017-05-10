Imports System.Linq.Expressions
Imports System.Threading.Tasks

Namespace Interfaces.Core
    Public Interface IRepository(Of TEntity As Class)
        Inherits IDisposable

#Region "メソッド"

#Region "取得"

        Function [Get](ParamArray keys As Object()) As TEntity

        Function GetAll() As IEnumerable(Of TEntity)

        Function Find(predicate As Expression(Of Func(Of TEntity, Boolean))) As IEnumerable(Of TEntity)

        Function SingleOrDefault(predicate As Expression(Of Func(Of TEntity, Boolean))) As TEntity

#End Region

#Region "追加"

        Sub Add(entity As TEntity)

        Sub AddRange(entities As IEnumerable(Of TEntity))

#End Region

#Region "更新"

        Sub Update(entity As TEntity)

#End Region

#Region "削除"

        Sub Remove(ParamArray keys As Object())

        Sub Remove(entity As TEntity)

        Sub RemoveRange(entities As IEnumerable(Of TEntity))

#End Region

#Region "コミット"

        Sub Save()

#End Region

#End Region

#Region "Asyncメソッド"

        Function GetAsync(id As String) As Task(Of TEntity)

        Function GetAllAsync() As Task(Of IEnumerable(Of TEntity))

        Function FindAsync(predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of IEnumerable(Of TEntity))

        Function SingleOrDefaultAsync(predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity)

#End Region
    End Interface
End Namespace