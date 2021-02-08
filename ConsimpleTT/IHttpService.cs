using System.Threading.Tasks;

namespace ConsimpleTT
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
    }
}
