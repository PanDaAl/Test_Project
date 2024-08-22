using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows.Controls;
using Test_Project.Database;
using Test_Project.Database.Strategy;

namespace Test_Project
{
    public partial class DatabasePage : Page
    {
        private IDatabaseOperation? _currentOperation;

        public DatabasePage()
        {
            InitializeComponent();
            GetNameTables();
        }

        private void GetNameTables()
        {
            using (TestContext db = new TestContext())
            {
                var tables = db.Model.GetEntityTypes()
                    .Where(x => x is IEntityType)
                    .Select(x => x.GetTableName())
                    .ToList();

                TablesComboBox.ItemsSource = tables;
                TablesComboBox.SelectedItem = tables[tables.IndexOf("phones")];
            }
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var table = comboBox.SelectedValue;

            switch (table)
            {
                case "phones":
                    _currentOperation = new PhoneOperation();
                    break;
                case "manufacturers":
                    _currentOperation = new ManufacturerOperation();
                    break;
                default:
                    break;
            }

            _currentOperation.LoadData(DataGridView);
        }
    }
}