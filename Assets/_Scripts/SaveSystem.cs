using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SaveSystem
{ 

    //the object you want to serialize
    DataToSave saveValues = new DataToSave();

    string fullPath;

    //if true data will be encripted with an XOR function
    bool encrypt = true;

    //for debug purpose only
    static string logText = "";

    public SaveSystem()
    {
        //the filename where saved data will be stored
        fullPath = Application.persistentDataPath + "/" + "Save";
    }

    //called from scene using built in unity events
    public void LoadGameValues()
    {
#if JSONSerializationFileSave || BinarySerializationFileSave
        logText += "\nLoad Started (File): " + fullPath;
#else
        logText += "\nLoad Started (PlayerPrefs): " + fullPath;
#endif
        SaveManager.Instance.Load<DataToSave>(fullPath, DataWasLoaded, encrypt);
    }

    private void DataWasLoaded(DataToSave data, SaveResult result, string message)
    {
        logText += "\nData Was Loaded";
        logText += "\nresult: " + result + ", message: " + message;

        if (result == SaveResult.EmptyData || result == SaveResult.Error)
        {
            logText += "\nNo Data File Found -> Creating new data...";
            saveValues = new DataToSave();
        }

        if (result == SaveResult.Success)
        {
            saveValues = data;
        }
       
        Globals.money = saveValues.money;
        Globals.days = saveValues.days;
        Globals.contractsCompleted = saveValues.contractsCompleted;
        Globals.totalMoneyEarned = saveValues.totalMoneyEarned;
        Globals.money = saveValues.money;


        var results1 = new List<KeyValuePair<string, int>>();
        var keys1 = saveValues.diamondListString;
        var values1 = saveValues.diamondListInt;
        results1 = keys1.Zip(values1, (x, y) => new KeyValuePair<string, int>(x, y)).ToList();
        Globals.diamondsGiven = results1.ToDictionary(x => x.Key, x => x.Value);

        var results2 = new List<KeyValuePair<string, bool>>();
        var keys2 = saveValues.skillListString;
        var values2 = saveValues.skillListBool;
        results2 = keys2.Zip(values2, (x, y) => new KeyValuePair<string, bool>(x, y)).ToList();
        Globals.skillShop = results2.ToDictionary(x => x.Key, x => x.Value);
    }

    public void SaveGameValues()
    {
        logText += "\nSave Started";

        saveValues.money = Globals.money;
        saveValues.days = Globals.days;
        saveValues.contractsCompleted = Globals.contractsCompleted;
        saveValues.totalMoneyEarned = Globals.totalMoneyEarned;
        saveValues.totalMoneySpend = Globals.totalMoneySpend;
        
        List<string> diamondListString = Globals.diamondsGiven.Keys.ToList();
        List<int> diamondListInt= Globals.diamondsGiven.Values.ToList();
        saveValues.diamondListString = diamondListString;
        saveValues.diamondListInt = diamondListInt;

        List<string> skillListString = Globals.skillShop.Keys.ToList();
        List<bool> skillListBool = Globals.skillShop.Values.ToList();
        saveValues.skillListString = skillListString;
        saveValues.skillListBool = skillListBool;

        SaveManager.Instance.Save(saveValues, fullPath, DataWasSaved, encrypt);
    }

    private void DataWasSaved(SaveResult result, string message)
    {        
        logText += "\nData Was Saved";
        logText += "\nresult: " + result + ", message: " + message;
        if (result == SaveResult.Error)
        {
            logText += "\nError saving data";
        }
        Debug.Log(logText);
    }

    public void Clear()
    {
        logText += "\nClear";
        SaveManager.Instance.ClearFIle(fullPath);
    }       
}
