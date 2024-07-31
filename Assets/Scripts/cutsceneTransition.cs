using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneTransition : MonoBehaviour
{
    public GameObject vignetterZoomer;
    private void OnEnable()
    {
        vignetterZoomer.SetActive(true);
        Invoke(nameof(toTheGame), 4f);
    }
    private void toTheGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
