using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GeneradorDeColores;

public partial class MainPage : ContentPage
{
	bool flag = false;
	Random rnd = new Random();

	public MainPage()
	{
		InitializeComponent();
		SetColor();
	}

	private void SetColor()
	{
		int r = (int)RedValue.Value;
		int g = (int)GreenValue.Value;
		int b = (int)BlueValue.Value;

		var color = Color.FromRgb(r,g,b);

		ContentPage.BackgroundColor = color;
		HexCode.Text = $"#{r:X2}{g:X2}{b:X2}";
	}

	private void OnSliderValueChanged(object? sender, ValueChangedEventArgs args)
	{
		if (flag) return;
		SetColor();
	}

	private void OnGenerateRandomColor(object? sender, EventArgs args)
	{
		flag = true;

		RedValue.Value = rnd.Next(0,256);
		GreenValue.Value = rnd.Next(0,256);
		BlueValue.Value = rnd.Next(0,256);

		flag = false;

		SetColor();
	}

	private async void OnHexTapped(object? sender, EventArgs args)
	{
		await Clipboard.SetTextAsync(HexCode.Text);
		await Toast.Make(
			"Color copiado",
			ToastDuration.Short
		).Show();
	}
}
