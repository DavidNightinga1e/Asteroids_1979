using TMPro;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public int currentScore;

    public TextMeshProUGUI TextMeshPro => textMeshPro;

#if UNITY_EDITOR
    [ContextMenu("Auto Set")]
    public void AutoSet()
    {
        this.AutoGetChildComponent(out textMeshPro);
    }
#endif
}