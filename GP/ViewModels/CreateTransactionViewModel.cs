using GP.Models;
using GP.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GP.ViewModels;

public partial class CreateTransactionViewModel(TransactionRepository repo) : ObservableObject
{
    [ObservableProperty]
    public partial string Error { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Description { get; set; } = string.Empty;

    [ObservableProperty]
    public partial decimal Amount { get; set; } = 0;

    [ObservableProperty]
    public partial DateTime Date { get; set; } = DateTime.Today;

    [ObservableProperty]
    public partial bool IsIncome { get; set; } = true;

    private readonly TransactionRepository _repo = repo;

    [RelayCommand]
    public async Task CreateTransaction()
    {
        if (!IsValid()) return;

        Transaction t = new()
        {
            Description = Description,
            Amount = Amount,
            Date = Date,
            IsIncome = IsIncome
        };

        _repo.Insert(t);

        await GoBack();
    }

    [RelayCommand]
    public static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Description))
        {
            Error = "La descripción es obligatoria";
            return false;
        }

        if (Amount <= 0)
        {
            Error = "Ingresa un monto válido";
            return false;
        }

        Error = string.Empty;
        return true;
    }
}