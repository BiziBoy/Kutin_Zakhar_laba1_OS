using System;
using System.IO;

namespace Kutin_Zakhar_laba1_OS
{
  class Program
  {
    /// <summary>
    /// Выводит информацию о дисках в консоль 
    /// </summary>
    static void getDiskInformation() 
    {
      DriveInfo[] drives = DriveInfo.GetDrives();
      //получить информацию о дисках
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

    /// <summary>
    /// Создает директорию по _pathDir, в которой создает текстовый файл по _path.
    /// </summary>
    /// <param name="_pathDir"></param>
    /// <param name="_path"></param>
    static void processATextFile(string _pathDir, string _path ) 
    {
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
          Console.WriteLine($"Файл, создан по пути: {_path}");
          //если файл создан, получить информацию о файле
          if (fileInf.Exists)
          {
            Console.WriteLine("Имя файла: {0}", fileInf.Name);
            Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
            Console.WriteLine("Размер: {0}", fileInf.Length);
            Console.WriteLine();
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
      Console.Write("Введите строку для записи в файл: ");
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
          Console.Write("Информация из файла: ");
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
        Console.WriteLine($"Файл по пути {_path} удален.");
      }
    }

    static void Main(string[] args)
    {
      getDiskInformation();
      string pathDir = @"C:\SomeDir";
      string path = @"C:\SomeDir\hta.txt";
      processATextFile(pathDir, path);
    }
  }
}
