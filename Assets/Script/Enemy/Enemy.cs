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
    private Coroutine damageCooldownCoroutine; // Hasar alýndýktan sonra beklenen süre


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
        canTakeDamage = true;

    }


    void Update()
    {
        // Karakter ile düþman arasýndaki mesafeyi hesapla
         distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Karakteri takip etme
        if (distanceToTarget <= chaseRange && distanceToTarget > stopRange)
        {
            // Düþmaný karaktere doðru hareket ettir
            Vector2 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;

            // Düþmaný karakterin olduðu tarafa döndür
            transform.right = moveDirection;

            _anim.SetBool("Walk", true);
            // Düþmaný karakterin olduðu tarafa döndür
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
            // Düþman hareket etmez
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

        // Saldýrma mesafesi
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
