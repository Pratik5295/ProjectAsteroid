using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject bulletpoint;
    public GameObject bulletPrefab;

    public GameObject crosshair;
    private Image crossImage;
    public float speed;

    public float MinXValue;
    public float MaxXValue;

    public Vector3 startingPosition;
    public GameObject GameManager;

    void Start()
    {
        bulletpoint = this.transform.GetChild(0).gameObject;
        startingPosition = this.transform.position;
        crossImage = crosshair.GetComponent<Image>();

        GameManager = GameObject.FindGameObjectWithTag("GameUI");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID

        if(this.transform.position.x > MinXValue && this.transform.position.x < MaxXValue)
        {
            this.transform.Translate(Input.acceleration.x * 0.8f, 0, 0);
        }
        if (this.transform.position.x < MinXValue)
        {
            this.transform.position = new Vector3(MinXValue + 0.5f, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > MaxXValue)
        {
            this.transform.position = new Vector3(MaxXValue - 0.5f, this.transform.position.y, this.transform.position.z);
        }


        

        
#endif
#if UNITY_EDITOR

        if(this.transform.position.x > MinXValue && this.transform.position.x < MaxXValue)
        {
            this.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime * speed, 4f * Time.deltaTime);
        }
       
        if(this.transform.position.x < MinXValue)
        {
            this.transform.position = new Vector3(MinXValue + 0.5f, this.transform.position.y, this.transform.position.z);
        }

        if(this.transform.position.x > MaxXValue)
        {
            this.transform.position = new Vector3(MaxXValue - 0.5f, this.transform.position.y, this.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletpoint.transform.position, Quaternion.identity);
        }

       
#endif

        CrosshairDetection();
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Jump();
        }
    }

    public void CrosshairDetection()
    {
        RaycastHit hit;

        if(Physics.Raycast(bulletpoint.transform.position,Vector3.forward,out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Enemy")
            {
                //Change the color of the crosshair;
                crossImage.color = Color.red;
            }

            else if(hit.collider.gameObject.tag == "BigEnemy")
            {
                crossImage.color = Color.blue;
            }
        }

        else
        {
            //Change the color back to normal
            crossImage.color = Color.yellow;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BigEnemy")
        {
            GameManager.GetComponent<GameUI>().health--;
            Destroy(this.gameObject, 0.5f);

            GameManager.GetComponent<GameUI>().ReSpawn();
        }

        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            GameManager.GetComponent<GameUI>().coins++;
            Destroy(other.gameObject);
        }
    }

    public void FiringBullets()
    {
        Instantiate(bulletPrefab, bulletpoint.transform.position, Quaternion.identity);
    }


    public void Jump()
    {
        Debug.Log("Jump selected");
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 80f, ForceMode.Impulse);
    }
}
