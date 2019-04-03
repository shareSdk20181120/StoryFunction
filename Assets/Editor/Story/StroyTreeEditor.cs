#if UNITY_EDITOR
using GameEditor.Window;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
//using Game.Config;

public class StroyTreeEditor : TreeNodeEditor
{
	private Dictionary<string, int> nameDic;
	private ShowEnumAlias ls = new ShowEnumAlias(typeof(Enum_Story_Location));
	private ShowEnumAlias rs = new ShowEnumAlias(typeof(Enum_Story_Role));
	private ShowEnumAlias es = new ShowEnumAlias(typeof(Enum_Story_Emoji));
	private ShowEnumAlias ras = new ShowEnumAlias(typeof(Enum_Story_RoleAnim));
	private ShowEnumAlias ts = new ShowEnumAlias(typeof(Enum_Story_Type));
	private ShowEnumAlias bs = new ShowEnumAlias(typeof(Enum_Story_BGM));

	private string find = string.Empty;

	[MenuItem("Window/StoryTree")]
	static void ShowWindow()
	{
		StroyTreeEditor window = EditorWindow.GetWindow<StroyTreeEditor>();
	}

	protected override void OnGuiDraw()
	{
		//GUILayout.BeginVertical();
		GUILayout.Label(path);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Save", GUILayout.Width(100)))
		{
			Save();
		}
		if (GUILayout.Button("Load", GUILayout.Width(100)))
		{
			Load();
		}
		if (GUILayout.Button("Clear", GUILayout.Width(100)))
		{
			ClearAllNode();
		}
		if (GUILayout.Button("FindNode", GUILayout.Width(100)))
		{
			Find();
		}
		find = GUILayout.TextField(find);
		GUILayout.EndHorizontal();
		//GUILayout.EndVertical();
	}

	private void Find()
	{
		if (!string.IsNullOrEmpty(find))
		{
			SRect r = nodeRootList[int.Parse(find)].windowRect;
			nodeRootList[int.Parse(find)].windowRect = new SRect(0, 0, r.width, r.height);
		}
	}

	protected override void OnDrawNodeWindow(NodeRoot node)
	{
		StoryNode sn = node as StoryNode;
		GUILayout.BeginVertical();

        #region 剧情类型  对话、旁白、选择、冒泡

        int index = EditorGUILayout.Popup("剧情类型：", ts.GetIndex((int)sn.type), ts.alias.ToArray());
        sn.type = (Enum_Story_Type)ts.GetEnum(index);// EditorGUILayout.Popup("剧情类型:", ts.GetIndex((int)sn.type), ts.alias.ToArray()));
		switch (sn.type)
		{
			case Enum_Story_Type.None:
				sn.text = EditorGUILayout.TextField("文本:", sn.text);
				sn.animType = Enum_Story_RoleAnim.None;
				sn.emoji = Enum_Story_Emoji.None;
				sn.location = Enum_Story_Location.None;
				sn.characterName = string.Empty;
				sn.roleId = 0;
				sn.roleType = Enum_Story_Role.None;
				break;
			case Enum_Story_Type.Dialog://对话
				sn.text = EditorGUILayout.TextField("文本:", sn.text);
				sn.location = (Enum_Story_Location)ls.GetEnum(EditorGUILayout.Popup("位置:", ls.GetIndex((int)sn.location), ls.alias.ToArray()));
				sn.roleType = (Enum_Story_Role)rs.GetEnum(EditorGUILayout.Popup("角色类型:", rs.GetIndex((int)sn.roleType), rs.alias.ToArray()));
				sn.animType = (Enum_Story_RoleAnim)ras.GetEnum(EditorGUILayout.Popup("角色动画:", ras.GetIndex((int)sn.animType), ras.alias.ToArray()));
				sn.characterName = EditorGUILayout.TextField("角色名字:", sn.characterName);
				sn.soundEffect = EditorGUILayout.TextField("音效:", sn.soundEffect);
				break;
			case Enum_Story_Type.Aside://旁白
				sn.text = EditorGUILayout.TextField("文本:", sn.text);
				sn.animType = Enum_Story_RoleAnim.None;
				sn.emoji = Enum_Story_Emoji.None;
				sn.location = Enum_Story_Location.None;
				sn.characterName = string.Empty;
				sn.roleId = 0;
				sn.roleType = Enum_Story_Role.None;
				sn.soundEffect = EditorGUILayout.TextField("音效:", sn.soundEffect);
				break;
			case Enum_Story_Type.CutTo://转场
				sn.textId = 0;
				sn.text = string.Empty;
				sn.animType = Enum_Story_RoleAnim.None;
				sn.emoji = Enum_Story_Emoji.None;
				sn.location = Enum_Story_Location.None;
				sn.roleId = 0;
				sn.characterName = string.Empty;
				sn.roleType = Enum_Story_Role.None;
				break;
			case Enum_Story_Type.Choice://选择
				sn.textId = 0;
				sn.text = string.Empty;
				sn.animType = Enum_Story_RoleAnim.None;
				sn.emoji = Enum_Story_Emoji.None;
				sn.location = Enum_Story_Location.None;
				sn.roleId = 0;
				sn.characterName = string.Empty;
				sn.roleType = Enum_Story_Role.None;
				break;
			case Enum_Story_Type.Bubble://冒泡
				sn.text = EditorGUILayout.TextField("文本:", sn.text);
				sn.location = (Enum_Story_Location)ls.GetEnum(EditorGUILayout.Popup("位置:", ls.GetIndex((int)sn.location), ls.alias.ToArray()));
				sn.roleType = (Enum_Story_Role)rs.GetEnum(EditorGUILayout.Popup("角色类型:", rs.GetIndex((int)sn.roleType), rs.alias.ToArray()));
				sn.animType = (Enum_Story_RoleAnim)ras.GetEnum(EditorGUILayout.Popup("角色动画:", ras.GetIndex((int)sn.animType), ras.alias.ToArray()));
				sn.characterName = EditorGUILayout.TextField("角色名字:", sn.characterName);
				break;
			case Enum_Story_Type.VoiceOver://字幕
				sn.text = EditorGUILayout.TextField("文本:", sn.text);
				sn.animType = Enum_Story_RoleAnim.None;
				sn.emoji = Enum_Story_Emoji.None;
				sn.location = Enum_Story_Location.None;
				sn.roleId = 0;
				sn.characterName = string.Empty;
				sn.roleType = Enum_Story_Role.None;
				break;
		}

        #endregion

        #region   立绘、通讯窗口、雪花窗口

        switch (sn.roleType)
		{
			case Enum_Story_Role.None:
				sn.emoji = Enum_Story_Emoji.None;
				break;
			case Enum_Story_Role.Normal://立绘
				sn.emoji = (Enum_Story_Emoji)es.GetEnum(EditorGUILayout.Popup("立绘表情:", es.GetIndex((int)sn.emoji), es.alias.ToArray()));
				break;
			case Enum_Story_Role.Window://通讯窗口
				sn.emoji = Enum_Story_Emoji.None;
				break;
			case Enum_Story_Role.NoiseWindow://雪花窗口
				sn.emoji = Enum_Story_Emoji.None;
				break;
			default:
				break;
		}

        #endregion

        sn.autoPlay = EditorGUILayout.Toggle("自动", sn.autoPlay);
		if (sn.autoPlay)
			sn.delayTime = EditorGUILayout.FloatField("延迟:", sn.delayTime);
		sn.backGround1 = EditorGUILayout.TextField("背景1:", sn.backGround1);
		sn.backGround2 = EditorGUILayout.TextField("背景2:", sn.backGround2);

        #region 声音类型
        sn.bgmType = (Enum_Story_BGM)ts.GetEnum(EditorGUILayout.Popup("BGM类型:", bs.GetIndex((int)sn.bgmType), bs.alias.ToArray()));
		switch (sn.bgmType)
		{
			case Enum_Story_BGM.None:
				sn.BGM = string.Empty;
				sn.fadeTime = 0f;
				sn.BGMLoop = false;
				break;
			case Enum_Story_BGM.Play:
				sn.BGM = EditorGUILayout.TextField("BGM:", sn.BGM);
				sn.fadeTime = EditorGUILayout.FloatField("淡化时间:", sn.fadeTime);
				sn.BGMLoop = EditorGUILayout.Toggle("循环播放:", sn.BGMLoop);
				break;
			case Enum_Story_BGM.Pause:
				sn.fadeTime = EditorGUILayout.FloatField("淡化时间:", sn.fadeTime);
				break;
			case Enum_Story_BGM.Continue:
				sn.fadeTime = EditorGUILayout.FloatField("淡化时间:", sn.fadeTime);
				break;
			case Enum_Story_BGM.Stop:
				sn.fadeTime = EditorGUILayout.FloatField("淡化时间:", sn.fadeTime);
				break;
			case Enum_Story_BGM.StopAndContinueLast:
				sn.fadeTime = EditorGUILayout.FloatField("淡化时间:", sn.fadeTime);
				break;
			default:
				break;
		}

        #endregion

        GUILayout.EndVertical();
	}

	protected override void AddNode()
	{
		StoryNode nodeRoot = new StoryNode();
		nodeRoot.WindowRect = new Rect(mousePosition.x, mousePosition.y, 300, 300);
		nodeRootList.Add(nodeRoot);
	}

	private void Save()
	{
		SaveFileDlg pth = new SaveFileDlg();
		pth.structSize = Marshal.SizeOf(pth);
		pth.filter = "bytes (*.bytes)";
		pth.file = new string(new char[256]);
		pth.maxFile = pth.file.Length;
		pth.fileTitle = new string(new char[64]);
		pth.maxFileTitle = pth.fileTitle.Length;
		pth.initialDir = Application.dataPath + "/HotUpdateRes/Story";  // default path
		UDebug.Log(Application.dataPath + "/HotUpdateRes/Story");
		pth.title = "保存story数据";
		pth.defExt = "bytes";
		pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

		Dictionary<string, int> plottext = new Dictionary<string, int>();
        string path = Application.dataPath + "/HotUpdateRes/Config/PlotTextConfig.txt";
        if(!File.Exists(path))
        {
            UDebug.LogError("can not find file : " + path);
            return;
        }
        string[] text = File.ReadAllLines(path);
		bool isChangeConfig = false;
		for (int i = 2; i < text.Length; i++)
		{
			if (string.IsNullOrEmpty(text[i])) continue;
			string[] plot = text[i].Split('\t');
			try
			{
				plottext.Add(plot[1], int.Parse(plot[0]));
			}
			catch (Exception e)
			{
				UDebug.Log(e.Message);
				UDebug.LogError("第一段：" + plot[0]);
				UDebug.LogError("第二段：" + plot[1]);
				throw;
			}
		}
		for (int i = 0; i < nodeRootList.Count; i++)
		{
			if (string.IsNullOrEmpty(((StoryNode)nodeRootList[i]).text))
				((StoryNode)nodeRootList[i]).textId = 0;
			else if (plottext.ContainsKey(((StoryNode)nodeRootList[i]).text))
			{
				((StoryNode)nodeRootList[i]).textId = plottext[((StoryNode)nodeRootList[i]).text];
			}
			else
			{
				int pltid = plottext[plottext.Keys.Last<string>()] + 1;
				plottext.Add(((StoryNode)nodeRootList[i]).text, pltid);
				((StoryNode)nodeRootList[i]).textId = pltid;
				isChangeConfig = true;
			}
		}
		if (isChangeConfig)
		{
			string[] newtext = new string[plottext.Count + 2];
			int i = 2;
			newtext[0] = "id" + "\t" + "text";
			newtext[1] = "int" + "\t" + "string";
			foreach (var item in plottext)
			{
				newtext[i] = item.Value + "\t" + item.Key;
				i++;
			}
			File.WriteAllLines(Application.dataPath + "/HotUpdateRes/Config/PlotTextConfig.txt", newtext);
		}

		if (SaveFileDialog.GetSaveFileName(pth))
		{
			string filepath = pth.file;//选择的文件路径;
			if (!OnSaveCheck())
			{
				return;
			}
			bool isSuccess = BinaryUtil.ObjectToFile(filepath, nodeRootList[0]);
			if (isSuccess)
			{
				path = filepath;
				UDebug.Log("Save succeed!");
			}
			else
				UDebug.LogError("Save failed!");
		}
	}
	public string Read(int id)
	{
		try
		{
			Dictionary<int, string> textDic = new Dictionary<int, string>();

			string[] newtext = File.ReadAllLines(Application.dataPath + "/HotUpdateRes/Config/PlotTextConfig.txt");
			for (int i = 2; i < newtext.Length; i++)
			{
				if (string.IsNullOrEmpty(newtext[i])) continue;
				string s = newtext[i];
				string[] a = s.Split('\t');
				textDic.Add(int.Parse(a[0]), a[1]);
			}
			return textDic[id];
		}
		catch (Exception)
		{
			UDebug.Log(id);
		}
		return null;
	}

	private bool OnSaveCheck()
	{
		nameDic = new Dictionary<string, int>();
		string[] contents = File.ReadAllLines(Application.dataPath + "/Character.csv");
		for (int i = 0; i < contents.Length; i++)
		{
			if (string.IsNullOrEmpty(contents[i]))
				continue;
			string kv = contents[i];
			string[] kvArray = kv.Split(',');
			nameDic.Add(kvArray[1], int.Parse(kvArray[0]));
		}
		for (int i = 0; i < nodeRootList.Count; i++)
		{
			StoryNode sn = nodeRootList[i] as StoryNode;
			if (string.IsNullOrEmpty(sn.characterName))
			{
				sn.roleId = 0;
				continue;
			}
			else if (!nameDic.ContainsKey(sn.characterName))
			{
				UDebug.LogError(sn.characterName + " is not exist!");
				nameDic.Clear();
				return false;
			}
			else
			{
				sn.roleId = nameDic[sn.characterName];
			}
		}
		nameDic.Clear();
		return true;
	}

	public string getrole(int id)
	{
		Dictionary<int, string> roleDic = new Dictionary<int, string>();
		string[] contents = File.ReadAllLines(Application.dataPath + "/Character.csv");
		for (int i = 0; i < contents.Length; i++)
		{
			string kv = contents[i];
			string[] kvArray = kv.Split(',');
			roleDic.Add(int.Parse(kvArray[0]), kvArray[1]);
		}
		return roleDic[id];
	}

	private void Load()
	{
		OpenFileDlg pth = new OpenFileDlg();
		pth.structSize = Marshal.SizeOf(pth);
		pth.filter = "bytes (*.bytes)";
		pth.file = new string(new char[256]);
		pth.maxFile = pth.file.Length;
		pth.fileTitle = new string(new char[64]);
		pth.maxFileTitle = pth.fileTitle.Length;
		pth.initialDir = Application.dataPath + "/HotUpdateRes/Story";  // default path  
		pth.title = "打开story文件";
		pth.defExt = "bytes";
		pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
		if (OpenFileDialog.GetOpenFileName(pth))
		{
			string filepath = pth.file;//选择的文件路径;
			ClearAllNode();
			path = pth.file;
			StoryNode sn = BinaryUtil.FileToObject(filepath) as StoryNode;
			AddNodeRecursive(sn);
		}
	}

	private void AddNodeRecursive(StoryNode sn)
	{
		if (sn.textId != 0) sn.text = Read(sn.textId);
		else sn.text = string.Empty;
		if (sn.roleId != 0) sn.characterName = getrole(sn.roleId);
		if (!nodeRootList.Contains(sn))
			nodeRootList.Add(sn);
		if (sn.childNodeList == null || sn.childNodeList.Count == 0)
			return;
		for (int i = 0; i < sn.childNodeList.Count; i++)
		{
			AddNodeRecursive((StoryNode)sn.childNodeList[i]);
		}
	}
}
#endif