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
    public partial Transaction Trans { get; set; } = new();

    private readonly TransactionRepository _repo = repo;

    [RelayCommand]
    public async Task CreateTransaction()
    {
        if (!IsValid()) return;

        _repo.Insert(Trans);
        await GoBack();
    }

    [RelayCommand]
    public static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Trans.Description))
        {
            Error = "La descripción es obligatoria";
            return false;
        }

        if (Trans.Amount <= 0)
        {
            Error = "Ingresa un monto válido";
            return false;
        }

        Error = string.Empty;
        return true;
    }
}