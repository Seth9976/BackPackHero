using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000105 RID: 261
	public class DebugUIHandlerVector4 : DebugUIHandlerWidget
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00021D18 File Offset: 0x0001FF18
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector4Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.fieldZ.nextUIHandler = this.fieldW;
			this.SetupSettings(this.fieldZ);
			this.fieldW.getter = () => this.m_Field.GetValue().w;
			this.fieldW.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldW.previousUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldW);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00021EA8 File Offset: 0x000200A8
		private void SetValue(float v, bool x = false, bool y = false, bool z = false, bool w = false)
		{
			Vector4 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			if (z)
			{
				value.z = v;
			}
			if (w)
			{
				value.w = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00021EFC File Offset: 0x000200FC
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00021F4C File Offset: 0x0002014C
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

		// Token: 0x060007B7 RID: 1975 RVA: 0x00021FC3 File Offset: 0x000201C3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00021FD6 File Offset: 0x000201D6
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00021FE4 File Offset: 0x000201E4
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00021FF2 File Offset: 0x000201F2
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00022010 File Offset: 0x00020210
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

		// Token: 0x0400043C RID: 1084
		public Text nameLabel;

		// Token: 0x0400043D RID: 1085
		public UIFoldout valueToggle;

		// Token: 0x0400043E RID: 1086
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x0400043F RID: 1087
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000440 RID: 1088
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000441 RID: 1089
		public DebugUIHandlerIndirectFloatField fieldW;

		// Token: 0x04000442 RID: 1090
		private DebugUI.Vector4Field m_Field;

		// Token: 0x04000443 RID: 1091
		private DebugUIHandlerContainer m_Container;
	}
}
