using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004E RID: 78
	internal static class NotificationUtilities
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public static ScriptPlayable<TimeNotificationBehaviour> CreateNotificationsPlayable(PlayableGraph graph, IEnumerable<IMarker> markers, double duration, DirectorWrapMode extrapolationMode)
		{
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = ScriptPlayable<TimeNotificationBehaviour>.Null;
			foreach (IMarker marker in markers)
			{
				INotification notification = marker as INotification;
				if (notification != null)
				{
					if (scriptPlayable.Equals(ScriptPlayable<TimeNotificationBehaviour>.Null))
					{
						scriptPlayable = TimeNotificationBehaviour.Create(graph, duration, extrapolationMode);
					}
					DiscreteTime discreteTime = (DiscreteTime)marker.time;
					DiscreteTime discreteTime2 = (DiscreteTime)duration;
					if (discreteTime >= discreteTime2 && discreteTime <= discreteTime2.OneTickAfter() && discreteTime2 != 0)
					{
						discreteTime = discreteTime2.OneTickBefore();
					}
					INotificationOptionProvider notificationOptionProvider = marker as INotificationOptionProvider;
					if (notificationOptionProvider != null)
					{
						scriptPlayable.GetBehaviour().AddNotification((double)discreteTime, notification, notificationOptionProvider.flags);
					}
					else
					{
						scriptPlayable.GetBehaviour().AddNotification((double)discreteTime, notification, NotificationFlags.Retroactive);
					}
				}
			}
			return scriptPlayable;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A3A0 File Offset: 0x000085A0
		public static bool TrackTypeSupportsNotifications(Type type)
		{
			TrackBindingTypeAttribute trackBindingTypeAttribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(type, typeof(TrackBindingTypeAttribute));
			return trackBindingTypeAttribute != null && (typeof(Component).IsAssignableFrom(trackBindingTypeAttribute.type) || typeof(GameObject).IsAssignableFrom(trackBindingTypeAttribute.type));
		}
	}
}
