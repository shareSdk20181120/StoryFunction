using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WindowToggle : UnityEditor.EditorWindow
{

    bool showBtn = true;
    //TagField
    string tagStr = "";
    string myStr = "hello world";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    //
    int index;
    string[] options = new string[] { "cube","sphere","plane"};
    //space
    bool fold = true;
    Vector4 roteComponents;
    Transform selectedTrans;
    //TextArea
    string text = "Nothing opened";
    TextAsset txtAsset;
    Vector2 scroll;
    Object source;

    [MenuItem("Example/toggleWindow")]
    static void Init()
    {
        WindowToggle window = (WindowToggle)GetWindow(typeof(WindowToggle), true, "my toggle window");
        window.Show();
    }
    private void OnGUI()
    {
        //Toggle
        showBtn = EditorGUILayout.Toggle("show btn", showBtn);
        if (showBtn)
            if (GUILayout.Button("close"))
                this.Close();
        //TagField--1、选择某个标签，2、选择多个物体，3、点击set tag按钮，这几个选择的物体的tag 全部改为设置的值
        tagStr = EditorGUILayout.TagField("tag for object", tagStr);
        if (GUILayout.Button("set tag"))
        {
            SetTags();
        }
        //
        GUILayout.Label("base settings", EditorStyles.boldLabel);
        myStr = EditorGUILayout.TextField("Text field ", myStr);
        groupEnabled = EditorGUILayout.BeginToggleGroup("optional setting ", groupEnabled);
        myBool = EditorGUILayout.Toggle("toggle ", myBool);
        myFloat = EditorGUILayout.Slider("slider ", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        //
        index = EditorGUILayout.Popup(index, options);
        if(GUILayout.Button("Create"))
        {
            CreateGameObject(index);
        }
        //space--
        if(Selection.activeGameObject)
        {
            selectedTrans = Selection.activeGameObject.transform;
            fold = EditorGUILayout.InspectorTitlebar(fold, selectedTrans);//标题条--- 折叠、 展开
            if (fold)
            {
                selectedTrans.position = EditorGUILayout.Vector3Field("Position", selectedTrans.position);
                EditorGUILayout.Space();
                roteComponents = EditorGUILayout.Vector4Field("Detailed Rotation", QuaternionToVector4(selectedTrans.localRotation));
                EditorGUILayout.Space();
                selectedTrans.localScale = EditorGUILayout.Vector3Field("scale", selectedTrans.localScale);
            }

            selectedTrans.localRotation = ConvertToQuaternion(roteComponents);
            EditorGUILayout.Space();
        }
        //TextField
        GUILayout.Label("select an obj in the hierarchy view");
        if (Selection.activeGameObject)
            Selection.activeGameObject.name = EditorGUILayout.TextField("obj name : ", Selection.activeGameObject.name);
        //this.Repaint();

        //TextArea
        source = EditorGUILayout.ObjectField(source, typeof(Object), true);
        if (source is TextAsset)
        {
            TextAsset newText = (TextAsset)source;
            if (newText != txtAsset)
            {
                ReadTextAsset(newText);
            }
            scroll = EditorGUILayout.BeginScrollView(scroll);
            text = EditorGUILayout.TextArea(text, GUILayout.Height(position.height - 10));
            EditorGUILayout.EndScrollView();

        }

    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }

    Quaternion ConvertToQuaternion(Vector4 v4)
    {
        return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    }

    Vector4 QuaternionToVector4(Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }
    void SetTags()
    {
        foreach(var m in Selection.gameObjects)
        {
            m.tag = tagStr;
        }
    }

    private  void ReadTextAsset(TextAsset txt)
    {
        if (txt == null)
            return;
        text = txt.text;
        txtAsset = txt;
    }

    private void CreateGameObject(int index)
    {
        switch(index)
        {
            case 0:
                GameObject ob = GameObject.CreatePrimitive(PrimitiveType.Cube);
                ob.transform.position = Vector3.zero;
                break;
            case 1:
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = Vector3.zero;
                break;
            case 2:
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.transform.position = Vector3.zero;
                break;
            default:
                break;
        }
    }


}
