using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    // Start is called before the first frame update

    public float SpawnPoint;
    public GameObject player;

    public int MaxTilesAllowed;

    public GameObject[] tilesPrefab;
    void Start()
    {
        MaxTilesAllowed = 0;
        SpawnPoint = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (MaxTilesAllowed <= 5)
        {
            TilesSpawning();
            Debug.Log("Spawn");
        }
        else
        {
            Debug.Log("Limit reached");
        }
        
    }

    public void TilesSpawning()
    {
        int value = Random.Range(0, tilesPrefab.Length);
        Instantiate(tilesPrefab[value], new Vector3(this.transform.position.x, this.transform.position.y, SpawnPoint), Quaternion.identity);
        SpawnPoint = SpawnPoint + tilesPrefab[value].GetComponent<Tiles>().Length;
        MaxTilesAllowed = MaxTilesAllowed + 1;
    }
}
