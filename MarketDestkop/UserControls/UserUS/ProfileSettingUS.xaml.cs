using System.Windows.Controls;
using MarketWpfProject.ViewModels.UserUserControlViewModel;

namespace MarketWpfProject.UserControls.UserUS
{
    public partial class ProfileSettingUS : UserControl
    {
        public ProfileSettingUS()
        {
            InitializeComponent();
            DataContext = new ProfileSettingUSViewModel();
        }
    }
}
