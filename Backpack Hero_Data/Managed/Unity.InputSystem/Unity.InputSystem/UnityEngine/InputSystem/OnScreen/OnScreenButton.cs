using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000090 RID: 144
	[AddComponentMenu("Input/On-Screen Button")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.6/manual/OnScreen.html#on-screen-buttons")]
	public class OnScreenButton : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0003D3BF File Offset: 0x0003B5BF
		public void OnPointerUp(PointerEventData eventData)
		{
			base.SendValueToControl<float>(0f);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003D3CC File Offset: 0x0003B5CC
		public void OnPointerDown(PointerEventData eventData)
		{
			base.SendValueToControl<float>(1f);
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0003D3D9 File Offset: 0x0003B5D9
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0003D3E1 File Offset: 0x0003B5E1
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
