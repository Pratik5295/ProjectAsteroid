using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] enemyPrefab;

    public float SpawnerTimer = 4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnerTimer -= Time.deltaTime;

        if(SpawnerTimer <= 0)
        {
            int value = (int)Random.Range(0, 3);
            Instantiate(enemyPrefab[value], this.transform.position, this.transform.rotation);
            SpawnerTimer = (float)Random.Range(3f, 5f);     //Randomly spawn between 3 to 5secs

        }


        this.transform.Translate(new Vector3(0, 0, 4f * Time.deltaTime));
    }
}
