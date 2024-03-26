using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    public Text txtWOOD;
    public Text txtIRON;
    public Text txtOIL;

    // Свойства для хранения значений ресурсов
    public int Wood { get; set; }
    public int Iron { get; set; }
    public int Oil { get; set; }
}