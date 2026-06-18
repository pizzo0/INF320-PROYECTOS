using System.Collections.ObjectModel;
using GP.Models;
using GP.Database;
using GP.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GP.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly TransactionRepository _repo;
    public ObservableCollection<Transaction> Transactions { get; } = [];

    public decimal Balance { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }

    public MainViewModel(TransactionRepository repo)
    {
        _repo = repo;
        LoadTransactions();
        LoadBalance();
    }

    public void LoadTransactions()
    {
        var list = _repo.Get();
        Transactions.Clear();
        foreach (var t in list) Transactions.Add(t);
    }

    public void LoadBalance()
    {
        var balance = _repo.CalculateBalance();
        var income = _repo.CalculateIncome();
        var expenses = _repo.CalculateExpenses();

        Balance = balance;
        TotalIncome = income;
        TotalExpenses = expenses;
    }

    [RelayCommand]
    public async Task GoToCreateTransaction()
    {
        await Shell.Current.GoToAsync(nameof(CreateTransactionPage));
        LoadBalance();
    }
}
