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
    
    public partial class Add : Window
    {
        public Delegate Updater;
        SqlCommand command=new SqlCommand();
        public Add()
        {
            InitializeComponent();
            command.Connection = MainWindow.con;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            job1 job=new job1();
            MainWindow.con.Open();
            MainWindow.com.CommandText = $"Select Role from Users where Login='{Log.Text}'";
            string role = (string)MainWindow.com.ExecuteScalar();
            ComboBoxItem comboItem=(ComboBoxItem)Rol.SelectedItem;
            string rol = Convert.ToString(comboItem.Content);
            if (role == null)
            {
                
                command.CommandText = $"Insert Users (Login,Password,Role) values('{Log.Text}','{Pass.Text}', '{rol}') select Login,Password,Role from Users";
                command.ExecuteNonQuery();
              MessageBox.Show("Добавление прошло успешно");

                
                Updater.DynamicInvoke();
                this.Close();
            }
            else MessageBox.Show("Такой пользователь уже существует");

            MainWindow.con.Close();
        }
    }
}
