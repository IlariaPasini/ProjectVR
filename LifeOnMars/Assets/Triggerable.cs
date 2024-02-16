using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;



public class Triggerable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int expected_input;
    int counted_input=0;
    [SerializeField]
    UnityEvent onComplete;
    [SerializeField]
    Material newMaterial;

    // Update is called once per frame
    public void SendInput()
    {
        counted_input++;
        if(counted_input==expected_input)
            onComplete.Invoke();
    }

    public void ChangeMaterialAt(int id){
        MeshRenderer mr=GetComponent<MeshRenderer>();
        Material[ ] materials=mr.materials;

        materials[id]=newMaterial;
        mr.materials=materials;
    }
}
