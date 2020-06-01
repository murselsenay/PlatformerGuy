using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{

    public Image[] ButtonBackground;
    public float TransparencyRate;
    public Text text;
    public GameObject canvas;
    public Text instantiatedText;
    string levelNumber;
    

    // Start is called before the first frame update
    void Start()
    {

        levelNumber = SceneManager.GetActiveScene().name;
        levelNumber = levelNumber.Remove(0, 6);
        Application.targetFrameRate = 60;
        Screen.SetResolution(1024, 576, true, 60);
        canvas = GameObject.Find("Canvas");
        instantiatedText=Instantiate(text, GameObject.FindGameObjectWithTag("LevelSpawnPoint").transform.position, GameObject.FindGameObjectWithTag("LevelSpawnPoint").transform.rotation);
        instantiatedText.transform.SetParent(canvas.transform);
        instantiatedText.text = "Level " + levelNumber;
        Destroy(instantiatedText, 2);
    }

    // Update is called once per frame
    void Update()
    {
        SetTransparency(TransparencyRate);
        if (CrossPlatformInputManager.GetButtonDown("retry"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

       



    }



    

    public void SetTransparency(float transparency)
    { //transparency is a value in the [0,1] range
        for (int i = 0; i < ButtonBackground.Length; i++)
        {
            Color color = ButtonBackground[i].color;
            color.a = transparency;
            ButtonBackground[i].color = color;
        }

    }

}
