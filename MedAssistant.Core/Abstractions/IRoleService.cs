﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IRoleService
    { 
        Task<int> FindRoleIdByRoleName(string RoleName);
         
    }
}
