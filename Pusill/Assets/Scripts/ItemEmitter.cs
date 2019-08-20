using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEmitter : MonoBehaviour {


    public ParticleSystem T_System;
    //BackgroundBehaviors BB;
    GameManager GM;

    //ParticleSystem m_System;
    GameObject[] trash;
    float baseVelocity;
    Vector3 resetPosition;
    int indexer;
    public string OriginalTag = "";

    public int numberOfKnownTrash;
    // Use this for initialization
    void Start () {
        GM = FindObjectOfType<GameManager>();
       // BB = FindObjectOfType<BackgroundBehaviors>();
        trash = GameObject.FindGameObjectsWithTag("Trash");
        resetPosition = trash[0].transform.position;
        baseVelocity = -1000;
        indexer = 0;
        StartCoroutine(ReleaseItemDelay());
    }

    void CallBackFunction(Collider2D other)
    {
        Debug.Log("HitSomething at: " + other.transform.position.x);
    }

    // Update is called once per frame
    void Update () {

        
    }

    private void LateUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int tempIndex=0;
        switch(other.name)
        {
            case "Bottle":
                tempIndex = 0;
                 break;
            case "Bottle (1)":
                tempIndex = 1;
                break;
            case "Bottle (2)":
                tempIndex = 2;
                break;
            case "Bottle (3)":
                tempIndex = 3;
                break;
            case "Bottle (4)"://ISSUES
                tempIndex = 0;
                break;
            default:
                tempIndex = 0;

                break;
        }
        Debug.Log("other = " +  other.name);

        Vector3 ScreenPoint = Camera.main.WorldToScreenPoint(other.transform.position);

        if(GM.Alerts != null)
        {
            GM.Alerts[tempIndex].SetActive(true);
        }

        
        GM.Alerts[tempIndex].transform.position = new Vector3(ScreenPoint.x - 160f, GM.Alerts[tempIndex].transform.position.y, GM.Alerts[tempIndex].transform.position.z);

        // Debug.Log("HitSomething at: " + other.transform.position.x);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int tempIndex = 0;
        if (collision.name == "TrashDetector")
        {
            switch (collision.name)
            {
                case "Bottle":
                    tempIndex = 0;
                    break;
                case "Bottle (1)":
                    tempIndex = 1;
                    break;
                case "Bottle (2)":
                    tempIndex = 2;
                    break;
                case "Bottle (3)":
                    tempIndex = 3;
                    break;
                case "Bottle (4)"://ISSUES
                    tempIndex = 0;
                    break;
                default:
                    tempIndex = 0;
                    break;
            }

            GM.Alerts[tempIndex].SetActive(true);
        }
    }




    void ReleaseItem(GameObject item)
    {
        item.SetActive(true);
        item.transform.position = new Vector3(item.transform.position.x + Random.Range(-500,1000), item.transform.position.y, item.transform.position.z);
        Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
        itemRB.velocity = new Vector2(0, baseVelocity + (-GM.sceneSpeed*7.5f));//Random.Range(0, 10));
        itemRB.angularVelocity = Random.Range(5, 45);
       // item.tag = OriginalTag;
    }

    public void ResetItem(GameObject item)
    {
        item.transform.position = resetPosition;
        Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
        itemRB.velocity = new Vector2(0, 0);
        itemRB.angularVelocity = 0;
        item.SetActive(false);
        // Change this when there is more than just trash... maybe... not... 
        item.tag = OriginalTag;
    }

    IEnumerator ReleaseItemDelay()
    {
        yield return new WaitForSeconds(1f);
        ReleaseItem(trash[indexer]);
        indexer++;
        //Debug.Log("indexer Count: " + indexer);
        //Debug.Log("Trash Count: "+trash.Length);
        if (indexer >= trash.Length) { indexer = 0; }

        //if (breakloop) { }
        StartCoroutine(ReleaseItemDelay());
        
    }
}
