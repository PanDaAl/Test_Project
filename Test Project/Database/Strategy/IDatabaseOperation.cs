using System.Windows.Controls;

namespace Test_Project.Database.Strategy
{
    interface IDatabaseOperation
    {
        void LoadData(DataGrid dataGrid);
    }
}