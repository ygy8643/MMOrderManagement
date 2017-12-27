@ModelType OrderManagement.Client.Entities.Models.OrderBlog.PostClient

@Code
    ViewData("Title") = "Details"
    Layout = "~/Views/Shared/_BlogLayout.vbhtml"
End Code

<link rel="stylesheet" type="text/css" href="~/Scripts/tinymce/plugins/codesample/css/prism.css" />
<script src="~/Scripts/tinymce/tinymce.min.js"></script>
<script src="~/Scripts/tinymce/plugins/codesample/prism.js"></script>

<div class="panel panel-info">
    <div class="panel-heading">
        <h3>@Model.Title</h3>
    </div>
    <div class="panel-body">
        <div>
            <span>Author:&nbsp;</span>@Html.ActionLink(IIf(IsNothing(Model.Author), "Nobody", Model.Author), "Details")
        </div>
        @*<div class="post-category">
            <span>Category:&nbsp;</span>@Html.ActionLink(Model.Category.Name, "Details")
        </div>
        <div class="post-tags">
            <span>Tags:</span>@Helpers.Tags(Html, Model.Tags)
        </div>*@
        <div class="post-date">
            @Model.PostedOn.ToString
        </div>

        @Html.Raw(Model.Description)

    </div>
    <div class="panel-footer textright">
        <p>
            @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}) |
            @Html.ActionLink("Back to List", "Index")
        </p>
    </div>
</div>


