using System.Threading.Tasks;

namespace Demo.Services.HTTPClientFactory.Contract
{
    public interface IHttpClientService
    {
        Task<string> GetListAsync(string path);

        Task<string> GetListWithHttpRequestMessageAsync(string path);

        Task<string> GetAsync(string path, string id);

        Task<string> GetListwithXMLHeaderAsync(string path);

        Task<string> CreateAsync(object body, string path);

        Task<string> CreateWithHttpRequestMessageAsync(object body, string path);

        Task<string> UpdateAsync(object body, string path, string id);

        Task<string> UpdateWithHttpRequestMessageAsync(object body, string path, string id);

        Task DeleteAsync(string path, string id);

        Task DeleteWithHttpRequestMessageAsync(string path, string id);
    }
}
