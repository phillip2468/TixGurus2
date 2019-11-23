using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using Ssample.Model;
using Ssample.ViewModel.zEmployees.Event_Management;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for SuccessfulTicketsView.xaml
    /// </summary>
    public partial class SuccessfulTicketsView : UserControl
    {
        /// <summary>
        /// Sets the context
        /// </summary>
        CustomerDatabaseEntities context = new CustomerDatabaseEntities();
        public SuccessfulTicketsView()
        {
            DataContext = new SuccessfulTicketsViewModel();
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
            Ticket_Details newRecord = args.RowData as Ticket_Details;
            Ticket_Details order = context.Ticket_Details.First(i => i.TicketID == newRecord.TicketID);

            order.TicketID = newRecord.TicketID;
            order.EventTitle = newRecord.EventTitle;
            order.EventStart = newRecord.EventStart;
            order.EventEnd = newRecord.EventEnd;
            order.Price = newRecord.Price;
            order.SeatLetter = newRecord.SeatLetter;
            order.SeatNumber = newRecord.SeatNumber;
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
            var item = args.Items[0] as Ticket_Details;
            var id = item.TicketID;
            var title = item.EventTitle;
            var start = item.EventStart;
            var end = item.EventEnd;
            var price = item.Price;
            var seatLetter = item.SeatLetter;
            var seatNumber = item.SeatNumber;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }
    }
}
