using AzureFileUpload;
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
using Demo.Common.Contstants;

namespace Demo.Services.Azure
{
    class BlobOperations
    {
        public static string tenantId;
        public static string applicationId;
        public static string clientSecret;

        public static void UploadFile(BlobOperationHelper azureOperationHelper)
        {
            CloudBlobContainer blobContainer = CreateCloudBlobContainer(tenantId, applicationId, clientSecret, azureOperationHelper.storageAccountName, azureOperationHelper.containerName, azureOperationHelper.storageEndPoint);
            blobContainer.CreateIfNotExistsAsync();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(azureOperationHelper.blobName);
            blob.UploadFromFileAsync(azureOperationHelper.srcPath);
        }
        public static void DownloadFile(BlobOperationHelper azureOperationHelper)
        {
            CloudBlobContainer blobContainer = CreateCloudBlobContainer(tenantId, applicationId, clientSecret, azureOperationHelper.storageAccountName, azureOperationHelper.containerName, azureOperationHelper.storageEndPoint);
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(azureOperationHelper.blobName);
            blob.DownloadToFileAsync(azureOperationHelper.destinationPath, FileMode.OpenOrCreate);
        }
        private static CloudBlobContainer CreateCloudBlobContainer(string tenantId, string applicationId, string clientSecret, string storageAccountName, string containerName, string storageEndPoint)
        {
            string accessToken = GetUserOAuthToken(tenantId, applicationId, clientSecret);
            TokenCredential tokenCredential = new TokenCredential(accessToken);
            StorageCredentials storageCredentials = new StorageCredentials(tokenCredential);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, storageAccountName, storageEndPoint, useHttps: true);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
            return blobContainer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="applicationId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        static string GetUserOAuthToken(string tenantId, string applicationId, string clientSecret)
        {
            string ResourceId = Constants.ResourceId;
            string AuthInstance = Constants.AuthInstance;
            string authority = string.Format(CultureInfo.InvariantCulture, AuthInstance, tenantId);
            AuthenticationContext authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(applicationId, clientSecret);
            AuthenticationResult result = authContext.AcquireTokenAsync(ResourceId, clientCred).Result;
            return result.AccessToken;
        }
    }
}
