using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Persistence
{
    public interface ILoginRepository
    {
        public Task<LoginModel> GetDetailsByUsername(string username);
    }
}
