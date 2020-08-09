jQuery.noConflict()(function ($) {
    $(document).ready(function () {
        loadDataTable();
    })
})

var dataTable;

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Singles/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "title", "width": "22%" },
            { "data": "artist", "width": "20%" },
            { "data": "genres.genre", "width": "15%" },
            { "data": "releasedDate", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="Singles/AddSingle/${data}" class="btn btn-success text-white" style="cursor:pointer; width:40px">
                            <i class="fas fa-edit"></i>
                        </a>
                        &nbsp;
                        <a onclick=Delete("Singles/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:40px">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>`;
                },
                "width": "18%"
            }
        ],
        "language": {
            "emptyTable": "No Record Found!"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "ARE YOU SURE YOU WANT TO DELETE IT?",
        text: "YOU WILL NOT BE ABLE TO RESTORE IT!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: "#BD6B55",
        confirmButtonText: "YES, DELETE IT!",
        closeOnConfirm: true
    },
        function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    )
}