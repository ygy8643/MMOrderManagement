@ModelType OrderManagement.Entities.Order
@Code
    ViewData("Title") = "订单明细"
End Code

<h2>明细</h2>

<div>
    <h4>订单</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.OrderDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrderDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Customer.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Customer.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ShipDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ShipDate)
        </dd>

    </dl>
</div>

<table class="table" id="orderdetails">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.Order.OrderId)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.Product.ProductName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.Quantity)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.PurchasePrice)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.SoldPrice)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrderDetails.FirstOrDefault.Link)
            </th>

            <th>@Html.ActionLink("新建明细", "Create", "OrderDetails", New With {.orderId = Model.OrderId}, Nothing)</th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model.OrderDetails
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
                    @Html.DisplayFor(Function(modelItem) item.Link)
                </td>
                <td>

                    @Html.ActionLink("编辑", "Edit", "OrderDetails", New With {.id = item.OrderDetailId}, Nothing) |
                    @Html.ActionLink("明细", "Details", "OrderDetails", New With {.id = item.OrderDetailId}, Nothing) |
                    @Html.ActionLink("删除", "Delete", "OrderDetails", New With {.id = item.OrderDetailId}, Nothing)
                </td>
            </tr>
        Next
    </tbody>
</table>

<p>
    @Html.ActionLink("返回列表", "Index", "Orders")
</p>

@section scripts
    <script>
        $(document).ready(function () {
            $("#orderdetails").DataTable();
        });
    </script>
End Section