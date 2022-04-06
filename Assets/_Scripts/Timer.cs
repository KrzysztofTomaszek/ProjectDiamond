using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TMP_Text text;

    private void Update()
    {
        if (gameObject.GetComponent<Slider>().value <= 0f)
        {
            SceneManager.LoadScene("End", LoadSceneMode.Single);
        }
        else
        {
            gameObject.GetComponent<Slider>().value -= Time.deltaTime;
            text.text = gameObject.GetComponent<Slider>().value.ToString("F2");
        }
    }
}
