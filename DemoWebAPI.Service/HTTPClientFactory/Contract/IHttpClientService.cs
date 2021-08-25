using System.Threading.Tasks;

namespace Demo.Services.HTTPClientFactory.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> GetListAsync(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> GetListWithHttpRequestMessageAsync(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetAsync(string path, string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> GetListwithXMLHeaderAsync(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> CreateAsync(object body, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> CreateWithHttpRequestMessageAsync(object body, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> UpdateAsync(object body, string path, string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> UpdateWithHttpRequestMessageAsync(object body, string path, string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(string path, string id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteWithHttpRequestMessageAsync(string path, string id);
    }
}
