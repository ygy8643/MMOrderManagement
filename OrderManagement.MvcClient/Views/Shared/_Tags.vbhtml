@ModelType IList(Of WcfService.TagDto)

<div class="well">
    <h3>Tags</h3>
    @For Each tag In Model
        @<div class="tag">@Html.TagLink(tag)</div>
    Next
</div>
