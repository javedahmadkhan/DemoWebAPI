using Demo.Common.Contstants;
using Demo.Services.Azure;
using System.IO;

namespace AzureFileUpload
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            // Set Ids of your Azure account
            BlobOperations.applicationId = "";
            BlobOperations.clientSecret = "";
            BlobOperations.tenantId = "";

            //Demo Upload File
            string srcPathToUpload = string.Format(@"File path");
            UploadFile(srcPathToUpload);

            //Demo Download File
            string azurePathInBlob = Constants.azurePathInBlob;
            string destinationPath = string.Format(@"File path");
            DownloadFile(destinationPath, azurePathInBlob);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcPath"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <param name="srcPath"></param>
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
