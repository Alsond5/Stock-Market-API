using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StockMarket.Attributes
{
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly int _roleid;

        public RoleAuthorizeAttribute(int roleid) {
            _roleid = roleid;
        }

        public int RoleId => _roleid;

        public override string ToString() {
            return $"RoleAuthorizeAttribute: RoleId = {_roleid}";
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true) {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (userRole == null || int.Parse(userRole) != _roleid) {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}