using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Animator cannonAnimController;
    public bool canFire = true;
    public GameObject cannonFirePoint;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(canFireTimer());
        cannonAnimController = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (canFire)
            {
                cannonAnimController.SetBool("isFired", true);
                Instantiate(cannonBallPrefab, cannonFirePoint.transform.position, cannonFirePoint.transform.rotation);
                canFire = false;
            }

        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           
                cannonAnimController.SetBool("isFired", false);
            

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            cannonAnimController.SetBool("isFired", false);


        }

    }
    public IEnumerator canFireTimer()
    {
        while (true)
        {
            if (!canFire)
            {
                canFire = true;
            }
            yield return new WaitForSeconds(5);
        }
    }
}
