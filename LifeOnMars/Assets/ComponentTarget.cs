using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Material emptyMaterial;
    MeshRenderer mr;
    Material originalMat;
    [SerializeField]
    GameObject requiredComponent;
    [SerializeField]
    string task_to_update;
    void Start()
    {
        mr=GetComponent<MeshRenderer>();
        originalMat=mr.material;

        mr.material=emptyMaterial;

        // for(int i=0;i<mr.materials.Length;i++){
        //     mr.materials[i]=emptyMaterial;
        // }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject==requiredComponent){
            other.gameObject.SetActive(false);

            mr.material=originalMat;
            TaskSystem.instance.UpdateTask(task_to_update,1);
        }
    }
}
