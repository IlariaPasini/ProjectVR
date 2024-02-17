using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
        [SerializeField] CraftElement craft;
        private string collisionOne;
        private GameObject createdOne;
void Start(){
    
    collisionOne = craft.secondElement;
    createdOne = craft.createdElement;

}
 void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SCONTRO");
        Vector3 positionCreated = collision.transform.position;
       // Quaternion rotationCreated = collision.transform.rotation;
      //Debug.Log(collision.gameObject.name);
    if(collision != null){    
    //Debug.Log(positionCreated);

       
       

         if(collisionOne == collision.gameObject.name){
            
            Destroy(collision.gameObject);

            Instantiate(createdOne, positionCreated, Quaternion.identity);
             
        }    
    }
    
    
}
}