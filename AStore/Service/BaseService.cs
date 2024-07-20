
using AStore_Library;
using AStore_Web.Models;
using AStore_Web.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace AStore_Web.Service
{
	public class BaseService:IBaseService
	{
		public APIResponse reponseModel { get; set; }
		public IHttpClientFactory httpClient { get; set; }

		public BaseService(IHttpClientFactory httpClient)
		{
			this.reponseModel = new APIResponse();
			this.httpClient = httpClient;
		}

		public async Task<T> SendAsync<T>(APIRequest apiRequest)
		{
			try
			{
				var client = httpClient.CreateClient("AStoreAPI");
				HttpRequestMessage message = new HttpRequestMessage();
				message.Headers.Add("Accept", "application/json");
				message.RequestUri = new Uri(apiRequest.Url);
				if (apiRequest.Data != null)
				{
					message.Content = new StringContent(
						JsonConvert.SerializeObject(apiRequest.Data),
						Encoding.UTF8, "application/json");
				}
				switch (apiRequest.apiType)
				{
					case SD.APIType.POST:
						message.Method = HttpMethod.Post;
						break;
					case SD.APIType.PUT:
						message.Method = HttpMethod.Put;
						break;
					case SD.APIType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;

				}
				HttpResponseMessage apiResponse = null;

				if (!string.IsNullOrEmpty(apiRequest.Token))
				{
					client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
				}
				apiResponse = await client.SendAsync(message);

				var apiContent = await apiResponse.Content.ReadAsStringAsync();
				try
				{
					APIResponse APIResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
					if ( apiResponse.StatusCode == HttpStatusCode.BadRequest ||
						apiResponse.StatusCode == HttpStatusCode.NotFound)
					{
						APIResponse.StatusCode = HttpStatusCode.BadRequest;
						APIResponse.IsSuccess = false;
						var res = JsonConvert.SerializeObject(APIResponse);
						var returnObj = JsonConvert.DeserializeObject<T>(res);
						return returnObj;
					}
				}
				catch (Exception ex)
				{
					var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
					return APIResponse;
				}
				var APIResponse2 = JsonConvert.DeserializeObject<T>(apiContent);
				return APIResponse2;

			}
			catch (Exception ex)
			{
				var dto = new APIResponse
				{
					ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
					IsSuccess = false
				};
				var res = JsonConvert.SerializeObject(dto);
				var APIResponse = JsonConvert.DeserializeObject<T>(res);
				return APIResponse;

			}
		}
	}
}
