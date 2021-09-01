//
// Copyright:   Copyright (c) 
//
// Description: HTTP Client Service Interface
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using System.Threading.Tasks;

namespace Demo.Services.HTTPClientFactory.Contract
{
	/// <summary>
	/// This interface is used for HTTP Client Factory functionality.
	/// </summary>
	public interface IHttpClientService
	{
		/// <summary>
		/// Get List Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		Task<string> GetListAsync(string path);

		/// <summary>
		/// Get List Method With Http Request Message
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		Task<string> GetListWithHttpRequestMessageAsync(string path);

		/// <summary>
		/// Get Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		Task<string> GetAsync(string path, string id);

		/// <summary>
		/// Get List Method with XML Header
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		Task<string> GetListwithXMLHeaderAsync(string path);

		/// <summary>
		/// Create Method
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		Task<string> CreateAsync(object body, string path);

		/// <summary>
		/// Create Method With Http Request Message
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <returns>Http Response</returns>
		Task<string> CreateWithHttpRequestMessageAsync(object body, string path);

		/// <summary>
		/// Update Method
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		Task<string> UpdateAsync(object body, string path, string id);

		/// <summary>
		/// Update Method With Http Request Message
		/// </summary>
		/// <param name="body">Request Body</param>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		/// <returns>Http Response</returns>
		Task<string> UpdateWithHttpRequestMessageAsync(object body, string path, string id);

		/// <summary>
		/// Delete Method
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		Task DeleteAsync(string path, string id);

		/// <summary>
		/// Delete Method With Http Request Message
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="id">Id</param>
		Task DeleteWithHttpRequestMessageAsync(string path, string id);
	}
}
