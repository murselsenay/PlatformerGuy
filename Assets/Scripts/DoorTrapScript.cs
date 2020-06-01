using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrapScript : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public bool upToDown;
    public bool downToUp;
    public bool upToDownToUp;
    public float speed = 0.1f;
    public bool reachTheTop;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (upToDown)
        {
            if (!TrapButton.instance.isPressed)
            {
                if (gameObject.transform.position.y < startPosition.y)
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            }
            else
            {
                if (gameObject.transform.position.y > endPosition.y)
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - speed);
            }
        }
        if (downToUp)
        {
            if (!TrapButton.instance.isPressed)
            {
                if (gameObject.transform.position.y > startPosition.y)
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - speed);
            }
            else
            {
                if (gameObject.transform.position.y < endPosition.y)
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + speed);
            }
        }
        if (upToDownToUp)
        {
            if (TrapButton.instance.isPressed)
            {
                if (reachTheTop)
                {
                    if (gameObject.transform.position.y > startPosition.y)
                        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - speed);
                }
                else
                {
                    if (gameObject.transform.position.y < endPosition.y)
                        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + speed);
                }

                if (gameObject.transform.position.y>=endPosition.y)
                {
                    reachTheTop = true;
                }
                else if (gameObject.transform.position.y <= startPosition.y)
                {
                    reachTheTop = false;
                }



            }



        }



    }

}
