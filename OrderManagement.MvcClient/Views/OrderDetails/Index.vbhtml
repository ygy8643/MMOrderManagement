@ModelType IEnumerable(Of OrderManagement.Entities.OrderDetail)
@Code
    ViewData("Title") = "列表"
End Code

<h2>列表</h2>

<p>
    @Html.ActionLink("新建", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Order.OrderId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Product.ProductName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Quantity)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.PurchasePrice)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.SoldPrice)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Status)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Link)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Order.OrderId)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Product.ProductName)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Quantity)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.PurchasePrice)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.SoldPrice)
            </td>
             <td>
                 @Html.DisplayFor(Function(modelItem) item.Status)
             </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Link)
            </td>
            <td>
                @Html.ActionLink("编辑", "Edit", New With {.id = item.OrderDetailId}) |
                @Html.ActionLink("详细", "Details", New With {.id = item.OrderDetailId}) |
                @Html.ActionLink("删除", "Delete", New With {.id = item.OrderDetailId})
            </td>
        </tr>
    Next

</table>
