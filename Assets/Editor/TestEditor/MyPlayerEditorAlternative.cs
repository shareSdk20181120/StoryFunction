using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyPlayerAlternative))]
public class MyPlayerEditorAlternative :Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        MyPlayerAlternative mp = (MyPlayerAlternative)target;
        mp.damage = EditorGUILayout.IntSlider("Damage", mp.damage, 0, 100);
        ProgressBar(mp.damage / 100f, "Damage");

        mp.armor = EditorGUILayout.IntSlider("Armor", mp.armor, 0, 100);
        ProgressBar(mp.armor / 100f, "Armor");
        bool allowSceneObjs = !EditorUtility.IsPersistent(target);
        mp.gun = (GameObject)EditorGUILayout.ObjectField("Gun Object", mp.gun, typeof(GameObject), allowSceneObjs);
    }

    void ProgressBar(float  value,string table)
    {
        Rect rt = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rt, value, table);
        EditorGUILayout.Space();
    }
}
