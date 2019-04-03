using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyPlayer))]
[CanEditMultipleObjects]
public class MyPlayerEditor : Editor
{
    SerializedProperty damageProp;
    SerializedProperty armorProp;
    SerializedProperty gunProp;

    void OnEnable()
    {
        damageProp = serializedObject.FindProperty("damage");
        armorProp = serializedObject.FindProperty("armor");
        gunProp = serializedObject.FindProperty("gun");
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));

        if (!armorProp.hasMultipleDifferentValues)
            ProgressBar(armorProp.intValue / 100f, "Armor");
        EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun Object"));
        serializedObject.ApplyModifiedProperties();
    }

    void ProgressBar(float value,string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}
