using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AreasManager : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        public static AreasManager Instance;
        
        public GameObject Sawmill;
        public GameObject Mine;
        public GameObject OilWell;
        public GameObject OilFactory;
        public GameObject SteelFactory;
        public GameObject LeadMine;
        public GameObject LeadFactory;
        public GameObject MilitaryFactory;
        
        public GameObject currentSelection { get; set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            currentSelection = null;
        }
        
        public void ToBuildSawmill()
        {
            //GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            var pos = currentSelection.transform.position;
            Instantiate(Sawmill, pos, Quaternion.identity, canvas.transform);
            Destroy(currentSelection);
        }
    }
}