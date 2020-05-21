using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,player.transform.position.z - offset.z);
    }
}
