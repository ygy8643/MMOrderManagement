@ModelType OrderManagement.Entities.Customer
@Code
    ViewData("Title") = "删除"
End Code

<h2>删除</h2>

<h3>确定删除此客户吗?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="删除" class="btn btn-default" /> |
            @Html.ActionLink("返回列表", "Index")
        </div>
    End Using
</div>
