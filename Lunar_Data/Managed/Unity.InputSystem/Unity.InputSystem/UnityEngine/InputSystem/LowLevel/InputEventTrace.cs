using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public sealed class InputEventTrace : IDisposable, IEnumerable<InputEventPtr>, IEnumerable
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000437A5 File Offset: 0x000419A5
		public static FourCC FrameMarkerEvent
		{
			get
			{
				return new FourCC('F', 'R', 'M', 'E');
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000437B4 File Offset: 0x000419B4
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x000437BC File Offset: 0x000419BC
		public int deviceId
		{
			get
			{
				return this.m_DeviceId;
			}
			set
			{
				this.m_DeviceId = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000437C5 File Offset: 0x000419C5
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000437CD File Offset: 0x000419CD
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x000437D8 File Offset: 0x000419D8
		public bool recordFrameMarkers
		{
			get
			{
				return this.m_RecordFrameMarkers;
			}
			set
			{
				if (this.m_RecordFrameMarkers == value)
				{
					return;
				}
				this.m_RecordFrameMarkers = value;
				if (this.m_Enabled)
				{
					if (value)
					{
						InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
						return;
					}
					InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
				}
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00043824 File Offset: 0x00041A24
		public long eventCount
		{
			get
			{
				return this.m_EventCount;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0004382C File Offset: 0x00041A2C
		public long totalEventSizeInBytes
		{
			get
			{
				return this.m_EventSizeInBytes;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00043834 File Offset: 0x00041A34
		public long allocatedSizeInBytes
		{
			get
			{
				if (this.m_EventBuffer == null)
				{
					return 0L;
				}
				return this.m_EventBufferSize;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00043849 File Offset: 0x00041A49
		public long maxSizeInBytes
		{
			get
			{
				return this.m_MaxEventBufferSize;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00043851 File Offset: 0x00041A51
		public ReadOnlyArray<InputEventTrace.DeviceInfo> deviceInfos
		{
			get
			{
				return this.m_DeviceInfos;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0004385E File Offset: 0x00041A5E
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x00043866 File Offset: 0x00041A66
		public Func<InputEventPtr, InputDevice, bool> onFilterEvent
		{
			get
			{
				return this.m_OnFilterEvent;
			}
			set
			{
				this.m_OnFilterEvent = value;
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000D8F RID: 3471 RVA: 0x0004386F File Offset: 0x00041A6F
		// (remove) Token: 0x06000D90 RID: 3472 RVA: 0x0004387D File Offset: 0x00041A7D
		public event Action<InputEventPtr> onEvent
		{
			add
			{
				this.m_EventListeners.AddCallback(value);
			}
			remove
			{
				this.m_EventListeners.RemoveCallback(value);
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0004388B File Offset: 0x00041A8B
		public InputEventTrace(InputDevice device, long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
			: this(bufferSizeInBytes, growBuffer, maxBufferSizeInBytes, growIncrementSizeInBytes)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			this.m_DeviceId = device.deviceId;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000438B4 File Offset: 0x00041AB4
		public InputEventTrace(long bufferSizeInBytes = 1048576L, bool growBuffer = false, long maxBufferSizeInBytes = -1L, long growIncrementSizeInBytes = -1L)
		{
			this.m_EventBufferSize = (long)((ulong)((uint)bufferSizeInBytes));
			if (!growBuffer)
			{
				this.m_MaxEventBufferSize = this.m_EventBufferSize;
				return;
			}
			if (maxBufferSizeInBytes < 0L)
			{
				this.m_MaxEventBufferSize = 268435456L;
			}
			else
			{
				this.m_MaxEventBufferSize = maxBufferSizeInBytes;
			}
			if (growIncrementSizeInBytes < 0L)
			{
				this.m_GrowIncrementSize = 1048576L;
				return;
			}
			this.m_GrowIncrementSize = growIncrementSizeInBytes;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00043918 File Offset: 0x00041B18
		public void WriteTo(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream fileStream = File.OpenWrite(filePath))
			{
				this.WriteTo(fileStream);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00043964 File Offset: 0x00041B64
		public unsafe void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream does not support seeking", "stream");
			}
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			InputEventTrace.FileFlags fileFlags = (InputEventTrace.FileFlags)0;
			if (InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsInFixedUpdate)
			{
				fileFlags |= InputEventTrace.FileFlags.FixedUpdate;
			}
			binaryWriter.Write(InputEventTrace.kFileFormat);
			binaryWriter.Write(InputEventTrace.kFileVersion);
			binaryWriter.Write((int)fileFlags);
			binaryWriter.Write((int)Application.platform);
			binaryWriter.Write((ulong)this.m_EventCount);
			binaryWriter.Write((ulong)this.m_EventSizeInBytes);
			foreach (InputEventPtr inputEventPtr in this)
			{
				uint sizeInBytes = inputEventPtr.sizeInBytes;
				byte[] array = new byte[sizeInBytes];
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					UnsafeUtility.MemCpy((void*)ptr, (void*)inputEventPtr.data, (long)((ulong)sizeInBytes));
					binaryWriter.Write(array);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			binaryWriter.Flush();
			long position = stream.Position;
			int num = this.m_DeviceInfos.LengthSafe<InputEventTrace.DeviceInfo>();
			binaryWriter.Write(num);
			for (int i = 0; i < num; i++)
			{
				ref InputEventTrace.DeviceInfo ptr2 = ref this.m_DeviceInfos[i];
				binaryWriter.Write(ptr2.deviceId);
				binaryWriter.Write(ptr2.layout);
				binaryWriter.Write(ptr2.stateFormat);
				binaryWriter.Write(ptr2.stateSizeInBytes);
				binaryWriter.Write(ptr2.m_FullLayoutJson ?? string.Empty);
			}
			binaryWriter.Flush();
			long num2 = stream.Position - position;
			binaryWriter.Write(num2);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00043B34 File Offset: 0x00041D34
		public void ReadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				this.ReadFrom(fileStream);
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00043B80 File Offset: 0x00041D80
		public unsafe void ReadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			if (binaryReader.ReadInt32() != InputEventTrace.kFileFormat)
			{
				throw new IOException(string.Format("Stream does not appear to be an InputEventTrace (no '{0}' code)", InputEventTrace.kFileFormat));
			}
			if (binaryReader.ReadInt32() > InputEventTrace.kFileVersion)
			{
				throw new IOException(string.Format("Stream is an InputEventTrace but a newer version (expected version {0} or below)", InputEventTrace.kFileVersion));
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			ulong num = binaryReader.ReadUInt64();
			ulong num2 = binaryReader.ReadUInt64();
			byte* eventBuffer = this.m_EventBuffer;
			if (num > 0UL && num2 > 0UL)
			{
				byte* ptr;
				if (this.m_EventBuffer != null && this.m_EventBufferSize >= (long)num2)
				{
					ptr = this.m_EventBuffer;
				}
				else
				{
					ptr = (byte*)UnsafeUtility.Malloc((long)num2, 4, Allocator.Persistent);
					this.m_EventBufferSize = (long)num2;
				}
				try
				{
					byte* ptr2 = ptr;
					byte* ptr3 = ptr2 + num2;
					long num3 = 0L;
					for (ulong num4 = 0UL; num4 < num; num4 += 1UL)
					{
						int num5 = binaryReader.ReadInt32();
						uint num6 = (uint)binaryReader.ReadUInt16();
						uint num7 = (uint)binaryReader.ReadUInt16();
						if ((ulong)num6 > (ulong)((long)(ptr3 - ptr2)))
						{
							break;
						}
						*(int*)ptr2 = num5;
						ptr2 += 4;
						*(short*)ptr2 = (short)((ushort)num6);
						ptr2 += 2;
						*(short*)ptr2 = (short)((ushort)num7);
						ptr2 += 2;
						int num8 = (int)(num6 - 4U - 2U - 2U);
						byte[] array = binaryReader.ReadBytes(num8);
						try
						{
							byte[] array2;
							byte* ptr4;
							if ((array2 = array) == null || array2.Length == 0)
							{
								ptr4 = null;
							}
							else
							{
								ptr4 = &array2[0];
							}
							UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr4, (long)num8);
						}
						finally
						{
							byte[] array2 = null;
						}
						ptr2 += num8.AlignToMultipleOf(4);
						num3 += (long)((ulong)num6.AlignToMultipleOf(4U));
						if (ptr2 >= ptr3)
						{
							break;
						}
					}
					int num9 = binaryReader.ReadInt32();
					InputEventTrace.DeviceInfo[] array3 = new InputEventTrace.DeviceInfo[num9];
					for (int i = 0; i < num9; i++)
					{
						array3[i] = new InputEventTrace.DeviceInfo
						{
							deviceId = binaryReader.ReadInt32(),
							layout = binaryReader.ReadString(),
							stateFormat = binaryReader.ReadInt32(),
							stateSizeInBytes = binaryReader.ReadInt32(),
							m_FullLayoutJson = binaryReader.ReadString()
						};
					}
					this.m_EventBuffer = ptr;
					this.m_EventBufferHead = this.m_EventBuffer;
					this.m_EventBufferTail = ptr3;
					this.m_EventCount = (long)num;
					this.m_EventSizeInBytes = num3;
					this.m_DeviceInfos = array3;
					goto IL_0296;
				}
				catch
				{
					if (ptr != eventBuffer)
					{
						UnsafeUtility.Free((void*)ptr, Allocator.Persistent);
					}
					throw;
				}
			}
			this.m_EventBuffer = null;
			this.m_EventBufferHead = null;
			this.m_EventBufferTail = null;
			IL_0296:
			if (this.m_EventBuffer != eventBuffer && eventBuffer != null)
			{
				UnsafeUtility.Free((void*)eventBuffer, Allocator.Persistent);
			}
			this.m_ChangeCounter++;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00043E7C File Offset: 0x0004207C
		public static InputEventTrace LoadFrom(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}
			InputEventTrace inputEventTrace;
			using (FileStream fileStream = File.OpenRead(filePath))
			{
				inputEventTrace = InputEventTrace.LoadFrom(fileStream);
			}
			return inputEventTrace;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00043EC8 File Offset: 0x000420C8
		public static InputEventTrace LoadFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream must be readable", "stream");
			}
			InputEventTrace inputEventTrace = new InputEventTrace(1048576L, false, -1L, -1L);
			inputEventTrace.ReadFrom(stream);
			return inputEventTrace;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00043F07 File Offset: 0x00042107
		public InputEventTrace.ReplayController Replay()
		{
			this.Disable();
			return new InputEventTrace.ReplayController(this);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00043F18 File Offset: 0x00042118
		public unsafe bool Resize(long newBufferSize, long newMaxBufferSize = -1L)
		{
			if (newBufferSize <= 0L)
			{
				throw new ArgumentException("Size must be positive", "newBufferSize");
			}
			if (this.m_EventBufferSize == newBufferSize)
			{
				return true;
			}
			if (newMaxBufferSize < newBufferSize)
			{
				newMaxBufferSize = newBufferSize;
			}
			byte* ptr = (byte*)UnsafeUtility.Malloc(newBufferSize, 4, Allocator.Persistent);
			if (ptr == null)
			{
				return false;
			}
			if (this.m_EventCount > 0L)
			{
				if (newBufferSize < this.m_EventBufferSize || this.m_HasWrapped)
				{
					InputEventPtr inputEventPtr = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
					InputEvent* ptr2 = (InputEvent*)ptr;
					int num = 0;
					int num2 = 0;
					long num3 = this.m_EventSizeInBytes;
					int num4 = 0;
					while ((long)num4 < this.m_EventCount)
					{
						uint sizeInBytes = inputEventPtr.sizeInBytes;
						uint num5 = sizeInBytes.AlignToMultipleOf(4U);
						if (num3 <= newBufferSize)
						{
							UnsafeUtility.MemCpy((void*)ptr2, (void*)inputEventPtr.ToPointer(), (long)((ulong)sizeInBytes));
							ptr2 = InputEvent.GetNextInMemory(ptr2);
							num2 += (int)num5;
							num++;
						}
						num3 -= (long)((ulong)num5);
						if (!this.GetNextEvent(ref inputEventPtr))
						{
							break;
						}
						num4++;
					}
					this.m_HasWrapped = false;
					this.m_EventCount = (long)num;
					this.m_EventSizeInBytes = (long)num2;
				}
				else
				{
					UnsafeUtility.MemCpy((void*)ptr, (void*)this.m_EventBufferHead, this.m_EventSizeInBytes);
				}
			}
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
			}
			this.m_EventBufferSize = newBufferSize;
			this.m_EventBuffer = ptr;
			this.m_EventBufferHead = ptr;
			this.m_EventBufferTail = this.m_EventBuffer + this.m_EventSizeInBytes;
			this.m_MaxEventBufferSize = newMaxBufferSize;
			this.m_ChangeCounter++;
			return true;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0004407C File Offset: 0x0004227C
		public unsafe void Clear()
		{
			byte* ptr = default(byte*);
			this.m_EventBufferTail = ptr;
			this.m_EventBufferHead = ptr;
			this.m_EventCount = 0L;
			this.m_EventSizeInBytes = 0L;
			this.m_ChangeCounter++;
			this.m_DeviceInfos = null;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000440C4 File Offset: 0x000422C4
		public void Enable()
		{
			if (this.m_Enabled)
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				this.Allocate();
			}
			InputSystem.onEvent += new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			if (this.m_RecordFrameMarkers)
			{
				InputSystem.onBeforeUpdate += this.OnBeforeUpdate;
			}
			this.m_Enabled = true;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00044125 File Offset: 0x00042325
		public void Disable()
		{
			if (!this.m_Enabled)
			{
				return;
			}
			InputSystem.onEvent -= new Action<InputEventPtr, InputDevice>(this.OnInputEvent);
			InputSystem.onBeforeUpdate -= this.OnBeforeUpdate;
			this.m_Enabled = false;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00044164 File Offset: 0x00042364
		public unsafe bool GetNextEvent(ref InputEventPtr current)
		{
			if (this.m_EventBuffer == null)
			{
				return false;
			}
			if (this.m_EventBufferHead == null)
			{
				return false;
			}
			if (!current.valid)
			{
				current = new InputEventPtr((InputEvent*)this.m_EventBufferHead);
				return true;
			}
			byte* ptr = (byte*)current.Next().data;
			byte* ptr2 = this.m_EventBuffer + this.m_EventBufferSize;
			if (ptr == this.m_EventBufferTail)
			{
				return false;
			}
			if ((long)(ptr2 - ptr) < 20L || ((InputEvent*)ptr)->sizeInBytes == 0U)
			{
				ptr = this.m_EventBuffer;
				if (ptr == (byte*)current.ToPointer())
				{
					return false;
				}
			}
			current = new InputEventPtr((InputEvent*)ptr);
			return true;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000441FF File Offset: 0x000423FF
		public IEnumerator<InputEventPtr> GetEnumerator()
		{
			return new InputEventTrace.Enumerator(this);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00044207 File Offset: 0x00042407
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0004420F File Offset: 0x0004240F
		public void Dispose()
		{
			this.Disable();
			this.Release();
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0004421D File Offset: 0x0004241D
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x00044226 File Offset: 0x00042426
		private unsafe byte* m_EventBuffer
		{
			get
			{
				return this.m_EventBufferStorage;
			}
			set
			{
				this.m_EventBufferStorage = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x00044230 File Offset: 0x00042430
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x00044239 File Offset: 0x00042439
		private unsafe byte* m_EventBufferHead
		{
			get
			{
				return this.m_EventBufferHeadStorage;
			}
			set
			{
				this.m_EventBufferHeadStorage = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00044243 File Offset: 0x00042443
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0004424C File Offset: 0x0004244C
		private unsafe byte* m_EventBufferTail
		{
			get
			{
				return this.m_EventBufferTailStorage;
			}
			set
			{
				this.m_EventBufferTailStorage = value;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00044256 File Offset: 0x00042456
		private unsafe void Allocate()
		{
			this.m_EventBuffer = (byte*)UnsafeUtility.Malloc(this.m_EventBufferSize, 4, Allocator.Persistent);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0004426B File Offset: 0x0004246B
		private unsafe void Release()
		{
			this.Clear();
			if (this.m_EventBuffer != null)
			{
				UnsafeUtility.Free((void*)this.m_EventBuffer, Allocator.Persistent);
				this.m_EventBuffer = null;
			}
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00044294 File Offset: 0x00042494
		private unsafe void OnBeforeUpdate()
		{
			if (this.m_RecordFrameMarkers)
			{
				InputEvent inputEvent = new InputEvent
				{
					type = InputEventTrace.FrameMarkerEvent,
					internalTime = InputRuntime.s_Instance.currentTime,
					sizeInBytes = (uint)UnsafeUtility.SizeOf<InputEvent>()
				};
				this.OnInputEvent(new InputEventPtr((InputEvent*)UnsafeUtility.AddressOf<InputEvent>(ref inputEvent)), null);
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000442F0 File Offset: 0x000424F0
		private unsafe void OnInputEvent(InputEventPtr inputEvent, InputDevice device)
		{
			if (inputEvent.handled)
			{
				return;
			}
			if (this.m_DeviceId != 0 && inputEvent.deviceId != this.m_DeviceId && inputEvent.type != InputEventTrace.FrameMarkerEvent)
			{
				return;
			}
			if (this.m_OnFilterEvent != null && !this.m_OnFilterEvent(inputEvent, device))
			{
				return;
			}
			if (this.m_EventBuffer == null)
			{
				return;
			}
			uint num = inputEvent.sizeInBytes.AlignToMultipleOf(4U);
			if ((ulong)num > (ulong)this.m_MaxEventBufferSize)
			{
				return;
			}
			if (this.m_EventBufferTail == null)
			{
				this.m_EventBufferHead = this.m_EventBuffer;
				this.m_EventBufferTail = this.m_EventBuffer;
			}
			byte* ptr = this.m_EventBufferTail + num;
			bool flag = ptr != this.m_EventBufferHead && this.m_EventBufferHead != this.m_EventBuffer;
			if (ptr != this.m_EventBuffer + this.m_EventBufferSize)
			{
				if (this.m_EventBufferSize < this.m_MaxEventBufferSize && !this.m_HasWrapped)
				{
					long num2 = Math.Max(this.m_GrowIncrementSize, (long)((ulong)num.AlignToMultipleOf(4U)));
					long num3 = this.m_EventBufferSize + num2;
					if (num3 > this.m_MaxEventBufferSize)
					{
						num3 = this.m_MaxEventBufferSize;
					}
					if (num3 < (long)((ulong)num))
					{
						return;
					}
					this.Resize(num3, -1L);
					ptr = this.m_EventBufferTail + num;
				}
				long num4 = this.m_EventBufferSize - (long)(this.m_EventBufferTail - this.m_EventBuffer);
				if (num4 < (long)((ulong)num))
				{
					this.m_HasWrapped = true;
					if (num4 >= 20L)
					{
						UnsafeUtility.MemClear((void*)this.m_EventBufferTail, 20L);
					}
					this.m_EventBufferTail = this.m_EventBuffer;
					ptr = this.m_EventBuffer + num;
					if (flag)
					{
						this.m_EventBufferHead = this.m_EventBuffer;
					}
					flag = ptr != this.m_EventBufferHead;
				}
			}
			if (flag)
			{
				byte* ptr2 = this.m_EventBufferHead;
				byte* ptr3 = this.m_EventBuffer + this.m_EventBufferSize - 20;
				while (ptr2 < ptr)
				{
					uint sizeInBytes = ((InputEvent*)ptr2)->sizeInBytes;
					ptr2 += sizeInBytes;
					this.m_EventCount -= 1L;
					this.m_EventSizeInBytes -= (long)((ulong)sizeInBytes);
					if (ptr2 != ptr3 || ((InputEvent*)ptr2)->sizeInBytes == 0U)
					{
						ptr2 = this.m_EventBuffer;
						break;
					}
				}
				this.m_EventBufferHead = ptr2;
			}
			byte* eventBufferTail = this.m_EventBufferTail;
			this.m_EventBufferTail = ptr;
			UnsafeUtility.MemCpy((void*)eventBufferTail, (void*)inputEvent.data, (long)((ulong)inputEvent.sizeInBytes));
			this.m_ChangeCounter++;
			this.m_EventCount += 1L;
			this.m_EventSizeInBytes += (long)((ulong)num);
			if (device != null)
			{
				bool flag2 = false;
				if (this.m_DeviceInfos != null)
				{
					for (int i = 0; i < this.m_DeviceInfos.Length; i++)
					{
						if (this.m_DeviceInfos[i].deviceId == device.deviceId)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					ArrayHelpers.Append<InputEventTrace.DeviceInfo>(ref this.m_DeviceInfos, new InputEventTrace.DeviceInfo
					{
						m_DeviceId = device.deviceId,
						m_Layout = device.layout,
						m_StateFormat = device.stateBlock.format,
						m_StateSizeInBytes = (int)device.stateBlock.alignedSizeInBytes,
						m_FullLayoutJson = (InputControlLayout.s_Layouts.IsGeneratedLayout(device.m_Layout) ? InputSystem.LoadLayout(device.layout).ToJson() : null)
					});
				}
			}
			if (this.m_EventListeners.length > 0)
			{
				DelegateHelpers.InvokeCallbacksSafe<InputEventPtr>(ref this.m_EventListeners, new InputEventPtr((InputEvent*)eventBufferTail), "InputEventTrace.onEvent", null);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0004465C File Offset: 0x0004285C
		private static FourCC kFileFormat
		{
			get
			{
				return new FourCC('I', 'E', 'V', 'T');
			}
		}

		// Token: 0x0400057A RID: 1402
		private const int kDefaultBufferSize = 1048576;

		// Token: 0x0400057B RID: 1403
		[NonSerialized]
		private int m_ChangeCounter;

		// Token: 0x0400057C RID: 1404
		[NonSerialized]
		private bool m_Enabled;

		// Token: 0x0400057D RID: 1405
		[NonSerialized]
		private Func<InputEventPtr, InputDevice, bool> m_OnFilterEvent;

		// Token: 0x0400057E RID: 1406
		[SerializeField]
		private int m_DeviceId;

		// Token: 0x0400057F RID: 1407
		[NonSerialized]
		private CallbackArray<Action<InputEventPtr>> m_EventListeners;

		// Token: 0x04000580 RID: 1408
		[SerializeField]
		private long m_EventBufferSize;

		// Token: 0x04000581 RID: 1409
		[SerializeField]
		private long m_MaxEventBufferSize;

		// Token: 0x04000582 RID: 1410
		[SerializeField]
		private long m_GrowIncrementSize;

		// Token: 0x04000583 RID: 1411
		[SerializeField]
		private long m_EventCount;

		// Token: 0x04000584 RID: 1412
		[SerializeField]
		private long m_EventSizeInBytes;

		// Token: 0x04000585 RID: 1413
		[SerializeField]
		private ulong m_EventBufferStorage;

		// Token: 0x04000586 RID: 1414
		[SerializeField]
		private ulong m_EventBufferHeadStorage;

		// Token: 0x04000587 RID: 1415
		[SerializeField]
		private ulong m_EventBufferTailStorage;

		// Token: 0x04000588 RID: 1416
		[SerializeField]
		private bool m_HasWrapped;

		// Token: 0x04000589 RID: 1417
		[SerializeField]
		private bool m_RecordFrameMarkers;

		// Token: 0x0400058A RID: 1418
		[SerializeField]
		private InputEventTrace.DeviceInfo[] m_DeviceInfos;

		// Token: 0x0400058B RID: 1419
		private static int kFileVersion = 1;

		// Token: 0x0200020C RID: 524
		private class Enumerator : IEnumerator<InputEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x06001479 RID: 5241 RVA: 0x0005F31C File Offset: 0x0005D51C
			public Enumerator(InputEventTrace trace)
			{
				this.m_Trace = trace;
				this.m_ChangeCounter = trace.m_ChangeCounter;
			}

			// Token: 0x0600147A RID: 5242 RVA: 0x0005F337 File Offset: 0x0005D537
			public void Dispose()
			{
				this.m_Trace = null;
				this.m_Current = default(InputEventPtr);
			}

			// Token: 0x0600147B RID: 5243 RVA: 0x0005F34C File Offset: 0x0005D54C
			public bool MoveNext()
			{
				if (this.m_Trace == null)
				{
					throw new ObjectDisposedException(this.ToString());
				}
				if (this.m_Trace.m_ChangeCounter != this.m_ChangeCounter)
				{
					throw new InvalidOperationException("Trace has been modified while enumerating!");
				}
				return this.m_Trace.GetNextEvent(ref this.m_Current);
			}

			// Token: 0x0600147C RID: 5244 RVA: 0x0005F39C File Offset: 0x0005D59C
			public void Reset()
			{
				this.m_Current = default(InputEventPtr);
				this.m_ChangeCounter = this.m_Trace.m_ChangeCounter;
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x0600147D RID: 5245 RVA: 0x0005F3BB File Offset: 0x0005D5BB
			public InputEventPtr Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x0600147E RID: 5246 RVA: 0x0005F3C3 File Offset: 0x0005D5C3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000B2B RID: 2859
			private InputEventTrace m_Trace;

			// Token: 0x04000B2C RID: 2860
			private int m_ChangeCounter;

			// Token: 0x04000B2D RID: 2861
			internal InputEventPtr m_Current;
		}

		// Token: 0x0200020D RID: 525
		[Flags]
		private enum FileFlags
		{
			// Token: 0x04000B2F RID: 2863
			FixedUpdate = 1
		}

		// Token: 0x0200020E RID: 526
		public class ReplayController : IDisposable
		{
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x0600147F RID: 5247 RVA: 0x0005F3D0 File Offset: 0x0005D5D0
			public InputEventTrace trace
			{
				get
				{
					return this.m_EventTrace;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x06001480 RID: 5248 RVA: 0x0005F3D8 File Offset: 0x0005D5D8
			// (set) Token: 0x06001481 RID: 5249 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
			public bool finished { get; private set; }

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x06001482 RID: 5250 RVA: 0x0005F3E9 File Offset: 0x0005D5E9
			// (set) Token: 0x06001483 RID: 5251 RVA: 0x0005F3F1 File Offset: 0x0005D5F1
			public bool paused { get; set; }

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x06001484 RID: 5252 RVA: 0x0005F3FA File Offset: 0x0005D5FA
			// (set) Token: 0x06001485 RID: 5253 RVA: 0x0005F402 File Offset: 0x0005D602
			public int position { get; private set; }

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x0005F40B File Offset: 0x0005D60B
			public IEnumerable<InputDevice> createdDevices
			{
				get
				{
					return this.m_CreatedDevices;
				}
			}

			// Token: 0x06001487 RID: 5255 RVA: 0x0005F418 File Offset: 0x0005D618
			internal ReplayController(InputEventTrace trace)
			{
				if (trace == null)
				{
					throw new ArgumentNullException("trace");
				}
				this.m_EventTrace = trace;
			}

			// Token: 0x06001488 RID: 5256 RVA: 0x0005F438 File Offset: 0x0005D638
			public void Dispose()
			{
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				this.finished = true;
				foreach (InputDevice inputDevice in this.m_CreatedDevices)
				{
					InputSystem.RemoveDevice(inputDevice);
				}
				this.m_CreatedDevices = default(InlinedArray<InputDevice>);
			}

			// Token: 0x06001489 RID: 5257 RVA: 0x0005F4A8 File Offset: 0x0005D6A8
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(InputDevice recordedDevice, InputDevice playbackDevice)
			{
				if (recordedDevice == null)
				{
					throw new ArgumentNullException("recordedDevice");
				}
				if (playbackDevice == null)
				{
					throw new ArgumentNullException("playbackDevice");
				}
				this.WithDeviceMappedFromTo(recordedDevice.deviceId, playbackDevice.deviceId);
				return this;
			}

			// Token: 0x0600148A RID: 5258 RVA: 0x0005F4DC File Offset: 0x0005D6DC
			public InputEventTrace.ReplayController WithDeviceMappedFromTo(int recordedDeviceId, int playbackDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					if (this.m_DeviceIDMappings[i].Key == recordedDeviceId)
					{
						if (recordedDeviceId == playbackDeviceId)
						{
							this.m_DeviceIDMappings.RemoveAtWithCapacity(i);
						}
						else
						{
							this.m_DeviceIDMappings[i] = new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId);
						}
						return this;
					}
				}
				if (recordedDeviceId == playbackDeviceId)
				{
					return this;
				}
				this.m_DeviceIDMappings.AppendWithCapacity(new KeyValuePair<int, int>(recordedDeviceId, playbackDeviceId), 10);
				return this;
			}

			// Token: 0x0600148B RID: 5259 RVA: 0x0005F559 File Offset: 0x0005D759
			public InputEventTrace.ReplayController WithAllDevicesMappedToNewInstances()
			{
				this.m_CreateNewDevices = true;
				return this;
			}

			// Token: 0x0600148C RID: 5260 RVA: 0x0005F563 File Offset: 0x0005D763
			public InputEventTrace.ReplayController OnFinished(Action action)
			{
				this.m_OnFinished = action;
				return this;
			}

			// Token: 0x0600148D RID: 5261 RVA: 0x0005F56D File Offset: 0x0005D76D
			public InputEventTrace.ReplayController OnEvent(Action<InputEventPtr> action)
			{
				this.m_OnEvent = action;
				return this;
			}

			// Token: 0x0600148E RID: 5262 RVA: 0x0005F578 File Offset: 0x0005D778
			public InputEventTrace.ReplayController PlayOneEvent()
			{
				InputEventPtr inputEventPtr;
				if (!this.MoveNext(true, out inputEventPtr))
				{
					throw new InvalidOperationException("No more events");
				}
				this.QueueEvent(inputEventPtr);
				return this;
			}

			// Token: 0x0600148F RID: 5263 RVA: 0x0005F5A3 File Offset: 0x0005D7A3
			public InputEventTrace.ReplayController Rewind()
			{
				this.m_Enumerator = null;
				this.m_AllEventsByTime = null;
				this.m_AllEventsByTimeIndex = -1;
				this.position = 0;
				return this;
			}

			// Token: 0x06001490 RID: 5264 RVA: 0x0005F5C2 File Offset: 0x0005D7C2
			public InputEventTrace.ReplayController PlayAllFramesOneByOne()
			{
				this.finished = false;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x06001491 RID: 5265 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
			public InputEventTrace.ReplayController PlayAllEvents()
			{
				this.finished = false;
				try
				{
					InputEventPtr inputEventPtr;
					while (this.MoveNext(true, out inputEventPtr))
					{
						this.QueueEvent(inputEventPtr);
					}
				}
				finally
				{
					this.Finished();
				}
				return this;
			}

			// Token: 0x06001492 RID: 5266 RVA: 0x0005F624 File Offset: 0x0005D824
			public InputEventTrace.ReplayController PlayAllEventsAccordingToTimestamps()
			{
				List<InputEventPtr> list = new List<InputEventPtr>();
				InputEventPtr inputEventPtr;
				while (this.MoveNext(true, out inputEventPtr))
				{
					list.Add(inputEventPtr);
				}
				list.Sort((InputEventPtr a, InputEventPtr b) => a.time.CompareTo(b.time));
				this.m_Enumerator.Dispose();
				this.m_Enumerator = null;
				this.m_AllEventsByTime = list;
				this.position = 0;
				this.finished = false;
				this.m_StartTimeAsPerFirstEvent = -1.0;
				this.m_AllEventsByTimeIndex = -1;
				InputSystem.onBeforeUpdate += this.OnBeginFrame;
				return this;
			}

			// Token: 0x06001493 RID: 5267 RVA: 0x0005F6C0 File Offset: 0x0005D8C0
			private void OnBeginFrame()
			{
				if (this.paused)
				{
					return;
				}
				InputEventPtr inputEventPtr;
				if (!this.MoveNext(false, out inputEventPtr))
				{
					if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
					{
						this.Finished();
					}
					return;
				}
				int num;
				if (inputEventPtr.type == InputEventTrace.FrameMarkerEvent)
				{
					InputEventPtr inputEventPtr2;
					if (!this.MoveNext(false, out inputEventPtr2))
					{
						this.Finished();
						return;
					}
					if (inputEventPtr2.type == InputEventTrace.FrameMarkerEvent)
					{
						num = this.position - 1;
						this.position = num;
						this.m_Enumerator.m_Current = inputEventPtr;
						return;
					}
					inputEventPtr = inputEventPtr2;
				}
				for (;;)
				{
					this.QueueEvent(inputEventPtr);
					InputEventPtr inputEventPtr3;
					if (!this.MoveNext(false, out inputEventPtr3))
					{
						break;
					}
					if (inputEventPtr3.type == InputEventTrace.FrameMarkerEvent)
					{
						goto Block_9;
					}
					inputEventPtr = inputEventPtr3;
				}
				if (this.m_AllEventsByTime == null || this.m_AllEventsByTimeIndex >= this.m_AllEventsByTime.Count)
				{
					this.Finished();
					return;
				}
				return;
				Block_9:
				this.m_Enumerator.m_Current = inputEventPtr;
				num = this.position - 1;
				this.position = num;
			}

			// Token: 0x06001494 RID: 5268 RVA: 0x0005F7C2 File Offset: 0x0005D9C2
			private void Finished()
			{
				this.finished = true;
				InputSystem.onBeforeUpdate -= this.OnBeginFrame;
				Action onFinished = this.m_OnFinished;
				if (onFinished == null)
				{
					return;
				}
				onFinished();
			}

			// Token: 0x06001495 RID: 5269 RVA: 0x0005F7EC File Offset: 0x0005D9EC
			private void QueueEvent(InputEventPtr eventPtr)
			{
				double internalTime = eventPtr.internalTime;
				if (this.m_AllEventsByTime != null)
				{
					eventPtr.internalTime = this.m_StartTimeAsPerRuntime + (eventPtr.internalTime - this.m_StartTimeAsPerFirstEvent);
				}
				else
				{
					eventPtr.internalTime = InputRuntime.s_Instance.currentTime;
				}
				int id = eventPtr.id;
				int deviceId = eventPtr.deviceId;
				eventPtr.deviceId = this.ApplyDeviceMapping(deviceId);
				Action<InputEventPtr> onEvent = this.m_OnEvent;
				if (onEvent != null)
				{
					onEvent(eventPtr);
				}
				try
				{
					InputSystem.QueueEvent(eventPtr);
				}
				finally
				{
					eventPtr.internalTime = internalTime;
					eventPtr.id = id;
					eventPtr.deviceId = deviceId;
				}
			}

			// Token: 0x06001496 RID: 5270 RVA: 0x0005F89C File Offset: 0x0005DA9C
			private bool MoveNext(bool skipFrameEvents, out InputEventPtr eventPtr)
			{
				eventPtr = default(InputEventPtr);
				int num;
				if (this.m_AllEventsByTime == null)
				{
					if (this.m_Enumerator == null)
					{
						this.m_Enumerator = new InputEventTrace.Enumerator(this.m_EventTrace);
					}
					while (this.m_Enumerator.MoveNext())
					{
						num = this.position + 1;
						this.position = num;
						eventPtr = this.m_Enumerator.Current;
						if (!skipFrameEvents || !(eventPtr.type == InputEventTrace.FrameMarkerEvent))
						{
							return true;
						}
					}
					return false;
				}
				if (this.m_AllEventsByTimeIndex + 1 >= this.m_AllEventsByTime.Count)
				{
					this.position = this.m_AllEventsByTime.Count;
					this.m_AllEventsByTimeIndex = this.m_AllEventsByTime.Count;
					return false;
				}
				if (this.m_AllEventsByTimeIndex < 0)
				{
					this.m_StartTimeAsPerFirstEvent = this.m_AllEventsByTime[0].internalTime;
					this.m_StartTimeAsPerRuntime = InputRuntime.s_Instance.currentTime;
				}
				else if (this.m_AllEventsByTimeIndex < this.m_AllEventsByTime.Count - 1 && this.m_AllEventsByTime[this.m_AllEventsByTimeIndex + 1].internalTime > this.m_StartTimeAsPerFirstEvent + (InputRuntime.s_Instance.currentTime - this.m_StartTimeAsPerRuntime))
				{
					return false;
				}
				this.m_AllEventsByTimeIndex++;
				num = this.position + 1;
				this.position = num;
				eventPtr = this.m_AllEventsByTime[this.m_AllEventsByTimeIndex];
				return true;
			}

			// Token: 0x06001497 RID: 5271 RVA: 0x0005FA0C File Offset: 0x0005DC0C
			private int ApplyDeviceMapping(int originalDeviceId)
			{
				for (int i = 0; i < this.m_DeviceIDMappings.length; i++)
				{
					KeyValuePair<int, int> keyValuePair = this.m_DeviceIDMappings[i];
					if (keyValuePair.Key == originalDeviceId)
					{
						return keyValuePair.Value;
					}
				}
				if (this.m_CreateNewDevices)
				{
					try
					{
						int num = this.m_EventTrace.deviceInfos.IndexOf((InputEventTrace.DeviceInfo x) => x.deviceId == originalDeviceId);
						if (num != -1)
						{
							InputEventTrace.DeviceInfo deviceInfo = this.m_EventTrace.deviceInfos[num];
							InternedString internedString = new InternedString(deviceInfo.layout);
							if (!InputControlLayout.s_Layouts.HasLayout(internedString))
							{
								if (string.IsNullOrEmpty(deviceInfo.m_FullLayoutJson))
								{
									return originalDeviceId;
								}
								InputSystem.RegisterLayout(deviceInfo.m_FullLayoutJson, null, null);
							}
							InputDevice inputDevice = InputSystem.AddDevice(internedString, null, null);
							this.WithDeviceMappedFromTo(originalDeviceId, inputDevice.deviceId);
							this.m_CreatedDevices.AppendWithCapacity(inputDevice, 10);
							return inputDevice.deviceId;
						}
					}
					catch
					{
					}
				}
				return originalDeviceId;
			}

			// Token: 0x04000B33 RID: 2867
			private InputEventTrace m_EventTrace;

			// Token: 0x04000B34 RID: 2868
			private InputEventTrace.Enumerator m_Enumerator;

			// Token: 0x04000B35 RID: 2869
			private InlinedArray<KeyValuePair<int, int>> m_DeviceIDMappings;

			// Token: 0x04000B36 RID: 2870
			private bool m_CreateNewDevices;

			// Token: 0x04000B37 RID: 2871
			private InlinedArray<InputDevice> m_CreatedDevices;

			// Token: 0x04000B38 RID: 2872
			private Action m_OnFinished;

			// Token: 0x04000B39 RID: 2873
			private Action<InputEventPtr> m_OnEvent;

			// Token: 0x04000B3A RID: 2874
			private double m_StartTimeAsPerFirstEvent;

			// Token: 0x04000B3B RID: 2875
			private double m_StartTimeAsPerRuntime;

			// Token: 0x04000B3C RID: 2876
			private int m_AllEventsByTimeIndex;

			// Token: 0x04000B3D RID: 2877
			private List<InputEventPtr> m_AllEventsByTime;
		}

		// Token: 0x0200020F RID: 527
		[Serializable]
		public struct DeviceInfo
		{
			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06001498 RID: 5272 RVA: 0x0005FB54 File Offset: 0x0005DD54
			// (set) Token: 0x06001499 RID: 5273 RVA: 0x0005FB5C File Offset: 0x0005DD5C
			public int deviceId
			{
				get
				{
					return this.m_DeviceId;
				}
				set
				{
					this.m_DeviceId = value;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x0600149A RID: 5274 RVA: 0x0005FB65 File Offset: 0x0005DD65
			// (set) Token: 0x0600149B RID: 5275 RVA: 0x0005FB6D File Offset: 0x0005DD6D
			public string layout
			{
				get
				{
					return this.m_Layout;
				}
				set
				{
					this.m_Layout = value;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x0600149C RID: 5276 RVA: 0x0005FB76 File Offset: 0x0005DD76
			// (set) Token: 0x0600149D RID: 5277 RVA: 0x0005FB7E File Offset: 0x0005DD7E
			public FourCC stateFormat
			{
				get
				{
					return this.m_StateFormat;
				}
				set
				{
					this.m_StateFormat = value;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x0600149E RID: 5278 RVA: 0x0005FB87 File Offset: 0x0005DD87
			// (set) Token: 0x0600149F RID: 5279 RVA: 0x0005FB8F File Offset: 0x0005DD8F
			public int stateSizeInBytes
			{
				get
				{
					return this.m_StateSizeInBytes;
				}
				set
				{
					this.m_StateSizeInBytes = value;
				}
			}

			// Token: 0x04000B3E RID: 2878
			[SerializeField]
			internal int m_DeviceId;

			// Token: 0x04000B3F RID: 2879
			[SerializeField]
			internal string m_Layout;

			// Token: 0x04000B40 RID: 2880
			[SerializeField]
			internal FourCC m_StateFormat;

			// Token: 0x04000B41 RID: 2881
			[SerializeField]
			internal int m_StateSizeInBytes;

			// Token: 0x04000B42 RID: 2882
			[SerializeField]
			internal string m_FullLayoutJson;
		}
	}
}
