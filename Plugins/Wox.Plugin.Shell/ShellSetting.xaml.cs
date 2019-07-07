using System.Windows;
using System.Windows.Controls;

namespace Wox.Plugin.Shell
{
    public partial class CMDSetting : UserControl
    {
        private readonly Settings _settings;

        public CMDSetting(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void CMDSetting_OnLoaded(object sender, RoutedEventArgs re)
        {
            ReplaceWinR.IsChecked = _settings.ReplaceWinR;
            ReplaceWinQ.IsChecked = _settings.ReplaceWinQ;
            LeaveShellOpen.IsChecked = _settings.LeaveShellOpen;
            LeaveShellOpen.IsEnabled = _settings.Shell != Shell.RunCommand;

            LeaveShellOpen.Checked += (o, e) =>
            {
                _settings.LeaveShellOpen = true;
            };

            LeaveShellOpen.Unchecked += (o, e) =>
            {
                _settings.LeaveShellOpen = false;
            };

            ReplaceWinR.Checked += (o, e) =>
            {
                _settings.ReplaceWinR = true;
                _settings.ReplaceWinQ = false;
                ReplaceWinQ.IsChecked = false;
            };
            ReplaceWinR.Unchecked += (o, e) =>
            {
                _settings.ReplaceWinR = false;
            };

            ReplaceWinQ.Checked += (o, e) =>
            {
                _settings.ReplaceWinQ = true;
                _settings.ReplaceWinR = false;
                ReplaceWinR.IsChecked = false;
            };
            ReplaceWinQ.Unchecked += (o, e) =>
            {
                _settings.ReplaceWinQ = false;
            };
            ShellComboBox.SelectedIndex = (int) _settings.Shell;
            ShellComboBox.SelectionChanged += (o, e) =>
            {
                _settings.Shell = (Shell) ShellComboBox.SelectedIndex;
                LeaveShellOpen.IsEnabled = _settings.Shell != Shell.RunCommand;
            };
        }
    }
}
