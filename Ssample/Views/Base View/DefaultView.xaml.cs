using Ssample.ViewModel;
using System.Windows.Controls;

namespace Ssample.Views
{
    /// <summary>
    /// Interaction logic for DefaultView.xaml
    /// </summary>
    public partial class DefaultView : UserControl
    {
        public DefaultView()
        {
            InitializeComponent();
            DataContext = new DefaultViewModel();
        }
    }
}
