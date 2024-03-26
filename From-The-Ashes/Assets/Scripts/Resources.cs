using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    
}

public class Wood : Resources
{
    public int wood;

    public Text txtWOOD;

    void Update()
    {
        txtWOOD.text = ": " + wood.ToString();
    }
}

public class Iron : Resources
{
    public int iron;

    public Text txtIRON;

    void Update()
    {
        txtIRON.text = ": " + iron.ToString();
    } 
}

public class Oil : Resources
{
    public int oil;

    public Text txtOIL;

    void Update()
    {
        txtOIL.text = ": " + oil.ToString();
    }
}