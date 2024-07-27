using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalScript : MonoBehaviour
{
    public void SetBomb()
    {
        gameObject.tag = "Untagged";
        gameObject.GetComponent<Brewer>().myText.SetActive(false);
        gameObject.GetComponent<Brewer>().enabled = false;
    }
}
