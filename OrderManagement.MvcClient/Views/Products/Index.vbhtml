@ModelType IEnumerable(Of OrderManagement.Entities.Product)
@Code
    ViewData("Title") = "产品列表"
End Code

<h2>产品</h2>

<p>
    @Html.ActionLink("新建产品", "Create")
</p>
<table class="table" id="orderDetails">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.ProductName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.ProductNameJp)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.ProductName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.ProductNameJp)
                </td>
                <td>
                    @Html.ActionLink("编辑", "Edit", New With {.id = item.ProductId}) |
                    @Html.ActionLink("明细", "Details", New With {.id = item.ProductId}) |
                    @Html.ActionLink("删除", "Delete", New With {.id = item.ProductId})
                </td>
            </tr>
        Next
    </tbody>

</table>

@section scripts
    <script>
        $(document).ready(function () {
            $("#orderDetails").DataTable();
        });
    </script>
End Section
