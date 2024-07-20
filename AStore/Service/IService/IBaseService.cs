
using AStore_Web.Models;

namespace AStore_Web.Service.IService
{
	public interface IBaseService
	{
		APIResponse reponseModel { get; set; }
		Task<T> SendAsync<T>(APIRequest apiRequest);
	}
}
