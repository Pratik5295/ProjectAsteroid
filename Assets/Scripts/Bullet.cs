using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameLoader;
    private Rigidbody rigi;

    private float speed = 20f;

    [SerializeField] private float playerSpeed;

    void Start()
    {
        gameLoader = GameObject.FindGameObjectWithTag("GameUI");
        rigi = this.GetComponent<Rigidbody>();
        playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ForwardSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = new Vector3(0, 0, playerSpeed + speed);

        Destroy(this.gameObject, 6f);
       
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit!");
            gameLoader.GetComponent<GameUI>().coins++;
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
