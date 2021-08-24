using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.IdentityModel.Clients.ActiveDirectory; //ADAL client library for getting the access token
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Demo.Services.Azure;


namespace AzureFileUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Ids of your Azure account
            BlobOperations.applicationId = "";
            BlobOperations.clientSecret = "";
            BlobOperations.tenantId = "";

            //Demo Upload File
            string srcPathToUpload = string.Format(@"File path");            
            UploadFile(srcPathToUpload);


            //Demo Download File
            string azurePathInBlob = "dev/files/myfile.txt";
            string destinationPath= string.Format(@"File path");
            DownloadFile(destinationPath, azurePathInBlob);

        }

        public static void UploadFile(string srcPath)
        {            
            BlobOperationHelper azureOperationHelper = new BlobOperationHelper();
            // your Storage Account Name
            azureOperationHelper.storageAccountName = "dbpoc";
            azureOperationHelper.storageEndPoint = "core.windows.net";
            // File path to upload
            azureOperationHelper.srcPath = srcPath; 
            // Your Container Name 
            azureOperationHelper.containerName = "filecontainer"; 
            // Destination Path you can set it file name or if you want to put it in folders do it like below
            azureOperationHelper.blobName = string.Format("dev/files/" + Path.GetFileName(srcPath)); 
            BlobOperations.UploadFile(azureOperationHelper);

        }

        public static void DownloadFile(string destinationPath, string srcPath)
        {           
            BlobOperationHelper azureOperationHelper = new BlobOperationHelper();
            // your Storage Account Name
            azureOperationHelper.storageAccountName = "dbpoc";
            azureOperationHelper.storageEndPoint = "core.windows.net";
            // Destination Path where you want to download file
            azureOperationHelper.destinationPath = destinationPath;
            // Your Container Name 
            azureOperationHelper.containerName = "filecontainer";
            // Blob Path in container where to download File
            azureOperationHelper.blobName = srcPath;
            
            BlobOperations.DownloadFile(azureOperationHelper);

        }       
    }
}
