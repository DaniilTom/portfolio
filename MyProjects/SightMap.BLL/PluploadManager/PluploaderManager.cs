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
        /// �������� ������ ����.
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public string[] GetFilesNames(string reference)
        {
            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                throw new DirectoryNotFoundException($"���� {uploadPath} �� ����������.");
            }

            string[] filePaths = Directory.GetFiles(uploadPath);

            if (filePaths.Length == 0)
                throw new FileNotFoundException($"� ���������� {uploadPath} ����������� �����.");

            return filePaths;
        }

        /// <summary>
        /// ���������� ������ �����
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

        public string GetMainPath()
        {
            return MainPath;
        }

        public string GetRelativeMainPath()
        {
            return RelativeMainPath;
        }

        public string GetReferenceId()
        {
            string newGuid = "";
            do
            {
                newGuid = Guid.NewGuid().ToString();
            } while (!refIdTable.ContainsKey(newGuid));

            refIdTable.Add(newGuid, newGuid);

            return newGuid;
        }

        public void MoveToMain(string reference, string newPrefix)
        {
            string uploadPath = GetUploadPath(reference);
            if (!Directory.Exists(uploadPath))
            {
                throw new DirectoryNotFoundException($"���� {uploadPath} ��� ������?");
            }

            string basePath = Path.Combine(MainPath, newPrefix);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            string[] filePaths = Directory.GetFiles(uploadPath);

            foreach (var from in filePaths)
            {
                string fileName = newPrefix + Path.GetFileName(from);
                string to = Path.Combine(basePath, fileName);
                File.Move(from, to);
            }

            Directory.Delete(uploadPath, true);
        }

        public void DeleteFromMain(string itemIdPath, string fileName)
        {
            string fullPath = Path.Combine(MainPath, fileName);
            File.Delete(fullPath);
        }

        public void DeleteTempDirectory(string reference)
        {
            string path = GetUploadPath(reference);
            try
            {
                Directory.Delete(path);
            }
            catch(PathTooLongException e)
            {
                throw;
            }
        }
    }
}
