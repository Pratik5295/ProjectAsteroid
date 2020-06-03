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
    public float ForwardSpeed;

    public float MinXValue;
    public float MaxXValue;

    public Vector3 startingPosition;
    public GameObject GameManager;


    public float MaxSpeed;

    public bool HasJumped;
    public bool IsShieldOn;
    [SerializeField] private float ShieldTimer = 0;

    void Start()
    {
        HasJumped = false;
        bulletpoint = this.transform.GetChild(0).gameObject;
        startingPosition = this.transform.position;
        ForwardSpeed = 20f;
        crossImage = crosshair.GetComponent<Image>();

        GameManager = GameObject.FindGameObjectWithTag("GameUI");
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID

        if(this.transform.position.x > MinXValue && this.transform.position.x < MaxXValue)
        {
            this.transform.Translate(Input.acceleration.x * 0.8f, 0, ForwardSpeed * Time.deltaTime);
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
            this.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, ForwardSpeed * Time.deltaTime);
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!HasJumped)
            {
                Jump();
            }
            
        }

#endif

        CrosshairDetection();
        ShieldPowerCounter();

        DeathZone();
    }

   
    public void FixedUpdate()
    {
        
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
        if (!IsShieldOn)
        {
            if (collision.gameObject.tag == "BigEnemy")
            {
                GameManager.GetComponent<GameUI>().health--;
                Destroy(this.gameObject, 0.5f);

                GameManager.GetComponent<GameUI>().ReSpawn();
            }
        }

        else
        {
            Destroy(collision.gameObject);
            GameManager.GetComponent<GameUI>().coins++;
        }

        if(collision.gameObject.tag == "Ground")
        {
            HasJumped = false;
        }

        else
        {
            HasJumped = true;
        }
      

        
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            GameManager.GetComponent<GameUI>().coins++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "ShieldPower")
        {
            Debug.Log("Shield power picked up");
            IsShieldOn = true;
        }

        if(other.gameObject.tag == "Goal")
        {
            Debug.Log("Goal reached. Level complete");
            ForwardSpeed = 0;
        }
    }

    public void FiringBullets()
    {
        Debug.Log("Fire button");
        Instantiate(bulletPrefab, bulletpoint.transform.position, Quaternion.identity);
    }


    public void Jump()
    {
        HasJumped = true;
        Debug.Log("Jump selected");
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 70f, ForceMode.Impulse);
    }


    public void DeathZone()
    {
        if(this.transform.position.y < -12f)
        {
            GameManager.GetComponent<GameUI>().health--;
            Debug.Log("Player fell off the grid and died");
            Destroy(this.gameObject);

            GameManager.GetComponent<GameUI>().ReSpawn();
        }
    }

    public void ShieldPowerCounter()
    {
        //Shield power will be on for 10 secs
        if (IsShieldOn)
        {
            ShieldTimer += Time.deltaTime;

            if(ShieldTimer >= 10f)
            {
                IsShieldOn = false;
                ShieldTimer = 0;
            }
        }
    }
}
