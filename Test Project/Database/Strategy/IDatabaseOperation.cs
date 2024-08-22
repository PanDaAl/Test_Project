using System.Windows.Controls;

namespace Test_Project.Database.Strategy
{
    interface IDatabaseOperation
    {
        void LoadData(TestContext db, DataGrid dataGrid);
        void AddData(TestContext db, object itemForAdd);
        void ChangeData(TestContext db, object editedItem, string columnName, string newValue);
        void RemoveData(TestContext db, object itemForRemove);
    }
}