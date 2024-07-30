using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EpochManager : MonoBehaviour
{
    public GameObject gate;
    public TMP_Text countDown;
    public TMP_Text killNum;
    public TMP_Text textHelper;
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
    private void Start()
    {
        SetTimer();
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
        if (countDownTime == 0)
        {
            CancelInvoke(nameof(countDown));
            StartEpoch();
            countDown.text = (countDownTime / 60).ToString() + ":" + (countDownTime - ((countDownTime / 60) * 60)).ToString();
        }
    }
    void StartEpoch()
    {
        CurrentEpoch += 1;
        EpochIsGoing = true;
        NeedToKill = killsPerEpoch[CurrentEpoch - 1];
    }
    void FinishEpoch()
    {
        EpochIsGoing = false;
        SetTimer();
    }
    void Update()
    {
        if (!EpochIsGoing && !WaitingGate)
        {
            countDown.text = (countDownTime / 60).ToString() + ":" + (countDownTime - ((countDownTime / 60) * 60)).ToString();
            textHelper.text = "Shadows are coming! Cook or buy crystals!";
            killNum.color = new Color(0,0,0,0);
            countDown.color = Color.white;
        }
        else if (EpochIsGoing && !WaitingGate)
        {
            killNum.text = killedShadows.ToString() + "/" + NeedToKill.ToString();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            if (killedShadows >= NeedToKill)
            {
                WaitingGate = true;
                Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
                CameraShaker.Instance.ShakeOnce(10f, 5f, 0.5f, 2f);
                spawner.arrow.SetActive(true);
                Instantiate(gate, new Vector3(Random.Range(corner1.position.x, corner2.position.x), Random.Range(corner1.position.y, corner2.position.y), 0), Quaternion.identity);

            }
            killNum.color = Color.white;
            countDown.color = new Color(0, 0, 0, 0);
            textHelper.text = "Defend the track!";

        }
        else if (EpochIsGoing && WaitingGate)
        {





            //spawner.HowManyEnemiesKilled = 0;
            //GateInstance

            // spawner.arrow.SetActive(true);

            countDown.color = new Color(0, 0, 0, 0);
           killNum.color = new Color(0, 0, 0, 0);
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
            textHelper.text = "Destroy the gate!";
        }


    }
    public void CloseGate()
    {
        WaitingGate = false;
        FinishEpoch();
        Spawner spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        CameraShaker.Instance.ShakeOnce(10f, 5f, 0.5f, 2f);
        spawner.arrow.SetActive(false);
        killedShadows = 0;
    }
}
