using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AreasManager : MonoBehaviour
    {
        public Buildings Building;
        public GameObject UI;
        
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
            var pos = currentSelection.transform.position;
            Instantiate(Sawmill, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildMine()
        {
            var pos = currentSelection.transform.position;
            Instantiate(Mine, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildOilWell()
        {
            var pos = currentSelection.transform.position;
            Instantiate(OilWell, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildOilFactory()
        {
            var pos = currentSelection.transform.position;
            Instantiate(OilFactory, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildSteelFactory()
        {
            var pos = currentSelection.transform.position;
            Instantiate(SteelFactory, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildLeadMine()
        {
            var pos = currentSelection.transform.position;
            Instantiate(LeadMine, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
        
        public void ToBuildLeadFactory()
        {
            var pos = currentSelection.transform.position;
            Instantiate(LeadFactory, pos, Quaternion.identity, canvas.transform);

            Destroy(currentSelection);
        }
        
        public void ToBuildMilitaryFactory()
        {
            var pos = currentSelection.transform.position;
            Instantiate(MilitaryFactory, pos, Quaternion.identity, canvas.transform);
            
            Destroy(currentSelection);
        }
    }
}