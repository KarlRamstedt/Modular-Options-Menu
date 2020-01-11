using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Quits application or exits playmode if in editor.
	/// </summary>
	[AddComponentMenu("Modular Options/Button/Quit")]
	[RequireComponent(typeof(Button))]
	public class QuitButton : MonoBehaviour {

		void Awake(){
			GetComponent<Button>().onClick.AddListener(() => Quit());
		}

		void Quit(){
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
}
