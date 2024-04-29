using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float DestroyTime;
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void Update()
    {
        Destroy(this.gameObject, DestroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     var enemy = collision.collider.GetComponent<Enemy>();

        if (enemy) 
        {
            enemy.EnemyTakeDamage(damage);
            Destroy(gameObject);

        }
    }

}
