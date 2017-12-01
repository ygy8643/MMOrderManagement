@ModelType IEnumerable(Of OrderManagement.Client.Entities.Models.OrderClient)
@Code
ViewData("Title") = "Index"
Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.OrderId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.CustomerId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OrderDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ShipDate)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.OrderType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.InvoiceNo)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Freight)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OrderId)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.CustomerId)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OrderDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ShipDate)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.OrderType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.InvoiceNo)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Freight)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.OrderId}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.OrderId}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.OrderId})
        </td>
    </tr>
Next

</table>
