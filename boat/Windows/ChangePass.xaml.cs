using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace boat
{
    /// <summary>
    /// Логика взаимодействия для ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        SqlCommand com = new SqlCommand();
        public ChangePass()
        {
            InitializeComponent();
            com.Connection = MainWindow.con;
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.con.Open();
            if (!string.IsNullOrEmpty(Tex2.Text))
            {
                com.CommandText = $"update Users set Password='{Tex2.Text}',LastPassChange='{DateTime.Now}' where [Login]='{Tex1.Text}'";
                com.ExecuteNonQuery();
                MainWindow mwind = new MainWindow();
                mwind.Show();
                this.Close();
                MainWindow.con.Close();
            }
        }
    }
}
