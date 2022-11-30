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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Threading;
using System.Timers;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Media.Animation;

namespace boat
{
    
    public partial class MainWindow : Window
    {
        DispatcherTimer time = new DispatcherTimer();
        public static string log=null;
        public static string pas=null;
        static public SqlConnection con = new SqlConnection(@"Server=DESKTOP-5IR13K0\SQLEXPRESS;database=Boats;Trusted_Connection=true;");
       static public SqlCommand com =new SqlCommand();
        int n = 3;
        int timec = 15;
        public MainWindow()
        {
            InitializeComponent();
            com.Connection = con;
            
        }
        public bool Per(string log,string pas)
        {
            Log.Text = pas; Pass.Text = pas;
            SqlCommand com5 = new SqlCommand();
            com5.Connection = con;
            com5.CommandText = $"update Users set LastActivity='{DateTime.Now}' where Login='{Log.Text}'";

            string role = null;
            n--;
            if (n == 0)
            {
                logIn.IsEnabled = false;
                time.Tick += new EventHandler(time_Tick);
                time.Interval = TimeSpan.FromSeconds(timec);
                time.Start();
                timec += 20;
            }
            con.Open();

            com.CommandText = $"Select Role from Users where Login='{Log.Text}' and Password='{Pass.Text}'";
            role = (string)com.ExecuteScalar();
            if (role == null)
            {
                MessageBox.Show("Неправильный логин или пароль");
                con.Close();
                return false;
                
            }
            else if (!string.IsNullOrWhiteSpace(Log.Text) || !string.IsNullOrWhiteSpace(Pass.Text) || !string.IsNullOrEmpty(Log.Text) || !string.IsNullOrEmpty(Pass.Text))
            {

                SqlCommand com4 = new SqlCommand($"select LastActivity from Users where Login='{Log.Text}' and Password='{Pass.Text}'", con);
                DateTime now = DateTime.Now;
                DateTime last = (DateTime)com4.ExecuteScalar();
                int razn = now.Month - last.Month;
                int razny = now.Year - last.Year;
                if (razn >= 1 || razny >= 1)
                {
                    MessageBox.Show("Данный аккаунт заблокирован за неактивность");
                    return false;
                }
                else
                {
                    SqlCommand com3 = new SqlCommand($"select LastPassChange from Users where Login='{Log.Text}' and Password='{Pass.Text}'", con);

                    DateTime then = (DateTime)com3.ExecuteScalar();

                    TimeSpan ts = now.Subtract(then);

                    double diff = ts.TotalDays;

                    if (diff >= 14)
                    {
                        MessageBox.Show("Смените пароль");
                        ChangePass CPass = new ChangePass();
                        CPass.Tex1.Text = Log.Text;
                        CPass.Show();
                        this.Close();
                    }
                    else if (role == "admin")
                    {
                        Admin adm = new Admin();
                        
                        com5.ExecuteNonQuery();
                        con.Close();
                        adm.Show();
                        this.Close();
                        return false;
                    }
                    else if (role == "user")
                    {
                        User usr = new User();
                        com5.ExecuteNonQuery();
                        con.Close();
                        usr.Show();
                        this.Close();
                        return false;
                    }
                }
            }
            con.Close();
            return !string.IsNullOrEmpty(log) && !string.IsNullOrWhiteSpace(pas);
        }
        private void logIn_Click(object sender, RoutedEventArgs e)
        {
          
            
                
                Per(Log.Text, Pass.Text);
            
            
        }
        private void time_Tick(object sender, EventArgs e)
        {
            logIn.IsEnabled = true;
            n = 3;
            
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            sign signu=new sign();
            signu.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
           
        }
    }
}
