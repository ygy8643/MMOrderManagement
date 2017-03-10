@ModelType OrderManagement.Entities.Customer
@Code
    ViewData("Title") = "详细"
End Code

<h2>详细</h2>

<div>
    <h4>客户</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Address)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Address)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.PostCode)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.PostCode)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Phone)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Phone)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("编辑", "Edit", New With {.id = Model.CustomerId}) |
    @Html.ActionLink("返回列表", "Index")
</p>
