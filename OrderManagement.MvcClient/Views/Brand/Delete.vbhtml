﻿@ModelType OrderManagement.Client.Entities.Models.OrderManagement.BrandClient
@Code
    ViewData("Title") = "Delete"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
