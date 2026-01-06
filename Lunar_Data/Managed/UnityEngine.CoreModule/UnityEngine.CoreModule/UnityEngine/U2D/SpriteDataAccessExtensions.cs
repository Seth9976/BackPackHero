using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.U2D
{
	// Token: 0x02000272 RID: 626
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteDataAccessExtensions
	{
		// Token: 0x06001B29 RID: 6953 RVA: 0x0002B7B0 File Offset: 0x000299B0
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool flag;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				flag = typeof(T) == typeof(Vector3);
				break;
			case VertexAttribute.Tangent:
				flag = typeof(T) == typeof(Vector4);
				break;
			case VertexAttribute.Color:
				flag = typeof(T) == typeof(Color32);
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				flag = typeof(T) == typeof(Vector2);
				break;
			case VertexAttribute.BlendWeight:
				flag = typeof(T) == typeof(BoneWeight);
				break;
			default:
				throw new InvalidOperationException(string.Format("The requested channel '{0}' is unknown.", channel));
			}
			bool flag2 = !flag;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The requested channel '{0}' does not match the return type {1}.", channel, typeof(T).Name));
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo channelInfo = SpriteDataAccessExtensions.GetChannelInfo(sprite, channel);
			byte* ptr = (byte*)channelInfo.buffer + channelInfo.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>((void*)ptr, channelInfo.stride, channelInfo.count);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0002B907 File Offset: 0x00029B07
		public static void SetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel, NativeArray<T> src) where T : struct
		{
			SpriteDataAccessExtensions.CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteDataAccessExtensions.SetChannelData(sprite, channel, src.GetUnsafeReadOnlyPtr<T>());
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0002B920 File Offset: 0x00029B20
		public static NativeArray<Matrix4x4> GetBindPoses(this Sprite sprite)
		{
			SpriteChannelInfo bindPoseInfo = SpriteDataAccessExtensions.GetBindPoseInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(bindPoseInfo.buffer, bindPoseInfo.count, Allocator.Invalid);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0002B94F File Offset: 0x00029B4F
		public static void SetBindPoses(this Sprite sprite, NativeArray<Matrix4x4> src)
		{
			SpriteDataAccessExtensions.SetBindPoseData(sprite, src.GetUnsafeReadOnlyPtr<Matrix4x4>(), src.Length);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0002B968 File Offset: 0x00029B68
		public static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo indicesInfo = SpriteDataAccessExtensions.GetIndicesInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(indicesInfo.buffer, indicesInfo.count, Allocator.Invalid);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0002B997 File Offset: 0x00029B97
		public static void SetIndices(this Sprite sprite, NativeArray<ushort> src)
		{
			SpriteDataAccessExtensions.SetIndicesData(sprite, src.GetUnsafeReadOnlyPtr<ushort>(), src.Length);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public static SpriteBone[] GetBones(this Sprite sprite)
		{
			return SpriteDataAccessExtensions.GetBoneInfo(sprite);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		public static void SetBones(this Sprite sprite, SpriteBone[] src)
		{
			SpriteDataAccessExtensions.SetBoneData(sprite, src);
		}

		// Token: 0x06001B32 RID: 6962
		[NativeName("HasChannel")]
		[MethodImpl(4096)]
		public static extern bool HasVertexAttribute([NotNull("ArgumentNullException")] this Sprite sprite, VertexAttribute channel);

		// Token: 0x06001B33 RID: 6963
		[MethodImpl(4096)]
		public static extern void SetVertexCount([NotNull("ArgumentNullException")] this Sprite sprite, int count);

		// Token: 0x06001B34 RID: 6964
		[MethodImpl(4096)]
		public static extern int GetVertexCount([NotNull("ArgumentNullException")] this Sprite sprite);

		// Token: 0x06001B35 RID: 6965 RVA: 0x0002B9D4 File Offset: 0x00029BD4
		private static SpriteChannelInfo GetBindPoseInfo([NotNull("ArgumentNullException")] Sprite sprite)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetBindPoseInfo_Injected(sprite, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001B36 RID: 6966
		[MethodImpl(4096)]
		private unsafe static extern void SetBindPoseData([NotNull("ArgumentNullException")] Sprite sprite, void* src, int count);

		// Token: 0x06001B37 RID: 6967 RVA: 0x0002B9EC File Offset: 0x00029BEC
		private static SpriteChannelInfo GetIndicesInfo([NotNull("ArgumentNullException")] Sprite sprite)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetIndicesInfo_Injected(sprite, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001B38 RID: 6968
		[MethodImpl(4096)]
		private unsafe static extern void SetIndicesData([NotNull("ArgumentNullException")] Sprite sprite, void* src, int count);

		// Token: 0x06001B39 RID: 6969 RVA: 0x0002BA04 File Offset: 0x00029C04
		private static SpriteChannelInfo GetChannelInfo([NotNull("ArgumentNullException")] Sprite sprite, VertexAttribute channel)
		{
			SpriteChannelInfo spriteChannelInfo;
			SpriteDataAccessExtensions.GetChannelInfo_Injected(sprite, channel, out spriteChannelInfo);
			return spriteChannelInfo;
		}

		// Token: 0x06001B3A RID: 6970
		[MethodImpl(4096)]
		private unsafe static extern void SetChannelData([NotNull("ArgumentNullException")] Sprite sprite, VertexAttribute channel, void* src);

		// Token: 0x06001B3B RID: 6971
		[MethodImpl(4096)]
		private static extern SpriteBone[] GetBoneInfo([NotNull("ArgumentNullException")] Sprite sprite);

		// Token: 0x06001B3C RID: 6972
		[MethodImpl(4096)]
		private static extern void SetBoneData([NotNull("ArgumentNullException")] Sprite sprite, SpriteBone[] src);

		// Token: 0x06001B3D RID: 6973
		[MethodImpl(4096)]
		internal static extern int GetPrimaryVertexStreamSize(Sprite sprite);

		// Token: 0x06001B3E RID: 6974
		[MethodImpl(4096)]
		private static extern void GetBindPoseInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		// Token: 0x06001B3F RID: 6975
		[MethodImpl(4096)]
		private static extern void GetIndicesInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		// Token: 0x06001B40 RID: 6976
		[MethodImpl(4096)]
		private static extern void GetChannelInfo_Injected(Sprite sprite, VertexAttribute channel, out SpriteChannelInfo ret);
	}
}
