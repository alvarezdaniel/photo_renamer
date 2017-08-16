using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace photo_renamer
{
    class Program
    {
        static void Main(string[] args)
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            Console.WriteLine($"photo-rename tool {version}");

            var currentFolder = Environment.CurrentDirectory;

            foreach (string file in Directory.EnumerateFiles(currentFolder, "*.*", SearchOption.TopDirectoryOnly))
            {
                var extension = Path.GetExtension(file);

                if (extension.ToUpper().Equals(".JPG") || extension.ToUpper().Equals(".MP4"))
                {
                    var fileName = Path.GetFileName(file);
                    var date = File.GetLastWriteTime(file);
                    var folder = $"{date.Year:D4}{date.Month:D2}{date.Day:D2}";

                    var message = $"Moving {fileName} to {folder} folder...";

                    try
                    {
                        var destinationFolder = Path.Combine(currentFolder, folder);
                        if (!Directory.Exists(destinationFolder))
                            Directory.CreateDirectory(destinationFolder);

                        var destinationFile = Path.Combine(destinationFolder, fileName);

                        File.Move(file, destinationFile);

                        Console.WriteLine($"{message}: Done");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{message}: Error {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
