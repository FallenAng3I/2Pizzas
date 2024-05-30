using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesCount : MonoBehaviour
{
    public Text txtWOOD;
    public Text txtIRON;
    public Text txtOIL;
    public Text txtFUEL;
    public Text txtSTEEL;
    public Text txtLEADORE;
    public Text txtLEAD;
    public Text txtAMMOS;
    
    private int _wood;
    private int _iron;
    private int _oil;
    private int _fuel;
    private int _steel;
    private int _leadOre;
    private int _lead;
    private int _ammos;
    
    public int Wood
    {
        get => _wood;
        set
        {
            _wood = value;
            txtWOOD.text = " " + _wood;
        }
    }

    public int Iron
    {
        get => _iron;
        set
        {
            _iron = value;
            txtIRON.text = " " + _iron;
        }
    }

    public int Oil
    {
        get => _oil;
        set
        { 
            _oil = value;
            txtOIL.text = " " + _oil;
        }
    }

    public int Fuel
    {
        get => _fuel;
        set
        { 
            _fuel = value;
            txtFUEL.text = " " + _fuel;
        }
    }

    public int Steel
    {
        get => _steel;
        set
        { 
            _steel = value;
            txtSTEEL.text = " " + _steel;
        }
    }

    public int LeadOre
    {
        get => _leadOre;
        set
        {
            _leadOre = value;
            txtLEADORE.text = " " + _leadOre;
        }
    }

    public int Lead
    {
        get => _lead;
        set
        {
            _lead = value;
            txtLEAD.text = " " + _lead;
        }
    }

    public int Ammos
    {
        get => _ammos;
        set
        {
            _ammos = value;
            txtAMMOS.text = " " + _ammos;
        }
    }
    
    
    
    /*   инструкция, чтобы добавить новый ресурс:
     *   1) добавить новое приватное поле:  private int _ВашРесурс       (обязательно с _ )
     *   2) добавить новое публичное поле класса:
     *
     *   public int ВашРесурс
     *   {
     *       get => _ВашРесурс;
     *       set
     *       }
     *           _ВашРесурс = value;
     *           txtВАШРЕСУРС.text = " " + _ВашРесурс;
     *       {
     *   }
     *
     *   3) добавить новое текстовое поле для отображения вашего ресурса: public Text txtВАШРЕСУРС.
     *
     *   Готово.
     */  
}