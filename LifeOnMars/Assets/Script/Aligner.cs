using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float maxDistance, movement_delta, delta_error;
    Vector3 startPos;
    [SerializeField]
    Transform antenna;
    [SerializeField]
    float antenna_angle_delta;
    [SerializeField]
    string taskToUpdate;
    int direction=0;
    void Start()
    {
        startPos=transform.localPosition;

        transform.localPosition+=Vector3.right*Random.Range(-1.0f,1.0f)*maxDistance;
    }

    public void Update(){
        if(direction==-1){
            _MoveLeft();
        }
        if(direction==1){
            _MoveRight();
        }

    
    }
    // Update is called once per frame
    void _MoveLeft()
    {
        print("Moving Left");
        if(Vector3.Dot(transform.localPosition-startPos, Vector3.right)>maxDistance)
            return;
        transform.localPosition+=Vector3.right*Time.deltaTime*movement_delta;
        antenna.Rotate(Vector3.up,Time.deltaTime*antenna_angle_delta,Space.World);


    }

    public void MoveLeft(){
        
        direction=-1;
    }
    public void MoveRight(){
        direction=1;
    }
    public void Stay(){
        direction=0;
        if(Mathf.Abs(Vector3.Dot(transform.localPosition-startPos, Vector3.right))<delta_error){
            TaskSystem.instance.UpdateTask(taskToUpdate,1);
            enabled=false;
        }
    }

    void _MoveRight(){
        print("Moving Right");
        if(Vector3.Dot(transform.localPosition-startPos, Vector3.right)<-maxDistance)
            return;
        transform.localPosition-=Vector3.right*Time.deltaTime*movement_delta;
        antenna.Rotate(Vector3.up,-Time.deltaTime*antenna_angle_delta,Space.World);

    }
}
