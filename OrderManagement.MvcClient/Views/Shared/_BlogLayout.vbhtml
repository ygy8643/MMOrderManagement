<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Order Management Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div class="navbar navbar-inverse">
        <div class="logo">
            <div class="logotext">
                <h1>Order Management Application</h1>
                <h2>Order, Management and More...</h2>
            </div>
        </div>
        <div class="container">
            <div class="navbar-collapse collapse" style="float: right;">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Order", "Index", "Order", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Customer", "Index", "Customer", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Product", "Index", "Product", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Brand", "Index", "Brand", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Species", "Index", "Species", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Inventory", "Index", "Inventory", Nothing, New With {.class = "navbar-brand"})</li>
                    <li>@Html.ActionLink("Blog", "Index", "Blog", Nothing, New With {.class = "navbar-brand"})</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container body-content">
        <div class="row">
            <div class="col-md-3">
                @Html.Action("GetSidebar", "Blog")
            </div>
            <div class="col-md-9">
                @RenderBody()
            </div>

        </div>
        <footer>
            <p>&copy; @DateTime.Now.Year - My Blog Application</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
</body>
</html>
