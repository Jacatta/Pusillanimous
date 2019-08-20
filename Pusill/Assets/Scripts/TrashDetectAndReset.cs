using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDetectAndReset : MonoBehaviour
{
    ParticleSystem ps;
    ItemEmitter IE;
    // Start is called before the first frame update
    void Start()
    {
        IE = FindObjectOfType<ItemEmitter>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // IE.ResetItem(other.gameObject);
    }



    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Something");
        IE.ResetItem(collision.gameObject);
    }
    */

}
