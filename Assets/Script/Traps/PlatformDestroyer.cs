using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public float DelayTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player") 
        
        {
            StartCoroutine(Delay());
        }
    }


    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(DelayTime);
        Destroy(this.gameObject);
    }
}
