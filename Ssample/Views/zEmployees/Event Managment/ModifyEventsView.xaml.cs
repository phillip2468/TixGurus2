using System.Data.Entity;
using System.Linq;
using Ssample.ViewModel.zEmployees.Event_Management;
using System.Windows.Controls;
using Ssample.Model;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for ModifyEventsView.xaml
    /// </summary>
    public partial class ModifyEventsView : UserControl
    {
        /// <summary>
        /// Sets the context
        /// </summary>
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();
        public ModifyEventsView()
        {
            DataContext = new ModifyEventsViewModel();
            InitializeComponent();
            this.DataGrid.RowValidated += dataGrid_RowValidated;
            this.DataGrid.RecordDeleting += dataGrid_RecordDeleting;
        }
        /// <summary>
        /// Adds record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidated(object sender, Syncfusion.UI.Xaml.Grid.RowValidatedEventArgs args)
        {
            Event_Details newRecord = args.RowData as Event_Details;
            Event_Details order = context.Event_Details.First(i => i.Event_ID == newRecord.Event_ID);

            order.Event_ID = newRecord.Event_ID;
            order.Event_Address = newRecord.Event_Address;
            order.Event_Description = newRecord.Event_Description;
            order.Capacity = newRecord.Capacity;
            order.Event_Start = newRecord.Event_Start;
            order.Event_End = newRecord.Event_End;
            order.Event_Location = newRecord.Event_Location;
            order.Event_Picture = newRecord.Event_Picture;
            order.Event_Title = newRecord.Event_Title;
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
            var item = args.Items[0] as Event_Details;
            var id = item.Event_ID;
            var address = item.Event_Address;
            var description = item.Event_Description;
            var capacity = item.Capacity;
            var eventStart = item.Event_Start;
            var eventEnd = item.Event_End;
            var eventPic = item.Event_Picture;
            var eventTitle = item.Event_Title;
            
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }
    }
}
