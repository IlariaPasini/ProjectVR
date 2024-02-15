using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] GameObject createdOne;

 void OnCollisionEnter(Collision collision)
    {
        Vector3 positionCreated = collision.transform.position;
       // Quaternion rotationCreated = collision.transform.rotation;
      //Debug.Log(collision.gameObject.name);
    if(collision != null){    
    //Debug.Log(positionCreated);

        CraftToBeRemoved hinge = collision.gameObject.GetComponent<CraftToBeRemoved>();
       

         if(hinge!= null){
            
            Destroy(collision.gameObject);

            Instantiate(createdOne, positionCreated, Quaternion.identity);
             
        }    
    }
    
    
}
}