using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using MultipleScoreCounter.ViewModels;
using ReactiveUI;

namespace MultipleScoreCounter.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
    }
}