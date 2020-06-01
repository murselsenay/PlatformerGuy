using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    Rigidbody2D sawBody;
    bool changeDirection;
    public float sawSpeed;
    float startPositionX;
    float startPositionY;
    public float horizontalTargetPosition;
    public float verticalTargetPosition;
    public bool horizontalMovement;

    void Start()
    {
        sawBody = gameObject.GetComponent<Rigidbody2D>();
        startPositionX = gameObject.transform.position.x;
        startPositionY = gameObject.transform.position.y;
        horizontalTargetPosition = gameObject.transform.position.x + horizontalTargetPosition;
        verticalTargetPosition = gameObject.transform.position.y + verticalTargetPosition;
    }


    void FixedUpdate()
    {

        if (horizontalMovement)
        {
            SwitchOnSaw(horizontalTargetPosition, sawSpeed);
        }
        else if (!horizontalMovement)
        {
            SwitchOnSaw(verticalTargetPosition, sawSpeed);
        }

        


    }

    public void SwitchOnSaw(float targetPosition_, float sawSpeed_)
    {

        if (horizontalMovement)
        {
            if (changeDirection)
            {
                if (gameObject.transform.position.x < targetPosition_)
                {
                    transform.Translate(Vector3.right * sawSpeed_ * Time.deltaTime);
                }
                else
                {
                    changeDirection = false;
                }
            }
            else if (!changeDirection)
            {
                if (gameObject.transform.position.x > startPositionX)
                {
                    transform.Translate(Vector3.right * -sawSpeed_ * Time.deltaTime);
                }
                else
                {
                    changeDirection = true;
                }
            }
        }
        else if (!horizontalMovement)
        {
            if (changeDirection)
            {
                if (gameObject.transform.position.y < targetPosition_)
                {
                    transform.Translate(Vector3.up * sawSpeed_ * Time.deltaTime);
                }
                else
                {
                    changeDirection = false;
                }
            }
            else if (!changeDirection)
            {
                if (gameObject.transform.position.y > startPositionY)
                {
                    transform.Translate(Vector3.up * -sawSpeed_ * Time.deltaTime);
                }
                else
                {
                    changeDirection = true;
                }
            }
        }

        
    }
}
