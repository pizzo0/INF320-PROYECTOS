using ConsumoYPropina.ViewModels;

namespace ConsumoYPropina.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		AmountEntry.Unfocused += (sender, e) => OnEntryUnfocused(sender, e, vm.Amount.ToString());
	}

	private void OnEntryUnfocused(object? sender, FocusEventArgs e, string value)
	{
		var entry = (Entry?)sender;
		if (string.IsNullOrWhiteSpace(entry?.Text)) entry?.Text = value;
	}
}