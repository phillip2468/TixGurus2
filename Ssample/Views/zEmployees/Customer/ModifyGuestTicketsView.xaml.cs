﻿using Ssample.Model;
using Ssample.ViewModel.zEmployees.Customer;
using Syncfusion.UI.Xaml.Grid;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;

namespace Ssample.Views.zEmployees.Customer
{
    /// <summary>
    /// Interaction logic for ModifyGuestTicketsView.xaml
    /// </summary>
    public partial class ModifyGuestTicketsView : UserControl
    {
        public ModifyGuestTicketsView()
        {
            InitializeComponent();
            DataContext = new ModifyGuestTicketsViewModel();
            DataGridTickets.RowValidated += dataGrid_RowValidatedForCustomerTickets;
            DataGridTickets.RecordDeleting += dataGrid_RecordDeletingForTickets;
            DataGridTransactions.RowValidated += dataGrid_RowValidatedForCustomerTransactions;
            DataGridTransactions.RecordDeleting += dataGrid_RecordDeletingForCustomerTransactions;
        }

        #region Logic for guest ticket details

        /// <summary>
        /// Edit rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidatedForCustomerTickets(object sender, RowValidatedEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Customer_Ticket_Details newRecord = args.RowData as Customer_Ticket_Details;
            if (sender == null)
            {
                return;
            }
            Guest_Ticket_Details order = context.Guest_Ticket_Details.First(i => i.ticketId == newRecord.ticketId);

            if (newRecord != null)
            {
                order.ticketId = newRecord.ticketId;
                order.email = newRecord.email;
                order.eventAddress = newRecord.eventAddress;
                order.fullName = newRecord.fullName;
                order.eventName = newRecord.eventName;
                order.placeName = newRecord.placeName;
                order.price = newRecord.price;
                order.seatLocation = newRecord.seatLocation;
                order.timeEnd = newRecord.timeEnd;
                order.timeStart = newRecord.timeStart;
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
        public void dataGrid_RecordDeletingForTickets(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Guest_Ticket_Details;
            var ticketId = item.ticketId;
            var email = item.email;
            var eventAddress = item.eventAddress;
            var fullName = item.fullName;
            var eventName = item.eventName;
            var placeName = item.placeName;
            var price = item.price;
            var seatLocation = item.seatLocation;
            var timeEnd = item.timeEnd;
            var timeStart = item.timeStart;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

        #endregion

        #region Logic for deleting and editing rows for guest transactions

        /// <summary>
        /// Edit rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void dataGrid_RowValidatedForCustomerTransactions(object sender, RowValidatedEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Guest_Transaction newRecord = args.RowData as Guest_Transaction;
            if (sender == null)
            {
                return;
            }
            Guest_Transaction order = context.Guest_Transaction.First(i => i.transactionId == newRecord.transactionId);

            if (newRecord != null)
            {
                order.transactionId = newRecord.transactionId;
                order.email = newRecord.email;
                order.eventAddress = newRecord.eventAddress;
                order.fullName = newRecord.fullName;
                order.eventName = newRecord.eventName;
                order.totalPrice = newRecord.totalPrice;
                order.address = newRecord.address;
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
        public void dataGrid_RecordDeletingForCustomerTransactions(object sender, RecordDeletingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            var item = args.Items[0] as Customer_Transaction;
            var transactionId = item.transactionId;
            var email = item.email;
            var eventAddress = item.eventAddress;
            var fullname = item.fullname;
            var eventName = item.eventName;
            var totalPrice = item.totalPrice;
            var address = item.address;

            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
            context.Dispose();
        }

        #endregion
    }


}
