using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
	private int _defaultLineHeight = 15;
	private int _defaultLabelWidth = 115;

	public override void OnInspectorGUI()
	{
		Node node = target as Node;
		EditorStyles.textArea.wordWrap = true;
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Text", GUILayout.Width(_defaultLabelWidth));
		node.Text = EditorGUILayout.TextArea(node.Text);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
		EditorGUILayout.LabelField("Next Node", GUILayout.Width(_defaultLabelWidth));
		node.NextNode = (Node)EditorGUILayout.ObjectField(node.NextNode, typeof(Node), true);
		EditorGUILayout.EndHorizontal();

		if (node.Options.Length > 0)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
			EditorGUILayout.LabelField("Permanent Choice", GUILayout.Width(_defaultLabelWidth));
			node.PermanentChoice = EditorGUILayout.Toggle(node.PermanentChoice);
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
		EditorGUILayout.LabelField("Give Clue", GUILayout.Width(_defaultLabelWidth));
		node.GiveClue = (Clue)EditorGUILayout.ObjectField(node.GiveClue, typeof(Clue), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
		EditorGUILayout.LabelField("Give Item", GUILayout.Width(_defaultLabelWidth));
		node.GiveItem = (Item)EditorGUILayout.ObjectField(node.GiveItem, typeof(Item), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
		EditorGUILayout.LabelField("Load Level", GUILayout.Width(_defaultLabelWidth));
		node.LoadLevel = EditorGUILayout.TextField(node.LoadLevel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
		EditorGUILayout.LabelField("Single Read", GUILayout.Width(_defaultLabelWidth));
		node.SingleRead = EditorGUILayout.Toggle(node.SingleRead);
		EditorGUILayout.EndHorizontal();

		if (node.SingleRead)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Height(_defaultLineHeight));
			EditorGUILayout.LabelField("Alt Node", GUILayout.Width(_defaultLabelWidth));
			node.AltNode = (Node)EditorGUILayout.ObjectField(node.AltNode, typeof(Node), true);
			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Add Option"))
		{
			GameObject newOption = new GameObject("Option");
			newOption.AddComponent<Option>();
			newOption.AddComponent<Text>();
			newOption.transform.SetParent(node.transform);
		}

		if (GUILayout.Button("Add Condition"))
		{
			GameObject newCondition = new GameObject("Condition");
			newCondition.AddComponent<Condition>();
			newCondition.transform.SetParent(node.transform);
		}

		node.gameObject.name = (node.Text == "" || node.Text == null) ? "<node>" : "\"" + node.Text.Substring(0, node.Text.Length > 30 ? 30 : node.Text.Length) + (node.Text.Length > 30 ? "..." : "") + "\"";
		
	}
}
