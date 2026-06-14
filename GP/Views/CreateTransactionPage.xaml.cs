using GP.ViewModels;

namespace GP.Views;

public partial class CreateTransactionPage : ContentPage
{
	public CreateTransactionPage(CreateTransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
