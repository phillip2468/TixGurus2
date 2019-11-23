using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleWPF.Input;
using SimpleWPF.ViewModels;
using Ssample.Model;

namespace Ssample.ViewModel.zEmployees.Event_Management
{
    public class SuccessfulTicketsViewModel : NavigationViewModelBase
    {
        public ICommand NavCommand { get; set; }
        public SuccessfulTicketsViewModel()
        {
            CustomerDatabaseEntities context = new CustomerDatabaseEntities();
            TicketList = (List<Ticket_Details>)(from data in context.Ticket_Details select data).ToList();
            NavCommand = new RelayCommand<NavigationViewModelBase>(Nav);
        }

        private void Nav(NavigationViewModelBase viewModel)
        {
            Navigate(viewModel);
        }

        private List<Ticket_Details> Ticket { get; set; }

        public List<Ticket_Details> TicketList
        {
            get => Ticket;
            set => Ticket = value;
        }
    }
}
