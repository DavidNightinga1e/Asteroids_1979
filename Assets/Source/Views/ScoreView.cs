using TMPro;
using UnityEngine;

namespace Source.Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        public TextMeshProUGUI TextMeshPro => textMeshPro;

        public void OnValidate()
        {
            this.AutoGetChildComponent(out textMeshPro);
        }
    }
}