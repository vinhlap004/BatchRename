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

        class ButtonClickTimes
        {
            public int Replace_Button { get; set; }
            public int NewCase_Button { get; set; }
            public int FullNameNormalize_Button { get; set; }
            public int Move_Button { get; set; }
            public int UniqueName_Button { get; set; }

            public ButtonClickTimes()
            {
                Replace_Button = 0;
                NewCase_Button = 0;
                FullNameNormalize_Button = 0;
                Move_Button = 0;
                UniqueName_Button = 0;
            }
        }

        BindingList<FileName> _fileNames = null;
        BindingList<FolderName> _fileFolder = null;


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

        BindingList<StringOperation> _actions = new BindingList<StringOperation>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileNames = new BindingList<FileName>();
            FileListView.ItemsSource = _fileNames;
            _fileFolder = new BindingList<FolderName>();
            FolderListView.ItemsSource = _fileFolder;
            actionsListBox.ItemsSource = _actions;
        }


        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
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
                string path = folderDlog.SelectedPath + "\\";
                string[] getFileNames = Directory.GetFiles(path);
                foreach (var filename in getFileNames)
                {
                    string newFilename = filename.Remove(0, path.Length);
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

                foreach (var foldername in foldernames)
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


        private void StartBatchFile_Button_Click(object sender, RoutedEventArgs e)
        {
            // vd thêm số 1 vào tên file
            //if (_fileNames != null)
            //{

            //    foreach (var file in _fileNames)
            //    {
            //        string file_name, file_extension;

            //        file_name = Path.GetFileNameWithoutExtension(file.pathFile + file.nameFile); //lấy tên file không bao gồm phần đuôi kiểu file
            //        file_extension = Path.GetExtension(file.pathFile + file.nameFile); //lấy phần đuôi của file vd: .pdf,.png

            //        File.Move(file.pathFile + file.nameFile, file.pathFile + file_name + "1" + file_extension); //đổi tên file đó thành tên file mới đã được mã hóa

            //        file.newFileName = file_name + "1" + file_extension;
            //        file.nameFile = file_name + "1" + file_extension;
            //        FileListView.ItemsSource = _fileNames;
            //    }

            //}
        }

        private void StartBatchFolder_Button_Click(object sender, RoutedEventArgs e)
        {

        }




        ButtonClickTimes btnClickTimes = new ButtonClickTimes();

        private void Move_Button_Click(object sender, RoutedEventArgs e)
        {


            var prototype = new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    Before = true,
                    Start = 0,
                    End = 13
                }

            };
            var action = prototype;
            action.Config();
            _actions.Add(action.Clone());

        }
               

        private void Replace_Button_Click(object sender, RoutedEventArgs e)
        {
            var prototype = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "From",
                    To = "To"
                }
            };
            var action = prototype;
            action.Config();
            _actions.Add(action.Clone());

        }

        private void NewCase_Button_Click(object sender, RoutedEventArgs e)
        {
            

            NewCaseControl newCaseControl = new NewCaseControl();
            newCaseControl.ShowDialog();
        }

        private void FullNameNormalize_Button_Click(object sender, RoutedEventArgs e)
        {
            var prototype = new FullnameNormalizeOperation()
            {
                Args = new FullnameNormalizeArg()
                {

                }
            };
            var action = prototype;
            action.Config();
            _actions.Add(action.Clone());
        }

        private void UniqueName_Button_Click(object sender, RoutedEventArgs e)
        {

            var prototype = new UniqueNameOperation()
            {
                Args = new UniqueNameArgs()
                {

                }
            };
            var action = prototype;
            action.Config();
            _actions.Add(action.Clone());
        }

        private void PreviewFolders_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviewFiles_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviewFiles_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviewFolders_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}