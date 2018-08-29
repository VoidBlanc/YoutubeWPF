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
using YoutubeWPF.Search;

namespace YoutubeWPF

{
    /// <summary>
    /// Interaction logic for SearchResultUC.xaml
    /// </summary>
    public partial class SearchResultUC : UserControl
    {
        public SearchResultUC()
        {
            InitializeComponent();
        }

        private void loadSearchResult()
        {
            ResultCardUC card = new ResultCardUC();
            
            // Need to loop and add resutl
        }
    }
}
