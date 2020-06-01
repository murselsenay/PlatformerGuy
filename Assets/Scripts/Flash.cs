using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyFlash());
    }

   public IEnumerator DestroyFlash()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
