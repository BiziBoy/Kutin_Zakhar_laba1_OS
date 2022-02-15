using System;
using System.IO;
using System.Xml.Linq;

namespace Kutin_Zakhar_laba1_OS
{
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
    /// Создает директорию по _pathDir, в которой создает текстовый файл по _path.
    /// </summary>
    /// <param name="_pathDir"></param>
    /// <param name="_path"></param>
    static void processTextFile(string _pathDir, string _path ) 
    {
      Console.WriteLine("2.Работа с файлами ");
      //Создать папку 
      DirectoryInfo dirInfo = new DirectoryInfo(_pathDir);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }
      FileInfo fileInf = new FileInfo(_path);
      try
      {
        using (FileStream fStream = File.Create(_path))
        {
          Console.WriteLine($"\tФайл, создан по пути: {_path}");
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
        using (StreamWriter sw = new StreamWriter(_path, true, System.Text.Encoding.Default))
        {
          sw.WriteLine(text);
        }
        //Открыть поток и прочитать файл
        using (StreamReader sr = new StreamReader(_path))
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
        Console.WriteLine($"\tФайл по пути {_path} удален.");
        Console.WriteLine();
      }
    }

    /// <summary>
    ///  Создает файл XML с названием fileName
    /// </summary>
    /// <param name="fileName"></param>
    static void processXMLFile(string fileName)
    {
      Console.WriteLine("4.Работа с форматом XML ");
      XDocument xDoc = new XDocument();
      Console.WriteLine($"\tФайл с именем {fileName} создан.");
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
      xDoc.Save(fileName);
      
      //загружаем документ
      XDocument xDocLoad = XDocument.Load(fileName);
      XElement studentsXElement = xDoc.Element("students");
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
      //удалить файл XML
      studentsXElement.RemoveAll();
      xDocLoad.Save(fileName);
      Console.WriteLine($"\tФайл с именем {fileName} очищен.");
    }

    static void Main(string[] args)
    {
      getDiskInformation();
      string pathDirTXT = @"C:\SomeDirTXT";
      string pathTXT = @"C:\SomeDirTXT\hta.txt";
      processTextFile(pathDirTXT, pathTXT);
      string fileName = "students.xml";
      processXMLFile(fileName);

      Console.Read();
    }
  }
}
