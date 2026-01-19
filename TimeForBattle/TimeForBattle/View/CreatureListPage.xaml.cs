namespace TimeForBattle.View;

public partial class CreatureListPage : ContentPage
{
	CreatureListViewModel viewModel;

    public CreatureListPage(CreatureListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		this.viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.RefreshCreaturesAsync();
    }
}