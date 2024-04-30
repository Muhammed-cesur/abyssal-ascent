using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{


    public float TeleportationRangeX;
    public float TeleportationRangeY;
    public float DelayTime;
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")

        {
            StartCoroutine(Delay());

        }
    }
 
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(DelayTime);
        Player.transform.position = new Vector2(transform.position.x + TeleportationRangeX, transform.position.y+TeleportationRangeY);
    }
}
