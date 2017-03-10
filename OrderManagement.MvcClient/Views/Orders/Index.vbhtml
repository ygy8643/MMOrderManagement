@ModelType IEnumerable(Of OrderManagement.Entities.Order)
@Code
    ViewData("Title") = "订单"
End Code

<h2>订单</h2>

<p>
    @Html.ActionLink("新建订单", "Create")
</p>
<table class="table" id="orders">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDate)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Customer.Name)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.ShipDate)
            </th>

            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrderDate)
                </td>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.Customer.Name)
                </td>

                <td>
                    @Html.DisplayFor(Function(modelItem) item.ShipDate)
                </td>

                <td>
                    @Html.ActionLink("编辑", "Edit", New With {.id = item.OrderId}) |
                    @Html.ActionLink("详细", "Details", New With {.id = item.OrderId}) |
                    @Html.ActionLink("删除", "Delete", New With {.id = item.OrderId})
                </td>
            </tr>
        Next
    </tbody>

</table>

@section scripts
    <script>
        $(document).ready(function () {
            $("#orders").DataTable();
        });
    </script>
End Section