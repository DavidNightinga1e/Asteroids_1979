using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Views
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private List<Image> lives;
    
        public TextMeshProUGUI TextMeshPro => textMeshPro;
        public List<Image> Lives => lives;

        public void OnValidate()
        {
            this.AutoGetChildComponent(out textMeshPro);

            var images = GetComponentsInChildren<Image>();
            if (images.Length == 3)
            {
                lives = images
                    .OrderBy(t => t.transform.GetSiblingIndex())
                    .ToList();
            }
            else
                throw new Exception("Unable to determine where lives are automatically");
        }
    }
}