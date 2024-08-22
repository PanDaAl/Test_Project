using System.Windows.Controls;
using System.Windows.Data;

namespace Test_Project.Database.Strategy
{
    class PhoneOperation : IDatabaseOperation
    {
        public void LoadData(DataGrid dataGrid)
        {
            using (TestContext db = new TestContext())
            {
                var phones = db.Phones.ToList();

                dataGrid.Columns.Clear();
                dataGrid.ItemsSource = phones;

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Модель", Binding = new Binding("Model") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Производитель", Binding = new Binding("Manufacturer") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Стоимость", Binding = new Binding("Price") });
            }
        }
    }
}