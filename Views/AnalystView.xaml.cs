using InvestmentCompany.ViewModels;
using System.Windows;

namespace InvestmentCompany.Views
{
    public partial class AnalystView : Window
    {
        public AnalystView(AnalystViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}