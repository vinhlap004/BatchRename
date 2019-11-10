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
    /// Interaction logic for MoveControl.xaml
    /// </summary>
    public partial class MoveControl : Window
    {
        MoveArgs myargs;
        public MoveControl(StringArgs args)
        {
            InitializeComponent();
            myargs = args as MoveArgs;
            Move_Start_TextBox.Text = myargs.Start.ToString();
            Move_End_TextBox.Text = myargs.End.ToString();
        }

        private void Add_MoveMenthod_Button_Click(object sender, RoutedEventArgs e)
        {
            myargs.Start = int.Parse(Move_Start_TextBox.Text);
            myargs.Start = int.Parse(Move_End_TextBox.Text);
            if(Move_To_After.IsChecked==true)
            {
                myargs.Before = false;
            }
            else
            {
                myargs.Before = true;
            }
            DialogResult = true;
            Close();
        }
    }
}
