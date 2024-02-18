using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Classe che permette di cambiare lo stato di visibilità di un pannello UI. Questo script va collegato ad un bottone UI.
public class EnableDisableMenuPanels : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private bool isTaskMenuEnabled = false;
    
    public void ButtonClicked(){
        isTaskMenuEnabled = !isTaskMenuEnabled;
        taskMenu.SetActive(isTaskMenuEnabled);
    }
    
}
