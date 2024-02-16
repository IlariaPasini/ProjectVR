using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ComponentTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Material emptyMaterial;
    MeshRenderer mr;
    Material originalMat;
    [SerializeField]
    bool with_object=true;
    [SerializeField]
    GameObject requiredComponent;
    [SerializeField]
    string expected_name;
    [SerializeField]
    string task_to_update;

    public UnityEvent onComplete;
    void Start()
    {
        mr=GetComponent<MeshRenderer>();
        originalMat=mr.material;
        
        Material[] newMaterials=new Material[mr.materials.Length];

        for(int i=0;i<mr.materials.Length;i++){
            newMaterials[i]=emptyMaterial;
        }
        mr.materials=newMaterials;
    }
    
    void OnTriggerEnter(Collider other)
    {   
        Storable s;
        if(with_object && other.gameObject==requiredComponent) {
            //other.gameObject.SetActive(false);
            
            other.GetComponent<XRGrabInteractable>().enabled=false;

            other.GetComponent<Rigidbody>().isKinematic=true;
            other.transform.position=transform.position;
            other.transform.rotation=transform.rotation;
            other.transform.localScale=transform.localScale;
            
            onComplete.Invoke();
            TaskSystem.instance.UpdateTask(task_to_update,1);
            gameObject.SetActive(false);
        }

        if(!with_object && other.TryGetComponent<Storable>(out s) && s.ItemName==expected_name){
            s.enabled=false;
            other.GetComponent<XRGrabInteractable>().enabled=false;

            other.GetComponent<Rigidbody>().isKinematic=true;
            other.transform.position=transform.position;
            other.transform.rotation=transform.rotation;
            other.transform.localScale=transform.localScale;
            
            onComplete.Invoke();
            TaskSystem.instance.UpdateTask(task_to_update,1);
            gameObject.SetActive(false);
            
        }
    }
}
