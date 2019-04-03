using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NodeRoot
{
	public NodeRoot parent = null;
	// 子节点
	public List<NodeRoot> childNodeList = new List<NodeRoot>();

	// 在窗口中显示位置
	public SRect windowRect = new SRect(0, 0, 100, 100);

	/// <summary>
	/// 是否为有效节点， isRelease = true 为已经销毁的节点，为无效节点
	/// </summary>
	[NonSerialized]
	public bool isRelease = false;
	/// <summary>
	/// 删除节点时调用
	/// </summary>
	public void Release()
	{
		isRelease = true;
	}

	public Rect WindowRect
	{
		get { return new Rect(windowRect.x, windowRect.y, 300, 300); }
		set
		{
			windowRect.x = value.x;
			windowRect.y = value.y;
			windowRect.width = value.width;
			windowRect.height = value.height;
		}
	}

	protected NodeRoot Copy()
	{
		NodeRoot nr = new StoryNode();
		nr.WindowRect = this.WindowRect;
		return nr;
	}
}

[Serializable]
public struct SRect
{
	public float x;
	public float y;
	public float width;
	public float height;

	public SRect(float xx, float yy, float ww, float hh)
	{
		x = xx;
		y = yy;
		width = ww;
		height = hh;
	}
}