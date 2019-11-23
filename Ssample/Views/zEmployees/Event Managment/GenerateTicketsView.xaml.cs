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
using Ssample.ViewModel.zEmployees.Event_Management;
using Syncfusion.UI.Xaml.Grid;

namespace Ssample.Views.zEmployees.Event_Managment
{
    /// <summary>
    /// Interaction logic for GenerateTicketsView.xaml
    /// </summary>
    public partial class GenerateTicketsView : UserControl
    {
        public GenerateTicketsView()
        {
            InitializeComponent();
            DataContext = new GenerateTicketsViewModel();
            this.DataGrid.AutoGeneratingColumn += datagrid_AutoGeneratingColumn;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }
        public void datagrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "Event_Start" || e.Column.MappingName == "Event_End")
            {
                // Setting default date and time format for Event Start and Event End column
                ((e.Column) as GridDateTimeColumn).Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime;
            }
        }
    }
}
