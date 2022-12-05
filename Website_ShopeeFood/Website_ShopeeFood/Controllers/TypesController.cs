using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Website_ShopeeFood.Models;
using System.Threading.Tasks;
using System.Linq;
using Website_ShopeeFood.Services;

namespace Website_ShopeeFood.Controllers
{
    public class TypesController : Controller
    {
        private readonly IAPIServices _APIServices;

        public TypesController(IAPIServices apiServicves)
        {
            this._APIServices = apiServicves;
        }


        List<TypesModel> dsMenu = new List<TypesModel>();

        public async Task<ActionResult> Types_Partial(int ?id)
        {
            List<TypesModel> types = new List<TypesModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_APIServices.getIPAddress()); //Chuyển URL

                client.DefaultRequestHeaders.Clear();

                //Định dạng format dữ liệu là JSon

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Gửi yêu cầu tìm dịch vụ Web API bằng cách sử dụng HttpClient

                HttpResponseMessage message = await client.GetAsync("api/types");

                //Kiểm tra xem có thành không ko

                if (message.IsSuccessStatusCode)
                {
                    //Lưu trữ phản hồi sau ghi gọi api
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    types = JsonConvert.DeserializeObject<List<TypesModel>>(areaMessage);

                    var dsMenu = new List<TypesModel>();

                    for (int i = 0; i <= 6; i++)
                    {
                        dsMenu.Add(types[i]);
                    }

                    return PartialView("Types_Partial", dsMenu);
                }
            }
            return NotFound();
        }

        //Hiển Thị Danh Mục Sản Phẩm
        public async Task<ActionResult> TypesofFoods_Partial()
        {
            List<PricesOfFoods> types = new List<PricesOfFoods>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_APIServices.getIPAddress()); //Chuyển URL

                client.DefaultRequestHeaders.Clear();

                //Định dạng format dữ liệu là JSon

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Gửi yêu cầu tìm dịch vụ Web API bằng cách sử dụng HttpClient

                HttpResponseMessage message = await client.GetAsync("api/types");

                //Kiểm tra xem có thành không ko

                if (message.IsSuccessStatusCode)
                {
                    //Lưu trữ phản hồi sau ghi gọi api
                    var areaMessage = message.Content.ReadAsStringAsync().Result;

                    types = JsonConvert.DeserializeObject<List<PricesOfFoods>>(areaMessage);

                    var dsMenu = new List<PricesOfFoods>();

                    for (int i = 7; i < 21; i++)
                    {
                        dsMenu.Add(types[i]);
                    }

                    return PartialView("TypesofFoods_Partial", dsMenu);
                }
            }
            return NotFound();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>             SUA LAIJ
        [HttpGet]
        public async Task<ActionResult> getListOfTypes_Partial()
        {
            int j = 0;

            List<TypesModel> types = new List<TypesModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_APIServices.getIPAddress());

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("api/types");

                if (message.IsSuccessStatusCode)
                {
                    var typesMessage = message.Content.ReadAsAsync<IEnumerable<TypesModel>>();

                    typesMessage.Wait();

                    var listTypes = typesMessage.Result;

                    foreach (var type in listTypes)
                    {
                        types.Add(type);
                    }

                    for(int i = 7; i < types.Count; i++)
                    {
                        dsMenu.Add(types[i]);
                    }

                    return PartialView("getListOfTypes_Partial", dsMenu);
                }
            }
            return NotFound();
        }

    }
}
