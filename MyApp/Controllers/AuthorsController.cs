using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Views.Authentication;
using System.Data;

namespace MyApp.Controllers
{
    [Authorize(Roles = UserRoles.Author)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
       
    }
}
