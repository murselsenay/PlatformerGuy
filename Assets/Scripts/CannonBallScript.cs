using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{

    Rigidbody2D cannonBallBody;
    // Start is called before the first frame update
    void Start()
    {
        cannonBallBody = gameObject.GetComponent<Rigidbody2D>();
        cannonBallBody.AddForce(transform.right * -20f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
}
