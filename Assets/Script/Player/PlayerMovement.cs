using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public float PlayerCurrenthealth;
    public float Maxhealth;
    private Health health;


    [SerializeField] private float speed;
    private float horizontal;
    private Animator _anim;

    public Transform GroundCheck;
    public LayerMask Ground;
  
    public bool Isgrounded;

    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Isgrounded = Physics2D.OverlapCircle(GroundCheck.position,.1f, Ground);

        if (Isgrounded && Input.GetKeyUp(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 5f);
            _anim.SetTrigger("Jump");
        }
    }
    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(horizontal * speed, _rigidbody2D.velocity.y);

        if (horizontal>=0.1f || horizontal <=-0.1)
        {
            _anim.SetBool("Run", true);
        }
        else
        {
            _anim.SetBool("Run", false);

        }
        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        PlayerCurrenthealth -= damage;
        //_anim.SetTrigger("Hit");
        health.UpdateHealthBar(PlayerCurrenthealth, Maxhealth);

        if (PlayerCurrenthealth <= 0)
        {
            // _anim.SetTrigger("Die");
            Invoke(nameof(DestroyPlayer), 2f);
        }
    }
    void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }


}
