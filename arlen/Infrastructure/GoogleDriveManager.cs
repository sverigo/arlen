/*Use this ->https://developers.google.com/drive/v3/web/quickstart/dotnet<- link for additional info*/
/* Registere in noreply.arlen@gmail.com account console as arlen-project*/
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using DriveData = Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace arlen.Infrastructure
{
    public class GoogleDriveManager
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "arlen-project";
        private static string client_secret_path = ".credentials/client_secret.json";
        private static string credentials_save_path = ".credentials/credentials.json";
        private DriveService service;
        private Dictionary<string, string> availableFolders = new Dictionary<string, string>()
        {
            { "static_images",  "1TQ_jfkkG_JWwzugM5HyBthiKoDPCXWrX"},
            { "user_files",     "1Fa0swMU8_W7HEilLnfn-JXfB-nLfBLNI" },
            { "user_images",    "1ambNJyBWXzdz-k3OPQ23WrVOmyI54FC4"}
        };

        public GoogleDriveManager(IHostingEnvironment hostingEnvironment)
        {
            UserCredential credential;
            // upload settings from client_secret.json file
            var stream = new FileStream(client_secret_path, FileMode.Open, FileAccess.Read);

            // save credentials
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credentials_save_path, true)).Result;

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

        }

        public string DriveUploadAndGetSrc(IFormFile file, string folder)
        {
            DriveData.File metadata = new DriveData.File()
            {
                Description = file.FileName,
                Name = Guid.NewGuid().ToString(),
                Parents = new List<string>
                {
                    availableFolders[folder]
                }
            };

            byte[] fileData = null;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileData = ms.ToArray();
            }

            Stream stream = new MemoryStream(fileData);
            var request = service.Files.Create(metadata, stream, file.ContentType);
            request.Fields = "id";
            request.Upload();

            string result = "https://drive.google.com/uc?id=" + request.ResponseBody.Id + "&export=download";
            return result;
        }

        public void DriveMoveFileToTrash(string link)
        {
            DriveData.File metadata = new DriveData.File()
            {
                Description = "Trashed by .NET application",
                Trashed = true
            };

            // extract ID from url address
            int from = link.IndexOf("id=") + 3;
            int to = link.IndexOf("&exp");
            string file_id;
            try
            {
                file_id = link.Substring(from, to - from);
                var req = service.Files.Update(metadata, file_id);
                req.Execute();
            }
            catch
            { }

        }
    }
}
