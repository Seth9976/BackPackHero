using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000183 RID: 387
public class Credits : MonoBehaviour
{
	// Token: 0x06000F8D RID: 3981 RVA: 0x000976ED File Offset: 0x000958ED
	private void Start()
	{
		if (this.mask)
		{
			this.mask.enabled = true;
		}
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x00097708 File Offset: 0x00095908
	private void Update()
	{
		this.contentMaskTransform.anchoredPosition += new Vector2(0f, DigitalCursor.main.GetHighestVector(DigitalCursor.VectorType.locked, 0.05f).y * -300f);
		if (this.limit)
		{
			this.contentMaskTransform.anchoredPosition = new Vector2(0f, Mathf.Clamp(this.contentMaskTransform.anchoredPosition.y, 0f, 388542.6f));
		}
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0009778C File Offset: 0x0009598C
	public void UpdateText()
	{
		float y = this.contentMaskTransform.anchoredPosition.y;
		float y2 = this.kickstarterList.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;
		float num = y + y2;
		float height = this.contentBox.rect.height;
		int num2 = Mathf.RoundToInt((num - 100f) / this.fontLineHeight);
		num2 = Mathf.RoundToInt((float)(num2 / 4)) * 4;
		string line = this.GetLine(this.path, num2, 36);
		this.kickstarterList.text = line;
		this.kickstarterList.rectTransform.localPosition = new Vector2(0f, Mathf.Min(0f, -1f * ((float)num2 * this.fontLineHeight)));
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x00097854 File Offset: 0x00095A54
	private string GetLine(string fileName, int line, int lineMax)
	{
		this.totalLines = 0;
		line = Mathf.Clamp(line, 0, 100000);
		string text = "";
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			string[] array = Resources.Load<TextAsset>("Kickstarter List/KickstarterList").text.Split("\n", StringSplitOptions.None);
			for (int i = line; i < line + lineMax; i++)
			{
				text += array[i];
				text += "\n";
			}
		}
		else
		{
			using (StreamReader streamReader = new StreamReader(fileName))
			{
				for (int j = 0; j < line; j++)
				{
					this.GetNextName(streamReader);
				}
				for (int k = line; k < line + lineMax; k++)
				{
					string text2 = this.GetNextName(streamReader);
					if (text2 == "")
					{
						break;
					}
					text += text2;
					for (int l = 0; l < 3; l++)
					{
						text2 = this.GetNextName(streamReader);
						if (text2 == "")
						{
							break;
						}
						text = text + ",  " + text2;
					}
					text += "\n";
				}
			}
		}
		return text;
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0009797C File Offset: 0x00095B7C
	private string GetNextName(StreamReader sr)
	{
		if (sr.EndOfStream)
		{
			return "";
		}
		string text = sr.ReadLine();
		this.totalLines++;
		while (this.lastLine == text)
		{
			text = sr.ReadLine();
			this.totalLines++;
			if (sr.EndOfStream)
			{
				return "";
			}
		}
		this.lastLine = text;
		return text;
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x000979E8 File Offset: 0x00095BE8
	public void EndEvent()
	{
		if (this.finished)
		{
			return;
		}
		this.finished = true;
		this.eventBoxAnimator.Play("Out");
		Singleton.Instance.showingOptions = false;
		GameManager main = GameManager.main;
		if (main)
		{
			main.SetAllItemColliders(true);
		}
		MenuManager main2 = MenuManager.main;
		if (main2)
		{
			main2.ShowButtons();
		}
	}

	// Token: 0x04000CAA RID: 3242
	[SerializeField]
	private float fontLineHeight = 41.4f;

	// Token: 0x04000CAB RID: 3243
	[SerializeField]
	private bool limit = true;

	// Token: 0x04000CAC RID: 3244
	[SerializeField]
	private TextMeshProUGUI kickstarterList;

	// Token: 0x04000CAD RID: 3245
	[SerializeField]
	private RectTransform contentMaskTransform;

	// Token: 0x04000CAE RID: 3246
	[SerializeField]
	private RectTransform contentBox;

	// Token: 0x04000CAF RID: 3247
	[SerializeField]
	private Animator eventBoxAnimator;

	// Token: 0x04000CB0 RID: 3248
	[SerializeField]
	private Mask mask;

	// Token: 0x04000CB1 RID: 3249
	[SerializeField]
	private RectTransform nextPartOfCredits;

	// Token: 0x04000CB2 RID: 3250
	private bool finished;

	// Token: 0x04000CB3 RID: 3251
	private string path = Application.streamingAssetsPath + "\\KickstarterList.txt";

	// Token: 0x04000CB4 RID: 3252
	private int totalLines;

	// Token: 0x04000CB5 RID: 3253
	private string lastLine = "";
}
