using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lab_Work_1_Melnikov_Timofey_BBBO_05_19
{
  class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }
  class User
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Company { get; set; }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("<================INFO================>");
      //Info();
      Console.WriteLine("<==============File Work=============>");
      //File_Work();
      Console.WriteLine("<===========JSON File Work===========>");
      //JSON_File_Work();
      Console.WriteLine("<============XML File Work===========>");
      //XML_File_Work();
      Console.WriteLine("<==========ARCHIVE File Work=========>");
      Archive_File_Work();
    }

    static void Info()
    {
      DriveInfo[] drives = DriveInfo.GetDrives();

      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"Название: {drive.Name}");
        Console.WriteLine($"Тип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"Объем диска: {drive.TotalSize}");
          Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"Метка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }

    static void File_Work()
    {

      File_Text_Adder();
      File_Text_Reader();
      Console.WriteLine("Вы хотите удалить файл?");
      Console.WriteLine("<Введите 1 для подтверждения, 2 для пропуска операции>");
      
      int choice = Convert.ToInt32(Console.ReadLine());
      if (choice == 1)
      {
        Console.WriteLine("Файл был успешно удален.");
        File_Text_Deleter();
      }
      else
      {
        Console.WriteLine("Файл не был удален.");
      }

    }

    static void JSON_File_Work()
    {
      JSON_File_Text_Work();

      Console.WriteLine("Вы хотите удалить файл?");
      Console.WriteLine("<Введите 1 для подтверждения, 2 для пропуска операции>");
      int choice = Convert.ToInt32(Console.ReadLine());
      if (choice == 1)
      {
        Console.WriteLine("Файл был успешно удален.");
        File.Delete(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Json_Lab.json");
      }
      else
      {
        Console.WriteLine("Файл не был удален.");
      }
    }

    //Тут не работает ввод, вывод, и нужно сделать удаление файла
    static void XML_File_Work()
    {


      XmlDocument xDoc = new XmlDocument();
      //xDoc.Load(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Users.xml");
      XmlElement xRoot = xDoc.DocumentElement;

      XmlElement userElem = xDoc.CreateElement("user");
      XmlAttribute nameAttr = xDoc.CreateAttribute("name");
      XmlElement companyElem = xDoc.CreateElement("company");
      XmlElement ageElem = xDoc.CreateElement("age");
      Console.WriteLine("Введите имя: ");
      XmlText nameText = xDoc.CreateTextNode(Console.ReadLine());
      Console.WriteLine("Введите компанию: ");
      XmlText companyText = xDoc.CreateTextNode(Console.ReadLine());
      Console.WriteLine("Введите возраст: ");
      XmlText ageText = xDoc.CreateTextNode(Console.ReadLine());

      nameAttr.AppendChild(nameText);
      companyElem.AppendChild(companyText);
      ageElem.AppendChild(ageText);
      //userElem.Attributes.Append(nameAttr);
      //userElem.AppendChild(companyElem);
      //userElem.AppendChild(ageElem);
      xRoot.AppendChild(nameAttr);
      xRoot.AppendChild(companyElem);
      xRoot.AppendChild(ageElem);
      xDoc.Save(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Users.xml");


    }

    //Создать архив, добавить файл в архив, разархивировать и прочитать файл, удалить файл и архив
    static void Archive_File_Work()
    {

    }

    static void File_Text_Adder()
    {
      string path = @"D:\Documents\БББО-05-19_Melnikov_Timofey";
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      Console.WriteLine("Введите строку для записи в файл:");
      string text = Console.ReadLine();

      using (FileStream fstream = new FileStream(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Text_Lab", FileMode.OpenOrCreate))
      {
        byte[] array = System.Text.Encoding.Default.GetBytes(text);
        fstream.Write(array, 0, array.Length);
        Console.WriteLine("Текст записан в файл");
      }
    }

    static void File_Text_Reader()
    {
      string path = @"D:\Documents\БББО-05-19_Melnikov_Timofey";
      DirectoryInfo dirInfo = new DirectoryInfo(path);

      using (FileStream fstream = File.OpenRead(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Text_Lab"))
      {
        byte[] array = new byte[fstream.Length];
        fstream.Read(array, 0, array.Length);
        string textFromFile = System.Text.Encoding.Default.GetString(array);
        Console.WriteLine($"Текст из файла: {textFromFile}");
      }
    }

    static void File_Text_Deleter()
    {
      File.Delete(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Text_Lab");
    }

    //Тут косяк с вводом и выводом информации
    static async Task JSON_File_Text_Work()
    {
      var options = new JsonSerializerOptions
      {
        WriteIndented = true
      };
      using (FileStream fs = new FileStream(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Json_Lab.json", FileMode.OpenOrCreate))
      {
        Person tom = new Person { Name = "Tom", Age = 35 };
        string json = JsonSerializer.Serialize<Person>(tom, options);
        byte[] array = System.Text.Encoding.Default.GetBytes(json);

        Console.WriteLine("Данные были записаны в файл.");
      }


      string jsonString = File.ReadAllText(@"D:\Documents\БББО-05-19_Melnikov_Timofey\Json_Lab.json");
      Person restoredPerson = JsonSerializer.Deserialize<Person>(jsonString);
      Console.WriteLine("Данные записаные в файле:");
      Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");

    }

  }
}
