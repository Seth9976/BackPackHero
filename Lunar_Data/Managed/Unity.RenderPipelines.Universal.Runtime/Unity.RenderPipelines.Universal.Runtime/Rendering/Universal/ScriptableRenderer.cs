using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007B RID: 123
	public abstract class ScriptableRenderer : IDisposable
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00018708 File Offset: 0x00016908
		[Obsolete("cameraDepth has been renamed to cameraDepthTarget. (UnityUpgradable) -> cameraDepthTarget")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RenderTargetIdentifier cameraDepth
		{
			get
			{
				return this.m_CameraDepthTarget;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00018710 File Offset: 0x00016910
		internal void ResetNativeRenderPassFrameData()
		{
			if (this.m_MergeableRenderPassesMapArrays == null)
			{
				this.m_MergeableRenderPassesMapArrays = new int[10][];
			}
			for (int i = 0; i < 10; i++)
			{
				if (this.m_MergeableRenderPassesMapArrays[i] == null)
				{
					this.m_MergeableRenderPassesMapArrays[i] = new int[20];
				}
				for (int j = 0; j < 20; j++)
				{
					this.m_MergeableRenderPassesMapArrays[i][j] = -1;
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00018770 File Offset: 0x00016970
		internal void SetupNativeRenderPassFrameData(CameraData cameraData, bool isRenderPassEnabled)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.setupFrameData))
			{
				int num = this.m_ActiveRenderPassQueue.Count - 1;
				this.m_MergeableRenderPassesMap.Clear();
				this.m_RenderPassesAttachmentCount.Clear();
				uint num2 = 0U;
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					ScriptableRenderPass scriptableRenderPass = this.m_ActiveRenderPassQueue[i];
					ScriptableRenderer.RenderPassDescriptor renderPassDescriptor = this.InitializeRenderPassDescriptor(cameraData, scriptableRenderPass);
					scriptableRenderPass.isLastPass = false;
					scriptableRenderPass.renderPassQueueIndex = i;
					if (scriptableRenderPass.useNativeRenderPass && isRenderPassEnabled)
					{
						Hash128 hash = ScriptableRenderer.CreateRenderPassHash(renderPassDescriptor, num2);
						this.m_PassIndexToPassHash[i] = hash;
						if (!this.m_MergeableRenderPassesMap.ContainsKey(hash))
						{
							this.m_MergeableRenderPassesMap.Add(hash, this.m_MergeableRenderPassesMapArrays[this.m_MergeableRenderPassesMap.Count]);
							this.m_RenderPassesAttachmentCount.Add(hash, 0);
						}
						else if (this.m_MergeableRenderPassesMap[hash][ScriptableRenderer.GetValidPassIndexCount(this.m_MergeableRenderPassesMap[hash]) - 1] != i - 1)
						{
							num2 += 1U;
							hash = ScriptableRenderer.CreateRenderPassHash(renderPassDescriptor, num2);
							this.m_PassIndexToPassHash[i] = hash;
							this.m_MergeableRenderPassesMap.Add(hash, this.m_MergeableRenderPassesMapArrays[this.m_MergeableRenderPassesMap.Count]);
							this.m_RenderPassesAttachmentCount.Add(hash, 0);
						}
						this.m_MergeableRenderPassesMap[hash][ScriptableRenderer.GetValidPassIndexCount(this.m_MergeableRenderPassesMap[hash])] = i;
					}
				}
				this.m_ActiveRenderPassQueue[num].isLastPass = true;
				for (int j = 0; j < this.m_ActiveRenderPassQueue.Count; j++)
				{
					this.m_ActiveRenderPassQueue[j].m_ColorAttachmentIndices = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
					this.m_ActiveRenderPassQueue[j].m_InputAttachmentIndices = new NativeArray<int>(8, Allocator.Temp, NativeArrayOptions.ClearMemory);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00018978 File Offset: 0x00016B78
		internal void UpdateFinalStoreActions(int[] currentMergeablePasses, CameraData cameraData)
		{
			for (int i = 0; i < this.m_FinalColorStoreAction.Length; i++)
			{
				this.m_FinalColorStoreAction[i] = RenderBufferStoreAction.Store;
			}
			this.m_FinalDepthStoreAction = RenderBufferStoreAction.Store;
			foreach (int num in currentMergeablePasses)
			{
				if (!ScriptableRenderer.m_UseOptimizedStoreActions || num == -1)
				{
					break;
				}
				ScriptableRenderPass scriptableRenderPass = this.m_ActiveRenderPassQueue[num];
				int num2 = ((scriptableRenderPass.renderTargetSampleCount != -1) ? scriptableRenderPass.renderTargetSampleCount : cameraData.cameraTargetDescriptor.msaaSamples);
				for (int k = 0; k < this.m_FinalColorStoreAction.Length; k++)
				{
					if (this.m_FinalColorStoreAction[k] == RenderBufferStoreAction.Store || this.m_FinalColorStoreAction[k] == RenderBufferStoreAction.StoreAndResolve || scriptableRenderPass.overriddenColorStoreActions[k])
					{
						this.m_FinalColorStoreAction[k] = scriptableRenderPass.colorStoreActions[k];
					}
					if (num2 > 1)
					{
						if (this.m_FinalColorStoreAction[k] == RenderBufferStoreAction.Store)
						{
							this.m_FinalColorStoreAction[k] = RenderBufferStoreAction.StoreAndResolve;
						}
						else if (this.m_FinalColorStoreAction[k] == RenderBufferStoreAction.DontCare)
						{
							this.m_FinalColorStoreAction[k] = RenderBufferStoreAction.Resolve;
						}
					}
				}
				if (this.m_FinalDepthStoreAction == RenderBufferStoreAction.Store || (this.m_FinalDepthStoreAction == RenderBufferStoreAction.StoreAndResolve && scriptableRenderPass.depthStoreAction == RenderBufferStoreAction.Resolve) || scriptableRenderPass.overriddenDepthStoreAction)
				{
					this.m_FinalDepthStoreAction = scriptableRenderPass.depthStoreAction;
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00018AB4 File Offset: 0x00016CB4
		internal void SetNativeRenderPassMRTAttachmentList(ScriptableRenderPass renderPass, ref CameraData cameraData, bool needCustomCameraColorClear, ClearFlag clearFlag)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.setMRTAttachmentsList))
			{
				int renderPassQueueIndex = renderPass.renderPassQueueIndex;
				Hash128 hash = this.m_PassIndexToPassHash[renderPassQueueIndex];
				int[] array = this.m_MergeableRenderPassesMap[hash];
				if (array.First<int>() == renderPassQueueIndex)
				{
					this.m_RenderPassesAttachmentCount[hash] = 0;
					this.UpdateFinalStoreActions(array, cameraData);
					int num = 0;
					bool flag = false;
					foreach (int num2 in array)
					{
						if (num2 == -1)
						{
							break;
						}
						ScriptableRenderPass scriptableRenderPass = this.m_ActiveRenderPassQueue[num2];
						for (int j = 0; j < scriptableRenderPass.m_ColorAttachmentIndices.Length; j++)
						{
							scriptableRenderPass.m_ColorAttachmentIndices[j] = -1;
						}
						for (int k = 0; k < scriptableRenderPass.m_InputAttachmentIndices.Length; k++)
						{
							scriptableRenderPass.m_InputAttachmentIndices[k] = -1;
						}
						uint validColorBufferCount = RenderingUtils.GetValidColorBufferCount(scriptableRenderPass.colorAttachments);
						int num3 = 0;
						while ((long)num3 < (long)((ulong)validColorBufferCount))
						{
							AttachmentDescriptor attachmentDescriptor = new AttachmentDescriptor((scriptableRenderPass.renderTargetFormat[num3] != GraphicsFormat.None) ? scriptableRenderPass.renderTargetFormat[num3] : ScriptableRenderer.GetDefaultGraphicsFormat(cameraData));
							RenderTargetIdentifier renderTargetIdentifier = (scriptableRenderPass.overrideCameraTarget ? scriptableRenderPass.colorAttachments[num3] : this.m_CameraColorTarget);
							int num4 = ScriptableRenderer.FindAttachmentDescriptorIndexInList(renderTargetIdentifier, this.m_ActiveColorAttachmentDescriptors);
							if (ScriptableRenderer.m_UseOptimizedStoreActions)
							{
								attachmentDescriptor.storeAction = this.m_FinalColorStoreAction[num3];
							}
							if (num4 == -1)
							{
								this.m_ActiveColorAttachmentDescriptors[num] = attachmentDescriptor;
								this.m_ActiveColorAttachmentDescriptors[num].ConfigureTarget(renderTargetIdentifier, (scriptableRenderPass.clearFlag & ClearFlag.Color) == ClearFlag.None, true);
								if (scriptableRenderPass.colorAttachments[num3] == this.m_CameraColorTarget && needCustomCameraColorClear && (clearFlag & ClearFlag.Color) != ClearFlag.None)
								{
									this.m_ActiveColorAttachmentDescriptors[num].ConfigureClear(CoreUtils.ConvertSRGBToActiveColorSpace(cameraData.camera.backgroundColor), 1f, 0U);
								}
								else if ((scriptableRenderPass.clearFlag & ClearFlag.Color) != ClearFlag.None)
								{
									this.m_ActiveColorAttachmentDescriptors[num].ConfigureClear(CoreUtils.ConvertSRGBToActiveColorSpace(scriptableRenderPass.clearColor), 1f, 0U);
								}
								scriptableRenderPass.m_ColorAttachmentIndices[num3] = num;
								num++;
								Dictionary<Hash128, int> renderPassesAttachmentCount = this.m_RenderPassesAttachmentCount;
								Hash128 hash2 = hash;
								int num5 = renderPassesAttachmentCount[hash2];
								renderPassesAttachmentCount[hash2] = num5 + 1;
							}
							else
							{
								scriptableRenderPass.m_ColorAttachmentIndices[num3] = num4;
							}
							num3++;
						}
						if (ScriptableRenderer.PassHasInputAttachments(scriptableRenderPass))
						{
							flag = true;
							this.SetupInputAttachmentIndices(scriptableRenderPass);
						}
						this.m_ActiveDepthAttachmentDescriptor = new AttachmentDescriptor(SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil));
						this.m_ActiveDepthAttachmentDescriptor.ConfigureTarget(scriptableRenderPass.overrideCameraTarget ? scriptableRenderPass.depthAttachment : this.m_CameraDepthTarget, (clearFlag & ClearFlag.DepthStencil) == ClearFlag.None, true);
						if ((clearFlag & ClearFlag.DepthStencil) != ClearFlag.None)
						{
							this.m_ActiveDepthAttachmentDescriptor.ConfigureClear(Color.black, 1f, 0U);
						}
						if (ScriptableRenderer.m_UseOptimizedStoreActions)
						{
							this.m_ActiveDepthAttachmentDescriptor.storeAction = this.m_FinalDepthStoreAction;
						}
					}
					if (flag)
					{
						this.SetupTransientInputAttachments(this.m_RenderPassesAttachmentCount[hash]);
					}
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00018E04 File Offset: 0x00017004
		private bool IsDepthOnlyRenderTexture(RenderTexture t)
		{
			return t.graphicsFormat == GraphicsFormat.None || t.graphicsFormat == GraphicsFormat.DepthAuto || t.graphicsFormat == GraphicsFormat.ShadowAuto;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00018E2C File Offset: 0x0001702C
		internal void SetNativeRenderPassAttachmentList(ScriptableRenderPass renderPass, ref CameraData cameraData, RenderTargetIdentifier passColorAttachment, RenderTargetIdentifier passDepthAttachment, ClearFlag finalClearFlag, Color finalClearColor)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.setAttachmentList))
			{
				int renderPassQueueIndex = renderPass.renderPassQueueIndex;
				Hash128 hash = this.m_PassIndexToPassHash[renderPassQueueIndex];
				int[] array = this.m_MergeableRenderPassesMap[hash];
				if (array.First<int>() == renderPassQueueIndex)
				{
					this.m_RenderPassesAttachmentCount[hash] = 0;
					this.UpdateFinalStoreActions(array, cameraData);
					int num = 0;
					foreach (int num2 in array)
					{
						if (num2 == -1)
						{
							break;
						}
						ScriptableRenderPass scriptableRenderPass = this.m_ActiveRenderPassQueue[num2];
						for (int j = 0; j < scriptableRenderPass.m_ColorAttachmentIndices.Length; j++)
						{
							scriptableRenderPass.m_ColorAttachmentIndices[j] = -1;
						}
						bool flag = cameraData.targetTexture != null;
						bool flag2 = renderPass.depthOnly || (flag && this.IsDepthOnlyRenderTexture(cameraData.targetTexture));
						AttachmentDescriptor attachmentDescriptor;
						if (flag2 && flag)
						{
							if (this.IsDepthOnlyRenderTexture(cameraData.targetTexture) && !scriptableRenderPass.overrideCameraTarget)
							{
								passColorAttachment = new RenderTargetIdentifier(cameraData.targetTexture);
							}
							else
							{
								passColorAttachment = renderPass.colorAttachment;
							}
							attachmentDescriptor = new AttachmentDescriptor(SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil));
						}
						else
						{
							attachmentDescriptor = new AttachmentDescriptor(cameraData.cameraTargetDescriptor.graphicsFormat);
						}
						if (scriptableRenderPass.overrideCameraTarget)
						{
							attachmentDescriptor = new AttachmentDescriptor((scriptableRenderPass.renderTargetFormat[0] != GraphicsFormat.None) ? scriptableRenderPass.renderTargetFormat[0] : ScriptableRenderer.GetDefaultGraphicsFormat(cameraData));
						}
						int num3 = ((scriptableRenderPass.renderTargetSampleCount != -1) ? scriptableRenderPass.renderTargetSampleCount : cameraData.cameraTargetDescriptor.msaaSamples);
						RenderTargetIdentifier renderTargetIdentifier = ((flag2 || passColorAttachment != BuiltinRenderTextureType.CameraTarget) ? passColorAttachment : (flag ? new RenderTargetIdentifier(cameraData.targetTexture.colorBuffer, 0, CubemapFace.Unknown, 0) : BuiltinRenderTextureType.CameraTarget));
						RenderTargetIdentifier renderTargetIdentifier2 = ((passDepthAttachment != BuiltinRenderTextureType.CameraTarget) ? passDepthAttachment : (flag ? new RenderTargetIdentifier(cameraData.targetTexture.depthBuffer, 0, CubemapFace.Unknown, 0) : BuiltinRenderTextureType.Depth));
						attachmentDescriptor.ConfigureTarget(renderTargetIdentifier, (finalClearFlag & ClearFlag.Color) == ClearFlag.None, true);
						if (ScriptableRenderer.PassHasInputAttachments(scriptableRenderPass))
						{
							this.SetupInputAttachmentIndices(scriptableRenderPass);
						}
						this.m_ActiveDepthAttachmentDescriptor = new AttachmentDescriptor(SystemInfo.GetGraphicsFormat(DefaultFormat.DepthStencil));
						this.m_ActiveDepthAttachmentDescriptor.ConfigureTarget(renderTargetIdentifier2, (finalClearFlag & ClearFlag.Depth) == ClearFlag.None, true);
						if (finalClearFlag != ClearFlag.None)
						{
							if (cameraData.renderType != CameraRenderType.Overlay || (flag2 && (finalClearFlag & ClearFlag.Color) != ClearFlag.None))
							{
								attachmentDescriptor.ConfigureClear(finalClearColor, 1f, 0U);
							}
							if ((finalClearFlag & ClearFlag.Depth) != ClearFlag.None)
							{
								this.m_ActiveDepthAttachmentDescriptor.ConfigureClear(Color.black, 1f, 0U);
							}
						}
						if (num3 > 1)
						{
							attachmentDescriptor.ConfigureResolveTarget(renderTargetIdentifier);
						}
						if (ScriptableRenderer.m_UseOptimizedStoreActions)
						{
							attachmentDescriptor.storeAction = this.m_FinalColorStoreAction[0];
							this.m_ActiveDepthAttachmentDescriptor.storeAction = this.m_FinalDepthStoreAction;
						}
						int num4 = ScriptableRenderer.FindAttachmentDescriptorIndexInList(num, attachmentDescriptor, this.m_ActiveColorAttachmentDescriptors);
						if (num4 == -1)
						{
							scriptableRenderPass.m_ColorAttachmentIndices[0] = num;
							this.m_ActiveColorAttachmentDescriptors[num] = attachmentDescriptor;
							num++;
							Dictionary<Hash128, int> renderPassesAttachmentCount = this.m_RenderPassesAttachmentCount;
							Hash128 hash2 = hash;
							int num5 = renderPassesAttachmentCount[hash2];
							renderPassesAttachmentCount[hash2] = num5 + 1;
						}
						else
						{
							scriptableRenderPass.m_ColorAttachmentIndices[0] = num4;
						}
					}
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00019188 File Offset: 0x00017388
		internal void ConfigureNativeRenderPass(CommandBuffer cmd, ScriptableRenderPass renderPass, CameraData cameraData)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.configure))
			{
				int renderPassQueueIndex = renderPass.renderPassQueueIndex;
				Hash128 hash = this.m_PassIndexToPassHash[renderPassQueueIndex];
				int[] array = this.m_MergeableRenderPassesMap[hash];
				if (array.First<int>() == renderPassQueueIndex)
				{
					foreach (int num in array)
					{
						if (num == -1)
						{
							break;
						}
						this.m_ActiveRenderPassQueue[num].Configure(cmd, cameraData.cameraTargetDescriptor);
					}
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001922C File Offset: 0x0001742C
		internal void ExecuteNativeRenderPass(ScriptableRenderContext context, ScriptableRenderPass renderPass, CameraData cameraData, ref RenderingData renderingData)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.execute))
			{
				int renderPassQueueIndex = renderPass.renderPassQueueIndex;
				Hash128 hash = this.m_PassIndexToPassHash[renderPassQueueIndex];
				int[] array = this.m_MergeableRenderPassesMap[hash];
				int num = this.m_RenderPassesAttachmentCount[hash];
				bool isLastPass = renderPass.isLastPass;
				bool flag = isLastPass && this.m_ActiveColorAttachmentDescriptors[0].loadStoreTarget == BuiltinRenderTextureType.CameraTarget;
				bool flag2 = renderPass.depthOnly || (cameraData.targetTexture != null && this.IsDepthOnlyRenderTexture(cameraData.targetTexture));
				bool flag3 = flag2 || ((!renderPass.overrideCameraTarget || (renderPass.overrideCameraTarget && renderPass.depthAttachment != BuiltinRenderTextureType.CameraTarget)) && !flag && (!isLastPass || !(cameraData.camera.targetTexture != null)));
				NativeArray<AttachmentDescriptor> nativeArray = new NativeArray<AttachmentDescriptor>((flag3 && !flag2) ? (num + 1) : 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < num; i++)
				{
					nativeArray[i] = this.m_ActiveColorAttachmentDescriptors[i];
				}
				if (flag3 && !flag2)
				{
					nativeArray[num] = this.m_ActiveDepthAttachmentDescriptor;
				}
				ScriptableRenderer.RenderPassDescriptor renderPassDescriptor = this.InitializeRenderPassDescriptor(cameraData, renderPass);
				int validPassIndexCount = ScriptableRenderer.GetValidPassIndexCount(array);
				uint subPassAttachmentIndicesCount = ScriptableRenderer.GetSubPassAttachmentIndicesCount(renderPass);
				NativeArray<int> nativeArray2 = new NativeArray<int>((int)((!flag2) ? subPassAttachmentIndicesCount : 0U), Allocator.Temp, NativeArrayOptions.ClearMemory);
				if (!flag2)
				{
					int num2 = 0;
					while ((long)num2 < (long)((ulong)subPassAttachmentIndicesCount))
					{
						nativeArray2[num2] = renderPass.m_ColorAttachmentIndices[num2];
						num2++;
					}
				}
				if (validPassIndexCount == 1 || array[0] == renderPassQueueIndex)
				{
					if (ScriptableRenderer.PassHasInputAttachments(renderPass))
					{
						Debug.LogWarning("First pass in a RenderPass should not have input attachments.");
					}
					context.BeginRenderPass(renderPassDescriptor.w, renderPassDescriptor.h, Math.Max(renderPassDescriptor.samples, 1), nativeArray, flag3 ? ((!flag2) ? num : 0) : (-1));
					nativeArray.Dispose();
					context.BeginSubPass(nativeArray2, false);
					this.m_LastBeginSubpassPassIndex = renderPassQueueIndex;
				}
				else if (!ScriptableRenderer.AreAttachmentIndicesCompatible(this.m_ActiveRenderPassQueue[this.m_LastBeginSubpassPassIndex], this.m_ActiveRenderPassQueue[renderPassQueueIndex]))
				{
					context.EndSubPass();
					if (ScriptableRenderer.PassHasInputAttachments(this.m_ActiveRenderPassQueue[renderPassQueueIndex]))
					{
						context.BeginSubPass(nativeArray2, this.m_ActiveRenderPassQueue[renderPassQueueIndex].m_InputAttachmentIndices, false);
					}
					else
					{
						context.BeginSubPass(nativeArray2, false);
					}
					this.m_LastBeginSubpassPassIndex = renderPassQueueIndex;
				}
				else if (ScriptableRenderer.PassHasInputAttachments(this.m_ActiveRenderPassQueue[renderPassQueueIndex]))
				{
					context.EndSubPass();
					context.BeginSubPass(nativeArray2, this.m_ActiveRenderPassQueue[renderPassQueueIndex].m_InputAttachmentIndices, false);
					this.m_LastBeginSubpassPassIndex = renderPassQueueIndex;
				}
				nativeArray2.Dispose();
				renderPass.Execute(context, ref renderingData);
				if (validPassIndexCount == 1 || array[validPassIndexCount - 1] == renderPassQueueIndex)
				{
					context.EndSubPass();
					context.EndRenderPass();
					this.m_LastBeginSubpassPassIndex = 0;
				}
				for (int j = 0; j < this.m_ActiveColorAttachmentDescriptors.Length; j++)
				{
					this.m_ActiveColorAttachmentDescriptors[j] = RenderingUtils.emptyAttachment;
					this.m_IsActiveColorAttachmentTransient[j] = false;
				}
				this.m_ActiveDepthAttachmentDescriptor = RenderingUtils.emptyAttachment;
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00019584 File Offset: 0x00017784
		internal void SetupInputAttachmentIndices(ScriptableRenderPass pass)
		{
			int validInputAttachmentCount = ScriptableRenderer.GetValidInputAttachmentCount(pass);
			pass.m_InputAttachmentIndices = new NativeArray<int>(validInputAttachmentCount, Allocator.Temp, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < validInputAttachmentCount; i++)
			{
				pass.m_InputAttachmentIndices[i] = ScriptableRenderer.FindAttachmentDescriptorIndexInList(pass.m_InputAttachments[i], this.m_ActiveColorAttachmentDescriptors);
				if (pass.m_InputAttachmentIndices[i] == -1)
				{
					Debug.LogWarning("RenderPass Input attachment not found in the current RenderPass");
				}
				else if (!this.m_IsActiveColorAttachmentTransient[pass.m_InputAttachmentIndices[i]])
				{
					this.m_IsActiveColorAttachmentTransient[pass.m_InputAttachmentIndices[i]] = pass.IsInputAttachmentTransient(i);
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00019620 File Offset: 0x00017820
		internal void SetupTransientInputAttachments(int attachmentCount)
		{
			for (int i = 0; i < attachmentCount; i++)
			{
				if (this.m_IsActiveColorAttachmentTransient[i])
				{
					this.m_ActiveColorAttachmentDescriptors[i].loadAction = RenderBufferLoadAction.DontCare;
					this.m_ActiveColorAttachmentDescriptors[i].storeAction = RenderBufferStoreAction.DontCare;
					this.m_ActiveColorAttachmentDescriptors[i].loadStoreTarget = BuiltinRenderTextureType.None;
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00019680 File Offset: 0x00017880
		internal static uint GetSubPassAttachmentIndicesCount(ScriptableRenderPass pass)
		{
			uint num = 0U;
			using (NativeArray<int>.Enumerator enumerator = pass.m_ColorAttachmentIndices.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current >= 0)
					{
						num += 1U;
					}
				}
			}
			return num;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000196D8 File Offset: 0x000178D8
		internal static bool AreAttachmentIndicesCompatible(ScriptableRenderPass lastSubPass, ScriptableRenderPass currentSubPass)
		{
			uint subPassAttachmentIndicesCount = ScriptableRenderer.GetSubPassAttachmentIndicesCount(lastSubPass);
			uint subPassAttachmentIndicesCount2 = ScriptableRenderer.GetSubPassAttachmentIndicesCount(currentSubPass);
			if (subPassAttachmentIndicesCount2 > subPassAttachmentIndicesCount)
			{
				return false;
			}
			uint num = 0U;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)subPassAttachmentIndicesCount2))
			{
				int num3 = 0;
				while ((long)num3 < (long)((ulong)subPassAttachmentIndicesCount))
				{
					if (currentSubPass.m_ColorAttachmentIndices[num2] == lastSubPass.m_ColorAttachmentIndices[num3])
					{
						num += 1U;
					}
					num3++;
				}
				num2++;
			}
			return num == subPassAttachmentIndicesCount2;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00019740 File Offset: 0x00017940
		internal static uint GetValidColorAttachmentCount(AttachmentDescriptor[] colorAttachments)
		{
			uint num = 0U;
			if (colorAttachments != null)
			{
				for (int i = 0; i < colorAttachments.Length; i++)
				{
					if (colorAttachments[i] != RenderingUtils.emptyAttachment)
					{
						num += 1U;
					}
				}
			}
			return num;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001977C File Offset: 0x0001797C
		internal static int GetValidInputAttachmentCount(ScriptableRenderPass renderPass)
		{
			int num = renderPass.m_InputAttachments.Length;
			if (num != 8)
			{
				return num;
			}
			for (int i = 0; i < num; i++)
			{
				if (renderPass.m_InputAttachments[i] == -1)
				{
					return i;
				}
			}
			return num;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000197C0 File Offset: 0x000179C0
		internal static int FindAttachmentDescriptorIndexInList(int attachmentIdx, AttachmentDescriptor attachmentDescriptor, AttachmentDescriptor[] attachmentDescriptors)
		{
			int num = -1;
			for (int i = 0; i <= attachmentIdx; i++)
			{
				AttachmentDescriptor attachmentDescriptor2 = attachmentDescriptors[i];
				if (attachmentDescriptor2.loadStoreTarget == attachmentDescriptor.loadStoreTarget && attachmentDescriptor2.graphicsFormat == attachmentDescriptor.graphicsFormat)
				{
					num = i;
					break;
				}
			}
			return num;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00019810 File Offset: 0x00017A10
		internal static int FindAttachmentDescriptorIndexInList(RenderTargetIdentifier target, AttachmentDescriptor[] attachmentDescriptors)
		{
			for (int i = 0; i < attachmentDescriptors.Length; i++)
			{
				AttachmentDescriptor attachmentDescriptor = attachmentDescriptors[i];
				if (attachmentDescriptor.loadStoreTarget == target)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00019848 File Offset: 0x00017A48
		internal static int GetValidPassIndexCount(int[] array)
		{
			if (array == null)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == -1)
				{
					return i;
				}
			}
			return array.Length - 1;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00019875 File Offset: 0x00017A75
		internal static bool PassHasInputAttachments(ScriptableRenderPass renderPass)
		{
			return renderPass.m_InputAttachments.Length != 8 || renderPass.m_InputAttachments[0] != -1;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001989B File Offset: 0x00017A9B
		internal static Hash128 CreateRenderPassHash(int width, int height, int depthID, int sample, uint hashIndex)
		{
			return new Hash128((uint)((width << 4) + height), (uint)depthID, (uint)sample, hashIndex);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000198AB File Offset: 0x00017AAB
		internal static Hash128 CreateRenderPassHash(ScriptableRenderer.RenderPassDescriptor desc, uint hashIndex)
		{
			return ScriptableRenderer.CreateRenderPassHash(desc.w, desc.h, desc.depthID, desc.samples, hashIndex);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000198CC File Offset: 0x00017ACC
		private ScriptableRenderer.RenderPassDescriptor InitializeRenderPassDescriptor(CameraData cameraData, ScriptableRenderPass renderPass)
		{
			int num = ((renderPass.renderTargetWidth != -1) ? renderPass.renderTargetWidth : cameraData.cameraTargetDescriptor.width);
			int num2 = ((renderPass.renderTargetHeight != -1) ? renderPass.renderTargetHeight : cameraData.cameraTargetDescriptor.height);
			int num3 = ((renderPass.renderTargetSampleCount != -1) ? renderPass.renderTargetSampleCount : cameraData.cameraTargetDescriptor.msaaSamples);
			RenderTargetIdentifier renderTargetIdentifier = (renderPass.overrideCameraTarget ? renderPass.depthAttachment : this.m_CameraDepthTarget);
			int num4 = (renderPass.depthOnly ? renderPass.colorAttachment.GetHashCode() : renderTargetIdentifier.GetHashCode());
			return new ScriptableRenderer.RenderPassDescriptor(num, num2, num3, num4);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00019980 File Offset: 0x00017B80
		private static GraphicsFormat GetDefaultGraphicsFormat(CameraData cameraData)
		{
			if (cameraData.isHdrEnabled)
			{
				GraphicsFormat graphicsFormat;
				if (!Graphics.preserveFramebufferAlpha && RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.B10G11R11_UFloatPack32, FormatUsage.Blend))
				{
					graphicsFormat = GraphicsFormat.B10G11R11_UFloatPack32;
				}
				else if (RenderingUtils.SupportsGraphicsFormat(GraphicsFormat.R16G16B16A16_SFloat, FormatUsage.Blend))
				{
					graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;
				}
				else
				{
					graphicsFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.HDR);
				}
				return graphicsFormat;
			}
			return SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000199CB File Offset: 0x00017BCB
		public virtual int SupportedCameraStackingTypes()
		{
			return 0;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000199CE File Offset: 0x00017BCE
		public bool SupportsCameraStackingType(CameraRenderType cameraRenderType)
		{
			return (this.SupportedCameraStackingTypes() & (1 << (int)cameraRenderType)) != 0;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000199E0 File Offset: 0x00017BE0
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000199E8 File Offset: 0x00017BE8
		protected ProfilingSampler profilingExecute { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000199F1 File Offset: 0x00017BF1
		internal DebugHandler DebugHandler { get; }

		// Token: 0x0600046C RID: 1132 RVA: 0x000199FC File Offset: 0x00017BFC
		public static void SetCameraMatrices(CommandBuffer cmd, ref CameraData cameraData, bool setInverseMatrices)
		{
			if (cameraData.xr.enabled)
			{
				cameraData.xr.UpdateGPUViewAndProjectionMatrices(cmd, ref cameraData, cameraData.xr.renderTargetIsRenderTexture);
				return;
			}
			Matrix4x4 viewMatrix = cameraData.GetViewMatrix(0);
			Matrix4x4 projectionMatrix = cameraData.GetProjectionMatrix(0);
			cmd.SetViewProjectionMatrices(viewMatrix, projectionMatrix);
			if (setInverseMatrices)
			{
				Matrix4x4 gpuprojectionMatrix = cameraData.GetGPUProjectionMatrix(0);
				gpuprojectionMatrix * viewMatrix;
				Matrix4x4 matrix4x = Matrix4x4.Inverse(viewMatrix);
				Matrix4x4 matrix4x2 = Matrix4x4.Inverse(gpuprojectionMatrix);
				Matrix4x4 matrix4x3 = matrix4x * matrix4x2;
				Matrix4x4 matrix4x4 = Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * viewMatrix;
				Matrix4x4 inverse = matrix4x4.inverse;
				cmd.SetGlobalMatrix(ShaderPropertyId.worldToCameraMatrix, matrix4x4);
				cmd.SetGlobalMatrix(ShaderPropertyId.cameraToWorldMatrix, inverse);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewMatrix, matrix4x);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseProjectionMatrix, matrix4x2);
				cmd.SetGlobalMatrix(ShaderPropertyId.inverseViewAndProjectionMatrix, matrix4x3);
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00019ADC File Offset: 0x00017CDC
		private void SetPerCameraShaderVariables(CommandBuffer cmd, ref CameraData cameraData)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.setPerCameraShaderVariables))
			{
				Camera camera = cameraData.camera;
				Rect pixelRect = cameraData.pixelRect;
				float num = (cameraData.isSceneViewCamera ? 1f : cameraData.renderScale);
				float num2 = pixelRect.width * num;
				float num3 = pixelRect.height * num;
				float num4 = pixelRect.width;
				float num5 = pixelRect.height;
				if (cameraData.xr.enabled)
				{
					num2 = (float)cameraData.cameraTargetDescriptor.width;
					num3 = (float)cameraData.cameraTargetDescriptor.height;
					num4 = (float)cameraData.cameraTargetDescriptor.width;
					num5 = (float)cameraData.cameraTargetDescriptor.height;
					this.useRenderPassEnabled = false;
				}
				if (camera.allowDynamicResolution)
				{
					num2 *= ScalableBufferManager.widthScaleFactor;
					num3 *= ScalableBufferManager.heightScaleFactor;
				}
				float nearClipPlane = camera.nearClipPlane;
				float farClipPlane = camera.farClipPlane;
				float num6 = (Mathf.Approximately(nearClipPlane, 0f) ? 0f : (1f / nearClipPlane));
				float num7 = (Mathf.Approximately(farClipPlane, 0f) ? 0f : (1f / farClipPlane));
				float num8 = (camera.orthographic ? 1f : 0f);
				float num9 = 1f - farClipPlane * num6;
				float num10 = farClipPlane * num6;
				Vector4 vector = new Vector4(num9, num10, num9 * num7, num10 * num7);
				if (SystemInfo.usesReversedZBuffer)
				{
					vector.y += vector.x;
					vector.x = -vector.x;
					vector.w += vector.z;
					vector.z = -vector.z;
				}
				float num11 = (cameraData.IsCameraProjectionMatrixFlipped() ? (-1f) : 1f);
				Vector4 vector2 = new Vector4(num11, nearClipPlane, farClipPlane, 1f * num7);
				cmd.SetGlobalVector(ShaderPropertyId.projectionParams, vector2);
				Vector4 vector3 = new Vector4(camera.orthographicSize * cameraData.aspectRatio, camera.orthographicSize, 0f, num8);
				cmd.SetGlobalVector(ShaderPropertyId.worldSpaceCameraPos, cameraData.worldSpaceCameraPos);
				cmd.SetGlobalVector(ShaderPropertyId.screenParams, new Vector4(num4, num5, 1f + 1f / num4, 1f + 1f / num5));
				cmd.SetGlobalVector(ShaderPropertyId.scaledScreenParams, new Vector4(num2, num3, 1f + 1f / num2, 1f + 1f / num3));
				cmd.SetGlobalVector(ShaderPropertyId.zBufferParams, vector);
				cmd.SetGlobalVector(ShaderPropertyId.orthoParams, vector3);
				cmd.SetGlobalVector(ShaderPropertyId.screenSize, new Vector4(num2, num3, 1f / num2, 1f / num3));
				float num12 = Math.Min((float)(-(float)Math.Log((double)(num4 / num2), 2.0)), 0f);
				cmd.SetGlobalVector(ShaderPropertyId.globalMipBias, new Vector2(num12, Mathf.Pow(2f, num12)));
				ScriptableRenderer.SetCameraMatrices(cmd, ref cameraData, true);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00019E0C File Offset: 0x0001800C
		private void SetPerCameraBillboardProperties(CommandBuffer cmd, ref CameraData cameraData)
		{
			Matrix4x4 viewMatrix = cameraData.GetViewMatrix(0);
			Vector3 worldSpaceCameraPos = cameraData.worldSpaceCameraPos;
			CoreUtils.SetKeyword(cmd, "BILLBOARD_FACE_CAMERA_POS", QualitySettings.billboardsFaceCameraPosition);
			Vector3 vector;
			Vector3 vector2;
			float num;
			ScriptableRenderer.CalculateBillboardProperties(in viewMatrix, out vector, out vector2, out num);
			cmd.SetGlobalVector(ShaderPropertyId.billboardNormal, new Vector4(vector2.x, vector2.y, vector2.z, 0f));
			cmd.SetGlobalVector(ShaderPropertyId.billboardTangent, new Vector4(vector.x, vector.y, vector.z, 0f));
			cmd.SetGlobalVector(ShaderPropertyId.billboardCameraParams, new Vector4(worldSpaceCameraPos.x, worldSpaceCameraPos.y, worldSpaceCameraPos.z, num));
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00019EB8 File Offset: 0x000180B8
		private static void CalculateBillboardProperties(in Matrix4x4 worldToCameraMatrix, out Vector3 billboardTangent, out Vector3 billboardNormal, out float cameraXZAngle)
		{
			Matrix4x4 matrix4x = worldToCameraMatrix;
			matrix4x = matrix4x.transpose;
			Vector3 vector = new Vector3(matrix4x.m00, matrix4x.m10, matrix4x.m20);
			Vector3 vector2 = new Vector3(matrix4x.m01, matrix4x.m11, matrix4x.m21);
			Vector3 vector3 = new Vector3(matrix4x.m02, matrix4x.m12, matrix4x.m22);
			Vector3 up = Vector3.up;
			Vector3 vector4 = Vector3.Cross(vector3, up);
			billboardTangent = ((!Mathf.Approximately(vector4.sqrMagnitude, 0f)) ? vector4.normalized : vector);
			billboardNormal = Vector3.Cross(up, billboardTangent);
			billboardNormal = ((!Mathf.Approximately(billboardNormal.sqrMagnitude, 0f)) ? billboardNormal.normalized : vector2);
			Vector3 vector5 = new Vector3(0f, 0f, 1f);
			float num = vector5.x * billboardTangent.z - vector5.z * billboardTangent.x;
			float num2 = vector5.x * billboardTangent.x + vector5.z * billboardTangent.z;
			cameraXZAngle = Mathf.Atan2(num, num2);
			if (cameraXZAngle < 0f)
			{
				cameraXZAngle += 6.2831855f;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00019FF4 File Offset: 0x000181F4
		private void SetPerCameraClippingPlaneProperties(CommandBuffer cmd, in CameraData cameraData)
		{
			CameraData cameraData2 = cameraData;
			Matrix4x4 gpuprojectionMatrix = cameraData2.GetGPUProjectionMatrix(0);
			cameraData2 = cameraData;
			Matrix4x4 viewMatrix = cameraData2.GetViewMatrix(0);
			Matrix4x4 matrix4x = CoreMatrixUtils.MultiplyProjectionMatrix(gpuprojectionMatrix, viewMatrix, cameraData.camera.orthographic);
			Plane[] array = ScriptableRenderer.s_Planes;
			GeometryUtility.CalculateFrustumPlanes(matrix4x, array);
			Vector4[] array2 = ScriptableRenderer.s_VectorPlanes;
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new Vector4(array[i].normal.x, array[i].normal.y, array[i].normal.z, array[i].distance);
			}
			cmd.SetGlobalVectorArray(ShaderPropertyId.cameraWorldClipPlanes, array2);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001A0B4 File Offset: 0x000182B4
		private void SetShaderTimeValues(CommandBuffer cmd, float time, float deltaTime, float smoothDeltaTime)
		{
			float num = time / 8f;
			float num2 = time / 4f;
			float num3 = time / 2f;
			Vector4 vector = time * new Vector4(0.05f, 1f, 2f, 3f);
			Vector4 vector2 = new Vector4(Mathf.Sin(num), Mathf.Sin(num2), Mathf.Sin(num3), Mathf.Sin(time));
			Vector4 vector3 = new Vector4(Mathf.Cos(num), Mathf.Cos(num2), Mathf.Cos(num3), Mathf.Cos(time));
			Vector4 vector4 = new Vector4(deltaTime, 1f / deltaTime, smoothDeltaTime, 1f / smoothDeltaTime);
			Vector4 vector5 = new Vector4(time, Mathf.Sin(time), Mathf.Cos(time), 0f);
			cmd.SetGlobalVector(ShaderPropertyId.time, vector);
			cmd.SetGlobalVector(ShaderPropertyId.sinTime, vector2);
			cmd.SetGlobalVector(ShaderPropertyId.cosTime, vector3);
			cmd.SetGlobalVector(ShaderPropertyId.deltaTime, vector4);
			cmd.SetGlobalVector(ShaderPropertyId.timeParameters, vector5);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001A1A9 File Offset: 0x000183A9
		public RenderTargetIdentifier cameraColorTarget
		{
			get
			{
				if (!this.m_IsPipelineExecuting && !this.isCameraColorTargetValid)
				{
					Debug.LogWarning("You can only call cameraColorTarget inside the scope of a ScriptableRenderPass. Otherwise the pipeline camera target texture might have not been created or might have already been disposed.");
				}
				return this.m_CameraColorTarget;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001A1CB File Offset: 0x000183CB
		internal virtual RenderTargetIdentifier GetCameraColorFrontBuffer(CommandBuffer cmd)
		{
			return 0;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001A1D3 File Offset: 0x000183D3
		public RenderTargetIdentifier cameraDepthTarget
		{
			get
			{
				if (!this.m_IsPipelineExecuting)
				{
					Debug.LogWarning("You can only call cameraDepthTarget inside the scope of a ScriptableRenderPass. Otherwise the pipeline camera target texture might have not been created or might have already been disposed.");
				}
				return this.m_CameraDepthTarget;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001A1ED File Offset: 0x000183ED
		protected List<ScriptableRendererFeature> rendererFeatures
		{
			get
			{
				return this.m_RendererFeatures;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001A1F5 File Offset: 0x000183F5
		protected List<ScriptableRenderPass> activeRenderPassQueue
		{
			get
			{
				return this.m_ActiveRenderPassQueue;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001A1FD File Offset: 0x000183FD
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001A205 File Offset: 0x00018405
		public ScriptableRenderer.RenderingFeatures supportedRenderingFeatures { get; set; } = new ScriptableRenderer.RenderingFeatures();

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001A20E File Offset: 0x0001840E
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001A216 File Offset: 0x00018416
		public GraphicsDeviceType[] unsupportedGraphicsDeviceTypes { get; set; } = new GraphicsDeviceType[0];

		// Token: 0x0600047B RID: 1147 RVA: 0x0001A220 File Offset: 0x00018420
		internal static void ConfigureActiveTarget(RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = colorAttachment;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = 0;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001A262 File Offset: 0x00018462
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001A26A File Offset: 0x0001846A
		internal bool useDepthPriming { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001A273 File Offset: 0x00018473
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001A27B File Offset: 0x0001847B
		internal bool stripShadowsOffVariants { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001A284 File Offset: 0x00018484
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001A28C File Offset: 0x0001848C
		internal bool stripAdditionalLightOffVariants { get; set; }

		// Token: 0x06000482 RID: 1154 RVA: 0x0001A298 File Offset: 0x00018498
		public ScriptableRenderer(ScriptableRendererData data)
		{
			this.profilingExecute = new ProfilingSampler("ScriptableRenderer.Execute: " + data.name);
			foreach (ScriptableRendererFeature scriptableRendererFeature in data.rendererFeatures)
			{
				if (!(scriptableRendererFeature == null))
				{
					scriptableRendererFeature.Create();
					this.m_RendererFeatures.Add(scriptableRendererFeature);
				}
			}
			this.ResetNativeRenderPassFrameData();
			this.useRenderPassEnabled = data.useNativeRenderPass && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
			this.Clear(CameraRenderType.Base);
			this.m_ActiveRenderPassQueue.Clear();
			if (UniversalRenderPipeline.asset)
			{
				this.m_StoreActionsOptimizationSetting = UniversalRenderPipeline.asset.storeActionsOptimization;
			}
			ScriptableRenderer.m_UseOptimizedStoreActions = this.m_StoreActionsOptimizationSetting != StoreActionsOptimization.Store;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001A46C File Offset: 0x0001866C
		public void Dispose()
		{
			for (int i = 0; i < this.m_RendererFeatures.Count; i++)
			{
				if (!(this.rendererFeatures[i] == null))
				{
					this.rendererFeatures[i].Dispose();
				}
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001A4C1 File Offset: 0x000186C1
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001A4C3 File Offset: 0x000186C3
		public void ConfigureCameraTarget(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget)
		{
			this.m_CameraColorTarget = colorTarget;
			this.m_CameraDepthTarget = depthTarget;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001A4D3 File Offset: 0x000186D3
		internal void ConfigureCameraTarget(RenderTargetIdentifier colorTarget, RenderTargetIdentifier depthTarget, RenderTargetIdentifier resolveTarget)
		{
			this.m_CameraColorTarget = colorTarget;
			this.m_CameraDepthTarget = depthTarget;
			this.m_CameraResolveTarget = resolveTarget;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001A4EA File Offset: 0x000186EA
		internal void ConfigureCameraColorTarget(RenderTargetIdentifier colorTarget)
		{
			this.m_CameraColorTarget = colorTarget;
		}

		// Token: 0x06000488 RID: 1160
		public abstract void Setup(ScriptableRenderContext context, ref RenderingData renderingData);

		// Token: 0x06000489 RID: 1161 RVA: 0x0001A4F3 File Offset: 0x000186F3
		public virtual void SetupLights(ScriptableRenderContext context, ref RenderingData renderingData)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001A4F5 File Offset: 0x000186F5
		public virtual void SetupCullingParameters(ref ScriptableCullingParameters cullingParameters, ref CameraData cameraData)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001A4F7 File Offset: 0x000186F7
		public virtual void FinishRendering(CommandBuffer cmd)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001A4FC File Offset: 0x000186FC
		public void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			bool flag = DebugDisplaySettings.Instance.RenderingSettings.debugSceneOverrideMode == DebugSceneOverrideMode.None;
			this.m_IsPipelineExecuting = true;
			ref CameraData ptr = ref renderingData.cameraData;
			Camera camera = ptr.camera;
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			CommandBuffer commandBuffer2 = (renderingData.cameraData.xr.enabled ? null : commandBuffer);
			using (new ProfilingScope(commandBuffer2, this.profilingExecute))
			{
				this.InternalStartRendering(context, ref renderingData);
				float time = Time.time;
				float deltaTime = Time.deltaTime;
				float smoothDeltaTime = Time.smoothDeltaTime;
				this.ClearRenderingState(commandBuffer);
				this.SetShaderTimeValues(commandBuffer, time, deltaTime, smoothDeltaTime);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				using (new ProfilingScope(null, ScriptableRenderer.Profiling.sortRenderPasses))
				{
					ScriptableRenderer.SortStable(this.m_ActiveRenderPassQueue);
				}
				this.SetupNativeRenderPassFrameData(ptr, this.useRenderPassEnabled);
				ScriptableRenderer.RenderBlocks renderBlocks = new ScriptableRenderer.RenderBlocks(this.m_ActiveRenderPassQueue);
				try
				{
					using (new ProfilingScope(null, ScriptableRenderer.Profiling.setupLights))
					{
						this.SetupLights(context, ref renderingData);
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.BeforeRendering) > 0)
					{
						using (new ProfilingScope(null, ScriptableRenderer.Profiling.RenderBlock.beforeRendering))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.BeforeRendering, in renderBlocks, context, ref renderingData, false);
						}
					}
					using (new ProfilingScope(null, ScriptableRenderer.Profiling.setupCamera))
					{
						if (ptr.renderType == CameraRenderType.Base)
						{
							context.SetupCameraProperties(camera, false);
							this.SetPerCameraShaderVariables(commandBuffer, ref ptr);
						}
						else
						{
							this.SetPerCameraShaderVariables(commandBuffer, ref ptr);
							this.SetPerCameraClippingPlaneProperties(commandBuffer, in ptr);
							this.SetPerCameraBillboardProperties(commandBuffer, ref ptr);
						}
						this.SetShaderTimeValues(commandBuffer, time, deltaTime, smoothDeltaTime);
						UniversalAdditionalCameraData universalAdditionalCameraData;
						if (camera.TryGetComponent<UniversalAdditionalCameraData>(out universalAdditionalCameraData))
						{
							universalAdditionalCameraData.motionVectorsPersistentData.Update(ref ptr);
						}
					}
					context.ExecuteCommandBuffer(commandBuffer);
					commandBuffer.Clear();
					this.BeginXRRendering(commandBuffer, context, ref renderingData.cameraData);
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.MainRenderingOpaque) > 0)
					{
						using (new ProfilingScope(null, ScriptableRenderer.Profiling.RenderBlock.mainRenderingOpaque))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.MainRenderingOpaque, in renderBlocks, context, ref renderingData, false);
						}
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.MainRenderingTransparent) > 0)
					{
						using (new ProfilingScope(null, ScriptableRenderer.Profiling.RenderBlock.mainRenderingTransparent))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.MainRenderingTransparent, in renderBlocks, context, ref renderingData, false);
						}
					}
					if (ptr.xr.enabled)
					{
						ptr.xr.canMarkLateLatch = false;
					}
					if (renderBlocks.GetLength(ScriptableRenderer.RenderPassBlock.AfterRendering) > 0)
					{
						using (new ProfilingScope(null, ScriptableRenderer.Profiling.RenderBlock.afterRendering))
						{
							this.ExecuteBlock(ScriptableRenderer.RenderPassBlock.AfterRendering, in renderBlocks, context, ref renderingData, false);
						}
					}
					this.EndXRRendering(commandBuffer, context, ref renderingData.cameraData);
					this.InternalFinishRendering(context, ptr.resolveFinalTarget);
					for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
					{
						this.m_ActiveRenderPassQueue[i].m_ColorAttachmentIndices.Dispose();
						this.m_ActiveRenderPassQueue[i].m_InputAttachmentIndices.Dispose();
					}
				}
				finally
				{
					((IDisposable)renderBlocks).Dispose();
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A914 File Offset: 0x00018B14
		public void EnqueuePass(ScriptableRenderPass pass)
		{
			this.m_ActiveRenderPassQueue.Add(pass);
			if (this.disableNativeRenderPassInFeatures)
			{
				pass.useNativeRenderPass = false;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001A934 File Offset: 0x00018B34
		protected static ClearFlag GetCameraClearFlag(ref CameraData cameraData)
		{
			CameraClearFlags clearFlags = cameraData.camera.clearFlags;
			if (cameraData.renderType == CameraRenderType.Overlay)
			{
				if (!cameraData.clearDepth)
				{
					return ClearFlag.None;
				}
				return ClearFlag.DepthStencil;
			}
			else
			{
				DebugHandler debugHandler = cameraData.renderer.DebugHandler;
				if (debugHandler != null && debugHandler.IsActiveForCamera(ref cameraData) && debugHandler.IsScreenClearNeeded)
				{
					return ClearFlag.All;
				}
				if (clearFlags == CameraClearFlags.Skybox && RenderSettings.skybox != null && cameraData.postProcessEnabled && cameraData.xr.enabled)
				{
					return ClearFlag.All;
				}
				if ((clearFlags == CameraClearFlags.Skybox && RenderSettings.skybox != null) || clearFlags == CameraClearFlags.Nothing)
				{
					return ClearFlag.DepthStencil;
				}
				return ClearFlag.All;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		internal void OnPreCullRenderPasses(in CameraData cameraData)
		{
			for (int i = 0; i < this.rendererFeatures.Count; i++)
			{
				if (this.rendererFeatures[i].isActive)
				{
					this.rendererFeatures[i].OnCameraPreCull(this, in cameraData);
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001AA10 File Offset: 0x00018C10
		protected void AddRenderPasses(ref RenderingData renderingData)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.addRenderPasses))
			{
				int num = this.activeRenderPassQueue.Count;
				for (int i = 0; i < num; i++)
				{
					if (this.activeRenderPassQueue[i] != null)
					{
						this.activeRenderPassQueue[i].useNativeRenderPass = false;
					}
				}
				for (int j = 0; j < this.rendererFeatures.Count; j++)
				{
					if (this.rendererFeatures[j].isActive)
					{
						if (!this.rendererFeatures[j].SupportsNativeRenderPass())
						{
							this.disableNativeRenderPassInFeatures = true;
						}
						this.rendererFeatures[j].AddRenderPasses(this, ref renderingData);
						this.disableNativeRenderPassInFeatures = false;
					}
				}
				num = this.activeRenderPassQueue.Count;
				for (int k = num - 1; k >= 0; k--)
				{
					if (this.activeRenderPassQueue[k] == null)
					{
						this.activeRenderPassQueue.RemoveAt(k);
					}
				}
				if (num > 0 && this.m_StoreActionsOptimizationSetting == StoreActionsOptimization.Auto)
				{
					ScriptableRenderer.m_UseOptimizedStoreActions = false;
				}
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001AB2C File Offset: 0x00018D2C
		private void ClearRenderingState(CommandBuffer cmd)
		{
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.clearRenderingState))
			{
				cmd.DisableShaderKeyword("_MAIN_LIGHT_SHADOWS");
				cmd.DisableShaderKeyword("_MAIN_LIGHT_SHADOWS_CASCADE");
				cmd.DisableShaderKeyword("_ADDITIONAL_LIGHTS_VERTEX");
				cmd.DisableShaderKeyword("_ADDITIONAL_LIGHTS");
				cmd.DisableShaderKeyword("_CLUSTERED_RENDERING");
				cmd.DisableShaderKeyword("_ADDITIONAL_LIGHT_SHADOWS");
				cmd.DisableShaderKeyword("_REFLECTION_PROBE_BLENDING");
				cmd.DisableShaderKeyword("_REFLECTION_PROBE_BOX_PROJECTION");
				cmd.DisableShaderKeyword("_SHADOWS_SOFT");
				cmd.DisableShaderKeyword("_MIXED_LIGHTING_SUBTRACTIVE");
				cmd.DisableShaderKeyword("LIGHTMAP_SHADOW_MIXING");
				cmd.DisableShaderKeyword("SHADOWS_SHADOWMASK");
				cmd.DisableShaderKeyword("_LINEAR_TO_SRGB_CONVERSION");
				cmd.DisableShaderKeyword("_LIGHT_LAYERS");
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001AC00 File Offset: 0x00018E00
		internal void Clear(CameraRenderType cameraType)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = BuiltinRenderTextureType.CameraTarget;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = 0;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = BuiltinRenderTextureType.CameraTarget;
			this.m_FirstTimeCameraColorTargetIsBound = cameraType == CameraRenderType.Base;
			this.m_FirstTimeCameraDepthTargetIsBound = true;
			this.m_CameraColorTarget = BuiltinRenderTextureType.CameraTarget;
			this.m_CameraDepthTarget = BuiltinRenderTextureType.CameraTarget;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001AC78 File Offset: 0x00018E78
		private void ExecuteBlock(int blockIndex, in ScriptableRenderer.RenderBlocks renderBlocks, ScriptableRenderContext context, ref RenderingData renderingData, bool submit = false)
		{
			ScriptableRenderer.RenderBlocks renderBlocks2 = renderBlocks;
			foreach (int num in renderBlocks2.GetRange(blockIndex))
			{
				ScriptableRenderPass scriptableRenderPass = this.m_ActiveRenderPassQueue[num];
				this.ExecuteRenderPass(context, scriptableRenderPass, ref renderingData);
			}
			if (submit)
			{
				context.Submit();
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		private bool IsRenderPassEnabled(ScriptableRenderPass renderPass)
		{
			return renderPass.useNativeRenderPass && this.useRenderPassEnabled;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001AD08 File Offset: 0x00018F08
		private void ExecuteRenderPass(ScriptableRenderContext context, ScriptableRenderPass renderPass, ref RenderingData renderingData)
		{
			using (new ProfilingScope(null, renderPass.profilingSampler))
			{
				ref CameraData ptr = ref renderingData.cameraData;
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(null, ScriptableRenderer.Profiling.RenderPass.configure))
				{
					if (this.IsRenderPassEnabled(renderPass) && ptr.isRenderPassSupportedCamera)
					{
						this.ConfigureNativeRenderPass(commandBuffer, renderPass, ptr);
					}
					else
					{
						renderPass.Configure(commandBuffer, ptr.cameraTargetDescriptor);
					}
					this.SetRenderPassAttachments(commandBuffer, renderPass, ref ptr);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
				if (this.IsRenderPassEnabled(renderPass) && ptr.isRenderPassSupportedCamera)
				{
					this.ExecuteNativeRenderPass(context, renderPass, ptr, ref renderingData);
				}
				else
				{
					renderPass.Execute(context, ref renderingData);
				}
				if (ptr.xr.enabled && ptr.xr.hasMarkedLateLatch)
				{
					ptr.xr.UnmarkLateLatchShaderProperties(commandBuffer, ref ptr);
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001AE10 File Offset: 0x00019010
		private void SetRenderPassAttachments(CommandBuffer cmd, ScriptableRenderPass renderPass, ref CameraData cameraData)
		{
			Camera camera = cameraData.camera;
			ClearFlag cameraClearFlag = ScriptableRenderer.GetCameraClearFlag(ref cameraData);
			if (RenderingUtils.GetValidColorBufferCount(renderPass.colorAttachments) == 0U)
			{
				return;
			}
			if (RenderingUtils.IsMRT(renderPass.colorAttachments))
			{
				bool flag = false;
				bool flag2 = false;
				int num = RenderingUtils.IndexOf(renderPass.colorAttachments, this.m_CameraColorTarget);
				if (num != -1 && this.m_FirstTimeCameraColorTargetIsBound)
				{
					this.m_FirstTimeCameraColorTargetIsBound = false;
					flag = (cameraClearFlag & ClearFlag.Color) != (renderPass.clearFlag & ClearFlag.Color) || CoreUtils.ConvertSRGBToActiveColorSpace(camera.backgroundColor) != renderPass.clearColor;
				}
				if (renderPass.depthAttachment == this.m_CameraDepthTarget && this.m_FirstTimeCameraDepthTargetIsBound)
				{
					this.m_FirstTimeCameraDepthTargetIsBound = false;
					flag2 = (cameraClearFlag & ClearFlag.DepthStencil) != (renderPass.clearFlag & ClearFlag.DepthStencil);
				}
				if (flag)
				{
					if ((cameraClearFlag & ClearFlag.Color) != ClearFlag.None && (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera))
					{
						ScriptableRenderer.SetRenderTarget(cmd, renderPass.colorAttachments[num], renderPass.depthAttachment, ClearFlag.Color, CoreUtils.ConvertSRGBToActiveColorSpace(camera.backgroundColor));
					}
					if ((renderPass.clearFlag & ClearFlag.Color) != ClearFlag.None)
					{
						uint num2 = RenderingUtils.CountDistinct(renderPass.colorAttachments, this.m_CameraColorTarget);
						RenderTargetIdentifier[] array = ScriptableRenderer.m_TrimmedColorAttachmentCopies[(int)num2];
						int num3 = 0;
						for (int i = 0; i < renderPass.colorAttachments.Length; i++)
						{
							if (renderPass.colorAttachments[i] != this.m_CameraColorTarget && renderPass.colorAttachments[i] != 0)
							{
								array[num3] = renderPass.colorAttachments[i];
								num3++;
							}
						}
						if ((long)num3 != (long)((ulong)num2))
						{
							Debug.LogError("writeIndex and otherTargetsCount values differed. writeIndex:" + num3.ToString() + " otherTargetsCount:" + num2.ToString());
						}
						if (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera)
						{
							ScriptableRenderer.SetRenderTarget(cmd, array, this.m_CameraDepthTarget, ClearFlag.Color, renderPass.clearColor);
						}
					}
				}
				ClearFlag clearFlag = ClearFlag.None;
				clearFlag |= (flag2 ? (cameraClearFlag & ClearFlag.DepthStencil) : (renderPass.clearFlag & ClearFlag.DepthStencil));
				clearFlag |= (flag ? (this.IsRenderPassEnabled(renderPass) ? (cameraClearFlag & ClearFlag.Color) : ClearFlag.None) : (renderPass.clearFlag & ClearFlag.Color));
				if (this.IsRenderPassEnabled(renderPass) && cameraData.isRenderPassSupportedCamera)
				{
					this.SetNativeRenderPassMRTAttachmentList(renderPass, ref cameraData, flag, clearFlag);
				}
				if (!RenderingUtils.SequenceEqual(renderPass.colorAttachments, ScriptableRenderer.m_ActiveColorAttachments) || renderPass.depthAttachment != ScriptableRenderer.m_ActiveDepthAttachment || clearFlag != ClearFlag.None)
				{
					int num4 = RenderingUtils.LastValid(renderPass.colorAttachments);
					if (num4 >= 0)
					{
						int num5 = num4 + 1;
						RenderTargetIdentifier[] array2 = ScriptableRenderer.m_TrimmedColorAttachmentCopies[num5];
						for (int j = 0; j < num5; j++)
						{
							array2[j] = renderPass.colorAttachments[j];
						}
						if (!this.IsRenderPassEnabled(renderPass) || !cameraData.isRenderPassSupportedCamera)
						{
							RenderTargetIdentifier renderTargetIdentifier = this.m_CameraDepthTarget;
							if (renderPass.overrideCameraTarget)
							{
								renderTargetIdentifier = renderPass.depthAttachment;
							}
							else
							{
								this.m_FirstTimeCameraDepthTargetIsBound = false;
							}
							ScriptableRenderer.SetRenderTarget(cmd, array2, renderTargetIdentifier, clearFlag, renderPass.clearColor);
						}
						if (cameraData.xr.enabled)
						{
							bool flag3 = RenderingUtils.IndexOf(renderPass.colorAttachments, cameraData.xr.renderTarget) != -1 && !cameraData.xr.renderTargetIsRenderTexture;
							cameraData.xr.UpdateGPUViewAndProjectionMatrices(cmd, ref cameraData, !flag3);
							return;
						}
					}
				}
			}
			else
			{
				RenderTargetIdentifier renderTargetIdentifier2 = renderPass.colorAttachment;
				RenderTargetIdentifier renderTargetIdentifier3 = renderPass.depthAttachment;
				if (!renderPass.overrideCameraTarget)
				{
					if (renderPass.renderPassEvent < RenderPassEvent.BeforeRenderingPrePasses)
					{
						return;
					}
					renderTargetIdentifier2 = this.m_CameraColorTarget;
					renderTargetIdentifier3 = this.m_CameraDepthTarget;
				}
				ClearFlag clearFlag2 = ClearFlag.None;
				Color color;
				if (renderTargetIdentifier2 == this.m_CameraColorTarget && this.m_FirstTimeCameraColorTargetIsBound)
				{
					this.m_FirstTimeCameraColorTargetIsBound = false;
					clearFlag2 |= cameraClearFlag & ClearFlag.Color;
					color = CoreUtils.ConvertSRGBToActiveColorSpace(camera.backgroundColor);
					if (this.m_FirstTimeCameraDepthTargetIsBound)
					{
						this.m_FirstTimeCameraDepthTargetIsBound = false;
						clearFlag2 |= cameraClearFlag & ClearFlag.DepthStencil;
					}
				}
				else
				{
					clearFlag2 |= renderPass.clearFlag & ClearFlag.Color;
					color = renderPass.clearColor;
				}
				if (this.m_CameraDepthTarget != BuiltinRenderTextureType.CameraTarget && (renderTargetIdentifier3 == this.m_CameraDepthTarget || renderTargetIdentifier2 == this.m_CameraDepthTarget) && this.m_FirstTimeCameraDepthTargetIsBound)
				{
					this.m_FirstTimeCameraDepthTargetIsBound = false;
					clearFlag2 |= cameraClearFlag & ClearFlag.DepthStencil;
				}
				else
				{
					clearFlag2 |= renderPass.clearFlag & ClearFlag.DepthStencil;
				}
				if (this.DebugHandler != null && this.DebugHandler.IsActiveForCamera(ref cameraData))
				{
					this.DebugHandler.TryGetScreenClearColor(ref color);
				}
				if (this.IsRenderPassEnabled(renderPass) && cameraData.isRenderPassSupportedCamera)
				{
					this.SetNativeRenderPassAttachmentList(renderPass, ref cameraData, renderTargetIdentifier2, renderTargetIdentifier3, clearFlag2, color);
					return;
				}
				if (renderTargetIdentifier2 != ScriptableRenderer.m_ActiveColorAttachments[0] || renderTargetIdentifier3 != ScriptableRenderer.m_ActiveDepthAttachment || clearFlag2 != ClearFlag.None || renderPass.colorStoreActions[0] != ScriptableRenderer.m_ActiveColorStoreActions[0] || renderPass.depthStoreAction != ScriptableRenderer.m_ActiveDepthStoreAction)
				{
					ScriptableRenderer.SetRenderTarget(cmd, renderTargetIdentifier2, renderTargetIdentifier3, clearFlag2, color, renderPass.colorStoreActions[0], renderPass.depthStoreAction);
					if (cameraData.xr.enabled)
					{
						bool flag4 = renderTargetIdentifier2 == cameraData.xr.renderTarget && !cameraData.xr.renderTargetIsRenderTexture;
						cameraData.xr.UpdateGPUViewAndProjectionMatrices(cmd, ref cameraData, !flag4);
					}
				}
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001B330 File Offset: 0x00019530
		private void BeginXRRendering(CommandBuffer cmd, ScriptableRenderContext context, ref CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				if (cameraData.xr.isLateLatchEnabled)
				{
					cameraData.xr.canMarkLateLatch = true;
				}
				cameraData.xr.StartSinglePass(cmd);
				cmd.EnableShaderKeyword("_USE_DRAW_PROCEDURAL");
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001B388 File Offset: 0x00019588
		private void EndXRRendering(CommandBuffer cmd, ScriptableRenderContext context, ref CameraData cameraData)
		{
			if (cameraData.xr.enabled)
			{
				cameraData.xr.StopSinglePass(cmd);
				cmd.DisableShaderKeyword("_USE_DRAW_PROCEDURAL");
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001B3BC File Offset: 0x000195BC
		internal static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment, ClearFlag clearFlag, Color clearColor)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = colorAttachment;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = 0;
			}
			ScriptableRenderer.m_ActiveColorStoreActions[0] = RenderBufferStoreAction.Store;
			ScriptableRenderer.m_ActiveDepthStoreAction = RenderBufferStoreAction.Store;
			for (int j = 1; j < ScriptableRenderer.m_ActiveColorStoreActions.Length; j++)
			{
				ScriptableRenderer.m_ActiveColorStoreActions[j] = RenderBufferStoreAction.Store;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			RenderBufferLoadAction renderBufferLoadAction = (((clearFlag & ClearFlag.Color) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			RenderBufferLoadAction renderBufferLoadAction2 = (((clearFlag & ClearFlag.Depth) != ClearFlag.None || (clearFlag & ClearFlag.Stencil) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			ScriptableRenderer.SetRenderTarget(cmd, colorAttachment, renderBufferLoadAction, RenderBufferStoreAction.Store, depthAttachment, renderBufferLoadAction2, RenderBufferStoreAction.Store, clearFlag, clearColor);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001B450 File Offset: 0x00019650
		internal static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorAttachment, RenderTargetIdentifier depthAttachment, ClearFlag clearFlag, Color clearColor, RenderBufferStoreAction colorStoreAction, RenderBufferStoreAction depthStoreAction)
		{
			ScriptableRenderer.m_ActiveColorAttachments[0] = colorAttachment;
			for (int i = 1; i < ScriptableRenderer.m_ActiveColorAttachments.Length; i++)
			{
				ScriptableRenderer.m_ActiveColorAttachments[i] = 0;
			}
			ScriptableRenderer.m_ActiveColorStoreActions[0] = colorStoreAction;
			ScriptableRenderer.m_ActiveDepthStoreAction = depthStoreAction;
			for (int j = 1; j < ScriptableRenderer.m_ActiveColorStoreActions.Length; j++)
			{
				ScriptableRenderer.m_ActiveColorStoreActions[j] = RenderBufferStoreAction.Store;
			}
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			RenderBufferLoadAction renderBufferLoadAction = (((clearFlag & ClearFlag.Color) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			RenderBufferLoadAction renderBufferLoadAction2 = (((clearFlag & ClearFlag.Depth) != ClearFlag.None) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load);
			if (!ScriptableRenderer.m_UseOptimizedStoreActions)
			{
				if (colorStoreAction != RenderBufferStoreAction.StoreAndResolve)
				{
					colorStoreAction = RenderBufferStoreAction.Store;
				}
				if (depthStoreAction != RenderBufferStoreAction.StoreAndResolve)
				{
					depthStoreAction = RenderBufferStoreAction.Store;
				}
			}
			ScriptableRenderer.SetRenderTarget(cmd, colorAttachment, renderBufferLoadAction, colorStoreAction, depthAttachment, renderBufferLoadAction2, depthStoreAction, clearFlag, clearColor);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001B4F8 File Offset: 0x000196F8
		private static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorAttachment, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, ClearFlag clearFlags, Color clearColor)
		{
			CoreUtils.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, clearFlags, clearColor);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001B508 File Offset: 0x00019708
		private static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorAttachment, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthAttachment, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlags, Color clearColor)
		{
			if (depthAttachment == BuiltinRenderTextureType.CameraTarget)
			{
				CoreUtils.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction, clearFlags, clearColor);
				return;
			}
			CoreUtils.SetRenderTarget(cmd, colorAttachment, colorLoadAction, colorStoreAction, depthAttachment, depthLoadAction, depthStoreAction, clearFlags, clearColor);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001B549 File Offset: 0x00019749
		private static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorAttachments, RenderTargetIdentifier depthAttachment, ClearFlag clearFlag, Color clearColor)
		{
			ScriptableRenderer.m_ActiveColorAttachments = colorAttachments;
			ScriptableRenderer.m_ActiveDepthAttachment = depthAttachment;
			CoreUtils.SetRenderTarget(cmd, colorAttachments, depthAttachment, clearFlag, clearColor);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001B562 File Offset: 0x00019762
		internal virtual void SwapColorBuffer(CommandBuffer cmd)
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001B564 File Offset: 0x00019764
		internal virtual void EnableSwapBufferMSAA(bool enable)
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001B566 File Offset: 0x00019766
		[Conditional("UNITY_EDITOR")]
		private void DrawGizmos(ScriptableRenderContext context, Camera camera, GizmoSubset gizmoSubset)
		{
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001B568 File Offset: 0x00019768
		[Conditional("UNITY_EDITOR")]
		private void DrawWireOverlay(ScriptableRenderContext context, Camera camera)
		{
			context.DrawWireOverlay(camera);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001B574 File Offset: 0x00019774
		private void InternalStartRendering(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.internalStartRendering))
			{
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					this.m_ActiveRenderPassQueue[i].OnCameraSetup(commandBuffer, ref renderingData);
				}
			}
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001B5EC File Offset: 0x000197EC
		private void InternalFinishRendering(ScriptableRenderContext context, bool resolveFinalTarget)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get();
			using (new ProfilingScope(null, ScriptableRenderer.Profiling.internalFinishRendering))
			{
				for (int i = 0; i < this.m_ActiveRenderPassQueue.Count; i++)
				{
					this.m_ActiveRenderPassQueue[i].FrameCleanup(commandBuffer);
				}
				if (resolveFinalTarget)
				{
					for (int j = 0; j < this.m_ActiveRenderPassQueue.Count; j++)
					{
						this.m_ActiveRenderPassQueue[j].OnFinishCameraStackRendering(commandBuffer);
					}
					this.FinishRendering(commandBuffer);
					this.m_IsPipelineExecuting = false;
				}
				this.m_ActiveRenderPassQueue.Clear();
			}
			this.ResetNativeRenderPassFrameData();
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001B6AC File Offset: 0x000198AC
		internal static void SortStable(List<ScriptableRenderPass> list)
		{
			for (int i = 1; i < list.Count; i++)
			{
				ScriptableRenderPass scriptableRenderPass = list[i];
				int num = i - 1;
				while (num >= 0 && scriptableRenderPass < list[num])
				{
					list[num + 1] = list[num];
					num--;
				}
				list[num + 1] = scriptableRenderPass;
			}
		}

		// Token: 0x04000320 RID: 800
		private const int kRenderPassMapSize = 10;

		// Token: 0x04000321 RID: 801
		private const int kRenderPassMaxCount = 20;

		// Token: 0x04000322 RID: 802
		private int m_LastBeginSubpassPassIndex;

		// Token: 0x04000323 RID: 803
		private Dictionary<Hash128, int[]> m_MergeableRenderPassesMap = new Dictionary<Hash128, int[]>(10);

		// Token: 0x04000324 RID: 804
		private int[][] m_MergeableRenderPassesMapArrays;

		// Token: 0x04000325 RID: 805
		private Hash128[] m_PassIndexToPassHash = new Hash128[20];

		// Token: 0x04000326 RID: 806
		private Dictionary<Hash128, int> m_RenderPassesAttachmentCount = new Dictionary<Hash128, int>(10);

		// Token: 0x04000327 RID: 807
		private AttachmentDescriptor[] m_ActiveColorAttachmentDescriptors = new AttachmentDescriptor[]
		{
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment,
			RenderingUtils.emptyAttachment
		};

		// Token: 0x04000328 RID: 808
		private AttachmentDescriptor m_ActiveDepthAttachmentDescriptor;

		// Token: 0x04000329 RID: 809
		private bool[] m_IsActiveColorAttachmentTransient = new bool[8];

		// Token: 0x0400032A RID: 810
		internal RenderBufferStoreAction[] m_FinalColorStoreAction = new RenderBufferStoreAction[8];

		// Token: 0x0400032B RID: 811
		internal RenderBufferStoreAction m_FinalDepthStoreAction;

		// Token: 0x0400032E RID: 814
		internal static ScriptableRenderer current = null;

		// Token: 0x04000331 RID: 817
		private StoreActionsOptimization m_StoreActionsOptimizationSetting;

		// Token: 0x04000332 RID: 818
		private static bool m_UseOptimizedStoreActions = false;

		// Token: 0x04000333 RID: 819
		private const int k_RenderPassBlockCount = 4;

		// Token: 0x04000334 RID: 820
		private List<ScriptableRenderPass> m_ActiveRenderPassQueue = new List<ScriptableRenderPass>(32);

		// Token: 0x04000335 RID: 821
		private List<ScriptableRendererFeature> m_RendererFeatures = new List<ScriptableRendererFeature>(10);

		// Token: 0x04000336 RID: 822
		private RenderTargetIdentifier m_CameraColorTarget;

		// Token: 0x04000337 RID: 823
		private RenderTargetIdentifier m_CameraDepthTarget;

		// Token: 0x04000338 RID: 824
		private RenderTargetIdentifier m_CameraResolveTarget;

		// Token: 0x04000339 RID: 825
		private bool m_FirstTimeCameraColorTargetIsBound = true;

		// Token: 0x0400033A RID: 826
		private bool m_FirstTimeCameraDepthTargetIsBound = true;

		// Token: 0x0400033B RID: 827
		private bool m_IsPipelineExecuting;

		// Token: 0x0400033C RID: 828
		internal bool isCameraColorTargetValid;

		// Token: 0x0400033D RID: 829
		internal bool disableNativeRenderPassInFeatures;

		// Token: 0x0400033E RID: 830
		internal bool useRenderPassEnabled;

		// Token: 0x0400033F RID: 831
		private static RenderTargetIdentifier[] m_ActiveColorAttachments = new RenderTargetIdentifier[] { 0, 0, 0, 0, 0, 0, 0, 0 };

		// Token: 0x04000340 RID: 832
		private static RenderTargetIdentifier m_ActiveDepthAttachment;

		// Token: 0x04000341 RID: 833
		private static RenderBufferStoreAction[] m_ActiveColorStoreActions = new RenderBufferStoreAction[8];

		// Token: 0x04000342 RID: 834
		private static RenderBufferStoreAction m_ActiveDepthStoreAction = RenderBufferStoreAction.Store;

		// Token: 0x04000343 RID: 835
		private static RenderTargetIdentifier[][] m_TrimmedColorAttachmentCopies = new RenderTargetIdentifier[][]
		{
			new RenderTargetIdentifier[0],
			new RenderTargetIdentifier[] { 0 },
			new RenderTargetIdentifier[] { 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0, 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0, 0, 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0, 0, 0, 0, 0 },
			new RenderTargetIdentifier[] { 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		// Token: 0x04000344 RID: 836
		private static Plane[] s_Planes = new Plane[6];

		// Token: 0x04000345 RID: 837
		private static Vector4[] s_VectorPlanes = new Vector4[6];

		// Token: 0x0200016C RID: 364
		private static class Profiling
		{
			// Token: 0x04000953 RID: 2387
			public static readonly ProfilingSampler setMRTAttachmentsList = new ProfilingSampler("NativeRenderPass SetNativeRenderPassMRTAttachmentList");

			// Token: 0x04000954 RID: 2388
			public static readonly ProfilingSampler setAttachmentList = new ProfilingSampler("NativeRenderPass SetNativeRenderPassAttachmentList");

			// Token: 0x04000955 RID: 2389
			public static readonly ProfilingSampler configure = new ProfilingSampler("NativeRenderPass ConfigureNativeRenderPass");

			// Token: 0x04000956 RID: 2390
			public static readonly ProfilingSampler execute = new ProfilingSampler("NativeRenderPass ExecuteNativeRenderPass");

			// Token: 0x04000957 RID: 2391
			public static readonly ProfilingSampler setupFrameData = new ProfilingSampler("NativeRenderPass SetupNativeRenderPassFrameData");

			// Token: 0x04000958 RID: 2392
			private const string k_Name = "ScriptableRenderer";

			// Token: 0x04000959 RID: 2393
			public static readonly ProfilingSampler setPerCameraShaderVariables = new ProfilingSampler("ScriptableRenderer.SetPerCameraShaderVariables");

			// Token: 0x0400095A RID: 2394
			public static readonly ProfilingSampler sortRenderPasses = new ProfilingSampler("Sort Render Passes");

			// Token: 0x0400095B RID: 2395
			public static readonly ProfilingSampler setupLights = new ProfilingSampler("ScriptableRenderer.SetupLights");

			// Token: 0x0400095C RID: 2396
			public static readonly ProfilingSampler setupCamera = new ProfilingSampler("Setup Camera Parameters");

			// Token: 0x0400095D RID: 2397
			public static readonly ProfilingSampler addRenderPasses = new ProfilingSampler("ScriptableRenderer.AddRenderPasses");

			// Token: 0x0400095E RID: 2398
			public static readonly ProfilingSampler clearRenderingState = new ProfilingSampler("ScriptableRenderer.ClearRenderingState");

			// Token: 0x0400095F RID: 2399
			public static readonly ProfilingSampler internalStartRendering = new ProfilingSampler("ScriptableRenderer.InternalStartRendering");

			// Token: 0x04000960 RID: 2400
			public static readonly ProfilingSampler internalFinishRendering = new ProfilingSampler("ScriptableRenderer.InternalFinishRendering");

			// Token: 0x04000961 RID: 2401
			public static readonly ProfilingSampler drawGizmos = new ProfilingSampler("DrawGizmos");

			// Token: 0x020001DC RID: 476
			public static class RenderBlock
			{
				// Token: 0x04000B6A RID: 2922
				private const string k_Name = "RenderPassBlock";

				// Token: 0x04000B6B RID: 2923
				public static readonly ProfilingSampler beforeRendering = new ProfilingSampler("RenderPassBlock.BeforeRendering");

				// Token: 0x04000B6C RID: 2924
				public static readonly ProfilingSampler mainRenderingOpaque = new ProfilingSampler("RenderPassBlock.MainRenderingOpaque");

				// Token: 0x04000B6D RID: 2925
				public static readonly ProfilingSampler mainRenderingTransparent = new ProfilingSampler("RenderPassBlock.MainRenderingTransparent");

				// Token: 0x04000B6E RID: 2926
				public static readonly ProfilingSampler afterRendering = new ProfilingSampler("RenderPassBlock.AfterRendering");
			}

			// Token: 0x020001DD RID: 477
			public static class RenderPass
			{
				// Token: 0x04000B6F RID: 2927
				private const string k_Name = "ScriptableRenderPass";

				// Token: 0x04000B70 RID: 2928
				public static readonly ProfilingSampler configure = new ProfilingSampler("ScriptableRenderPass.Configure");
			}
		}

		// Token: 0x0200016D RID: 365
		internal struct RenderPassDescriptor
		{
			// Token: 0x06000998 RID: 2456 RVA: 0x0004022F File Offset: 0x0003E42F
			internal RenderPassDescriptor(int width, int height, int sampleCount, int rtID)
			{
				this.w = width;
				this.h = height;
				this.samples = sampleCount;
				this.depthID = rtID;
			}

			// Token: 0x04000962 RID: 2402
			internal int w;

			// Token: 0x04000963 RID: 2403
			internal int h;

			// Token: 0x04000964 RID: 2404
			internal int samples;

			// Token: 0x04000965 RID: 2405
			internal int depthID;
		}

		// Token: 0x0200016E RID: 366
		public class RenderingFeatures
		{
			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000999 RID: 2457 RVA: 0x0004024E File Offset: 0x0003E44E
			// (set) Token: 0x0600099A RID: 2458 RVA: 0x00040256 File Offset: 0x0003E456
			[Obsolete("cameraStacking has been deprecated use SupportedCameraRenderTypes() in ScriptableRenderer instead.", false)]
			public bool cameraStacking { get; set; }

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x0600099B RID: 2459 RVA: 0x0004025F File Offset: 0x0003E45F
			// (set) Token: 0x0600099C RID: 2460 RVA: 0x00040267 File Offset: 0x0003E467
			public bool msaa { get; set; } = true;
		}

		// Token: 0x0200016F RID: 367
		private static class RenderPassBlock
		{
			// Token: 0x04000968 RID: 2408
			public static readonly int BeforeRendering = 0;

			// Token: 0x04000969 RID: 2409
			public static readonly int MainRenderingOpaque = 1;

			// Token: 0x0400096A RID: 2410
			public static readonly int MainRenderingTransparent = 2;

			// Token: 0x0400096B RID: 2411
			public static readonly int AfterRendering = 3;
		}

		// Token: 0x02000170 RID: 368
		internal struct RenderBlocks : IDisposable
		{
			// Token: 0x0600099F RID: 2463 RVA: 0x0004029C File Offset: 0x0003E49C
			public RenderBlocks(List<ScriptableRenderPass> activeRenderPassQueue)
			{
				this.m_BlockEventLimits = new NativeArray<RenderPassEvent>(4, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockRanges = new NativeArray<int>(this.m_BlockEventLimits.Length + 1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockRangeLengths = new NativeArray<int>(this.m_BlockRanges.Length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.BeforeRendering] = RenderPassEvent.BeforeRenderingPrePasses;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.MainRenderingOpaque] = RenderPassEvent.AfterRenderingOpaques;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.MainRenderingTransparent] = RenderPassEvent.AfterRenderingPostProcessing;
				this.m_BlockEventLimits[ScriptableRenderer.RenderPassBlock.AfterRendering] = (RenderPassEvent)2147483647;
				this.FillBlockRanges(activeRenderPassQueue);
				this.m_BlockEventLimits.Dispose();
				for (int i = 0; i < this.m_BlockRanges.Length - 1; i++)
				{
					this.m_BlockRangeLengths[i] = this.m_BlockRanges[i + 1] - this.m_BlockRanges[i];
				}
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x0004038E File Offset: 0x0003E58E
			public void Dispose()
			{
				this.m_BlockRangeLengths.Dispose();
				this.m_BlockRanges.Dispose();
			}

			// Token: 0x060009A1 RID: 2465 RVA: 0x000403A8 File Offset: 0x0003E5A8
			private void FillBlockRanges(List<ScriptableRenderPass> activeRenderPassQueue)
			{
				int num = 0;
				int num2 = 0;
				this.m_BlockRanges[num++] = 0;
				for (int i = 0; i < this.m_BlockEventLimits.Length - 1; i++)
				{
					while (num2 < activeRenderPassQueue.Count && activeRenderPassQueue[num2].renderPassEvent < this.m_BlockEventLimits[i])
					{
						num2++;
					}
					this.m_BlockRanges[num++] = num2;
				}
				this.m_BlockRanges[num] = activeRenderPassQueue.Count;
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x0004042C File Offset: 0x0003E62C
			public int GetLength(int index)
			{
				return this.m_BlockRangeLengths[index];
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0004043A File Offset: 0x0003E63A
			public ScriptableRenderer.RenderBlocks.BlockRange GetRange(int index)
			{
				return new ScriptableRenderer.RenderBlocks.BlockRange(this.m_BlockRanges[index], this.m_BlockRanges[index + 1]);
			}

			// Token: 0x0400096C RID: 2412
			private NativeArray<RenderPassEvent> m_BlockEventLimits;

			// Token: 0x0400096D RID: 2413
			private NativeArray<int> m_BlockRanges;

			// Token: 0x0400096E RID: 2414
			private NativeArray<int> m_BlockRangeLengths;

			// Token: 0x020001DE RID: 478
			public struct BlockRange : IDisposable
			{
				// Token: 0x06000AB8 RID: 2744 RVA: 0x00043481 File Offset: 0x00041681
				public BlockRange(int begin, int end)
				{
					this.m_Current = ((begin < end) ? begin : end);
					this.m_End = ((end >= begin) ? end : begin);
					this.m_Current--;
				}

				// Token: 0x06000AB9 RID: 2745 RVA: 0x000434AD File Offset: 0x000416AD
				public ScriptableRenderer.RenderBlocks.BlockRange GetEnumerator()
				{
					return this;
				}

				// Token: 0x06000ABA RID: 2746 RVA: 0x000434B8 File Offset: 0x000416B8
				public bool MoveNext()
				{
					int num = this.m_Current + 1;
					this.m_Current = num;
					return num < this.m_End;
				}

				// Token: 0x17000240 RID: 576
				// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000434DE File Offset: 0x000416DE
				public int Current
				{
					get
					{
						return this.m_Current;
					}
				}

				// Token: 0x06000ABC RID: 2748 RVA: 0x000434E6 File Offset: 0x000416E6
				public void Dispose()
				{
				}

				// Token: 0x04000B71 RID: 2929
				private int m_Current;

				// Token: 0x04000B72 RID: 2930
				private int m_End;
			}
		}
	}
}
