using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

//classe che gestisce il menu di gioco a livello visuale. 
//Di fatto è un semplice script che attiva e disattiva l'HUD quando viene premuto un tasto del controller

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {
        //faccio sì che l'oggetto sia disattivato all'avvio del gioco 
        //(si poteva fare dall'editor ma così siamo sicuri di non scordarcelo)
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
