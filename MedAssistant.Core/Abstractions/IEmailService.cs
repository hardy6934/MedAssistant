﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IEmailService
    {
        void SendEmailForRecoveryPassword(string email, string path);
    }
}
