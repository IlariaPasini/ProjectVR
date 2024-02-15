using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisableMenuPanels : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private bool isTaskMenuEnabled = false;
    
    public void ButtonClicked(){
        isTaskMenuEnabled = !isTaskMenuEnabled;
        taskMenu.SetActive(isTaskMenuEnabled);
    }
    
}
