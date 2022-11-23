using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Website_ShopeeFood.Models;
using Newtonsoft.Json;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Website_ShopeeFood.Controllers
{
    public class AreasController : Controller
    {

        string Baseurl = "https://localhost:5001/";

        [HttpGet]
        public async Task<ActionResult> Areas_Partial()
        {
            List<AreasModel> areas = new List<AreasModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl); //Chuyển URL

                client.DefaultRequestHeaders.Clear();

                //Định dạng format dữ liệu là JSon

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Gửi yêu cầu tìm dịch vụ Web API bằng cách sử dụng HttpClient

                HttpResponseMessage message = await client.GetAsync("api/areas");

                //Kiểm tra xem có thành không ko

                if (message.IsSuccessStatusCode)
                {
                    //Lưu trữ phản hồi sau ghi gọi api
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<List<AreasModel>>(areaMessage);

                    return PartialView("Areas_Partial", areas);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> NameOfArea_Partial(int AreaId)
        {
            AreaId = int.Parse(HttpContext.Session.GetString("AreaID"));

            AreasModel areas = new AreasModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("areas/getNameAreas/"+AreaId+"");

                if (message.IsSuccessStatusCode)
                {
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<AreasModel>(areaMessage);

                    return PartialView("NameOfArea_Partial", areas);
                }
            }
            return NotFound();
        }

        public async Task<AreasModel> GetNameArea(int areaId)
        {
            //AreaId = int.Parse(HttpContext.Session.GetString("AreaID"));

            AreasModel areas = new AreasModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync($"areas/getNameAreas/{areaId}");

                if (message.IsSuccessStatusCode)
                {
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<AreasModel>(areaMessage);

                    return areas;
                }
            }
            return null;
        }
    }
}
