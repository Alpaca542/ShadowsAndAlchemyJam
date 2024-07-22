using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeScript : MonoBehaviour
{
    public void Die()
    {
        transform.parent.transform.parent.gameObject.GetComponent<shrederSctipt>().Check();
        Destroy(gameObject);
    }
}
