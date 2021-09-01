//
// Copyright:   Copyright (c) 
//
// Description: SFTP Hepler Class
//
// Project: 
//
// Author:  Javed Ahmad Khan
//
// Created Date:  
//

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services
{
    /// <summary>
    /// This class is used to performing SFTP related functionality.
    /// </summary>
    public static class SFTPHelper
    {
        private static readonly string Uri = Environment.GetEnvironmentVariable("SFTPUrl");

        /// <summary>
        /// Send file to SFTP Server
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="container">Azure blob container</param>
        /// <returns>Boolean</returns>
        public static async Task<bool> PostSendFile(string container, string fileName)
        {
            try
            {
                var request = new
                {
                    BlobContainer = container,
                    BlobName = fileName
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(new Uri(Uri), httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var message = await response.Content.ReadAsStringAsync();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Method : PostSendFile : {ex}");
            }
        }
    }
}
