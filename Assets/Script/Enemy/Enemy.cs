using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float EnemyCurrenthealth;
    public float Maxhealth;

    private Health health;
    private Animator _anim;

    public Transform target; // Karakterin transformu
    public float chaseRange = 5f; // Karakteri takip etme mesafesi
    public float stopRange = 1f; // Durma mesafesi
    public float attackRange = 0.5f; // Saldýrma mesafesi
    public float moveSpeed = 3f; // Düþmanýn hareket hýzý

    private Rigidbody2D rb;
  



    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        rb = GetComponent<Rigidbody2D>();
       
    }


    void Update()
    {
        // Karakter ile düþman arasýndaki mesafeyi hesapla
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Karakteri takip etme
        if (distanceToTarget <= chaseRange && distanceToTarget > stopRange)
        {
            // Düþmaný karaktere doðru hareket ettir
            Vector2 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // Düþman hareket etmez
            rb.velocity = Vector2.zero;
        }

        // Saldýrma mesafesine gelindiðinde
        if (distanceToTarget <= attackRange)
        {
            // Burada saldýrma kodu yazýlabilir veya istenilen iþlem yapýlabilir.
        }
    }

    void OnDrawGizmos()
    {
        // Takip mesafesi
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Durma mesafesi
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopRange);

        // Saldýrma mesafesi
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
