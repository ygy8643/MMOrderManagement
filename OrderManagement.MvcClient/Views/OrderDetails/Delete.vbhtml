@ModelType OrderManagement.Entities.OrderDetail
@Code
    ViewData("Title") = "删除"
End Code

<h2>删除</h2>

<h3>确定删除此条明细吗?</h3>
<div>
    <h4>订单明细</h4>
    <hr />
    <dl class="dl-horizontal">

        <dt>
            @Html.DisplayNameFor(Function(model) model.Status)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Status)
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

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="删除" class="btn btn-default" /> |
            @Html.ActionLink("返回列表", "Index")
        </div>
    End Using
</div>
