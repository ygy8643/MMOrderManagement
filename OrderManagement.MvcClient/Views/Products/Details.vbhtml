@ModelType OrderManagement.Entities.Product
@Code
    ViewData("Title") = "明细"
End Code

<h2>明细</h2>

<div>
    <h4>产品</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.ProductName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ProductName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ProductNameJp)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ProductNameJp)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("编辑", "Edit", New With {.id = Model.ProductId}) |
    @Html.ActionLink("返回列表", "Index")
</p>
