using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;
    public bool canShake;
    private float temp_shake_intensity = 0;
    public Text text;
    public bool canSpawn = true;
    public Transform fallPoint;
    public GameObject keyPrefab;
    GameObject insKeyPrefab;
    
    /*void OnGUI() //button creating
    {
        if (GUI.Button(new Rect(20, 40, 80, 20), "Shake"))
        {
            Shake();
        }
    }*/
    void Start()
    {

        
        text = GameObject.FindWithTag("HintText").GetComponent<Text>() as Text;
        TextManager.instance.StartCoroutine(TextManager.instance.FixedShowText(text, "sometimes, you should hit somethings to get your prize !", "", 0.05f));
        StartCoroutine(canShakeEnable());
    }
    void Update()
    {
       
        if (temp_shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }
    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Flash" && canShake)
        {
            Shake();
            canShake = false;
            if (keyPrefab!=null)
            { 
            if (canSpawn)
            {

                insKeyPrefab= Instantiate(keyPrefab, fallPoint.position, fallPoint.rotation);
                insKeyPrefab.GetComponent<KeyScript>().Fall = true;
                canSpawn = false;
            }
            }
        }
    }

    public IEnumerator canShakeEnable()
    {
        while (true)
        {
            if (!canShake)
            {
                canShake = true;
            }
            yield return new WaitForSeconds(1.2f);
            
        }

    }
}
