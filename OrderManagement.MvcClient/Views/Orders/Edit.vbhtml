@ModelType OrderManagement.Entities.Order
@Code
    ViewData("Title") = "编辑"
End Code

<h2>编辑</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
        <h4>订单</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        @Html.HiddenFor(Function(model) model.OrderId)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.CustomerId, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("CustomerId", Nothing, htmlAttributes:= New With { .class = "form-control" })
                @Html.ValidationMessageFor(Function(model) model.CustomerId, "", New With { .class = "text-danger" })
            </div>
        </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.OrderType, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EnumDropDownListFor(Function(model) model.OrderType, htmlAttributes:=New With {.class = "form-control"})
                 @Html.ValidationMessageFor(Function(model) model.OrderType, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.InvoiceNo, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.InvoiceNo, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.InvoiceNo, "", New With {.class = "text-danger"})
             </div>
         </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.OrderDate, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.OrderDate, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.OrderDate, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.ShipDate, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.ShipDate, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.ShipDate, "", New With { .class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="保存" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("返回列表", "Index")
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
