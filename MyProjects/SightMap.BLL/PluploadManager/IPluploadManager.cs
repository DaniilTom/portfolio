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
        void DeleteTempDirectory(string reference);
        void DeleteFromMain(string itemIdPath, string fileName);
        string[] DeleteUnnecessaryUploadedFiles(string reference, string[] actualNames);
        void MoveToMain(string reference, string newPrefix);
        string GetUploadPath(string reference);
        string GetMainPath();
        string GetRelativeMainPath();
        string GetReferenceId();
    }
}
