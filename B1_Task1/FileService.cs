using B1_Task1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace B1_Task1
{
    public class FileService
    {
        private string _path;
        private ProgressBar _bar;
        private Label _imported;
        private Label _left;
        private Label _sum;
        private Label _median;

        public FileService(string path, ProgressBar bar, Label imported, Label left, Label sum, Label median)
        {
            _path = path;
            _bar = bar;
            _imported = imported;
            _left = left;
            _sum = sum;
            _median = median;
        }

        public async void AddToDB(IProgress<int> progress)
        {
            var lines = File.ReadAllLines(_path);
            int totalLines = lines.Length;
            int linesAdded = 0;

            using (var context = new AppDbContext())
            {
                foreach (var line in lines)
                {
                    var columns = line.Split(new string[] { "||" }, StringSplitOptions.None);
                    if (columns.Length == 5)
                    {
                        var fromFile = new TextFromFile
                        {
                            Date = DateTime.Parse(columns[0]),
                            LatinString = columns[1],
                            RussianString = columns[2],
                            IntegerNum = int.Parse(columns[3]),
                            FloatNum = float.Parse(columns[4])
                        };

                        context.TextFromFile.Add(fromFile);

                        linesAdded++;
                        Application.Current.Dispatcher.Invoke(() => {
                            progress.Report(linesAdded * 100 / totalLines);
                            _imported.Content = $"{linesAdded}";
                            _left.Content = $"{totalLines - linesAdded}";
                        });
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async void writeToDb()
        {
            var progress = new Progress<int>(value =>
            {
                _bar.Value = value;
            });

            await Task.Run(() =>
            {
                AddToDB(progress);
            });

            MessageBox.Show("Added");
        }

        public void StoredProcedure()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Files;Integrated Security=True;"))
            {
                connection.Open();

                string query = @"
                    declare @IntSum bigint
                    declare @FloatMedian float

                    select @IntSum = SUM(cast(IntegerNum as bigint)) from TextFromFile
                    select @FloatMedian = percentile_cont(0.5) within group (order by FloatNum)
                            over () from TextFromFile
                    select @IntSum as IntSum, @FloatMedian as FloatMedian
                ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                _sum.Content = reader.GetInt64(0);
                                _median.Content = reader.GetDouble(1).ToString("F8");
                            }
                        }
                        catch
                        {
                            _sum.Content = "No Data";
                        }
                    }
                }
            }

        }

        public void DeleteFromDb()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Files;Integrated Security=True;"))
            {
                connection.Open();

                string query = @"
                    delete from TextFromFile
                    DBCC CHECKIDENT(TextFromFile, RESEED, 0)
                ";

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Deleted");
        }
    }
}
