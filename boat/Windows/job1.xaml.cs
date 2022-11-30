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
    /// Логика взаимодействия для job1.xaml
    /// </summary>
    public partial class job1 : Window
    {
       public SqlCommand com=new SqlCommand();
       public SqlDataAdapter adapter=new SqlDataAdapter();
       public DataTable table=new DataTable();
       public DataTable table1 = new DataTable();
       public int usr_ch;
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public job1()
        {
            InitializeComponent();
            com.Connection = MainWindow.con;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.con.Open();
            com.CommandText = "Select Login,Password,Role from Users";
            adapter=new SqlDataAdapter(com);
            adapter.Fill(table);
            DGrid.ItemsSource = table.DefaultView;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Add adD = new Add();
            RefreshListEvent += new RefreshList(RefreshListView);
            adD.Updater = RefreshListEvent;
            adD.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Update upd = new Update();
                int usr_ch = Convert.ToInt32(usr.SelectedItem);
                upd.Tex1.Text = ((DataRowView)DGrid.SelectedItems[usr_ch]).Row["Login"].ToString();
                upd.Tex2.Text = ((DataRowView)DGrid.SelectedItems[usr_ch]).Row["Password"].ToString();
                upd.Tex3.Text = ((DataRowView)DGrid.SelectedItems[usr_ch]).Row["Login"].ToString();
                RefreshListEvent += new RefreshList(RefreshListView); 
                upd.Updater= RefreshListEvent;
                upd.Show();
            }
            catch
            {
                MessageBox.Show("ошибка");
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            SqlCommand com2=new SqlCommand();
            com2.Connection = MainWindow.con;
            com2.CommandText = "Select Login from Users";
            var da = new SqlDataAdapter(com2);
            da.Fill(dt);
            string str = dt.DefaultView[DGrid.SelectedIndex]["Login"].ToString();
           
            if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DGrid.ItemsSource = null;
                DGrid.Items.Clear();
                DataTable table1 = new DataTable();
                com.CommandText = $"Delete Users Where Login='{str}' Select Login,Password,Role From Users ";
                adapter = new SqlDataAdapter(com);

                adapter.Fill(table1);
                DGrid.ItemsSource = table1.DefaultView;
                MainWindow.con.Close();
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Admin adm = new Admin();
            adm.Show();
            this.Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DGrid.ItemsSource = null;
            DGrid.Items.Clear();
            table1.Clear();
            com.CommandText = "Select Login,Password,Role From Users ";
            adapter = new SqlDataAdapter(com);
            adapter.Fill(table1);
            DGrid.ItemsSource = table1.DefaultView;
            MainWindow.con.Close();
            
        }
        private void RefreshListView()
        {
            DGrid.ItemsSource = null;
            DGrid.Items.Clear();
            table1.Clear();
            com.CommandText = "Select Login,Password,Role From Users ";
            adapter = new SqlDataAdapter(com);
            adapter.Fill(table1);
            DGrid.ItemsSource = table1.DefaultView;
            MainWindow.con.Close();
        }
    }
}
