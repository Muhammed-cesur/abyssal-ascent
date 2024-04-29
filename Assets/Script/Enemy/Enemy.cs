using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyCurrenthealth;
    public float Maxhealth;

    private Health health;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }


    public void EnemyTakeDamage(float damage)
    {
        EnemyCurrenthealth -= damage;
        //_anim.SetTrigger("Hit");
        health.UpdateHealthBar(EnemyCurrenthealth, Maxhealth);

        if (EnemyCurrenthealth <= 0)
        {
           // _anim.SetTrigger("Die");
            Invoke(nameof(DestroyEnemy), 2f);
        }
    }
    void DestroyEnemy () 
    {
        Destroy(this.gameObject);
    }
}
