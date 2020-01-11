using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace ModularOptions {
	/// <summary>
	/// Presents data as a slider in the inspector,
	/// which aside from increasing usability also prevents erroneous values.
	/// </summary>
	[CustomPropertyDrawer(typeof(FloatSlider))]
	public class FloatSliderDrawer : PropertyDrawer {

		public override void OnGUI(Rect _rect, SerializedProperty _prop, GUIContent _label){
			var inspectedScript = (Component)_prop.serializedObject.targetObject; //Cast to component for access to GetComponent
			var slider = inspectedScript.GetComponent<UnityEngine.UI.Slider>();
			if (slider == null){
				EditorGUI.HelpBox(_rect, "Error: No Slider on Object", MessageType.Error);
			} else {
				_prop.NextVisible(true); //Skip over wrapper data class and get the first-and-only value inside
				if (slider.wholeNumbers) //Make intSlider if using whole numbers
					_prop.PropertySlider(_rect, _label, (int)slider.minValue, (int)slider.maxValue);
				else
					_prop.PropertySlider(_rect, _label, slider.minValue, slider.maxValue);
			}
		}
	}

	/// <summary>
	/// Presents data as a dropdown selection in the inspector,
	/// which aside from increasing usability also prevents erroneous values.
	/// </summary>
	[CustomPropertyDrawer(typeof(IntDropdown))]
	public class IntDropdownDrawer : PropertyDrawer {

		public override void OnGUI(Rect _rect, SerializedProperty _prop, GUIContent _label){
			var inspectedScript = (Component)_prop.serializedObject.targetObject; //Cast to component for access to GetComponent
#if TMP_PRESENT
			var dropdown = inspectedScript.GetComponent<TMPro.TMP_Dropdown>();
#else
			var dropdown = inspectedScript.GetComponent<UnityEngine.UI.Dropdown>();
#endif
			if (dropdown == null){
				EditorGUI.HelpBox(_rect, "Error: No Dropdown on Object", MessageType.Error);
			} else {
				_prop.NextVisible(true); //Skip over wrapper data class and get the first-and-only value inside
				GUIContent[] optionText = new GUIContent[dropdown.options.Count];
				for (int i = 0; i < optionText.Length; i++)
					optionText[i] = new GUIContent(dropdown.options[i].text);

				_prop.PropertyDropdown(_rect, _label, optionText);
			}
		}
	}

	/// <summary>
	/// No functionality, just hides the class wrapping the bool.
	/// </summary>
	[CustomPropertyDrawer(typeof(BoolToggle))]
	public class BoolToggleDrawer : PropertyDrawer {
		public override void OnGUI(Rect _rect, SerializedProperty _prop, GUIContent _label){
			_prop.NextVisible(true); //Skip over wrapper data class and get the first-and-only value inside
			EditorGUI.PropertyField(_rect, _prop, _label);
		}
	}


	/// <summary>
	/// Draws the OptionPreset inspector in a more compact way that automatically sizes sub-arrays
	/// and labels presetData fields with the corresponding option names for ease of use.
	/// </summary>
	[CustomEditor(typeof(OptionPreset))]
	public class OptionPresetInspector : Editor {

		SerializedProperty nameProp, defaultProp;
		SerializedProperty sliderDataArrayProp, dropdownDataArrayProp, toggleDataArrayProp;
		AnimBool sliderFold, dropdownFold, toggleFold;
		GUIContent newSliderLabel = new GUIContent("+ New Slider", "Add a new Slider by putting a reference in this field.");
		GUIContent newDropdownLabel = new GUIContent("+ New Dropdown", "Add a new Dropdown by putting a reference in this field.");
		GUIContent newToggleLabel = new GUIContent("+ New Toggle", "Add a new Toggle by putting a reference in this field.");
		GUIContent removeLabel = new GUIContent("x", "Delete element");
		GUILayoutOption removeButtonWidth = GUILayout.Width(18f);
		SliderOption newSlider;
		DropdownOption newDropdown;
		ToggleOption newToggle;

		const string noOptionsWarning = "Warning: No Dropdown Options";
		const string nullWarning = "Warning: Null reference";
		const string errorSelfListenText = "Preset can't listen to itself";

		void OnEnable(){
			nameProp = serializedObject.FindProperty("optionName");
			defaultProp = serializedObject.FindProperty("defaultSetting");
			sliderDataArrayProp = serializedObject.FindProperty("sliderPresetData");
			dropdownDataArrayProp = serializedObject.FindProperty("dropdownPresetData");
			toggleDataArrayProp = serializedObject.FindProperty("togglePresetData");
			sliderFold = new AnimBool(true);
			dropdownFold = new AnimBool(true);
			toggleFold = new AnimBool(true);
			sliderFold.valueChanged.AddListener(Repaint);
			dropdownFold.valueChanged.AddListener(Repaint);
			toggleFold.valueChanged.AddListener(Repaint);
		}
		void OnDisable(){ //Might not be needed, but just in case
			sliderFold.valueChanged.RemoveListener(Repaint);
			dropdownFold.valueChanged.RemoveListener(Repaint);
			toggleFold.valueChanged.RemoveListener(Repaint);
		}

		public override void OnInspectorGUI(){
			serializedObject.Update();
			EditorGUILayout.PropertyField(nameProp); //Property fields take care of lots automatically (like prefab stuff)
			EditorGUILayout.PropertyField(defaultProp);

			var presetScript = (OptionPreset)target;
			bool inScene = presetScript.gameObject.scene.IsValid(); //Whether to accept references from a scene
			bool objectPickerClosed = Event.current.type == EventType.Repaint && EditorGUIUtility.GetObjectPickerControlID() == 0; //True during Repaint event when ObjectPicker is closed (ControlID is 0 when closed). Data should only be manipulated during Repaint to avoid element-number-missmatch between Layout and Repaint events.
#if TMP_PRESENT
			var presets = presetScript.GetComponent<TMPro.TMP_Dropdown>().options;
#else
			var presets = presetScript.GetComponent<UnityEngine.UI.Dropdown>().options;
#endif
			var presetCount = presets.Count-1; //Assumes last one is 'Custom'

			if (presetCount < 0){
				EditorGUILayout.HelpBox(noOptionsWarning, MessageType.Warning);
				serializedObject.ApplyModifiedProperties();
				return;
			}


			EditorGUILayout.BeginVertical(GUI.skin.box);
			var foldoutRect = EditorGUILayout.GetControlRect();
			foldoutRect.xMin += 12f; //Foldout sticks out of the side of the box unless offset
			sliderFold.target = EditorGUI.Foldout(foldoutRect, sliderFold.target, "Sliders", true);
			if (EditorGUILayout.BeginFadeGroup(sliderFold.faded)){
				for (int i = 0; i < sliderDataArrayProp.arraySize; i++){
					var data = sliderDataArrayProp.GetArrayElementAtIndex(i);
					data.NextVisible(true); //Traverse to UI reference

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button(removeLabel, EditorStyles.miniButtonLeft, removeButtonWidth)){
						sliderDataArrayProp.DeleteArrayElementAtIndex(i);
						EditorGUILayout.EndHorizontal();
						continue;
					} else {
						EditorGUILayout.PropertyField(data, GUIContent.none);
						if (data.objectReferenceValue == null){
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.HelpBox(nullWarning, MessageType.Warning);
							continue; //Skip drawing presetData for missing element
						}
					}
					EditorGUILayout.EndHorizontal();

					EditorGUI.indentLevel += 2;
					var slider = ((SliderOption)data.objectReferenceValue).GetComponent<UnityEngine.UI.Slider>();
					data.NextVisible(false); //Traverse to Data array
					if (data.arraySize != presetCount)
						data.arraySize = presetCount; //Ensure changes to number of presets are applied
					for (int j = 0; j < presetCount; j++){
						var values = data.GetArrayElementAtIndex(j);
						if (slider.wholeNumbers) //Make intSlider if using whole numbers
							values.PropertySlider(new GUIContent(presets[j].text), (int)slider.minValue, (int)slider.maxValue);
						else
							values.PropertySlider(new GUIContent(presets[j].text), slider.minValue, slider.maxValue);
					}
					EditorGUI.indentLevel -= 2;
				}

				newSlider = (SliderOption)EditorGUILayout.ObjectField(newSliderLabel, newSlider, typeof(SliderOption), inScene);
				if (newSlider != null && objectPickerClosed){
					sliderDataArrayProp.InsertArrayElementAtIndex(sliderDataArrayProp.arraySize);
					var data = sliderDataArrayProp.GetArrayElementAtIndex(sliderDataArrayProp.arraySize-1); //Assumes last one is 'Custom'
					data.NextVisible(true); //Traverse to UI reference
					data.objectReferenceValue = newSlider;
					data.NextVisible(false); //Traverse to Data array
					data.arraySize = presetCount;
					newSlider = null; //TODO: Set keyboard focus to new element
				}
			}
			EditorGUILayout.EndFadeGroup();
			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginVertical(GUI.skin.box);
			foldoutRect = EditorGUILayout.GetControlRect();
			foldoutRect.xMin += 12f; //Foldout sticks out of the side of the box unless offset
			dropdownFold.target = EditorGUI.Foldout(foldoutRect, dropdownFold.target, "Dropdowns", true);
			if (EditorGUILayout.BeginFadeGroup(dropdownFold.faded)){
				for (int i = 0; i < dropdownDataArrayProp.arraySize; i++){
					var data = dropdownDataArrayProp.GetArrayElementAtIndex(i);
					data.NextVisible(true); //Traverse to UI reference

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button(removeLabel, EditorStyles.miniButtonLeft, removeButtonWidth)){
						dropdownDataArrayProp.DeleteArrayElementAtIndex(i);
						EditorGUILayout.EndHorizontal();
						continue;
					} else {
						EditorGUILayout.PropertyField(data, GUIContent.none);
						if (data.objectReferenceValue == null){
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.HelpBox(nullWarning, MessageType.Warning);
							continue; //Skip drawing presetData for missing element
						}
					}
					EditorGUILayout.EndHorizontal();
					if (data.objectReferenceValue == presetScript){
						EditorGUILayout.HelpBox(errorSelfListenText, MessageType.Error);
						continue;
					}

					EditorGUI.indentLevel += 2;
#if TMP_PRESENT
					var dropdown = ((DropdownOption)data.objectReferenceValue).GetComponent<TMPro.TMP_Dropdown>();
#else
					var dropdown = ((DropdownOption)data.objectReferenceValue).GetComponent<UnityEngine.UI.Dropdown>();
#endif
					data.NextVisible(false); //Traverse to Data array
					if (data.arraySize != presetCount)
						data.arraySize = presetCount; //Ensure changes to number of presets are applied
					var optionText = new GUIContent[dropdown.options.Count];
					for (int j = 0; j < optionText.Length; j++)
						optionText[j] = new GUIContent(dropdown.options[j].text);
					for (int j = 0; j < presetCount; j++){
						var values = data.GetArrayElementAtIndex(j);
						values.PropertyDropdown(new GUIContent(presets[j].text), optionText);
					}
					EditorGUI.indentLevel -= 2;
				}

				newDropdown = (DropdownOption)EditorGUILayout.ObjectField(newDropdownLabel, newDropdown, typeof(DropdownOption), inScene);
				if (newDropdown != null){
					if (newDropdown == presetScript)
						EditorGUILayout.HelpBox(errorSelfListenText, MessageType.Error);
					else if (objectPickerClosed){
						dropdownDataArrayProp.InsertArrayElementAtIndex(dropdownDataArrayProp.arraySize);
						var data = dropdownDataArrayProp.GetArrayElementAtIndex(dropdownDataArrayProp.arraySize - 1);
						data.NextVisible(true); //Traverse to UI reference
						data.objectReferenceValue = newDropdown;
						data.NextVisible(false); //Traverse to Data array
						data.arraySize = presetCount;
						newDropdown = null;
					}
				}
			}
			EditorGUILayout.EndFadeGroup();
			EditorGUILayout.EndVertical();


			EditorGUILayout.BeginVertical(GUI.skin.box);
			foldoutRect = EditorGUILayout.GetControlRect();
			foldoutRect.xMin += 12f; //Foldout sticks out of the side of the box unless offset
			toggleFold.target = EditorGUI.Foldout(foldoutRect, toggleFold.target, "Toggles", true);
			if (EditorGUILayout.BeginFadeGroup(toggleFold.faded)){
				for (int i = 0; i < toggleDataArrayProp.arraySize; i++){
					var data = toggleDataArrayProp.GetArrayElementAtIndex(i);
					data.NextVisible(true); //Traverse to UI reference

					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button(removeLabel, EditorStyles.miniButtonLeft, removeButtonWidth)){
						toggleDataArrayProp.DeleteArrayElementAtIndex(i);
						EditorGUILayout.EndHorizontal();
						continue;
					} else {
						EditorGUILayout.PropertyField(data, GUIContent.none);
						if (data.objectReferenceValue == null){
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.HelpBox(nullWarning, MessageType.Warning);
							continue; //Skip drawing presetData for missing element
						}
					}
					EditorGUILayout.EndHorizontal();

					EditorGUI.indentLevel += 2;
					data.NextVisible(false); //Traverse to Data array
					if (data.arraySize != presetCount)
						data.arraySize = presetCount; //Ensure changes to number of presets are applied
					for (int j = 0; j < presetCount; j++){
						EditorGUILayout.PropertyField(data.GetArrayElementAtIndex(j), new GUIContent(presets[j].text));
					}
					EditorGUI.indentLevel -= 2;
				}

				newToggle = (ToggleOption)EditorGUILayout.ObjectField(newToggleLabel, newToggle, typeof(ToggleOption), inScene);
				if (newToggle != null && objectPickerClosed){
					toggleDataArrayProp.InsertArrayElementAtIndex(toggleDataArrayProp.arraySize);
					var data = toggleDataArrayProp.GetArrayElementAtIndex(toggleDataArrayProp.arraySize - 1);
					data.NextVisible(true); //Traverse to UI reference
					data.objectReferenceValue = newToggle;
					data.NextVisible(false); //Traverse to Data array
					data.arraySize = presetCount;
					newToggle = null;
				}
			}
			EditorGUILayout.EndFadeGroup();
			EditorGUILayout.EndVertical();

			serializedObject.ApplyModifiedProperties();
		}
	}


	public static class EditorGUILayoutExtensions {
		/// <summary>
		/// An auto-layout float slider property.
		/// </summary>
		public static void PropertySlider(this SerializedProperty _prop, GUIContent _label, float _minValue, float _maxValue){
			var rect = EditorGUILayout.GetControlRect();
			_prop.PropertySlider(rect, _label, _minValue, _maxValue);
		}
		/// <summary>
		/// An auto-layout int slider property.
		/// NOTE: Uses property floatValue (not intValue) for data storage due to flexibility.
		/// </summary>
		public static void PropertySlider(this SerializedProperty _prop, GUIContent _label, int _minValue, int _maxValue){
			var rect = EditorGUILayout.GetControlRect();
			_prop.PropertySlider(rect, _label, _minValue, _maxValue);
		}

		/// <summary>
		/// An auto-layout int dropdown property.
		/// </summary>
		public static void PropertyDropdown(this SerializedProperty _prop, GUIContent _label, GUIContent[] _options){
			var rect = EditorGUILayout.GetControlRect();
			_prop.PropertyDropdown(rect, _label, _options);
		}
	}

	public static class EditorGUIExtensions {
		/// <summary>
		/// A float slider property.
		/// </summary>
		public static void PropertySlider(this SerializedProperty _prop, Rect _rect, GUIContent _label, float _minValue, float _maxValue){
			_label = EditorGUI.BeginProperty(_rect, _label, _prop);
			EditorGUI.BeginChangeCheck();
			var newValue = Mathf.Clamp(_prop.floatValue, _minValue, _maxValue); //Assure value conforms to limits
			newValue = EditorGUI.Slider(_rect, _label, newValue, _minValue, _maxValue);
			if (EditorGUI.EndChangeCheck()) //Only assign value if changed by user, else multi-editing can errouneously overwrite it
				_prop.floatValue = newValue;
			EditorGUI.EndProperty();
		}
		/// <summary>
		/// An int slider property.
		/// NOTE: Uses property floatValue (not intValue) for data storage due to flexibility.
		/// </summary>
		public static void PropertySlider(this SerializedProperty _prop, Rect _rect, GUIContent _label, int _minValue, int _maxValue){
			_label = EditorGUI.BeginProperty(_rect, _label, _prop);
			EditorGUI.BeginChangeCheck();
			var newValue = Mathf.Clamp((int)_prop.floatValue, _minValue, _maxValue); //Ensure value conforms to range
			newValue = EditorGUI.IntSlider(_rect, _label, newValue, _minValue, _maxValue);
			if (EditorGUI.EndChangeCheck()) //Only assign value if changed by user, else multi-editing can errouneously overwrite it
				_prop.floatValue = newValue;
			EditorGUI.EndProperty();
		}

		/// <summary>
		/// An int dropdown property.
		/// </summary>
		public static void PropertyDropdown(this SerializedProperty _prop, Rect _rect, GUIContent _label, GUIContent[] _options){
			_label = EditorGUI.BeginProperty(_rect, _label, _prop);
			EditorGUI.BeginChangeCheck();
			var newValue = Mathf.Clamp(_prop.intValue, 0, _options.Length-1); //Limit value to existing options
			newValue = EditorGUI.Popup(_rect, _label, newValue, _options);
			if (EditorGUI.EndChangeCheck()) //Only assign value if changed by user, else multi-editing can errouneously overwrite it
				_prop.intValue = newValue;
			EditorGUI.EndProperty();
		}
	}
}
