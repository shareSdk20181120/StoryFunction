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
	/// 将对象序列化为byte[]  
	/// 使用IFormatter的Serialize序列化  
	/// </summary>  
	/// <param name="obj">需要序列化的对象</param>  
	/// <returns>序列化获取的二进制流</returns>  
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
	/// 将对象序列化为byte[]  
	/// 使用IFormatter的Serialize序列化  
	/// </summary>  
	/// <param name="obj">需要序列化的对象</param>  
	/// <returns>序列化获取的二进制流</returns>  
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
	/// 文件反序列化为对象
	/// </summary>
	/// <param name="path">文件路径</param>
	/// <returns></returns>
	public static object FileToObject(string path)     // 二进制反序列化  
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