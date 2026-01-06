using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class TextScroller : MonoBehaviour
{
	// Token: 0x0600006D RID: 109 RVA: 0x00005175 File Offset: 0x00003375
	private void Start()
	{
		this.isPlaying = false;
		this.DisplayMessageInstant("Hello there. I feel a bit silly for reverseing this whole tinh.");
		this.DisplayMessage("Hello there. I feel a bit silly for reverseing this whole tinh.");
		this.DisplayMessage("Oh my. What a grand adventure!");
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000519F File Offset: 0x0000339F
	private void Update()
	{
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000051A1 File Offset: 0x000033A1
	private void DisplayMessageInstant(string text)
	{
		if (this.isPlaying)
		{
			base.StopCoroutine(this.textCoroutine);
		}
		this.textUI.text = text;
		this.textUI.maxVisibleCharacters = text.Length;
		this.isPlaying = false;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x000051DC File Offset: 0x000033DC
	public void DisplayMessage(string text)
	{
		if (this.isPlaying)
		{
			base.StopCoroutine(this.textCoroutine);
		}
		this.textUI.text = text;
		this.textUI.maxVisibleCharacters = 0;
		this.textCoroutine = base.StartCoroutine(this.DisplayMessageOverTime(text));
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00005228 File Offset: 0x00003428
	private IEnumerator DisplayMessageOverTime(string text)
	{
		this.isPlaying = true;
		int num = 0;
		while (num < text.Length)
		{
			int num2 = num;
			num = num2 + 1;
			this.textUI.maxVisibleCharacters = num;
			float time = 0f;
			this.textUI.ForceMeshUpdate(false, false);
			TMP_TextInfo textInfo = this.textUI.textInfo;
			while (time < this.speedPerLetter)
			{
				time += Time.deltaTime;
				TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[num - 1];
				Vector3[] vertices = textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].vertices;
				if (!tmp_CharacterInfo.isVisible)
				{
					break;
				}
				this.textUI.ForceMeshUpdate(false, false);
				Vector3[] array = vertices;
				float num3 = Mathf.Lerp(10f, 0f, time / this.speedPerLetter);
				array[tmp_CharacterInfo.vertexIndex] = vertices[tmp_CharacterInfo.vertexIndex] + new Vector3(-1f, -1f, 0f) * num3;
				array[tmp_CharacterInfo.vertexIndex + 1] = vertices[tmp_CharacterInfo.vertexIndex + 1] + new Vector3(-1f, 1f, 0f) * num3;
				array[tmp_CharacterInfo.vertexIndex + 2] = vertices[tmp_CharacterInfo.vertexIndex + 2] + new Vector3(1f, 1f, 0f) * num3;
				array[tmp_CharacterInfo.vertexIndex + 3] = vertices[tmp_CharacterInfo.vertexIndex + 3] + new Vector3(1f, -1f, 0f) * num3;
				for (int i = 0; i < textInfo.meshInfo.Length; i++)
				{
					TMP_MeshInfo tmp_MeshInfo = textInfo.meshInfo[i];
					tmp_MeshInfo.mesh.vertices = tmp_MeshInfo.vertices;
					this.textUI.UpdateGeometry(tmp_MeshInfo.mesh, i);
				}
				yield return null;
			}
			textInfo = null;
		}
		this.isPlaying = false;
		yield break;
	}

	// Token: 0x0400003F RID: 63
	[SerializeField]
	private TextMeshProUGUI textUI;

	// Token: 0x04000040 RID: 64
	[SerializeField]
	private float speedPerLetter = 0.05f;

	// Token: 0x04000041 RID: 65
	private bool isPlaying;

	// Token: 0x04000042 RID: 66
	private Coroutine textCoroutine;
}
