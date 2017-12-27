@Imports PagedList.Mvc
@ModelType PagedList.IPagedList(Of OrderManagement.Client.Entities.Models.OrderBlog.PostClient)
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_BlogLayout.vbhtml"
End Code

@*Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount*@

<div class="row">
    <div class="col-md-10">
        @If Model.PageCount > 1 Then
            @Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {page}))
        End If
    </div>

    <div class="col-md-2 pagination">
        @Html.ActionLink("Create Post", "Create", Nothing, New With {.class = "btn btn-info"})
    </div>
</div>

@For Each item In Model
    @Html.Partial("_PostTemplate", item)
Next
