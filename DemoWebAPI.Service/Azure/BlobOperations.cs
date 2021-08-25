using Demo.Common.Contstants;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services.Azure
{
    /// <summary>
    /// 
    /// </summary>
    public static class BlobOperations
    {
        private static readonly string tenantId = Environment.GetEnvironmentVariable("tenantId");
        private static readonly string applicationId = Environment.GetEnvironmentVariable("applicationId");
        private static readonly string clientSecret = Environment.GetEnvironmentVariable("clientSecret");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="azureOperationHelper"></param>
        public static async Task<bool> BlobUpload<T>(BlobOperationHelper azureOperationHelper, List<T> list)
        {
            var blobContainer = await CreateCloudBlobContainer(azureOperationHelper.StorageAccountName, azureOperationHelper.ContainerName, azureOperationHelper.StorageEndPoint);

            var ifNotExists = await blobContainer.CreateIfNotExistsAsync();
            if (ifNotExists) return false;

            var content = GetListIntoBytes(list);

            var blob = blobContainer.GetBlockBlobReference(azureOperationHelper.BlobName);
            blob.Properties.ContentType = azureOperationHelper.BlobContentType;

            if (!(await blob.ExistsAsync()) && content != null)
            {
                await blob.UploadFromByteArrayAsync(content, 0, content.Length);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="azureOperationHelper"></param>
        public static async Task<bool> DownloadFile(BlobOperationHelper azureOperationHelper)
        {
            var blobContainer = await CreateCloudBlobContainer(azureOperationHelper.StorageAccountName, azureOperationHelper.ContainerName, azureOperationHelper.StorageEndPoint);

            var ifNotExists = await blobContainer.CreateIfNotExistsAsync();
            if (ifNotExists) return false;

            var blob = blobContainer.GetBlockBlobReference(azureOperationHelper.BlobName);

            if (await blob.ExistsAsync())
            {
                await blob.DownloadToFileAsync(azureOperationHelper.DestinationPath, FileMode.OpenOrCreate);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="applicationId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="storageAccountName"></param>
        /// <param name="containerName"></param>
        /// <param name="storageEndPoint"></param>
        /// <returns></returns>
        private static async Task<CloudBlobContainer> CreateCloudBlobContainer(string storageAccountName, string containerName, string storageEndPoint)
        {
            var accessToken = await GetUserOAuthToken();

            var tokenCredential = new TokenCredential(accessToken);
            var storageCredentials = new StorageCredentials(tokenCredential);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, storageAccountName, storageEndPoint, useHttps: true);

            var blobClient = cloudStorageAccount.CreateCloudBlobClient();

            var blobContainer = blobClient.GetContainerReference(containerName);
            return blobContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="applicationId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        private static async Task<string> GetUserOAuthToken()
        {
            var ResourceId = Constants.ResourceId;
            var AuthInstance = Constants.AuthInstance;
            var authority = string.Format(CultureInfo.InvariantCulture, AuthInstance, tenantId);

            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(applicationId, clientSecret);

            var authenticationResult = await authContext.AcquireTokenAsync(ResourceId, clientCred);
            return authenticationResult.AccessToken;
        }

        /// <summary>
        /// Generic Method to Convert List into Bytes
        /// </summary>
        /// <param name="list">List</param>
        /// <returns>Bytes</returns>
        private static byte[] GetListIntoBytes<T>(List<T> list)
        {
            var numProperties = list[0].GetType().GetProperties().Count();
            var sb = new StringBuilder();

            foreach (var line in list)
            {
                var typeProperties = line.GetType().GetProperties();

                for (var i = 1; i <= numProperties; i++)
                {
                    var value = string.Empty;
                    if (typeProperties[i - 1].GetValue(line) != null)
                    {
                        value = typeProperties[i - 1].GetValue(line).ToString();
                    }

                    if (i != numProperties)
                    {
                        sb.Append(value + " ");
                    }
                    else
                    {
                        sb.Append(value + Environment.NewLine);
                    }
                }
            }

            return Encoding.ASCII.GetBytes(sb.ToString());
        }
    }
}
