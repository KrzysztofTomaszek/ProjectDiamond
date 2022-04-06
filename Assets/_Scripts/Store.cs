using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{

    [SerializeField] GameObject moneyText;
    [SerializeField] GameObject canvas;
    SaveSystem save;
       
    // Start is called before the first frame update
    void Start()
    {
        save = new SaveSystem();
        UpdateMoney(moneyText, Globals.money);
        HideBoughtButtons();
        TierSkillsActiveSet("moreDiamond");
        TierSkillsActiveSet("moreContracts");
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.skillShop["winGame"])SceneManager.LoadScene("Final", LoadSceneMode.Single);
    }

    void HideBoughtButtons()
    {
        foreach(KeyValuePair<string, bool> entry in Globals.skillShop)
        {
            if (entry.Value == true) canvas.transform.Find(entry.Key).gameObject.SetActive(false); 
        }
    }

    void TierSkillsActiveSet(string skillName)
    {
        string earlyEntryString = "111111";
        bool earlyEntryBool = false;

        foreach (KeyValuePair<string, bool> entry in Globals.skillShop)
        {
            if (earlyEntryString.Substring(0, earlyEntryString.Length - 1) == skillName && earlyEntryBool == true)
            {
                if((entry.Key.Substring(0, entry.Key.Length - 1) == skillName && entry.Value == false))
                {
                    int lastActiveNumber = int.Parse(earlyEntryString.Substring(skillName.Length));
                    canvas.transform.Find(skillName + (lastActiveNumber + 1)).gameObject.SetActive(true);
                }                
            }
            earlyEntryString = entry.Key; 
            earlyEntryBool = entry.Value;
        }
    }

    public void ActiveButton(string data)
    {
        string[] dataA = data.Split('_');
        string skillName = dataA[0];
        int tier = int.Parse(dataA[1]);
        if (Globals.skillShop[skillName+(tier-1)]==true)
        {
            GameObject buttToUnhide = canvas.transform.Find(skillName+tier).gameObject;
            buttToUnhide.SetActive(true);
        }       
    }

    public void BuySkill(string data)
    {        
        string[] dataA = data.Split('_');
        string skillName = dataA[0];
        int cost = int.Parse(dataA[1]);
        if ((Globals.money - cost)>=0)
        {
            Globals.skillShop[skillName] = true;
            Globals.money -= cost;
            GameObject butt = GameObject.Find(skillName);
            butt.SetActive(false);
            UpdateMoney(moneyText, Globals.money);
            save.SaveGameValues();
        }            
    }

    void UpdateMoney(GameObject Text, int money)
    {
        Text.GetComponentInChildren<TextMeshProUGUI>().text = "Money: " + money.ToString() + "$";
    }
}
