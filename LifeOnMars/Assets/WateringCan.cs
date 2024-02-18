using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{   
    [SerializeField]
    bool doesEmpty;
    [SerializeField]
    float quantity, angle_threshold;
    
    ParticleSystem ps;
    [SerializeField]
    LayerMask mask;
    
    // Start is called before the first frame update
    void Start()
    {
        ps=GetComponentInChildren<ParticleSystem>(true);

    }

    // Update is called once per frame
    void Update()
    {
        float angle=Vector3.Angle(Vector3.up, transform.forward);

        if(angle>angle_threshold){
            ps.Play();
        }else{
            ps.Stop();
        }
    }

    void FixedUpdate(){
        RaycastHit hit;

        if(Physics.Raycast(ps.transform.position,Vector3.down,out hit,2,mask)){
            //Call watering on plants
            Waterable w;

            if(hit.transform.TryGetComponent<Waterable>(out w)){
                w.Water();
            }
        }
    }
}
