using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLevel : MonoBehaviour {
   
    private int ok;

    public GameObject firePrefab;

    private GameObject player;


	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ok = 0;
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ok == 0)
            {
                StartCoroutine(wait());
                ok = 1;
            }
        }
    }

    private IEnumerator wait()
    {
        Destroy(player);
        Instantiate(firePrefab,transform.position, transform.rotation);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }


}
