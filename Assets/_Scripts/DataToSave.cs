//to serialize a class you must put [System.Serializable] before class declaration
// if you want to serialize just a part from the class, then you put [System.Serializable] before each public proprety
//you can add here as many properties as you like

using System.Collections.Generic;

[System.Serializable]
public class DataToSave
{
    public int money = 0;
    public int days = 100;
    public int contractsCompleted = 0;
    public int totalMoneyEarned = 3;
    public int totalMoneySpend = 0;
    public List<string> diamondListString = new List<string>
    {
        "yellow",
        "red",
        "purple",
        "blue",
        "green",
        "jocker"
    };
    public List<int> diamondListInt = new  List<int>
    {
        0,
        0,
        0,
        0,
        0,
        0
    };
    public List<string> skillListString = new List<string>
    {
        "erase",
        "jockers",
        "betterAdding",
        "winGame",
        "moreContracts1",
        "moreContracts2",
        "moreDiamond1",
        "moreDiamond2",
        "moreDiamond3",
        "moreDiamond4"
    };
    public List<bool> skillListBool = new  List<bool>
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false
    };
}

