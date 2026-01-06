using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000088 RID: 136
public class RunTypeDescriptionWindow : MonoBehaviour
{
	// Token: 0x0600039F RID: 927 RVA: 0x00011FA4 File Offset: 0x000101A4
	public void SetDescription(RunType runType)
	{
		foreach (object obj in this.descriptorParent)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		if (runType.runDescription.Length > 1)
		{
			Object.Instantiate<GameObject>(this.descriptorPrefab, this.descriptorParent).GetComponentInChildren<ReplacementText>().SetKey(runType.runDescription);
		}
		foreach (RunType.RunProperty runProperty in runType.runProperties)
		{
			if (runProperty.descriptorKey.Length >= 1)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.descriptorPrefab, this.descriptorParent);
				ReplacementText componentInChildren = gameObject.GetComponentInChildren<ReplacementText>();
				componentInChildren.SetKey(runProperty.descriptorKey);
				float num = runProperty.value;
				if (runProperty.isPercentage)
				{
					num *= 100f;
				}
				componentInChildren.AddAdditionalText(num.ToString(), ReplacementText.AdditionalText.position.ReplaceVariable);
				RunTypeDescriptor component = gameObject.GetComponent<RunTypeDescriptor>();
				if (runProperty.showObjectsWithImages)
				{
					foreach (GameObject gameObject2 in runProperty.objs)
					{
						Object.Instantiate<GameObject>(this.descriptorImagePrefab, component.imagesParent).GetComponent<Image>().sprite = gameObject2.GetComponentInChildren<SpriteRenderer>().sprite;
					}
				}
			}
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.descriptorParent.GetComponent<RectTransform>());
	}

	// Token: 0x040002C8 RID: 712
	[SerializeField]
	private GameObject descriptorPrefab;

	// Token: 0x040002C9 RID: 713
	[SerializeField]
	private Transform descriptorParent;

	// Token: 0x040002CA RID: 714
	[SerializeField]
	private GameObject descriptorImagePrefab;
}
