using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace AnimeFigureStoreAPI.Controllers
{
    public class RestController : Controller
    {
        IAnimeFiguresService animeFiguresService;
        public RestController(
            IAnimeFiguresService animeFiguresService)
        {
            this.animeFiguresService = animeFiguresService;
        }

        [HttpPost]
        public IActionResult AddAnimeFigure([FromQuery(Name = "Name")] string name, [FromQuery(Name = "Url")] string url)
        {
            if (name != null && url != null)
            {
                // добавляем пользователя в список
                var figure = new AnimeFigure(name, url);
                animeFiguresService.AddAnimeFigure(figure);
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }
        }

        // получение всех пользователей
        [HttpGet]
        public IActionResult GetAllAnimeFigures()
        {
            var animeFigures = animeFiguresService.GetAnimeFigures();
            return new JsonResult(animeFigures);
        }

        // получение одного пользователя по id
        [HttpGet]
        public IActionResult GetAnimeFigure([FromQuery(Name = "Id")] string id)
        {
            var GuidId = new Guid(id);
            // получаем пользователя по id
            var animeFigure = animeFiguresService.GetAnimeFigure(GuidId);
            return new JsonResult(animeFigure);
        }

        [HttpDelete]
        public IActionResult DeleteAnimeFigure([FromQuery(Name = "Id")] string id)
        {
            var GuidId = new Guid(id);
            // получаем пользователя по id
            var answer = animeFiguresService.DeleteAnimeFigure(GuidId);
            if (answer)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }
            
        }

        [HttpPut]
        public IActionResult UpdateAnimeFigure([FromQuery(Name = "Id")] string id,
            [FromQuery(Name = "Name")] string name,
            [FromQuery(Name = "Url")] string url
            )
        {

            if (name != null)
            {
                var figure = new AnimeFigure(name, url, id);
                var answer = animeFiguresService.UpdateAnimeFigure(figure);
            }
            return StatusCode(200);
        }
    }
}
