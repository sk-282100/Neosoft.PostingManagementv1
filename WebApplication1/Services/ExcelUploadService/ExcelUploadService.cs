
using Newtonsoft.Json;
using OfficeOpenXml;
using PostingManagement.UI.Exceptions;
using PostingManagement.UI.Helpers.Constants;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using System.Text;

namespace PostingManagement.UI.Services.ExcelUploadService
{
    public class ExcelUploadService : IExcelUploadService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public ExcelUploadService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<Response<List<UploadHistoryDetails>>> GetUploadHistories(int fileTypeCode)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/ExcelUpload/GetUploadHistoryList?fileTypeCode=" + fileTypeCode))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var uploadResult = JsonConvert.DeserializeObject<Response<List<UploadHistoryDetails>>>(apiResponse);
                    return uploadResult;
                }
            }
        }
        public async Task<Response<GetWorkFlowStatus>> GetWorkFlowStaus()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/ExcelUpload/GetWorkflowStatus"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<GetWorkFlowStatus>>(apiResponse);
                    return result;
                }
            }
        }
        public async Task<Response<bool>> ResetWorkflow()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/ExcelUpload/ResetWorkflow"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Response<bool>>(apiResponse);
                    return result;
                }
            }
        }

        public async Task<string> GetUploadedRecordsByBatchId(int batchId, int fileTypeCode)
        {
            var request = new UploadedRecords() { BatchId = batchId, FileTypeCode = fileTypeCode };
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/v1/ExcelUpload/GetAllRecords?fileTypeCode=" + request.FileTypeCode + "&batchId=" + request.BatchId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }
            }
        }

        public async Task<ExcelUploadResponseModel> UploadExcel(ExcelUploadViewModel model, string uploadedBy)
        {
            //calling the Upload Function for Validation and Upload according to FileType
            try
            {
                if (model.FileType == ExcelFileUploadName.BranchMaster)
                {
                    return await BranchMasterFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.EmployeeMaster)
                {
                    return await EmployeeMasterFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.InterRegionPromotion)
                {
                    return await InterRegionalPromotionFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.InterRegionRequestTransfer)
                {
                    return await InterRegionRequestTransferFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.InterZonalPromotion)
                {
                    return await InterZonalPromotionFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.InterZonalRequestTranfer)
                {
                    return await InterZonalRequestTransferFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.RegionMaster)
                {
                    return await RegionMasterFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.ZoneMaster)
                {
                    return await ZoneMasterFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.DepartmentMaster)
                {
                    return await DepartmentMasterFileUpload(model.ExcelFile, uploadedBy);
                }
                else if (model.FileType == ExcelFileUploadName.VacancyPool)
                {
                    return await VacancyPoolFileUpload(model.ExcelFile, uploadedBy);
                }
                else
                {
                    return new ExcelUploadResponseModel() { Succeeded = false, Message = "File type not found" };
                }
            }
            catch (Exception e)
            {
                ExcelUploadResult data = new ExcelUploadResult() { SuccessCount = 0, UploadStatus = "Failed" };
                return new ExcelUploadResponseModel() { Succeeded = false, Message = e.Message, Data = data };
            }

        }

        #region Functions for Excel Format Validation and Upload 
        private async Task<ExcelUploadResponseModel> DepartmentMasterFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel Format
            string[] excelColumns = new string[] {
            "DEPARTMENT_CODE", "DEPARTMENT_NAME"
            };
            //Intializing List for storing the Excel data 
            List<DepartmentMaster> listDepartmentMaster = new List<DepartmentMaster>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 2; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing the empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List 
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DepartmentMaster obj = new DepartmentMaster();
                        obj.DepartmentCode = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        obj.DepartmentName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();

                        listDepartmentMaster.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<DepartmentMaster> request = new ExcelUploadRequest<DepartmentMaster>() { FileName = fileName, FileData = listDepartmentMaster };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/DepartmentMasterExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> ZoneMasterFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel format
            string[] excelColumns = new string[]
                        {
                "ZONECODE","ZONENAME","STATE","STATE_ID","DISTRICT","DISTRICT_ID","CITY","CITY_ID"
                        };
            //Intializing List for storing the Excel data 
            List<ZoneMaster> zoneMasterList = new List<ZoneMaster>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 8; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }
                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List 
                    for (int row = 2; row <= rowCount; row++)
                    {
                        ZoneMaster obj = new ZoneMaster();

                        #region ZoneMaster object assigned
                        obj.ZoneCode = Convert.ToString(worksheet.Cells[row, 1].Value);
                        obj.ZoneName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.State = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.StateId = Convert.ToInt32(worksheet.Cells[row, 4].Value);
                        obj.District = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.DistrictId = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                        obj.City = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        obj.CityId = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                        #endregion

                        zoneMasterList.Add(obj);

                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<ZoneMaster> request = new ExcelUploadRequest<ZoneMaster>() { FileName = fileName, FileData = zoneMasterList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/ZoneMasterExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> RegionMasterFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel format
            string[] excelColumns = new string[]
                        {
                "REGIONCODE","REGION_NAME","ZONECODE","ZONENAME","STATE","STATE_ID","DISTRICT","DISTRICT_ID","CITY","CITY_ID"
                        };
            //Intializing List for storing the Excel data 
            List<RegionMaster> RegionMasterList = new List<RegionMaster>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 10; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List 
                    for (int row = 2; row <= rowCount; row++)
                    {
                        RegionMaster obj = new RegionMaster();

                        #region regionMaster object assigned
                        obj.RegionCode = Convert.ToString(worksheet.Cells[row, 1].Value);
                        obj.RegionName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.ZoneCode = Convert.ToString(worksheet.Cells[row, 3].Value);
                        obj.ZoneName = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.State = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.StateId = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                        obj.District = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        obj.DistrictId = Convert.ToInt32(worksheet.Cells[row, 8].Value);
                        obj.City = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();
                        obj.CityId = Convert.ToInt32(worksheet.Cells[row, 10].Value);
                        #endregion

                        RegionMasterList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<RegionMaster> request = new ExcelUploadRequest<RegionMaster>() { FileName = fileName, FileData = RegionMasterList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/RegionMasterExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> InterZonalRequestTransferFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel Format
            string[] excelColumns = new string[]
                        {
                "EMPLID","NAME","GENDER","DESIGNATION","SCALE","LOCATION_NAME","CURRENT_RO","ZONE_BEFORE_AMALGAMATION","ZONE_DATE_BEFORE_AMALGAMATION","TIME_AWAY_FROM_ZONE","ZONE_AFTER_AMALGAMATION","ZONE_DATE_AFTER_AMALGAMATION","DIRECT_PROMOTEE","TRANSFER_CATEGORY","TEMPORARY_TRANSFER_MONTH","TRANSFER_SEQ_NO","APPLIED_STATE","APPLIED_ZONE","APPLIED_REGION_1","APPLIED_REGION_2","APPLIED_REGION_3","TRANSFER_REASON","APPLICATION_DATE","DIARISED_DATE","STATUS","DATE_OF_PROMOTION","DATE_OF_REVERSION","DISABLED","DATE_OF_MARRIAGE","SPECIALIST_CATEGORY","TRANSFER_TYPE","TEMPORARY_TRANSFER_DETAILS","ASSESTS_AND_LIABILITIES_DETAIL","STATUS_OF_SUBMISSION_OF_APPR","COMMENTS","REQUEST_TYPE"
                        };

            //Intializing List for storing the Excel data 
            List<InterZonalRequestTransfer> InterZonalRequestTransferList = new List<InterZonalRequestTransfer>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 36; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing Empty rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel Data to List
                    for (int row = 2; row <= rowCount; row++)
                    {
                        InterZonalRequestTransfer obj = new InterZonalRequestTransfer();

                        #region InterZonalRequestTransfer object assigned 
                        obj.EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        obj.EmployeeName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.Gender = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.Designation = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.Scale = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.LocationName = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        obj.CurrentRo = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        obj.ZoneBeforeAmalgamation = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                        obj.ZoneDateBeforeAmalgamation = Convert.ToDateTime(worksheet.Cells[row, 9].Value);
                        obj.TimeAwayFromZone = Convert.ToString(worksheet.Cells[row, 10].Value).Trim();
                        obj.ZoneAfterAmalgamation = Convert.ToString(worksheet.Cells[row, 11].Value).Trim();
                        obj.ZoneDateAfterAmalgamation = Convert.ToDateTime(worksheet.Cells[row, 12].Value);
                        obj.DirectPromotee = Convert.ToString(worksheet.Cells[row, 13].Value).Trim();
                        obj.TransferCategory = Convert.ToString(worksheet.Cells[row, 14].Value).Trim();
                        obj.TemporaryTransferMonth = Convert.ToString(worksheet.Cells[row, 15].Value).Trim();
                        obj.TransferSequenceNumber = Convert.ToString(worksheet.Cells[row, 16].Value).Trim();
                        obj.AppliedState = Convert.ToString(worksheet.Cells[row, 17].Value).Trim();
                        obj.AppliedZone = Convert.ToString(worksheet.Cells[row, 18].Value).Trim();
                        obj.AppliedRegion1 = Convert.ToString(worksheet.Cells[row, 19].Value).Trim();
                        obj.AppliedRegion2 = Convert.ToString(worksheet.Cells[row, 20].Value).Trim();
                        obj.AppliedRegion3 = Convert.ToString(worksheet.Cells[row, 21].Value).Trim();
                        obj.TransferReason = Convert.ToString(worksheet.Cells[row, 22].Value).Trim();
                        obj.ApplicationDate = Convert.ToDateTime(worksheet.Cells[row, 23].Value);
                        obj.DiarisedDate = Convert.ToDateTime(worksheet.Cells[row, 24].Value);
                        obj.Status = Convert.ToString(worksheet.Cells[row, 25].Value).Trim();
                        obj.DateOfPromotion = Convert.ToDateTime(worksheet.Cells[row, 26].Value);
                        obj.DateOfReversion = Convert.ToDateTime(worksheet.Cells[row, 27].Value);
                        obj.Disabled = Convert.ToString(worksheet.Cells[row, 28].Value).Trim();
                        obj.DateOfMarriage = Convert.ToDateTime(worksheet.Cells[row, 29].Value);
                        obj.SpecialistCategory = Convert.ToString(worksheet.Cells[row, 30].Value).Trim();
                        obj.TransferType = Convert.ToString(worksheet.Cells[row, 31].Value).Trim();
                        obj.TemporaryTransferDetails = Convert.ToString(worksheet.Cells[row, 32].Value).Trim();
                        obj.AssetsAndLiabilitiesDetail = Convert.ToString(worksheet.Cells[row, 33].Value).Trim();
                        obj.StatusOfSubmissionofApproval = Convert.ToString(worksheet.Cells[row, 34].Value).Trim();
                        obj.Comments = Convert.ToString(worksheet.Cells[row, 35].Value).Trim();
                        obj.RequestType = Convert.ToString(worksheet.Cells[row, 36].Value).Trim();
                        #endregion
                        InterZonalRequestTransferList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<InterZonalRequestTransfer> request = new ExcelUploadRequest<InterZonalRequestTransfer>() { FileName = fileName, FileData = InterZonalRequestTransferList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/InterZonalRequestTransferExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> InterZonalPromotionFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel Format
            string[] excelColumns = new string[]
                        {
                "EMPLID","ZONE_PREFERENCE1","ZONE_1_REGION_PREFERENCE1","ZONE_1_REGION_PREFERENCE2","ZONE_1_REGION_PREFERENCE3","ZONE_PREFERENCE2","ZONE_2_REGION_PREFERENCE1","ZONE_2_REGION_PREFERENCE2","ZONE_2_REGION_PREFERENCE3","ZONE_PREFERENCE3","ZONE_3_REGION_PREFERENCE1","ZONE_3_REGION_PREFERENCE2","ZONE_3_REGION_PREFERENCE3","ZONE_PREFERENCE4","ZONE_4_REGION_PREFERENCE1","ZONE_4_REGION_PREFERENCE2","ZONE_4_REGION_PREFERENCE3","ZONE_PREFERENCE5","ZONE_5_REGION_PREFERENCE1","ZONE_5_REGION_PREFERENCE2","ZONE_5_REGION_PREFERENCE3"
                        };

            //Intializing List for storing the Excel data 
            List<InterZonalPromotion> InterZonalPromotionList = new List<InterZonalPromotion>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 21; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing Empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Conkverting Excel data to List
                    for (int row = 2; row <= rowCount; row++)
                    {
                        InterZonalPromotion obj = new InterZonalPromotion();

                        #region InterZonalPromotion object assigned
                        obj.EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        obj.ZonePreference1 = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.Zone1RegionPreference1 = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.Zone1RegionPreference2 = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.Zone1RegionPreference3 = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.ZonePreference2 = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        obj.Zone2RegionPreference1 = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        obj.Zone2RegionPreference2 = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                        obj.Zone2RegionPreference3 = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();
                        obj.ZonePreference3 = Convert.ToString(worksheet.Cells[row, 10].Value).Trim();
                        obj.Zone3RegionPreference1 = Convert.ToString(worksheet.Cells[row, 11].Value).Trim();
                        obj.Zone3RegionPreference2 = Convert.ToString(worksheet.Cells[row, 12].Value).Trim();
                        obj.Zone3RegionPreference3 = Convert.ToString(worksheet.Cells[row, 13].Value).Trim();
                        obj.ZonePreference4 = Convert.ToString(worksheet.Cells[row, 14].Value).Trim();
                        obj.Zone4RegionPreference1 = Convert.ToString(worksheet.Cells[row, 15].Value).Trim();
                        obj.Zone4RegionPreference2 = Convert.ToString(worksheet.Cells[row, 16].Value).Trim();
                        obj.Zone4RegionPreference3 = Convert.ToString(worksheet.Cells[row, 17].Value).Trim();
                        obj.ZonePreference5 = Convert.ToString(worksheet.Cells[row, 18].Value).Trim();
                        obj.Zone5RegionPreference1 = Convert.ToString(worksheet.Cells[row, 19].Value).Trim();
                        obj.Zone5RegionPreference2 = Convert.ToString(worksheet.Cells[row, 20].Value).Trim();
                        obj.Zone5RegionPreference3 = Convert.ToString(worksheet.Cells[row, 21].Value).Trim();
                        #endregion

                        InterZonalPromotionList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<InterZonalPromotion> request = new ExcelUploadRequest<InterZonalPromotion>() { FileName = fileName, FileData = InterZonalPromotionList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/InterZonalPromotionExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> InterRegionRequestTransferFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel  format
            string[] excelColumns = new string[]
                        {
                "EMPLID","TRANSFER_SEQ_NO","GENDER","DIRECT_PROMOTEE","SCALE","TRANSFER_TYPE","NAME","DESIGNATION","LOCATION_NAME","CURRENT_RO","FROM_ZONE","REQUIRED_STATE","APPLIED_ZONE","DATE_OF_JOINING","APPLICATION_DATE","REGION_BEFORE_AMALGAMATION","REGION_DATE_BEFORE_AMALGMATION","TIME_SPENT_AWAY_FROM_REGION","REGION_AFTER_AMALGAMATION","REGION_DATE_AFTER_AMALGMATION","APPLIED_REGION_1","APPLIED_REGION_2","APPLIED_REGION_3","DT_OF_PROMOTION_TO_PRSNT_SCALE","DATE_OF_REVERSION","DATE_OF_MARRIAGE","TEMPORARY_TRANSFER_DETAILS","ASSEST_AND_LIABILITIES_DETAILS","STATUS_OF_SUBMISSION","COMMENTS","REQUEST_TYPE","TRANSFER_REASON"
                        };
            //Intializing List for storing the Excel data 
            List<InterRegionRequestTransfer> InterRegionRequestTransferList = new List<InterRegionRequestTransfer>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 32; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing Empty rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List
                    for (int row = 2; row <= rowCount; row++)
                    {
                        InterRegionRequestTransfer obj = new InterRegionRequestTransfer();

                        #region InterRegionRequestTransfer object assigned
                        obj.EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        obj.TranferSequenceNo = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.Gender = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.DirectPromotee = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.Scale = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.TransferType = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        obj.Name = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                        obj.Designation = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                        obj.LocationName = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();
                        obj.CurrentRo = Convert.ToString(worksheet.Cells[row, 10].Value).Trim();
                        obj.FromZone = Convert.ToString(worksheet.Cells[row, 11].Value).Trim();
                        obj.RequiredState = Convert.ToString(worksheet.Cells[row, 12].Value).Trim();
                        obj.AppliedZone = Convert.ToString(worksheet.Cells[row, 13].Value).Trim();
                        obj.DateOfJoining = Convert.ToDateTime(worksheet.Cells[row, 14].Value);
                        obj.ApplicationDate = Convert.ToDateTime(worksheet.Cells[row, 15].Value);
                        obj.RegionBeforeAmalgamation = Convert.ToString(worksheet.Cells[row, 16].Value).Trim();
                        obj.RegionDateBeforeAmalgamation = Convert.ToDateTime(worksheet.Cells[row, 17].Value);
                        obj.TimeSpentAwayFromRegion = Convert.ToString(worksheet.Cells[row, 18].Value).Trim();
                        obj.RegionAfterAmalgamation = Convert.ToString(worksheet.Cells[row, 19].Value).Trim();
                        obj.RegionDateAfterAmalgamation = Convert.ToDateTime(worksheet.Cells[row, 20].Value);
                        obj.AppliedRegion1 = Convert.ToString(worksheet.Cells[row, 21].Value).Trim();
                        obj.AppliedRegion2 = Convert.ToString(worksheet.Cells[row, 22].Value).Trim();
                        obj.AppliedRegion3 = Convert.ToString(worksheet.Cells[row, 23].Value).Trim();
                        obj.DateOfPromotionToPresentDate = Convert.ToDateTime(worksheet.Cells[row, 24].Value);
                        obj.DateOfReversion = Convert.ToDateTime(worksheet.Cells[row, 25].Value);
                        obj.DateOfMarriage = Convert.ToDateTime(worksheet.Cells[row, 26].Value);
                        obj.TemporaryTransferDetails = Convert.ToString(worksheet.Cells[row, 27].Value).Trim();
                        obj.AssetAndLiablitiesDetails = Convert.ToString(worksheet.Cells[row, 28].Value).Trim();
                        obj.StatusOfSubmission = Convert.ToString(worksheet.Cells[row, 29].Value).Trim();
                        obj.Comments = Convert.ToString(worksheet.Cells[row, 30].Value).Trim();
                        obj.RequestType = Convert.ToString(worksheet.Cells[row, 31].Value).Trim();
                        obj.TransferReason = Convert.ToString(worksheet.Cells[row, 32].Value).Trim();
                        #endregion

                        InterRegionRequestTransferList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<InterRegionRequestTransfer> request = new ExcelUploadRequest<InterRegionRequestTransfer>() { FileName = fileName, FileData = InterRegionRequestTransferList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/InterRegionRequestTransferExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> InterRegionalPromotionFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel Format
            string[] excelColumns = new string[]
                        {
                "EMPLID","CURRENT_SCALE","PROMOTED_SCALE","ACTION","EFFDT","PROMOTION_TYPE"
                        };
            //Intializing List for storing the Excel data 
            List<InterRegionalPromotion> InterRegionalPromotionList = new List<InterRegionalPromotion>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 6; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing Empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List
                    for (int row = 2; row <= rowCount; row++)
                    {
                        InterRegionalPromotion obj = new InterRegionalPromotion();

                        #region InterRegionalPromotion object Assigned
                        obj.EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                        obj.CurrentScale = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.PromotedScale = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.Action = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.Effdt = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                        obj.PromotionType = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        #endregion

                        InterRegionalPromotionList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<InterRegionalPromotion> request = new ExcelUploadRequest<InterRegionalPromotion>() { FileName = fileName, FileData = InterRegionalPromotionList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/InterRegionalPromotionExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> BranchMasterFileUpload(IFormFile excelFile, string uploadedBy)
        {
            //Excel Format
            string[] excelColumns = new string[] {
            "OLD_BRANCH_CODE", "BRANCH_CODE", "BRANCH_NAME", "STATE","STATE_ID","DISTRICT","DISTRICT_ID","CITY","CITY_ID", "AREA","DATE_OF_OPENING","ZONE_NAME","ZONE_CODE","REGION_CODE", "REGION_NAME", "BRANCH_TYPE", "BANK_NAME","ADMINSTRATIVE_FLAG"
            };
            //Intializing List for storing the Excel data 
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 18; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing empty rows from excel 
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }

                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }

                    //convert the worksheet to list of object
                    rowCount = worksheet.Dimension.Rows;
                    List<BranchMaster> excelDataList = new List<BranchMaster>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        BranchMaster obj = new BranchMaster();

                        #region BranchMaster Object assigned
                        obj.OldBranchCode = Convert.ToString(worksheet.Cells[row, 1].Value).Trim();
                        obj.BranchCode = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.BranchName = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.State = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.StateId = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                        obj.District = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                        obj.DistrictId = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                        obj.City = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                        obj.CityId = Convert.ToInt32(worksheet.Cells[row, 9].Value);
                        obj.Area = Convert.ToString(worksheet.Cells[row, 10].Value).Trim();
                        obj.DateOfOpening = Convert.ToDateTime(worksheet.Cells[row, 11].Value);
                        obj.ZoneName = Convert.ToString(worksheet.Cells[row, 12].Value).Trim();
                        obj.ZoneCode = Convert.ToString(worksheet.Cells[row, 13].Value).Trim();
                        obj.RegionCode = Convert.ToString(worksheet.Cells[row, 14].Value).Trim();
                        obj.RegionName = Convert.ToString(worksheet.Cells[row, 15].Value).Trim();
                        obj.BranchType = Convert.ToString(worksheet.Cells[row, 16].Value).Trim();
                        obj.BankName = Convert.ToString(worksheet.Cells[row, 17].Value).Trim();
                        obj.AdministrativeFlag = Convert.ToString(worksheet.Cells[row, 18].Value).Trim();
                        #endregion

                        excelDataList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<BranchMaster> request = new ExcelUploadRequest<BranchMaster>() { FileName = fileName, FileData = excelDataList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/BranchMasterExcelUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }

                }
            }
        }

        private async Task<ExcelUploadResponseModel> EmployeeMasterFileUpload(IFormFile excelFile, string uploadedBy)
        {
            if (excelFile == null)
            {
                return null;
            }
            else
            {
                //Excel format
                string[] excelColumns = new string[]
                            {
                "EMPLID","EMP_NAME","LOCATION","LOCATION_DESR","DEPT_RO","REGIONCODE","REGION_NAME","ZONECODE","ZONENAME","JOBCODE","DESIGNATION","ROLE_START_DATE","SCALE_CODE","SCALE","UBI_JOB_ROLE","BANK_NAME","BRTH_DT","SEX","DISABILITY","LOCATION_START_DATE","DOMICILE_ZONE","RO_START_DATE","LAST_PROMOTION_DATE","ZO_START_DATE"
                            };
                //Intializing List for storing the Excel data 
                List<EmployeeMaster> excelDataList = new List<EmployeeMaster>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        //Checking the format of the Excel file 
                        for (int col = 1; col <= 24; col++)
                        {
                            if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                            {
                                throw new InvalideExcelFormatException("Excel is not in correct format");
                            }
                        }
                        //Removing Empty Rows from Excel 
                        var rowCount = worksheet.Dimension.Rows;
                        var maxColumns = worksheet.Dimension.Columns;
                        for (int row = rowCount; row > 1; row--)
                        {
                            bool isRowEmpty = true;
                            for (int column = 1; column <= maxColumns; column++)
                            {
                                var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                                if (!string.IsNullOrEmpty(cellEntry))
                                {
                                    isRowEmpty = false;
                                    break;
                                }

                            }
                            if (!isRowEmpty)
                                continue;
                            else
                                worksheet.DeleteRow(row);
                        }
                        rowCount = worksheet.Dimension.Rows;

                        //Converting Excel data to List
                        for (int row = 2; row <= rowCount; row++)
                        {
                            EmployeeMaster obj = new EmployeeMaster();

                            #region  EmployeeMaster object assigned
                            obj.EmployeeId = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                            obj.EmployeeName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                            obj.Location = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                            obj.LocationDesired = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                            obj.DepartmentRo = Convert.ToString(worksheet.Cells[row, 5].Value).Trim();
                            obj.RegionCode = Convert.ToString(worksheet.Cells[row, 6].Value).Trim();
                            obj.RegionName = Convert.ToString(worksheet.Cells[row, 7].Value).Trim();
                            obj.ZoneCode = Convert.ToString(worksheet.Cells[row, 8].Value).Trim();
                            obj.ZoneName = Convert.ToString(worksheet.Cells[row, 9].Value).Trim();
                            obj.JobCode = Convert.ToString(worksheet.Cells[row, 10].Value).Trim();
                            obj.Designation = Convert.ToString(worksheet.Cells[row, 11].Value).Trim();
                            obj.RoleStartDate = Convert.ToDateTime(worksheet.Cells[row, 12].Value);
                            obj.ScaleCode = Convert.ToString(worksheet.Cells[row, 13].Value).Trim();
                            obj.Scale = Convert.ToString(worksheet.Cells[row, 14].Value).Trim();
                            obj.UbijobRole = Convert.ToString(worksheet.Cells[row, 15].Value).Trim();
                            obj.BankName = Convert.ToString(worksheet.Cells[row, 16].Value).Trim();
                            obj.BirthDate = Convert.ToDateTime(worksheet.Cells[row, 17].Value);
                            obj.Sex = Convert.ToString(worksheet.Cells[row, 18].Value).Trim();
                            obj.Disability = Convert.ToString(worksheet.Cells[row, 19].Value).Trim();
                            obj.LocationStartDate = Convert.ToDateTime(worksheet.Cells[row, 20].Value);
                            obj.DomicileZone = Convert.ToString(worksheet.Cells[row, 21].Value).Trim();
                            obj.RostartDate = Convert.ToDateTime(worksheet.Cells[row, 22].Value);
                            obj.LastPromotionDate = Convert.ToDateTime(worksheet.Cells[row, 23].Value);
                            obj.ZostartDate = Convert.ToDateTime(worksheet.Cells[row, 24].Value);
                            #endregion

                            excelDataList.Add(obj);
                        }

                        //create the request for data upload
                        string fileName = excelFile.FileName;
                        ExcelUploadRequest<EmployeeMaster> request = new ExcelUploadRequest<EmployeeMaster>() { FileName = fileName, FileData = excelDataList };

                        //Client Handler Part
                        using (var httpClient = new HttpClient(_clientHandler))
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                            string name = uploadedBy;
                            using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/EmployeeMasterExcelUpload?username=" + name, content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                                return uploadResult;
                            }
                        }
                    }
                }
            }
        }

        private async Task<ExcelUploadResponseModel> VacancyPoolFileUpload(IFormFile excelFile, string uploadedBy)
        {

            //Excel Format
            string[] excelColumns = new string[]
                        {
                "ZoneCode","ZoneName","RegionCode","RegionName","No Of Vacancy(Zone)","No Of Vacancy(Region)"
                        };
            //Intializing List for storing the Excel data 
            List<VacancyPool> VacancyPoolList = new List<VacancyPool>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await excelFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    //Checking the format of the Excel file 
                    for (int col = 1; col <= 6; col++)
                    {
                        if (excelColumns[col - 1] != Convert.ToString(worksheet.Cells[1, col].Value))
                        {
                            throw new InvalideExcelFormatException("Excel is not in correct format");
                        }
                    }

                    //Removing Empty Rows
                    var rowCount = worksheet.Dimension.Rows;
                    var maxColumns = worksheet.Dimension.Columns;
                    for (int row = rowCount; row > 1; row--)
                    {
                        bool isRowEmpty = true;
                        for (int column = 1; column <= maxColumns; column++)
                        {
                            var cellEntry = Convert.ToString(worksheet.Cells[row, column].Value);
                            if (!string.IsNullOrEmpty(cellEntry))
                            {
                                isRowEmpty = false;
                                break;
                            }
                        }
                        if (!isRowEmpty)
                            continue;
                        else
                            worksheet.DeleteRow(row);
                    }
                    rowCount = worksheet.Dimension.Rows;

                    //Converting Excel data to List
                    for (int row = 2; row <= rowCount; row++)
                    {
                        VacancyPool obj = new VacancyPool();

                        #region Vacancy Pool object Assigned
                        obj.ZoneCode = Convert.ToString(worksheet.Cells[row, 1].Value).Trim();
                        obj.ZoneName = Convert.ToString(worksheet.Cells[row, 2].Value).Trim();
                        obj.RegionCode = Convert.ToString(worksheet.Cells[row, 3].Value).Trim();
                        obj.RegionName = Convert.ToString(worksheet.Cells[row, 4].Value).Trim();
                        obj.NoOfVacancyZone = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                        obj.NoOfVacancyRegion = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                        #endregion

                        VacancyPoolList.Add(obj);
                    }

                    //create the request for data upload
                    string fileName = excelFile.FileName;
                    ExcelUploadRequest<VacancyPool> request = new ExcelUploadRequest<VacancyPool>() { FileName = fileName, FileData = VacancyPoolList };

                    //Client Handler Part
                    using (var httpClient = new HttpClient(_clientHandler))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        string name = uploadedBy;
                        using (var response = await httpClient.PostAsync("https://localhost:5000/api/v1/ExcelUpload/VacancyPoolUpload?username=" + name, content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var uploadResult = JsonConvert.DeserializeObject<ExcelUploadResponseModel>(apiResponse);
                            return uploadResult;
                        }
                    }
                }
            }
            
        }
        
        #endregion
    }
}
