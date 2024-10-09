var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblCategorias').DataTable({
        "ajax": {
            "url": "/Admin/Categories/GetCategories",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "categoryId", "width": "5%"},
            {"data": "name", "width": "40%"},
            {"data": "createdAt", "width": "20%"},
            {
                "data": "categoryId",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/admin/categories/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                <i class="bi bi-pencil-square"></i>
                            </a>
                            <a onclick=Delete("/admin/categories/DeleteCategory/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
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