var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblArticlesTags').DataTable({
        "ajax": {
            "url": "/Admin/Tags/GetArticlesWithTags",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "articleId", "width": "10%"},
            {"data": "title", "width": "30%"},
            {"data": "tags[0].name", "width": "30%"},
        ],
    })
}
