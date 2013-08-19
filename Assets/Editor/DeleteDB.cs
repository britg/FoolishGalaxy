
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class DeleteDB : ScriptableWizard
{
	[MenuItem ("Assets/Reset DB")]

	static void DoDeleteDB()
	{
		File.Delete(Application.persistentDataPath + "/foolish_galaxy.db");
    Debug.Log("Replacing db");
	}
}