using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEndHagan.Controllers
{
    [ApiController]
    [Authorize]
    [Route ("api/photo")]
    public class PhotoController : ControllerBase
    {
    }
}
