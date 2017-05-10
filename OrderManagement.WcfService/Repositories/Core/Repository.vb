Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports OrderManagement.WcfService.Interfaces.Core

Namespace Repositories.Core
    Public Class Repository(Of TEntity As Class)
        Implements IRepository(Of TEntity)

#Region "フィールド"

        Protected ReadOnly Context As DbContext

        Protected ReadOnly MyDbSet As DbSet(Of TEntity)

#End Region

#Region "コンストラクター"

        Public Sub New(context As DbContext)
            Me.Context = context
            MyDbSet = context.[Set](Of TEntity)()
        End Sub

#End Region

#Region "メソッド"

#Region "取得"

        Public Function [Get](ParamArray keys As Object()) As TEntity Implements IRepository(Of TEntity).[Get]
            Return MyDbSet.Find(keys)
        End Function

        Public Function GetAll() As IEnumerable(Of TEntity) Implements IRepository(Of TEntity).GetAll
            Return MyDbSet.AsNoTracking.ToList()
        End Function

        Public Function Find(predicate As Expression(Of Func(Of TEntity, Boolean))) As IEnumerable(Of TEntity) _
            Implements IRepository(Of TEntity).Find
            Return MyDbSet.AsNoTracking.Where(predicate).ToList
        End Function

        Public Function SingleOrDefault(predicate As Expression(Of Func(Of TEntity, Boolean))) As TEntity _
            Implements IRepository(Of TEntity).SingleOrDefault
            Return MyDbSet.AsNoTracking.SingleOrDefault(predicate)
        End Function

#End Region

#Region "追加"

        Public Sub Add(entity As TEntity) Implements IRepository(Of TEntity).Add
            MyDbSet.Add(entity)
        End Sub

        Public Sub AddRange(entities As IEnumerable(Of TEntity)) Implements IRepository(Of TEntity).AddRange
            MyDbSet.AddRange(entities)
        End Sub

#End Region

#Region "更新"

        Public Sub Update(entity As TEntity) Implements IRepository(Of TEntity).Update
            MyDbSet.Attach(entity)
            Context.Entry(entity).State = EntityState.Modified
        End Sub

#End Region

#Region "削除"

        Public Sub Remove(ParamArray keys() As Object) Implements IRepository(Of TEntity).Remove
            Dim entityToDelete As TEntity = Context.[Set](Of TEntity).Find(keys)
            Context.[Set](Of TEntity).Remove(entityToDelete)
        End Sub

        Public Sub Remove(entity As TEntity) Implements IRepository(Of TEntity).Remove
            MyDbSet.Remove(entity)
        End Sub

        Public Sub RemoveRange(entities As IEnumerable(Of TEntity)) Implements IRepository(Of TEntity).RemoveRange
            MyDbSet.RemoveRange(entities)
        End Sub

#End Region

#Region "コミット"

        Public Sub Save() Implements IRepository(Of TEntity).Save
            Context.SaveChanges()
        End Sub

#End Region

#End Region

#Region "Asyncメソッド"

        Public Async Function GetAsync(id As String) As Task(Of TEntity) Implements IRepository(Of TEntity).GetAsync
            Return Await MyDbSet.FindAsync(id)
        End Function

        Public Async Function FindAsync(predicate As Expression(Of Func(Of TEntity, Boolean))) _
            As Task(Of IEnumerable(Of TEntity)) Implements IRepository(Of TEntity).FindAsync
            Return Await MyDbSet.Where(predicate).ToListAsync()
        End Function

        Public Async Function GetAllAsync() As Task(Of IEnumerable(Of TEntity)) _
            Implements IRepository(Of TEntity).GetAllAsync
            Return Await MyDbSet.ToListAsync()
        End Function

        Public Async Function SingleOrDefaultAsync(predicate As Expression(Of Func(Of TEntity, Boolean))) _
            As Task(Of TEntity) Implements IRepository(Of TEntity).SingleOrDefaultAsync
            Return Await MyDbSet.SingleOrDefaultAsync(predicate)
        End Function

#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace