using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Player : MonoBehaviour
{   
    public void PlayButton()
    {
        AudioController.instance.PlayButton();
    }

    public void PlayCash()
    {
        AudioController.instance.PlayCash();
    }
}
