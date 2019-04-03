using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class BinaryUtil
{
	public static bool ObjectToFile(string name, object obj)
	{
		Stream flstr = null;
		BinaryWriter binaryWriter = null;
		try
		{
			flstr = new FileStream(name, FileMode.Create);
			binaryWriter = new BinaryWriter(flstr);
			var buff = FormatterObjectBytes(obj);
			binaryWriter.Write(buff);
		}
		catch (Exception er)
		{
			throw new Exception(er.Message);
		}
		finally
		{
			if (binaryWriter != null) binaryWriter.Close();
			if (flstr != null) flstr.Close();
		}
		return true;
	}

	/// <summary>  
	/// ���������л�Ϊbyte[]  
	/// ʹ��IFormatter��Serialize���л�  
	/// </summary>  
	/// <param name="obj">��Ҫ���л��Ķ���</param>  
	/// <returns>���л���ȡ�Ķ�������</returns>  
	public static byte[] FormatterObjectBytes(object obj)
	{
		if (obj == null)
			throw new ArgumentNullException("obj");
		byte[] buff;
		try
		{
			using (var ms = new MemoryStream())
			{
				IFormatter iFormatter = new BinaryFormatter();
				iFormatter.Serialize(ms, obj);
				buff = ms.GetBuffer();
			}
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
		return buff;
	}

	/// <summary>  
	/// ���������л�Ϊbyte[]  
	/// ʹ��IFormatter��Serialize���л�  
	/// </summary>  
	/// <param name="obj">��Ҫ���л��Ķ���</param>  
	/// <returns>���л���ȡ�Ķ�������</returns>  
	public static object FormatterBytesObject(byte[] bytes)
	{
		if (bytes == null || bytes.Length == 0)
			throw new ArgumentNullException("obj");
		object obj;
		try
		{
			using (var ms = new MemoryStream(bytes))
			{
				IFormatter iFormatter = new BinaryFormatter();
				obj = iFormatter.Deserialize(ms);
			}
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
		return obj;
	}

	/// <summary>
	/// �ļ������л�Ϊ����
	/// </summary>
	/// <param name="path">�ļ�·��</param>
	/// <returns></returns>
	public static object FileToObject(string path)     // �����Ʒ����л�  
	{
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter();
		object obj = null;
		try
		{
			obj = bf.Deserialize(fs);
			fs.Close();
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
		return obj;
	}
}