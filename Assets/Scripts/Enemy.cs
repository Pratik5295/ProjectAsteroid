using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameLoader;
    private Rigidbody rigi;

    public GameObject explosionPrefab;
    [SerializeField]private float speed;
    void Start()
    {
        rigi = this.GetComponent<Rigidbody>();
        gameLoader = GameObject.FindGameObjectWithTag("GameUI");
        speed = (int)Random.Range(-7f, -10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = new Vector3(0, 0, speed);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Mothership")
        {
            Debug.Log("Mothership hit");
            gameLoader.GetComponent<GameUI>().coins--;
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!");
            gameLoader.GetComponent<GameUI>().health--;
            Destroy(this.gameObject);
        }
     
    }


    public void Destruction()
    {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
