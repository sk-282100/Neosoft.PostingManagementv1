//$.ajax({
//    type: "GET",
//    url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
//    data: '{}',
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    success: GetRecords,
//    failure: function (response) {
//        window.location.replace("/Error/Error500");
//    },
//    error: function (response) {
//        window.location.replace("/Error/Error500");
//    }
//});

function ViewRecords(BatchId) {
    GetRecords(BatchId);    
}
function GetRecords(BatchId) {
    $('#itemModel').modal('show');
    if ($("#excelUploadType").val() == 3) {
        GetEmployeeMasterRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 2) {
        GetDepartmentMasterRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 1) {
        GetBranchMasterRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 4) {
        GetInterRegionalPromotionRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 5) {
        GetInterRegionalRequestTransferRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 6) {
        GetInterZonalPromotionRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 7) {
        GetInterZonalRequestTransferRecords(BatchId)
    }
    else if ($("#excelUploadType").val() == 8) {
        GetRegionMasterRecords(BatchId);
    }
    else if ($("#excelUploadType").val() == 9) {
        GetZoneMasterRecords(BatchId);
    }
};
function GetDepartmentMasterRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#departmentMasterRecords')) {
        $('#departmentMasterRecords').DataTable().destroy();
    }
    $("#departmentMasterRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'departmentCode' },
                { data: 'departmentName' }
            ],

        });
    $("#departmentMasterRecords").show();
    ShowRecords();
}
function GetEmployeeMasterRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#employeeMasterRecords')) {
        $('#employeeMasterRecords').DataTable().destroy();
    }
    $("#employeeMasterRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'employeeId' },
                { data: 'employeeName' },
                { data: 'location' },
                { data: 'locationDesired' },
                { data: 'departmentRo' },
                { data: 'regionCode' },
                { data: 'regionName' },
                { data: 'zoneCode' },
                { data: 'zoneName' },
                { data: 'jobCode' },
                { data: 'designation' },
                { data: 'roleStartDate' },
                { data: 'scaleCode' },
                { data: 'scale' },
                { data: 'ubijobRole' },
                { data: 'bankName' },
                { data: 'birthDate' },
                { data: 'sex' },
                { data: 'disability' },
                { data: 'locationStartDate' },
                { data: 'domicileZone' },
                { data: 'rostartDate' },
                { data: 'lastPromotionDate' },
                { data: 'zostartDate' }
            ],

        });
    $("#employeeMasterRecords").show();
    ShowRecords();
}
function GetBranchMasterRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#branchMasterRecords')) {
        $('#branchMasterRecords').DataTable().destroy();
    }
    $("#branchMasterRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'oldBranchCode' },
                { data: 'branchCode' },
                { data: 'branchName' },
                { data: 'state' },
                { data: 'stateId' },
                { data: 'district' },
                { data: 'districtId' },
                { data: 'city' },
                { data: 'cityId' },
                { data: 'area' },
                { data: 'dateOfOpening' },
                { data: 'zoneName' },
                { data: 'zoneCode' },
                { data: 'regionCode' },
                { data: 'regionName' },
                { data: 'branchType' },
                { data: 'bankName' },
                { data: 'administrativeFlag' }
                
            ],

        });
    $("#branchMasterRecords").show();
    ShowRecords();
}
function GetInterRegionalPromotionRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#interRegionalPromotionRecords')) {       
        $('#interRegionalPromotionRecords').DataTable().destroy();
    }
    $("#interRegionalPromotionRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },                     
                { data: 'employeeId' },
                { data: 'currentScale' },
                { data: 'promotedScale' },
                { data: 'action' },
                { data: 'effdt' },
                { data: 'promotionType' }
            ],

        });
    $("#interRegionalPromotionRecords").show();
    ShowRecords();
}
function GetInterRegionalRequestTransferRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#interRegionalRequestRecords')) {
        $('#interRegionalRequestRecords').DataTable().destroy();
    }
    $("#interRegionalRequestRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'employeeId'},
                { data: 'tranferSequenceNo' },
                { data: 'gender' }, 
                { data: 'directPromotee' }, 
                { data: 'scale' }, 
                { data: 'transferType' }, 
                { data: 'name' }, 
                { data: 'designation' }, 
                { data: 'locationName' },
                { data: 'currentRo' }, 
                { data: 'fromZone' }, 
                { data: 'requiredState' }, 
                { data: 'appliedZone' }, 
                { data: 'dateOfJoining' }, 
                { data: 'applicationDate' }, 
                { data: 'regionBeforeAmalgamation' }, 
                { data: 'regionDateBeforeAmalgamation' }, 
                { data: 'timeSpentAwayFromRegion' }, 
                { data: 'regionAfterAmalgamation' }, 
                { data: 'regionDateAfterAmalgamation' }, 
                { data: 'appliedRegion1' }, 
                { data: 'appliedRegion2' }, 
                { data: 'appliedRegion3' },
                { data: 'dateOfPromotionToPresentDate' }, 
                { data: 'dateOfReversion' },
                { data: 'dateOfMarriage' }, 
                { data: 'temporaryTransferDetails' },
                { data: 'assetAndLiablitiesDetails' }, 
                { data: 'statusOfSubmission' }, 
                { data: 'comments' },
                { data: 'requestType' },
                { data: 'transferReason' }
            ],

        });
    $("#interRegionalRequestRecords").show();
    ShowRecords();
}
function GetInterZonalPromotionRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#interZonalPromotionRecords')) {
        $('#interZonalPromotionRecords').DataTable().destroy();
    }
    $("#interZonalPromotionRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'employeeId' },
                { data: 'zonePreference1' },
                { data: 'zone1RegionPreference1' },
                { data: 'zone1RegionPreference2' },   
                { data: 'zone1RegionPreference3' },   
                { data: 'zonePreference2' },   
                { data: 'zone2RegionPreference1' },   
                { data: 'zone2RegionPreference2' },   
                { data: 'zone2RegionPreference3' },   
                { data: 'zonePreference3' },   
                { data: 'zone3RegionPreference1' },   
                { data: 'zone3RegionPreference2' },   
                { data: 'zone3RegionPreference3' },   
                { data: 'zonePreference4' },   
                { data: 'zone4RegionPreference1' },   
                { data: 'zone4RegionPreference2' },   
                { data: 'zone4RegionPreference3' },   
                { data: 'zonePreference5' },   
                { data: 'zone5RegionPreference1' },   
                { data: 'zone5RegionPreference2' },   
                { data: 'zone5RegionPreference3' }   
                
            ],

        });
    $("#interZonalPromotionRecords").show();
    ShowRecords();
}
function GetInterZonalRequestTransferRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#interZonalRequestRecords')) {
        $('#interZonalRequestRecords').DataTable().destroy();
    }
    $("#interZonalRequestRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [
                { data: 'batchId' },
                { data: 'employeeId' },


                { data: 'employeeName' },  
                { data: 'gender' },  
                { data: 'designation' },  
                { data: 'scale' },  
                { data: 'locationName' },  
                { data: 'currentRo' },  
                { data: 'zoneBeforeAmalgamation' },    
                { data: 'zoneDateBeforeAmalgamation' },    
                { data: 'timeAwayFromZone' },    
                { data: 'zoneAfterAmalgamation' },    
                { data: 'zoneDateAfterAmalgamation' },    
                { data: 'directPromotee' },    
                { data: 'transferCategory' },    
                { data: 'temporaryTransferMonth' },    
                { data: 'transferSequenceNumber' },    
                { data: 'appliedState' },    
                { data: 'appliedZone' },    
                { data: 'appliedRegion1' },    
                { data: 'appliedRegion2' },    
                { data: 'appliedRegion3' },    
                { data: 'transferReason' },    
                { data: 'applicationDate' },    
                { data: 'diarisedDate' },  
                { data: 'status' },    
                { data: 'dateOfPromotion' },    
                { data: 'dateOfReversion' },    
                { data: 'disabled' },    
                { data: 'dateOfMarriage' },     
                { data: 'specialistCategory' },    
                { data: 'transferType' },    
                { data: 'temporaryTransferDetails' },    
                { data: 'assetsAndLiabilitiesDetail' },    
                { data: 'statusOfSubmissionofApproval' },    
                { data: 'comments' },    
                { data: 'requestType' }   

            ],

        });
    $("#interZonalRequestRecords").show();
    ShowRecords();
}
function GetRegionMasterRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#regionMasterRecords')) {
        $('#regionMasterRecords').DataTable().destroy();
    }
    $("#regionMasterRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [

                { data: 'batchId' },    
                { data: 'regionCode' },  
                { data: 'regionName' },  
                { data: 'zoneCode' },  
                { data: 'zoneName' },  
                { data: 'state' },  
                { data: 'stateId' },  
                { data: 'district' },  
                { data: 'districtId' },  
                { data: 'city' },  
                { data: 'cityId' }  

            ],

        });
    $("#regionMasterRecords").show();
    ShowRecords();
}
function GetZoneMasterRecords(BatchId) {
    if ($.fn.dataTable.isDataTable('#zoneMasterRecords')) {
        $('#zoneMasterRecords').DataTable().destroy();
    }
    $("#zoneMasterRecords").DataTable(
        {
            clear: true,
            serverSide: true,
            /*searching: false,*/
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },
            dom: 'lBfrtip',
            columns: [

                { data: 'batchId' },
                { data: 'zoneCode' },
                { data: 'zoneName' },
                { data: 'state' },
                { data: 'stateId' },
                { data: 'district' },
                { data: 'districtId' },
                { data: 'city' },
                { data: 'cityId' }

            ],

        });
    $("#zoneMasterRecords").show();
    ShowRecords();
}


function ShowRecords() {
    $("#recordsDiv").show();
}