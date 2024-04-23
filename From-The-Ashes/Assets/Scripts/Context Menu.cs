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
}
