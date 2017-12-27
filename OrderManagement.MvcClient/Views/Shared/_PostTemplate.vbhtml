@ModelType OrderManagement.Client.Entities.Models.OrderBlog.PostClient

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
        <div class="post-date textright">
            @Model.PostedOn.ToString
        </div>
        @Html.Raw(Model.ShortDescription)
    </div>
    <div class="panel-footer textright">
        @Html.ActionLink("continue...", "Details", "Blog", routeValues:=New With {.id = Model.Id}, htmlAttributes:=Nothing)
    </div>
</div>