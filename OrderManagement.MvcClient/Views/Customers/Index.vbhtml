@ModelType IEnumerable(Of OrderManagement.Entities.Customer)
@Code
    ViewData("Title") = "客户列表"
End Code

<h2>客户列表</h2>

<p>
    @Html.ActionLink("新建客户", "Create")
</p>
<table class="table" id="customers">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Address)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.PostCode)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Phone)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Name)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Address)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.PostCode)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Phone)
                </td>
                <td>
                    @Html.ActionLink("编辑", "Edit", New With {.id = item.CustomerId}) |
                    @Html.ActionLink("明细", "Details", New With {.id = item.CustomerId}) |
                    @Html.ActionLink("删除", "Delete", New With {.id = item.CustomerId})
                </td>
            </tr>
        Next
    </tbody>
</table>

@section scripts
    <script>
        $(document).ready(function () {
            $("#customers").DataTable();
        });
    </script>
End Section