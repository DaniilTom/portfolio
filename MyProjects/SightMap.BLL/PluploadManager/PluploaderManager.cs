using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SightMap.BLL.PluploadManager
{
    public class PluploaderManager : IPluploadManager
    {
        public static Hashtable refIdTable = new Hashtable();
        public string UploadPath { get; }
        public string MainPath { get; }
        public string RelativeMainPath { get; }

        public const string PartialFileExtension = ".partial";

        public PluploaderManager(string ContentRootPath)
        { 
            RelativeMainPath = "\\img\\";
            UploadPath = ContentRootPath + "\\wwwroot\\temp\\";
            MainPath = ContentRootPath + "\\wwwroot\\img\\";
        }

        public void DeleteFiles(string reference, string newPrefix)
        {

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

        /// <summary>
        /// Возвращает только имена
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="actualNames"></param>
        /// <returns></returns>
        public string[] DeleteUnnecessaryUploadedFiles(string reference, string[] actualNames)
        {
            string[] currentFiles = GetFilesNames(reference).ToArray();
            for(int i = 0; i < currentFiles.Length; i++)
            {
                if(!actualNames.Contains(Path.GetFileName(currentFiles[i])))
                {
                    File.Delete(currentFiles[i]);
                }
            }

            return currentFiles.Select(file => Path.GetFileName(file)).ToArray();
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
                file.CopyTo(fileStream);
            }
        }

        public string GetUploadPath(string reference)
        {
            string uploadPath = UploadPath;

            return Path.Combine(uploadPath, reference);
        }

        public string GetMainPath(string prefix ="")
        {
            string path = Path.Combine(MainPath, prefix);

            return path;
        }

        public string GetRelativeMainPath(string prefix)
        {
            string path = Path.Combine(RelativeMainPath, prefix);

            return path;
        }

        public string GetReferenceId()
        {
            string newGuid = "";
            do
            {
                newGuid = Guid.NewGuid().ToString();
            } while (refIdTable.ContainsKey(newGuid));

            refIdTable.Add(newGuid, newGuid);

            return newGuid;
        }

        public string MoveToMain(string reference, string newPrefix, string fileName)
        {
            string uploadPath = GetUploadPath(reference);

            if (!Directory.Exists(uploadPath))
                throw new DirectoryNotFoundException($"Путь {uploadPath} не существует.");
            

            string from = Path.Combine(uploadPath, fileName);

            if (!File.Exists(from))
                throw new FileNotFoundException($"Файл {from} не найден.");

            string mainPathWithPrefix = Path.Combine(MainPath, newPrefix);
            
            if (!Directory.Exists(mainPathWithPrefix))
            {
                Directory.CreateDirectory(mainPathWithPrefix);
            }

            string extension = Path.GetExtension(fileName);
            string to;
            do
            {
                to = Path.Combine(mainPathWithPrefix,
                                    Guid.NewGuid().ToString() + extension);
            } while (File.Exists(to));

            File.Move(from, to);

            return Path.GetFileName(to);
        }

        public void DeleteFromMain(string itemIdPath, string fileName)
        {
            string fullPath = Path.Combine(MainPath, itemIdPath, fileName);
            File.Delete(fullPath);
        }

        public void DeleteTempDirectory(string reference)
        {
            string path = GetUploadPath(reference);
            try
            {
                Directory.Delete(path, true);
            }
            catch(PathTooLongException e)
            {
                throw;
            }
            catch(DirectoryNotFoundException)
            {

            }
            refIdTable.Remove(reference);
        }
    }
}
