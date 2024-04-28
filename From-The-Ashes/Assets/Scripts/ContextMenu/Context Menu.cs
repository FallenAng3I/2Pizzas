using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    public GameObject Context_Menu;
    
    public void OpenMenu()         //открыть меню
    {
        Context_Menu.SetActive(true);
    }
    
    public void CloseMenu()        //закрыть меню
    {
        Context_Menu.SetActive(false);
    }
    
    public void OnButtonClick()     // кулдаун до закрытия, чтобы кнопки в меню успевали сработать
    {
        StartCoroutine(CloseMenuAfterDelay());
    }

    private IEnumerator CloseMenuAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        
        CloseMenu();
    }
}
