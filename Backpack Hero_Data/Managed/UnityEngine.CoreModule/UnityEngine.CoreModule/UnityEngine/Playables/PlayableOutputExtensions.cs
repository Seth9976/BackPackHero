using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000446 RID: 1094
	public static class PlayableOutputExtensions
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x000403FC File Offset: 0x0003E5FC
		public static bool IsOutputNull<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsNull();
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x00040424 File Offset: 0x0003E624
		public static bool IsOutputValid<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().IsValid();
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0004044C File Offset: 0x0003E64C
		public static Object GetReferenceObject<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetReferenceObject();
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00040474 File Offset: 0x0003E674
		public static void SetReferenceObject<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetReferenceObject(value);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0004049C File Offset: 0x0003E69C
		public static Object GetUserData<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetUserData();
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000404C4 File Offset: 0x0003E6C4
		public static void SetUserData<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetUserData(value);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000404EC File Offset: 0x0003E6EC
		public static Playable GetSourcePlayable<U>(this U output) where U : struct, IPlayableOutput
		{
			return new Playable(output.GetHandle().GetSourcePlayable());
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00040518 File Offset: 0x0003E718
		public static void SetSourcePlayable<U, V>(this U output, V value) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), output.GetSourceOutputPort<U>());
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x00040550 File Offset: 0x0003E750
		public static void SetSourcePlayable<U, V>(this U output, V value, int port) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), port);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x00040584 File Offset: 0x0003E784
		public static int GetSourceOutputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000405AC File Offset: 0x0003E7AC
		public static float GetWeight<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetWeight();
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000405D4 File Offset: 0x0003E7D4
		public static void SetWeight<U>(this U output, float value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetWeight(value);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000405FC File Offset: 0x0003E7FC
		public static void PushNotification<U>(this U output, Playable origin, INotification notification, object context = null) where U : struct, IPlayableOutput
		{
			output.GetHandle().PushNotification(origin.GetHandle(), notification, context);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0004062C File Offset: 0x0003E82C
		public static INotificationReceiver[] GetNotificationReceivers<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetNotificationReceivers();
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00040654 File Offset: 0x0003E854
		public static void AddNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().AddNotificationReceiver(receiver);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0004067C File Offset: 0x0003E87C
		public static void RemoveNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().RemoveNotificationReceiver(receiver);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000406A4 File Offset: 0x0003E8A4
		[Obsolete("Method GetSourceInputPort has been renamed to GetSourceOutputPort (UnityUpgradable) -> GetSourceOutputPort<U>(*)", false)]
		public static int GetSourceInputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000406CB File Offset: 0x0003E8CB
		[Obsolete("Method SetSourceInputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceInputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000406CB File Offset: 0x0003E8CB
		[Obsolete("Method SetSourceOutputPort has been deprecated. Use SetSourcePlayable(Playable, Port) instead.", false)]
		public static void SetSourceOutputPort<U>(this U output, int value) where U : struct, IPlayableOutput
		{
			output.SetSourcePlayable(output.GetSourcePlayable<U>(), value);
		}
	}
}
