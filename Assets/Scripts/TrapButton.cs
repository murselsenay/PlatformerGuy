using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{

    public static TrapButton instance;
    SpriteRenderer trapButtonSprite;
    public Sprite pressedButton;
    public Sprite nonPressedButton;
    public bool isPressed;
    GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        trapButtonSprite = gameObject.GetComponent<SpriteRenderer>();
        trapButtonSprite.sprite = nonPressedButton;
        buttons = GameObject.FindGameObjectsWithTag("PlatformButton");
    }

    // Update is called once per frame
    void Update()
    {




    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isPressed = true;
        trapButtonSprite.sprite = pressedButton;
        if (isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = true;
            }
        }
        else if (!isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = false;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        isPressed = true;
        trapButtonSprite.sprite = pressedButton;
        if (isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = true;
            }
        }
        else if (!isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isPressed = false;
        trapButtonSprite.sprite = nonPressedButton;
        if (isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = true;
            }
        }
        else if (!isPressed)
        {
            foreach (GameObject button in buttons)
            {
                button.GetComponent<TrapButton>().isPressed = false;
            }
        }
    }
}
