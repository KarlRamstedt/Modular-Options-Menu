using System.Collections.Generic;
using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Sets active built-in Unity Quality presets.
	/// Works with Render Pipelines that use QualityLevels (currently Builtin and HDRP).
	/// </summary>
	/// <remarks>
	/// I recommend using individual settings and grouping them with an OptionPreset instead of this.
	/// That affords the user a much greater freedom in what settings to use.
	/// Unity saves QualityLevel by default, so this class doesn't need anything from the Option base classes.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/Quality Level Dropdown")]
#if TMP_PRESENT
	[RequireComponent(typeof(TMPro.TMP_Dropdown))]
#else
	[RequireComponent(typeof(UnityEngine.UI.Dropdown))]
#endif
	public sealed class QualityLevelDropdown : MonoBehaviour {
#if TMP_PRESENT
		TMPro.TMP_Dropdown dropdown;
#else
		UnityEngine.UI.Dropdown dropdown;
#endif

		void Awake(){
#if TMP_PRESENT
			dropdown = GetComponent<TMPro.TMP_Dropdown>();
#else
			dropdown = GetComponent<UnityEngine.UI.Dropdown>();
#endif
			dropdown.ClearOptions();
			dropdown.AddOptions(new List<string>(QualitySettings.names)); //Convert names array to List and add it to the dropdown
			dropdown.value = QualitySettings.GetQualityLevel();
			dropdown.RefreshShownValue();

			dropdown.onValueChanged.AddListener((int _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
		}

		void OnValueChange(int _value){
			QualitySettings.SetQualityLevel(_value, true);
		}
    }
}
