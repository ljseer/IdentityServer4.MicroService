﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Validation;

namespace IdentityServer4.MicroService.Tenant
{
    public class TenantTokenRequestValidator : ICustomTokenRequestValidator
    {
        HttpContext _context;

        public TenantTokenRequestValidator(IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor.HttpContext;
        }

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            if (!context.Result.IsError)
            {
                var TenantClaim = _context.GetTenantClaim();

                if (TenantClaim != null)
                {
                    context.Result.ValidatedRequest.ClientClaims.Add(TenantClaim);
                }
            }

            return Task.CompletedTask;
        }
    }
}
