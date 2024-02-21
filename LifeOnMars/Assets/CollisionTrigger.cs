using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] string[] expected_storables;
    [SerializeField] string task_to_update;
    [SerializeField] UnityEvent<string> events;

    [SerializeField] public bool active=false;

    public void Activate(bool enable){
        active=enable;
    }
    
    // Start is called before the first frame update

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Storable s;
        if(active && other.TryGetComponent<Storable>(out s) && expected_storables.Contains(s.ItemName)){
            events.Invoke(s.ItemName);
            s.gameObject.SetActive(false);
            TaskSystem.instance.UpdateTask(task_to_update,1);
        }
    }
    
}
