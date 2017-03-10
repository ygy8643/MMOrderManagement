@ModelType OrderManagement.Entities.OrderDetail
@Code
    ViewData("Title") = "明细"
End Code

<h2>明细</h2>

<div>
    <h4>订单明细</h4>
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.Order.OrderId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Order.OrderId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Product.ProductName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Product.ProductName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Quantity)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Quantity)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PurchasePrice)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PurchasePrice)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.SoldPrice)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.SoldPrice)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Link)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Link)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("编辑", "Edit", New With {.id = Model.OrderDetailId}) |
    @Html.ActionLink("返回列表", "Details", "Orders", New With {.id = Model.OrderId}, Nothing)
</p>
