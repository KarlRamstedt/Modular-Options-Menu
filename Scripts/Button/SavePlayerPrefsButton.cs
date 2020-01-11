using UnityEngine;
using UnityEngine.UI;

namespace ModularOptions {
	/// <summary>
	/// Add this to the object of whatever button you want to save preferences with, like a 'Back' or 'Apply' button.
	/// Changed settings are kept in memory even without calling PlayerPrefs.Save(), but not saved to disk until
	/// PlayerPrefs.Save() is called or the application quits. Saving avoids loss of data in the case of a crash.
	/// </summary>
	[AddComponentMenu("Modular Options/Button/Save PlayerPrefs")]
	[RequireComponent(typeof(Button))]
	public class SavePlayerPrefsButton : MonoBehaviour {
		
		void Awake(){
			GetComponent<Button>().onClick.AddListener(() => PlayerPrefs.Save());
		}
	}
}
