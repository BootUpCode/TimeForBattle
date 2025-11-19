using TimeForBattle.View;

namespace TimeForBattle;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));
        Routing.RegisterRoute(nameof(CombatListPage), typeof(CombatListPage));
        Routing.RegisterRoute(nameof(CreatureListPage), typeof(CreatureListPage));
        Routing.RegisterRoute(nameof(CreatureDetailsPage), typeof(CreatureDetailsPage));
        Routing.RegisterRoute(nameof(InitiativePage), typeof(InitiativePage));
        Routing.RegisterRoute(nameof(AddCreaturePage), typeof(AddCreaturePage));
    }
}
