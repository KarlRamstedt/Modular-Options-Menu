using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Add this to the object of whatever button you want to save preferences with, like a 'Back' or 'Apply' button.
	/// </summary>
	[AddComponentMenu("Modular Options/Button/Save PlayerPrefs")]
	[RequireComponent(typeof(Button))]
	public class SavePlayerPrefsButton : MonoBehaviour {
		
		void Awake(){
			GetComponent<Button>().onClick.AddListener(() => OptionSaveSystem.SaveToDisk());
		}
	}
}
