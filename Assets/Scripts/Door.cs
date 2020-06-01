using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string levelNumber;
    public Animator doorAnimController;
    public Text text;
    void Start()
    {
        doorAnimController = gameObject.GetComponent<Animator>();
        text= GameObject.FindWithTag("DoorText").GetComponent<Text>() as Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Player.instance.collectedKeys > 0)
            {


                levelNumber = SceneManager.GetActiveScene().name;
                levelNumber = levelNumber.Remove(0, 6);


                doorAnimController.SetBool("isLocked", false);            
                TextManager.instance.StartCoroutine(TextManager.instance.ShowText(text, "Congratulations !", "", 0.1f));
                Player.instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                StartCoroutine(NextLevel());
            }
            else
            {
                TextManager.instance.StartCoroutine(TextManager.instance.ShowText(text, "You need a key!", "", 0.1f));
            }
            //Player.instance.collectedKeys--;
            //Player.instance.RefreshKeyText(Player.instance.collectedKeys.ToString());
        }
    }
    public IEnumerator NextLevel()
    {
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level_" + (int.Parse(levelNumber) + 1).ToString());
    }
}
