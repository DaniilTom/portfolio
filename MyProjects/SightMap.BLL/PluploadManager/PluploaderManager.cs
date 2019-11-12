using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SightMap.BLL.PluploadManager
{
    public class PluploaderManager : IPluploadManager
    {
        public string UploadPath { get; set; }
        public string MainPath { get; set; }

        public const string PartialFileExtension = ".partial";

        public PluploaderManager(string ContentRootPath)
        {
            UploadPath = ContentRootPath + "\\wwwroot\\temp\\";
            MainPath = ContentRootPath + "\\wwwroot\\img\\";
        }

        public void DeleteFiles(string reference)
        {
            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                throw new DirectoryNotFoundException($"Путь {uploadPath} уже удален?");
            }

            string[] filePaths = Directory.GetFiles(uploadPath);
            
            foreach (var from in filePaths)
            {
                string to = Path.GetFileName(from);
                File.Move(from, Path.Combine(MainPath, to));
            }

            Directory.Delete(uploadPath, true);
        }

        public IEnumerable<FileStream> GetFiles(string reference)
        {
            string[] filePaths = GetFilesNames(reference);

            List<FileStream> files = new List<FileStream>();
            foreach (var file in filePaths)
                files.Add(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read));

            return files;
        }

        /// <summary>
        /// Включает полный путь.
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public string[] GetFilesNames(string reference)
        {
            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                throw new DirectoryNotFoundException($"Путь {uploadPath} не существует.");
            }

            string[] filePaths = Directory.GetFiles(uploadPath);

            if (filePaths.Length == 0)
                throw new FileNotFoundException($"В директории {uploadPath} отсутствуют файлы.");

            return filePaths;
        }


        public string[] GetFilesNames(string reference, string[] actualNames)
        {
            List<string> currentFiles = GetFilesNames(reference).ToList();
            for(int i = 0; i < currentFiles.Count; i++)
            {
                if(!actualNames.Contains(Path.GetFileName(currentFiles[i])))
                {
                    File.Delete(currentFiles[i--]);
                }
            }

            return currentFiles.ToArray();
        }

        public void SaveChunk(IFormFile file, string reference, string fileName, int chunk, int chunks)
        {

            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileSavePath = Path.Combine(uploadPath, Path.GetFileName(fileName));

            string partialFileSavePath = string.Concat(fileSavePath, PartialFileExtension);

            using (var fileStream = chunk == 0 ? File.Create(partialFileSavePath) : File.Open(partialFileSavePath, FileMode.Append))
            {
                //file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(fileStream);
            }

            if (chunk == chunks - 1)
            {
                if (File.Exists(fileSavePath))
                {
                    File.Delete(fileSavePath);
                }

                File.Move(partialFileSavePath, fileSavePath);
            }
        }

        public void SaveFile(IFormFile file, string reference)
        {
            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileSavePath = Path.Combine(uploadPath, Path.GetFileName(file.FileName));
            using (var fileStream = File.Create(fileSavePath))
            {
                //file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(fileStream);
            }
        }

        public string GetUploadPath(string reference)
        {
            string uploadPath = UploadPath;

            return Path.Combine(uploadPath, reference);
        }
    }
}
