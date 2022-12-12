using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentLounge_Backend.Models;
using System.Security.Claims;

namespace StudentLounge_Backend.Controllers
{
    public abstract class SecuredController : ControllerBase
    {
        protected bool UserIdIsValid(string userId)
        {
            return !string.IsNullOrWhiteSpace(userId);
        }

        protected string GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string? userId = "";
            if (identity != null)
            {
                var claims = identity.Claims;
                userId = claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
            }
            return userId ?? "";
        }
    }
}
