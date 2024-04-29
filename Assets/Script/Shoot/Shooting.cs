using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Shooting : MonoBehaviour
{
    public Transform ShootPoint;
    public GameObject Bullet;
   
    [SerializeField] private float speed;
    private bool canClick = true;
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
    _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            StartCoroutine(Delay());
            
        }

    }

    IEnumerator Delay()
    {
        canClick = false;
        yield return new WaitForSecondsRealtime(.1f);
        _anim.SetTrigger("Shoot");
        yield return new WaitForSecondsRealtime(.5f);
        var shoot = Instantiate(Bullet, ShootPoint.position, transform.rotation);
        Debug.Log("dsadas");
        
        shoot.GetComponent<Rigidbody2D>().velocity = ShootPoint.right * speed;

        canClick = true;
        
    }

}
