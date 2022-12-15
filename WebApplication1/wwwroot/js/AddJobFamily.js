$(function () {

    $.ajax({
        type: "GET",
        url: "/JobFamily/GetAllJobFamily",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });

    $('#addJobFamilyId').click(function () {

        var regx = new RegExp(/^[A-Z][\w_@*]{1,15}$/);
        var inputValue = $('#JobFamilyName').val().trim();
        //Alert If In Job Family Name Is Not Entered Any Thing
        if (inputValue == '') {
            $('#JobFamilyName').notify("Job Family Name is required !!!", "error");
        }
        //Alert If Job Family Name Length Is Less Than 2 Character
        else if (inputValue.length < 2) {
            $('#JobFamilyName').notify("Job Family Name should contain more than 1 characters", "error");
        }
        //Alert If Job Family Name Length Is More Than 15 
        else if (inputValue.length > 15) {
            $('#JobFamilyName').notify("Job Family Name should not contain more than 15 characters", "error");
        }
        //Alert for Job Family Name Usinh Regular Expression
        else if (!regx.test(inputValue)) {
            $('#JobFamilyName').notify("Job Family Name should start with Uppercase alphabatical character and \n it contain only aplhanumeric and _ @ *", "error");
        }

        else if (inputValue != "") {
            //Call JobFamilyAlreadyExist Action 
            $.ajax({
                type: "GET",
                url: "/JobFamily/IsJobFamilyAlreadyExist?jobFamilyName=" + inputValue,
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        //Alert For Job Family Name Which is Already Exist
                        $('#JobFamilyName').notify(" This Job Family Name is already Present!!!", "error");
                        isValid = false;
                    }
                    else if (response == false) {
                        if ($('#JobFamilyId').val() != '') {
                            $('#addJobFamilyForm').submit();
                        }
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
});
//Data Table Get All JobFamilies  Using Input Table Id 
function OnSuccess(response) {
    $("#getAllJobFamilyId").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            order: [],
            bPaginate: true,
            data: response,
            columns: [

                { data: 'jobFamilyName'},
                {
                   
                    data: null,
                    "mRender": function (data, type, full) {

                        var id = "'" + data.jobFamilyId + "'";
                        return '<a class="btn btn-warning btn-sm mx-4" href="/JobFamily/EditJobFamily?id=' + data.jobFamilyId + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button class="btn btn-danger btn-sm" id="delete"  onclick="Delete(' + id + ')"  ><i class="bi bi-trash-fill" title="Delete"></i></a> ';
                        
                    }
                }
            ],
        });
};
//Alert if Job Family Name added successfully
$(function () {
    var status = $('#addjobFamilyResponse').val();
    if (status == "true") {
        $.notify("Job family added successfully", "success");
    }
})





