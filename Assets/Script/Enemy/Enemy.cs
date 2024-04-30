using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float EnemyCurrenthealth;
    public float Maxhealth;

    public float Damage;
    public float DamgeDelayTime;
    private float distanceToTarget;

    private bool canTakeDamage = true; // Hasar alabilirlik durumu
    private Coroutine damageCooldownCoroutine; // Hasar al�nd�ktan sonra beklenen s�re


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
        canTakeDamage = true;

    }


    void Update()
    {
        // Karakter ile d��man aras�ndaki mesafeyi hesapla
         distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Karakteri takip etme
        if (distanceToTarget <= chaseRange && distanceToTarget > stopRange)
        {
            // D��man� karaktere do�ru hareket ettir
            Vector2 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;

            // D��man� karakterin oldu�u tarafa d�nd�r
            transform.right = moveDirection;

            _anim.SetBool("Walk", true);
            // D��man� karakterin oldu�u tarafa d�nd�r
            if (moveDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (moveDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            // D��man hareket etmez
            rb.velocity = Vector2.zero;
            _anim.SetBool("Walk", false);
        }

        Attacked();
    }

    private void Attacked()
    {
        if (canTakeDamage && distanceToTarget <= attackRange)
        {


            var player = target.GetComponent<PlayerMovement>();
            if (player)
            {
                StartCoroutine(StartDamageCooldown());
                player.PlayerTakeDamage(Damage);
                _anim.SetBool("Walk", false);
                _anim.SetTrigger("Attack");
                
            }
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
        _anim.SetTrigger("Hit");
        health.UpdateHealthBar(EnemyCurrenthealth, Maxhealth);

        if (EnemyCurrenthealth <= 0)
        {
            canTakeDamage = false;
        Destroy(this.gameObject);

        }
    }
    private IEnumerator StartDamageCooldown()
    {
        canTakeDamage = false; 
        yield return new WaitForSeconds(DamgeDelayTime); 
        canTakeDamage = true; 
    }
}
