using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameLoader;

    private Rigidbody rigi;
    [SerializeField] private float speed;
    public GameObject playerPosition;
    [SerializeField] private Vector3 targetPosition;
    void Start()
    {
        gameLoader = GameObject.FindGameObjectWithTag("GameUI");
        rigi = this.GetComponent<Rigidbody>();
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        targetPosition = playerPosition.transform.position;
        targetPosition = new Vector3(playerPosition.transform.position.x, playerPosition.transform.position.y, playerPosition.transform.position.z - 5f);
        speed = (int)Random.Range(6f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);

        if(this.transform.position.z < playerPosition.transform.position.z - 4f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Mothership")
        {
            Debug.Log("Mothership hit");
            gameLoader.GetComponent<GameUI>().coins--;
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!");
            gameLoader.GetComponent<GameUI>().health--;
            Destroy(this.gameObject);
        }
      
    }
}
