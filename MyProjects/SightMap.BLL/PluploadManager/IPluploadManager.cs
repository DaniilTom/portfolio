using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace SightMap.BLL.PluploadManager
{ 
    public interface IPluploadManager
    {
        void SaveChunk(IFormFile file, string reference, string name, int chunk, int chunks);
        void SaveFile(IFormFile file, string reference);
        IEnumerable<FileStream> GetFiles(string reference);
        string[] GetFilesNames(string reference);
        string[] GetFilesNames(string reference, string[] actualNames);
        void DeleteFiles(string reference, string newFoldet);
        string GetUploadPath(string reference);
        string GetMainPath();
        string GetRelativeMainPath();
    }
}
