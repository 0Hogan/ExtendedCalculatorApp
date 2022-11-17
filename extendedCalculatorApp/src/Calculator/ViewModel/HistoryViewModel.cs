using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Calculator.ViewModel
{
    public partial class HistoryViewModel : ObservableObject
    {
        public HistoryViewModel()
        {
            PreviousCalculations = new();
        }

        [ObservableProperty]
        ObservableCollection<string> previousCalculations;

        [ObservableProperty]
        string lastCalculation;

        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrWhiteSpace(LastCalculation))
                return;
            PreviousCalculations.Add(LastCalculation);
        }

        [RelayCommand]
        void ClearHistory()
        {
            PreviousCalculations?.Clear();
        }

    }
}
