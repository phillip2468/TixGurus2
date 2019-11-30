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
using Ssample.ViewModel.Buying_tickets;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.Views.Buying_tickets
{
    /// <summary>
    /// Interaction logic for ViewEventsView.xaml
    /// </summary>
    public partial class ViewEventsView : UserControl
    {
        public ViewEventsView()
        {
            InitializeComponent();
            DataGrid.AutoGeneratingColumn += SfDataGrid_OnAutoGeneratingColumn;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewEventsViewModel();
        }

        private void SfDataGrid_OnAutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "ticketID")
                e.Cancel = true;

        }
    }
}
