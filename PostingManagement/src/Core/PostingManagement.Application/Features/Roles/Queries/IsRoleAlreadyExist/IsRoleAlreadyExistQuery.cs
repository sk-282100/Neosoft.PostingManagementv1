﻿using MediatR;
using PostingManagement.Application.Responses;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Application.Features.Roles.Queries.IsRoleAlreadyExist
{
    public class IsRoleAlreadyExistQuery :IRequest<Response<bool>>
    {
        public string RoleName { get; set; }
    }
}
