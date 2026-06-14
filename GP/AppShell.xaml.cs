using GP.Views;

namespace GP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CreateTransactionPage), typeof(CreateTransactionPage));
	}
}
