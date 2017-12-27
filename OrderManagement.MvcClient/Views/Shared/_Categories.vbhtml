@ModelType IList(Of WcfService.CategoryDto)

<div class="well">
    <h3>Categories</h3>
    <div class="list-group">
        @For Each category In Model
            @Html.CategoryLink(category)
        Next
    </div>
</div>