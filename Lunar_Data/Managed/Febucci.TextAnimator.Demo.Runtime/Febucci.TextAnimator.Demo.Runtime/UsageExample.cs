using System;
using UnityEngine;

namespace Febucci.UI.Examples
{
	// Token: 0x02000004 RID: 4
	[AddComponentMenu("")]
	public class UsageExample : MonoBehaviour
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002522 File Offset: 0x00000722
		private void Awake()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002524 File Offset: 0x00000724
		private void Start()
		{
			this.ShowText();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000252C File Offset: 0x0000072C
		public void ShowText()
		{
			this.textAnimatorPlayer.ShowText(this.textToShow);
		}

		// Token: 0x0400000D RID: 13
		public TypewriterByCharacter textAnimatorPlayer;

		// Token: 0x0400000E RID: 14
		[TextArea(3, 50)]
		[SerializeField]
		private string textToShow = " ";
	}
}
