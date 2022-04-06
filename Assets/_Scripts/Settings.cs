using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider master;
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;
    [SerializeField] TMP_Dropdown quality;

    float masterVol;
    float bgmVol;
    float sfxVol;

    private void Start()
    {
        mixer.GetFloat("MasterVol", out masterVol);
        mixer.GetFloat("BGMVol", out bgmVol);
        mixer.GetFloat("SFXVol", out sfxVol);

        master.value = Mathf.Pow(10f, masterVol/20f);
        bgm.value = Mathf.Pow(10f, bgmVol / 20f);
        sfx.value = Mathf.Pow(10f, sfxVol / 20f);

        if (SceneManager.GetActiveScene().name == "Start")
            quality.value = QualitySettings.GetQualityLevel();
    }

    public void MasterChange(float volume)
    {
        mixer.SetFloat("MasterVol", 20f * Mathf.Log10(volume));
    }

    public void BGMChange(float volume)
    {
        mixer.SetFloat("BGMVol", 20f * Mathf.Log10(volume));
    }

    public void SFXChange(float volume)
    {
        mixer.SetFloat("SFXVol", 20f * Mathf.Log10(volume));
    }

    public void QualityChange(int id)
    {
        QualitySettings.SetQualityLevel(id);
    }

    public void ShowSettings(GameObject gameObject)
    {
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
   
}
