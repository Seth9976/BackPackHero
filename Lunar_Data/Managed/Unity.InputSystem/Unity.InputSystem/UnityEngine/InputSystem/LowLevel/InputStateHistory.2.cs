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
		// Token: 0x06000E7F RID: 3711 RVA: 0x000470E4 File Offset: 0x000452E4
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

		// Token: 0x06000E80 RID: 3712 RVA: 0x0004713C File Offset: 0x0004533C
		public InputStateHistory(InputControl<TValue> control)
			: base(control)
		{
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00047148 File Offset: 0x00045348
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

		// Token: 0x06000E82 RID: 3714 RVA: 0x000471F4 File Offset: 0x000453F4
		~InputStateHistory()
		{
			base.Destroy();
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00047220 File Offset: 0x00045420
		public unsafe InputStateHistory<TValue>.Record AddRecord(InputStateHistory<TValue>.Record record)
		{
			int num;
			InputStateHistory.RecordHeader* ptr = base.AllocateRecord(out num);
			InputStateHistory<TValue>.Record record2 = new InputStateHistory<TValue>.Record(this, num, ptr);
			record2.CopyFrom(record);
			return record2;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004724C File Offset: 0x0004544C
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

		// Token: 0x06000E85 RID: 3717 RVA: 0x000472E8 File Offset: 0x000454E8
		public new IEnumerator<InputStateHistory<TValue>.Record> GetEnumerator()
		{
			return new InputStateHistory<TValue>.Enumerator(this);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000472F5 File Offset: 0x000454F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700042C RID: 1068
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
			// Token: 0x060014D2 RID: 5330 RVA: 0x000603C0 File Offset: 0x0005E5C0
			public Enumerator(InputStateHistory<TValue> history)
			{
				this.m_History = history;
				this.m_Index = -1;
			}

			// Token: 0x060014D3 RID: 5331 RVA: 0x000603D0 File Offset: 0x0005E5D0
			public bool MoveNext()
			{
				if (this.m_Index + 1 >= this.m_History.Count)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x060014D4 RID: 5332 RVA: 0x000603F8 File Offset: 0x0005E5F8
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00060401 File Offset: 0x0005E601
			public InputStateHistory<TValue>.Record Current
			{
				get
				{
					return this.m_History[this.m_Index];
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00060414 File Offset: 0x0005E614
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060014D7 RID: 5335 RVA: 0x00060421 File Offset: 0x0005E621
			public void Dispose()
			{
			}

			// Token: 0x04000B5A RID: 2906
			private readonly InputStateHistory<TValue> m_History;

			// Token: 0x04000B5B RID: 2907
			private int m_Index;
		}

		// Token: 0x0200021C RID: 540
		public new struct Record : IEquatable<InputStateHistory<TValue>.Record>
		{
			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00060423 File Offset: 0x0005E623
			internal unsafe InputStateHistory.RecordHeader* header
			{
				get
				{
					return this.m_Owner.GetRecord(this.recordIndex);
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00060436 File Offset: 0x0005E636
			internal int recordIndex
			{
				get
				{
					return this.m_IndexPlusOne - 1;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x060014DA RID: 5338 RVA: 0x00060440 File Offset: 0x0005E640
			public unsafe bool valid
			{
				get
				{
					return this.m_Owner != null && this.m_IndexPlusOne != 0 && this.header->version == this.m_Version;
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x060014DB RID: 5339 RVA: 0x00060467 File Offset: 0x0005E667
			public InputStateHistory<TValue> owner
			{
				get
				{
					return this.m_Owner;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x060014DC RID: 5340 RVA: 0x0006046F File Offset: 0x0005E66F
			public int index
			{
				get
				{
					this.CheckValid();
					return this.m_Owner.RecordIndexToUserIndex(this.recordIndex);
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x060014DD RID: 5341 RVA: 0x00060488 File Offset: 0x0005E688
			public unsafe double time
			{
				get
				{
					this.CheckValid();
					return this.header->time;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x060014DE RID: 5342 RVA: 0x0006049C File Offset: 0x0005E69C
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

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x060014DF RID: 5343 RVA: 0x000604F8 File Offset: 0x0005E6F8
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

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00060560 File Offset: 0x0005E760
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

			// Token: 0x060014E1 RID: 5345 RVA: 0x000605BC File Offset: 0x0005E7BC
			internal unsafe Record(InputStateHistory<TValue> owner, int index, InputStateHistory.RecordHeader* header)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = header->version;
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x000605DA File Offset: 0x0005E7DA
			internal Record(InputStateHistory<TValue> owner, int index)
			{
				this.m_Owner = owner;
				this.m_IndexPlusOne = index + 1;
				this.m_Version = 0U;
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x000605F3 File Offset: 0x0005E7F3
			public TValue ReadValue()
			{
				this.CheckValid();
				return this.m_Owner.ReadValue<TValue>(this.header);
			}

			// Token: 0x060014E4 RID: 5348 RVA: 0x0006060C File Offset: 0x0005E80C
			public unsafe void* GetUnsafeMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeMemoryPtrUnchecked();
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x0006061C File Offset: 0x0005E81C
			internal unsafe void* GetUnsafeMemoryPtrUnchecked()
			{
				if (this.m_Owner.controls.Count == 1 && !this.m_Owner.m_AddNewControls)
				{
					return (void*)this.header->statePtrWithoutControlIndex;
				}
				return (void*)this.header->statePtrWithControlIndex;
			}

			// Token: 0x060014E6 RID: 5350 RVA: 0x00060663 File Offset: 0x0005E863
			public unsafe void* GetUnsafeExtraMemoryPtr()
			{
				this.CheckValid();
				return this.GetUnsafeExtraMemoryPtrUnchecked();
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x00060671 File Offset: 0x0005E871
			internal unsafe void* GetUnsafeExtraMemoryPtrUnchecked()
			{
				if (this.m_Owner.extraMemoryPerRecord == 0)
				{
					throw new InvalidOperationException("No extra memory has been set up for history records; set extraMemoryPerRecord");
				}
				return (void*)(this.header + this.m_Owner.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - this.m_Owner.extraMemoryPerRecord / sizeof(InputStateHistory.RecordHeader));
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x000606AC File Offset: 0x0005E8AC
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

			// Token: 0x060014E9 RID: 5353 RVA: 0x0006071E File Offset: 0x0005E91E
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

			// Token: 0x060014EA RID: 5354 RVA: 0x00060759 File Offset: 0x0005E959
			public bool Equals(InputStateHistory<TValue>.Record other)
			{
				return this.m_Owner == other.m_Owner && this.m_IndexPlusOne == other.m_IndexPlusOne && this.m_Version == other.m_Version;
			}

			// Token: 0x060014EB RID: 5355 RVA: 0x00060788 File Offset: 0x0005E988
			public override bool Equals(object obj)
			{
				if (obj is InputStateHistory<TValue>.Record)
				{
					InputStateHistory<TValue>.Record record = (InputStateHistory<TValue>.Record)obj;
					return this.Equals(record);
				}
				return false;
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x000607AD File Offset: 0x0005E9AD
			public override int GetHashCode()
			{
				return (((((this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0) * 397) ^ this.m_IndexPlusOne) * 397) ^ (int)this.m_Version;
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x000607DF File Offset: 0x0005E9DF
			public override string ToString()
			{
				if (!this.valid)
				{
					return "<Invalid>";
				}
				return string.Format("{{ control={0} value={1} time={2} }}", this.control, this.ReadValue(), this.time);
			}

			// Token: 0x04000B5C RID: 2908
			private readonly InputStateHistory<TValue> m_Owner;

			// Token: 0x04000B5D RID: 2909
			private readonly int m_IndexPlusOne;

			// Token: 0x04000B5E RID: 2910
			private uint m_Version;
		}
	}
}
