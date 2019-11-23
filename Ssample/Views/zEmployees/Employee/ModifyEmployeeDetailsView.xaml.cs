using Ssample.Model;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.Views
{
    /// <summary>
    /// Interaction logic for ModifyEmployeeDetailsView.xaml
    /// </summary>
    public partial class ModifyEmployeeDetailsView : UserControl
    {
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();

        public ModifyEmployeeDetailsView()
        {
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;
        }

        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            Employee_Details newRecord = args.RowData as Employee_Details;
            Employee_Details order = context.Employee_Details.First(i => i.Employee_ID == newRecord.Employee_ID);

            order.Employee_ID = newRecord.Employee_ID;
            order.Email = newRecord.Email;
            order.Password = newRecord.Password;
            order.First_Name = newRecord.First_Name;
            order.Last_Name = newRecord.Last_Name;
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Employee_Details;
            var id = item.Employee_ID;
            var email = item.Email;
            var password = item.Password;
            var firstName = item.First_Name;
            var lastName = item.Last_Name;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }
    }
}
