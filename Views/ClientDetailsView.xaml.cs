using InvestmentCompany.ViewModels;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class ClientDetailsView : Window
    {
        public ClientDetailsView(ClientDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}