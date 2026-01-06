using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000090 RID: 144
	[AddComponentMenu("Input/On-Screen Button")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/OnScreen.html#on-screen-buttons")]
	public class OnScreenButton : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x0003D383 File Offset: 0x0003B583
		public void OnPointerUp(PointerEventData eventData)
		{
			base.SendValueToControl<float>(0f);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003D390 File Offset: 0x0003B590
		public void OnPointerDown(PointerEventData eventData)
		{
			base.SendValueToControl<float>(1f);
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0003D39D File Offset: 0x0003B59D
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0003D3A5 File Offset: 0x0003B5A5
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x0400041B RID: 1051
		[InputControl(layout = "Button")]
		[SerializeField]
		private string m_ControlPath;
	}
}
