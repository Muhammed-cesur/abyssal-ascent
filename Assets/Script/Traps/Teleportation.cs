using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{


    public float TeleportationRange;
    public float DelayTime;
    public GameObject Player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            StartCoroutine(Delay());
           
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(DelayTime);
        Player.transform.position = new Vector2(transform.position.x + TeleportationRange, transform.position.y);
    }
}
