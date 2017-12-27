@ModelType OrderManagement.Client.Entities.Models.OrderBlog.PostClient

@Code
    ViewData("Title") = "Edit"
    Layout = "~/Views/Shared/_BlogLayout.vbhtml"
End Code

<script src="~/Scripts/tinymce/tinymce.min.js"></script>

<h2>Edit</h2>
@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Post</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        @Html.HiddenFor(Function(model) model.Id)

        <!--分類ID-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.CategoryId, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-5">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-repeat" aria-hidden="true"></span>
                    </div>
                    @Html.DropDownList("CategoryId", Nothing, htmlAttributes:=New With {.class = "form-control"})
                </div>
                @Html.ValidationMessageFor(Function(model) model.CategoryId, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--タイトル-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-tag" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--説明-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.ShortDescription, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextAreaFor(Function(model) model.ShortDescription, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.ShortDescription, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--内容-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextAreaFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!---->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Meta, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.Meta, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Meta, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!---->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.UrlSlug, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-flash" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.UrlSlug, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.UrlSlug, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--作者-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Author, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.Author, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Author, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--公開-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Published, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="checkbox">
                    @Html.EditorFor(Function(model) model.Published)
                    @Html.ValidationMessageFor(Function(model) model.Published, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>

        <!--発行日-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.PostedOn, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-inbox" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.PostedOn, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.PostedOn, "", New With {.class = "text-danger"})
            </div>
        </div>

        <!--変更日-->
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Modified, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                <div class="input-group">
                    <div class="input-group-addon">
                        <span class="glyphicon glyphicon-map-marker" aria-hidden="true"></span>
                    </div>
                    @Html.EditorFor(Function(model) model.Modified, New With {.htmlAttributes = New With {.class = "form-control"}})
                </div>
                @Html.ValidationMessageFor(Function(model) model.Modified, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-info" />
                @Html.ActionLink("Back to List", "Index", Nothing, New With {.class = "btn btn-info"})
            </div>
        </div>
    </div>
End Using

<!--High Light The Code-->
<script type="text/javascript">
    //Description
    tinymce.init({
        selector: "textarea#Description",
        theme: "modern",
        height: 500,
        plugins: [
             "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
             "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
             "save table contextmenu directionality emoticons template paste textcolor codesample"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic codesample | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons",
        fontsize_formats: "12pt 12pt 12pt 14pt 18pt 24pt 36pt",
        automatic_uploads: true,
        images_upload_base_path: '/Content/images'
    });

    //ShortDescription
    tinymce.init({
        selector: "textarea#ShortDescription",
        theme: "modern",
        height: 200,
        plugins: [
             "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
             "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
             "save table contextmenu directionality emoticons template paste textcolor codesample"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic codesample",
        fontsize_formats: "12pt 12pt 12pt 14pt 18pt 24pt 36pt",
        automatic_uploads: true,
        images_upload_base_path: '/Content/images'
    });
</script>

