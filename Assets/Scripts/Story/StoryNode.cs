using System;
using Tool;
using UnityEngine;

[Serializable]
public class StoryNode : NodeRoot
{
	//立绘ID
	public int roleId;
	//对话ID
	public int textId;
	//是否自动
	public bool autoPlay;
	//背景图
	public string backGround1 = string.Empty;
	//背景图
	public string backGround2 = string.Empty;
	//剧情背景音乐
	public string BGM = string.Empty;
	public float fadeTime = 0f;
	public bool BGMLoop = false;
	//表情音效
	public string soundEffect = string.Empty;
	//对话文本
	[NonSerialized]
	public string text = string.Empty;
	//立绘名
	[NonSerialized]
	public string characterName = string.Empty;
	//角色类型（立绘/通讯窗/雪花通讯窗）
	public Enum_Story_Role roleType;
	//角色位置（左/中/右）
	public Enum_Story_Location location;
	//延迟时间
	public float delayTime;
	//动画类型（出场，退场，平移）
	public Enum_Story_RoleAnim animType;
    /// <summary> 剧情类型（旁白/对话/选择）    /// </summary>
    public Enum_Story_Type type;
	//表情类型
	public Enum_Story_Emoji emoji;
	//BGM类型
	public Enum_Story_BGM bgmType;

	public NodeRoot Clone()
	{
		StoryNode sn = (StoryNode)base.Copy();
		sn.roleId = this.roleId;
		sn.textId = this.textId;
		sn.autoPlay = this.autoPlay;
		sn.backGround1 = this.backGround1;
		sn.backGround2 = this.backGround2;
		sn.BGM = this.BGM;
		sn.fadeTime = this.fadeTime;
		sn.BGMLoop = this.BGMLoop;
		sn.soundEffect = this.soundEffect;
		sn.text = this.text;
		sn.characterName = this.characterName;
		sn.roleType = this.roleType;
		sn.location = this.location;
		sn.delayTime = this.delayTime;
		sn.animType = this.animType;
		sn.type = this.type;
		sn.emoji = this.emoji;
		sn.bgmType = this.bgmType;
		return sn;
	}
}

/// <summary>立绘动画</summary>
public enum Enum_Story_RoleAnim
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>登场</summary>
	[EnumAlias("登场")]
	Appear = 1,
	/// <summary>退场</summary>
	[EnumAlias("退场")]
	Disappear = 2,
	/// <summary>平移</summary>
	[EnumAlias("平移")]
	Move = 3,
	/// <summary>跳动</summary>
	[EnumAlias("跳动")]
	Jump = 4,
	/// <summary>震动</summary>
	[EnumAlias("震动")]
	Rock = 5,
}

/// <summary>角色类型</summary>
public enum Enum_Story_Role
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>立绘</summary>
	[EnumAlias("立绘")]
	Normal = 1,
	/// <summary>通讯窗</summary>
	[EnumAlias("通讯窗")]
	Window = 2,
	/// <summary>雪花窗</summary>
	[EnumAlias("雪花窗")]
	NoiseWindow = 3,
}

/// <summary>角色位置</summary>
public enum Enum_Story_Location
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>左</summary>
	[EnumAlias("左")]
	Left = 1,
	/// <summary>中</summary>
	[EnumAlias("中")]
	Center = 2,
	/// <summary>右</summary>
	[EnumAlias("右")]
	Right = 3,
}

/// <summary>剧情类型  对话、旁白、转场、选择、冒泡、字母</summary>
public enum Enum_Story_Type
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>对话</summary>
	[EnumAlias("对话")]
	Dialog = 1,
	/// <summary>旁白</summary>
	[EnumAlias("旁白")]
	Aside = 2,
	/// <summary>转场</summary>
	[EnumAlias("转场")]
	CutTo = 3,
	/// <summary>选择</summary>
	[EnumAlias("选择")]
	Choice = 4,
	/// <summary>冒泡</summary>
	[EnumAlias("冒泡")]
	Bubble = 5,
	/// <summary>字幕</summary>
	[EnumAlias("字幕")]
	VoiceOver = 6,
}

/// <summary>文字动画  打字、渐显</summary>
public enum Enum_Story_TextType
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>打字</summary>
	[EnumAlias("打字")]
	Narrator = 1,
	/// <summary>渐显</summary>
	[EnumAlias("渐显")]
	Alpha = 2,
	//Roll = 3,
	//Messy = 4,
	//Shake = 5,
	//Jump = 6,
	//Glich = 7,
}

/// <summary>立绘表情</summary>
public enum Enum_Story_Emoji
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>吃惊</summary>
	[EnumAlias("吃惊")]
	ChiJing = 1,
	/// <summary>大声</summary>
	[EnumAlias("大声")]
	DaSheng = 2,
	/// <summary>犯困</summary>
	[EnumAlias("犯困")]
	FanKun = 3,
	/// <summary>反应</summary>
	[EnumAlias("反应")]
	FanYing = 4,
	/// <summary>高兴</summary>
	[EnumAlias("高兴")]
	GaoXing = 5,
	/// <summary>害怕</summary>
	[EnumAlias("害怕")]
	HaiPa = 6,
	/// <summary>害羞</summary>
	[EnumAlias("害羞")]
	HaiXiu = 7,
	/// <summary>慌张</summary>
	[EnumAlias("慌张")]
	HuangZhang = 8,
	/// <summary>混乱</summary>
	[EnumAlias("混乱")]
	HunLuan = 9,
	/// <summary>灵感</summary>
	[EnumAlias("灵感")]
	LingGan = 10,
	/// <summary>生气</summary>
	[EnumAlias("生气")]
	ShengQi = 11,
	/// <summary>疑问</summary>
	[EnumAlias("疑问")]
	YiWen = 12,
	/// <summary>郁闷</summary>
	[EnumAlias("郁闷")]
	YuMen = 13,
	/// <summary>自信</summary>
	[EnumAlias("自信")]
	ZiXin = 14,
}

/// <summary> 背景音乐 </summary>
public enum Enum_Story_BGM
{
	/// <summary>无</summary>
	[EnumAlias("无")]
	None = 0,
	/// <summary>播放</summary>
	[EnumAlias("播放")]
	Play = 1,
	/// <summary>暂停</summary>
	[EnumAlias("暂停")]
	Pause = 2,
	/// <summary>继续</summary>
	[EnumAlias("继续")]
	Continue = 3,
	/// <summary>停止</summary>
	[EnumAlias("停止")]
	Stop = 4,
	/// <summary>再续前曲</summary>
	[EnumAlias("再续前曲")]
	StopAndContinueLast = 5,
}