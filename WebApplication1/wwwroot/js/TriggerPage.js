var table;

$(function () {

    LoadDataTable();

    LoadScaleField();

    $("#SaveTriggerBtn").click(function () {
        var isValid = true;
        if ($("#ScaleId1").val() == '') {
            isValid = false;
            $('#ScaleId1').notify("Please Select the scale", "error");
        }
        if ($("#createScaleYears").val() == '') {
            isValid = false;
            $('#createScaleYears').notify("Please enter the value");
        }
        else if ($("#createScaleYears").val() < 0) {
            isValid = false;
            $('#createScaleYears').notify("Please enter the value greater equals to or greater than 0");
        }
        if ($('#createScaleMonth').val() == '') {
            isValid = false;
            $('#createScaleMonth').notify("Please enter the value");
        }
        else if ($("#createScaleMonth").val() < 0) {
            isValid = false;
            $('#createScaleMonth').notify("Please enter the value greater equals to or greater than 0");
        }
        if (isValid == true) {
            var tenure = parseInt($('#createScaleMonth').val()) + parseInt($("#createScaleYears").val()) * 12;
            var mandatoryStatus = $('input:radio[name=createMandatoryOption]:checked').val();
            var bodyData = {
                ScaleId: $('#ScaleId1').val(),
                Tenure: tenure,
                Mandatory: mandatoryStatus
            }; 
            $.ajax({
                type: "POST",
                url: "/TriggerView/AddNewTrigger",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(bodyData),
                success: function (response) {
                    if (response.data == true) {
                        $('#createModal').modal('hide');
                        $('#createTriggerForm')[0].reset();
                        $.notify("trigger added successfully ", "success");
                        table.destroy();
                        LoadDataTable();
                    }
                },
                failure: function () {
                    $.notify("Addition Failed Try again later", "error");
                },
                error: function () {
                    $.notify("Somethiung is wrong contect to Dev Team", "error");
                }

            });
        }
    });


    $("#updateTriggerBtn").click(function () {
        var isValid = true;
        if ($("#ScaleId2").val() == '') {
            isValid = false;
            $('#ScaleId2').notify("Please Select the scale", "error");
        }

        if ($("#updateScaleYears").val() == '') {
            isValid = false;
            $('#updateScaleYears').notify("Please enter the value");
        }
        else if ($("#updateScaleYears").val() < 0) {
            isValid = false;
            $('#updateScaleYears').notify("Please enter the value greater equals to or greater than 0");
        }
        if ($('#upadateScaleMonth').val() == '') {
            isValid = false;
            $('#upadateScaleMonth').notify("Please enter the value");
        }
        else if ($("#upadateScaleMonth").val() < 0) {
            isValid = false;
            $('#upadateScaleMonth').notify("Please enter the value greater equals to or greater than 0");
        }
        if (isValid == true) {
            var tenure = parseInt($('#upadateScaleMonth').val()) + parseInt($("#updateScaleYears").val()) * 12;
            var mandatoryStatus = $('input:radio[name=updateMandatoryOption]:checked').val();
            var bodyData = {
                TriggerId: $('#TriggerId').val(),
                ScaleId: $('#ScaleId2').val(),
                Tenure: tenure,
                Mandatory: mandatoryStatus
            };
            $.ajax({
                type: "POST",
                url: "/TriggerView/UpdateTrigger",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(bodyData),
                success: function (response) {
                    if (response.data == true) {
                        $('#updateModal').modal('hide');
                        $('#updateTriggerForm')[0].reset();
                        $.notify("trigger updated successfully ", "success");
                        table.destroy();
                        LoadDataTable();
                    }
                },
                failure: function () {
                    $.notify("Updation Failed Try again later", "error");
                },
                error: function () {
                    $.notify("Something is wrong contect to Dev Team", "error");
                }

            });
        }
    });


    $(document).on('click', 'button#EditTriggerBtn', function () {
        $('#updateModal').modal('show');
        var tr = $(this).closest('tr');
        var row = table.row(tr).data();
        $.ajax({
            type: "GET",
            url: "/TriggerView/GetTriggerById?id=" + $(this).val(),
            contentType: "application/json , charset = utf-8",
            dataType: "json",
            success: function (response) {
                $("#TriggerId").val(response.triggerId);
                $("#ScaleId2 option").each(function () {
                    console.log($(this).html().trim() + "___" + row.scaleName.trim());
                    if ($(this).html().trim() == row.scaleName.trim()) {
                        $(this).prop("selected", true);
                        console.log("success");
                    }
                });
                $("#updateScaleYears").val(parseInt(response.tenure/12));
                $("#upadateScaleMonth").val(response.tenure%12);
                $("input:radio[name=updateMandatoryOption][value=" + response.mandatory +"]").prop("checked",true);
            },
            failure: function () {
                $.notify("Fail to Load the trigger's Data", "error");
            },
            error: function () {
                $.notify("Somethiung is wrong contect to Dev Team", "error");
            }
        });
    });

    $(document).on('click', 'button.DatatableDeleteBtn', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        Swal.fire({
            title: 'Are you sure you want to Delete this Trigger?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    url: '/TriggerView/DeleteTrigger?id=' + $(this).val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.data = true) {
                            $.notify("trigger Deleted successfully", "success");
                            row.remove().draw();
                        }
                        else {
                            $.notify(" Deletion has failed", "success");

                        }
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });
            }
        });
        return false;
    });


});
function LoadDataTable() {
    //geting all values of trigger
    $.ajax({
        type: "GET",
        url: "/TriggerView/GetAllTrigger",
        contentType: "application/json , charset = utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function () {
            $.notify("Fail to Load the trigger's Data", "error");
        },
        error: function () {
            $.notify("Somethiung is wrong contect to Dev Team", "error");
        }
    });
}

function OnSuccess(response) {
    table = $("#triggerDetailsTable").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            order: [],
            bPaginate: true,
            data: response,
            columns: [

                { data: 'scaleName' },
                {
                    data: 'tenure',
                    "mRender": function (data, type, full) {
                        return parseInt(data / 12) + ' Years ' + data % 12 + ' Months';
                    }
                },
                {
                    data: 'mandatory',
                    "mRender": function (data, type, full) {
                        if (data == "Yes") {
                            return '<i class="px-2 py-1 fw-semibold text-success bg-success bg-opacity-10 border border-success border-opacity-10 rounded-2">Yes</i>';
                        }
                        else if (data == "No") {
                            return '<i class="px-2 py-1 fw-semibold text-danger bg-danger bg-opacity-10 border border-danger border-opacity-10 rounded-2">No</i>';
                        }
                    }
                },
                {
                    data: null,
                    orderable: false,
                    "mRender": function (data, type, full) {
                        return '<button id="EditTriggerBtn" class="btn btn-warning btn-sm mx-4" value="' + data.triggerId + '" ><i class="bi bi-pencil-fill" title="Edit"></i></button><button id="DeleteTrigger" class="btn btn-danger btn-sm DatatableDeleteBtn "  value="' + data.triggerId + '" ><i class="bi bi-trash-fill" title="Delete"></i></button> ';

                    }
                }
            ],
            dom: 'Bfrtip',
            buttons: [
                {
                    className: "text-success border-red newTriggerBtn",
                    text: 'New Trigger',
                    action: function (e, dt, node, config) {
                        $('#createModal').modal('show');
                        LoadScaleField();
                    }
                }
            ]
        });
};

//loading the form for Creating the new trigger 
function LoadScaleField() {

    var scaleId = $(".ScaleId");
    scaleId.empty();
    scaleId.append($("<option></option>").val('').html('Select Scale'));

    $.ajax({
        type: "GET",
        url: "/TriggerView/GetAllScale",
        contentType: "application/json , charset = utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, scale) {
                scaleId.append($("<option></option>").val(scale.scaleId).html(scale.scaleName));
            });
        },
        failure: function () {
            $.notify("Fail to Load the trigger's Data", "error");
        },
        error: function () {
            $.notify("Somethiung is wrong contect to Dev Team !!!", "error");
        }
    });

}
