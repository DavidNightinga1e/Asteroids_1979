using TMPro;
using UnityEngine;

namespace Source.Views
{
	public class DebugUiView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI text;

		public TextMeshProUGUI Text => text;
	}
}