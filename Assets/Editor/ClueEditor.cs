using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Clue))]
public class ClueEditor : Editor
{

	public override void OnInspectorGUI()
	{
		Clue clue = target as Clue;

		EditorGUILayout.LabelField("Name");
		clue.Name = EditorGUILayout.TextField(clue.Name);

		clue.clueLevel = (Clue.ClueLevel)EditorGUILayout.EnumPopup("Level", clue.clueLevel);

		EditorGUILayout.LabelField("Description");
		clue.Description = EditorGUILayout.TextArea(clue.Description);
		EditorStyles.textField.wordWrap = true;

		clue.gameObject.name = (clue.name == "" || clue.name == null) ? "<clue>" : clue.name;
		
	}
}