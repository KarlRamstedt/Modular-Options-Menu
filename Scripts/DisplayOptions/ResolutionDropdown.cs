using System.Collections.Generic;
using UnityEngine;

namespace ModularOptions {
	/// <remarks>
	/// Unity saves screen settings by default, so this class doesn't need anything from the Option base classes.
	/// Unity uses maximum supported RefreshRate by default. For explicit control: <see cref="ModularOptions.ResolutionRefreshRateDropdown"/>.
	/// Minor bug: wrong dropdown value is shown until you change resolution if you exit fullscreen mode and exit the application.
	/// </remarks>
	[AddComponentMenu("Modular Options/Display/Resolution Dropdown")]
#if TMP_PRESENT
	[RequireComponent(typeof(TMPro.TMP_Dropdown))]
#else
	[RequireComponent(typeof(UnityEngine.UI.Dropdown))]
#endif
	public sealed class ResolutionDropdown : MonoBehaviour {

		[Tooltip("Text separating Horizontal from Vertical Resolution.")]
		public string separator = "x";

		List<Vector2Int> resolutions = new List<Vector2Int>();
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
			UpdateResolutions();

			dropdown.onValueChanged.AddListener((int _) => OnValueChange(_)); //UI classes use Unity events, requiring delegates (delegate() { OnValueChange(); }) or lambda expressions (() => OnValueChange()). Listeners are not persistent, so no need to unsub
		}

		void UpdateResolutions(){ //TODO: OnResolutionChange event to refresh list for supporting dynamic screen swithing?
			var availableResolutions = Screen.resolutions;
			List<string> options = new List<string>();
			int currentResIndex = 0;
			var currentRes = new Vector2Int(Screen.width, Screen.height);
			
			for (int i = 0, len = availableResolutions.Length; i < len; i++){
				var tempRes = new Vector2Int(availableResolutions[i].width, availableResolutions[i].height);
				if (!resolutions.Contains(tempRes)){
					resolutions.Add(tempRes);
					options.Add(tempRes.x + separator + tempRes.y);

					if (tempRes.Equals(currentRes))
						currentResIndex = resolutions.Count-1; //Can't use i as index since a new list is built from the original
				}
			}
			dropdown.ClearOptions();
			dropdown.AddOptions(options);
			dropdown.value = currentResIndex;
			dropdown.RefreshShownValue();
		}

		void OnValueChange(int _resolutionIndex){
			var newRes = resolutions[_resolutionIndex];
			Screen.SetResolution(newRes.x, newRes.y, Screen.fullScreenMode); //Add ", 0" at the end to always use the highest RefreshRate
		}
	}
}
