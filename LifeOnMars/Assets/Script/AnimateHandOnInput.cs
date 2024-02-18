using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//classe che prende in input il valore di pressione del trigger del controller e lo passa come valore di input all'animator

public class AnimateHandOnInput : MonoBehaviour
{
    //dall'editor prendo in input il valore di pressione del trigger del controller
    [SerializeField] InputActionProperty pinchAnimationAction;
    [SerializeField] InputActionProperty gripAnimationAction;

    [SerializeField] Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //prendo il valore di pressione del trigger del controller e lo passo come valore di input all'animator
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
        
        //provare con il visore
        //Debug.Log(triggerValue);
    }
}
