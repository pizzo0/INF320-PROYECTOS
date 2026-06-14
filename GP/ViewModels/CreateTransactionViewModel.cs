using GP.Models;
using GP.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GP.ViewModels;

public partial class CreateTransactionViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Error { get; set; }

    [ObservableProperty]
    public partial Transaction Trans { get; set; }

    private readonly TransactionRepository _transactionRepository;

    public CreateTransactionViewModel(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
        Error = string.Empty;
        Trans = new();
    }

    [RelayCommand]
    public async Task CreateTransaction()
    {
        if (!IsValid()) return;

        _transactionRepository.Insert(Trans);
        await GoBack();
    }

    [RelayCommand]
    public async Task GoBack()
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