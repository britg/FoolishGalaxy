
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateSimplePrefab : ScriptableWizard
{
	[MenuItem ("Assets/Create Simple Prefab")]

	static void DoCreateSimplePrefab()
	{
		Transform[] transforms = Selection.transforms;
		foreach (Transform t in transforms) {
			Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Prefabs/"+t.gameObject.name+".prefab");
			PrefabUtility.ReplacePrefab(t.gameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);
		}
	}
}