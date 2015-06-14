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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Mvvm.UI;
using DevExpress.Data;
using ViewModel;

namespace DXGridTestUI
{
    public class CustomSummaryEventArgsConverter : EventArgsConverterBase<CustomSummaryEventArgs>
    {
        public CustomSummaryEventArgsConverter()
        {
        }

        protected override object Convert(CustomSummaryEventArgs e)
        {
            switch (e.SummaryProcess) {
                case CustomSummaryProcess.Start: return CustomSummaryArgs.Start;
                case CustomSummaryProcess.Calculate: 
                    var n = (int?) e.FieldValue;
                    return CustomSummaryArgs.NewCalculate(n.Value);
                case CustomSummaryProcess.Finalize:
                    Action<int> a = delegate(int sum)
                    {
                        e.TotalValue = sum;
                    };
                    return CustomSummaryArgs.NewFinish(a);
                default: return CustomSummaryArgs.Start;
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int n;

        private void GridControl_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.IsTotalSummary)
            {
                Console.Out.WriteLine(e.ToString());
                switch (e.SummaryProcess)
                {
                    case DevExpress.Data.CustomSummaryProcess.Start:
                        n = 0;
                        break;

                    case DevExpress.Data.CustomSummaryProcess.Calculate:
                        var fv = (int?) e.FieldValue;
                        n = n + fv.Value;
                        break;

                    case DevExpress.Data.CustomSummaryProcess.Finalize:
                        e.TotalValue = n;
                        break;
                }
            }
        }
    }
}
