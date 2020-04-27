using System;
using System.IO;

namespace SmartLogger
{
    internal interface IFileNameCreator
    {
        string CreateUniqueFileName(string filepath);
    }

    internal sealed class FileNameCreator : IFileNameCreator
    {
        public string CreateUniqueFileName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            string fileName = Path.GetFileNameWithoutExtension(filePath);
            int indexOfNum = fileName.IndexOf((letter) => char.IsNumber(letter));

            int number = 0;
            if (indexOfNum < 0)
            {
                number = 1;
            }
            else
            {
                number = int.Parse(fileName.Substring(indexOfNum, fileName.Length - indexOfNum));
            }

            string path = Path.GetDirectoryName(filePath);
            string uniqueFileName = null;
            do
            {
                uniqueFileName = Path.Combine(path, fileName + number + ".txt");
                number++;
            }
            while (File.Exists(uniqueFileName));

            return uniqueFileName;
        }
    }
}
