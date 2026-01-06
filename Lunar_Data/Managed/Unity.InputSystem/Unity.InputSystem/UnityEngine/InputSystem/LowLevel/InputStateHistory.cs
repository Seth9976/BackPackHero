using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F5 RID: 245
	public class InputStateHistory : IDisposable, IEnumerable<InputStateHistory.Record>, IEnumerable, IInputStateChangeMonitor
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000467CB File Offset: 0x000449CB
		public int Count
		{
			get
			{
				return this.m_RecordCount;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000467D3 File Offset: 0x000449D3
		public uint version
		{
			get
			{
				return this.m_CurrentVersion;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x000467DB File Offset: 0x000449DB
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x000467E3 File Offset: 0x000449E3
		public int historyDepth
		{
			get
			{
				return this.m_HistoryDepth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("History depth cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_HistoryDepth = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00046813 File Offset: 0x00044A13
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0004681B File Offset: 0x00044A1B
		public int extraMemoryPerRecord
		{
			get
			{
				return this.m_ExtraMemoryPerRecord;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Memory size cannot be negative", "value");
				}
				if (this.m_RecordBuffer.IsCreated)
				{
					throw new NotImplementedException();
				}
				this.m_ExtraMemoryPerRecord = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0004684C File Offset: 0x00044A4C
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0004687E File Offset: 0x00044A7E
		public InputUpdateType updateMask
		{
			get
			{
				InputUpdateType? updateMask = this.m_UpdateMask;
				if (updateMask == null)
				{
					return InputSystem.s_Manager.updateMask & ~InputUpdateType.Editor;
				}
				return updateMask.GetValueOrDefault();
			}
			set
			{
				if (value == InputUpdateType.None)
				{
					throw new ArgumentException("'InputUpdateType.None' is not a valid update mask", "value");
				}
				this.m_UpdateMask = new InputUpdateType?(value);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0004689F File Offset: 0x00044A9F
		public ReadOnlyArray<InputControl> controls
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Controls, 0, this.m_ControlCount);
			}
		}

		// Token: 0x17000428 RID: 1064
		public InputStateHistory.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int num = this.UserIndexToRecordIndex(index);
				return new InputStateHistory.Record(this, num, this.GetRecord(num));
			}
			set
			{
				if (index < 0 || index >= this.m_RecordCount)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.m_RecordCount), "index");
				}
				int num = this.UserIndexToRecordIndex(index);
				new InputStateHistory.Record(this, num, this.GetRecord(num)).CopyFrom(value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0004696B File Offset: 0x00044B6B
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x00046973 File Offset: 0x00044B73
		public Action<InputStateHistory.Record> onRecordAdded { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0004697C File Offset: 0x00044B7C
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x00046984 File Offset: 0x00044B84
		public Func<InputControl, double, InputEventPtr, bool> onShouldRecordStateChange { get; set; }

		// Token: 0x06000E65 RID: 3685 RVA: 0x0004698D File Offset: 0x00044B8D
		public InputStateHistory(int maxStateSizeInBytes)
		{
			if (maxStateSizeInBytes <= 0)
			{
				throw new ArgumentException("State size must be >= 0", "maxStateSizeInBytes");
			}
			this.m_AddNewControls = true;
			this.m_StateSizeInBytes = maxStateSizeInBytes.AlignToMultipleOf(4);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000469C8 File Offset: 0x00044BC8
		public InputStateHistory(string path)
		{
			using (InputControlList<InputControl> inputControlList = InputSystem.FindControls(path))
			{
				this.m_Controls = inputControlList.ToArray(false);
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00046A2C File Offset: 0x00044C2C
		public InputStateHistory(InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.m_Controls = new InputControl[] { control };
			this.m_ControlCount = 1;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00046A64 File Offset: 0x00044C64
		public InputStateHistory(IEnumerable<InputControl> controls)
		{
			if (controls != null)
			{
				this.m_Controls = controls.ToArray<InputControl>();
				this.m_ControlCount = this.m_Controls.Length;
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00046A94 File Offset: 0x00044C94
		~InputStateHistory()
		{
			this.Dispose();
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00046AC0 File Offset: 0x00044CC0
		public void Clear()
		{
			this.m_HeadIndex = 0;
			this.m_RecordCount = 0;
			this.m_CurrentVersion += 1U;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00046AE0 File Offset: 0x00044CE0
		public unsafe InputStateHistory.Record AddRecord(InputStateHistory.Record record)
		{
			int num;
			InputStateHistory.RecordHeader* ptr = this.AllocateRecord(out num);
			InputStateHistory.Record record2 = new InputStateHistory.Record(this, num, ptr);
			record2.CopyFrom(record);
			return record2;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00046B0C File Offset: 0x00044D0C
		public void StartRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.AddChangeMonitor(inputControl, this, -1L, 0U);
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00046B64 File Offset: 0x00044D64
		public void StopRecording()
		{
			foreach (InputControl inputControl in this.controls)
			{
				InputState.RemoveChangeMonitor(inputControl, this, -1L);
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00046BBC File Offset: 0x00044DBC
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, InputEventPtr eventPtr)
		{
			if (eventPtr.IsA<DeltaStateEvent>())
			{
				throw new NotImplementedException();
			}
			if (!eventPtr.IsA<StateEvent>())
			{
				throw new ArgumentException(string.Format("Event must be a state event but is '{0}' instead", eventPtr), "eventPtr");
			}
			byte* ptr = (byte*)StateEvent.From(eventPtr)->state - control.device.stateBlock.byteOffset;
			return this.RecordStateChange(control, (void*)ptr, eventPtr.time);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00046C2C File Offset: 0x00044E2C
		public unsafe InputStateHistory.Record RecordStateChange(InputControl control, void* statePtr, double time)
		{
			int num = this.m_Controls.IndexOfReference(control, this.m_ControlCount);
			if (num == -1)
			{
				if (!this.m_AddNewControls)
				{
					throw new ArgumentException(string.Format("Control '{0}' is not part of InputStateHistory", control), "control");
				}
				if ((ulong)control.stateBlock.alignedSizeInBytes > (ulong)((long)this.m_StateSizeInBytes))
				{
					throw new InvalidOperationException(string.Format("Cannot add control '{0}' with state larger than {1} bytes", control, this.m_StateSizeInBytes));
				}
				num = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Controls, ref this.m_ControlCount, control, 10);
			}
			int num2;
			InputStateHistory.RecordHeader* ptr = this.AllocateRecord(out num2);
			ptr->time = time;
			ref InputStateHistory.RecordHeader ptr2 = ref *ptr;
			uint num3 = this.m_CurrentVersion + 1U;
			this.m_CurrentVersion = num3;
			ptr2.version = num3;
			byte* ptr3 = ptr->statePtrWithoutControlIndex;
			if (this.m_ControlCount > 1 || this.m_AddNewControls)
			{
				ptr->controlIndex = num;
				ptr3 = ptr->statePtrWithControlIndex;
			}
			uint alignedSizeInBytes = control.stateBlock.alignedSizeInBytes;
			uint byteOffset = control.stateBlock.byteOffset;
			UnsafeUtility.MemCpy((void*)ptr3, (void*)((byte*)statePtr + byteOffset), (long)((ulong)alignedSizeInBytes));
			InputStateHistory.Record record = new InputStateHistory.Record(this, num2, ptr);
			Action<InputStateHistory.Record> onRecordAdded = this.onRecordAdded;
			if (onRecordAdded != null)
			{
				onRecordAdded(record);
			}
			return record;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00046D59 File Offset: 0x00044F59
		public IEnumerator<InputStateHistory.Record> GetEnumerator()
		{
			return new InputStateHistory.Enumerator(this);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00046D66 File Offset: 0x00044F66
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00046D6E File Offset: 0x00044F6E
		public void Dispose()
		{
			this.StopRecording();
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00046D82 File Offset: 0x00044F82
		protected void Destroy()
		{
			if (this.m_RecordBuffer.IsCreated)
			{
				this.m_RecordBuffer.Dispose();
				this.m_RecordBuffer = default(NativeArray<byte>);
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00046DA8 File Offset: 0x00044FA8
		private void Allocate()
		{
			if (!this.m_AddNewControls)
			{
				this.m_StateSizeInBytes = 0;
				foreach (InputControl inputControl in this.controls)
				{
					this.m_StateSizeInBytes = (int)Math.Max((uint)this.m_StateSizeInBytes, inputControl.stateBlock.alignedSizeInBytes);
				}
			}
			int num = this.bytesPerRecord * this.m_HistoryDepth;
			this.m_RecordBuffer = new NativeArray<byte>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00046E44 File Offset: 0x00045044
		protected internal int RecordIndexToUserIndex(int index)
		{
			if (index < this.m_HeadIndex)
			{
				return this.m_HistoryDepth - this.m_HeadIndex + index;
			}
			return index - this.m_HeadIndex;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00046E67 File Offset: 0x00045067
		protected internal int UserIndexToRecordIndex(int index)
		{
			return (this.m_HeadIndex + index) % this.m_HistoryDepth;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00046E78 File Offset: 0x00045078
		protected internal unsafe InputStateHistory.RecordHeader* GetRecord(int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				throw new InvalidOperationException("History buffer has been disposed");
			}
			if (index < 0 || index >= this.m_HistoryDepth)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.GetRecordUnchecked(index);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00046EB1 File Offset: 0x000450B1
		internal unsafe InputStateHistory.RecordHeader* GetRecordUnchecked(int index)
		{
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + index * this.bytesPerRecord);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00046EC8 File Offset: 0x000450C8
		protected internal unsafe InputStateHistory.RecordHeader* AllocateRecord(out int index)
		{
			if (!this.m_RecordBuffer.IsCreated)
			{
				this.Allocate();
			}
			index = (this.m_HeadIndex + this.m_RecordCount) % this.m_HistoryDepth;
			if (this.m_RecordCount == this.m_HistoryDepth)
			{
				this.m_HeadIndex = (this.m_HeadIndex + 1) % this.m_HistoryDepth;
			}
			else
			{
				this.m_RecordCount++;
			}
			return (InputStateHistory.RecordHeader*)((byte*)this.m_RecordBuffer.GetUnsafePtr<byte>() + this.bytesPerRecord * index);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00046F48 File Offset: 0x00045148
		protected unsafe TValue ReadValue<TValue>(InputStateHistory.RecordHeader* data) where TValue : struct
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl inputControl = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			InputControl<TValue> inputControl2 = inputControl as InputControl<TValue>;
			if (inputControl2 == null)
			{
				throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from control '{1}' with value type '{2}'", typeof(TValue).GetNiceTypeName(), inputControl, inputControl.valueType.GetNiceTypeName()));
			}
			byte* ptr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl.stateBlock.byteOffset;
			return inputControl2.ReadValueFromState((void*)ptr);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00046FF4 File Offset: 0x000451F4
		protected unsafe object ReadValueAsObject(InputStateHistory.RecordHeader* data)
		{
			bool flag = this.m_ControlCount == 1 && !this.m_AddNewControls;
			InputControl inputControl = (flag ? this.controls[0] : this.controls[data->controlIndex]);
			byte* ptr = (flag ? data->statePtrWithoutControlIndex : data->statePtrWithControlIndex);
			ptr -= inputControl.stateBlock.byteOffset;
			return inputControl.ReadValueFromStateAsObject((void*)ptr);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0004706C File Offset: 0x0004526C
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
		{
			bool currentUpdateType = InputState.currentUpdateType != InputUpdateType.None;
			InputUpdateType updateMask = this.updateMask;
			if (((currentUpdateType ? InputUpdateType.Dynamic : InputUpdateType.None) & updateMask) == InputUpdateType.None)
			{
				return;
			}
			if (this.onShouldRecordStateChange != null && !this.onShouldRecordStateChange(control, time, eventPtr))
			{
				return;
			}
			this.RecordStateChange(control, control.currentStatePtr, time);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000470B2 File Offset: 0x000452B2
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000470B4 File Offset: 0x000452B4
		internal int bytesPerRecord
		{
			get
			{
				return (this.m_StateSizeInBytes + this.m_ExtraMemoryPerRecord + ((this.m_ControlCount == 1 && !this.m_AddNewControls) ? 12 : 16)).AlignToMultipleOf(4);
			}
		}

		// Token: 0x040005EB RID: 1515
		private const int kDefaultHistorySize = 128;

		// Token: 0x040005EE RID: 1518
		internal InputControl[] m_Controls;

		// Token: 0x040005EF RID: 1519
		internal int m_ControlCount;

		// Token: 0x040005F0 RID: 1520
		private NativeArray<byte> m_RecordBuffer;

		// Token: 0x040005F1 RID: 1521
		private int m_StateSizeInBytes;

		// Token: 0x040005F2 RID: 1522
		private int m_RecordCount;

		// Token: 0x040005F3 RID: 1523
		private int m_HistoryDepth = 128;

		// Token: 0x040005F4 RID: 1524
		private int m_ExtraMemoryPerRecord;

		// Token: 0x040005F5 RID: 1525
		internal int m_HeadIndex;

		// Token: 0x040005F6 RID: 1526
		internal uint m_CurrentVersion;

		// Token: 0x040005F7 RID: 1527
		private InputUpdateType? m_UpdateMask;

		// Token: 0x040005F8 RID: 1528
		internal readonly bool m_AddNewControls;

		// Token: 0x02000218 RID: 536
		private struct Enumerator : IEnumerator<InputStateHistory.Record>, IEnumerator, IDisposable
		{
			// Token: 0x060014B3 RID: 5299 RVA: 0x0005FDD2 File Offset: 0x0005DFD2
			public Enumerator(InputStateHistory history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x060014B4 RID: 5300 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x060014B5 RID: 5301 RVA: 0x0005FE0A File Offset: 0x0005E00A
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0005FE13 File Offset: 0x0005E013
			public InputStateHistory.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0005FE26 File Offset: 0x0005E026
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060014B8 RID: 5304 RVA: 0x0005FE33 File Offset: 0x0005E033
			public void Dispose()
			{
			}

			// Token: 0x04000B4E RID: 2894
			private readonly InputStateHistory m_History;

			// Token: 0x04000B4F RID: 2895
			private int m_Index;
		}

		// Token: 0x02000219 RID: 537
		[StructLayout(LayoutKind.Explicit)]
		protected internal struct RecordHeader
		{
			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0005FE38 File Offset: 0x0005E038
			public unsafe byte* statePtrWithControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x060014BA RID: 5306 RVA: 0x0005FE54 File Offset: 0x0005E054
			public unsafe byte* statePtrWithoutControlIndex
			{
				get
				{
					fixed (byte* ptr = &this.m_StateWithoutControlIndex.FixedElementField)
					{
						return ptr;
					}
				}
			}

			// Token: 0x04000B50 RID: 2896
			[FieldOffset(0)]
			public double time;

			// Token: 0x04000B51 RID: 2897
			[FieldOffset(8)]
			public uint version;

			// Token: 0x04000B52 RID: 2898
			[FieldOffset(12)]
			public int controlIndex;

			// Token: 0x04000B53 RID: 2899
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(12)]
			private InputStateHistory.RecordHeader.<m_StateWithoutControlIndex>e__FixedBuffer m_StateWithoutControlIndex;

			// Token: 0x04000B54 RID: 2900
			[FixedBuffer(typeof(byte), 1)]
			[FieldOffset(16)]
			private InputStateHistory.RecordHeader.<m_StateWithControlIndex>e__FixedBuffer m_StateWithControlIndex;

			// Token: 0x04000B55 RID: 2901
			public const int kSizeWithControlIndex = 16;

			// Token: 0x04000B56 RID: 2902
			public const int kSizeWithoutControlIndex = 12;

			// Token: 0x0200026F RID: 623
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithoutControlIndex>e__FixedBuffer
			{
				// Token: 0x04000C94 RID: 3220
				public byte FixedElementField;
			}

			// Token: 0x02000270 RID: 624
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1)]
			public struct <m_StateWithControlIndex>e__FixedBuffer
			{
				// Token: 0x04000C95 RID: 3221
				public byte FixedElementField;
			}
		}

		// Token: 0x0200021A RID: 538
		public struct Record : IEquatable<InputStateHistory.Record>
		{
			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x060014BB RID: 5307 RVA: 0x0005FE6F File Offset: 0x0005E06F
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x060014BC RID: 5308 RVA: 0x0005FE82 File Offset: 0x0005E082
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x060014BD RID: 5309 RVA: 0x0005FE8C File Offset: 0x0005E08C
			internal uint version
			{
				get
				{
					return this.m_Version;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x060014BE RID: 5310 RVA: 0x0005FE94 File Offset: 0x0005E094
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060014BF RID: 5311 RVA: 0x0005FEBB File Offset: 0x0005E0BB
			public InputStateHistory owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0005FEC3 File Offset: 0x0005E0C3
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060014C1 RID: 5313 RVA: 0x0005FEDC File Offset: 0x0005E0DC
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0005FEF0 File Offset: 0x0005E0F0
			public unsafe InputControl control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return controls[0];
					}
					return controls[this.header->controlIndex];
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0005FF44 File Offset: 0x0005E144
			public InputStateHistory.Record next
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num + 1);
					return new InputStateHistory.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0005FFAC File Offset: 0x0005E1AC
			public InputStateHistory.Record previous
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num - 1 < 0)
					{
						return default(InputStateHistory.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num - 1);
					return new InputStateHistory.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x00060008 File Offset: 0x0005E208
			internal unsafe Record(InputStateHistory owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x060014C6 RID: 5318 RVA: 0x00060026 File Offset: 0x0005E226
			public TValue ReadValue<TValue>() where TValue : struct
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x060014C7 RID: 5319 RVA: 0x0006003F File Offset: 0x0005E23F
			public object ReadValueAsObject()
			{
				this.CheckValid();
				return this.m_Owner.ReadValueAsObject(this.header);
			}

			// Token: 0x060014C8 RID: 5320 RVA: 0x00060058 File Offset: 0x0005E258
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x060014C9 RID: 5321 RVA: 0x00060068 File Offset: 0x0005E268
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x060014CA RID: 5322 RVA: 0x000600AF File Offset: 0x0005E2AF
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x060014CB RID: 5323 RVA: 0x000600BD File Offset: 0x0005E2BD
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x060014CC RID: 5324 RVA: 0x000600F8 File Offset: 0x0005E2F8
			public unsafe void CopyFrom(InputStateHistory.Record record)
			{
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				this.CheckValid();
				InputControl control = record.control;
				int num = this.m_Owner.controls.IndexOfReference(control);
				if (num == -1)
				{
					if (!this.m_Owner.m_AddNewControls)
					{
						throw new InvalidOperationException(string.Format("Control '{0}' is not tracked by target history", record.control));
					}
					num = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.m_Owner.m_Controls, ref this.m_Owner.m_ControlCount, control, 10);
				}
				int stateSizeInBytes = this.m_Owner.m_StateSizeInBytes;
				if (stateSizeInBytes != record.m_Owner.m_StateSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot copy record from owner with state size '{0}' to owner with state size '{1}'", record.m_Owner.m_StateSizeInBytes, stateSizeInBytes));
				}
				InputStateHistory.RecordHeader* header = this.header;
				InputStateHistory.RecordHeader* header2 = record.header;
				UnsafeUtility.MemCpy((void*)header, (void*)header2, 12L);
				ref InputStateHistory.RecordHeader ptr = ref *header;
				InputStateHistory owner = this.m_Owner;
				uint num2 = owner.m_CurrentVersion + 1U;
				owner.m_CurrentVersion = num2;
				ptr.version = num2;
				this.m_Version = header->version;
				byte* ptr2 = header->statePtrWithoutControlIndex;
				if (this.m_Owner.controls.Count > 1 || this.m_Owner.m_AddNewControls)
				{
					header->controlIndex = num;
					ptr2 = header->statePtrWithControlIndex;
				}
				byte* ptr3 = ((record.m_Owner.m_ControlCount > 1 || record.m_Owner.m_AddNewControls) ? header2->statePtrWithControlIndex : header2->statePtrWithoutControlIndex);
				UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr3, (long)stateSizeInBytes);
				int extraMemoryPerRecord = this.m_Owner.m_ExtraMemoryPerRecord;
				if (extraMemoryPerRecord > 0 && extraMemoryPerRecord == record.m_Owner.m_ExtraMemoryPerRecord)
				{
					UnsafeUtility.MemCpy(this.GetUnsafeExtraMemoryPtr(), record.GetUnsafeExtraMemoryPtr(), (long)extraMemoryPerRecord);
				}
				Action<InputStateHistory.Record> onRecordAdded = this.m_Owner.onRecordAdded;
				if (onRecordAdded == null)
				{
					return;
				}
				onRecordAdded(this);
			}

			// Token: 0x060014CD RID: 5325 RVA: 0x000602CF File Offset: 0x0005E4CF
			internal unsafe void CheckValid()
			{
				if (this.m_Owner == null || this.m_IndexPlusOne == 0)
				{
					throw new InvalidOperationException("Value not initialized");
				}
				if (this.header->version != this.m_Version)
				{
					throw new InvalidOperationException("Record is no longer valid");
				}
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x0006030A File Offset: 0x0005E50A
			public bool Equals(InputStateHistory.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x060014CF RID: 5327 RVA: 0x00060338 File Offset: 0x0005E538
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory.Record)
				{
					InputStateHistory.Record record = (InputStateHistory.Record)obj;
					return this.Equals(record);
				}
				return false;
			}

			// Token: 0x060014D0 RID: 5328 RVA: 0x0006035D File Offset: 0x0005E55D
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x060014D1 RID: 5329 RVA: 0x0006038F File Offset: 0x0005E58F
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValueAsObject(), this.time);
			}

			// Token: 0x04000B57 RID: 2903
			private readonly InputStateHistory m_Owner;

			// Token: 0x04000B58 RID: 2904
			private readonly int m_IndexPlusOne;

			// Token: 0x04000B59 RID: 2905
			private uint m_Version;
		}
	}
}
