using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] GameObject createdOne;
    [SerializeField] GameObject CollisionOne;
 void OnCollisionEnter(Collision collision)
    {
        Vector3 positionCreated = collision.transform.position;
       // Quaternion rotationCreated = collision.transform.rotation;
      //Debug.Log(collision.gameObject.name);
    if(collision != null){    
    //Debug.Log(positionCreated);

       
       

         if(CollisionOne.name == collision.gameObject.name){
            
            Destroy(collision.gameObject);

            Instantiate(createdOne, positionCreated, Quaternion.identity);
             
        }    
    }
    
    
}
}