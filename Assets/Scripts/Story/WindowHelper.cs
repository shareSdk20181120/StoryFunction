#if UNITY_EDITOR
namespace GameEditor.Window
{
	using System;
	using System.Runtime.InteropServices;

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class FileDlg
	{
		public int structSize = 0;
		public IntPtr dlgOwner = IntPtr.Zero;//���ڱ�ʾָ����߾����ƽ̨�ض�����  ����C#����Win32 APIʱ������C#����c/c++д��dllʱ
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
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]//StructLayout--�������ṹ�����ֶ����й��ڴ��е��������֡����ഫ�ݸ���Ҫָ�����ֵķ��йܴ��룬����ʾ�����಼�ֺ���Ҫ��
    //LayoutKind.Sequential--����Ա��������ֵ�˳����в���  CharSet = CharSet.Auto--����ṹ�е��ַ�����Ա������Dllʱ������ʽ
    public class SaveFileDlg : FileDlg
	{

	}


}
#endif