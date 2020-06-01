using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    float speed = 5f;
    public Animator birdAnimatorController;
    public GameObject birdStartSpot;
    public GameObject birdEndSpot;
    public bool canFly;
    public bool isArrivedEnd;
    public bool isArrivedStart;
    public static Bird instance;
    public GameObject generalDeathPrefab;
    public GameObject keyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isArrivedEnd = false;
        isArrivedStart = true;
        birdAnimatorController = gameObject.GetComponent<Animator>();
        birdStartSpot = GameObject.Find("BirdStartSpot");
        birdEndSpot = GameObject.Find("BirdEndSpot");
        transform.position = birdStartSpot.transform.position;
        StartCoroutine(CheckPlayer());

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (BirdSpotScript.instance.isPlayerDetected)
            canFly = true;

        if (canFly)
        {
            if (BirdSpotScript.instance.playerEnterExitSituation==1 && !isArrivedEnd)
            {
                transform.position = Vector2.MoveTowards(transform.position, birdEndSpot.transform.position, step);
                birdAnimatorController.SetBool("canFly", true);
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
           else if (BirdSpotScript.instance.playerEnterExitSituation == 2 || BirdSpotScript.instance.isPlayerDetected==false)
            {
                transform.position = Vector2.MoveTowards(transform.position, birdStartSpot.transform.position, step);
                birdAnimatorController.SetBool("canFly", true);
                transform.localScale= new Vector2(1, transform.localScale.y);
                isArrivedEnd = false;
            }

        }
        else
        {
            birdAnimatorController.SetBool("canFly", false);
        }
        if (isArrivedEnd || isArrivedStart)
        {
            birdAnimatorController.SetBool("canFly", false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BirdEndSpot")
        {
            canFly = false;
            isArrivedEnd = true;
        }
        if (other.gameObject.tag=="BirdStartSpot")
        {
            isArrivedStart = true;
        }
        if (other.gameObject.tag=="Box")
        {
            Instantiate(generalDeathPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(keyPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

        }
    }
   

    public IEnumerator CheckPlayer()
    {
        while (true)
        {
            if (isArrivedEnd && !canFly)
            {
                BirdSpotScript.instance.playerEnterExitSituation = 2;
                canFly = true;

            }
            yield return new WaitForSeconds(1);
        }
        
    }

}
