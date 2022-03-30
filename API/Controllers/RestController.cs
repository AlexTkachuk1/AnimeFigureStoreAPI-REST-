using API.Models;
using AutoMapper;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerApp.BLL.Interfaces;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RestController : Controller
    {
        IAnimeFiguresService animeFiguresService;
        public readonly IMapper mapper;
        public RestController(
            IMapper mapper,
            IAnimeFiguresService animeFiguresService)
        {
            this.mapper = mapper;
            this.animeFiguresService = animeFiguresService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> APIServerAsync(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            var path = request.Path;
            //string expressionForNumber = "^/api/users/([0 - 9]+)$";   // если id представляет число

            // 2e752824-1657-4c7f-844b-6ec2e168e99c
            string expressionForGuid = @"https://localhost:44310/Rest/APIServerAsync?Id=\w{8}-\w{4}-\w{4}-\w{4}-\w{12}$";
            if (path == "https://localhost:44310/Rest/APIServerAsync/" && request.Method == "GET")
            {
                await GetAllAnimeFigures();
            }
            else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "GET")
            {
                // получаем id из адреса url
                string? id = path.Value?.Split("/")[3];
                await GetAnimeFigure(id, response, request);
            }
            else if (path.Value.Contains("https://localhost:44310/Rest/APIServerAsync")&& request.Method == "POST")
            {
                await CreateAnimeFigure(response, request);
            }
            else if (path.Value.Contains("https://localhost:44310/Rest/APIServerAsync") && request.Method == "PUT")
            {
                await UpdateAnimeFigure(response, request);
            }
            else if (Regex.IsMatch(path, expressionForGuid) && request.Method == "DELETE")
            {
                string? id = path.Value?.Split("/")[3];
                await DeleteAnimeFigure(id, response, request);
            }
            return View();
        }

        // получение всех пользователей
        async Task<JsonResult> GetAllAnimeFigures()
        {
            var animeFigures = animeFiguresService.GetAnimeFigures();
            return new JsonResult(animeFigures);
        }

        // получение одного пользователя по id
        async Task<JsonResult> GetAnimeFigure(string? id, HttpResponse response, HttpRequest request)
        {
            var GuidId = new Guid(id);
            // получаем пользователя по id
            var animeFigure = animeFiguresService.GetAnimeFigure(GuidId);
            // если пользователь найден, отправляем его
            if (animeFigure != null)
            {
                return new JsonResult(animeFigure);
            }

            // если не найден, отправляем статусный код и сообщение об ошибке
            else
            {
                response.StatusCode = 404;
                return new JsonResult("Пользователь не найден");
            }
        }

        async Task<JsonResult> DeleteAnimeFigure(string? id, HttpResponse response, HttpRequest request)
        {
            var GuidId = new Guid(id);
            // получаем пользователя по id
            var answer = animeFiguresService.DeleteAnimeFigure(GuidId);
            // если пользователь найден, удаляем его
            if (answer)
            {
                response.StatusCode = 200;
                return new JsonResult("Пользователь удален");
            }
            // если не найден, отправляем статусный код и сообщение об ошибке
            else
            {
                response.StatusCode = 404;
                return new JsonResult("Пользователь не найден");
            }
        }

        async Task<JsonResult> CreateAnimeFigure(HttpResponse response, HttpRequest request)
        {
            try
            {
                // получаем данные пользователя
                var animeFigure = request.HttpContext.Request.Query;
                var animeFigureName = animeFigure["Name"].ToString();
                var animeFigureUrl = animeFigure["Url"].ToString();

                if (animeFigureName != null)
                {
                    // добавляем пользователя в список
                    var figure = new AnimeFigure(animeFigureName, animeFigureUrl);
                    animeFiguresService.AddAnimeFigure(figure);
                    response.StatusCode = 200;
                    return new JsonResult("Лот создан успешно");
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception)
            {
                response.StatusCode = 400;
                return new JsonResult("Некорректные данные");
            }
        }

        async Task<JsonResult> UpdateAnimeFigure(HttpResponse response, HttpRequest request)
        {
            try
            {
                // получаем данные пользователя
                var animeFigure = request.HttpContext.Request.Query;
                var animeFigureName = animeFigure["Name"].ToString();
                var animeFigureUrl = animeFigure["Url"].ToString();

                if (animeFigureName != null)
                {
                    var figure = new AnimeFigure(animeFigureName, animeFigureUrl);
                    var answer = animeFiguresService.UpdateAnimeFigure(figure);
                    // если пользователь найден, изменяем его данные и отправляем обратно клиенту
                    if (answer)
                    {
                        response.StatusCode = 200;
                        return new JsonResult("Данные обновлены");
                    }
                    else
                    {
                        response.StatusCode = 404;
                        return new JsonResult("Пользователь не найден");
                    }
                }
                else
                {
                    throw new Exception("Некорректные данные");
                }
            }
            catch (Exception)
            {
                response.StatusCode = 400;
                return new JsonResult("Некорректные данные");
            }
        }
    }
}