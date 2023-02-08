using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for OrmWindow.xaml
    /// </summary>
    public partial class OrmWindow : Window
    {
        public ObservableCollection<Entity.Department> Departments { get; set; }
        public ObservableCollection<Entity.Product> Products { get; set; }
        private SqlConnection _connection;
        public OrmWindow()
        {
            InitializeComponent();
            Departments = new();
            Products = new();
            DataContext = this;
            _connection = new(App.ConnectionString);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Open();
                SqlCommand cmd = new() { Connection = _connection };
                cmd.CommandText = "SELECT D.Id, D.Name FROM Departments D";
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Departments.Add(new Entity.Department
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Window will be closed", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

            try
            {

                SqlCommand cmd = new() { Connection = _connection };
                cmd.CommandText = "SELECT P.Id, P.Name FROM Products P";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products.Add(new Entity.Product
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Window will be closed", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // работа с БД через ОРМ упрощена до работы с коллекцией
            // sender - item, который вмещает в себя ссылку на ентити. Department в коллекции Departments
            if(sender is ListViewItem item)
            {
                if(item.Content is Entity.Department department)
                {
                    MessageBox.Show(department.ToString());

                }
            }
        }
    }
}
