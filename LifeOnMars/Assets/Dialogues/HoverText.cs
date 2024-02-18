using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverText : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable xrs=GetComponentInParent<XRSimpleInteractable>(true);
        UnityAction<HoverEnterEventArgs> ua1=new UnityAction<HoverEnterEventArgs>((_)=>gameObject.SetActive(true));
        UnityAction<HoverExitEventArgs> ua2=new UnityAction<HoverExitEventArgs>((_)=>gameObject.SetActive(false));
        xrs.hoverEntered.AddListener(ua1);
        xrs.hoverExited.AddListener(ua2);
        gameObject.SetActive(false);
        // xrs.hoverEntered+=()=>{gameObject.SetActive(true);};
        // xrs.hoverEntered+=()=>{gameObject.SetActive(true);};
    }

 
    
}
