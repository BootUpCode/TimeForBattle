namespace TimeForBattle;

public partial class App : Application
{
    public static ViewModel.CreatureListViewModel? InitiativeViewModel { get; private set; }

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        const int newHeight = 800;
        const int newWidth = 340;

        var newWindow = new Window(new AppShell())
        {
            Height = newHeight,
            Width = newWidth
        };

        return newWindow;

        //return new Window(new AppShell());
    }
}