@ModelType OrderManagement.Client.Entities.Models.OrderManagement.OrderClient
@Code
    ViewData("Title") = "Delete"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>OrderClient</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.OrderId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrderId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.CustomerId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.CustomerId)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrderDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrderDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ShipDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ShipDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.OrderType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.OrderType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.InvoiceNo)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.InvoiceNo)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Freight)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Freight)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
