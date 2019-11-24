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

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for PurchaseTicketsView.xaml
    /// </summary>
    public partial class PurchaseTicketsView : UserControl
    {
        public PurchaseTicketsView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/au/home");
        }
    }
}
