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

namespace registr
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static int _code;
        public Window1(int code)
        {
            InitializeComponent();
            _code = code;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(CodeTB.Text) == _code)
            {
                MessageBox.Show("Почта подтверждена!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный код!");
                CodeTB.Clear();
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
