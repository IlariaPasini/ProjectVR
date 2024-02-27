using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Classe che permette di cambiare lo stato di visibilità di un pannello UI. Questo script va collegato ad un bottone UI.
public class EnableDisableMenuPanels : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private GameObject commandMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private bool isTaskMenuEnabled = false;
    [SerializeField] private bool isCommandMenuEnabled = false;
    [SerializeField] private bool isQuitMenuEnabled = false;
    
    /*public void ButtonClicked(){
        isMenuEnabled = !isMenuEnabled;
        taskMenu.SetActive(isMenuEnabled);
    }*/

    public void TaskMenuEnabledOnClick(){
        if(commandMenu.activeSelf)
            commandMenu.SetActive(false);
        if (quitMenu.activeSelf)
            quitMenu.SetActive(false);
        isTaskMenuEnabled = !taskMenu.activeSelf;
        taskMenu.SetActive(isTaskMenuEnabled);
    }

    public void CommandMenuEnabledOnClick(){
        if(taskMenu.activeSelf)
            taskMenu.SetActive(false);
        if (quitMenu.activeSelf)
            quitMenu.SetActive(false);
        isCommandMenuEnabled = !commandMenu.activeSelf;
        commandMenu.SetActive(isCommandMenuEnabled);
    }

    public void QuitMenuEnabledOnClick()
    {
        if (taskMenu.activeSelf)
            taskMenu.SetActive(false);
        if (commandMenu.activeSelf)
            commandMenu.SetActive(false);
        isQuitMenuEnabled = !quitMenu.activeSelf;
        quitMenu.SetActive(isQuitMenuEnabled);
    }
}
