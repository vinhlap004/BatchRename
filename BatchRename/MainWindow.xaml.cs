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
using Path = System.IO.Path;

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
            //vd thêm số 1 vào tên file
            if (_fileNames != null)
            {
                List<FileName> prefilename = new List<FileName>();
                foreach (var file in _fileNames)
                {
                    string file_name, file_extension;
                    FileName fileName = new FileName();
                    file_name = Path.GetFileNameWithoutExtension(file.pathFile + file.nameFile); //lấy tên file không bao gồm phần đuôi kiểu file
                    file_extension = Path.GetExtension(file.pathFile + file.nameFile); //lấy phần đuôi của file vd: .pdf,.png
                    string newname = "";
                    string error = "";
                    foreach (var action in _actions)
                    {
                        newname = action.Operate(file_name, file_extension);

                        int check = 1;
                        bool flag = false;

                        while (!flag)
                        {
                            for (int i = 0; i < _fileNames.Count; i++)
                            {
                                if (_fileNames.IndexOf(file) != i && newname == _fileNames[i].nameFile)
                                {
                                    newname = Path.GetFileNameWithoutExtension(file.pathFile + newname);
                                    newname += $"({check})" + file_extension;
                                    error = $"File name is exists. Change file name to {newname}";
                                    check++;
                                    i = 0;
                                }
                            }
                            flag = true;

                        }

                        file_extension = Path.GetExtension(file.pathFile + newname);
                        file_name = Path.GetFileNameWithoutExtension(file.pathFile + newname);
                    }
                    File.Move(file.pathFile + file.nameFile, file.pathFile + newname); //đổi tên file đó thành tên file mới đã được mã hóa
                    fileName.nameFile = newname;
                    fileName.pathFile = file.pathFile;
                    fileName.errorFile = error;
                    prefilename.Add(fileName);

                }

                _fileNames.Clear();
                foreach (var item in prefilename)
                {
                    _fileNames.Add(item);
                }

                FileListView.ItemsSource = _fileNames;

                System.Windows.Forms.MessageBox.Show("Change Name success!");
            }
        }

        private void StartBatchFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_fileFolder != null)
            {
                List<FolderName> prefoldername = new List<FolderName>();
                foreach (var folder in _fileFolder)
                {
                    var folder_name = folder.nameFolder;

                    var FolderName = new FolderName();
                    string newname = "";
                    string error = "";
                    foreach (var action in _actions)
                    {
                        newname = action.Operate(folder_name, null);

                        int check = 1;
                        bool flag = false;

                        while (!flag)
                        {
                            for (int i = 0; i < _fileFolder.Count; i++)
                            {
                                if (_fileFolder.IndexOf(folder) != i && newname == _fileFolder[i].nameFolder)
                                {
                                    newname += $" ({check})";
                                    error = $"Folder name is exists. Change folder name to {newname}";
                                    check++;
                                    i = 0;
                                }
                            }
                            flag = true;
                        }
                        folder_name = newname;
                    }
                    Guid g = Guid.NewGuid();
                    string guidstring = g.ToString();
                    string temp = folder.pathFolder + guidstring;
                    Directory.Move(folder.pathFolder + folder.nameFolder, temp);
                    Directory.Move(temp, folder.pathFolder + newname);


                    FolderName.nameFolder = newname;
                    FolderName.pathFolder = folder.pathFolder;
                    FolderName.errorFolder = error;
                    prefoldername.Add(FolderName);
                }
                _fileFolder.Clear();
                foreach (var item in prefoldername)
                {
                    _fileFolder.Add(item);
                }

                FolderListView.ItemsSource = _fileFolder;

                System.Windows.Forms.MessageBox.Show("Change Name success!");
            }
        }

        private void PreviewFolders_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_fileFolder != null)
            {
                List<FolderName> prefoldername = new List<FolderName>();
                foreach (var folder in _fileFolder)
                {
                    var folder_name = folder.nameFolder;

                    var FolderName = new FolderName();
                    string newname = "";
                    string error = "";
                    foreach (var action in _actions)
                    {
                        newname = action.Operate(folder_name, null);

                        int check = 1;
                        bool flag = false;

                        while (!flag)
                        {
                            for (int i = 0; i < _fileFolder.Count; i++)
                            {
                                if (_fileFolder.IndexOf(folder) != i && newname == _fileFolder[i].nameFolder)
                                {
                                    newname += $" ({check})";
                                    error = $"Folder name is exists. Change folder name to {newname}";
                                    check++;
                                    i = 0;
                                }
                            }
                            flag = true;
                        }
                        folder_name = newname;
                    }

                    FolderName.nameFolder = folder.nameFolder;
                    FolderName.newFolderName = newname;
                    FolderName.pathFolder = folder.pathFolder;
                    FolderName.errorFolder = error;
                    prefoldername.Add(FolderName);
                }

                FolderListView.ItemsSource = prefoldername;

            }
        }


        private void PreviewFiles_Button_Click(object sender, RoutedEventArgs e)
        {
            //vd thêm số 1 vào tên file
            if (_fileNames != null)
            {
                List<FileName> prefilename = new List<FileName>();
                foreach (var file in _fileNames)
                {
                    string file_name, file_extension;
                    FileName fileName = new FileName();
                    file_name = Path.GetFileNameWithoutExtension(file.pathFile + file.nameFile); //lấy tên file không bao gồm phần đuôi kiểu file
                    file_extension = Path.GetExtension(file.pathFile + file.nameFile); //lấy phần đuôi của file vd: .pdf,.png
                    string newname = "";
                    string error = "";
                    foreach (var action in _actions)
                    {
                        newname = action.Operate(file_name, file_extension);

                        int check = 1;
                        bool flag = false;

                        while (!flag)
                        {
                            for (int i = 0; i < _fileNames.Count; i++)
                            {
                                if (_fileNames.IndexOf(file) != i && newname == _fileNames[i].nameFile)
                                {
                                    newname = Path.GetFileNameWithoutExtension(file.pathFile + newname);
                                    newname += $"({check})" + file_extension;
                                    error = $"File name is exists. Change file name to {newname}";
                                    check++;
                                    i = 0;
                                }
                            }
                            flag = true;

                        }

                        file_extension = Path.GetExtension(file.pathFile + newname);
                        file_name = Path.GetFileNameWithoutExtension(file.pathFile + newname);
                    }
                    fileName.nameFile = file.nameFile;
                    fileName.newFileName = newname;
                    fileName.pathFile = file.pathFile;
                    fileName.errorFile = error;
                    prefilename.Add(fileName);

                }

                FileListView.ItemsSource = prefilename;

            }

        }

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
            if (action.Config())
            {
                _actions.Add(action.Clone());
            }

        }


        private void Replace_Button_Click(object sender, RoutedEventArgs e)
        {
            var prototype = new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = "",
                    To = "",
                    ReplaceName = true
                }
            };
            var action = prototype;
            if (action.Config())
            {
                _actions.Add(action.Clone());
            }
        }

        private void NewCase_Button_Click(object sender, RoutedEventArgs e)
        {
            var prototype = new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    optionNewCase = 1
                }
            };
            var action = prototype;
            if (action.Config())
            {
                _actions.Add(action.Clone());
            }
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
            if (action.Config())
            {
                _actions.Add(action.Clone());
            }
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
            if (action.Config())
            {
                _actions.Add(action.Clone());
            }
        }

        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            var item = actionsListBox.SelectedItem as
                StringOperation;

            item.Config();
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            var index = actionsListBox.SelectedIndex;
            _actions.RemoveAt(index);
        }


        string Replace = "BatchRename.ReplaceArgs";
        string Move = "BatchRename.MoveArgs";
        string Newcase = "BatchRename.NewCaseArgs";
        string Unique = "BatchRename.UniqueNameArgs";
        string Normalize = "BatchRename.FullnameNormalizeArg";
        private void Save_ListAction_Button_Click(object sender, RoutedEventArgs e)
        {
            // save file dialog
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Title = "Save text Files",
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            // get filename
            string filename = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog.FileName;


                var writer = new StreamWriter(filename);
                // Dong dau tien la luot di hien tai

                foreach (var item in _actions)
                {
                    string mySaveArgs = "";
                    string args = item.Args.ToString();

                    //args.CompareTo(Replace) == 1
                    if (args == Replace)
                    {
                        ReplaceArgs saveargs = item.Args as ReplaceArgs;
                        mySaveArgs = Replace + " From:" + saveargs.From + " To:" + saveargs.To + " " + saveargs.ReplaceName.ToString();
                        writer.WriteLine(mySaveArgs);

                    }
                    if (args == Move)
                    {
                        MoveArgs saveargs = item.Args as MoveArgs;
                        mySaveArgs = Move + " " + saveargs.Start + " " + saveargs.End + " " + saveargs.Before.ToString();
                        writer.WriteLine(mySaveArgs);
                    }
                    if (args == Newcase)
                    {
                        NewCaseArgs saveargs = item.Args as NewCaseArgs;
                        mySaveArgs = Newcase + " " + saveargs.optionNewCase.ToString();
                        writer.WriteLine(mySaveArgs);
                    }
                    if (args == Unique)
                    {
                        UniqueNameArgs saveargs = item.Args as UniqueNameArgs;
                        mySaveArgs = Unique;
                        writer.WriteLine(mySaveArgs);
                    }
                    if (args == Normalize)
                    {
                        FullnameNormalizeArg saveargs = item.Args as FullnameNormalizeArg;
                        mySaveArgs = Normalize;
                        writer.WriteLine(mySaveArgs);
                    }
                }
                writer.Close();
            }
        }
        private void LoadFromFile_Button_Click(object sender, RoutedEventArgs e)
        {
            string line = "";
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _actions.Clear();
                var reader = new StreamReader(openFile.FileName);
                line = reader.ReadLine();
                while (line != null)
                {
                    var tokens = line.Split(new string[] { " " }, StringSplitOptions.None);
                    {
                        if (tokens[0] == Replace)
                        {
                            string firstString = tokens[1].Substring(0, 5).Trim();
                            string secondString = tokens[1].Substring(5).Trim();
                            tokens[1] = secondString;

                            string firstString1 = tokens[2].Substring(0, 3).Trim();
                            string secondString1 = tokens[2].Substring(3).Trim();
                            tokens[2] = secondString1;

                           
                            bool temp = false;
                            if (tokens[3] == "True")
                            {
                                temp = true;
                            }

                            var prototype = new ReplaceOperation()
                            {
                                Args = new ReplaceArgs
                                {
                                    From = tokens[1],
                                    To = tokens[2],
                                    ReplaceName = temp

                                }
                            };
                            var action = prototype;
                            _actions.Add(action.Clone());
                        }
                        else
                        {
                            if (tokens[0] == Move)
                            {
                                bool temp;
                             
                                if (tokens[3] == "True")
                                {
                                    temp = true;
                                }
                                else
                                {
                                    temp = false;
                                }
                                var prototype = new MoveOperation()
                                {
                                    Args = new MoveArgs
                                    {
                                        Start = int.Parse(tokens[1]),
                                        End = int.Parse(tokens[2]),
                                        Before = temp
                                    }
                                };
                                var action = prototype;
                                _actions.Add(action.Clone());
                            }
                            else
                            {
                                if (tokens[0] == Newcase)
                                {
                                    var prototype = new NewCaseOperation()
                                    {
                                        Args = new NewCaseArgs
                                        {
                                            optionNewCase = int.Parse(tokens[1])
                                        }
                                    };
                                    var action = prototype;
                                    _actions.Add(action.Clone());
                                }
                                else
                                {
                                    if (tokens[0] == Unique)
                                    {
                                        var prototype = new UniqueNameOperation()
                                        {
                                            Args = new UniqueNameArgs
                                            {

                                            }
                                        };
                                        var action = prototype;
                                        _actions.Add(action.Clone());

                                    }
                                    else
                                    {
                                        if (tokens[0] == Normalize)
                                        {
                                            var prototype = new FullnameNormalizeOperation()
                                            {
                                                Args = new FullnameNormalizeArg
                                                {

                                                }
                                            };
                                            var action = prototype;
                                            _actions.Add(action.Clone());
                                        }
                                        else
                                        {
                                            System.Windows.Forms.MessageBox.Show("Khong hop le!!!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    line = reader.ReadLine();

                }

            }
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            _actions.Clear();
            _fileFolder.Clear();
            _fileNames.Clear();
            FileListView.ItemsSource = _fileNames;
            FolderListView.ItemsSource = _fileFolder;
        }
    }

}