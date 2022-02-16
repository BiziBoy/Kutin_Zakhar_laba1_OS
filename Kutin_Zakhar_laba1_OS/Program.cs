using System;
using System.IO;
using System.Xml.Linq;
using System.Text.Json;

namespace Kutin_Zakhar_laba1_OS
{
  class Student
  {
    public string name { get; set; }
    public string surName { get; set; }
    public int year { get; set; }
    public string group { get; set; }
  }

  class Program
  {
    /// <summary>
    /// Выводит информацию о дисках в консоль 
    /// </summary>
    static void getDiskInformation() 
    {
      Console.WriteLine("1.Вывести информацию в консоль о логических дисках, именах, метке тома, размере типе файловой системы. ");
      DriveInfo[] drives = DriveInfo.GetDrives();
      //получить информацию о дисках
      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"\tНазвание: {drive.Name}");
        Console.WriteLine($"\tТип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"\tОбъем диска: {drive.TotalSize}");
          Console.WriteLine($"\tСвободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"\tМетка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }

    /// <summary>
    /// Создает текстовый файл по path.
    /// </summary>
    /// <param name="path"></param>
    static void processTextFile(string path ) 
    {
      Console.WriteLine("2.Работа с файлами ");
      FileInfo fileInf = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileInf.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileInf.Name);
            Console.WriteLine("\tВремя создания: {0}", fileInf.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileInf.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.Write("\tВведите строку для записи в файл: ");
      string text = Console.ReadLine();
      try
      {
        //перезаписывает файл, добавляя строку
        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
        {
          sw.WriteLine(text);
        }
        //Открыть поток и прочитать файл
        using (StreamReader sr = new StreamReader(path))
        {
          Console.Write("\tИнформация из файла: ");
          Console.WriteLine(sr.ReadToEnd());
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      //если файл создан, удалить его
      if (fileInf.Exists)
      {
        fileInf.Delete();
        Console.WriteLine($"\tФайл по пути {path} удален.");
        Console.WriteLine();
      }
    }

    /// <summary>
    /// Создает файл JSON по path
    /// </summary>
    /// <param name="path"></param>
    static void processJsonFile(string path)
    {
      Console.WriteLine("3.Работа с форматом JSON ");
      FileInfo fileJSON = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileJSON.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileJSON.Name);
            Console.WriteLine("\tВремя создания: {0}", fileJSON.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileJSON.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      try
      {
      //запись данных
      using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
        {
          Student student = new Student();
          Console.Write("\tВведите имя студента: ");
          student.name = Console.ReadLine();
          Console.Write("\tВведите фамилию студента: ");
          student.surName = Console.ReadLine();
          Console.Write("\tВведите группу студента: ");
          student.group = Console.ReadLine();
          Console.Write("\tВведите год поступления студента: ");
          student.year = int.Parse(Console.ReadLine());
          sw.WriteLine(JsonSerializer.Serialize<Student>(student));
        }
        //чтение данных
        using (StreamReader sr = new StreamReader(path))
        {
          Console.Write("\tИнформация из файла:\n ");
          Student restoredStudent = JsonSerializer.Deserialize<Student>(sr.ReadToEnd());
          Console.WriteLine($"\t\tName: {restoredStudent.name}\n\t\tSurname: {restoredStudent.surName}");
          Console.WriteLine($"\t\tGroup: {restoredStudent.group}\n\t\tYear: {restoredStudent.surName}");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      //если файл создан, удалить его
      if (fileJSON.Exists)
      {
        fileJSON.Delete();
        Console.WriteLine($"\tФайл по пути {path} удален.");
        Console.WriteLine();
      }
    }

    /// <summary>
    ///  Создает файл XML с названием fileName
    /// </summary>
    /// <param name="path"></param>
    static void processXMLFile(string path)
    {
      Console.WriteLine("4.Работа с форматом XML ");
      FileInfo fileXML = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileXML.Exists)
          {
            Console.WriteLine("\tИмя файла: {0}", fileXML.Name);
            Console.WriteLine("\tВремя создания: {0}", fileXML.CreationTime);
            Console.WriteLine("\tРазмер: {0}", fileXML.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      XDocument xDoc = new XDocument();
      //создаем элемент - студент
      XElement student = new XElement("student");
      Console.Write("\tВведите имя студента: ");
      XAttribute nameXAttr = new XAttribute("name", Console.ReadLine());
      Console.Write("\tВведите фамилию студента: ");
      XAttribute surnameXAttr = new XAttribute("surname", Console.ReadLine());
      Console.Write("\tВведите группу студента: ");
      XElement groupXElm = new XElement("group", Console.ReadLine());
      Console.Write("\tВведите год поступления студента: ");
      XElement yearXElm = new XElement("year", Console.ReadLine());
      Console.Write("\tВведите факультет студента: ");
      XElement facultyXElm = new XElement("faculty", Console.ReadLine());
      //добавим выше введенные данные к student
      student.Add(nameXAttr);
      student.Add(surnameXAttr);
      student.Add(groupXElm);
      student.Add(yearXElm);
      student.Add(facultyXElm);
      //создадим корневой элемент
      XElement students = new XElement("students");
      //добавим в корневой элемент введеннго студента
      students.Add(student);
      // добавляем корневой элемент в документ
      xDoc.Add(students);
      //сохраняем документ
      xDoc.Save(path);
      
      //загружаем документ
      XDocument xDocLoad = XDocument.Load(path);
      XElement studentXElement = xDocLoad.Element("students").Element("student");
      //получаем информацию из документа
      nameXAttr = studentXElement.Attribute("name");
      surnameXAttr = studentXElement.Attribute("surname");
      groupXElm = studentXElement.Element("group");
      yearXElm = studentXElement.Element("year");
      facultyXElm = studentXElement.Element("faculty");
      //вывод информации на консоль
      Console.WriteLine("\tИнформация в файле: ");
      Console.WriteLine($"\t\tИмя и фамилия студента: {nameXAttr.Value} {surnameXAttr.Value}");
      Console.WriteLine($"\t\tГруппа студента: {groupXElm.Value}");
      Console.WriteLine($"\t\tГод поступления студента: {yearXElm.Value}");
      Console.WriteLine($"\t\tФакультет студента: {facultyXElm.Value}");
      //если файл создан, удалить его
      if (fileXML.Exists)
      {
        fileXML.Delete();
        Console.WriteLine($"\tФайл по пути {path} удален.");
        Console.WriteLine();
      }
    }

    static void Main(string[] args)
    {
      getDiskInformation();
      string pathTXT = "text.txt";
      processTextFile(pathTXT);
      string pathJSON = "student.json";
      processJsonFile(pathJSON);
      string pathXML = "students.xml";
      processXMLFile(pathXML);

      Console.Read();
    }
  }
}
