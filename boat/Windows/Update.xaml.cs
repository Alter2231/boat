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
using static System.Net.Mime.MediaTypeNames;

namespace boat
{
    /// <summary>
    /// Логика взаимодействия для Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Delegate Updater;
        SqlCommand com = new SqlCommand();
        public Update()
        {
            InitializeComponent();
            com.Connection = MainWindow.con;
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            job1 job = new job1();
            ComboBoxItem ComboItem = (ComboBoxItem)Rol.SelectedItem;
            string role = ComboItem.Name;
       
            
            
            MainWindow.con.Open();
            if (!string.IsNullOrEmpty(Tex1.Text) && !string.IsNullOrWhiteSpace(Tex1.Text) && !string.IsNullOrEmpty(Tex2.Text) && !string.IsNullOrWhiteSpace(Tex2.Text))
            {
                try
                {
                    SqlCommand command = new SqlCommand($"Update Users set Login='{Tex1.Text}', Password='{Tex2.Text}', Role='{role}' where Login='{Tex3.Text}'", MainWindow.con);
                    command.ExecuteNonQuery();
                    Updater.DynamicInvoke();
                    MessageBox.Show("Запись успешно Обновлена!", "Поздравляю!");
                    this.Close();
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены", "Ошибка!");
            }
            MainWindow.con.Close();
            
        }
    }
}
