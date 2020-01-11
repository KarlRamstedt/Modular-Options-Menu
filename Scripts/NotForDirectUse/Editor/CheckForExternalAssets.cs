using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

namespace ModularOptions {
	/// <summary>
	/// Checks if the external namespaces exist (like TMPro for TextMesh Pro) and compiles scripts accordingly.
	/// </summary>
	[InitializeOnLoad]
	sealed class CheckForExternalAssets {

		public static readonly Dictionary<string, string> namespaceDefinePairs = new Dictionary<string, string>(){
			["TMPro"] = "TMP_PRESENT",
			["UnityEngine.Rendering.Universal"] = "URP_PRESENT",
			["UnityEngine.Rendering.HighDefinition"] = "HDRP_PRESENT",
			["UnityEngine.Rendering.PostProcessing"] = "UNITY_POST_PROCESSING_STACK_V2",
			["AuraAPI"] = "AURA_PRESENT"
		};

		static CheckForExternalAssets(){
			if (EditorApplication.isPlayingOrWillChangePlaymode)
				return;

			SetDefinesIfNamespacesExist();
		}

		static void SetDefinesIfNamespacesExist(){
			var namespaces = DefineUtilities.GetNamespaces();

			var add = new HashSet<string>(); //Can be done with lists, but Sets prevent accidental duplicates
			var remove = new HashSet<string>();
			foreach (var pair in namespaceDefinePairs){
				if (namespaces.Contains(pair.Key)) //Add or Remove defines depending on the existence of the Namespace
					add.Add(pair.Value);
				else
					remove.Add(pair.Value);
			}

			var validBuildTargets = DefineUtilities.GetValidBuildTargets();
			DefineUtilities.AddDefines(add, validBuildTargets);
			DefineUtilities.RemoveDefines(remove, validBuildTargets);
		}
	}

	/// <summary>
	/// Manipulates Unity Scripting Define Symbols as a collection of strings.
	/// </summary>
	public static class DefineUtilities {
		/// <summary>
		/// ScriptingDefineSymbols are separated into a collection by any of these,
		/// but always written back using the separator in index 0.
		/// Semicolon (;) is the intended separator according to Unity Documentation.
		/// </summary>
		public static readonly char[] separators = { ';' };

		public static void AddDefine(string _define, IEnumerable<BuildTargetGroup> _buildTargets){
			foreach (var target in _buildTargets){
				var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();
				var set = new HashSet<string>(defines.Split(separators).Where(_ => !string.IsNullOrEmpty(_)));

				if (set.Contains(_define))
					continue;
				defines = set.Aggregate((a, b) => a + separators[0] + b) + separators[0] + _define;
				PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
			}
        }

		public static void RemoveDefine(string _define, IEnumerable<BuildTargetGroup> _buildTargets){
            foreach (var target in _buildTargets){
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();
                var set = new HashSet<string>(defines.Split(separators).Where(_ => !string.IsNullOrEmpty(_)));

				if (!set.Remove(_define)) //If not in list then no changes needed
					continue;
                defines = set.Aggregate((a, b) => a + separators[0] + b);
                PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
        }

		public static void AddDefines(HashSet<string> _defines, IEnumerable<BuildTargetGroup> _buildTargets){
            foreach (var target in _buildTargets){
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();
                var set = new HashSet<string>(defines.Split(separators).Where(_ => !string.IsNullOrEmpty(_)));

				bool defineAdded = false;
				foreach (var define in _defines){
					if (!set.Contains(define)){
						set.Add(define);
						defineAdded = true;
					}
				}
				if (!defineAdded)
					continue;
				defines = set.Aggregate((a, b) => a + separators[0] + b);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
        }

		public static void RemoveDefines(HashSet<string> _defines, IEnumerable<BuildTargetGroup> _buildTargets){
            foreach (var target in _buildTargets){
                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(target).Trim();
                var set = new HashSet<string>(defines.Split(separators).Where(_ => !string.IsNullOrEmpty(_)));

				bool defineRemoved = false;
				foreach (var define in _defines){
					if (set.Remove(define))
						defineRemoved = true;
				}
				if (!defineRemoved)
					continue;
				defines = set.Aggregate((a, b) => a + separators[0] + b);
				PlayerSettings.SetScriptingDefineSymbolsForGroup(target, defines);
            }
        }

		public static IEnumerable<BuildTargetGroup> GetValidBuildTargets(){
			return Enum.GetValues(typeof(BuildTargetGroup))
				.Cast<BuildTargetGroup>()
				.Where(_ => _ != BuildTargetGroup.Unknown)
				.Where(_ => !IsObsolete(_));
		}

        public static bool IsObsolete(this BuildTargetGroup group){
            var obsoleteAttributes = typeof(BuildTargetGroup)
				.GetField(group.ToString())
				.GetCustomAttributes(typeof(ObsoleteAttribute), false);

            return obsoleteAttributes != null && obsoleteAttributes.Length > 0;
        }

		/// <returns>
		/// Unique namespaces in all Assemblies in the CurrentDomain.
		/// </returns>
		public static HashSet<string> GetNamespaces(){
			var namespaces = new HashSet<string>();
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()){
				foreach (var type in assembly.GetTypes())
					namespaces.Add(type.Namespace);
			}
			return namespaces;
		}

		public static bool NamespaceExists(string _namespace){
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()){
				foreach (var type in assembly.GetTypes())
					if (type.Namespace == _namespace)
						return true;
			}
			return false;
		}
	}
}
