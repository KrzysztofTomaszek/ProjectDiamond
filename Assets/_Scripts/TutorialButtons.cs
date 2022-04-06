using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    [SerializeField] GameObject plan1;
    [SerializeField] GameObject plan2;
    [SerializeField] GameObject plan3;
    [SerializeField] GameObject plan4;


    public void ActivatePlan(int planID)
    {
        HideAll();
        switch(planID)
        {
            case 1:
                plan1.SetActive(true);
                break;
            case 2:
                plan2.SetActive(true);
                break;
            case 3:
                plan3.SetActive(true);
                break;
            case 4:
                plan4.SetActive(true);
                break;
        }
    }

    void HideAll()
    {
        plan1.SetActive(false);
        plan2.SetActive(false);
        plan3.SetActive(false);
        plan4.SetActive(false);
    }
}
