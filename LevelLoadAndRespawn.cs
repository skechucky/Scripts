using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadAndRespawn : MonoBehaviour
{

    public int nextLevel;

    void Update()
    {
        if (Gate.canPassGate == true)
        {
            StartCoroutine(AfterSecondsNextLevel());
            Gate.canPassGate = false;
        }

        if (Player.isDead == true)
        {
            StartCoroutine(AfterSeconds());
        }

        if (nextLevel == 6)
        {
            Key.level = 5; // new power on lvl 5
        }
    }


    private IEnumerator AfterSeconds()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
        SceneManager.LoadScene(nextLevel - 1);
    }

    private IEnumerator AfterSecondsNextLevel()
    {
        yield return new WaitForSeconds(0.09f);
        SceneManager.LoadScene(nextLevel);
    }

}

