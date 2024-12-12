using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder
{
    public class CanvasMonitor : MonoBehaviour
    {
        public enum ToolboxPanels
        {
            Root,
            Construction,
            Eletricity,
            Aqueduct
        }

        //
        // Public Interface
        //
        public bool isPointerOverGUI { get; set; }

        //
        // Cached References
        //
        [SerializeField] private CanvasGroup rootPanel = null;
        [SerializeField] private CanvasGroup constructionPanel = null;
        [SerializeField] private CanvasGroup electricityPanel = null;
        [SerializeField] private CanvasGroup aqueductPanel = null;

        //
        // Internal 
        public static float count = 0f;
        public Text counttext;
        private ToolboxPanels activePanel = ToolboxPanels.Construction;
        public void countchange()
        {
            count++;
            counttext.text = count.ToString();
        }
        public ToolboxPanels GetActivePanel()
        {
            return activePanel;
        }

        public void SetActivePanel(ToolboxPanels newActivePanel)
        {
            rootPanel.gameObject.SetActive(false);
            constructionPanel.gameObject.SetActive(false);
            electricityPanel.gameObject.SetActive(false);
            aqueductPanel.gameObject.SetActive(false);

            switch (newActivePanel)
            {
                case ToolboxPanels.Construction:
                    constructionPanel.gameObject.SetActive(true);
                    break;

                case ToolboxPanels.Eletricity:
                    electricityPanel.gameObject.SetActive(true);
                    break;

                case ToolboxPanels.Aqueduct:
                    aqueductPanel.gameObject.SetActive(true);
                    break;
                default:
                    // None
                    rootPanel.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
