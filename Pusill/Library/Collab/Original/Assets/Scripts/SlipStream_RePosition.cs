using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlipStream_RePosition : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager GM;
    Vector3 currentPos;
    Vector3 targetPos;
    Vector3 newPosition;
    public bool ReadytoMove;
    public int speedMult = 100;
    int indie=0;

    public ParticleSystem psPlayerEntered;
    public ParticleSystem psHelix;
    public ParticleSystem psSceneSpeedUp;
    public ParticleSystem psArrows;

    public Image SlipStreamHighlight;

    Animator Anim;

    ParticleSystem.MinMaxCurve psC_startLifeSpan;
    ParticleSystem.EmissionModule psEmitter;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        currentPos = this.transform.position;
        

        ReadytoMove = true;

        psPlayerEntered.gameObject.SetActive(false);
        psSceneSpeedUp.gameObject.SetActive(false);
        psC_startLifeSpan = psHelix.main.startLifetime;
        psEmitter = psSceneSpeedUp.emission;
        targetPos = new Vector3((500), currentPos.y, 0f);
        StartCoroutine(MoveToNewPositionTwo());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 250f, 1500f),
        transform.position.y,
         transform.position.z);

        var main = psArrows.main;
        if (GM.sceneSpeed < 25) { main.simulationSpeed = 5f; }
        else
        {
            main.simulationSpeed = (GM.sceneSpeed / 10) * 2;
        }


        // main.simulationSpeed = hSliderValue;
        currentPos = transform.position;
        int Rando=0;
      // 
        /*
        if (Random.Range(0, 60) == 1)
        {
            if (ReadytoMove == true)
            {
                Debug.Log(transform.position.x);
                if (transform.position.x < 200)
                {
                    Debug.Log("Im too far left");
                    Rando = Random.Range(10, 20)*100;
                    Debug.Log(Rando);
                }
                else if (transform.position.x > 1000)
                {
                    Debug.Log("Im too far Right");
                    Rando = Random.Range(-10, -4);
                }else
                {
                    Debug.Log("Rando isnt Random");
                    Rando = 5*100;// Random.Range(-10, 10);
                }
                Debug.Log("ResetPositoin");
                targetPos = new Vector3((500), currentPos.y, 0f);
               // targetPos = newPosition;
            }
        }
          */
        

        if(GM.sceneSpeed>0&& GM.sceneSpeed < 100)
        {
            psEmitter.rateOverTime = 4f;
          //  main.simulationSpeed = 5;
          /*
            main.startColor = new Color(main.startColor.color.r, 
                main.startColor.color.g, main.startColor.color.b, 
               100f);
               */
        }
        else if(GM.sceneSpeed >100 && GM.sceneSpeed<300)
        {
            psEmitter.rateOverTime = 20f;
           // main.simulationSpeed = 10;

        }
        else if (GM.sceneSpeed > 300 && GM.sceneSpeed < 600)
        {
          //  main.simulationSpeed = 40;
            psEmitter.rateOverTime = 40f;
        }

           // Debug.Log("Move it");
        MoveToNewPosition();
        ReadytoMove = false;
        if ((int)Time.time % 23 == 0)// && ReadytoMove == true)
        {

        }

        if(currentPos==targetPos)
        {
            var HelixLife = psHelix.main.startLifetime;
            var newV = psHelix.velocityOverLifetime;
            newV.orbitalZ = 0f;
            HelixLife= psC_startLifeSpan;
            ReadytoMove = true;
        }
    }

    public void ResetToNewPosition()
    {
        Anim = GetComponent<Animator>();
        Anim.SetTrigger("Reset");
        targetPos = new Vector3(2000, 0, 0);
        MoveToNewPosition();
        //this.gameObject.SetActive(false);
    }

    public Vector3 newTargetPos()
    {
      //  targetPos = new Vector3(x,currentPos.y,0);
        return targetPos;
    }

    IEnumerator MoveToNewPositionTwo()
    {

        int Rando = Random.Range(1, 10);
        indie++;
        yield return new WaitForSeconds(Rando * .2f);
        if (currentPos == targetPos|| indie > 1)
        {
            indie = 0;
            Rando = Random.Range(-10, 10);
            targetPos = new Vector3(((Rando * 100) ), currentPos.y, 0f);
            if(targetPos.x<300)
            {
                Debug.Log("Too far left");
                Rando = Random.Range(2, 10);
                targetPos = new Vector3(((Rando * 100)), currentPos.y, 0f);
            }
        }
       // targetPos = new Vector3(((Rando*10) +500), currentPos.y, 0f);

        StartCoroutine(MoveToNewPositionTwo());

    }

    public void MoveToNewPosition()
    {
        /*
        Debug.Log("Move to new Called;");
       
        if (transform.position.x >= 0)
        {
            
            newV.orbitalZ = -.7f;
        }
        else
        {
            newV.orbitalZ =.8f;
        }
*/

        var HelixLife = psHelix.main.startLifetime;
        var newV = psHelix.velocityOverLifetime;
        float f = new float();
        f = Mathf.PingPong(Time.deltaTime,-.5f);
        newV.orbitalZ = f;
        //  HelixLife = new ParticleSystem.MinMaxCurve(0f,-10f);
        //int i = Random.Range(0, 100);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, Time.deltaTime * speedMult);


     //   targetPos = new Vector3(targetPos.x+ 500f, 2800, targetPos.z);
     /*
        if (targetPos.x >= 500)
        { targetPos = new Vector3(400, targetPos.y, targetPos.z); }

        if (targetPos.x <= -450)
        { targetPos = new Vector3(0, targetPos.y, targetPos.z); }
        */
        transform.position = Vector3.MoveTowards(currentPos, targetPos, Time.deltaTime *speedMult);

        
    }


}
