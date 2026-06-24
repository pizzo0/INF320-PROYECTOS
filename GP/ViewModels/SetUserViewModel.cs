using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GP.Database;
using GP.Models;

namespace GP.ViewModels;

public partial class SetUserViewModel : ObservableObject
{
    private readonly UserRepository _repo;

    [ObservableProperty]
    public partial string Username { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Error { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool CanGoBack { get; set; }

    public SetUserViewModel(UserRepository repo)
    {
        _repo = repo;

        var user = _repo.GetUser();
        if (user is not null)
        {
            Username = user.Username;
            CanGoBack = !string.IsNullOrWhiteSpace(Username);
        }
        else
        {
            CanGoBack = false;
        }
    }

    [RelayCommand]
    public async Task SaveUser()
    {
        var fixedUsername = Username.Trim();
        if (string.IsNullOrWhiteSpace(fixedUsername))
        {
            Error = "El nombre no puede estar vacio";
            return;
        }
        if (!fixedUsername.All(char.IsLetter))
        {
            Error = "El nombre solo puede contener letras";
            return;
        }

        _repo.Save(new User { Username = fixedUsername });
        Error = string.Empty;

        await GoBack();
    }

    [RelayCommand]
    public static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
