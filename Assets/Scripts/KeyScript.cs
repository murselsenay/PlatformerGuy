using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public bool Move = false;    ///gives you control in inspector to trigger it or not
    public bool Fall = false;
    public Vector3 MoveVector = Vector3.up; //unity already supplies us with a readonly vector representing up and we are just chaching that into MoveVector
    public float MoveRange = 2.0f; //change this to increase/decrease the distance between the highest and lowest points of the bounce
    public float MoveSpeed = 10f; //change this to make it faster or slower
    private Vector2 target;
    public float targetX, targetY;
    public static KeyScript instance;
    public bool isActive;
    private KeyScript bounceKey; //for caching this transform
    Vector3 startPosition; //used to cache the start position of the transform
    void Start()
    {
        
        bounceKey = this;
        startPosition = bounceKey.transform.position;
        target = new Vector2(targetX, targetY);
        if (instance != null)
        {
            Debug.Log("More than one TextManager in the scene");
        }
        else
        {
            instance = this;
        }
      
    }
    void Update()
    {
        if (Move)
        {
            Fall = false;
            bounceKey.transform.position = startPosition + MoveVector * (MoveRange * Mathf.Sin(Time.timeSinceLevelLoad * MoveSpeed));
            
        }


        if (Fall)
        {
            Move = false;
            bounceKey.transform.position = Vector2.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);

        }
 
    }
}
