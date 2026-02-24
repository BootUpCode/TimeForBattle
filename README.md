# TimeForBattle
TimeForBattle is an app designed to assist Game Masters of tabletop roleplaying games. The app can quickly set up combat scenarios and keep track of important statistics, while the Game Master can focus on delivering an unforgettable experience.

# Quick and easy encounter management
Tabletop roleplaying games often incorporate turn-based combat. During a combat encounter the Game Master strategically controls the players' opponents, keeps track of the turn order, narrates the battle with epic descriptions and arbitrates game rule disputes.
That's a lot for one person to handle - and the players don't want to be kept waiting!
A Game Master can use TimeForBattle to quickly assign creatures to a combat encounter and view a clear summary of their statistics. TimeForBattle can also automatically roll for initiative, attack rolls and saving throws, saving the Game Master a lot of time and calculations.

# Overview and Instructions
To try TimeForBattle on Windows PC:
1. Download TimeForBattle.exe from this repository
2. If .NET 9.0 is not yet installed, download and install the [most recent release](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
3. Run TimeForBattle.exe

<img width="1600" height="3000" alt="TimeForBattle guide Complete" src="https://github.com/user-attachments/assets/fe5fc0fc-3b88-4beb-98e2-2c8bf89ca47e" />

# Updates and plans
- Add creature list page (Done!)
- Add creature details page (Done!)
- Add creature creation page (Done!)
- Add creature Create/Read/Update/Delete to database (Done!)
- Add initiative list page (Done!)
- Add player characters as a separate class from other creatures (Done!)
- Add saving and loading separate combat encounters (Done!)
- Add rolling initiative (Done!)
  - Automatic rolling and sorting of creatures (Done!)
  - Custom input of initiative for player characters (Done!)
  - Separate initiative rolling for creatures added after initiative was rolled (Done!)
- Add hit points tracking (Done!)
  - Input of current hit points (Done!)
  - Subtraction of hit points (Done!)
- Add automatic rolling of saves and attacks (Done!)
  - Change Save Modifier variables to integer, with AttributeConverter to convert to integer and back to display string to a mod with +/- (Done!)
  - Create UI element with most recent roll, with secondary advantage roll, with modifier and roll and creature descriptions (Done!)
  - Add buttons to roll saves (Done!)
- Add hotkeys to roll actions (Done!)
  - Include damage rolls (Done!)
  - Add a second hotkey (Done!)
- Add combat info (Done!)
  - Track turn count (Done!)
- Add ability to quickly import stat blocks from text (Done!)
  - Include hotkeys in import function (Done!)
  - Adjust import function to be compatible with multiple older versions of stat blocks (Done!)
- Add encounter renaming (Done!)
- Add status tracking to initiative page (Done!)
  - Track duration of status in rounds (Done!)
  - UI warning when creature is immune to assigned status (Done!)
- Adjust UI colors (Done!)
- Sort function for creature list (Done!)
- Add app icon (Done!)
- Add main menu icon (Done!)
- Add splash/loading screen (Done!)
- Add search function for creature list (Done!)
- Finish final touches
  - Documentation
  - Warnings and errors
  - Platform tests
  - UI colors and symbols
