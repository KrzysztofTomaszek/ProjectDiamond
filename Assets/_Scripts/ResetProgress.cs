using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    SaveSystem save;

    // Start is called before the first frame update
    void Start()
    {
        save = new SaveSystem();
        save.LoadGameValues();
    }   
}
