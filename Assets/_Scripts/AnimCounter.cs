using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimCounter : MonoBehaviour
{
    TextMeshProUGUI field;
    float targetScore;
    float currentDisplayScore = 0;


    public void Counting(float score)
    {
        targetScore = score;
        field = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountUpToTarget());
    }

    IEnumerator CountUpToTarget()
    {
        while (currentDisplayScore < targetScore)
        {
            currentDisplayScore += Time.deltaTime*30f; // or whatever to get the speed you like
            currentDisplayScore = Mathf.Clamp(currentDisplayScore, 0f, targetScore);
            field.text = Mathf.Round(currentDisplayScore) + "";
            yield return null;
        }
    }
}

 
