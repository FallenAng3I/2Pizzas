using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class AreasManager : MonoBehaviour
    {
        public Resources UI;
        public GameObject Menu;
        
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
            GameObject instance = Instantiate(Sawmill, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildMine()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(Mine, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildOilWell()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(OilWell, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildOilFactory()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(OilFactory, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildSteelFactory()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(SteelFactory, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildLeadMine()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(LeadMine, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildLeadFactory()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(LeadFactory, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
        
        public void ToBuildMilitaryFactory()
        {
            var pos = currentSelection.transform.position;
            GameObject instance = Instantiate(MilitaryFactory, pos, Quaternion.identity, canvas.transform);
            instance.GetComponent<Buildings>().resources = UI;
            instance.GetComponent<ContextMenu>().Context_Menu = Menu;
            Destroy(currentSelection);
        }
    }
}