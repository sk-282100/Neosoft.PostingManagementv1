using System.Collections.Generic;

namespace PostingManagement.UI.Models.Responses
{


    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }
        public BaseResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public BaseResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

    public class PagedResponse<T> : BaseResponse<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int totalCount, int page, int pageSize)
        {
            this.TotalCount = totalCount;
            this.Page = page;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
