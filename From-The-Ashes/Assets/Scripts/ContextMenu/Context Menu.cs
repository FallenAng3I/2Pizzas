using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    public GameObject Context_Menu;
    
    public void OpenMenu()
    {
        Context_Menu.SetActive(true);
    }
    
    public void CloseMenu()
    {
        Context_Menu.SetActive(false);
    }
    
    public void OnButtonClick()
    {
        StartCoroutine(CloseMenuAfterDelay());
    }

    private IEnumerator CloseMenuAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        
        CloseMenu();
    }
}
