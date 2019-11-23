using Ssample.Model;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;

namespace Ssample.Views
{
    /// <summary>
    /// Interaction logic for ModifyCustomerDetailsView.xaml
    /// </summary>
    public partial class ModifyCustomerDetailsView : UserControl
    {

        public ModifyCustomerDetailsView()
        {
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;

        }
        /// <summary>
        /// Edit rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Customer_Details newRecord = args.RowData as Customer_Details;
            if (sender == null)
            {
                return;
            }
            Customer_Details order = context.Customer_Details.First(i => i.Customer_ID == newRecord.Customer_ID);

            if (newRecord != null)
            {
                order.Customer_ID = newRecord.Customer_ID;
                order.Email = newRecord.Email;
                order.Password = newRecord.Password;
                order.First_Name = newRecord.First_Name;
                order.Last_Name = newRecord.Last_Name;
                order.Address = newRecord.Address;
                order.City = newRecord.City;
                order.State = newRecord.State;
                order.Postcode = newRecord.Postcode;
                order.Date_Of_Birth = newRecord.Date_Of_Birth;
                order.Date_Created = newRecord.Date_Created;
            }

            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            context.Dispose();
        }
        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Customer_Details;
            var id = item.Customer_ID;
            var email = item.Email;
            var password = item.Password;
            var firstName = item.First_Name;
            var lastName = item.Last_Name;
            var address = item.Address;
            var city = item.City;
            var state = item.State;
            var postcode = item.Postcode;
            var dateOfBirth = item.Date_Of_Birth;
            var dateCreated = item.Date_Created;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

    }
}
