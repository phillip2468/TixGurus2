using Ssample.ViewModel.Buying_tickets;
using System.Windows.Controls;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for SuccessfulCustomerTicketPurchaseView.xaml
    /// </summary>
    public partial class SuccessfulCustomerTicketPurchaseView : UserControl
    {
        public SuccessfulCustomerTicketPurchaseView()
        {
            InitializeComponent();
            DataContext = new SuccessfulCustomerTicketPurchaseViewModel();
        }
    }
}
