using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreInfoHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject info;
    [SerializeField] GameObject[] allInfos;

    public void OnPointerEnter(PointerEventData eventData)
    {
        HideAllInfo();
        SetInfo();
    }

    void HideAllInfo()
    {
        foreach (GameObject obj in allInfos) obj.SetActive(false);
    }

    void SetInfo()
    {
        info.SetActive(true);
    }

}
