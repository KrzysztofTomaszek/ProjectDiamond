using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    //Not To Save
    public static readonly Sprite[] colorsSprite = new Sprite[6]
    {
        Resources.LoadAll<Sprite>("DiamondV1")[0],  //Yellow
        Resources.LoadAll<Sprite>("DiamondV1")[1],  //Red        
        Resources.LoadAll<Sprite>("DiamondV1")[3],  //Purple
        Resources.LoadAll<Sprite>("DiamondV1")[4],  //Blue
        Resources.LoadAll<Sprite>("DiamondV1")[5],   //Green   
        Resources.LoadAll<Sprite>("DiamondV1")[2]   //Jocker
    };

    public static readonly Sprite necklace = Resources.Load<Sprite>("necklace");

    public static readonly Sprite jockerSprite = colorsSprite[5];

    public static readonly string[] colorsName = new string[6]
    {
        "DiamondV1_0",
        "DiamondV1_1",        
        "DiamondV1_3",
        "DiamondV1_4",
        "DiamondV1_5",
        "DiamondV1_2"
    };

    public static readonly Dictionary<string, Sprite> colorsDic = new Dictionary<string, Sprite>
    {
        {"yellow",colorsSprite[0]},
        {"red",colorsSprite[1]},        
        {"purple",colorsSprite[2]},
        {"blue",colorsSprite[3]},
        {"green",colorsSprite[4]},
        {"jocker",colorsSprite[5]}
    };



    //To Save
    public static int money = 0;

    public static int days = 100;

    public static int contractsCompleted = 0;

    public static int totalMoneyEarned = 3;

    public static int totalMoneySpend = 0;

    public static Dictionary<string, int> diamondsGiven = new Dictionary<string, int>
    {
        {"yellow",0 },
        {"red",0 },
        {"purple",0 },
        {"blue",0 },
        {"green",0 },
        {"jocker",0 }
    };

    public static Dictionary<string, bool> skillShop = new Dictionary<string, bool>
    {
        {"erase",false},
        {"jockers",false},
        {"betterAdding",false},
        {"winGame",false},
        {"moreContracts1",false},
        {"moreContracts2",false},
        {"moreDiamond1",false},
        {"moreDiamond2",false},
        {"moreDiamond3",false},
        {"moreDiamond4",false}
    };    
}
