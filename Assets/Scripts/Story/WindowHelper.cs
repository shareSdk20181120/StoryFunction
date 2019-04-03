#if UNITY_EDITOR
namespace GameEditor.Window
{
	using System;
	using System.Runtime.InteropServices;

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class FileDlg
	{
		public int structSize = 0;
		public IntPtr dlgOwner = IntPtr.Zero;//用于表示指针或者句柄的平台特定类型  用在C#调用Win32 API时，或者C#调用c/c++写的dll时
		public IntPtr instance = IntPtr.Zero;
		public String filter = null;
		public String customFilter = null;
		public int maxCustFilter = 0;
		public int filterIndex = 0;
		public String file = null;
		public int maxFile = 0;
		public String fileTitle = null;
		public int maxFileTitle = 0;
		public String initialDir = null;
		public String title = null;
		public int flags = 0;
		public short fileOffset = 0;
		public short fileExtension = 0;
		public String defExt = null;
		public IntPtr custData = IntPtr.Zero;
		public IntPtr hook = IntPtr.Zero;
		public String templateName = null;
		public IntPtr reservedPtr = IntPtr.Zero;
		public int reservedInt = 0;
		public int flagsEx = 0;
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class OpenFileDlg : FileDlg
	{

	}
	public class OpenFileDialog
	{
		[DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
		public static extern bool GetOpenFileName([In, Out] OpenFileDlg ofd);
	}
	public class SaveFileDialog
	{
		[DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
		public static extern bool GetSaveFileName([In, Out] SaveFileDlg ofd);
	}
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]//StructLayout--控制类或结构数据字段在托管内存中的物理布局。将类传递给需要指定布局的非托管代码，则显示控制类布局很重要。
    //LayoutKind.Sequential--将成员按照其出现的顺序进行布局  CharSet = CharSet.Auto--定义结构中的字符串成员被传给Dll时的排序方式
    public class SaveFileDlg : FileDlg
	{

	}


}
#endif