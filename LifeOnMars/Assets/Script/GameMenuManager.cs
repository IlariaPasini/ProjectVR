using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private InputActionProperty showButton;

    //voglio che la UI sia sempre ancorata al braccio del giocatore
    [SerializeField] private Transform bracelet;
    [SerializeField] private float spawnDistance = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }

        if(menu.activeSelf){
            menu.transform.position = bracelet.position + new Vector3(bracelet.forward.x-3, bracelet.forward.y, bracelet.forward.z)*spawnDistance;
            menu.transform.rotation = bracelet.rotation;   
            menu.transform.Rotate(0,0,-90,Space.Self); 
        }
    }
}
