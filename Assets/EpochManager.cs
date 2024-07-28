using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EpochManager : MonoBehaviour
{
    public TMP_Text countDown;
    public TMP_Text killNum;
     int countDownTime;

    public int killedShadows = 0;
    public int NeedToKill = 0;
    public int seconds =90;
    public int CurrentEpoch=0;

    bool EpochIsGoing = false;


    public int[] killsPerEpoch;
    private void Start()
    {
        SetTimer();
    }
    public void SetTimer()
    {
        countDownTime = seconds;
        InvokeRepeating(nameof(countDownOne), 1f,1f);
        killedShadows = 0;
    }
    void countDownOne()
    {
        countDownTime -= 1;
        if (countDownTime == 0)
        {
            CancelInvoke(nameof(countDown));
            StartEpoch();
            
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
        if(!EpochIsGoing)
        {
            countDown.text = (countDownTime / 60).ToString() + ":" + (countDownTime - ((countDownTime / 60) * 60)).ToString();
        }
        else
        {
            killNum.text = killedShadows.ToString()+"/"+NeedToKill.ToString();
            if(killedShadows == NeedToKill )
            {
                Invoke(nameof(FinishEpoch), 1f);
            }
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnShops();
            GameObject.FindWithTag("Spawner").GetComponent<Spawner>().SpawnEnemies();
        }
        
    }
}
