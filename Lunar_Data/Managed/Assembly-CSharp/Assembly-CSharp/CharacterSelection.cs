using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001B RID: 27
public class CharacterSelection : MonoBehaviour
{
	// Token: 0x060000C3 RID: 195 RVA: 0x00005890 File Offset: 0x00003A90
	private void Start()
	{
		if (CharacterSelection.selectedCharacter == null && base.transform.GetSiblingIndex() == 0)
		{
			this.Select();
		}
		if (UnlockManager.instance.IsUnlocked(this.character))
		{
			this.characterImage.color = Color.white;
			return;
		}
		this.characterImage.color = Color.black;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x000058F0 File Offset: 0x00003AF0
	private void Update()
	{
		if (CharacterSelection.selectedCharacter != this)
		{
			this.simpleAnimator.PlayAnimation("idle");
			this.selection.SetActive(false);
		}
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000591C File Offset: 0x00003B1C
	public void Select()
	{
		if (CharacterSelection.selectedCharacter == this)
		{
			return;
		}
		if (!UnlockManager.instance.IsUnlocked(this.character))
		{
			Debug.Log("Character not unlocked");
			return;
		}
		CharacterSelection.selectedCharacter = this;
		this.simpleAnimator.PlayAnimation("walk");
		this.selection.SetActive(true);
		Singleton.instance.selectedCharacter = this.character;
		RunTypeWindow.instance.SetupRunTypes();
	}

	// Token: 0x04000092 RID: 146
	[SerializeField]
	public static CharacterSelection selectedCharacter;

	// Token: 0x04000093 RID: 147
	[SerializeField]
	private PlayableCharacter character;

	// Token: 0x04000094 RID: 148
	[SerializeField]
	private SimpleAnimator simpleAnimator;

	// Token: 0x04000095 RID: 149
	[SerializeField]
	private GameObject selection;

	// Token: 0x04000096 RID: 150
	[SerializeField]
	private Image characterImage;
}
