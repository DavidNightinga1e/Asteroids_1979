using TMPro;
using UnityEngine;

namespace Source.Components
{
    public class GameOverScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        public TextMeshProUGUI TextMeshPro => textMeshPro;
        
#if UNITY_EDITOR
        [ContextMenu("Auto Set")]
        public void AutoSet()
        {
            this.AutoGetChildComponent(out textMeshPro);
        }
#endif
    }
}