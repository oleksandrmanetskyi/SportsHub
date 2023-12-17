using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Services.Services.Interfaces
{
    public interface IBlobStorage
    {
        Task UploadBlobAsync(string name, string base64);
        Task<string> DownloadBlobAsync(string name);
        Task DeleteBlobAsync(string name);
    }
}
