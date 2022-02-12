using System;
using System.IO;

namespace Kutin_Zakhar_laba1_OS
{
  class Program
  {
    static void Main(string[] args)
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

      string pathDir = @"C:\SomeDir";
      //Создать папку 
      DirectoryInfo dirInfo = new DirectoryInfo(pathDir);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
      }

      string path = @"C:\SomeDir\hta.txt";
      FileInfo fileInf = new FileInfo(path);
      try
      {
        using (FileStream fStream = File.Create(path))
        {
          Console.WriteLine($"Файл, создан по пути: {path}");
          //если файл создан, получить информацию о файле
          if (fileInf.Exists)
          {
            Console.WriteLine("Имя файла: {0}", fileInf.Name);
            Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
            Console.WriteLine("Размер: {0}", fileInf.Length);
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
        using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
        {
          sw.WriteLine(text);
        }
        //Открыть поток и прочитать его обратно
        using (StreamReader sr = File.OpenText(path))
        {
          string s = "";
          while ((s = sr.ReadLine()) != null)
          {
            Console.WriteLine(s);
          }
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
        Console.WriteLine($"Файл по пути {path} удален.");
      }
    }
  }
}
