using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject SmokeEffect;
    public GameObject EndSmoke;

    public GameObject settingsPanel;
    public GameObject menuPanel;
    public void OnStartClicked()
    {
        SmokeEffect.SetActive(true);
        Invoke(nameof(InvokeOpenLevel), 4f);
    }
    public void InvokeOpenLevel()
    {
        SceneManager.LoadScene("StartScene");
    }


    public void OnReStartClicked()
    {
        SmokeEffect.SetActive(true);
        Invoke(nameof(ReInvokeOpenLevel), 4f);
    }
    public void ReInvokeOpenLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenu()
    {
        SmokeEffect.SetActive(true);
        Invoke(nameof(ReMenu), 4f);
    }
    public void ReMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Started") && SceneManager.GetActiveScene().name == "Menu")
        {
            EndSmoke.SetActive(false);
            PlayerPrefs.SetInt("Started", 1);
        }
    }
    public void OpenSetting()
    {
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}