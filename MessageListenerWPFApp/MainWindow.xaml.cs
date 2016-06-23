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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MessageListenerWPFApp.Business.Interfaces;
using MessageListenerWPFApp.Business;
using System.Windows.Media.Animation;

namespace MessageListenerWPFApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                lblCopyRight.Content = string.Format("© {0} - CodeProject - For those who code", DateTime.Now.Year);
                IDBListener dbListener = new HubConfigurationManager();
                grdConfigurationLookup.ItemsSource = new ConfigurationLookUpBL(dbListener).ConfigurationLookUpCaches;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Title);
            }
        }
    }

}
