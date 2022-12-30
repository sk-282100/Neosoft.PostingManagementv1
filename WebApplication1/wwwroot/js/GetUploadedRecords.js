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
                { data: 'batchId', name: 'batchId' },
                { data: 'departmentCode', name: 'departmentCode' },
                { data: 'departmentName', name: 'departmentName' }
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
                { data: 'batchId', name: 'batchId' },
                { data: 'employeeId', name: 'employeeId' },
                { data: 'employeeName', name: 'employeeName' },
                { data: 'location', name: 'location' },
                { data: 'locationDesired', name: 'locationDesired' },
                { data: 'departmentRo', name: 'departmentRo' },
                { data: 'regionCode', name: 'regionCode' },
                { data: 'regionName', name: 'regionName' },
                { data: 'zoneCode', name: 'zoneCode' },
                { data: 'zoneName', name:'zoneName' },
                { data: 'jobCode', name: 'jobCode' },
                { data: 'designation', name: 'designation' },
                { data: 'roleStartDate', name: 'roleStartDate' },
                { data: 'scaleCode', name: 'scaleCode' },
                { data: 'scale', name: 'scale' },
                { data: 'ubijobRole', name: 'ubijobRole' },
                { data: 'bankName', name: 'bankName' },
                { data: 'birthDate', name: 'birthDate' },
                { data: 'sex', name: 'sex' },
                { data: 'disability', name: 'disability' },
                { data: 'locationStartDate', name: 'locationStartDate' },
                { data: 'domicileZone', name: 'domicileZone' },
                { data: 'rostartDate', name: 'rostartDate' },
                { data: 'lastPromotionDate', name: 'lastPromotionDate' },
                { data: 'zostartDate', name: 'zostartDate' }
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
                { data: 'batchId', name: 'batchId' },
                { data: 'oldBranchCode', name: 'oldBranchCode' },
                { data: 'branchCode', name: 'branchCode' },
                { data: 'branchName', name: 'branchName' },
                { data: 'state', name:'state' },
                { data: 'stateId', name:'stateId' },
                { data: 'district', name: 'district' },
                { data: 'districtId', name: 'districtId' },
                { data: 'city', name: 'city' },
                { data: 'cityId', name:'cityId' },
                { data: 'area', name: 'area' },
                { data: 'dateOfOpening', name: 'dateOfOpening' },
                { data: 'zoneName', name: 'zoneName' },
                { data: 'zoneCode', name: 'zoneCode' },
                { data: 'regionCode', name: 'regionCode' },
                { data: 'regionName', name: 'regionName' },
                { data: 'branchType', name: 'branchType' },
                { data: 'bankName', name: 'bankName' },
                { data: 'administrativeFlag', name: 'administrativeFlag' }
                
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
                { data: 'batchId', name: 'batchId' },                     
                { data: 'employeeId', name: 'employeeId' },
                { data: 'currentScale', name: 'currentScale' },
                { data: 'promotedScale', name: 'promotedScale' },
                { data: 'action', name: 'action' },
                { data: 'effdt', name: 'effdt' },
                { data: 'promotionType', name: 'promotionType' }
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
                { data: 'batchId', name: 'batchId' },
                { data: 'employeeId', name: 'employeeId'},
                { data: 'tranferSequenceNo', name: 'tranferSequenceNo' },
                { data: 'gender', name: 'gender' }, 
                { data: 'directPromotee', name: 'directPromotee' }, 
                { data: 'scale', name: 'scale' }, 
                { data: 'transferType', name: 'transferType' }, 
                { data: 'name', name: 'name' }, 
                { data: 'designation', name: 'designation' }, 
                { data: 'locationName', name: 'locationName' },
                { data: 'currentRo', name: 'currentRo' }, 
                { data: 'fromZone', name: 'fromZone' }, 
                { data: 'requiredState', name: 'requiredState' }, 
                { data: 'appliedZone', name: 'appliedZone' }, 
                { data: 'dateOfJoining', name: 'dateOfJoining' }, 
                { data: 'applicationDate', name: 'applicationDate' }, 
                { data: 'regionBeforeAmalgamation', name: 'regionBeforeAmalgamation' }, 
                { data: 'regionDateBeforeAmalgamation', name: 'regionDateBeforeAmalgamation' }, 
                { data: 'timeSpentAwayFromRegion', name: 'timeSpentAwayFromRegion' }, 
                { data: 'regionAfterAmalgamation', name: 'regionAfterAmalgamation' }, 
                { data: 'regionDateAfterAmalgamation', name: 'regionDateAfterAmalgamation' }, 
                { data: 'appliedRegion1', name: 'appliedRegion1' }, 
                { data: 'appliedRegion2', name: 'appliedRegion2' }, 
                { data: 'appliedRegion3', name: 'appliedRegion3' },
                { data: 'dateOfPromotionToPresentDate', name: 'dateOfPromotionToPresentDate' }, 
                { data: 'dateOfReversion', name: 'dateOfReversion' },
                { data: 'dateOfMarriage', name: 'dateOfMarriage' }, 
                { data: 'temporaryTransferDetails', name: 'temporaryTransferDetails' },
                { data: 'assetAndLiablitiesDetails', name: 'assetAndLiablitiesDetails' }, 
                { data: 'statusOfSubmission', name: 'statusOfSubmission' }, 
                { data: 'comments', name: 'comments' },
                { data: 'requestType', name: 'requestType' },
                { data: 'transferReason', name: 'transferReason' }
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
                { data: 'batchId', name: 'batchId' },
                { data: 'employeeId', name: 'employeeId' },
                { data: 'zonePreference1', name: 'zonePreference1' },
                { data: 'zone1RegionPreference1', name: 'zone1RegionPreference1' },
                { data: 'zone1RegionPreference2', name: 'zone1RegionPreference2' },   
                { data: 'zone1RegionPreference3', name: 'zone1RegionPreference3' },   
                { data: 'zonePreference2', name: 'zonePreference2' },   
                { data: 'zone2RegionPreference1', name: 'zone2RegionPreference1' },   
                { data: 'zone2RegionPreference2', name: 'zone2RegionPreference2' },   
                { data: 'zone2RegionPreference3', name: 'zone2RegionPreference3' },   
                { data: 'zonePreference3', name: 'zonePreference3' },   
                { data: 'zone3RegionPreference1', name: 'zone3RegionPreference1' },   
                { data: 'zone3RegionPreference2', name: 'zone3RegionPreference2' },   
                { data: 'zone3RegionPreference3', name: 'zone3RegionPreference3' },   
                { data: 'zonePreference4', name: 'zonePreference4' },   
                { data: 'zone4RegionPreference1', name: 'zone4RegionPreference1' },   
                { data: 'zone4RegionPreference2', name: 'zone4RegionPreference2' },   
                { data: 'zone4RegionPreference3', name: 'zone4RegionPreference3' },   
                { data: 'zonePreference5', name: 'zonePreference5' },   
                { data: 'zone5RegionPreference1', name: 'zone5RegionPreference1' },   
                { data: 'zone5RegionPreference2', name: 'zone5RegionPreference2' },   
                { data: 'zone5RegionPreference3', name: 'zone5RegionPreference3' }   
                
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
                { data: 'batchId', name: 'batchId' },
                { data: 'employeeId', name: 'employeeId' },
                { data: 'employeeName', name: 'employeeName' },  
                { data: 'gender', name: 'gender' },  
                { data: 'designation', name: 'designation' },  
                { data: 'scale', name: 'scale' },  
                { data: 'locationName', name: 'locationName' },  
                { data: 'currentRo', name: 'currentRo' },  
                { data: 'zoneBeforeAmalgamation', name: 'zoneBeforeAmalgamation' },    
                { data: 'zoneDateBeforeAmalgamation', name: 'zoneDateBeforeAmalgamation' },    
                { data: 'timeAwayFromZone', name: 'timeAwayFromZone' },    
                { data: 'zoneAfterAmalgamation', name: 'zoneAfterAmalgamation' },    
                { data: 'zoneDateAfterAmalgamation', name: 'zoneDateAfterAmalgamation' },    
                { data: 'directPromotee', name: 'directPromotee' },    
                { data: 'transferCategory', name: 'transferCategory' },    
                { data: 'temporaryTransferMonth', name: 'temporaryTransferMonth' },    
                { data: 'transferSequenceNumber', name: 'transferSequenceNumber' },    
                { data: 'appliedState', name: 'appliedState' },    
                { data: 'appliedZone', name: 'appliedZone' },    
                { data: 'appliedRegion1', name: 'appliedRegion1' },    
                { data: 'appliedRegion2', name: 'appliedRegion2' },    
                { data: 'appliedRegion3', name: 'appliedRegion3' },    
                { data: 'transferReason', name: 'transferReason' },    
                { data: 'applicationDate', name: 'applicationDate' },    
                { data: 'diarisedDate', name: 'diarisedDate' },  
                { data: 'status', name: 'status' },    
                { data: 'dateOfPromotion', name: 'dateOfPromotion' },    
                { data: 'dateOfReversion', name: 'dateOfReversion' },    
                { data: 'disabled', name: 'disabled' },    
                { data: 'dateOfMarriage', name: 'dateOfMarriage' },     
                { data: 'specialistCategory', name: 'specialistCategory' },    
                { data: 'transferType', name: 'transferType' },    
                { data: 'temporaryTransferDetails', name: 'temporaryTransferDetails' },    
                { data: 'assetsAndLiabilitiesDetail', name: 'assetsAndLiabilitiesDetail' },    
                { data: 'statusOfSubmissionofApproval', name: 'statusOfSubmissionofApproval' },    
                { data: 'comments', name: 'comments' },    
                { data: 'requestType', name: 'requestType' }   

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

                { data: 'batchId', name: 'batchId' },    
                { data: 'regionCode', name: 'regionCode' },  
                { data: 'regionName', name: 'regionName' },  
                { data: 'zoneCode', name: 'zoneCode' },  
                { data: 'zoneName', name: 'zoneName' },  
                { data: 'state', name: 'state' },  
                { data: 'stateId', name: 'stateId' },  
                { data: 'district', name: 'district' },  
                { data: 'districtId', name: 'districtId' },  
                { data: 'city', name: 'city' },  
                { data: 'cityId', name: 'cityId' }  

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

                { data: 'batchId', name: 'batchId' },
                { data: 'zoneCode', name: 'zoneCode' },
                { data: 'zoneName', name: 'zoneName' },
                { data: 'state', name: 'state' },
                { data: 'stateId', name: 'stateId' },
                { data: 'district', name: 'district' },
                { data: 'districtId', name: 'districtId' },
                { data: 'city', name: 'city' },
                { data: 'cityId', name: 'cityId' }

            ],

        });
    $("#zoneMasterRecords").show();
    ShowRecords();
}


function ShowRecords() {
    $("#recordsDiv").show();
}