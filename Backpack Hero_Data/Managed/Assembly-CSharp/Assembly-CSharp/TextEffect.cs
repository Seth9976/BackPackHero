using System;
using TMPro;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class TextEffect : MonoBehaviour
{
	// Token: 0x06001093 RID: 4243 RVA: 0x0009DB97 File Offset: 0x0009BD97
	private void Start()
	{
		if (!this.text)
		{
			this.text = base.GetComponent<TextMeshProUGUI>();
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0009DBB4 File Offset: 0x0009BDB4
	private void LateUpdate()
	{
		this.text.ForceMeshUpdate(false, false);
		TMP_TextInfo textInfo = this.text.textInfo;
		if (textInfo.characterInfo.Length < this.text.maxVisibleCharacters || this.text.maxVisibleCharacters == 0 || this.text.maxVisibleCharacters >= textInfo.characterCount - 1)
		{
			return;
		}
		TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[this.text.maxVisibleCharacters - 1];
		if (!tmp_CharacterInfo.isVisible || tmp_CharacterInfo.vertexIndex <= 0)
		{
			return;
		}
		if (this.maxVisibleCharacters != this.text.maxVisibleCharacters)
		{
			this.maxVisibleCharacters = this.text.maxVisibleCharacters;
			this.time = 0f;
		}
		else
		{
			this.time += Time.deltaTime;
		}
		Vector3[] vertices = textInfo.meshInfo[tmp_CharacterInfo.materialReferenceIndex].vertices;
		if (!this.invert)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector = vertices[tmp_CharacterInfo.vertexIndex + i];
				vertices[tmp_CharacterInfo.vertexIndex + i] = vector - new Vector3(0f, Mathf.Max(0f, 4f * this.wigglyAmount - this.time * (3f * this.wigglyAmount)), 0f);
			}
		}
		else
		{
			for (int j = 0; j < 4; j++)
			{
				Vector3 vector2 = vertices[tmp_CharacterInfo.vertexIndex + j];
				vertices[tmp_CharacterInfo.vertexIndex + j] = vector2 + new Vector3(0f, Mathf.Max(0f, 0f - this.time * (3f * this.wigglyAmount)), 0f);
			}
		}
		for (int k = 0; k < textInfo.meshInfo.Length; k++)
		{
			TMP_MeshInfo tmp_MeshInfo = textInfo.meshInfo[k];
			tmp_MeshInfo.mesh.vertices = tmp_MeshInfo.vertices;
			this.text.UpdateGeometry(tmp_MeshInfo.mesh, k);
		}
	}

	// Token: 0x04000D80 RID: 3456
	[SerializeField]
	public bool invert;

	// Token: 0x04000D81 RID: 3457
	[SerializeField]
	private float wigglyAmount = 5f;

	// Token: 0x04000D82 RID: 3458
	public TextMeshProUGUI text;

	// Token: 0x04000D83 RID: 3459
	private float time;

	// Token: 0x04000D84 RID: 3460
	private int maxVisibleCharacters;
}
