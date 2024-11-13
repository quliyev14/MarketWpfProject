using MarketWpfProject.ViewModels;
using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows;

namespace MarketDestkop.Views
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            DataContext = new SignUpViewModels();
            LoadCountryPhoneNumber();
        }

        private void LoadCountryPhoneNumber()
        {
            var countryCodes = new string[]
            {
                 "+994",
                 "+90",
                 "+1",
                 "+44",
                 "+91",
                 "+49",
                 "+33",
                 "+81",
                 "+61",
                 "+55",
                 "+7",
                 "+86",
                 "+34",
                 "+39",
                 "+47",
                 "+46",
                 "+31",
                 "+41",
                 "+32",
                 "+27",
                 "+52",
                 "+62",
                 "+63",
                 "+64",
                 "+65",
                 "+66",
                 "+48",
                 "+43",
                 "+20",
                 "+98",
                 "+234",
                 "+94",
                 "+92",
                 "+977",
                 "+880"
            };

            foreach (var number in countryCodes)
                countrycode.Items.Add(number);
        }
    }
}
