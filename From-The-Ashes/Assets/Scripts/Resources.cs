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

public class Wood : MonoBehaviour
{
    // Метод для увеличения количества древесины
    public void IncreaseWood(int amount)
    {
        GetComponent<Resources>().Wood += amount;
    }
}

public class Iron : MonoBehaviour
{
    // Метод для увеличения количества железа
    public void IncreaseIron(int amount)
    {
        GetComponent<Resources>().Iron += amount;
    }
}

public class Oil : MonoBehaviour
{
    // Метод для увеличения количества нефти
    public void IncreaseOil(int amount)
    {
        GetComponent<Resources>().Oil += amount;
    }
}