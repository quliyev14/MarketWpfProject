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
                 "+994(AZ)",
                 "+1 (US)",
                 "+44 (UK)",
                 "+91 (IN)",
                 "+90 (TR)",
                 "+49 (DE)",
                 "+33 (FR)",
                 "+81 (JP)",
                 "+61 (AU)",
                 "+55 (BR)",
                 "+7 (RU)",
                 "+86 (CN)",
                 "+34 (ES)",
                 "+39 (IT)",
                 "+47 (NO)",
                 "+46 (SE)",
                 "+31 (NL)",
                 "+41 (CH)",
                 "+32 (BE)",
                 "+27 (ZA)",
                 "+52 (MX)",
                 "+62 (ID)",
                 "+63 (PH)",
                 "+64 (NZ)",
                 "+65 (SG)",
                 "+66 (TH)",
                 "+48 (PL)",
                 "+43 (AT)",
                 "+20 (EG)",
                 "+98 (IR)",
                 "+234 (NG)",
                 "+94 (LK)",
                 "+92 (PK)",
                 "+977 (NP)",
                 "+880 (BD)"
            };

            foreach (var number in countryCodes)
                countrycode.Items.Add(number);
        }
    }
}
