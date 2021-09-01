//
// Copyright:   Copyright (c) 
//
// Description: Http Client Factory Service Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Services.HTTPClientFactory.Contract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.Services.HTTPClientFactory.Service
{
	/// <summary>
	/// This Class is used for HTTP Client Factory functionality.
	/// </summary>
    public class HttpClientFactoryService : IHttpClientService
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly HttpClient httpClient;

		/// <summary>
		/// Constructor for Http Client Factory Service
		/// </summary>
		/// <param name="httpClientFactory">IHttp Client Factory</param>
		/// <param name="httpClient">Http Client</param>
		public HttpClientFactoryService(IHttpClientFactory httpClientFactory, HttpClient httpClient)
		{
			this.httpClientFactory = httpClientFactory;
			this.httpClient = httpClientFactory.CreateClient();
		}

		/// <summary>
		/// Get List Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		public async Task<string> GetListAsync(string path)
		{
			using (var response = await httpClient.GetAsync(path, HttpCompletionOption.ResponseHeadersRead))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Get List Method With Http Request Message
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		public async Task<string> GetListWithHttpRequestMessageAsync(string path)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, path);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			using (var response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Get Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		public async Task<string> GetAsync(string path, string id)
		{
			var uri = Path.Combine(path, id);

			using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Get List Method with XML Header
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		public async Task<string> GetListwithXMLHeaderAsync(string path)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, path);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

			using (var response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();

				var content = await response.Content.ReadAsStringAsync();

				var doc = XDocument.Parse(content);
				foreach (var element in doc.Descendants())
				{
					element.Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
					element.Name = element.Name.LocalName;
				}

				return doc.ToString();
			}
		}

		/// <summary>
		/// Create Method
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		public async Task<string> CreateAsync(object body, string path)
		{
			var requestBody = JsonSerializer.Serialize(body);

			var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

			using (var response = await httpClient.PostAsync(path, requestContent))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Create Method With Http Request Message
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		public async Task<string> CreateWithHttpRequestMessageAsync(object body, string path)
		{
			var requestBody = JsonSerializer.Serialize(body);

			var request = new HttpRequestMessage(HttpMethod.Post, path);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Content = new StringContent(requestBody, Encoding.UTF8);
			request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			using (var response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Update Method
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		public async Task<string> UpdateAsync(object body, string path, string id)
		{
			var requestBody = JsonSerializer.Serialize(body);

			var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

			var uri = Path.Combine(path, id);

			using (var response = await httpClient.PutAsync(uri, requestContent))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Update Method With Http Request Message
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		public async Task<string> UpdateWithHttpRequestMessageAsync(object body, string path, string id)
		{
			var requestBody = JsonSerializer.Serialize(body);

			var uri = Path.Combine(path, id);

			var request = new HttpRequestMessage(HttpMethod.Put, uri);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			request.Content = new StringContent(requestBody, Encoding.UTF8);
			request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			using (var response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsStringAsync();
			}
		}

		/// <summary>
		/// Delete Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		public async Task DeleteAsync(string path, string id)
		{
			var uri = Path.Combine(path, id);

			using (var response = await httpClient.DeleteAsync(uri))
			{
				response.EnsureSuccessStatusCode();
			}
		}

		/// <summary>
		/// Delete Method With Http Request Message
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		public async Task DeleteWithHttpRequestMessageAsync(string path, string id)
		{
			var uri = Path.Combine(path, id);

			var request = new HttpRequestMessage(HttpMethod.Delete, uri);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			using (var response = await httpClient.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
			}
		}
	}
}
