using CoinExchangeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoinExchangeApp.Services
{
    public class ApiServices
    {
        //https://www.coinexchange.io/api/v1/
        private const string URLBASE = "https://www.coinexchange.io/api/{0}/{1}";
        public async Task<Response> Get<T>(string endPoint, string prefixToken = null,string accessToken = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URLBASE);

                    var url = string.Format("{0}", endPoint);
                    var response = await client.GetAsync(url);


                    if (!response.IsSuccessStatusCode)
                    {
                        
                        return new Response
                        {
                            IsSuccess = false,
                            Message = response.StatusCode.ToString(),
                        };
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var list = JsonConvert.DeserializeObject<T>(result);

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = list,
                    };

                }                
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
