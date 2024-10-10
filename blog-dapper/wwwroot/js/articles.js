var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblArticles').DataTable({
        "ajax": {
            "url": "/Admin/Articles/GetArticles",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "articleId", "width": "5%"},
            {"data": "title", "width": "40%"},
            {
                "data": "image",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <img src="../${data}" alt="Articulo" style="width: 100px; height: 100px;" />
                        </div>
                    `;
                }, "width": "10%"
            },
            {"data": "state", "width": "5%"},
            {"data": "category.name", "width": "10%"},
            {"data": "createdAt", "width": "20%"},
            {
                "data": "articleId",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/articles/edit/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/admin/articles/deletearticle/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="bi bi-x-square"></i>
                            </a>
                        </div>
                    `;
                }, "width": "35%"
            }
        ],
    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this article!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            url,
            type: "DELETE",
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            }
        });
    })
}