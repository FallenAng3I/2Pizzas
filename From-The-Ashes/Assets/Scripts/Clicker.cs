using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    // Ссылки на кнопки, которые будут отвечать за добычу ресурсов
    [FormerlySerializedAs("sawmillButton")] public Button Sawmill;
    [FormerlySerializedAs("mineButton")] public Button Mine;
    [FormerlySerializedAs("oilWellButton")] public Button OilWell;


    public Resources resources; // Ссылка на скрипт Resources для доступа к текстовым полям с ресурсами

    void Start()
    {
        // Добавляем слушателей на кнопки для обработки нажатий
        Sawmill.onClick.AddListener(IncreaseWood);
        Mine.onClick.AddListener(IncreaseIron);
        OilWell.onClick.AddListener(IncreaseOil);
    }
    
    public void IncreaseWood() // Метод для увеличения счётчика ресурса wood при нажатии на кнопку Sawmill
    {
        resources.Wood++;
        UpdateWoodUI(); // Обновляем отображение счётчика wood на экране
    }
    
    public void IncreaseIron()
    {
        resources.Iron++;
        UpdateIronUI(); // Обновляем отображение счётчика iron на экране
    }
    
    public void IncreaseOil()
    {
        resources.Oil++;
        UpdateOilUI(); // Обновляем отображение счётчика oil на экране
    }
    
    private void UpdateWoodUI()
    {
        resources.txtWOOD.text = resources.Wood.ToString();
    }
    
    private void UpdateIronUI()
    {
        resources.txtIRON.text = resources.Iron.ToString();
    }
    
    private void UpdateOilUI()
    {
        resources.txtOIL.text = resources.Oil.ToString();
    }
}