using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace boat
{
    /// <summary>
    /// Логика взаимодействия для PdfExp.xaml
    /// </summary>
    public partial class PdfExp : Window
    {
        BaseFont helveticaBase;
        SqlCommand command = new SqlCommand();
        public PdfExp()
        {
            InitializeComponent();
            command.Connection = MainWindow.con;
        }

        private void exp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var document = new Document();
                command.CommandText = $"select Login from Users";
                helveticaBase = BaseFont.CreateFont(@"../arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                //MainWindow.con.Open();
                //string asd = (string)command.ExecuteScalar();
                using (var writer = PdfWriter.GetInstance(document, new FileStream("Certificate.pdf", FileMode.Create)))
                {
                    document.Open();

                    writer.DirectContent.BeginText();
                    writer.DirectContent.SetFontAndSize(helveticaBase, 12f);
                    writer.DirectContent.ShowTextAligned(Element.ALIGN_CENTER, "Сертификат выдан " + "asdfa", 200, 766, 0);


                    writer.DirectContent.EndText();

                    MessageBox.Show("Сетификат успешно создан");

                    document.Close();
                    writer.Close();

                    Process.Start("Certificate.pdf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
