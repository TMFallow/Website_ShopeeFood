using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Website_ShopeeFood.Models;
using static System.Net.WebRequestMethods;

namespace Website_ShopeeFood.Controllers
{
    public class AreasController : Controller
    {
        string Baseurl = "https://localhost:5001/";
        public async Task<ActionResult> Index()
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

                if(message.IsSuccessStatusCode)
                {

                    //Lưu trữ phản hồi sau ghi gọi api
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    areas = JsonConvert.DeserializeObject<List<AreasModel>>(areaMessage);
                }
            }
                return View(areas);
        }
    }
}
