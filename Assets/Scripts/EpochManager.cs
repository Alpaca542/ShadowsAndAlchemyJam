using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EpochManager : MonoBehaviour
{
    public GameObject gate;
    public TMP_Text countDown;
    public TMP_Text killNum;
    public TMP_Text textHelper;
    public TMP_Text EpochCounter;
    int countDownTime;
    public Transform corner1;
    public Transform corner2;
    public int killedShadows = 0;
    public int NeedToKill = 0;
    public int seconds = 90;
    public int CurrentEpoch = 0;

    bool EpochIsGoing = false;
    public bool WaitingGate = false;

    public int[] killsPerEpoch;


   // public GameObject musicsoft;
    //public GameObject musictough;
    private void Start()
    {
        SetTimer();
        Invoke(nameof(StartEpoch), seconds);
    }
    public void SetTimer()
    {
        countDownTime = seconds;
        InvokeRepeating(nameof(countDownOne), 1f, 1f);
        killedShadows = 0;
    }
    void countDownOne()
    {
        countDownTime -= 1;

    }
    void StartEpoch()
    {
        CurrentEpoch += 1;
        EpochIsGoing = true;
        NeedToKill = killsPerEpoch[CurrentEpoch - 1];
        //musicsoft.SetActive(false);
        //musictough.SetActive(true);

    }
    void FinishEpoch()
    {
       
        EpochIsGoing = false;
        countDownTime = seconds;
        if(CurrentEpoch+1>7)
        {
            GameObject.FindWithTag("loser").GetComponent<loser>().win();
        }
       // musicsoft.SetActive(true);
       // musictough.SetActive(false);
        //SetTimer();
    }
    void Update()
    {
        if (!EpochIsGoing && !WaitingGate)
        {

            countDown.text = (countDownTime / 60).ToString() + ":" + (countDownTime - ((countDownTime / 60) * 60)).ToString();
            textHelper.text = "Shadows are coming! Cook or buy crystals!";
            killNum.color = new Color32(0, 0, 0, 0);
            countDown.color = new Color32(255, 255, 255, 255);
            foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(i.gameObject);
            }
        }
        else if (EpochIsGoing && !WaitingGate)
        {
            killNum.text = killedShadows.ToString() + "/" + NeedToKill.ToString();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            if (killedShadows >= NeedToKill)
            {

                WaitingGate = true;
                Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
                
                spawner.arrow.SetActive(true);
                Instantiate(gate, new Vector3(Random.Range(corner1.position.x, corner2.position.x), Random.Range(corner1.position.y, corner2.position.y), 0), Quaternion.identity);
                CameraShaker.Instance.ShakeOnce(10f, 5f, 0.5f, 2f);

            }
            killNum.color = new Color32(255, 255, 255, 255);
            countDown.color = new Color32(0, 0, 0, 0);
            textHelper.text = "Defend the truck!";

        }
        else if (EpochIsGoing && WaitingGate)
        {
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            countDown.color = new Color32(0, 0, 0, 0);
            killNum.color = new Color32(0, 0, 0, 0);
            // GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            textHelper.text = "Destroy the gate with your truck";
        }
        GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
        EpochCounter.text = (7-CurrentEpoch) + " gates left";
    }
    public void CloseGate()
    {
        WaitingGate = false;
        foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(i.gameObject);
        }
        FinishEpoch();
        Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        CameraShaker.Instance.ShakeOnce(10f, 5f, 0.5f, 2f);
        spawner.arrow.SetActive(false);
        killedShadows = 0;
        countDownTime = seconds;
        Invoke(nameof(StartEpoch), seconds);
    }
}
