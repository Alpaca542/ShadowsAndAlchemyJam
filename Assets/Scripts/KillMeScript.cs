using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Die()
    {
        transform.parent.transform.parent.gameObject.GetComponent<shrederSctipt>().Check();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
