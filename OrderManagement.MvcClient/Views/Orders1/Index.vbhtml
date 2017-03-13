@ModelType IEnumerable(Of OrderManagement.Entities.Order)
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
            @Html.DisplayNameFor(Function(model) model.OrderType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OrderDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ShipDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Customer.Name)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OrderType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OrderDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ShipDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Customer.Name)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.OrderId }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.OrderId }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.OrderId })
        </td>
    </tr>
Next

</table>
