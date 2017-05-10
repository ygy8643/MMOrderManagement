@ModelType OrderManagement.Entities.Order
@Code
    ViewData("Title") = "删除"
End Code

<h2>删除</h2>

<h3>确定删除此订单吗?</h3>
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
            @Html.DisplayNameFor(Function(model) model.OrderType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrderType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ShipDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ShipDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.InvoiceNo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.InvoiceNo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Customer.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Customer.Name)
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
