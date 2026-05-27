using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ConsumoYPropina.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public partial int Amount { get; set; }

    [ObservableProperty]
    public partial int PeopleCount { get; set; }

    [ObservableProperty]
    public partial int TipPercentage { get; set; }

    public MainViewModel()
    {
        Amount = 0;
        PeopleCount = 1;
        TipPercentage = 0;
    }

    public decimal SubtotalPerPerson => PeopleCount > 0 ? (decimal)Amount/PeopleCount : 0;
    public decimal TipPerPerson => PeopleCount > 0 ? (decimal)Amount*TipPercentage/100/PeopleCount : 0;
    public decimal TotalPerPerson => SubtotalPerPerson+TipPerPerson;

    private void UpdateCalculations()
    {
        OnPropertyChanged(nameof(SubtotalPerPerson));
        OnPropertyChanged(nameof(TipPerPerson));
        OnPropertyChanged(nameof(TotalPerPerson));
    }

    partial void OnAmountChanged(int value)
    {
        if (value < 0)
        {
            Amount = 0;
            return;
        }
        UpdateCalculations();
    }

    partial void OnPeopleCountChanged(int value)
    {
        if (value <= 0)
        {
            PeopleCount = 1;
            return;
        }
        UpdateCalculations();
    }

    partial void OnTipPercentageChanged(int value)
    {
        if (value < 0)
        {
            TipPercentage = 0;
            return;
        }
        else if (value > 50)
        {
            TipPercentage = 50;
            return;
        }
        UpdateCalculations();
    }

    [RelayCommand]
    private void SetTipPercentage(string value) => TipPercentage = int.Parse(value);

    [RelayCommand]
    private void IncreasePeopleCount() => PeopleCount = PeopleCount+1;

    [RelayCommand]
    private void DecreasePeopleCount() => PeopleCount = PeopleCount-1;
}