﻿@ModelType Models.WidgetViewModel

<div id="sidebars">
    @Html.Partial("_SearchPanel")
    @Html.Partial("_Categories", Model.Categories)
    @Html.Partial("_Tags", Model.Tags)
    @Html.Partial("_LatestPosts", Model.LatestPosts)
</div>