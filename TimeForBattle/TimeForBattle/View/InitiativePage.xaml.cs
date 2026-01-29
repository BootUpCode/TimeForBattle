namespace TimeForBattle.View;

public partial class InitiativePage : ContentPage
{
	InitiativeViewModel viewModel;

	public InitiativePage(InitiativeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
        viewModel.IsBusy = true;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.IsBusy = true;
        await Task.Delay(100);
        await viewModel.RefreshInitiativeAsync();
    }

    protected async override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        await viewModel.SaveCombatAsync();
    }

    private async void InitiativeEntryCompleted(object sender, EventArgs e)
    {
        await viewModel.SaveCombatAsync();
    }
}