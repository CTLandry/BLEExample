using BLEExample.ViewModels;

namespace BLEExample.Views;

public partial class BLEScanView : ContentPage
{
	public BLEScanView(BLEScanViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}