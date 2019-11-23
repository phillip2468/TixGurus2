using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ssample.Model;

namespace Ssample.Views.zEmployees
{
    /// <summary>
    /// Interaction logic for AddCustomerDetailsView.xaml
    /// </summary>
    public partial class AddCustomerDetailsView : UserControl
    {
        public AddCustomerDetailsView()
        {
            InitializeComponent();
            this.DataGrid.RowValidating += SfDataGrid1_RowValidating;
        }

        public void SfDataGrid1_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs args)
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            Customer_Details newRecord = args.RowData as Customer_Details;
            Customer_Details currentCustomer = new Customer_Details();

            if (newRecord != null)
            {
                currentCustomer.Customer_ID = newRecord.Customer_ID;
                currentCustomer.Email = newRecord.Email;
                currentCustomer.Password = newRecord.Password;
                currentCustomer.First_Name = newRecord.First_Name;
                currentCustomer.Last_Name = newRecord.Last_Name;
                currentCustomer.Address = newRecord.Address;
                currentCustomer.Phone_Number = newRecord.Phone_Number;
                currentCustomer.City = newRecord.City;
                currentCustomer.State = newRecord.State;
                currentCustomer.Postcode = newRecord.Postcode;
                currentCustomer.Date_Of_Birth = newRecord.Date_Of_Birth;
                currentCustomer.Date_Created = newRecord.Date_Created;
            }


            using (context)
            {
                context.Customer_Details.Add(currentCustomer);
                context.SaveChanges();
                context.Dispose();
            }

        }
    }
}
