using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class SpecialWeapons : MonoBehaviour
{
    public Transform firePoint;
    public GameObject flashPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(flashPrefab, firePoint.position, firePoint.rotation);
    }
}
