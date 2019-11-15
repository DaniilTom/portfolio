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

        /// <summary>
        /// ���������� ���� �� ��������� ����� � ���������� � �������������
        ///  ����� �����. ���������� ����� ��� �����.
        /// </summary>
        /// <param name="reference"></param>
        /// <param name="newPrefix"></param>
        /// <exception cref="DirectoryNotFoundException"/>
        string MoveToMain(string reference, string newPrefix, string fileName);
        string GetUploadPath(string reference);

        /// <summary>
        /// ���������� ���������� ���� � ����� ����������� �������� ��������.
        /// ���� ����� �������, ��������� ���� � ����� ����������� �������.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        string GetMainPath(string prefix);
        string GetRelativeMainPath(string prefix);
        string GetReferenceId();
    }
}
