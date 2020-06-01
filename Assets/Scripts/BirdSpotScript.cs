using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpotScript : MonoBehaviour
{

    public bool isPlayerDetected;
    public static BirdSpotScript instance;
    public int playerEnterExitSituation;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            isPlayerDetected = true;
            playerEnterExitSituation = 1;
            Bird.instance.isArrivedStart = false;
        }
        if (other.gameObject.tag=="Bird")
        {

            Bird.instance.isArrivedEnd = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
            playerEnterExitSituation = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Bird.instance.isArrivedEnd)
            {
                
                playerEnterExitSituation = 2;
            }
            isPlayerDetected = false;

        }
    }
}
