using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorStateSpace;

public class StretchNFling : MonoBehaviour
{

    public Vector3 InitialMousePosition;
    BackgroundBehaviors BB;
    GameManager GM;
    SquidBehavior SB;

    
    // Start is called before the first frame update
    void Start()
    {
        BB = GameObject.FindObjectOfType<BackgroundBehaviors>();
        GM = FindObjectOfType<GameManager>();
        SB = GameObject.FindObjectOfType<SquidBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        InitialMousePosition = Input.mousePosition;
        Debug.Log("MouseDown");
    }
    private void OnMouseDrag()
    {
        Debug.Log("MouseDraged");
        Vector3 currentPos = new Vector3();
        currentPos = Input.mousePosition;

        SB.RB.transform.position = Vector3.MoveTowards(SB.transform.position, currentPos, SB.speed * Time.deltaTime);
        /*
                                                    new Vector3((SB.transform.position.x) - ((InitialMousePosition.x - currentPos.x) * .20f), 
                                                    ((SB.transform.position.y) - ((InitialMousePosition.y-currentPos.y)*.20f)), 
                                                    (SB.transform.position.z - (InitialMousePosition.z - currentPos.z )* .20f)), SB.speed * Time.deltaTime );
                                                    */
        //SB.behaviorState.state = "WipeOut";
        //add DELAY
        //SB.Behavior_Tether(currentPos);

    }



    private void OnMouseUp()
    {
        Debug.Log("Launched");

        SB.RB.velocity = (InitialMousePosition - Input.mousePosition) * 1000;
       // SB.ReadyToFollow = false;
        GM.sceneSpeed += 100;
        SB.ResetArms();
    }



    }
