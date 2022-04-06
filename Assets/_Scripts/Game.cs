using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SpriteGlow;

public class Game : MonoBehaviour
{
    [SerializeField] GameObject[] Diamonds = new GameObject[12];
    [SerializeField] GameObject[] DiamondShelf = new GameObject[3];
    [SerializeField] GameObject[] DiamondsContrac1 = new GameObject[5];
    [SerializeField] GameObject[] DiamondsContrac2 = new GameObject[5];
    [SerializeField] GameObject[] DiamondsContrac3 = new GameObject[5];
    [SerializeField] GameObject[] DiamondsContrac4 = new GameObject[5];
    [SerializeField] GameObject[] DiamondsContrac5 = new GameObject[5];
    

    SpriteRenderer[] DiamondsRenderer = new SpriteRenderer[12];
    SpriteRenderer[] DiamondShelfRenderer = new SpriteRenderer[3];
    SpriteRenderer[] DiamondsContrac1Renderer = new SpriteRenderer[5];
    SpriteRenderer[] DiamondsContrac2Renderer = new SpriteRenderer[5];
    SpriteRenderer[] DiamondsContrac3Renderer = new SpriteRenderer[5];
    SpriteRenderer[] DiamondsContrac4Renderer = new SpriteRenderer[5];
    SpriteRenderer[] DiamondsContrac5Renderer = new SpriteRenderer[5];
    SpriteRenderer[] DiamondsChain = new SpriteRenderer[5];

    List<LineRenderer> lineList = new List<LineRenderer>();

    [SerializeField] LineRenderer linePrefab;

    [SerializeField] GameObject c2f1Butt;
    [SerializeField] GameObject newDiamondButt;
    [SerializeField] GameObject newContractButt;
    [SerializeField] GameObject sellContract1Butt;
    [SerializeField] GameObject sellContract2Butt;
    [SerializeField] GameObject sellContract3Butt;
    [SerializeField] GameObject sellContract4Butt;
    [SerializeField] GameObject sellContract5Butt;
    [SerializeField] GameObject moneyText;
    [SerializeField] GameObject contrac4;
    [SerializeField] GameObject contrac5;

    int ChangeTwoMode = 0; // 0-Start, 1-Cost, 2-Select First, 3-Select Second
    int money = 3;
    int chainPosition = 0;
    int Contract1Worth = 0;
    int Contract2Worth = 0;
    int Contract3Worth = 0;
    int Contract4Worth = 0;
    int Contract5Worth = 0;

    RaycastHit2D lastHit;

    SpriteRenderer shelfDiamond;
    SpriteRenderer changeFirstDiamond;
    SpriteRenderer changeSecondDiamond;
    
    bool ShelflMode = false;
    bool ChainingMode = false;
    bool c2f1ButtState = true;

    Dictionary<int, int[]> diamondPath = new Dictionary<int, int[]>
    {
        {1,new int[]{3,4}},
        {2,new int[]{4,5}},
        {3,new int[]{1,6}},
        {4,new int[]{1,2,6,7}},
        {5,new int[]{2,7}},
        {6,new int[]{3,4,8,9}},
        {7,new int[]{4,5,9,10}},
        {8,new int[]{6,11}},
        {9,new int[]{6,7,11,12}},
        {10,new int[]{7,12}},
        {11,new int[]{8,9}},
        {12,new int[]{9,10}}
    };


    // Start is called before the first frame update
    void Start()
    {
        InitializeSpriteArray(ref Diamonds, DiamondsRenderer);
        InitializeSpriteArray(ref DiamondShelf, DiamondShelfRenderer);
        InitializeSpriteArray(ref DiamondsContrac1, DiamondsContrac1Renderer);
        InitializeSpriteArray(ref DiamondsContrac2, DiamondsContrac2Renderer);
        InitializeSpriteArray(ref DiamondsContrac3, DiamondsContrac3Renderer);
        InitializeSpriteArray(ref DiamondsContrac4, DiamondsContrac4Renderer);
        InitializeSpriteArray(ref DiamondsContrac5, DiamondsContrac5Renderer);
        if(!Globals.skillShop["moreContracts1"]) contrac4.SetActive(false);
        if(!Globals.skillShop["moreContracts2"]) contrac5.SetActive(false);
        InitializeSheet();
        GenerateDiamondsInShelf(DiamondShelfRenderer);
        GenerateDiamondsInContract(DiamondsContrac1Renderer);
        GenerateDiamondsInContract(DiamondsContrac2Renderer);
        GenerateDiamondsInContract(DiamondsContrac3Renderer);
        GenerateDiamondsInContract(DiamondsContrac4Renderer);
        GenerateDiamondsInContract(DiamondsContrac5Renderer);        
        money += Globals.money;
        UpdateMoney(moneyText, money);
        sellContract1Butt.SetActive(false);
        sellContract2Butt.SetActive(false);
        sellContract3Butt.SetActive(false);
        sellContract4Butt.SetActive(false);
        sellContract5Butt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Globals.money = money;
        CustomOnMouseOver();
        if (c2f1ButtState == true) if (DiamondsOnSheet() <= 3) ChangeTwoForOneButtonStatus(false);

        

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            SpriteRenderer ren = new SpriteRenderer();
            if (hit.collider != null) ren = hit.collider.gameObject.GetComponent<SpriteRenderer>();

            if (hit.collider != null && hit.collider.name.Contains("DiamondG"))
            {
              AudioController.instance.PlayButton();
            }

            if (ChangeTwoMode == 0  && ShelflMode && hit.collider != null && hit.collider.name.Contains("DiamondShelf")) 
            {
                if (Globals.skillShop["betterAdding"])UnclickOnShelf(ren);
                else if (!ChainingMode)UnclickOnShelf(ren);                
            }
            else if (ChangeTwoMode == 0  && !ShelflMode && hit.collider != null && hit.collider.name.Contains("DiamondShelf"))
            {
                if(Globals.skillShop["betterAdding"]) GetFromShelf(ren);
                else if(!ChainingMode)GetFromShelf(ren);
            }
            else if (ChangeTwoMode == 0 && !ChainingMode && hit.collider != null && hit.collider.name.Contains("DiamondG") && !ShelflMode)
            {
                if (ren.sprite != null)
                { 
                    chainPosition = 0;
                    DiamondsChain = new SpriteRenderer[5];
                    newContractButt.GetComponent<Button>().interactable = false;
                    ChangeTwoForOneButtonStatus(false);
                    ChainingMode = true;
                    AddToChain(ren, chainPosition);
                }
            }
            else if (ChangeTwoMode == 0 && ChainingMode && hit.collider != null && hit.collider.name.Contains("DiamondG") && !ShelflMode)
            {
               if(ren.sprite!=null)AddToChain(ren,chainPosition);
            }
            else if (ChangeTwoMode == 1 && !ChainingMode && hit.collider.name.Contains("DiamondG") && !ShelflMode && ren.sprite != null)
            {
                ren.sprite = null;
                ChangeTwoMode = 2;
            }
            else if (ChangeTwoMode == 2 && !ChainingMode && hit.collider.name.Contains("DiamondG") && !ShelflMode && ren.sprite != null)
            {
                changeFirstDiamond = ren;
                ren.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 3;
                ChangeTwoMode = 3;
            }
            else if (ChangeTwoMode == 3 && !ChainingMode && hit.collider.name.Contains("DiamondG") && !ShelflMode && ren.sprite != null)
            {
                if(ren.name != changeFirstDiamond.name)
                {
                    changeSecondDiamond = ren;
                    changeFirstDiamond.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
                    ChangeTwoMode = 0;
                    ChangeTwoDiamonds(ref changeFirstDiamond, ref changeSecondDiamond);
                    ChangeTwoForOneButtonStatus(true);
                }                
            }
            else if (ChangeTwoMode == 0 && hit.collider != null && hit.collider.name.Contains("DiamondG") && ShelflMode && ren.sprite == null)
            {
                if (Globals.skillShop["betterAdding"]) SetFromShelf(ren);
                else if (!ChainingMode) SetFromShelf(ren);                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            SpriteRenderer ren = new SpriteRenderer();
            if (hit.collider != null) ren = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            if (ChangeTwoMode == 0 && !ChainingMode && hit.collider != null && hit.collider.name.Contains("DiamondG") && !ShelflMode && Globals.skillShop["erase"])
            {
                AudioController.instance.PlayButton();                
                ren.sprite = null;
            }
        }
    }

    void InitializeSpriteArray(ref GameObject[] Arr, Renderer[] ArrS)
    {
        for (int i = 0; i < Arr.Length; i++)
        {
            ArrS[i] = Arr[i].GetComponent<SpriteRenderer>();
        }
    }

    int DiamondsOnSheet()
    {
        int value = 0;
        foreach (SpriteRenderer ren in DiamondsRenderer)
            if (ren.sprite != null) value++;
        return value;
    }

    void InitializeSheet()
    {
        DiamondsRenderer[10].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[8].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[7].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[6].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[5].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[3].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        DiamondsRenderer[2].sprite = null;
        DiamondsRenderer[4].sprite = null;
        DiamondsRenderer[11].sprite = null;
        DiamondsRenderer[1].sprite = null;
        DiamondsRenderer[9].sprite = null;
        DiamondsRenderer[0].sprite = null;
        if (Globals.skillShop["moreDiamond1"]) DiamondsRenderer[2].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        if(Globals.skillShop["moreDiamond2"]) DiamondsRenderer[4].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        if(Globals.skillShop["moreDiamond3"]) DiamondsRenderer[11].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
        if(Globals.skillShop["moreDiamond4"]) DiamondsRenderer[1].sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
    }

    
    void GenerateDiamondsInContract(SpriteRenderer[] Arr)
    {
        foreach (SpriteRenderer ren in Arr)
            ren.sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
    }

    void GenerateDiamondsInShelf(SpriteRenderer[] Arr)
    {
        if (Globals.skillShop["jockers"])foreach(SpriteRenderer ren in Arr)ren.sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length)];
        else foreach(SpriteRenderer ren in Arr)ren.sprite = Globals.colorsSprite[Random.Range(0, Globals.colorsSprite.Length - 1)];
    }

    public void ShelfButton()
    {
        if (money > 0)
        {
            money--;
            Globals.totalMoneySpend++;
            UpdateMoney(moneyText, money);
            GenerateDiamondsInShelf(DiamondShelfRenderer);
        }
    }

    public void ContractsButton()
    {
        if (money > 0)
        {
            money--;
            Globals.totalMoneySpend++;
            UpdateMoney(moneyText, money);
            GenerateDiamondsInContract(DiamondsContrac1Renderer);
            ShowNecklace(DiamondsContrac1Renderer[0].gameObject);

            GenerateDiamondsInContract(DiamondsContrac2Renderer);
            ShowNecklace(DiamondsContrac2Renderer[0].gameObject);

            GenerateDiamondsInContract(DiamondsContrac3Renderer);
            ShowNecklace(DiamondsContrac3Renderer[0].gameObject);

            if(Globals.skillShop["moreContracts1"])
            {
                GenerateDiamondsInContract(DiamondsContrac4Renderer);
                ShowNecklace(DiamondsContrac4Renderer[0].gameObject);
            }

            if (Globals.skillShop["moreContracts2"])
            {
                GenerateDiamondsInContract(DiamondsContrac5Renderer);
                ShowNecklace(DiamondsContrac5Renderer[0].gameObject);
            }
        }
    }

    public void ChangeTwoModeSwitch()
    {
        if (ShelflMode == false && ChangeTwoMode == 0)
        {
            ChangeTwoMode = 1;
            ChangeTwoForOneButtonStatus(false);
        }        
    }


    void AddToChain(SpriteRenderer ren, int chain)
    {
        bool ifAvalible = false;
        
        if(chain != 0)
        {
            int earlierDiamond = int.Parse((DiamondsChain[chain - 1].name.Substring(8)));
            diamondPath.TryGetValue(earlierDiamond, out int[] earlierDiamondPath);
            int diamondNum = int.Parse(ren.name.Substring(8));
            foreach(int n in earlierDiamondPath) if(n == diamondNum)ifAvalible = true;
            foreach (SpriteRenderer sr in DiamondsChain) if (sr != null) if (sr.name == ren.name)ifAvalible = false;            
            if (DiamondsChain[chain-1] != null) if(DiamondsChain[chain - 1].name == ren.name)
            {
                if (chainPosition-1 != 0)
                {
                    Destroy(lineList.Last());
                    lineList.Remove(lineList.Last());
                }
                DiamondsChain[chain - 1].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
                    if (chain - 2 >= 0) DiamondsChain[chain - 2].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 5;

                    DiamondsChain[chain - 1] = null;
                chainPosition--;
                if (chainPosition==0)
                {
                    newContractButt.GetComponent<Button>().interactable = true;
                        ChangeTwoForOneButtonStatus(true);
                    ClearAllContracts();
                    ChainingMode = false;
                }
                StartCheckingContracts();
            }
        }
        else ifAvalible = true;

        if(ren.sprite==null) ifAvalible = false;

        if (chain > 4) return;

        if (ifAvalible)
        {
            DiamondsChain[chain] = ren;
            if (chain >= 1)
            {
                SpawnConnection(DiamondsChain[chain - 1].gameObject.transform, DiamondsChain[chain].gameObject.transform);
            }
            //if (chain != 0) DiamondsChain[chain - 1].color = Color.grey;
            if (chain != 0) DiamondsChain[chain - 1].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 3;
            ren.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 5;
            chainPosition++;
            StartCheckingContracts();
        }        
    }

    void ChangeTwoForOneButtonStatus(bool active)
    {
        if(active && DiamondsOnSheet()>=3)
        {
            c2f1ButtState = true;
            c2f1Butt.GetComponent<Button>().interactable = true;
        }
        else
        {
            c2f1ButtState = false;
            c2f1Butt.GetComponent<Button>().interactable = false;
        }
    }



    void StartCheckingContracts()
    {
        HideContractsButtons();
        CheckContract(DiamondsContrac1Renderer);
        CheckContract(DiamondsContrac2Renderer);
        CheckContract(DiamondsContrac3Renderer);
        if(Globals.skillShop["moreContracts1"])CheckContract(DiamondsContrac4Renderer);
        if(Globals.skillShop["moreContracts2"])CheckContract(DiamondsContrac5Renderer);
    }

    void ClearAllContracts()
    {
        ClearContract(DiamondsContrac1Renderer);
        ClearContract(DiamondsContrac2Renderer);
        ClearContract(DiamondsContrac3Renderer);
        ClearContract(DiamondsContrac4Renderer);
        ClearContract(DiamondsContrac5Renderer);
    }

    void CheckContract(SpriteRenderer[] renA)
    {
        int chainN = chainPosition - 1;
        int chainLeft = 0;
        int chainRight = 0;
        bool ifAvalibleR = true;
        bool ifAvalibleL = true;
        ClearContract(renA);
        

        for (int i = 0; i <= chainN; i++)
        {
            if(DiamondsChain[i]!=null)
            {
                if (DiamondsChain[i].sprite == renA[i].sprite || DiamondsChain[i].sprite == Globals.jockerSprite) chainLeft++;
                else
                {
                    ifAvalibleL = false;
                    break;
                }
            }
            else break;
        }
        
        for (int i = 0; i <= chainN; i++)
        {
            if (DiamondsChain[i] != null)
            {
                if (DiamondsChain[i].sprite == renA[renA.Length - i - 1].sprite) chainRight++;
                else
                {
                    ifAvalibleR = false;
                    break;
                }
            }
            else break;            
        }
       
        if (chainLeft > chainRight)
        {
            if (ifAvalibleL)
            {
                for(int i = 0; i < chainLeft; i++) renA[i].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 3;
                if (chainLeft >= 3) ContractActivation(renA, chainLeft);
            }
        }
        else
        {
            if (ifAvalibleR)
            {
                for(int i = renA.Length - 1; i >= renA.Length - chainRight; i--) renA[i].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 3;
                if (chainRight >= 3) ContractActivation(renA, chainRight);
            }
        }              
    }

    void ContractActivation(SpriteRenderer[] renA, int lenght)
    {
        int contractWorth = 0;

        switch (lenght)
        {
            case 3:
                contractWorth = 3;
                break;
            case 4:
                contractWorth = 5;
                break;
            case 5:
                contractWorth = 8;
                break;
        }
        GameObject button;

        switch (int.Parse(renA[0].name.Substring(15,1)))
        {
            case 1:
                button = sellContract1Butt;
                Contract1Worth = contractWorth;
                break;
            case 2:
                button = sellContract2Butt;
                Contract2Worth = contractWorth;
                break;
            case 3:
                button = sellContract3Butt;
                Contract3Worth = contractWorth;
                break;
            case 4:
                button = sellContract4Butt;
                Contract4Worth = contractWorth;
                break;
            case 5:
                button = sellContract5Butt;
                Contract5Worth = contractWorth;
                break;
            default:
                button = null;
                break;
        }
        button.SetActive(true);
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Sell for " + contractWorth.ToString() + "$";        
    }

    void HideContractsButtons()
    {
        sellContract1Butt.SetActive(false);
        sellContract2Butt.SetActive(false);
        sellContract3Butt.SetActive(false);
        sellContract4Butt.SetActive(false);
        sellContract5Butt.SetActive(false);

        Contract1Worth = 0;
        Contract2Worth = 0;
        Contract3Worth = 0;  
        Contract4Worth = 0;
        Contract5Worth = 0;
    }

    public void SellContract(int contractIndex)
    {
        int moneyToAdd = 0;
        switch (contractIndex)
        {
            case 1:
                moneyToAdd = Contract1Worth;
                HideNecklace(DiamondsContrac1Renderer[0].gameObject);
                break;
            case 2:
                moneyToAdd = Contract2Worth;
                HideNecklace(DiamondsContrac2Renderer[0].gameObject);
                break;
            case 3:
                moneyToAdd = Contract3Worth;
                HideNecklace(DiamondsContrac3Renderer[0].gameObject);
                break;
            case 4:
                moneyToAdd = Contract4Worth;
                HideNecklace(DiamondsContrac4Renderer[0].gameObject);
                break;
            case 5:
                moneyToAdd = Contract5Worth;
                HideNecklace(DiamondsContrac5Renderer[0].gameObject);
                break;
        }
        money += moneyToAdd;
        CountDiamonds(DiamondsChain);
        ClearDiamonds(DiamondsChain, 0, chainPosition);
        DestroyAllconection();
        newContractButt.GetComponent<Button>().interactable = true;
        ChangeTwoForOneButtonStatus(true);
        ClearAllContracts();
        ChainingMode = false;
        UpdateMoney(moneyText, money);
        Globals.totalMoneyEarned += moneyToAdd;
        Globals.contractsCompleted++;
        HideContractsButtons();
    }

    void UpdateMoney(GameObject Text, int money)
    {
        Text.GetComponentInChildren<TextMeshProUGUI>().text = "Money: " + money.ToString() + "$";
    }

    void HideNecklace(GameObject necklaceChild)  //To mo¿e byæ animacja - nie usuwaæ
    {
        //necklaceChild.transform.parent.gameObject.GetComponent<Animation>().Play();
        //necklaceChild.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = null
        necklaceChild.transform.parent.gameObject.SetActive(false);
    }

    void ShowNecklace(GameObject necklaceChild) //To mo¿e byæ animacja - nie usuwaæ
    {
        necklaceChild.transform.parent.gameObject.SetActive(true);
        //necklaceChild.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = Globals.necklace;
    }

    void ClearContract(SpriteRenderer[] renA, int start=0, int end=5)
    {
        for (int i = start; i < end; i++)
        {
            renA[i].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        }
    }

    void ClearDiamonds(SpriteRenderer[] renA, int start = 0, int end = 5)
    {
        for (int i = start; i < end; i++)
        {
            renA[i].gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
            renA[i].sprite = null;
        }
    }

    void GetFromShelf(SpriteRenderer ren)
    {
        if (ren.sprite != null)
        {
            //ren.color = Color.grey;
            ren.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 3;
            shelfDiamond = ren;
            ShelflMode = !ShelflMode;
            ChangeTwoForOneButtonStatus(false);
            newDiamondButt.GetComponent<Button>().interactable = false;
        }            
    }

    void SetFromShelf(SpriteRenderer ren)
    {
        ren.sprite = shelfDiamond.sprite;
        ChangeTwoForOneButtonStatus(true);
        shelfDiamond.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        shelfDiamond.sprite = null;
        ShelflMode = !ShelflMode;
        newDiamondButt.GetComponent<Button>().interactable = true;
    }

    void UnclickOnShelf(SpriteRenderer ren)
    {
        ShelflMode = false;
        shelfDiamond.gameObject.GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        newDiamondButt.GetComponent<Button>().interactable = true;
        ChangeTwoForOneButtonStatus(true);

        if (shelfDiamond.gameObject.name != ren.gameObject.name)
        {
            GetFromShelf(ren);
        }       
    }

    void ChangeTwoDiamonds(ref SpriteRenderer d1, ref SpriteRenderer d2)
    {
        Sprite sP = d1.sprite;
                 
        d1.sprite = d2.sprite;
        d2.sprite = sP;
    }

    void CustomOnMouseOver()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
       
        if (lastHit.collider == null)
        {
            if (hit.collider != null) //Enter
            {
                hit.collider.gameObject.transform.localScale = new Vector3(11f, 11f, 1);
                lastHit = hit;
            }
        }
        else
        {
            if (hit.collider != null && hit.collider.name != lastHit.collider.name) //Enter
            {
                hit.collider.gameObject.transform.localScale = new Vector3(11f, 11f, 1);
                lastHit = hit;
            }
            else if (hit.collider != null && hit.collider.name == lastHit.collider.name) //Stay
            {

            }
            else if (hit.collider == null) //Over
            {
                lastHit.collider.gameObject.transform.localScale = new Vector3(7.36f, 7.36f, 7.36f);
                lastHit = new RaycastHit2D();
            }
        }        
    }
    //ToFix
    void CountDiamonds(SpriteRenderer[] chain)
    {
        for(int i=0; i < chainPosition; i++)
        {
            switch(int.Parse(chain[i].sprite.name.Substring(10,1)))
            {
                case 0:
                    Globals.diamondsGiven["yellow"]++;
                    break;
                case 1:
                    Globals.diamondsGiven["red"]++;
                    break;
                case 2:
                    Globals.diamondsGiven["jocker"]++;
                    break;
                case 3:
                    Globals.diamondsGiven["purple"]++;
                    break;
                case 4:
                    Globals.diamondsGiven["blue"]++;
                    break;
                case 5:
                    Globals.diamondsGiven["green"]++;
                    break;
            }
        }        
    }

    void SpawnConnection(Transform start , Transform end)
    {
        LineRenderer newLine = Instantiate(linePrefab);
        lineList.Add(newLine);
        newLine.GetComponent<Connection>().AssignConnection(start, end);
    }

    void DestroyAllconection()
    {
        for(int i= lineList.Count; i>0;i--)
        {
            Destroy(lineList.Last());
            lineList.Remove(lineList.Last());
        }        
    }
}