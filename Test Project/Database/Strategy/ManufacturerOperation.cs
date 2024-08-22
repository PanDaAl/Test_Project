using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Test_Project.Database.Tables;

namespace Test_Project.Database.Strategy
{
    class ManufacturerOperation : IDatabaseOperation
    {
        public void LoadData(TestContext db, DataGrid dataGrid)
        {
            var manufacturers = db.Manufacturers.ToList();

            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = manufacturers;

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("Name") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Страна", Binding = new Binding("Country") });
        }
        public void AddData(TestContext db, object itemForAdd)
        {
            var manufacturer = (Manufacturer)itemForAdd;
            var manufacturerExists = db.Manufacturers.Any(m => m.Name == manufacturer.Name);

            if (!manufacturerExists)
            {
                db.Add(manufacturer);
                db.SaveChanges();
                MessageBox.Show("Производитель добавлен");
            }
            else
            {
                MessageBox.Show("Такой производитель уже существует");
            }
        }
        public void ChangeData(TestContext db, object editedItem, string columnName, string newValue)
        {
            var manufacturer = (Manufacturer)editedItem;
            var manufacturerFromDB = db.Manufacturers.FirstOrDefault(m => m.Name == manufacturer.Name);

            if (manufacturerFromDB != null)
            {
                switch (columnName)
                {
                    case "Название":
                        MessageBox.Show("Название производителя нельзя изменить");
                        return;
                    case "Страна":
                        manufacturerFromDB.Country = newValue;
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
                MessageBox.Show("Производитель изменен");
            }
        }
        public void RemoveData(TestContext db, object itemForRemove)
        {
            var manufacturer = (Manufacturer)itemForRemove;

            db.Remove(manufacturer);
            db.SaveChanges();

            MessageBox.Show("Производитель удален");
        }
    }
}