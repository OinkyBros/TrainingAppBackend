using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Oinky.TrainingAppAPI.Controllers.API
{
    [Route("api/v{version:apiVersion}/icon")]
    [ApiController]
    [ApiVersion("1.0")]
    public class IconController : ControllerBase
    {
        public IconController(IconService iconService)
        {
            m_iconService = iconService;
        }

        /// <summary>
        /// Get a champion icon
        /// </summary>
        /// <param name="championName">The name of the requested champion</param>
        /// <returns>The requested champion icon</returns>
        [HttpGet]
        [Route("champion/{championName}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No icon found for the given champion name")]
        [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, Description = "The icon service is not available")]
        public IActionResult GetChampionIcon(string championName)
        {
            if (!m_iconService.IsValid)
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            FileInfo icon = m_iconService.GetChampionIcon(championName);
            if (icon == null)
                return NotFound();
            return File(System.IO.File.OpenRead(icon.FullName), "image/png");
        }

        /// <summary>
        /// Get a profile icon
        /// </summary>
        /// <param name="profileID">Id of the profile icon</param>
        /// <returns>The requested profile icon</returns>
        [HttpGet]
        [Route("profile/{profileID}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No icon found for the given profile id was not found")]
        [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, Description = "The icon service is not available")]
        public IActionResult GetProfileIcon(int profileID)
        {
            if (!m_iconService.IsValid)
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            FileInfo icon = m_iconService.GetProfileIcon(profileID);
            if (icon == null)
                return NotFound();
            return File(System.IO.File.OpenRead(icon.FullName), "image/png");
        }

        private IconService m_iconService;
    }
}