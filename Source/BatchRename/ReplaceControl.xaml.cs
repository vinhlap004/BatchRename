using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceControl.xaml
    /// </summary>
    /// 
    
    public partial class ReplaceControl : Window
    {
        ReplaceArgs myArgs;
        public ReplaceControl(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as ReplaceArgs;
            Replace_From_TextBox.Text = myArgs.From;
            Replace_To_TextBox.Text = myArgs.To;

        }

        private void Replace_AddtoList_Button_Click(object sender, RoutedEventArgs e)
        {
            myArgs.From = Replace_From_TextBox.Text;
            myArgs.To = Replace_To_TextBox.Text;
            if (Name.IsChecked == true)
            {
                myArgs.ReplaceName = true;
            }
            else
            {
                myArgs.ReplaceName = false;
            }
            DialogResult = true;
            Close();
        }
    }
}
