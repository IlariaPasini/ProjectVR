using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    // Start is called before the first frame update
    Rover r;
    [SerializeField]
    int id;
    TextMeshProUGUI tmp;
    Button button;
    void Start()
    {
        tmp=GetComponentInChildren<TextMeshProUGUI>();
        button=GetComponent<Button>();
        r=GetComponentInParent<Rover>();
        button.onClick.AddListener(()=>{r.Get(id);});
        r.on_inv_update+=TextUpdate;
        TextUpdate();
    }

    // Update is called once per frame
    void TextUpdate()
    {
        if(r.stored_objects.Count<=id){
            tmp.text="None";
            button.interactable=false;
            return;
        }

        Storable storable;
        storable=r.stored_objects[id].GetComponent<Storable>();
        button.interactable=true;
        tmp.text=storable.ItemName; 
    }
}
