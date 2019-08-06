using System;
using static System.Console;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

namespace WorkingWithFileSystems
{
    class Program
    {
        static void WorkWithDirectories()
        {
            // Определение собственного пути к папке
            string userFolder = GetFolderPath(SpecialFolder.Personal);

            var customFolder = new string[] { userFolder, "Mark_Price_9", "WorkingWithFileSystems", "NewFolder" };
            string dir = Combine(customFolder);
            WriteLine($"Working with: {dir}");

            // Проверка существования папки
            WriteLine($"Does it exist? {Exists(dir)}");

            // Создание каталога
            WriteLine("Creating it...");
            CreateDirectory(dir);
            WriteLine($"Does it exist? {Exists(dir)}");
            Write("Confirm the directory exists, and then press ENTER");
            ReadLine();

            // Удаление каталога
            WriteLine("Deleting it...");
            Delete(dir, recursive: true);
            WriteLine($"Does it exist? {Exists(dir)}");
        }

        static void WorkWithFiles()
        {
            // Определение пути к каталогу
            string userFolder = GetFolderPath(SpecialFolder.Personal);

            var customFolder = new string[] { userFolder, "Mark_Price_9", "WorkingWithFileSystems", "NewFolder" };
            string dir = Combine(customFolder);
            CreateDirectory(dir);

            // Определение путей к файлам.
            string textFile = Combine(dir, "Dummy.txt");
            string backupFile = Combine(dir, "Dummy.bak");

            WriteLine($"Working with {textFile}");

            // Проверка существования файла
            WriteLine($"Does it exist? : {File.Exists(textFile)}");

            // Создание текстового файла и запись текстовой строки
            StreamWriter textWriter = File.CreateText(textFile);
            textWriter.WriteLine("Hello, C#!");
            textWriter.Close(); // Закрытие файла и высвобождение ресурсов.

            // Копирование файла с перезаписью (если существует)
            File.Copy(
                sourceFileName: textFile,
                destFileName: backupFile,
                overwrite: true);

            WriteLine($"Does {backupFile} exist? : {File.Exists(backupFile)}");

            Write("Confirm the files exist, and then press ENTER");
            ReadLine();

            // Удаление файла
            File.Delete(textFile);
            WriteLine($"Does it exist? : {File.Exists(textFile)}");

            // Чтение содержимого текстового файла
            WriteLine($"Reading contents of {backupFile}");
            StreamReader textReader = File.OpenText(backupFile);
            WriteLine(textReader.ReadToEnd());
            textReader.Close();

            
            WriteLine($"File Name : {GetFileName(textFile)}");
            WriteLine($"File Name without extension : {GetFileNameWithoutExtension(textFile)}");
            WriteLine($"File extension : {GetExtension(textFile)}");
            WriteLine($"Random file name : {GetRandomFileName()}");
            WriteLine($"Temporary File Name : {GetTempFileName()}");


            var info = new FileInfo(backupFile);
            WriteLine($"{backupFile}");
            WriteLine($" Contains {info.Length} bytes");
            WriteLine($" Last accessed {info.LastAccessTime}");
            WriteLine($" Has readonly set to {info.IsReadOnly}");
        }

        static void WorkingWithDrives()
        {
            WriteLine($"|--------------------------------|------------|---------|--------------------|--------------------|");
            WriteLine($"|            Name                |    Type    | Format  |        Size        |    Free space      |");
            WriteLine($"|--------------------------------|------------|---------|--------------------|--------------------|");

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    WriteLine($"| {drive.Name,-30} | {drive.DriveType,-10} | {drive.DriveFormat,-7} | {drive.TotalSize,18:N0} | {drive.AvailableFreeSpace,18:N0} |");
                }
                else
                {
                    WriteLine($"| {drive.Name,-30} | {drive.DriveType,-10}");
                }
            }
            WriteLine($"|--------------------------------|------------|---------|--------------------|--------------------|");
        }
        static void OutputFileSystemInfo()
        {
            WriteLine($"Path.PathSeparator : {PathSeparator}");
            WriteLine($"Path.DirectorySeparatorChar : {DirectorySeparatorChar}");
            WriteLine($"Directory.GetCurrentDirectory() : {GetCurrentDirectory()}");
            WriteLine($"Environment.CurrentDirectory : {CurrentDirectory}");
            WriteLine($"Environment.SystemDirectory : {SystemDirectory}");
            WriteLine($"Path.GetTempPath() : {GetTempPath()}");
            WriteLine($"GetFolderPath(SpecialFolder) :");
            WriteLine($"\tSystem : {GetFolderPath(SpecialFolder.System)}");
            WriteLine($"\tApplicationData : {GetFolderPath(SpecialFolder.ApplicationData)}");
            WriteLine($"\tMyDocuments : {GetFolderPath(SpecialFolder.MyDocuments)}");
            WriteLine($"\tPersonal : {GetFolderPath(SpecialFolder.Personal)}");
        }


        static void Main(string[] args)
        {
            //OutputFileSystemInfo();
            //WorkingWithDrives();
            //WorkWithDirectories();
            WorkWithFiles();
        }
    }
}
