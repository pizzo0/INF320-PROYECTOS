using GP.ViewModels;

namespace GP.Views;

public partial class SetUserPage : ContentPage
{
    public SetUserPage(SetUserViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
