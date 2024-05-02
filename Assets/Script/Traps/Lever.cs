using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    private Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ) 
        {
            StartCoroutine(LeverD());
        }
    }
    IEnumerator LeverD()
    {
        yield return new WaitForSecondsRealtime(1f);
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("Lever");
        Debug.Log("lever");
    }
}
