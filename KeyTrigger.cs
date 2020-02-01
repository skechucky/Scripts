using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public bool keyOnMap;
    public float X1;
    public float X2;
    public float Y;

    public ParticleSystem particle;
    public GameObject KeyPrefab;

    void Start()
    {
        keyOnMap = true;
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (keyOnMap == true)
            {
                keySpawn();
                particle.gameObject.SetActive(false);
            }
        }
    }

    private void keySpawn()
    {
        Vector3 position =new Vector2(Random.Range(X1,X2),Y);
        Instantiate(KeyPrefab, position, Quaternion.identity);
        keyOnMap = false;
    }

}
