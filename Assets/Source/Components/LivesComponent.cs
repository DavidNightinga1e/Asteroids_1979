using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private List<Image> lives;
    
    public TextMeshProUGUI TextMeshPro => textMeshPro;
    public List<Image> Lives => lives;

#if UNITY_EDITOR
    [ContextMenu("Auto Set")]
    public void AutoSet()
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
#endif
}