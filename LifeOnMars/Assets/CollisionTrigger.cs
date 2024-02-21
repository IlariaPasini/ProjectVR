using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] string[] expected_storables;

    [SerializeField] UnityEvent<string> events;
    // Start is called before the first frame update

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Storable s;
        if(other.TryGetComponent<Storable>(out s) && expected_storables.Contains(s.ItemName)){
            events.Invoke(s.ItemName);
            s.gameObject.SetActive(false);
        }
    }
}
