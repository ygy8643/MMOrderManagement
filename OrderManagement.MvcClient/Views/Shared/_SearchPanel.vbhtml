<div class="well">
    @Using Html.BeginForm("Search", "Blog", FormMethod.Post, New With {.id = "searchForm", .class = "form-inline"})
        @<div class="form-group">
            <label class="sr-only" for="exampleInputAmount">Amount (in dollars)</label>
            <div class="input-group">
                <div class="input-group-addon">
                    <span class="glyphicon glyphicon-question-sign"></span>
                </div>
                <input type="text" class="form-control" id="txtSearchString" name="searchString" placeholder="タイトルで検索">
                <span class="input-group-btn">
                    <button class="btn btn-info" type="submit">Go!</button>
                </span>
            </div>
        </div>
    End Using
</div>
