using B1_Task1.Controllers;
using B1_Task1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace B1_Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int FILE_AMOUNT = 100;
        const int LINES_AMOUNT = 100000;
        string outputPath = @"output.txt";
        string editedOutputPath = @"edited_output.txt";
        List<string> list = new List<string>();
        List<string> editedList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            FilesGenerating rndString = new FilesGenerating(outputPath, editedOutputPath, editedList);
            rndString.CreateFiles(FILE_AMOUNT, LINES_AMOUNT, list);
            
        }

        

        private void removeLine_Click(object sender, RoutedEventArgs e)
        {
            FilesGenerating rndString = new FilesGenerating(outputPath, editedOutputPath, editedList);
            label2.Content = rndString.MergeAndRemove(list, outputPath, lineToRemove.Text);
            rndString.WriteEdited(editedList, editedOutputPath);
        }


        private void writeToDb_Click(object sender, RoutedEventArgs e)
        {
            FileService fileService = new FileService(editedOutputPath, progressBar, imported, left, lblSum, lblMedian);

            fileService.writeToDb();
        }

        private void storedProcedure_Click(object sender, RoutedEventArgs e)
        {
            FileService fileService = new FileService(editedOutputPath, progressBar, imported, left, lblSum, lblMedian);
            fileService.StoredProcedure();
        }

        private void deleteFromDb_Click(object sender, RoutedEventArgs e)
        {
            FileService fileService = new FileService(editedOutputPath, progressBar, imported, left, lblSum, lblMedian);
            fileService.DeleteFromDb();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.Delete(outputPath);
            File.Delete(editedOutputPath);
            Parallel.ForEach(list, x =>
            {
                File.Delete(x);
            });
        }
    }
}
