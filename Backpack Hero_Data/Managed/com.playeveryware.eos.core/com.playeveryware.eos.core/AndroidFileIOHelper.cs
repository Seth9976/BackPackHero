using System;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000002 RID: 2
public class AndroidFileIOHelper : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static string ReadAllText(string filePath)
	{
		string text;
		using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(filePath))
		{
			unityWebRequest.timeout = 2;
			unityWebRequest.SendWebRequest();
			while (!unityWebRequest.isDone)
			{
			}
			if (unityWebRequest.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Requesting " + filePath + ", please make sure it exists and is a valid config");
				throw new Exception("UnityWebRequest didn't succeed, Result : " + unityWebRequest.result.ToString());
			}
			text = unityWebRequest.downloadHandler.text;
		}
		return text;
	}
}
