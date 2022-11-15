using Newtonsoft.Json;
using OfficeOpenXml;
using PostingManagement.UI.Exceptions;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.ExcelFileTypes;
using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace PostingManagement.UI.Services.ExcelUploadService
{
    public class ExcelUploadService:IExcelUploadService
    {
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public ExcelUploadService()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<ExcelUploadResponseModel> UploadExcel(ExcelUploadViewModel model, string uploadedBy)
        {
            try
            {                
                if (model.FileType == "EmployeeMaster")
                {
                    return await EmployeeMasterFileUpload(model.ExcelFile, uploadedBy);
                }                
                else
                {
                    
                    return new ExcelUploadResponseModel() { Succeeded = false, Message = "File type not found" };
                }                
            }
            catch(Exception e)
            {
                ExcelUploadResult data = new ExcelUploadResult() { SuccessCount = 0, UploadStatus = "Failed" };
                return new ExcelUploadResponseModel() { Succeeded = false, Message = e.Message ,Data=data};
            }
            
        }
        
        private async Task<ExcelUploadResponseModel> EmployeeMasterFileUpload(IFormFile excelFile, string uploadedBy) 
        {
            string[] excelColumns = new string[]
                        {
                "EMPLID","EMP_NAME","LOCATION","LOCATION_DESR","DEPT_RO","REGIONCODE","REGION_NAME","ZONECODE","ZONENAME","JOBCODE","DESIGNATION","ROLE_START_DATE","SCALE_CODE","SCALE","UBI_JOB_ROLE","BANK_NAME","BRTH_DT","SEX","DISABILITY","LOCATION_START_DATE","DOMICILE_ZONE","RO_START_DATE","LAST_PROMOTION_DATE","ZO_START_DATE"
                        };
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
                        //StringContent name = new StringContent(JsonConvert.SerializeObject(uploadedBy), Encoding.UTF8, "application/json");
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
}
