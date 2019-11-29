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
                currentCustomer.customerID = newRecord.customerID;
                currentCustomer.email = newRecord.email;
                currentCustomer.password = newRecord.password;
                currentCustomer.firstName = newRecord.firstName;
                currentCustomer.lastName = newRecord.lastName;
                currentCustomer.address = newRecord.address;
                currentCustomer.phoneNumber = newRecord.phoneNumber;
                currentCustomer.city = newRecord.city;
                currentCustomer.state = newRecord.state;
                currentCustomer.postcode = newRecord.postcode;
                currentCustomer.dateOfBirth = newRecord.dateOfBirth;
                currentCustomer.dateCreated = newRecord.dateCreated;
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
