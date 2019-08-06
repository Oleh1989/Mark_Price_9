using System;
using System.IO;
using System.Xml;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using System.IO.Compression;

namespace WorkingWithStreams
{
    class Program
    {
        // Определение массива позывных пилота Viper
        static string[] callsigns = new string[]
            {"Husker", "Starbuck", "Apollo", "Boomer", "Bulldog", "Athena", "Helo", "Racetrack"};

        static void WorkWithText()
        {
            // Определение файла для записи
            string textFile = Combine(CurrentDirectory, "streams.txt");

            // Создание текстового файла и возвращение помощника записи
            StreamWriter text = File.CreateText(textFile);

            // Перечисление строк с записью каждой из них в поток в отдельной строке
            foreach (string item in callsigns)
            {
                text.WriteLine(item);
            }
            text.Close();

            // Вывод содержимого файла в консоль.
            WriteLine($"{textFile} contains {new FileInfo(textFile).Length} bytes.");
            WriteLine(File.ReadAllText(textFile));
        }

        static void WorkWithXml()
        {
            FileStream xmlFileStream = null;
            XmlWriter xml = null;
            try
            {
                // Определение файла для записи
                string xmlFile = Combine(CurrentDirectory, "strems.xml");

                // Создание файловых потоков
                xmlFileStream = File.Create(xmlFile);

                // Оборачивание файлового потока в помощник записи XML
                // и автоматическое добавление отступов для вложенных элементов.
                xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

                // Запись объявления XML
                xml.WriteStartDocument();

                // Запись корневого элемента
                xml.WriteStartElement("callsigns");

                // Перечисление строк и запись каждой в поток
                foreach (string item in callsigns)
                {
                    xml.WriteElementString("callsigns", item);
                }

                // Запись закрывающего корневого элемента
                xml.WriteEndElement();

                // Закрытие помощника и потока
                xml.Close();
                xmlFileStream.Close();

                // Вывод содержимого файла в консоль
                WriteLine($"{xmlFile} contains {new FileInfo(xmlFile).Length} bytes");
                WriteLine(File.ReadAllText(xmlFile));
            }
            catch(Exception ex)
            {
                // Если путь не существует, то исключение будет перехвачено
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            finally
            {
                if (xml != null)
                {
                    xml.Dispose();
                    WriteLine("The XML writer's unmanaged resources have been disposed");
                }
                if (xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed");
                }
            }

            using (FileStream file2 = File.OpenWrite(Path.Combine(CurrentDirectory, "file2.txt")))
            {
                using (StreamWriter writer2 = new StreamWriter(file2))
                {
                    try
                    {
                        writer2.WriteLine("Welcome, .NET Core!");
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"{ex.GetType()} says {ex.Message}");                        
                    }
                }
            }
            
        }

        static void WorkingWithCompression()
        {
            // Сжатие XML-вывода
            string gzipFilePath = Combine(CurrentDirectory, "streams.gzip");

            FileStream gzipFile = File.Create(gzipFilePath);
            using (GZipStream compressor = new GZipStream(gzipFile, CompressionMode.Compress))
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    foreach (string item in callsigns)
                    {
                        xmlGzip.WriteElementString("callsign", item);
                    }
                }
            }

            // Выводит всё содержимое сжатого файла в консоль
            WriteLine($"{gzipFilePath} contains {new FileInfo(gzipFilePath).Length} bytes");
            WriteLine(File.ReadAllText(gzipFilePath));

            // Чтение сжатого файла
            WriteLine("Reading the compressed XML file:");
            gzipFile = File.Open(gzipFilePath, FileMode.Open);
            using (GZipStream decompressor = new GZipStream(gzipFile, CompressionMode.Decompress))
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read())
                    {
                        // Проверка, находимся ли мы в данный момент на узле
                        // элемента с именем callsign
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read();
                            WriteLine($"{reader.Value}");
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            //WorkWithText();
            WorkWithXml();
            WorkingWithCompression();
        }
    }
}
