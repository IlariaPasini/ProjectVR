using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{   
    [SerializeField]
    bool doesEmpty;
    [SerializeField]
    float quantity, angle_threshold;
    float angle;
    [SerializeField]
    bool isPlaying;
    [SerializeField]
    public bool triggerWatering=true;
    ParticleSystem ps;
    ParticleSystem.EmissionModule pe;
    [SerializeField]
    LayerMask mask;
    
    // Start is called before the first frame update
    public void ActivateWatering(bool enabled){
        triggerWatering=enabled;
    }
    void Start()
    {
        ps=GetComponentInChildren<ParticleSystem>(true);
        pe=ps.emission;
        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        angle=Vector3.Angle(Vector3.up, transform.forward);

        if(angle>angle_threshold){
            //ps.Play(true);
            pe.enabled=true;
        }else{
            //ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            
            pe.enabled=false;
        }

        isPlaying=pe.enabled;
    }

    void FixedUpdate(){
        RaycastHit hit;

        if(triggerWatering && isPlaying && Physics.Raycast(ps.transform.position,Vector3.down,out hit,2,mask)){
            //Call watering on plants
            Waterable w;

            if(hit.transform.TryGetComponent<Waterable>(out w)){
                w.Water();
            }
        }
    }
}
