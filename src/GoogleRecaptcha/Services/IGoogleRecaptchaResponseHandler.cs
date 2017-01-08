﻿using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.Owin;

namespace GoogleRecaptcha.Services
{
    public interface IGoogleRecaptchaResponseHandler
    {
        Task Handle(IOwinContext context, GoogleRecaptchaResponse result);
    }
}