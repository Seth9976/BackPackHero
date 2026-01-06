using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F6 RID: 246
	public class InputStateHistory<TValue> : InputStateHistory, IReadOnlyList<InputStateHistory<TValue>.Record>, IEnumerable<InputStateHistory<TValue>.Record>, IEnumerable, IReadOnlyCollection<InputStateHistory<TValue>.Record> where TValue : struct
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x00047130 File Offset: 0x00045330
		public InputStateHistory(int? maxStateSizeInBytes = null)
			: base(maxStateSizeInBytes ?? UnsafeUtility.SizeOf<TValue>())
		{
			int? num = maxStateSizeInBytes;
			int num2 = UnsafeUtility.SizeOf<TValue>();
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				throw new ArgumentException("Max state size cannot be smaller than sizeof(TValue)", "maxStateSizeInBytes");
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00047188 File Offset: 0x00045388
		public InputStateHistory(InputControl<TValue> control)
			: base(control)
		{
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00047194 File Offset: 0x00045394
		public InputStateHistory(string path)
			: base(path)
		{
			foreach (InputControl inputControl in base.controls)
			{
				if (!typeof(TValue).IsAssignableFrom(inputControl.valueType))
				{
					throw new ArgumentException(string.Format("Control '{0}' matched by '{1}' has value type '{2}' which is incompatible with '{3}'", new object[]
					{
						inputControl,
						path,
						inputControl.valueType.GetNiceTypeName(),
						typeof(TValue).GetNiceTypeName()
					}));
				}
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00047240 File Offset: 0x00045440
		~InputStateHistory()
		{
			base.Destroy();
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0004726C File Offset: 0x0004546C
		public unsafe InputStateHistory<TValue>.Record AddRecord(InputStateHistory<TValue>.Record record)
		{
			int num;
			InputStateHistory.RecordHeader* ptr = base.AllocateRecord(out num);
			InputStateHistory<TValue>.Record record2 = new InputStateHistory<TValue>.Record(this, num, ptr);
			record2.CopyFrom(record);
			return record2;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00047298 File Offset: 0x00045498
		public unsafe InputStateHistory<TValue>.Record RecordStateChange(InputControl<TValue> control, TValue value, double time = -1.0)
		{
			InputEventPtr inputEventPtr;
			InputStateHistory<TValue>.Record record2;
			using (StateEvent.From(control.device, out inputEventPtr, Allocator.Temp))
			{
				byte* ptr = (byte*)StateEvent.From(inputEventPtr)->state - control.device.stateBlock.byteOffset;
				control.WriteValueIntoState(value, (void*)ptr);
				if (time >= 0.0)
				{
					inputEventPtr.time = time;
				}
				InputStateHistory.Record record = base.RecordStateChange(control, inputEventPtr);
				record2 = new InputStateHistory<TValue>.Record(this, record.recordIndex, record.header);
			}
			return record2;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00047334 File Offset: 0x00045534
		public new IEnumerator<InputStateHistory<TValue>.Record> GetEnumerator()
		{
			return new InputStateHistory<TValue>.Enumerator(this);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00047341 File Offset: 0x00045541
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700042E RID: 1070
		public InputStateHistory<TValue>.Record this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int num = base.UserIndexToRecordIndex(index);
				return new InputStateHistory<TValue>.Record(this, num, base.GetRecord(num));
			}
			set
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, base.Count), "index");
				}
				int num = base.UserIndexToRecordIndex(index);
				new InputStateHistory<TValue>.Record(this, num, base.GetRecord(num)).CopyFrom(value);
			}
		}

		// Token: 0x0200021B RID: 539
		private struct Enumerator : IEnumerator<InputStateHistory<TValue>.Record>, IEnumerator, IDisposable
		{
			// Token: 0x060014D9 RID: 5337 RVA: 0x000605D4 File Offset: 0x0005E7D4
			public Enumerator(InputStateHistory<TValue> history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x060014DA RID: 5338 RVA: 0x000605E4 File Offset: 0x0005E7E4
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x060014DB RID: 5339 RVA: 0x0006060C File Offset: 0x0005E80C
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060014DC RID: 5340 RVA: 0x00060615 File Offset: 0x0005E815
			public InputStateHistory<TValue>.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060014DD RID: 5341 RVA: 0x00060628 File Offset: 0x0005E828
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x00060635 File Offset: 0x0005E835
			public void Dispose()
			{
			}

			// Token: 0x04000B5B RID: 2907
			private readonly InputStateHistory<TValue> m_History;

			// Token: 0x04000B5C RID: 2908
			private int m_Index;
		}

		// Token: 0x0200021C RID: 540
		public new struct Record : IEquatable<InputStateHistory<TValue>.Record>
		{
			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x00060637 File Offset: 0x0005E837
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0006064A File Offset: 0x0005E84A
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00060654 File Offset: 0x0005E854
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0006067B File Offset: 0x0005E87B
			public InputStateHistory<TValue> owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00060683 File Offset: 0x0005E883
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0006069C File Offset: 0x0005E89C
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000606B0 File Offset: 0x0005E8B0
			public unsafe InputControl<TValue> control
			{
				get
				{
					this.CheckValid();
					ReadOnlyArray<InputControl> controls = this.m_Owner.controls;
					if (controls.Count == 1 && !this.m_Owner.m_AddNewControls)
					{
						return (InputControl<TValue>)controls[0];
					}
					return (InputControl<TValue>)controls[this.header->controlIndex];
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0006070C File Offset: 0x0005E90C
			public InputStateHistory<TValue>.Record next
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num + 1 >= this.m_Owner.Count)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num + 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00060774 File Offset: 0x0005E974
			public InputStateHistory<TValue>.Record previous
			{
				get
				{
					this.CheckValid();
					int num = this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
					if (num - 1 < 0)
					{
						return default(InputStateHistory<TValue>.Record);
					}
					int num2 = this.m_Owner.UserIndexToRecordIndex(num - 1);
					return new InputStateHistory<TValue>.Record(this.m_Owner, num2, this.m_Owner.GetRecord(num2));
				}
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x000607D0 File Offset: 0x0005E9D0
			internal unsafe Record(InputStateHistory<TValue> owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x060014E9 RID: 5353 RVA: 0x000607EE File Offset: 0x0005E9EE
			internal Record(InputStateHistory<TValue> owner, int index)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = 0U;
			}

			// Token: 0x060014EA RID: 5354 RVA: 0x00060807 File Offset: 0x0005EA07
			public TValue ReadValue()
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x060014EB RID: 5355 RVA: 0x00060820 File Offset: 0x0005EA20
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x00060830 File Offset: 0x0005EA30
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x00060877 File Offset: 0x0005EA77
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x00060885 File Offset: 0x0005EA85
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x060014EF RID: 5359 RVA: 0x000608C0 File Offset: 0x0005EAC0
			public void CopyFrom(InputStateHistory<TValue>.Record record)
			{
				this.CheckValid();
				if (!record.valid)
				{
					throw new ArgumentException("Given history record is not valid", "record");
				}
				InputStateHistory.Record record2 = new InputStateHistory.Record(this.m_Owner, this.recordIndex, this.header);
				record2.CopyFrom(new InputStateHistory.Record(record.m_Owner, record.recordIndex, record.header));
				this.m_Version = record2.version;
			}

			// Token: 0x060014F0 RID: 5360 RVA: 0x00060932 File Offset: 0x0005EB32
			private unsafe void CheckValid()
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

			// Token: 0x060014F1 RID: 5361 RVA: 0x0006096D File Offset: 0x0005EB6D
			public bool Equals(InputStateHistory<TValue>.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x060014F2 RID: 5362 RVA: 0x0006099C File Offset: 0x0005EB9C
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory<TValue>.Record)
				{
					InputStateHistory<TValue>.Record record = (InputStateHistory<TValue>.Record)obj;
					return this.Equals(record);
				}
				return false;
			}

			// Token: 0x060014F3 RID: 5363 RVA: 0x000609C1 File Offset: 0x0005EBC1
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x060014F4 RID: 5364 RVA: 0x000609F3 File Offset: 0x0005EBF3
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValue(), this.time);
			}

			// Token: 0x04000B5D RID: 2909
			private readonly InputStateHistory<TValue> m_Owner;

			// Token: 0x04000B5E RID: 2910
			private readonly int m_IndexPlusOne;

			// Token: 0x04000B5F RID: 2911
			private uint m_Version;
		}
	}
}
