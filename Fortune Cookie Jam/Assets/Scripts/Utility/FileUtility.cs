using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
public static class FileUtility{
	public static bool filesOK;

	public static string LoadJsonFromFile(string path, bool isResource)
	{
		if(isResource){
			string filePath = path.Replace(".json", "");
			Debug.Log(filePath);

			TextAsset targetFile = Resources.Load<TextAsset>(filePath); 
			Debug.Log(targetFile.text);
			return targetFile.text;
		} else {
			return LoadJsonFromNonResource(Application.persistentDataPath + path);
		}
	}

	private static string LoadJsonFromNonResource(string path)
	{
		Debug.LogWarning(path);
		StreamReader reader = new StreamReader(path);
		string response = "";
		while(!reader.EndOfStream)
		{
			response += reader.ReadLine();
		}
		reader.Close();
		return response;
	}

	public static void WriteJsonToFile(string path, string content)
	{
		StreamWriter writer = new StreamWriter(path);
		writer.WriteLine(content);
		writer.Close();
	}

	public static bool checkFile(string addPath){
		string path = Application.persistentDataPath + addPath;
		if (!System.IO.File.Exists(path)){
			return false;
		} else {
			return true;
		}
	}

	public static bool initFile(string addPath, string dataToPlace){
		string path = Application.persistentDataPath + addPath;
		if (!System.IO.File.Exists(path)){
			 // Create the file.
			using (FileStream fs = File.Create(path))
			{
				Byte[] info = new UTF8Encoding(true).GetBytes(dataToPlace);
				// Add some information to the file.
				fs.Write(info, 0, info.Length);
			}
			return true;
		} else {
			return true;
		}
	}
}