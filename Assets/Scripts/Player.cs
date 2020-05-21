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
    void Start()
    {
        bulletpoint = this.transform.GetChild(0).gameObject;

        crossImage = crosshair.GetComponent<Image>();
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

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Instantiate(bulletPrefab, bulletpoint.transform.position, Quaternion.identity);
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
        }

        else
        {
            //Change the color back to normal
            crossImage.color = Color.yellow;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
    }
}
