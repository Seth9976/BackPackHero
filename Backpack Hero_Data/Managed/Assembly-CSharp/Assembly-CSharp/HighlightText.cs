using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class HighlightText : MonoBehaviour
{
	// Token: 0x060008DE RID: 2270 RVA: 0x0005CBF0 File Offset: 0x0005ADF0
	private string ReplaceText(string text, string key, string emojiName)
	{
		string text2 = LangaugeManager.main.GetTextByKey(key).ToLower();
		string text3 = string.Format("\\b{0}\\b", text2);
		return Regex.Replace(text.ToLower(), text3, "<sprite name=\"" + emojiName + "\">" + text2);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0005CC38 File Offset: 0x0005AE38
	private void Start()
	{
		this.text = base.GetComponentInChildren<TextMeshProUGUI>();
		this.canvas = base.GetComponentInParent<Canvas>();
		this.cardParent = base.GetComponentInParent<Card>();
		if (this.text.fontStyle == FontStyles.Italic)
		{
			return;
		}
		if (Singleton.Instance.showEmojis && !this.disableEmoji)
		{
			this.ShowEmojis();
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0005CC94 File Offset: 0x0005AE94
	public void ShowEmojis()
	{
		this.text.text = this.ReplaceText(this.text.text, "gold", "spriteemojiatlas_73");
		this.text.text = this.ReplaceText(this.text.text, "se1", "spriteemojiatlas_1");
		this.text.text = this.ReplaceText(this.text.text, "se2", "spriteemojiatlas_6");
		this.text.text = this.ReplaceText(this.text.text, "se3", "spriteemojiatlas_0");
		this.text.text = this.ReplaceText(this.text.text, "se4", "spriteemojiatlas_4");
		this.text.text = this.ReplaceText(this.text.text, "se5", "spriteemojiatlas_3");
		this.text.text = this.ReplaceText(this.text.text, "se6", "spriteemojiatlas_5");
		this.text.text = this.ReplaceText(this.text.text, "se7", "spriteemojiatlas_2");
		this.text.text = this.ReplaceText(this.text.text, "se8", "spriteemojiatlas_14");
		this.text.text = this.ReplaceText(this.text.text, "se9", "spriteemojiatlas_16");
		this.text.text = this.ReplaceText(this.text.text, "se10", "spriteemojiatlas_8");
		this.text.text = this.ReplaceText(this.text.text, "se11", "spriteemojiatlas_24");
		this.text.text = this.ReplaceText(this.text.text, "se12", "spriteemojiatlas_36");
		this.text.text = this.ReplaceText(this.text.text, "se13", "spriteemojiatlas_37");
		this.text.text = this.ReplaceText(this.text.text, "se14", "spriteemojiatlas_54");
		this.text.text = this.ReplaceText(this.text.text, "se15", "spriteemojiatlas_55");
		this.text.text = this.ReplaceText(this.text.text, "se16", "spriteemojiatlas_71");
		this.text.text = this.ReplaceText(this.text.text, "damage", "spriteemojiatlas_9");
		this.text.text = this.ReplaceText(this.text.text, "block", "spriteemojiatlas_10");
		this.text.text = this.ReplaceText(this.text.text, "energy", "spriteemojiatlas_17");
		this.text.text = this.ReplaceText(this.text.text, "hp", "spriteemojiatlas_18");
		this.text.text = this.ReplaceText(this.text.text, "is5", "spriteemojiatlas_20");
		this.text.text = this.ReplaceText(this.text.text, "is6b", "spriteemojiatlas_21");
		this.text.text = this.ReplaceText(this.text.text, "is7b", "spriteemojiatlas_22");
		this.text.text = this.ReplaceText(this.text.text, "is2b", "spriteemojiatlas_19");
		this.text.text = this.ReplaceText(this.text.text, "disabled", "spriteemojiatlas_25");
		this.text.text = this.ReplaceText(this.text.text, "is11", "spriteemojiatlas_60");
		this.text.text = this.ReplaceText(this.text.text, "is21", "spriteemojiatlas_58");
		this.text.text = this.ReplaceText(this.text.text, "is22", "spriteemojiatlas_72");
		this.text.text = this.ReplaceText(this.text.text, "luck", "spriteemojiatlas_31");
		this.text.text = this.ReplaceText(this.text.text, "is3b", "spriteemojiatlas_32");
		this.text.text = this.ReplaceText(this.text.text, "banished", "spriteemojiatlas_29");
		this.text.text = this.ReplaceText(this.text.text, "discarded", "spriteemojiatlas_61");
		this.text.text = this.ReplaceText(this.text.text, "mana", "spriteemojiatlas_33");
		this.text.text = this.ReplaceText(this.text.text, "food", "spriteemojiatlas_49");
		this.text.text = this.ReplaceText(this.text.text, "material", "spriteemojiatlas_50");
		this.text.text = this.ReplaceText(this.text.text, "treasure", "spriteemojiatlas_51");
		this.text.text = this.ReplaceText(this.text.text, "population", "spriteemojiatlas_53");
		this.text.text = this.ReplaceText(this.text.text, "is24", "spriteemojiatlas_74");
		this.text.text = this.ReplaceText(this.text.text, "is35", "spriteemojiatlas_80");
		this.text.text = this.ReplaceText(this.text.text, "is36", "spriteemojiatlas_57");
		this.text.text = this.ReplaceText(this.text.text, "is38", "spriteemojiatlas_103");
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0005D2E0 File Offset: 0x0005B4E0
	public string GetWord()
	{
		if (!this.text)
		{
			return "";
		}
		Vector3 vector = Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position);
		int num = TMP_TextUtilities.FindIntersectingWord(this.text, vector, Camera.main);
		if (num != -1)
		{
			this.text.textInfo.wordInfo[num].ToString();
			return this.text.textInfo.wordInfo[num].GetWord();
		}
		int num2 = TMP_TextUtilities.FindIntersectingCharacter(this.text, vector, Camera.main, true);
		if (num2 == -1)
		{
			return "";
		}
		if (!this.text.textInfo.characterInfo[num2].spriteAsset)
		{
			return "";
		}
		int spriteIndex = this.text.textInfo.characterInfo[num2].spriteIndex;
		switch (spriteIndex)
		{
		case 0:
			return "spikes";
		case 1:
			return "poison";
		case 2:
			return "weak";
		case 3:
			return "slow";
		case 4:
			return "haste";
		case 5:
			return "rage";
		case 6:
			return "regen";
		case 7:
		case 11:
		case 12:
		case 13:
		case 15:
		case 23:
		case 26:
		case 27:
		case 28:
		case 30:
		case 34:
		case 35:
		case 38:
		case 39:
		case 40:
		case 41:
		case 42:
		case 43:
		case 44:
		case 45:
		case 46:
		case 47:
		case 48:
		case 52:
		case 56:
		case 59:
		case 62:
		case 63:
		case 64:
		case 65:
		case 66:
		case 67:
		case 68:
		case 69:
		case 70:
		case 75:
		case 76:
		case 77:
		case 78:
		case 79:
			break;
		case 8:
			return "burn";
		case 9:
			return "damage";
		case 10:
			return "block";
		case 14:
			return "dodge";
		case 16:
			return "freeze";
		case 17:
			return "energy";
		case 18:
			return "hp";
		case 19:
			return "projectile";
		case 20:
			return "conductive";
		case 21:
			return "heavy";
		case 22:
			return "float";
		case 24:
			return "tough hide";
		case 25:
			return "disabled";
		case 29:
			return "banished";
		case 31:
			return "luck";
		case 32:
			return "anchored";
		case 33:
			return "mana";
		case 36:
			return "charm";
		case 37:
			return "sleep";
		case 49:
			return "food";
		case 50:
			return "material";
		case 51:
			return "treasure";
		case 53:
			return "population";
		case 54:
			return "zombie";
		case 55:
			return "exhaust";
		case 57:
			return "vampiric";
		case 58:
			return "natural";
		case 60:
			return "piercing";
		case 61:
			return "discarded";
		case 71:
			return "curse";
		case 72:
			return "cleansed";
		case 73:
			return "gold";
		case 74:
			return "ghostly";
		case 80:
			return "unique";
		default:
			if (spriteIndex == 103)
			{
				return "temporary";
			}
			break;
		}
		return "";
	}

	// Token: 0x040006FE RID: 1790
	[SerializeField]
	private GameObject simpleCard;

	// Token: 0x040006FF RID: 1791
	private TextMeshProUGUI text;

	// Token: 0x04000700 RID: 1792
	private float timeToDisplayCard;

	// Token: 0x04000701 RID: 1793
	private string savedWord = "";

	// Token: 0x04000702 RID: 1794
	private Canvas canvas;

	// Token: 0x04000703 RID: 1795
	public bool disableEmoji;

	// Token: 0x04000704 RID: 1796
	private Card cardParent;

	// Token: 0x04000705 RID: 1797
	private GameObject previewCard;
}
