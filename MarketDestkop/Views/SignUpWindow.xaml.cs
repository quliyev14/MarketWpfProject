using MarketWpfProject.ViewModels;
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
using System.Windows.Shapes;

namespace MarketDestkop.Views
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
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
