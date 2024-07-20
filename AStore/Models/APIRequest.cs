using static AStore_Library.SD;
namespace AStore_Web.Models
{
	public class APIRequest
	{
		public APIType apiType { get; set; } = APIType.GET;
		public string Url { get; set; }
		public object Data { get; set; }
		public string Token { get; set; }
	}
}
