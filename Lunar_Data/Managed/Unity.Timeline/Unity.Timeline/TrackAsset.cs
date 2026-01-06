using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x02000010 RID: 16
	[IgnoreOnPlayableTrack]
	[Serializable]
	public abstract class TrackAsset : PlayableAsset, ISerializationCallbackReceiver, IPropertyPreview, ICurvesOwner
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00004E92 File Offset: 0x00003092
		protected virtual void OnBeforeTrackSerialize()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004E94 File Offset: 0x00003094
		protected virtual void OnAfterTrackDeserialize()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004E96 File Offset: 0x00003096
		internal virtual void OnUpgradeFromVersion(int oldVersion)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004E98 File Offset: 0x00003098
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 3;
			if (this.m_Children != null)
			{
				for (int i = this.m_Children.Count - 1; i >= 0; i--)
				{
					TrackAsset trackAsset = this.m_Children[i] as TrackAsset;
					if (trackAsset != null && trackAsset.parent != this)
					{
						trackAsset.parent = this;
					}
				}
			}
			this.OnBeforeTrackSerialize();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004F04 File Offset: 0x00003104
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_ClipsCache = null;
			this.Invalidate();
			if (this.m_Version < 3)
			{
				this.UpgradeToLatestVersion();
				this.OnUpgradeFromVersion(this.m_Version);
			}
			foreach (IMarker marker in this.GetMarkers())
			{
				marker.Initialize(this);
			}
			this.OnAfterTrackDeserialize();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F80 File Offset: 0x00003180
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000126 RID: 294 RVA: 0x00004F84 File Offset: 0x00003184
		// (remove) Token: 0x06000127 RID: 295 RVA: 0x00004FB8 File Offset: 0x000031B8
		internal static event Action<TimelineClip, GameObject, Playable> OnClipPlayableCreate;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000128 RID: 296 RVA: 0x00004FEC File Offset: 0x000031EC
		// (remove) Token: 0x06000129 RID: 297 RVA: 0x00005020 File Offset: 0x00003220
		internal static event Action<TrackAsset, GameObject, Playable> OnTrackAnimationPlayableCreate;

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005053 File Offset: 0x00003253
		public double start
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_Start;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005067 File Offset: 0x00003267
		public double end
		{
			get
			{
				this.UpdateDuration();
				return (double)this.m_End;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000507B File Offset: 0x0000327B
		public sealed override double duration
		{
			get
			{
				this.UpdateDuration();
				return (double)(this.m_End - this.m_Start);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000509A File Offset: 0x0000329A
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000050A2 File Offset: 0x000032A2
		public bool muted
		{
			get
			{
				return this.m_Muted;
			}
			set
			{
				this.m_Muted = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000050AC File Offset: 0x000032AC
		public bool mutedInHierarchy
		{
			get
			{
				if (this.muted)
				{
					return true;
				}
				TrackAsset trackAsset = this;
				while (trackAsset.parent as TrackAsset != null)
				{
					trackAsset = (TrackAsset)trackAsset.parent;
					if (trackAsset as GroupTrack != null)
					{
						return trackAsset.mutedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000050FC File Offset: 0x000032FC
		public TimelineAsset timelineAsset
		{
			get
			{
				TrackAsset trackAsset = this;
				while (trackAsset != null)
				{
					if (trackAsset.parent == null)
					{
						return null;
					}
					TimelineAsset timelineAsset = trackAsset.parent as TimelineAsset;
					if (timelineAsset != null)
					{
						return timelineAsset;
					}
					trackAsset = trackAsset.parent as TrackAsset;
				}
				return null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000514A File Offset: 0x0000334A
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005152 File Offset: 0x00003352
		public PlayableAsset parent
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

		// Token: 0x06000133 RID: 307 RVA: 0x0000515B File Offset: 0x0000335B
		public IEnumerable<TimelineClip> GetClips()
		{
			return this.clips;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005163 File Offset: 0x00003363
		internal TimelineClip[] clips
		{
			get
			{
				if (this.m_Clips == null)
				{
					this.m_Clips = new List<TimelineClip>();
				}
				if (this.m_ClipsCache == null)
				{
					this.m_CacheSorted = false;
					this.m_ClipsCache = this.m_Clips.ToArray();
				}
				return this.m_ClipsCache;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000519E File Offset: 0x0000339E
		public virtual bool isEmpty
		{
			get
			{
				return !this.hasClips && !this.hasCurves && this.GetMarkerCount() == 0;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000051BB File Offset: 0x000033BB
		public bool hasClips
		{
			get
			{
				return this.m_Clips != null && this.m_Clips.Count != 0;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000051D5 File Offset: 0x000033D5
		public bool hasCurves
		{
			get
			{
				return this.m_Curves != null && !this.m_Curves.empty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000051F8 File Offset: 0x000033F8
		public bool isSubTrack
		{
			get
			{
				TrackAsset trackAsset = this.parent as TrackAsset;
				return trackAsset != null && trackAsset.GetType() == base.GetType();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000522D File Offset: 0x0000342D
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				TrackBindingTypeAttribute trackBindingTypeAttribute;
				if (!TrackAsset.s_TrackBindingTypeAttributeCache.TryGetValue(base.GetType(), out trackBindingTypeAttribute))
				{
					trackBindingTypeAttribute = (TrackBindingTypeAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TrackBindingTypeAttribute));
					TrackAsset.s_TrackBindingTypeAttributeCache.Add(base.GetType(), trackBindingTypeAttribute);
				}
				Type type = ((trackBindingTypeAttribute != null) ? trackBindingTypeAttribute.type : null);
				yield return ScriptPlayableBinding.Create(base.name, this, type);
				yield break;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000523D File Offset: 0x0000343D
		public IEnumerable<TrackAsset> GetChildTracks()
		{
			this.UpdateChildTrackCache();
			return this.m_ChildTrackCache;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000524B File Offset: 0x0000344B
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005253 File Offset: 0x00003453
		internal string customPlayableTypename
		{
			get
			{
				return this.m_CustomPlayableFullTypename;
			}
			set
			{
				this.m_CustomPlayableFullTypename = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000525C File Offset: 0x0000345C
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005264 File Offset: 0x00003464
		public AnimationClip curves
		{
			get
			{
				return this.m_Curves;
			}
			internal set
			{
				this.m_Curves = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000526D File Offset: 0x0000346D
		string ICurvesOwner.defaultCurvesName
		{
			get
			{
				return "Track Parameters";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005274 File Offset: 0x00003474
		Object ICurvesOwner.asset
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005277 File Offset: 0x00003477
		Object ICurvesOwner.assetOwner
		{
			get
			{
				return this.timelineAsset;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000527F File Offset: 0x0000347F
		TrackAsset ICurvesOwner.targetTrack
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005282 File Offset: 0x00003482
		internal List<ScriptableObject> subTracksObjects
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000528A File Offset: 0x0000348A
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005292 File Offset: 0x00003492
		public bool locked
		{
			get
			{
				return this.m_Locked;
			}
			set
			{
				this.m_Locked = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000529C File Offset: 0x0000349C
		public bool lockedInHierarchy
		{
			get
			{
				if (this.locked)
				{
					return true;
				}
				TrackAsset trackAsset = this;
				while (trackAsset.parent as TrackAsset != null)
				{
					trackAsset = (TrackAsset)trackAsset.parent;
					if (trackAsset as GroupTrack != null)
					{
						return trackAsset.lockedInHierarchy;
					}
				}
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000052EC File Offset: 0x000034EC
		public bool supportsNotifications
		{
			get
			{
				if (this.m_SupportsNotifications == null)
				{
					this.m_SupportsNotifications = new bool?(NotificationUtilities.TrackTypeSupportsNotifications(base.GetType()));
				}
				return this.m_SupportsNotifications.Value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000531C File Offset: 0x0000351C
		private void __internalAwake()
		{
			if (this.m_Clips == null)
			{
				this.m_Clips = new List<TimelineClip>();
			}
			this.m_ChildTrackCache = null;
			if (this.m_Children == null)
			{
				this.m_Children = new List<ScriptableObject>();
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000534B File Offset: 0x0000354B
		public void CreateCurves(string curvesClipName)
		{
			if (this.m_Curves != null)
			{
				return;
			}
			this.m_Curves = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? "Track Parameters" : curvesClipName, this, true);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005379 File Offset: 0x00003579
		public virtual Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Create(graph, inputCount);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005382 File Offset: 0x00003582
		public sealed override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return Playable.Null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000538C File Offset: 0x0000358C
		public TimelineClip CreateDefaultClip()
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			Type type = null;
			object[] array = customAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				TrackClipTypeAttribute trackClipTypeAttribute = array[i] as TrackClipTypeAttribute;
				if (trackClipTypeAttribute != null && typeof(IPlayableAsset).IsAssignableFrom(trackClipTypeAttribute.inspectedType) && typeof(ScriptableObject).IsAssignableFrom(trackClipTypeAttribute.inspectedType))
				{
					type = trackClipTypeAttribute.inspectedType;
					break;
				}
			}
			if (type == null)
			{
				string text = "Cannot create a default clip for type ";
				Type type2 = base.GetType();
				Debug.LogWarning(text + ((type2 != null) ? type2.ToString() : null));
				return null;
			}
			return this.CreateAndAddNewClipOfType(type);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005436 File Offset: 0x00003636
		public TimelineClip CreateClip<T>() where T : ScriptableObject, IPlayableAsset
		{
			return this.CreateClip(typeof(T));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005448 File Offset: 0x00003648
		public bool DeleteClip(TimelineClip clip)
		{
			if (!this.m_Clips.Contains(clip))
			{
				throw new InvalidOperationException("Cannot delete clip since it is not a child of the TrackAsset.");
			}
			return this.timelineAsset != null && this.timelineAsset.DeleteClip(clip);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000547F File Offset: 0x0000367F
		public IMarker CreateMarker(Type type, double time)
		{
			return this.m_Markers.CreateMarker(type, time, this);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000548F File Offset: 0x0000368F
		public T CreateMarker<T>(double time) where T : ScriptableObject, IMarker
		{
			return (T)((object)this.CreateMarker(typeof(T), time));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000054A7 File Offset: 0x000036A7
		public bool DeleteMarker(IMarker marker)
		{
			return this.m_Markers.Remove(marker);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000054B5 File Offset: 0x000036B5
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.m_Markers.GetMarkers();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000054C2 File Offset: 0x000036C2
		public int GetMarkerCount()
		{
			return this.m_Markers.Count;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000054CF File Offset: 0x000036CF
		public IMarker GetMarker(int idx)
		{
			return this.m_Markers[idx];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000054E0 File Offset: 0x000036E0
		internal TimelineClip CreateClip(Type requestedType)
		{
			if (this.ValidateClipType(requestedType))
			{
				return this.CreateAndAddNewClipOfType(requestedType);
			}
			string text = "Clips of type ";
			string text2 = ((requestedType != null) ? requestedType.ToString() : null);
			string text3 = " are not permitted on tracks of type ";
			Type type = base.GetType();
			throw new InvalidOperationException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005534 File Offset: 0x00003734
		internal TimelineClip CreateAndAddNewClipOfType(Type requestedType)
		{
			TimelineClip timelineClip = this.CreateClipOfType(requestedType);
			this.AddClip(timelineClip);
			return timelineClip;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005554 File Offset: 0x00003754
		internal TimelineClip CreateClipOfType(Type requestedType)
		{
			if (!this.ValidateClipType(requestedType))
			{
				string text = "Clips of type ";
				string text2 = ((requestedType != null) ? requestedType.ToString() : null);
				string text3 = " are not permitted on tracks of type ";
				Type type = base.GetType();
				throw new InvalidOperationException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateInstance(requestedType);
			if (scriptableObject == null)
			{
				throw new InvalidOperationException("Could not create an instance of the ScriptableObject type " + requestedType.Name);
			}
			scriptableObject.name = requestedType.Name;
			TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, this);
			return this.CreateClipFromAsset(scriptableObject);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000055E0 File Offset: 0x000037E0
		internal TimelineClip CreateClipFromPlayableAsset(IPlayableAsset asset)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (asset as ScriptableObject == null)
			{
				throw new ArgumentException("CreateClipFromPlayableAsset  only supports ScriptableObject-derived Types");
			}
			if (!this.ValidateClipType(asset.GetType()))
			{
				string text = "Clips of type ";
				Type type = asset.GetType();
				string text2 = ((type != null) ? type.ToString() : null);
				string text3 = " are not permitted on tracks of type ";
				Type type2 = base.GetType();
				throw new InvalidOperationException(text + text2 + text3 + ((type2 != null) ? type2.ToString() : null));
			}
			return this.CreateClipFromAsset(asset as ScriptableObject);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005668 File Offset: 0x00003868
		private TimelineClip CreateClipFromAsset(ScriptableObject playableAsset)
		{
			TimelineClip timelineClip = this.CreateNewClipContainerInternal();
			timelineClip.displayName = playableAsset.name;
			timelineClip.asset = playableAsset;
			IPlayableAsset playableAsset2 = playableAsset as IPlayableAsset;
			if (playableAsset2 != null)
			{
				double duration = playableAsset2.duration;
				if (!double.IsInfinity(duration) && duration > 0.0)
				{
					timelineClip.duration = Math.Min(Math.Max(duration, TimelineClip.kMinDuration), TimelineClip.kMaxTimeValue);
				}
			}
			try
			{
				this.OnCreateClip(timelineClip);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.Message, playableAsset);
				return null;
			}
			return timelineClip;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000056FC File Offset: 0x000038FC
		internal IEnumerable<ScriptableObject> GetMarkersRaw()
		{
			return this.m_Markers.GetRawMarkerList();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005709 File Offset: 0x00003909
		internal void ClearMarkers()
		{
			this.m_Markers.Clear();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005716 File Offset: 0x00003916
		internal void AddMarker(ScriptableObject e)
		{
			this.m_Markers.Add(e);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005724 File Offset: 0x00003924
		internal bool DeleteMarkerRaw(ScriptableObject marker)
		{
			return this.m_Markers.Remove(marker, this.timelineAsset, this);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000573C File Offset: 0x0000393C
		private int GetTimeRangeHash()
		{
			double num = double.MaxValue;
			double num2 = double.MinValue;
			int count = this.m_Markers.Count;
			for (int i = 0; i < this.m_Markers.Count; i++)
			{
				IMarker marker = this.m_Markers[i];
				if (marker is INotification)
				{
					if (marker.time < num)
					{
						num = marker.time;
					}
					if (marker.time > num2)
					{
						num2 = marker.time;
					}
				}
			}
			return num.GetHashCode().CombineHash(num2.GetHashCode());
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000057C7 File Offset: 0x000039C7
		internal void AddClip(TimelineClip newClip)
		{
			if (!this.m_Clips.Contains(newClip))
			{
				this.m_Clips.Add(newClip);
				this.m_ClipsCache = null;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000057EC File Offset: 0x000039EC
		private Playable CreateNotificationsPlayable(PlayableGraph graph, Playable mixerPlayable, GameObject go, Playable timelinePlayable)
		{
			TrackAsset.s_BuildData.markerList.Clear();
			this.GatherNotifications(TrackAsset.s_BuildData.markerList);
			DirectorWrapMode directorWrapMode = DirectorWrapMode.None;
			PlayableDirector playableDirector;
			if (go.TryGetComponent<PlayableDirector>(out playableDirector))
			{
				directorWrapMode = playableDirector.extrapolationMode;
			}
			double duration = this.timelineAsset.duration;
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = NotificationUtilities.CreateNotificationsPlayable(graph, TrackAsset.s_BuildData.markerList, duration, directorWrapMode);
			if (scriptPlayable.IsValid<ScriptPlayable<TimeNotificationBehaviour>>())
			{
				scriptPlayable.GetBehaviour().timeSource = timelinePlayable;
				if (mixerPlayable.IsValid<Playable>())
				{
					scriptPlayable.SetInputCount(1);
					graph.Connect<Playable, ScriptPlayable<TimeNotificationBehaviour>>(mixerPlayable, 0, scriptPlayable, 0);
					scriptPlayable.SetInputWeight(mixerPlayable, 1f);
				}
			}
			return scriptPlayable;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005890 File Offset: 0x00003A90
		internal Playable CreatePlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree, Playable timelinePlayable)
		{
			this.UpdateDuration();
			Playable playable = Playable.Null;
			if (this.CanCreateMixerRecursive())
			{
				playable = this.CreateMixerPlayableGraph(graph, go, tree);
			}
			Playable playable2 = this.CreateNotificationsPlayable(graph, playable, go, timelinePlayable);
			TrackAsset.s_BuildData.Clear();
			if (!playable2.IsValid<Playable>() && !playable.IsValid<Playable>())
			{
				Debug.LogErrorFormat("Track {0} of type {1} has no notifications and returns an invalid mixer Playable", new object[]
				{
					base.name,
					base.GetType().FullName
				});
				return Playable.Create(graph, 0);
			}
			if (!playable2.IsValid<Playable>())
			{
				return playable;
			}
			return playable2;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000591C File Offset: 0x00003B1C
		internal virtual Playable CompileClips(PlayableGraph graph, GameObject go, IList<TimelineClip> timelineClips, IntervalTree<RuntimeElement> tree)
		{
			Playable playable = this.CreateTrackMixer(graph, go, timelineClips.Count);
			for (int i = 0; i < timelineClips.Count; i++)
			{
				Playable playable2 = this.CreatePlayable(graph, go, timelineClips[i]);
				if (playable2.IsValid<Playable>())
				{
					playable2.SetDuration(timelineClips[i].duration);
					RuntimeClip runtimeClip = new RuntimeClip(timelineClips[i], playable2, playable);
					tree.Add(runtimeClip);
					graph.Connect<Playable, Playable>(playable2, 0, playable, i);
					playable.SetInputWeight(i, 0f);
				}
			}
			this.ConfigureTrackAnimation(tree, go, playable);
			return playable;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000059B0 File Offset: 0x00003BB0
		private void GatherCompilableTracks(IList<TrackAsset> tracks)
		{
			if (!this.muted && this.CanCreateTrackMixer())
			{
				tracks.Add(this);
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherCompilableTracks(tracks);
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005A20 File Offset: 0x00003C20
		private void GatherNotifications(List<IMarker> markers)
		{
			if (!this.muted && this.CanCompileNotifications())
			{
				markers.AddRange(this.GetMarkers());
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherNotifications(markers);
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005A94 File Offset: 0x00003C94
		internal virtual Playable CreateMixerPlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree)
		{
			if (tree == null)
			{
				throw new ArgumentException("IntervalTree argument cannot be null", "tree");
			}
			if (go == null)
			{
				throw new ArgumentException("GameObject argument cannot be null", "go");
			}
			TrackAsset.s_BuildData.Clear();
			this.GatherCompilableTracks(TrackAsset.s_BuildData.trackList);
			if (TrackAsset.s_BuildData.trackList.Count == 0)
			{
				return Playable.Null;
			}
			Playable playable = Playable.Null;
			ILayerable layerable = this as ILayerable;
			if (layerable != null)
			{
				playable = layerable.CreateLayerMixer(graph, go, TrackAsset.s_BuildData.trackList.Count);
			}
			if (playable.IsValid<Playable>())
			{
				for (int i = 0; i < TrackAsset.s_BuildData.trackList.Count; i++)
				{
					Playable playable2 = TrackAsset.s_BuildData.trackList[i].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[i].clips, tree);
					if (playable2.IsValid<Playable>())
					{
						graph.Connect<Playable, Playable>(playable2, 0, playable, i);
						playable.SetInputWeight(i, 1f);
					}
				}
				return playable;
			}
			if (TrackAsset.s_BuildData.trackList.Count == 1)
			{
				return TrackAsset.s_BuildData.trackList[0].CompileClips(graph, go, TrackAsset.s_BuildData.trackList[0].clips, tree);
			}
			for (int j = 0; j < TrackAsset.s_BuildData.trackList.Count; j++)
			{
				TrackAsset.s_BuildData.clipList.AddRange(TrackAsset.s_BuildData.trackList[j].clips);
			}
			return this.CompileClips(graph, go, TrackAsset.s_BuildData.clipList, tree);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005C2E File Offset: 0x00003E2E
		internal void ConfigureTrackAnimation(IntervalTree<RuntimeElement> tree, GameObject go, Playable blend)
		{
			if (!this.hasCurves)
			{
				return;
			}
			blend.SetAnimatedProperties(this.m_Curves);
			tree.Add(new InfiniteRuntimeClip(blend));
			if (TrackAsset.OnTrackAnimationPlayableCreate != null)
			{
				TrackAsset.OnTrackAnimationPlayableCreate(this, go, blend);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005C68 File Offset: 0x00003E68
		internal void SortClips()
		{
			TimelineClip[] clips = this.clips;
			if (!this.m_CacheSorted)
			{
				Array.Sort<TimelineClip>(this.clips, (TimelineClip clip1, TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
				this.m_CacheSorted = true;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005CB5 File Offset: 0x00003EB5
		internal void ClearClipsInternal()
		{
			this.m_Clips = new List<TimelineClip>();
			this.m_ClipsCache = null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005CC9 File Offset: 0x00003EC9
		internal void ClearSubTracksInternal()
		{
			this.m_Children = new List<ScriptableObject>();
			this.Invalidate();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005CDC File Offset: 0x00003EDC
		internal void OnClipMove()
		{
			this.m_CacheSorted = false;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005CE8 File Offset: 0x00003EE8
		internal TimelineClip CreateNewClipContainerInternal()
		{
			TimelineClip timelineClip = new TimelineClip(this);
			timelineClip.asset = null;
			double num = 0.0;
			for (int i = 0; i < this.m_Clips.Count - 1; i++)
			{
				double num2 = this.m_Clips[i].duration;
				if (double.IsInfinity(num2))
				{
					num2 = (double)TimelineClip.kDefaultClipDurationInSeconds;
				}
				num = Math.Max(num, this.m_Clips[i].start + num2);
			}
			timelineClip.mixInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
			timelineClip.mixOutCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
			timelineClip.start = num;
			timelineClip.duration = (double)TimelineClip.kDefaultClipDurationInSeconds;
			timelineClip.displayName = "untitled";
			return timelineClip;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005DBD File Offset: 0x00003FBD
		internal void AddChild(TrackAsset child)
		{
			if (child == null)
			{
				return;
			}
			this.m_Children.Add(child);
			child.parent = this;
			this.Invalidate();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005DE4 File Offset: 0x00003FE4
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Children == null || this.m_Children.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject scriptableObject = this.m_Children[this.m_Children.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Children.Count - 1; i++)
			{
				if (this.m_Children[i] == asset)
				{
					for (int j = this.m_Children.Count - 1; j > i; j--)
					{
						this.m_Children[j] = this.m_Children[j - 1];
					}
					this.m_Children[i] = scriptableObject;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005EA6 File Offset: 0x000040A6
		internal bool RemoveSubTrack(TrackAsset child)
		{
			if (this.m_Children.Remove(child))
			{
				this.Invalidate();
				child.parent = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005EC6 File Offset: 0x000040C6
		internal void RemoveClip(TimelineClip clip)
		{
			this.m_Clips.Remove(clip);
			this.m_ClipsCache = null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005EDC File Offset: 0x000040DC
		internal virtual void GetEvaluationTime(out double outStart, out double outDuration)
		{
			outStart = 0.0;
			outDuration = 1.0;
			outStart = double.PositiveInfinity;
			double num = double.NegativeInfinity;
			if (this.hasCurves)
			{
				outStart = 0.0;
				num = TimeUtility.GetAnimationClipLength(this.curves);
			}
			foreach (TimelineClip timelineClip in this.clips)
			{
				outStart = Math.Min(timelineClip.start, outStart);
				num = Math.Max(timelineClip.end, num);
			}
			if (this.HasNotifications())
			{
				double notificationDuration = this.GetNotificationDuration();
				outStart = Math.Min(notificationDuration, outStart);
				num = Math.Max(notificationDuration, num);
			}
			if (double.IsInfinity(outStart) || double.IsInfinity(num))
			{
				outStart = (outDuration = 0.0);
				return;
			}
			outDuration = num - outStart;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005FB6 File Offset: 0x000041B6
		internal virtual void GetSequenceTime(out double outStart, out double outDuration)
		{
			this.GetEvaluationTime(out outStart, out outDuration);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005FC0 File Offset: 0x000041C0
		public virtual void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			GameObject gameObjectBinding = this.GetGameObjectBinding(director);
			if (gameObjectBinding != null)
			{
				driver.PushActiveGameObject(gameObjectBinding);
			}
			if (this.hasCurves)
			{
				driver.AddObjectProperties(this, this.m_Curves);
			}
			foreach (TimelineClip timelineClip in this.clips)
			{
				if (timelineClip.curves != null && timelineClip.asset != null)
				{
					driver.AddObjectProperties(timelineClip.asset, timelineClip.curves);
				}
				IPropertyPreview propertyPreview = timelineClip.asset as IPropertyPreview;
				if (propertyPreview != null)
				{
					propertyPreview.GatherProperties(director, driver);
				}
			}
			foreach (TrackAsset trackAsset in this.GetChildTracks())
			{
				if (trackAsset != null)
				{
					trackAsset.GatherProperties(director, driver);
				}
			}
			if (gameObjectBinding != null)
			{
				driver.PopActiveGameObject();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000060BC File Offset: 0x000042BC
		internal GameObject GetGameObjectBinding(PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			Object genericBinding = director.GetGenericBinding(this);
			GameObject gameObject = genericBinding as GameObject;
			if (gameObject != null)
			{
				return gameObject;
			}
			Component component = genericBinding as Component;
			if (component != null)
			{
				return component.gameObject;
			}
			return null;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006108 File Offset: 0x00004308
		internal bool ValidateClipType(Type clipType)
		{
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(TrackClipTypeAttribute), true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				if (((TrackClipTypeAttribute)customAttributes[i]).inspectedType.IsAssignableFrom(clipType))
				{
					return true;
				}
			}
			return typeof(PlayableTrack).IsAssignableFrom(base.GetType()) && typeof(IPlayableAsset).IsAssignableFrom(clipType) && typeof(ScriptableObject).IsAssignableFrom(clipType);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000618C File Offset: 0x0000438C
		protected virtual void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006190 File Offset: 0x00004390
		private void UpdateDuration()
		{
			int num = this.CalculateItemsHash();
			if (num == this.m_ItemsHash)
			{
				return;
			}
			this.m_ItemsHash = num;
			double num2;
			double num3;
			this.GetSequenceTime(out num2, out num3);
			this.m_Start = (DiscreteTime)num2;
			this.m_End = (DiscreteTime)(num2 + num3);
			this.CalculateExtrapolationTimes();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000061DF File Offset: 0x000043DF
		protected internal virtual int CalculateItemsHash()
		{
			return HashUtility.CombineHash(this.GetClipsHash(), TrackAsset.GetAnimationClipHash(this.m_Curves), this.GetTimeRangeHash());
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006200 File Offset: 0x00004400
		protected virtual Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip)
		{
			if (!graph.IsValid())
			{
				throw new ArgumentException("graph must be a valid PlayableGraph");
			}
			if (clip == null)
			{
				throw new ArgumentNullException("clip");
			}
			IPlayableAsset playableAsset = clip.asset as IPlayableAsset;
			if (playableAsset != null)
			{
				Playable playable = playableAsset.CreatePlayable(graph, gameObject);
				if (playable.IsValid<Playable>())
				{
					playable.SetAnimatedProperties(clip.curves);
					playable.SetSpeed(clip.timeScale);
					if (TrackAsset.OnClipPlayableCreate != null)
					{
						TrackAsset.OnClipPlayableCreate(clip, gameObject, playable);
					}
				}
				return playable;
			}
			return Playable.Null;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006284 File Offset: 0x00004484
		internal void Invalidate()
		{
			this.m_ChildTrackCache = null;
			TimelineAsset timelineAsset = this.timelineAsset;
			if (timelineAsset != null)
			{
				timelineAsset.Invalidate();
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000062B0 File Offset: 0x000044B0
		internal double GetNotificationDuration()
		{
			if (!this.supportsNotifications)
			{
				return 0.0;
			}
			double num = 0.0;
			int count = this.m_Markers.Count;
			for (int i = 0; i < count; i++)
			{
				IMarker marker = this.m_Markers[i];
				if (marker is INotification)
				{
					num = Math.Max(num, marker.time);
				}
			}
			return num;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006314 File Offset: 0x00004514
		internal virtual bool CanCompileClips()
		{
			return this.hasClips || this.hasCurves;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006326 File Offset: 0x00004526
		public virtual bool CanCreateTrackMixer()
		{
			return this.CanCompileClips();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006330 File Offset: 0x00004530
		internal bool IsCompilable()
		{
			if (typeof(GroupTrack).IsAssignableFrom(base.GetType()))
			{
				return false;
			}
			bool flag = !this.mutedInHierarchy && (this.CanCreateTrackMixer() || this.CanCompileNotifications());
			if (!flag)
			{
				using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCompilable())
						{
							return true;
						}
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000063BC File Offset: 0x000045BC
		private void UpdateChildTrackCache()
		{
			if (this.m_ChildTrackCache == null)
			{
				if (this.m_Children == null || this.m_Children.Count == 0)
				{
					this.m_ChildTrackCache = TrackAsset.s_EmptyCache;
					return;
				}
				List<TrackAsset> list = new List<TrackAsset>(this.m_Children.Count);
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					TrackAsset trackAsset = this.m_Children[i] as TrackAsset;
					if (trackAsset != null)
					{
						list.Add(trackAsset);
					}
				}
				this.m_ChildTrackCache = list;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006442 File Offset: 0x00004642
		internal virtual int Hash()
		{
			return this.clips.Length + (this.m_Markers.Count << 16);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000645C File Offset: 0x0000465C
		private int GetClipsHash()
		{
			int num = 0;
			foreach (TimelineClip timelineClip in this.m_Clips)
			{
				num = num.CombineHash(timelineClip.Hash());
			}
			return num;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000064B8 File Offset: 0x000046B8
		protected static int GetAnimationClipHash(AnimationClip clip)
		{
			int num = 0;
			if (clip != null && !clip.empty)
			{
				num = num.CombineHash(clip.frameRate.GetHashCode()).CombineHash(clip.length.GetHashCode());
			}
			return num;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006501 File Offset: 0x00004701
		private bool HasNotifications()
		{
			return this.m_Markers.HasNotifications();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000650E File Offset: 0x0000470E
		private bool CanCompileNotifications()
		{
			return this.supportsNotifications && this.m_Markers.HasNotifications();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006528 File Offset: 0x00004728
		private bool CanCreateMixerRecursive()
		{
			if (this.CanCreateTrackMixer())
			{
				return true;
			}
			using (IEnumerator<TrackAsset> enumerator = this.GetChildTracks().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CanCreateMixerRecursive())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400006D RID: 109
		private const int k_LatestVersion = 3;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x0400006F RID: 111
		[Obsolete("Please use m_InfiniteClip (on AnimationTrack) instead.", false)]
		[SerializeField]
		[HideInInspector]
		[FormerlySerializedAs("m_animClip")]
		internal AnimationClip m_AnimClip;

		// Token: 0x04000070 RID: 112
		private static TrackAsset.TransientBuildData s_BuildData = TrackAsset.TransientBuildData.Create();

		// Token: 0x04000071 RID: 113
		internal const string kDefaultCurvesName = "Track Parameters";

		// Token: 0x04000074 RID: 116
		[SerializeField]
		[HideInInspector]
		private bool m_Locked;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		[HideInInspector]
		private bool m_Muted;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		[HideInInspector]
		private string m_CustomPlayableFullTypename = string.Empty;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		[HideInInspector]
		private AnimationClip m_Curves;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		[HideInInspector]
		private PlayableAsset m_Parent;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Children;

		// Token: 0x0400007A RID: 122
		[NonSerialized]
		private int m_ItemsHash;

		// Token: 0x0400007B RID: 123
		[NonSerialized]
		private TimelineClip[] m_ClipsCache;

		// Token: 0x0400007C RID: 124
		private DiscreteTime m_Start;

		// Token: 0x0400007D RID: 125
		private DiscreteTime m_End;

		// Token: 0x0400007E RID: 126
		private bool m_CacheSorted;

		// Token: 0x0400007F RID: 127
		private bool? m_SupportsNotifications;

		// Token: 0x04000080 RID: 128
		private static TrackAsset[] s_EmptyCache = new TrackAsset[0];

		// Token: 0x04000081 RID: 129
		private IEnumerable<TrackAsset> m_ChildTrackCache;

		// Token: 0x04000082 RID: 130
		private static Dictionary<Type, TrackBindingTypeAttribute> s_TrackBindingTypeAttributeCache = new Dictionary<Type, TrackBindingTypeAttribute>();

		// Token: 0x04000083 RID: 131
		[SerializeField]
		[HideInInspector]
		protected internal List<TimelineClip> m_Clips = new List<TimelineClip>();

		// Token: 0x04000084 RID: 132
		[SerializeField]
		[HideInInspector]
		private MarkerList m_Markers = new MarkerList(0);

		// Token: 0x02000067 RID: 103
		internal enum Versions
		{
			// Token: 0x04000147 RID: 327
			Initial,
			// Token: 0x04000148 RID: 328
			RotationAsEuler,
			// Token: 0x04000149 RID: 329
			RootMotionUpgrade,
			// Token: 0x0400014A RID: 330
			AnimatedTrackProperties
		}

		// Token: 0x02000068 RID: 104
		private static class TrackAssetUpgrade
		{
		}

		// Token: 0x02000069 RID: 105
		private struct TransientBuildData
		{
			// Token: 0x0600032F RID: 815 RVA: 0x0000B5AC File Offset: 0x000097AC
			public static TrackAsset.TransientBuildData Create()
			{
				return new TrackAsset.TransientBuildData
				{
					trackList = new List<TrackAsset>(20),
					clipList = new List<TimelineClip>(500),
					markerList = new List<IMarker>(100)
				};
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000B5EF File Offset: 0x000097EF
			public void Clear()
			{
				this.trackList.Clear();
				this.clipList.Clear();
				this.markerList.Clear();
			}

			// Token: 0x0400014B RID: 331
			public List<TrackAsset> trackList;

			// Token: 0x0400014C RID: 332
			public List<TimelineClip> clipList;

			// Token: 0x0400014D RID: 333
			public List<IMarker> markerList;
		}
	}
}
