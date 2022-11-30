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
using System.Data;
using System.Data.SqlClient;
namespace boat
{
    /// <summary>
    /// Логика взаимодействия для sign.xaml
    /// </summary>
    public partial class sign : Window
    {
        SqlCommand sqlCommand = new SqlCommand();
        public sign()
        {
            InitializeComponent();
            sqlCommand.Connection = MainWindow.con;
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mwind=new MainWindow();
            MainWindow.con.Open();
            MainWindow.com.CommandText = $"Select Role from Users where Login='{Log.Text}'";
            string role = (string)MainWindow.com.ExecuteScalar();
            if (role == null)
            {
                sqlCommand.CommandText =$"Insert Users (Login,Password,Role,LastPassChange,LastActivity) values('{Log.Text}','{Pass.Text}', 'user','{DateTime.Now}','{DateTime.Now}')";
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Регистрация прошла успешно");
                mwind.Show();
                this.Close();
            }
            else MessageBox.Show("Такой пользователь уже существует");
            MainWindow.con.Close();
        }
    }
}
