using System.Collections.ObjectModel;
using GP.Models;
using GP.Database;
using GP.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;

namespace GP.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly TransactionRepository _repo;
    public ObservableCollection<Transaction> Transactions { get; } = [];

    [ObservableProperty]
    public partial decimal Balance { get; set; }

    [ObservableProperty]
    public partial decimal TotalIncome { get; set; }

    [ObservableProperty]
    public partial decimal TotalExpenses { get; set; }

    public MainViewModel(TransactionRepository repo)
    {
        _repo = repo;
        LoadTransactions();
    }

    public void LoadTransactions()
    {
        var list = _repo.Get();
        Transactions.Clear();

        foreach (var t in list) Transactions.Add(t);
        LoadBalance();
    }

    public void LoadBalance()
    {
        Balance = _repo.CalculateBalance();
        TotalIncome = _repo.CalculateIncome();
        TotalExpenses = _repo.CalculateExpenses();
    }

    [RelayCommand]
    public async Task GoToCreateTransaction()
    {
        await Shell.Current.GoToAsync(nameof(CreateTransactionPage));
    }

    [RelayCommand]
    public async Task DeleteTransaction(Transaction transaction)
    {
        if (transaction == null) return;

        bool confirm = await Shell.Current.DisplayAlertAsync(
            "Eliminar transacción",
            $"¿Quieres eliminar la transacción \"{transaction.Description}\"?",
            "Eliminar",
            "Cancelar"
        );
        if (!confirm) return;

        _repo.Delete(transaction);
        Transactions.Remove(transaction);
        LoadBalance();
        await Toast.Make("Transacción eliminada").Show();
    }
}
