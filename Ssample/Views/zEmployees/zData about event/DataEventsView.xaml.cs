using Ssample.ViewModel;
using System.Windows.Controls;

namespace Ssample.Views.Sign_In
{
    /// <summary>
    /// Interaction logic for DataEventsView.xaml
    /// </summary>
    public partial class DataEventsView : UserControl
    {
        public DataEventsView()
        {
            InitializeComponent();
            DataContext = new DataEventsViewModel();
        }
    }
}
