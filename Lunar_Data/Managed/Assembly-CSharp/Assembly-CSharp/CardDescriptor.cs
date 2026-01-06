using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000013 RID: 19
public class CardDescriptor : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x000037EE File Offset: 0x000019EE
	private void OnEnable()
	{
		if (this.cardType == CardDescriptor.CardType.FullCard)
		{
			return;
		}
		if (CardDescriptor.instance)
		{
			Object.Destroy(CardDescriptor.instance.gameObject);
		}
		CardDescriptor.instance = this;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000381B File Offset: 0x00001A1B
	private void OnDisable()
	{
		if (CardDescriptor.instance == this)
		{
			CardDescriptor.instance = null;
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003830 File Offset: 0x00001A30
	private void Start()
	{
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003844 File Offset: 0x00001A44
	private void Update()
	{
		this.canvasGroup.alpha = Mathf.Lerp(this.canvasGroup.alpha, 1f, Time.deltaTime * 25f);
		if (this.cardType == CardDescriptor.CardType.FullCard)
		{
			return;
		}
		if (this.contentSizeFitter)
		{
			this.contentSizeFitter.enabled = false;
			this.contentSizeFitter.enabled = true;
		}
		GameObject selectedCard = CardManager.instance.GetSelectedCard();
		if (CardManager.instance.selectedCard && selectedCard)
		{
			base.transform.position = selectedCard.transform.position + Vector3.up * 145f * selectedCard.transform.lossyScale.y;
		}
		if (this.energyRequirementRect.localScale.x > 1f)
		{
			this.energyRequirementRect.localScale = Vector3.Lerp(this.energyRequirementRect.localScale, Vector3.one, Time.deltaTime * 10f);
		}
		this.KeepOnScreen();
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003958 File Offset: 0x00001B58
	private void KeepOnScreen()
	{
		RectTransform rectTransform = CanvasManager.instance.rectTransform;
		RectTransform rectTransform2 = this.rectTransform;
		Vector2 vector = rectTransform2.sizeDelta * base.transform.localScale;
		Vector2 vector2 = rectTransform.sizeDelta * (rectTransform2.anchorMin - Vector2.one / 2f);
		Vector2 vector3 = vector * (rectTransform2.pivot - Vector2.one / 2f * 2f);
		Vector2 vector4 = vector * (Vector2.one / 2f * 2f - rectTransform2.pivot);
		float num = rectTransform.sizeDelta.x * -0.5f - vector2.x - vector4.x + vector.x;
		float num2 = rectTransform.sizeDelta.x * 0.5f - vector2.x + vector3.x;
		float num3 = rectTransform.sizeDelta.y * -0.5f - vector2.y - vector4.y + vector.y;
		float num4 = rectTransform.sizeDelta.y * 0.5f - vector2.y + vector3.y;
		Vector2 anchoredPosition = rectTransform2.anchoredPosition;
		anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, num, num2);
		anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, num3, num4);
		rectTransform2.anchoredPosition = anchoredPosition;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003AE0 File Offset: 0x00001CE0
	public void SetCardImage(Sprite sprite)
	{
		this.cardImage.sprite = sprite;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00003AF0 File Offset: 0x00001CF0
	public void SetCardUseAndClassTypes(CardEffect.UseType useType, float lengthOfEffect, CardEffect.ClassType classType)
	{
		this.useTypeText.ResetAdditionalText();
		if (useType != CardEffect.UseType.Active)
		{
			if (useType == CardEffect.UseType.Passive)
			{
				this.useTypeText.AddAdditionalText((lengthOfEffect > 0f) ? (" (" + lengthOfEffect.ToString() + ")") : "", ReplacementText.AdditionalText.position.After);
				this.useTypeText.SetKey("Passive");
			}
		}
		else
		{
			this.useTypeText.SetKey("Active");
		}
		this.classTypeText.SetKey(classType.ToString());
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00003B7D File Offset: 0x00001D7D
	public void DisableUseAndClassTypes()
	{
		this.useTypeText.transform.gameObject.SetActive(false);
		this.classTypeText.transform.gameObject.SetActive(false);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00003BAB File Offset: 0x00001DAB
	public void SetCardTexts(string name, string description)
	{
		this.canvasGroup.alpha = 0f;
		this.cardName.SetKey(name);
		this.cardDescription.SetKey(description);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00003BD5 File Offset: 0x00001DD5
	public void SetEnergyRequirement(int energy)
	{
		this.energyRequirement.text = energy.ToString();
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00003BE9 File Offset: 0x00001DE9
	public void DisableEnergyRequirement()
	{
		this.energyRequirement.transform.parent.gameObject.SetActive(false);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00003C06 File Offset: 0x00001E06
	public void PulseEnergyRequirement()
	{
		this.energyRequirementRect.localScale = Vector3.one * 1.5f;
	}

	// Token: 0x04000047 RID: 71
	[SerializeField]
	public CardDescriptor.CardType cardType;

	// Token: 0x04000048 RID: 72
	public static CardDescriptor instance;

	// Token: 0x04000049 RID: 73
	[SerializeField]
	private RectTransform rectTransform;

	// Token: 0x0400004A RID: 74
	[SerializeField]
	private ContentSizeFitter contentSizeFitter;

	// Token: 0x0400004B RID: 75
	[SerializeField]
	private CanvasGroup canvasGroup;

	// Token: 0x0400004C RID: 76
	[SerializeField]
	private Image cardImage;

	// Token: 0x0400004D RID: 77
	[SerializeField]
	private ReplacementText cardName;

	// Token: 0x0400004E RID: 78
	[SerializeField]
	private ReplacementText cardDescription;

	// Token: 0x0400004F RID: 79
	[SerializeField]
	private RectTransform energyRequirementRect;

	// Token: 0x04000050 RID: 80
	[SerializeField]
	private TextMeshProUGUI energyRequirement;

	// Token: 0x04000051 RID: 81
	[SerializeField]
	private ReplacementText useTypeText;

	// Token: 0x04000052 RID: 82
	[SerializeField]
	private ReplacementText classTypeText;

	// Token: 0x020000BE RID: 190
	[SerializeField]
	public enum CardType
	{
		// Token: 0x040003D4 RID: 980
		SingleCardPreview,
		// Token: 0x040003D5 RID: 981
		FullCard
	}
}
