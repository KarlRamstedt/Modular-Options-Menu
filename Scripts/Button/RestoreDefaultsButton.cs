using System.Collections.Generic;
using UnityEngine;

namespace ModularOptions {
	/// <summary>
	/// Add this to the object of whatever button you want to restore default settings with.
	/// Restores default settings only for the referenced options. Option references can be auto-filled by
	/// right-clicking on the component and selecting one of the Auto-Fill options.
	/// </summary>
	[AddComponentMenu("Modular Options/Button/Restore Defaults")]
	[RequireComponent(typeof(UnityEngine.UI.Button))]
	public class RestoreDefaultsButton : MonoBehaviour {
		
		public SliderOption[] sliders;
		public List<DropdownOption> dropdowns;
		public ToggleOption[] toggles;

		List<OptionPreset> presets = new List<OptionPreset>();

		/// <summary>
		/// Move presets to separate list to restore after other options. (to ensure presets overwrite listener defaults)
		/// </summary>
		void Awake(){
			for (int i = dropdowns.Count - 1; i >= 0 ; i--){ //Reverse loop to allow removal during loop
				if (dropdowns[i] is OptionPreset){
					presets.Add((OptionPreset)dropdowns[i]);
					dropdowns.RemoveAt(i);
				}
			}
			GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => RestoreDefaults());
		}

		public void RestoreDefaults(){
			for (int i = 0; i < sliders.Length; i++){
				sliders[i].Value = sliders[i].defaultSetting.value;
			}
			for (int i = 0; i < dropdowns.Count; i++){
				dropdowns[i].Value = dropdowns[i].defaultSetting.value;
			}
			for (int i = 0; i < toggles.Length; i++){
				toggles[i].Value = toggles[i].defaultSetting.value;
			}
			for (int i = 0; i < presets.Count; i++){
				presets[i].Value = presets[i].defaultSetting.value;
			}
		}

#if UNITY_EDITOR
		[ContextMenu("Auto-Fill Sibling Options")]
		void FillSiblings(){
			AutoFill(transform.parent);
		}
		[ContextMenu("Auto-Fill Parent-Sibling Options")]
		void FillParentSiblings(){
			AutoFill(transform.parent.parent);
		}

		/// <summary>
		/// Automatically assigns collections of options to Restore.
		/// </summary>
		void AutoFill(Transform _targetTrans){
			UnityEditor.Undo.RecordObject(this, "RestoreDefaults AutoFill");
			sliders = _targetTrans.GetComponentsInChildren<SliderOption>();
			dropdowns = new List<DropdownOption>(_targetTrans.GetComponentsInChildren<DropdownOption>());
			toggles = _targetTrans.GetComponentsInChildren<ToggleOption>();
		}
#endif
	}
}
