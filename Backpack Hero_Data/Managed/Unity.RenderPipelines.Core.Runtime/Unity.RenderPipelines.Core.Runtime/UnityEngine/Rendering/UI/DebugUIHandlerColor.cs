using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EF RID: 239
	public class DebugUIHandlerColor : DebugUIHandlerWidget
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.ColorField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldR.getter = () => this.m_Field.GetValue().r;
			this.fieldR.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldR.nextUIHandler = this.fieldG;
			this.SetupSettings(this.fieldR);
			this.fieldG.getter = () => this.m_Field.GetValue().g;
			this.fieldG.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldG.previousUIHandler = this.fieldR;
			this.fieldG.nextUIHandler = this.fieldB;
			this.SetupSettings(this.fieldG);
			this.fieldB.getter = () => this.m_Field.GetValue().b;
			this.fieldB.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldB.previousUIHandler = this.fieldG;
			this.fieldB.nextUIHandler = (this.m_Field.showAlpha ? this.fieldA : null);
			this.SetupSettings(this.fieldB);
			this.fieldA.gameObject.SetActive(this.m_Field.showAlpha);
			this.fieldA.getter = () => this.m_Field.GetValue().a;
			this.fieldA.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldA.previousUIHandler = this.fieldB;
			this.SetupSettings(this.fieldA);
			this.UpdateColor();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001FABC File Offset: 0x0001DCBC
		private void SetValue(float x, bool r = false, bool g = false, bool b = false, bool a = false)
		{
			Color value = this.m_Field.GetValue();
			if (r)
			{
				value.r = x;
			}
			if (g)
			{
				value.g = x;
			}
			if (b)
			{
				value.b = x;
			}
			if (a)
			{
				value.a = x;
			}
			this.m_Field.SetValue(value);
			this.UpdateColor();
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001FB18 File Offset: 0x0001DD18
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001FB68 File Offset: 0x0001DD68
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001FBDF File Offset: 0x0001DDDF
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001FBF2 File Offset: 0x0001DDF2
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001FC00 File Offset: 0x0001DE00
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001FC0E File Offset: 0x0001DE0E
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001FC29 File Offset: 0x0001DE29
		internal void UpdateColor()
		{
			if (this.colorImage != null)
			{
				this.colorImage.color = this.m_Field.GetValue();
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x040003DF RID: 991
		public Text nameLabel;

		// Token: 0x040003E0 RID: 992
		public UIFoldout valueToggle;

		// Token: 0x040003E1 RID: 993
		public Image colorImage;

		// Token: 0x040003E2 RID: 994
		public DebugUIHandlerIndirectFloatField fieldR;

		// Token: 0x040003E3 RID: 995
		public DebugUIHandlerIndirectFloatField fieldG;

		// Token: 0x040003E4 RID: 996
		public DebugUIHandlerIndirectFloatField fieldB;

		// Token: 0x040003E5 RID: 997
		public DebugUIHandlerIndirectFloatField fieldA;

		// Token: 0x040003E6 RID: 998
		private DebugUI.ColorField m_Field;

		// Token: 0x040003E7 RID: 999
		private DebugUIHandlerContainer m_Container;
	}
}
