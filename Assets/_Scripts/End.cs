using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    SaveSystem save;

    [SerializeField] TextMeshProUGUI dolar;
    [SerializeField] TextMeshProUGUI dolarSpend;
    [SerializeField] TextMeshProUGUI dolarEarned;
    [SerializeField] TextMeshProUGUI contracts;
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI red;
    [SerializeField] TextMeshProUGUI blue;
    [SerializeField] TextMeshProUGUI green;
    [SerializeField] TextMeshProUGUI jocker;
    [SerializeField] TextMeshProUGUI purple;
    [SerializeField] TextMeshProUGUI yellow;


    // Start is called before the first frame update
    void Awake()
    {
        Globals.days--;
        save = new SaveSystem();
        save.SaveGameValues();
    }

    private void Start()
    {
        SetCourutineStats();
    }

    void SetStatsOld()
    {
        dolar.text = Globals.money.ToString();
        day.text = Globals.days.ToString();
        red.text = Globals.diamondsGiven["red"].ToString();
        blue.text = Globals.diamondsGiven["blue"].ToString();
        green.text = Globals.diamondsGiven["green"].ToString();
        jocker.text = Globals.diamondsGiven["jocker"].ToString();
        purple.text = Globals.diamondsGiven["purple"].ToString();
        yellow.text = Globals.diamondsGiven["yellow"].ToString();
        dolarSpend.text = Globals.totalMoneySpend.ToString();
        contracts.text = Globals.contractsCompleted.ToString();
        dolarEarned.text = Globals.totalMoneyEarned.ToString();
    }


    void SetCourutineStats()
    {
        day.GetComponent<AnimCounter>().Counting(Globals.days + 1);
        dolar.GetComponent<AnimCounter>().Counting(Globals.money);
        dolarEarned.GetComponent<AnimCounter>().Counting(Globals.totalMoneyEarned);
        red.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["red"]);
        green.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["green"]);
        blue.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["blue"]);
        jocker.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["jocker"]);
        purple.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["purple"]);
        yellow.GetComponent<AnimCounter>().Counting(Globals.diamondsGiven["yellow"]);
        dolarSpend.GetComponent<AnimCounter>().Counting(Globals.totalMoneySpend);
        contracts.GetComponent<AnimCounter>().Counting(Globals.contractsCompleted);
    }
}
