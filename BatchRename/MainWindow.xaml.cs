using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        class FileName
        {
            public string nameFile { get; set; }
            public string newFileName { get; set; }
            public string pathFile { get; set; }
            public string errorFile { get; set; }
        }

        class FolderName
        {
            public string nameFolder { get; set; }
            public string newFolderName { get; set; }
            public string pathFolder { get; set; }
            public string errorFolder { get; set; }
        }

        BindingList<FileName> _fileNames = new BindingList<FileName>();
        BindingList<FolderName> _fileFolder = new BindingList<FolderName>();


        /// <summary>
        /// Lấy CSDL
        /// </summary>
        class DAO
        {

        }


        /// <summary>
        /// xử lý nghiệp vụ
        /// </summary>
        class BUS
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileListView.ItemsSource = _fileNames;
            FolderListView.ItemsSource = _fileFolder;
        }


        // Phần xử lý sự kiện
        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            _ = System.Windows.MessageBox.Show("Batch Name Application\nThis app will help you change file name(s) or folder name(s) quickly.\n " +
                "Written by:\n1712472 - Lo Huy Hung\n1712555 - Chau Vinh Lap");
        }

        private void AddFile_Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDlog = new FolderBrowserDialog();

            // check show dialog is success or fail
            if (folderDlog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // save path of you choice
                string path = folderDlog.SelectedPath;
                string[] getFileNames = Directory.GetFiles(path);
                foreach (var filename in getFileNames)
                {
                    string newFilename = filename.Remove(0, path.Length + 2);
                    var Filename = new FileName()
                    {
                        nameFile = newFilename,
                        newFileName = "",
                        pathFile = path,
                        errorFile = ""
                    };
                    _fileNames.Add(Filename);
                }
            }
        }


        public void Add_folder_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderDialog.SelectedPath + "\\";
                var foldernames = Directory.GetDirectories(path);

                foreach(var foldername in foldernames)
                {
                    var newFolderName = foldername.Remove(0, path.Length);
                    var Foldername = new FolderName()
                    {
                        nameFolder = newFolderName,
                        newFolderName = "",
                        pathFolder = path,
                        errorFolder = ""
                    };
                    _fileFolder.Add(Foldername);
                }
                
            }

        }
    }
}