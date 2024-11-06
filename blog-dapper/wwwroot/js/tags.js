var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTags').DataTable({
        "ajax": {
            "url": "/Admin/Tags/GetTags",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "tagId", "width": "10%"},
            {"data": "name", "width": "30%"},
            {"data": "createdAt", "width": "30%"},
            {
                "data": "tagId",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/tags/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/admin/tags/DeleteTag/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="bi bi-x-square"></i>
                            </a>
                        </div>
                    `;
                }, "width": "30%"
            }
        ],
    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this category!",
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