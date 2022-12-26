var table;
var responseArray = new Array();
$(document).ready(function () {
    var employeeIdList = JSON.parse(sessionStorage.getItem("EmployeeIdList"))
    $.ajax({
        type: "POST",
        traditional: true,
        url: "/Transfer/MatchingRequestTransferVacancyData/",
        data: JSON.stringify(employeeIdList),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: MatchEmployeeVacancyList,
        failure: "",
        error: function (response) {
            window.location.replace("/Transfer/EmployeeTransferView/");
        },
    });
}); 

function MatchEmployeeVacancyList(response) {
    responseArray = response
   table = $("#matchingVacancy").DataTable({
            searching: false,
            bLengthChange: false,
            lengthMenu: [[10, 20, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            processing: true,
            data: response,
            columns: [
                { data: 'zoneName' },
                { data: 'regionName' },
                { data: 'vacancy' },
                { data: 'selectedEmployeesCount' },
                {
                    className:'',
                    data: null,
                    mRender: function (data, type, full) {
                        if (data.matchedUnMatchedVacancy == true) {
                            return '<span class="badge bg-success">Matched</span>';
                        } else {
                            return '<span class="badge bg-danger">UnMatched</span>'
                        }
                    }
                },
            ],
            columnDefs:[
                {
                    targets: [0,1,2,3,4],
                    className:"text-center"
                }
                ],
            order: [[0, 'asc']]
    });
}

function GenerateList() {
    var flag = 0;
    for (let i = 0; i < responseArray.length; i++) {
        if (responseArray[i].matchedUnMatchedVacancy == false) {
            flag = 1;
            break;
        }
    }

    if (flag == 1) {
        $.notify("Vacancy Count does not match \n Please Match the vacancies to proceed further","error")
    }
    else {
        $.notify("Success! Vacancy Count matches \n with the selected employees ", "success");
        window.setTimeout(function () {
            window.location.href = "/Transfer/FinalizeEmployeeTransferViewCo"
        }, 3000);
    }
    
    

   
}