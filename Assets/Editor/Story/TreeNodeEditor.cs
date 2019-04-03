#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeNodeEditor : EditorWindow
{
	// 保存窗口中所有节点
	protected static List<NodeRoot> nodeRootList = new List<NodeRoot>();
	// 当前选择的节点
	protected NodeRoot selectNode = null;

	protected static NodeRoot cpoyNode = null;
	// 鼠标的位置
	protected Vector2 mousePosition;
	// 添加连线
	protected bool makeTransitionMode = false;
	Vector2 scrollPos;
	protected string path = string.Empty;

	/// # 代表 shift	& 代表 alt		% 代表 Ctrl
	[MenuItem("GameObject/Copy Current Components &C")]
	protected static void Copy()
	{
		if (cpoyNode != null)
		{
			NodeRoot cop = ((StoryNode)cpoyNode).Clone();
			nodeRootList.Add(cop);
			cpoyNode = cop;
		}
	}

	// 添加节点
	/// # 代表 shift	& 代表 alt		% 代表 Ctrl
	[MenuItem("GameObject/Copy Current Components &Q")]
	public static void MoreNode()
	{
		NodeRoot nodeRoot = new StoryNode();
		nodeRoot.WindowRect = new Rect(100, 100, 300, 300);
		nodeRootList.Add(nodeRoot);
	}

	public virtual void OnGUI()
	{
		OnGuiDraw();
        Rect rect = new Rect(0, 0, position.width > 200 ? position.width : 200,position.height>200 ?position.height:200); 
		scrollPos = GUI.BeginScrollView(rect, scrollPos, new Rect(0, 0, 10000, 10000));
		Event _event = Event.current;
		mousePosition = _event.mousePosition;

		//遍历所有节点，移除无效节点
		for (int i = nodeRootList.Count - 1; i >= 0; --i)
		{
			if (nodeRootList[i].isRelease)
			{
				nodeRootList.RemoveAt(i);
			}
		}

		if (_event.button == 0)
			if (_event.type == EventType.MouseDown)
			{
				int copyIndex = 0;
				cpoyNode = GetMouseInNode(out copyIndex);
			}

		if (_event.button == 1) // 鼠标右键
		{
			if (_event.type == EventType.MouseDown)
			{
				if (!makeTransitionMode)
				{
					bool clickedOnNode = false;
					int selectIndex = 0;
					selectNode = GetMouseInNode(out selectIndex);
					clickedOnNode = (selectNode != null);

					if (!clickedOnNode)
					{
						ShowMenu(0);
					}
					else
					{
						ShowMenu(1);
					}
				}
			}
		}

		// 选择节点为空时，无法连线
		if (selectNode == null)
		{
			makeTransitionMode = false;
		}

		if (!makeTransitionMode)
		{
			if (_event.type == EventType.MouseUp)
			{
				selectNode = null;
			}
		}

		// 在连线状态，按下鼠标
		if (makeTransitionMode && _event.type == EventType.MouseDown)
		{
			int selectIndex = 0;
			NodeRoot newSelectNode = GetMouseInNode(out selectIndex);
			// 如果按下鼠标时，选中了一个节点，则将 新选中根节点 添加为 selectNode 的子节点
			if (selectNode != newSelectNode)
			{
				newSelectNode.parent = selectNode;
				selectNode.childNodeList.Add(newSelectNode);
			}

			// 取消连线状态
			makeTransitionMode = false;
			// 清空选择节点
			selectNode = null;
		}

		// 连线状态下 选择节点不为空 
		if (makeTransitionMode && selectNode != null)
		{
			// 获取鼠标位置
			Rect mouseRect = new Rect(mousePosition.x, mousePosition.y, 10, 10);
			// 显示连线 从 选中节点到 鼠标位置的线
			DrawNodeCurve(selectNode.WindowRect, mouseRect);
		}

		// 开始绘制节点 
		// 注意：必须在  BeginWindows(); 和 EndWindows(); 之间 调用 GUI.Window 才能显示
		BeginWindows();
		for (int i = 0; i < nodeRootList.Count; i++)
		{
			NodeRoot nodeRoot = nodeRootList[i];
			nodeRoot.WindowRect = GUI.Window(i, nodeRoot.WindowRect, DrawNodeWindow, "Node" + i);

			DrawToChildCurve(nodeRoot);
		}
		EndWindows();

		// 重新绘制
		Repaint();

		GUI.EndScrollView();
	}
	void DrawNodeWindow(int id)
	{
		if (id >= nodeRootList.Count)
			return;
		NodeRoot nodeRoot = nodeRootList[id];
		// 可拖拽位置的 window
		OnDrawNodeWindow(nodeRoot);
		GUI.DragWindow();
	}

	protected virtual void OnGuiDraw()
	{

	}

	protected virtual void OnDrawNodeWindow(NodeRoot node)
	{

	}

	// 获取鼠标所在位置的节点
	private NodeRoot GetMouseInNode(out int index)
	{
		index = 0;
		NodeRoot selectRoot = null;
		for (int i = 0; i < nodeRootList.Count; i++)
		{
			NodeRoot nodeRoot = nodeRootList[i];
			// 如果鼠标位置 包含在 节点的 Rect 范围，则视为可以选择的节点
			if (nodeRoot.WindowRect.Contains(mousePosition))
			{
				selectRoot = nodeRoot;
				index = i;
				break;
			}
		}

		return selectRoot;
	}

	private void ShowMenu(int type)
	{
		GenericMenu menu = new GenericMenu();
		if (type == 0)
		{
			// 添加一个新节点
			menu.AddItem(new GUIContent("Add Node"), false, AddNode);
		}
		else
		{
			// 连线子节点
			menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition);
			menu.AddSeparator("");
			// 删除节点
			menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
		}

		menu.ShowAsContext();
		Event.current.Use();
	}


	protected virtual void AddNode()
	{
		NodeRoot nodeRoot = new NodeRoot();
		nodeRoot.WindowRect = new Rect(mousePosition.x, mousePosition.y, 300, 300);
		nodeRootList.Add(nodeRoot);
	}

	// 连线子节点
	private void MakeTransition()
	{
		makeTransitionMode = true;
	}

	// 删除节点
	protected void DeleteNode()
	{
		int selectIndex = 0;
		selectNode = GetMouseInNode(out selectIndex);
		if (selectNode != null)
		{
			nodeRootList[selectIndex].Release();
			nodeRootList.Remove(selectNode);
		}
	}

	// 清除所有节点
	protected void ClearAllNode()
	{
		path = string.Empty;
		while (nodeRootList.Count > 0)
		{
			nodeRootList[0].Release();
			nodeRootList.RemoveAt(0);
		}
	}

	/// <summary>
	/// 每帧绘制从 节点到所有子节点的连线
	/// </summary>
	/// <param name="nodeRoot"></param>
	private void DrawToChildCurve(NodeRoot nodeRoot)
	{
		for (int i = nodeRoot.childNodeList.Count - 1; i >= 0; --i)
		{
			NodeRoot childNode = nodeRoot.childNodeList[i];
			// 删除无效节点
			if (childNode == null || childNode.isRelease)
			{
				nodeRoot.childNodeList.RemoveAt(i);
				continue;
			}
			DrawNodeCurve(nodeRoot.WindowRect, childNode.WindowRect);
		}
	}

	// 绘制线
	public static void DrawNodeCurve(Rect start, Rect end)
	{
		Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
		Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);

		Handles.DrawLine(startPos, endPos);
	}
}
#endif