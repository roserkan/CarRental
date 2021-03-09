using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        private static string _currentDirectory = Environment.CurrentDirectory + "\\wwwroot";
        private static string _folderName = "\\images\\";
        public static IResult Add(IFormFile file)
        {
            var fileExists = CheckFileExtension(file);
            if (!fileExists.Success)
            {
                return new ErrorResult(fileExists.Message);
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            var extensionValid = CheckFileExtensionValid(extension);
            if (!extensionValid.Success)
            {
                return new ErrorResult(extensionValid.Message);
            }

            var guidName = Guid.NewGuid().ToString("N");

            CheckDirectoryExists(_currentDirectory + _folderName);

            // --

            CreateImageFile(_currentDirectory + _folderName + guidName + extension, file);
            return new SuccessResult((_folderName + guidName + extension).Replace("\\", "/"));
        }

        public static IResult Delete(string path)
        {
            DeleteOldImageFile((_currentDirectory + path).Replace("/", "\\"));
            return new SuccessResult();
        }
        public static IResult Update(IFormFile file, string imagePath)
        {
            var fileExists = CheckFileExtension(file);
            if (fileExists.Message != null)
            {
                return new ErrorResult(fileExists.Message);
            }

            var type = Path.GetExtension(file.FileName).ToLower();
            var typeValid = CheckFileExtensionValid(type);
            var randomName = Guid.NewGuid().ToString("N");

            if (typeValid.Message != null)
            {
                return new ErrorResult(typeValid.Message);
            }

            DeleteOldImageFile((_currentDirectory + imagePath).Replace("/", "\\"));
            CheckDirectoryExists(_currentDirectory + _folderName);
            CreateImageFile(_currentDirectory + _folderName + randomName + type, file);
            return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
        }

        private static void DeleteOldImageFile(string directory)
        {
            if (File.Exists(directory.Replace("/", "\\")))
            {
                File.Delete(directory.Replace("/", "\\"));
            }
        }

        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream fs = File.Create(directory))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private static void CheckDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static IResult CheckFileExtensionValid(string extension)
        {
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            {
                return new ErrorResult("Hatalı dosya uzantısı");
            }
            return new SuccessResult();
        }

        private static IResult CheckFileExtension(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("File doesn't exists.");
        }
    }
}
