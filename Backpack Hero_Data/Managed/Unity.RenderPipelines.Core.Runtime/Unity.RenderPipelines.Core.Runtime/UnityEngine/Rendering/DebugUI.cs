using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006D RID: 109
	public class DebugUI
	{
		// Token: 0x02000146 RID: 326
		public class Container : DebugUI.Widget, DebugUI.IContainer
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600084B RID: 2123 RVA: 0x00023180 File Offset: 0x00021380
			// (set) Token: 0x0600084C RID: 2124 RVA: 0x00023188 File Offset: 0x00021388
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600084D RID: 2125 RVA: 0x00023191 File Offset: 0x00021391
			// (set) Token: 0x0600084E RID: 2126 RVA: 0x0002319C File Offset: 0x0002139C
			public override DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
					foreach (DebugUI.Widget widget in this.children)
					{
						widget.panel = value;
					}
				}
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x000231F0 File Offset: 0x000213F0
			public Container()
			{
				base.displayName = "";
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x00023249 File Offset: 0x00021449
			public Container(string displayName, ObservableList<DebugUI.Widget> children)
			{
				base.displayName = displayName;
				this.children = children;
				children.ItemAdded += this.OnItemAdded;
				children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x00023288 File Offset: 0x00021488
			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x000232D8 File Offset: 0x000214D8
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this.m_Panel;
					e.item.parent = this;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x00023312 File Offset: 0x00021512
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x00023348 File Offset: 0x00021548
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + base.queryPath.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}
		}

		// Token: 0x02000147 RID: 327
		public class Foldout : DebugUI.Container, DebugUI.IValueField
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000855 RID: 2133 RVA: 0x000233B0 File Offset: 0x000215B0
			public bool isReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000856 RID: 2134 RVA: 0x000233B3 File Offset: 0x000215B3
			// (set) Token: 0x06000857 RID: 2135 RVA: 0x000233BB File Offset: 0x000215BB
			public string[] columnLabels { get; set; }

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000858 RID: 2136 RVA: 0x000233C4 File Offset: 0x000215C4
			// (set) Token: 0x06000859 RID: 2137 RVA: 0x000233CC File Offset: 0x000215CC
			public string[] columnTooltips { get; set; }

			// Token: 0x0600085A RID: 2138 RVA: 0x000233D5 File Offset: 0x000215D5
			public Foldout()
			{
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x000233DD File Offset: 0x000215DD
			public Foldout(string displayName, ObservableList<DebugUI.Widget> children, string[] columnLabels = null, string[] columnTooltips = null)
				: base(displayName, children)
			{
				this.columnLabels = columnLabels;
				this.columnTooltips = columnTooltips;
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x000233F6 File Offset: 0x000215F6
			public bool GetValue()
			{
				return this.opened;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x000233FE File Offset: 0x000215FE
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x0002340B File Offset: 0x0002160B
			public void SetValue(object value)
			{
				this.SetValue((bool)value);
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x00023419 File Offset: 0x00021619
			public object ValidateValue(object value)
			{
				return value;
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x0002341C File Offset: 0x0002161C
			public void SetValue(bool value)
			{
				this.opened = value;
			}

			// Token: 0x04000512 RID: 1298
			public bool opened;

			// Token: 0x04000513 RID: 1299
			public bool isHeader;

			// Token: 0x04000514 RID: 1300
			public List<DebugUI.Foldout.ContextMenuItem> contextMenuItems;

			// Token: 0x0200018D RID: 397
			public struct ContextMenuItem
			{
				// Token: 0x040005D0 RID: 1488
				public string displayName;

				// Token: 0x040005D1 RID: 1489
				public Action action;
			}
		}

		// Token: 0x02000148 RID: 328
		public class HBox : DebugUI.Container
		{
			// Token: 0x06000861 RID: 2145 RVA: 0x00023425 File Offset: 0x00021625
			public HBox()
			{
				base.displayName = "HBox";
			}
		}

		// Token: 0x02000149 RID: 329
		public class VBox : DebugUI.Container
		{
			// Token: 0x06000862 RID: 2146 RVA: 0x00023438 File Offset: 0x00021638
			public VBox()
			{
				base.displayName = "VBox";
			}
		}

		// Token: 0x0200014A RID: 330
		public class Table : DebugUI.Container
		{
			// Token: 0x06000863 RID: 2147 RVA: 0x0002344B File Offset: 0x0002164B
			public Table()
			{
				base.displayName = "Array";
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x00023460 File Offset: 0x00021660
			public void SetColumnVisibility(int index, bool visible)
			{
				bool[] visibleColumns = this.VisibleColumns;
				if (index < 0 || index > visibleColumns.Length)
				{
					return;
				}
				visibleColumns[index] = visible;
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x00023484 File Offset: 0x00021684
			public bool GetColumnVisibility(int index)
			{
				bool[] visibleColumns = this.VisibleColumns;
				return index >= 0 && index <= visibleColumns.Length && visibleColumns[index];
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000866 RID: 2150 RVA: 0x000234A8 File Offset: 0x000216A8
			public bool[] VisibleColumns
			{
				get
				{
					if (this.m_Header != null)
					{
						return this.m_Header;
					}
					int num = 0;
					if (base.children.Count != 0)
					{
						num = ((DebugUI.Container)base.children[0]).children.Count;
						for (int i = 1; i < base.children.Count; i++)
						{
							if (((DebugUI.Container)base.children[i]).children.Count != num)
							{
								Debug.LogError("All rows must have the same number of children.");
								return null;
							}
						}
					}
					this.m_Header = new bool[num];
					for (int j = 0; j < num; j++)
					{
						this.m_Header[j] = true;
					}
					return this.m_Header;
				}
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x00023556 File Offset: 0x00021756
			protected override void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemAdded(sender, e);
				this.m_Header = null;
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x00023567 File Offset: 0x00021767
			protected override void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemRemoved(sender, e);
				this.m_Header = null;
			}

			// Token: 0x04000517 RID: 1303
			public bool isReadOnly;

			// Token: 0x04000518 RID: 1304
			private bool[] m_Header;

			// Token: 0x0200018E RID: 398
			public class Row : DebugUI.Foldout
			{
				// Token: 0x06000931 RID: 2353 RVA: 0x00025021 File Offset: 0x00023221
				public Row()
				{
					base.displayName = "Row";
				}
			}
		}

		// Token: 0x0200014B RID: 331
		[Flags]
		public enum Flags
		{
			// Token: 0x0400051A RID: 1306
			None = 0,
			// Token: 0x0400051B RID: 1307
			EditorOnly = 2,
			// Token: 0x0400051C RID: 1308
			RuntimeOnly = 4,
			// Token: 0x0400051D RID: 1309
			EditorForceUpdate = 8
		}

		// Token: 0x0200014C RID: 332
		public abstract class Widget
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000869 RID: 2153 RVA: 0x00023578 File Offset: 0x00021778
			// (set) Token: 0x0600086A RID: 2154 RVA: 0x00023580 File Offset: 0x00021780
			public virtual DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x0600086B RID: 2155 RVA: 0x00023589 File Offset: 0x00021789
			// (set) Token: 0x0600086C RID: 2156 RVA: 0x00023591 File Offset: 0x00021791
			public virtual DebugUI.IContainer parent
			{
				get
				{
					return this.m_Parent;
				}
				internal set
				{
					this.m_Parent = value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600086D RID: 2157 RVA: 0x0002359A File Offset: 0x0002179A
			// (set) Token: 0x0600086E RID: 2158 RVA: 0x000235A2 File Offset: 0x000217A2
			public DebugUI.Flags flags { get; set; }

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600086F RID: 2159 RVA: 0x000235AB File Offset: 0x000217AB
			// (set) Token: 0x06000870 RID: 2160 RVA: 0x000235B3 File Offset: 0x000217B3
			public string displayName { get; set; }

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000871 RID: 2161 RVA: 0x000235BC File Offset: 0x000217BC
			// (set) Token: 0x06000872 RID: 2162 RVA: 0x000235C4 File Offset: 0x000217C4
			public string tooltip { get; set; }

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000873 RID: 2163 RVA: 0x000235CD File Offset: 0x000217CD
			// (set) Token: 0x06000874 RID: 2164 RVA: 0x000235D5 File Offset: 0x000217D5
			public string queryPath { get; private set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000875 RID: 2165 RVA: 0x000235DE File Offset: 0x000217DE
			public bool isEditorOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.EditorOnly);
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x000235F6 File Offset: 0x000217F6
			public bool isRuntimeOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.RuntimeOnly);
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000877 RID: 2167 RVA: 0x0002360E File Offset: 0x0002180E
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000878 RID: 2168 RVA: 0x00023622 File Offset: 0x00021822
			public bool isHidden
			{
				get
				{
					Func<bool> func = this.isHiddenCallback;
					return func != null && func();
				}
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x00023635 File Offset: 0x00021835
			internal virtual void GenerateQueryPath()
			{
				this.queryPath = this.displayName.Trim();
				if (this.m_Parent != null)
				{
					this.queryPath = this.m_Parent.queryPath + " -> " + this.queryPath;
				}
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x00023671 File Offset: 0x00021871
			public override int GetHashCode()
			{
				return this.queryPath.GetHashCode();
			}

			// Token: 0x17000103 RID: 259
			// (set) Token: 0x0600087B RID: 2171 RVA: 0x0002367E File Offset: 0x0002187E
			public DebugUI.Widget.NameAndTooltip nameAndTooltip
			{
				set
				{
					this.displayName = value.name;
					this.tooltip = value.tooltip;
				}
			}

			// Token: 0x0400051E RID: 1310
			protected DebugUI.Panel m_Panel;

			// Token: 0x0400051F RID: 1311
			protected DebugUI.IContainer m_Parent;

			// Token: 0x04000524 RID: 1316
			public Func<bool> isHiddenCallback;

			// Token: 0x0200018F RID: 399
			public struct NameAndTooltip
			{
				// Token: 0x040005D2 RID: 1490
				public string name;

				// Token: 0x040005D3 RID: 1491
				public string tooltip;
			}
		}

		// Token: 0x0200014D RID: 333
		public interface IContainer
		{
			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600087D RID: 2173
			ObservableList<DebugUI.Widget> children { get; }

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600087E RID: 2174
			// (set) Token: 0x0600087F RID: 2175
			string displayName { get; set; }

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000880 RID: 2176
			string queryPath { get; }
		}

		// Token: 0x0200014E RID: 334
		public interface IValueField
		{
			// Token: 0x06000881 RID: 2177
			object GetValue();

			// Token: 0x06000882 RID: 2178
			void SetValue(object value);

			// Token: 0x06000883 RID: 2179
			object ValidateValue(object value);
		}

		// Token: 0x0200014F RID: 335
		public class Button : DebugUI.Widget
		{
			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x000236A0 File Offset: 0x000218A0
			// (set) Token: 0x06000885 RID: 2181 RVA: 0x000236A8 File Offset: 0x000218A8
			public Action action { get; set; }
		}

		// Token: 0x02000150 RID: 336
		public class Value : DebugUI.Widget
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000887 RID: 2183 RVA: 0x000236B9 File Offset: 0x000218B9
			// (set) Token: 0x06000888 RID: 2184 RVA: 0x000236C1 File Offset: 0x000218C1
			public Func<object> getter { get; set; }

			// Token: 0x06000889 RID: 2185 RVA: 0x000236CA File Offset: 0x000218CA
			public Value()
			{
				base.displayName = "";
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x000236E8 File Offset: 0x000218E8
			public object GetValue()
			{
				return this.getter();
			}

			// Token: 0x04000527 RID: 1319
			public float refreshRate = 0.1f;
		}

		// Token: 0x02000151 RID: 337
		public abstract class Field<T> : DebugUI.Widget, DebugUI.IValueField
		{
			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600088B RID: 2187 RVA: 0x000236F5 File Offset: 0x000218F5
			// (set) Token: 0x0600088C RID: 2188 RVA: 0x000236FD File Offset: 0x000218FD
			public Func<T> getter { get; set; }

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x0600088D RID: 2189 RVA: 0x00023706 File Offset: 0x00021906
			// (set) Token: 0x0600088E RID: 2190 RVA: 0x0002370E File Offset: 0x0002190E
			public Action<T> setter { get; set; }

			// Token: 0x0600088F RID: 2191 RVA: 0x00023717 File Offset: 0x00021917
			object DebugUI.IValueField.ValidateValue(object value)
			{
				return this.ValidateValue((T)((object)value));
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x0002372A File Offset: 0x0002192A
			public virtual T ValidateValue(T value)
			{
				return value;
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x0002372D File Offset: 0x0002192D
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x0002373A File Offset: 0x0002193A
			public T GetValue()
			{
				return this.getter();
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x00023747 File Offset: 0x00021947
			public void SetValue(object value)
			{
				this.SetValue((T)((object)value));
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x00023758 File Offset: 0x00021958
			public void SetValue(T value)
			{
				T t = this.ValidateValue(value);
				if (!t.Equals(this.getter()))
				{
					this.setter(t);
					Action<DebugUI.Field<T>, T> action = this.onValueChanged;
					if (action == null)
					{
						return;
					}
					action(this, t);
				}
			}

			// Token: 0x0400052A RID: 1322
			public Action<DebugUI.Field<T>, T> onValueChanged;
		}

		// Token: 0x02000152 RID: 338
		public class BoolField : DebugUI.Field<bool>
		{
		}

		// Token: 0x02000153 RID: 339
		public class HistoryBoolField : DebugUI.BoolField
		{
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000897 RID: 2199 RVA: 0x000237BA File Offset: 0x000219BA
			// (set) Token: 0x06000898 RID: 2200 RVA: 0x000237C2 File Offset: 0x000219C2
			public Func<bool>[] historyGetter { get; set; }

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000899 RID: 2201 RVA: 0x000237CB File Offset: 0x000219CB
			public int historyDepth
			{
				get
				{
					Func<bool>[] historyGetter = this.historyGetter;
					if (historyGetter == null)
					{
						return 0;
					}
					return historyGetter.Length;
				}
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x000237DB File Offset: 0x000219DB
			public bool GetHistoryValue(int historyIndex)
			{
				return this.historyGetter[historyIndex]();
			}
		}

		// Token: 0x02000154 RID: 340
		public class IntField : DebugUI.Field<int>
		{
			// Token: 0x0600089C RID: 2204 RVA: 0x000237F2 File Offset: 0x000219F2
			public override int ValidateValue(int value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x0400052C RID: 1324
			public Func<int> min;

			// Token: 0x0400052D RID: 1325
			public Func<int> max;

			// Token: 0x0400052E RID: 1326
			public int incStep = 1;

			// Token: 0x0400052F RID: 1327
			public int intStepMult = 10;
		}

		// Token: 0x02000155 RID: 341
		public class UIntField : DebugUI.Field<uint>
		{
			// Token: 0x0600089E RID: 2206 RVA: 0x00023842 File Offset: 0x00021A42
			public override uint ValidateValue(uint value)
			{
				if (this.min != null)
				{
					value = (uint)Mathf.Max((int)value, (int)this.min());
				}
				if (this.max != null)
				{
					value = (uint)Mathf.Min((int)value, (int)this.max());
				}
				return value;
			}

			// Token: 0x04000530 RID: 1328
			public Func<uint> min;

			// Token: 0x04000531 RID: 1329
			public Func<uint> max;

			// Token: 0x04000532 RID: 1330
			public uint incStep = 1U;

			// Token: 0x04000533 RID: 1331
			public uint intStepMult = 10U;
		}

		// Token: 0x02000156 RID: 342
		public class FloatField : DebugUI.Field<float>
		{
			// Token: 0x060008A0 RID: 2208 RVA: 0x00023892 File Offset: 0x00021A92
			public override float ValidateValue(float value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x04000534 RID: 1332
			public Func<float> min;

			// Token: 0x04000535 RID: 1333
			public Func<float> max;

			// Token: 0x04000536 RID: 1334
			public float incStep = 0.1f;

			// Token: 0x04000537 RID: 1335
			public float incStepMult = 10f;

			// Token: 0x04000538 RID: 1336
			public int decimals = 3;
		}

		// Token: 0x02000157 RID: 343
		private static class EnumUtility
		{
			// Token: 0x060008A2 RID: 2210 RVA: 0x000238F0 File Offset: 0x00021AF0
			internal static GUIContent[] MakeEnumNames(Type enumType)
			{
				return enumType.GetFields(BindingFlags.Static | BindingFlags.Public).Select(delegate(FieldInfo fieldInfo)
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(InspectorNameAttribute), false);
					if (customAttributes.Length != 0)
					{
						return new GUIContent(((InspectorNameAttribute)customAttributes.First<object>()).displayName);
					}
					return new GUIContent(Regex.Replace(fieldInfo.Name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 "));
				}).ToArray<GUIContent>();
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00023924 File Offset: 0x00021B24
			internal static int[] MakeEnumValues(Type enumType)
			{
				Array values = Enum.GetValues(enumType);
				int[] array = new int[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = (int)values.GetValue(i);
				}
				return array;
			}
		}

		// Token: 0x02000158 RID: 344
		public class EnumField : DebugUI.Field<int>
		{
			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00023965 File Offset: 0x00021B65
			// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0002396D File Offset: 0x00021B6D
			public Func<int> getIndex { get; set; }

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00023976 File Offset: 0x00021B76
			// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0002397E File Offset: 0x00021B7E
			public Action<int> setIndex { get; set; }

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00023987 File Offset: 0x00021B87
			// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00023994 File Offset: 0x00021B94
			public int currentIndex
			{
				get
				{
					return this.getIndex();
				}
				set
				{
					this.setIndex(value);
				}
			}

			// Token: 0x17000110 RID: 272
			// (set) Token: 0x060008AA RID: 2218 RVA: 0x000239A2 File Offset: 0x00021BA2
			public Type autoEnum
			{
				set
				{
					this.enumNames = DebugUI.EnumUtility.MakeEnumNames(value);
					this.enumValues = DebugUI.EnumUtility.MakeEnumValues(value);
					this.InitIndexes();
					this.InitQuickSeparators();
				}
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x000239C8 File Offset: 0x00021BC8
			internal void InitQuickSeparators()
			{
				IEnumerable<string> enumerable = this.enumNames.Select(delegate(GUIContent x)
				{
					string[] array = x.text.Split('/', StringSplitOptions.None);
					if (array.Length == 1)
					{
						return "";
					}
					return array[0];
				});
				this.quickSeparators = new int[enumerable.Distinct<string>().Count<string>()];
				string text = null;
				int i = 0;
				int num = 0;
				while (i < this.quickSeparators.Length)
				{
					string text2 = enumerable.ElementAt(num);
					while (text == text2)
					{
						text2 = enumerable.ElementAt(++num);
					}
					text = text2;
					this.quickSeparators[i] = num++;
					i++;
				}
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x00023A60 File Offset: 0x00021C60
			internal void InitIndexes()
			{
				if (this.enumNames == null)
				{
					this.enumNames = new GUIContent[0];
				}
				this.indexes = new int[this.enumNames.Length];
				for (int i = 0; i < this.enumNames.Length; i++)
				{
					this.indexes[i] = i;
				}
			}

			// Token: 0x04000539 RID: 1337
			public GUIContent[] enumNames;

			// Token: 0x0400053A RID: 1338
			public int[] enumValues;

			// Token: 0x0400053B RID: 1339
			internal int[] quickSeparators;

			// Token: 0x0400053C RID: 1340
			internal int[] indexes;
		}

		// Token: 0x02000159 RID: 345
		public class HistoryEnumField : DebugUI.EnumField
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060008AE RID: 2222 RVA: 0x00023AB8 File Offset: 0x00021CB8
			// (set) Token: 0x060008AF RID: 2223 RVA: 0x00023AC0 File Offset: 0x00021CC0
			public Func<int>[] historyIndexGetter { get; set; }

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00023AC9 File Offset: 0x00021CC9
			public int historyDepth
			{
				get
				{
					Func<int>[] historyIndexGetter = this.historyIndexGetter;
					if (historyIndexGetter == null)
					{
						return 0;
					}
					return historyIndexGetter.Length;
				}
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x00023AD9 File Offset: 0x00021CD9
			public int GetHistoryValue(int historyIndex)
			{
				return this.historyIndexGetter[historyIndex]();
			}
		}

		// Token: 0x0200015A RID: 346
		public class BitField : DebugUI.Field<Enum>
		{
			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00023AF0 File Offset: 0x00021CF0
			// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00023AF8 File Offset: 0x00021CF8
			public GUIContent[] enumNames { get; private set; }

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00023B01 File Offset: 0x00021D01
			// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00023B09 File Offset: 0x00021D09
			public int[] enumValues { get; private set; }

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00023B12 File Offset: 0x00021D12
			// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00023B1A File Offset: 0x00021D1A
			public Type enumType
			{
				get
				{
					return this.m_EnumType;
				}
				set
				{
					this.m_EnumType = value;
					this.enumNames = DebugUI.EnumUtility.MakeEnumNames(value);
					this.enumValues = DebugUI.EnumUtility.MakeEnumValues(value);
				}
			}

			// Token: 0x04000542 RID: 1346
			private Type m_EnumType;
		}

		// Token: 0x0200015B RID: 347
		public class ColorField : DebugUI.Field<Color>
		{
			// Token: 0x060008BA RID: 2234 RVA: 0x00023B44 File Offset: 0x00021D44
			public override Color ValidateValue(Color value)
			{
				if (!this.hdr)
				{
					value.r = Mathf.Clamp01(value.r);
					value.g = Mathf.Clamp01(value.g);
					value.b = Mathf.Clamp01(value.b);
					value.a = Mathf.Clamp01(value.a);
				}
				return value;
			}

			// Token: 0x04000543 RID: 1347
			public bool hdr;

			// Token: 0x04000544 RID: 1348
			public bool showAlpha = true;

			// Token: 0x04000545 RID: 1349
			public bool showPicker = true;

			// Token: 0x04000546 RID: 1350
			public float incStep = 0.025f;

			// Token: 0x04000547 RID: 1351
			public float incStepMult = 5f;

			// Token: 0x04000548 RID: 1352
			public int decimals = 3;
		}

		// Token: 0x0200015C RID: 348
		public class Vector2Field : DebugUI.Field<Vector2>
		{
			// Token: 0x04000549 RID: 1353
			public float incStep = 0.025f;

			// Token: 0x0400054A RID: 1354
			public float incStepMult = 10f;

			// Token: 0x0400054B RID: 1355
			public int decimals = 3;
		}

		// Token: 0x0200015D RID: 349
		public class Vector3Field : DebugUI.Field<Vector3>
		{
			// Token: 0x0400054C RID: 1356
			public float incStep = 0.025f;

			// Token: 0x0400054D RID: 1357
			public float incStepMult = 10f;

			// Token: 0x0400054E RID: 1358
			public int decimals = 3;
		}

		// Token: 0x0200015E RID: 350
		public class Vector4Field : DebugUI.Field<Vector4>
		{
			// Token: 0x0400054F RID: 1359
			public float incStep = 0.025f;

			// Token: 0x04000550 RID: 1360
			public float incStepMult = 10f;

			// Token: 0x04000551 RID: 1361
			public int decimals = 3;
		}

		// Token: 0x0200015F RID: 351
		public class MessageBox : DebugUI.Widget
		{
			// Token: 0x04000552 RID: 1362
			public DebugUI.MessageBox.Style style;

			// Token: 0x02000192 RID: 402
			public enum Style
			{
				// Token: 0x040005D9 RID: 1497
				Info,
				// Token: 0x040005DA RID: 1498
				Warning,
				// Token: 0x040005DB RID: 1499
				Error
			}
		}

		// Token: 0x02000160 RID: 352
		public class Panel : DebugUI.IContainer, IComparable<DebugUI.Panel>
		{
			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00023C4C File Offset: 0x00021E4C
			// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00023C54 File Offset: 0x00021E54
			public DebugUI.Flags flags { get; set; }

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00023C5D File Offset: 0x00021E5D
			// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00023C65 File Offset: 0x00021E65
			public string displayName { get; set; }

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00023C6E File Offset: 0x00021E6E
			// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00023C76 File Offset: 0x00021E76
			public int groupIndex { get; set; }

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00023C7F File Offset: 0x00021E7F
			public string queryPath
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00023C87 File Offset: 0x00021E87
			public bool isEditorOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00023C94 File Offset: 0x00021E94
			public bool isRuntimeOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.RuntimeOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00023CA1 File Offset: 0x00021EA1
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060008CA RID: 2250 RVA: 0x00023CB5 File Offset: 0x00021EB5
			public bool editorForceUpdate
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorForceUpdate) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060008CB RID: 2251 RVA: 0x00023CC2 File Offset: 0x00021EC2
			// (set) Token: 0x060008CC RID: 2252 RVA: 0x00023CCA File Offset: 0x00021ECA
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x060008CD RID: 2253 RVA: 0x00023CD4 File Offset: 0x00021ED4
			// (remove) Token: 0x060008CE RID: 2254 RVA: 0x00023D0C File Offset: 0x00021F0C
			public event Action<DebugUI.Panel> onSetDirty = delegate
			{
			};

			// Token: 0x060008CF RID: 2255 RVA: 0x00023D44 File Offset: 0x00021F44
			public Panel()
			{
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00023DB7 File Offset: 0x00021FB7
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this;
					e.item.parent = this;
				}
				this.SetDirty();
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x00023DDF File Offset: 0x00021FDF
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				this.SetDirty();
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x00023E08 File Offset: 0x00022008
			public void SetDirty()
			{
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
				this.onSetDirty(this);
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x00023E60 File Offset: 0x00022060
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + this.displayName.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00023EC8 File Offset: 0x000220C8
			int IComparable<DebugUI.Panel>.CompareTo(DebugUI.Panel other)
			{
				if (other != null)
				{
					return this.groupIndex.CompareTo(other.groupIndex);
				}
				return 1;
			}
		}
	}
}
