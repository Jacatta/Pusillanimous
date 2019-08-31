using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDetectAndReset : MonoBehaviour
{
    ParticleSystem ps;
    ScoreKeeper SK;
    ItemEmitter IE;
    // Start is called before the first frame update
    void Start()
    {
        IE = FindObjectOfType<ItemEmitter>();
        SK = FindObjectOfType<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // IE.ResetItem(other.gameObject);
        Debug.Log("missed out on a somfin");
        if (other.tag=="Collectable")
        {
            Debug.Log("missed out on a coin");
            SK.streak = 0;
            //Play Waaa' Waa- missed Sound
        }
    }



    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit Something");
        IE.ResetItem(collision.gameObject);
    }
    

}
