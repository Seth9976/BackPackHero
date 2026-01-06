using System;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F2 RID: 242
	public class DebugUIHandlerEnumHistory : DebugUIHandlerEnumField
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x0002019C File Offset: 0x0001E39C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryEnumField historyEnumField = widget as DebugUI.HistoryEnumField;
			int num = ((historyEnumField != null) ? historyEnumField.historyDepth : 0);
			this.historyValues = new Text[num];
			for (int i = 0; i < num; i++)
			{
				Text text = Object.Instantiate<Text>(this.valueLabel, base.transform);
				Vector3 position = text.transform.position;
				position.x += (float)(i + 1) * 60f;
				text.transform.position = position;
				Text component = text.GetComponent<Text>();
				component.color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyValues[i] = component;
			}
			base.SetWidget(widget);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00020244 File Offset: 0x0001E444
		protected override void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			this.valueLabel.text = this.m_Field.enumNames[num].text;
			DebugUI.HistoryEnumField historyEnumField = this.m_Field as DebugUI.HistoryEnumField;
			int num2 = ((historyEnumField != null) ? historyEnumField.historyDepth : 0);
			for (int i = 0; i < num2; i++)
			{
				if (i < this.historyValues.Length && this.historyValues[i] != null)
				{
					this.historyValues[i].text = historyEnumField.enumNames[historyEnumField.GetHistoryValue(i)].text;
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000202F5 File Offset: 0x0001E4F5
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.m_Field.currentIndex = this.m_Field.getIndex();
			this.valueLabel.text = this.m_Field.enumNames[this.m_Field.currentIndex].text;
			yield break;
		}

		// Token: 0x040003EE RID: 1006
		private Text[] historyValues;

		// Token: 0x040003EF RID: 1007
		private const float xDecal = 60f;
	}
}
