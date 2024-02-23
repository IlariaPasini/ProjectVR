using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Chiama le callback Set quando l'oggetto ha approssimativamente la direzione della normale specificata, e quelle r
/// </summary>


public class CallOnNormal : MonoBehaviour
{
    enum TransformDir{
        FORWARD,
        UP,
        RIGHT
    }
    [SerializeField]
    bool cameraRelative, transitionActive=false;

    [SerializeField]
    Vector3 normal;
    [SerializeField]
    TransformDir comparisonVector=TransformDir.FORWARD;
    [SerializeField]
    float setThreshold, resetThreshold;
    [SerializeField]
    UnityEvent onSet, onReset;
    [SerializeField]
    UnityEvent<float> onTransition;

    float dot;
    bool set=false,reset=true;

    public bool TransitionActive { get => transitionActive; set => transitionActive = value; }

    // Update is called once per frame
    void Update()
    {
        CalculateDot();

        if(dot>setThreshold){
            if(!set){
                onSet.Invoke();
                set=true;
                reset=false;
            }
        }else if(dot<resetThreshold){
            if(!reset){
                onReset.Invoke();
                reset=true;
                set=false;
            }
        }else if(TransitionActive){
            onTransition.Invoke((dot-resetThreshold)/(setThreshold-resetThreshold));
        }
    }
    void OnDisable(){
        TransitionActive=false;
    }

    void CalculateDot(){

        Vector3 cameraNormal=normal;
        if(cameraRelative)
            cameraNormal=Camera.main.transform.InverseTransformDirection(normal);
        switch(comparisonVector){
            case TransformDir.FORWARD:
            dot=Vector3.Dot(cameraNormal, transform.forward);
            break;
            case TransformDir.UP:
            dot=Vector3.Dot(cameraNormal, transform.up);
            break;
            case TransformDir.RIGHT:
            dot=Vector3.Dot(cameraNormal, transform.right);
            break;
        }
    }
}
