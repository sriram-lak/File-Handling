// Program.cs
using System;

namespace FileHandling
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfigService configService = new ConfigService();
                string directoryPath = configService.GetDirectoryPath ();

                IUserInputService inputService = new UserInputService(directoryPath);
                IFileService fileService = new FileService();

                FileManagerApp app = new FileManagerApp(fileService, inputService);
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
            }
        }
    }
}

// IConfigService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal interface IConfigService
    {
        string GetDirectoryPath();
    }
}

// ConfigService.cs
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal class ConfigService : IConfigService
    {
        public string GetDirectoryPath()
        {
            string path = ConfigurationManager.AppSettings["DirectoryPath"];
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Directory path in App config is invalid or does not exist.");
            }
            return path;
        }
    }
}

// IUserInputService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal interface IUserInputService
    {
        string GetValidatedFileName();
        string GetTextToAppend();
        bool ConfirmAction(string message);
    }
}

// UserInputService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal class UserInputService : IUserInputService
    {
        private readonly string directoryPath;

        public UserInputService(string path)
        {
            directoryPath = path;
        }

        public string GetValidatedFileName()
        {
            while (true)
            {
                Console.Write("Enter file name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("File name cannot be empty.");
                    continue;
                }

                if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    Console.WriteLine("File name contains invalid characters.");
                    continue;
                }

                return Path.Combine(directoryPath, name);
            }
        }

        public string GetTextToAppend()
        {
            Console.Write("Enter text to append: ");
            return Console.ReadLine();
        }

        public bool ConfirmAction(string message)
        {
            Console.Write($"{message} (y/n): ");
            return Console.ReadLine()?.ToLower() == "y";
        }
    }
}

//IFileService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal interface IFileService
    {
        void CreateFile(string fileName);
        void ReadFile(string fileName);
        void AppendToFile(string fileName);
        void DeleteFile(string fileName);
    }
}

// FileService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal class FileService : IFileService
    {
        public void CreateFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("File already exists.");
                return;
            }

            File.Create(filePath).Close();
            Console.WriteLine("File created successfully.");
        }

        public void ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            using StreamReader reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();

            Console.WriteLine(string.IsNullOrEmpty(content) ? "File is empty." : content);
        }

        public void AppendToFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            Console.Write("Enter text to append: ");
            string input = Console.ReadLine();

            using StreamWriter writer = new StreamWriter(filePath, append: true);
            writer.WriteLine(input);
            Console.WriteLine("Text appended.");
        }

        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            File.Delete(filePath);
            Console.WriteLine("File deleted.");
        }
    }
}

// FileManagerApp.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    internal class FileManagerApp
    {
        private readonly IFileService fileService;
        private readonly IUserInputService inputService;

        public FileManagerApp(IFileService fileService, IUserInputService inputService)
        {
            this.fileService = fileService;
            this.inputService = inputService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- File Manager ---");
                Console.WriteLine("1. Create File");
                Console.WriteLine("2. Read File");
                Console.WriteLine("3. Append to File");
                Console.WriteLine("4. Delete File");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                string filePath = inputService.GetValidatedFileName();

                switch (choice)
                {
                    case "1":
                        if (inputService.ConfirmAction("Do you want to create this file?"))
                            fileService.CreateFile(filePath);
                        break;
                    case "2":
                        fileService.ReadFile(filePath);
                        break;
                    case "3":
                        fileService.AppendToFile(filePath);
                        break;
                    case "4":
                        if (inputService.ConfirmAction("Are you sure to delete this file?"))
                            fileService.DeleteFile(filePath);
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
