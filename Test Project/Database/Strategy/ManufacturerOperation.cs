using System.Windows.Controls;
using System.Windows.Data;

namespace Test_Project.Database.Strategy
{
    class ManufacturerOperation : IDatabaseOperation
    {
        public void LoadData(DataGrid dataGrid)
        {
            using (TestContext db = new TestContext())
            {
                var manufacturers = db.Manufacturers.ToList();

                dataGrid.Columns.Clear();
                dataGrid.ItemsSource = manufacturers;

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Название", Binding = new Binding("Name") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Страна", Binding = new Binding("Country") });
            }
        }
    }
}