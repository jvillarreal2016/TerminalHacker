using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game config data
    const string menuHint = "You may type menu at any point.";
    string[] levelOnePasswords = { "books", "desk", "shelf", "paper", "wood" };
    string[] levelTwoPasswords = { "uniform", "police", "prisoner", "patrol", "victim" };
    string[] levelThreePasswords = { "fighterjet", "military", "commander", "airforce", "security" };

    // Use this for initialization

    //Game State
    int level;
    enum Screen {MainMenu, Password, Win };
    Screen currentScreen;
    string password;

	void Start ()
    {
        showMainMenu();
	}
	
    void showMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for Local Library");
        Terminal.WriteLine("Press 2 for Police Station");
        Terminal.WriteLine("Press 3 for Pentagon");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu") // we can always go direct to main menu
        {
            showMainMenu();
        }
       else if (input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("If on the web version, close the tab");
            Application.Quit();
        }
       else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
       else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

     void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007") // Easter egg
        {
            Terminal.WriteLine("Please select a level Mr. Bond");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = levelOnePasswords[Random.Range(0, levelOnePasswords.Length)];
                break;
            case 2:
                password = levelTwoPasswords[Random.Range(0, levelOnePasswords.Length)];
                break;
            case 3:
                password = levelThreePasswords[Random.Range(0, levelThreePasswords.Length)];
                break;
            default:        //catch all statement
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
                DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
     _________
    /        //
   /        //
  /        //
 /________//
(________)/
"
                );
                break;
            case 2:
                Terminal.WriteLine("Have a key");
                Terminal.WriteLine(@"
  __
 /0 \_________
 \__/-='='='=/
");
                break;
            case 3:
                Terminal.WriteLine("You served your country well");
                Terminal.WriteLine("Here is NASA");
                Terminal.WriteLine(@"
  _ __   __ _ ____  __ _
 | '_ \ / _` /  __\/ _ `|
 | | | | (_| \__  \ (_| |
 |_| |_|\__,_|____)\__,_|
");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }
}