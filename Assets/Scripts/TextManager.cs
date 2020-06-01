using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    public static TextManager instance;



    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one TextManager in the scene");
        }
        else
        {
            instance = this;
        }

    }


    void Start()
    {

    }
    public IEnumerator ShowText(Text text, string fullText, string currentText, float delay)
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            text.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1);
        text.GetComponent<Text>().text = "";
    }

    public IEnumerator FixedShowText(Text text, string fullText, string currentText, float delay)
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            text.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

}
