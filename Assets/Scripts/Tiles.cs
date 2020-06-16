using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    // Start is called before the first frame update

    //This script will be used for tile length and deleting the extra tiles


    public float Length;
    public GameObject player;
    public GameObject TileParent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TileParent = GameObject.FindGameObjectWithTag("TileParent");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Delete();
    }

    public void Delete()
    {
        if(player.transform.position.z> this.transform.position.z + Length + 100f)
        {
            TileParent.GetComponent<TileGeneration>().MaxTilesAllowed--;
            Destroy(this.gameObject);
        }
    }
}
