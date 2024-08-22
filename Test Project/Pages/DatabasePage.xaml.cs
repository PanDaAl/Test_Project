using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Windows;
using System.Windows.Controls;
using Test_Project.Database;
using Test_Project.Database.Strategy;

namespace Test_Project
{
    public partial class DatabasePage : Page
    {
        private IDatabaseOperation _currentOperation;
        private TestContext _db = new TestContext();

        public DatabasePage()
        {
            InitializeComponent();
            GetNameTables();
        }

        private void GetNameTables()
        {
            var tables = _db.Model.GetEntityTypes()
                .Where(x => x is IEntityType)
                .Select(x => x.GetTableName())
                .ToList();

            TablesComboBox.ItemsSource = tables;
            TablesComboBox.SelectedItem = tables[tables.IndexOf("phones")];
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedTable = comboBox.SelectedValue;

            switch (selectedTable)
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

            _currentOperation.LoadData(_db, DataGridView);
        }

        private void AddDataToDB(object sender, RoutedEventArgs e)
        {
            var itemForAdd = DataGridView.SelectedItem;

            if (itemForAdd != null)
            {
                _currentOperation.AddData(_db, itemForAdd);
            }
            else
            {
                MessageBox.Show("Выберите добавляемую строку");
            }
        }
        private void SaveChangesDataFromCellEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            var editedItem = e.Row.Item;
            var editedColumn = e.Column;
            string columnName = editedColumn.Header.ToString();
            string? newValue = null;

            switch (e.EditingElement)
            {
                case ComboBox comboBox:
                    newValue = comboBox.Text;
                    break;
                case TextBox textBox:
                    newValue = textBox.Text;
                    break;
                default:
                    break;
            }

            _currentOperation.ChangeData(_db, editedItem, columnName, newValue);
        }
        private void RemoveDataFromDB(object sender, RoutedEventArgs e)
        {
            var itemForRemove = DataGridView.SelectedItem;

            if (itemForRemove != null)
            {
                _currentOperation.RemoveData(_db, itemForRemove);
            }
            else
            {
                MessageBox.Show("Выберите удаляемую строку");
            }

            _currentOperation.LoadData(_db, DataGridView);
        }
    }
}