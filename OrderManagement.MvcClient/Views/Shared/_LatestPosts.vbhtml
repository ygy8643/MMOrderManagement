@ModelType IList(Of WcfService.PostDto)

<div class="well">
    <h3>Latest Posts</h3>
    @If Model.Count > 0 Then
        @<ul>
            @For Each post In Model
                @<li>@Html.PostLink(post)</li>
            Next
        </ul>
    End If
</div>
