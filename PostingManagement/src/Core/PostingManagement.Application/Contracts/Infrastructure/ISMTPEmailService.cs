using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Contracts.Infrastructure
{
    public interface ISMTPEmailService
    {
        public Task<bool> SendOTPEmail(string userEmail, int otp,DateTime expiryTime);
    }
}
