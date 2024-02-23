using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public virtual bool CanReceive(Storable s){
        return true;
    }
    public virtual void Receive(GameObject g){
        g.SetActive(false);
    }

    protected virtual void enableOutline()
    {
        GetComponent<Outline>().enabled = true;
    }

    protected virtual void disableOutline()
    {
        GetComponent<Outline>().enabled = false;
    }
}
