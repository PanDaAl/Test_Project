using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Test_Project.Database.Tables;

namespace Test_Project.Database.Strategy
{
    class PhoneOperation : IDatabaseOperation
    {
        public void LoadData(TestContext db, DataGrid dataGrid)
        {
            var phones = db.Phones.ToList();
            var manufacturers = db.Manufacturers.Select(m => m.Name).ToList();

            dataGrid.Columns.Clear();
            dataGrid.ItemsSource = phones;

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Модель", Binding = new Binding("Model") });

            DataGridComboBoxColumn comboBoxManufacturer = new DataGridComboBoxColumn()
            {
                Header = "Производитель",
                SelectedItemBinding = new Binding("Manufacturer"),
                ItemsSource = manufacturers
            };
            dataGrid.Columns.Add(comboBoxManufacturer);

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Стоимость", Binding = new Binding("Price") });
        }
        public void AddData(TestContext db, object itemForAdd)
        {
            var phone = (Phone)itemForAdd;
            var phoneExists = db.Phones.Any(p => p.Model == phone.Model);

            if (!phoneExists)
            {
                db.Add(phone);
                db.SaveChanges();
                MessageBox.Show("Телефон добавлен");
            }
            else
            {
                MessageBox.Show("Такая модель телефона уже существует");
            }
        }
        public void ChangeData(TestContext db, object editedItem, string columnName, string newValue)
        {
            var phone = (Phone)editedItem;
            var phoneFromDB = db.Phones.FirstOrDefault(p => p.Model == phone.Model);

            if (phoneFromDB != null)
            {
                switch (columnName)
                {
                    case "Модель":
                        MessageBox.Show("Модель телефона нельзя изменить");
                        return;
                    case "Производитель":
                        phoneFromDB.Manufacturer = newValue;
                        break;
                    case "Стоимость":
                        if (double.TryParse(newValue, out double price))
                        {
                            phoneFromDB.Price = price;
                        }
                        else
                        {
                            MessageBox.Show("Некорректное значение для стоимости");
                            return;
                        }
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
                MessageBox.Show("Телефон изменен");
            }
        }
        public void RemoveData(TestContext db, object itemForRemove)
        {
            var phone = (Phone)itemForRemove;

            db.Remove(phone);
            db.SaveChanges();

            MessageBox.Show("Телефон удален");
        }
    }
}