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
    public float attackRange = 0.5f; // Sald�rma mesafesi
    public float moveSpeed = 3f; // D��man�n hareket h�z�

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
        // Karakter ile d��man aras�ndaki mesafeyi hesapla
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Karakteri takip etme
        if (distanceToTarget <= chaseRange && distanceToTarget > stopRange)
        {
            // D��man� karaktere do�ru hareket ettir
            Vector2 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
        }
        else
        {
            // D��man hareket etmez
            rb.velocity = Vector2.zero;
        }

        // Sald�rma mesafesine gelindi�inde
        if (distanceToTarget <= attackRange)
        {
            // Burada sald�rma kodu yaz�labilir veya istenilen i�lem yap�labilir.
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

        // Sald�rma mesafesi
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
