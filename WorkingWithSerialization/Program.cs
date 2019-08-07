using System;                       // DateTime
using System.Collections.Generic;   // List<T>, HashSet<T>
using System.Xml.Serialization;     // XmlSerializer
using System.IO;                    // FileStream
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft.Json;

namespace WorkingWithSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание графа объектов
            var people = new List<Person>
            {
                new Person(3000M) {FirstName = "Alice",     LastName = "Smith",     DateOfBirth = new DateTime(1974, 3, 14)},
                new Person(4000M) {FirstName = "Bob",       LastName = "Jones",     DateOfBirth = new DateTime(1969, 11, 23)},
                new Person(20000M) {FirstName = "Charlie",  LastName = "Rose",      DateOfBirth = new DateTime(1964, 5, 4),
                    Children = new HashSet<Person>
                    {
                        new Person(0M) {FirstName = "Sally", LastName = "Rose",     DateOfBirth = new DateTime(1990, 7, 12)}
                    }
                }
            };

            // Создание файла для записи
            string path = Combine(CurrentDirectory, "people.xml");

            FileStream stream = File.Create(path);

            // Создание объекта, который будет отформатирован в виде
            // списка людей в формате XML
            var xs = new XmlSerializer(typeof(List<Person>));

            // Сериализация графа объектов в поток
            xs.Serialize(stream, people);

            // Необходимо закрыть поток, чтобы разблокировать файл
            stream.Close();

            WriteLine($"Written {new FileInfo(path).Length} bytes of XML to {path}");
            WriteLine();

            // Отображение сериализованного графа объектов.
            WriteLine(File.ReadAllText(path));


            FileStream xmlLoad = File.Open(path, FileMode.Open);
            // Десериализация и привидение графа объектов к списку людей.
            var loadedPeople = (List<Person>)xs.Deserialize(xmlLoad);

            foreach (var item in loadedPeople)
            {
                WriteLine($"{item.LastName} has {item.Children.Count} children");
            }
            xmlLoad.Close();


            WriteLine(new string('#', 100));

            // Создание файла для записи
            string jsonPath = Combine(CurrentDirectory, "people.json");

            StreamWriter jsonStream = File.CreateText(jsonPath);

            // Создание объекта для хранения в JSON
            var jss = new JsonSerializer();

            // Сериализация графа объектов в строку
            jss.Serialize(jsonStream, people);
            jsonStream.Close(); // Разблокировать файл

            WriteLine();
            WriteLine($"Written {new FileInfo(jsonPath).Length} bytes of JSON to {jsonPath}");

            // Отображение сериализованного графа объектов
            WriteLine(File.ReadAllText(jsonPath));
        }
    }
}
