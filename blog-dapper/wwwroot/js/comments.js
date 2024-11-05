var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblComments').DataTable({
        "ajax": {
            "url": "/Admin/Comments/GetComments",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "commentId", "width": "5%"},
            {"data": "title", "width": "20%"},
            {"data": "message", "width": "35%"},
            {"data": "article.title", "width": "20%"},
            {"data": "createdAt", "width": "20%"},
            {
                "data": "commentId",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a onclick=Delete("/admin/categories/DeleteComment/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
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
        text: "Once deleted, you will not be able to recover this comment!",
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