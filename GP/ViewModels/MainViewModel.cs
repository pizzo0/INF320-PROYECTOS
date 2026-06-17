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
    }

    [RelayCommand]
    public async Task GoToCreateTransaction()
    {
        await Shell.Current.GoToAsync(nameof(CreateTransactionPage));
    }
}