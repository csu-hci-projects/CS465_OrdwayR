using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardUI))]

public class GlyphSelectorEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     BoardUI boardUI = (BoardUI)target;

    //     EditorGUILayout.LabelField("Primary Glyph", EditorStyles.boldLabel);
    //     EditorGUILayout.LabelField("Connector", EditorStyles.boldLabel);
    //     EditorGUILayout.LabelField("Secondary Glyph", EditorStyles.boldLabel);

    //     boardUI.primaryGlyph = (GlyphType)GUILayout.SelectionGrid(
    //         (int)boardUI.primaryGlyph,
    //         new string[] { "Defense", "Health", "Attack", "Buff" },
    //         1,
    //         EditorStyles.radioButton
    //     );

    //     boardUI.connector = (ConnectorType)GUILayout.SelectionGrid(
    //         (int)boardUI.connector,
    //         new string[] { "Link", "Weave" },
    //         1,
    //         EditorStyles.radioButton
    //     );

    //     boardUI.secondaryGlyph = (GlyphType)GUILayout.SelectionGrid(
    //        (int)boardUI.secondaryGlyph,
    //        new string[] { "Defense", "Health", "Attack", "Buff" },
    //        1,
    //        EditorStyles.radioButton
    //    );

    //     EditorUtility.SetDirty(target); // Ensures changes are saved
    // }
}