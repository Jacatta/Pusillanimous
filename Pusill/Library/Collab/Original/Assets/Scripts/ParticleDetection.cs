using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleDetection : MonoBehaviour {

    ParticleSystem ps;
    GameManager GM;
    Dictionary<uint,int> TrashToAlert;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    //SerializedObject thisParticle;

    public int ParticleIndexer;

  //  public Image Alert1;
        

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        GM = FindObjectOfType<GameManager>();
        ParticleIndexer = 0;

        TrashToAlert = new Dictionary<uint, int>();


    }

    private void Start()
    {
        
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        
        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            if (TrashToAlert.ContainsKey(p.randomSeed))
                continue;

            TrashToAlert.Add(p.randomSeed, ParticleIndexer);

            GM.Alerts[ParticleIndexer].SetActive(true);
            GM.Alerts[ParticleIndexer].transform.position = new Vector3(p.position.x, GM.Alerts[ParticleIndexer].transform.position.y, GM.Alerts[ParticleIndexer].transform.position.z);
            ParticleIndexer++;
            
            if (ParticleIndexer >= GM.Alerts.Length)
                ParticleIndexer = 0;
        }

        for(int i = 0; i < numExit; i++)
        {


            ParticleSystem.Particle p = exit[i];

            if (!TrashToAlert.ContainsKey(p.randomSeed))
                continue;

            int AlertIndex = TrashToAlert[p.randomSeed];

            TrashToAlert.Remove(p.randomSeed);
            GM.Alerts[AlertIndex].SetActive(false);
        }

        // re-assign the modified particles back into the particle system
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
