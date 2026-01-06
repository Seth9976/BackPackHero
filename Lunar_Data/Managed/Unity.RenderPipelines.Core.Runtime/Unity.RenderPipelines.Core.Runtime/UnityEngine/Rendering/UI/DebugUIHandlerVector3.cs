using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000104 RID: 260
	public class DebugUIHandlerVector3 : DebugUIHandlerWidget
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x000219B0 File Offset: 0x0001FBB0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector3Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = () => this.m_Field.GetValue().x;
			this.fieldX.setter = delegate(float v)
			{
				this.SetValue(v, true, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = () => this.m_Field.GetValue().y;
			this.fieldY.setter = delegate(float v)
			{
				this.SetValue(v, false, true, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = () => this.m_Field.GetValue().z;
			this.fieldZ.setter = delegate(float v)
			{
				this.SetValue(v, false, false, true);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.SetupSettings(this.fieldZ);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private void SetValue(float v, bool x = false, bool y = false, bool z = false)
		{
			Vector3 value = this.m_Field.GetValue();
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
			this.m_Field.SetValue(value);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00021B2C File Offset: 0x0001FD2C
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => this.m_Field.incStep;
			field.incStepMultGetter = () => this.m_Field.incStepMult;
			field.decimalsGetter = () => (float)this.m_Field.decimals;
			field.Init();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00021B7C File Offset: 0x0001FD7C
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

		// Token: 0x060007A4 RID: 1956 RVA: 0x00021BF3 File Offset: 0x0001FDF3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00021C06 File Offset: 0x0001FE06
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00021C14 File Offset: 0x0001FE14
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00021C22 File Offset: 0x0001FE22
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00021C40 File Offset: 0x0001FE40
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

		// Token: 0x04000435 RID: 1077
		public Text nameLabel;

		// Token: 0x04000436 RID: 1078
		public UIFoldout valueToggle;

		// Token: 0x04000437 RID: 1079
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000438 RID: 1080
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000439 RID: 1081
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x0400043A RID: 1082
		private DebugUI.Vector3Field m_Field;

		// Token: 0x0400043B RID: 1083
		private DebugUIHandlerContainer m_Container;
	}
}
