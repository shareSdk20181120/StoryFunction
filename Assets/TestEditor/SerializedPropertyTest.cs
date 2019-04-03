using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyObject:ScriptableObject
{
    public int myInt = 42;
}

public class SerializedPropertyTest : MonoBehaviour {


	// Use this for initialization
	void Start () {

        MyObject obj = ScriptableObject.CreateInstance<MyObject>();
        SerializedObject serial = new SerializedObject(obj);
        SerializedProperty property = serial.FindProperty("myInt");
        Debug.Log("my int " + property.intValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
