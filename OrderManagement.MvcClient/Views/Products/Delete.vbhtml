@ModelType OrderManagement.Entities.Product
@Code
    ViewData("Title") = "删除"
End Code

<h2>删除</h2>

<h3>确定删除此产品吗?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="删除" class="btn btn-default" /> |
            @Html.ActionLink("返回列表", "Index")
        </div>
    End Using
</div>
