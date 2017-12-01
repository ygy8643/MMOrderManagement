@ModelType OrderManagement.Client.Entities.Models.BrandClient
@Code
    ViewData("Title") = "Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Details</h2>

<div>
    <h4>BrandClient</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.BrandId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BrandId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BrandName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BrandName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.BrandNameJp)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.BrandNameJp)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With {.id = Model.BrandId}) |
    @Html.ActionLink("Back to List", "Index")
</p>
