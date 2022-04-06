using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    SaveSystem save;
    [SerializeField] TextMeshProUGUI day;


    // Start is called before the first frame update
    void Start()
    {
        save = new SaveSystem();
        save.LoadGameValues();
        day.text = "Day: " + Globals.days;
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.days<=0) SceneManager.LoadScene("Loose", LoadSceneMode.Single);
    }

    public void DeleteSave()
    {
        save.Clear();
    }

}
