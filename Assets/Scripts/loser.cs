using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loser : MonoBehaviour
{
    public void lose()
    {
        Invoke(nameof(loadlose), 2f);
    }
    public void win()
    {
        Invoke(nameof(loadwin), 2f);
    }

    void loadwin()
    {
        SceneManager.LoadScene("win");
    }
    void loadlose()
    {
        SceneManager.LoadScene("lose");
    }
}
