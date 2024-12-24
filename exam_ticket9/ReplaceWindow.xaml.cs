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

namespace exam_ticket9
{
    /// <summary>
    /// Логика взаимодействия для ReplaceWindow.xaml
    /// </summary>
    public partial class  ReplaceWindow : Window
    {
        private TextBox editor;

        public ReplaceWindow(TextBox editor)
        {
            this.editor = editor;
            InitializeComponent();
        }

        private void ReplaceText_Click(object sender, RoutedEventArgs e)
        {
            string oldText = OldTextBox.Text;
            string newText = NewTextBox.Text;
            editor.Text = editor.Text.Replace(oldText, newText);
            this.Close();
        }

        
    }
}
