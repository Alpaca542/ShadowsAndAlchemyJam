using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeScript : MonoBehaviour
{
    public void Die()
    {
        GetComponent<soundManager>().PlaySound(0, 0.7f, 1.3f);
        transform.parent.transform.parent.gameObject.GetComponent<shrederSctipt>().Check();
        Destroy(gameObject);
    }
}
