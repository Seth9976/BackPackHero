using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002A4 RID: 676
	[NativeHeader("PlatformDependent/Win/Webcam/PhotoCaptureFrame.h")]
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
	public sealed class PhotoCaptureFrame : IDisposable
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0002DE8B File Offset: 0x0002C08B
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x0002DE93 File Offset: 0x0002C093
		public int dataLength { get; private set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x0002DE9C File Offset: 0x0002C09C
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		public bool hasLocationData { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0002DEAD File Offset: 0x0002C0AD
		// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0002DEB5 File Offset: 0x0002C0B5
		public CapturePixelFormat pixelFormat { get; private set; }

		// Token: 0x06001CB6 RID: 7350
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern int GetDataLength();

		// Token: 0x06001CB7 RID: 7351
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern bool GetHasLocationData();

		// Token: 0x06001CB8 RID: 7352
		[ThreadAndSerializationSafe]
		[MethodImpl(4096)]
		private extern CapturePixelFormat GetCapturePixelFormat();

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public bool TryGetCameraToWorldMatrix(out Matrix4x4 cameraToWorldMatrix)
		{
			cameraToWorldMatrix = Matrix4x4.identity;
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				cameraToWorldMatrix = this.GetCameraToWorldMatrix();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0002DEFC File Offset: 0x0002C0FC
		[ThreadAndSerializationSafe]
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		[NativeName("GetCameraToWorld")]
		private Matrix4x4 GetCameraToWorldMatrix()
		{
			Matrix4x4 matrix4x;
			this.GetCameraToWorldMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0002DF14 File Offset: 0x0002C114
		public bool TryGetProjectionMatrix(out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag;
			if (hasLocationData)
			{
				projectionMatrix = this.GetProjection();
				flag = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0002DF50 File Offset: 0x0002C150
		public bool TryGetProjectionMatrix(float nearClipPlane, float farClipPlane, out Matrix4x4 projectionMatrix)
		{
			bool hasLocationData = this.hasLocationData;
			bool flag3;
			if (hasLocationData)
			{
				float num = 0.01f;
				bool flag = nearClipPlane < num;
				if (flag)
				{
					nearClipPlane = num;
				}
				bool flag2 = farClipPlane < nearClipPlane + num;
				if (flag2)
				{
					farClipPlane = nearClipPlane + num;
				}
				projectionMatrix = this.GetProjection();
				float num2 = 1f / (farClipPlane - nearClipPlane);
				float num3 = -(farClipPlane + nearClipPlane) * num2;
				float num4 = -(2f * farClipPlane * nearClipPlane) * num2;
				projectionMatrix.m22 = num3;
				projectionMatrix.m23 = num4;
				flag3 = true;
			}
			else
			{
				projectionMatrix = Matrix4x4.identity;
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0002DFE4 File Offset: 0x0002C1E4
		[ThreadAndSerializationSafe]
		[NativeConditional("PLATFORM_WIN && !PLATFORM_XBOXONE", "Matrix4x4f()")]
		private Matrix4x4 GetProjection()
		{
			Matrix4x4 matrix4x;
			this.GetProjection_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		public void UploadImageDataToTexture(Texture2D targetTexture)
		{
			bool flag = targetTexture == null;
			if (flag)
			{
				throw new ArgumentNullException("targetTexture");
			}
			bool flag2 = this.pixelFormat > CapturePixelFormat.BGRA32;
			if (flag2)
			{
				throw new ArgumentException("Uploading PhotoCaptureFrame to a texture is only supported with BGRA32 CameraFrameFormat!");
			}
			this.UploadImageDataToTexture_Internal(targetTexture);
		}

		// Token: 0x06001CBF RID: 7359
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[NativeName("UploadImageDataToTexture")]
		[MethodImpl(4096)]
		private extern void UploadImageDataToTexture_Internal(Texture2D targetTexture);

		// Token: 0x06001CC0 RID: 7360
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		public extern IntPtr GetUnsafePointerToBuffer();

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0002E044 File Offset: 0x0002C244
		public void CopyRawImageDataIntoBuffer(List<byte> byteBuffer)
		{
			bool flag = byteBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("byteBuffer");
			}
			byte[] array = new byte[this.dataLength];
			this.CopyRawImageDataIntoBuffer_Internal(array);
			bool flag2 = byteBuffer.Capacity < array.Length;
			if (flag2)
			{
				byteBuffer.Capacity = array.Length;
			}
			byteBuffer.Clear();
			byteBuffer.AddRange(array);
		}

		// Token: 0x06001CC2 RID: 7362
		[NativeName("CopyRawImageDataIntoBuffer")]
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		internal extern void CopyRawImageDataIntoBuffer_Internal([Out] byte[] byteArray);

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		internal PhotoCaptureFrame(IntPtr nativePtr)
		{
			this.m_NativePtr = nativePtr;
			this.dataLength = this.GetDataLength();
			this.hasLocationData = this.GetHasLocationData();
			this.pixelFormat = this.GetCapturePixelFormat();
			GC.AddMemoryPressure((long)this.dataLength);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0002E0F4 File Offset: 0x0002C2F4
		private void Cleanup()
		{
			bool flag = this.m_NativePtr != IntPtr.Zero;
			if (flag)
			{
				GC.RemoveMemoryPressure((long)this.dataLength);
				this.Dispose_Internal();
				this.m_NativePtr = IntPtr.Zero;
			}
		}

		// Token: 0x06001CC5 RID: 7365
		[NativeName("Dispose")]
		[ThreadAndSerializationSafe]
		[NativeConditional("(PLATFORM_WIN || PLATFORM_WINRT) && !PLATFORM_XBOXONE")]
		[MethodImpl(4096)]
		private extern void Dispose_Internal();

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0002E137 File Offset: 0x0002C337
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0002E148 File Offset: 0x0002C348
		~PhotoCaptureFrame()
		{
			this.Cleanup();
		}

		// Token: 0x06001CC8 RID: 7368
		[MethodImpl(4096)]
		private extern void GetCameraToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06001CC9 RID: 7369
		[MethodImpl(4096)]
		private extern void GetProjection_Injected(out Matrix4x4 ret);

		// Token: 0x04000960 RID: 2400
		private IntPtr m_NativePtr;
	}
}
