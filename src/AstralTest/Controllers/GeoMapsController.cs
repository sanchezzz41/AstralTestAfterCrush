using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AstralTest.Domain.Entities;
using AstralTest.GeoLocation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AstralTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с гео данными
    /// </summary>
    [Authorize(Roles = nameof(RolesOption.User))]
    [Route("GeoMaps")]
    public class GeoMapsController : Controller
    {
        private readonly IGeoService _geoService;
        public GeoMapsController(IGeoService geoService)
        {
            _geoService = geoService;
        }

        /// <summary>
        /// Метод возвращает широту и долготу 
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetLatLon([FromQuery] string adress)
        {
            var result = await _geoService.GetLatLon(adress);
            return new
            {
                Latitude = result.Latitude,
                Longitude = result.Longitude
            };
        }

        /// <summary>
        /// Возвращает кусок карты по указанным координатам
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("Map")]
        public async Task<object> GetImageMap([FromQuery]string latitude, [FromQuery]string longitude)
        {
            var geoModel = new GeoPosition { Latitude = latitude, Longitude = longitude };
            var resultBytes = await _geoService.GetImage(geoModel);
            return File(resultBytes, "image/jpeg","Map.jpg");
        }
    }
}
