@ModelType IEnumerable(Of OrderManagement.Entities.Brand)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.BrandName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.BrandNameJp)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BrandName)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.BrandNameJp)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.BrandId }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.BrandId }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.BrandId })
        </td>
    </tr>
Next

</table>
