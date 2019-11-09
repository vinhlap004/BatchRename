using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for ReplaceOption.xaml
    /// </summary>
    public partial class ReplaceOption : UserControl
    {

        string From;
        string To;
        StringArgs args = new ReplaceArgs();
        public ReplaceOption()
        {
            InitializeComponent();
         
        }
        private void Replace_AddtoList_Button_Click(object sender, RoutedEventArgs e)
        {
            From = Replace_From_TextBox.Text;
            To = Replace_To_TextBox.Text;
            
            Content = null;
        }
    }
}
