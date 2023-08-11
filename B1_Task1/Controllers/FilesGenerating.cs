using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace B1_Task1.Controllers
{
    public class FilesGenerating
    {

        private string _outputPath;
        private string _editedPath;
        private List<string> _editedList;
        const string latinChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        const string russianChars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public FilesGenerating(string outputPath, string editedPath, List<string> editedList)
        {
            _outputPath = outputPath;
            _editedPath = editedPath;
            _editedList = editedList;
        }

        public void CreateFiles(int filesAmount, int linesAmount, List<string> list)
        {
            for (int i = 0; i < filesAmount; i++)
            {
                string path = @$"file{i}.txt";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    for (int j = 0; j < linesAmount; j++)
                    {
                        RandomString randomString = new RandomString();
                        writer.WriteLine($"{randomString.RandomDate()}||{randomString.RandomLetter(latinChars)}||{randomString.RandomLetter(russianChars)}||{randomString.RandomInt()}||{randomString.RandomFloat()}");
                    }

                    writer.Close();
                    list.Add(path);
                }
            }
        }

        public int MergeAndRemove(List<string> files, string output, string? filter)
        {
            try
            {
                int totalLinesRemoved = 0;

                using (StreamWriter writer = new StreamWriter(output, false))
                {
                    foreach (string inputFile in files)
                    {
                        using (StreamReader reader = new StreamReader(inputFile))
                        {
                            string? line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (string.IsNullOrEmpty(filter))
                                {
                                    writer.WriteLine(line);
                                }
                                else
                                {
                                    if (line.Contains(filter))
                                    {
                                        if (!_editedList.Contains(inputFile))
                                        {
                                            _editedList.Add(inputFile);
                                        }
                                        totalLinesRemoved++;
                                        continue;
                                    }

                                    writer.WriteLine(line);
                                }
                            }

                        }
                    }

                }

                return totalLinesRemoved;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void WriteEdited(List<string> files, string output)
        {

            using (StreamWriter writer = new StreamWriter(output, false))
            {
                foreach (string inputFile in files)
                {
                    using (StreamReader reader = new StreamReader(inputFile))
                    {
                        string? line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

            }

            _editedList.Clear();
        }

    }
}
