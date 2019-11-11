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
    /// Interaction logic for NewCaseControl.xaml
    /// </summary>
    public partial class NewCaseControl : Window
    {
        NewCaseArgs myArgs;
        public NewCaseControl(StringArgs args)
        {
            InitializeComponent();
            myArgs = args as NewCaseArgs;

        }

        private void ContentControl_AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (upper_case.IsChecked==true)
            {
                myArgs.optionNewCase = 1;
            }
            else
            {
                if(lower_case.IsChecked==true)
                {
                    myArgs.optionNewCase = 2;
                }
                else
                {
                    myArgs.optionNewCase = 3;
                }
            }
            DialogResult = true;
            Close();
        }
    }
}
