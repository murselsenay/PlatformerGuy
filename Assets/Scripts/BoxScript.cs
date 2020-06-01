using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Rigidbody2D boxBody;
    public ParticleSystem draggingParticle;
    // Start is called before the first frame update
    void Start()
    {
        boxBody = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (boxBody.velocity.x != 0)
        {
            draggingParticle.Play();
        }
        else
        {
            draggingParticle.Stop();
        }
    }
}
