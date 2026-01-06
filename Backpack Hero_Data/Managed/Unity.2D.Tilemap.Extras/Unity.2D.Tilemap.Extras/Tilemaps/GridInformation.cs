using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000009 RID: 9
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/GridInformation.html")]
	[AddComponentMenu("Tilemap/Grid Information")]
	[Serializable]
	public class GridInformation : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000032F4 File Offset: 0x000014F4
		internal Dictionary<GridInformation.GridInformationKey, GridInformation.GridInformationValue> PositionProperties
		{
			get
			{
				return this.m_PositionProperties;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032FC File Offset: 0x000014FC
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (base.GetComponentInParent<Grid>() == null)
			{
				return;
			}
			this.m_PositionIntKeys.Clear();
			this.m_PositionIntValues.Clear();
			this.m_PositionStringKeys.Clear();
			this.m_PositionStringValues.Clear();
			this.m_PositionFloatKeys.Clear();
			this.m_PositionFloatValues.Clear();
			this.m_PositionDoubleKeys.Clear();
			this.m_PositionDoubleValues.Clear();
			this.m_PositionObjectKeys.Clear();
			this.m_PositionObjectValues.Clear();
			this.m_PositionColorKeys.Clear();
			this.m_PositionColorValues.Clear();
			foreach (KeyValuePair<GridInformation.GridInformationKey, GridInformation.GridInformationValue> keyValuePair in this.m_PositionProperties)
			{
				switch (keyValuePair.Value.type)
				{
				case GridInformationType.Integer:
					this.m_PositionIntKeys.Add(keyValuePair.Key);
					this.m_PositionIntValues.Add((int)keyValuePair.Value.data);
					continue;
				case GridInformationType.String:
					this.m_PositionStringKeys.Add(keyValuePair.Key);
					this.m_PositionStringValues.Add(keyValuePair.Value.data as string);
					continue;
				case GridInformationType.Float:
					this.m_PositionFloatKeys.Add(keyValuePair.Key);
					this.m_PositionFloatValues.Add((float)keyValuePair.Value.data);
					continue;
				case GridInformationType.Double:
					this.m_PositionDoubleKeys.Add(keyValuePair.Key);
					this.m_PositionDoubleValues.Add((double)keyValuePair.Value.data);
					continue;
				case GridInformationType.Color:
					this.m_PositionColorKeys.Add(keyValuePair.Key);
					this.m_PositionColorValues.Add((Color)keyValuePair.Value.data);
					continue;
				}
				this.m_PositionObjectKeys.Add(keyValuePair.Key);
				this.m_PositionObjectValues.Add(keyValuePair.Value.data as Object);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003544 File Offset: 0x00001744
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_PositionProperties.Clear();
			for (int num = 0; num != Math.Min(this.m_PositionIntKeys.Count, this.m_PositionIntValues.Count); num++)
			{
				GridInformation.GridInformationValue gridInformationValue;
				gridInformationValue.type = GridInformationType.Integer;
				gridInformationValue.data = this.m_PositionIntValues[num];
				this.m_PositionProperties.Add(this.m_PositionIntKeys[num], gridInformationValue);
			}
			for (int num2 = 0; num2 != Math.Min(this.m_PositionStringKeys.Count, this.m_PositionStringValues.Count); num2++)
			{
				GridInformation.GridInformationValue gridInformationValue2;
				gridInformationValue2.type = GridInformationType.String;
				gridInformationValue2.data = this.m_PositionStringValues[num2];
				this.m_PositionProperties.Add(this.m_PositionStringKeys[num2], gridInformationValue2);
			}
			for (int num3 = 0; num3 != Math.Min(this.m_PositionFloatKeys.Count, this.m_PositionFloatValues.Count); num3++)
			{
				GridInformation.GridInformationValue gridInformationValue3;
				gridInformationValue3.type = GridInformationType.Float;
				gridInformationValue3.data = this.m_PositionFloatValues[num3];
				this.m_PositionProperties.Add(this.m_PositionFloatKeys[num3], gridInformationValue3);
			}
			for (int num4 = 0; num4 != Math.Min(this.m_PositionDoubleKeys.Count, this.m_PositionDoubleValues.Count); num4++)
			{
				GridInformation.GridInformationValue gridInformationValue4;
				gridInformationValue4.type = GridInformationType.Double;
				gridInformationValue4.data = this.m_PositionDoubleValues[num4];
				this.m_PositionProperties.Add(this.m_PositionDoubleKeys[num4], gridInformationValue4);
			}
			for (int num5 = 0; num5 != Math.Min(this.m_PositionObjectKeys.Count, this.m_PositionObjectValues.Count); num5++)
			{
				GridInformation.GridInformationValue gridInformationValue5;
				gridInformationValue5.type = GridInformationType.UnityObject;
				gridInformationValue5.data = this.m_PositionObjectValues[num5];
				this.m_PositionProperties.Add(this.m_PositionObjectKeys[num5], gridInformationValue5);
			}
			for (int num6 = 0; num6 != Math.Min(this.m_PositionColorKeys.Count, this.m_PositionColorValues.Count); num6++)
			{
				GridInformation.GridInformationValue gridInformationValue6;
				gridInformationValue6.type = GridInformationType.Color;
				gridInformationValue6.data = this.m_PositionColorValues[num6];
				this.m_PositionProperties.Add(this.m_PositionColorKeys[num6], gridInformationValue6);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000037A2 File Offset: 0x000019A2
		public bool SetPositionProperty<T>(Vector3Int position, string name, T positionProperty)
		{
			throw new NotImplementedException("Storing this type is not accepted in GridInformation");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000037AE File Offset: 0x000019AE
		public bool SetPositionProperty(Vector3Int position, string name, int positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.Integer, positionProperty);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000037BF File Offset: 0x000019BF
		public bool SetPositionProperty(Vector3Int position, string name, string positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.String, positionProperty);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000037CB File Offset: 0x000019CB
		public bool SetPositionProperty(Vector3Int position, string name, float positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.Float, positionProperty);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000037DC File Offset: 0x000019DC
		public bool SetPositionProperty(Vector3Int position, string name, double positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.Double, positionProperty);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000037ED File Offset: 0x000019ED
		public bool SetPositionProperty(Vector3Int position, string name, Object positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.UnityObject, positionProperty);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000037F9 File Offset: 0x000019F9
		public bool SetPositionProperty(Vector3Int position, string name, Color positionProperty)
		{
			return this.SetPositionProperty(position, name, GridInformationType.Color, positionProperty);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000380C File Offset: 0x00001A0C
		private bool SetPositionProperty(Vector3Int position, string name, GridInformationType dataType, object positionProperty)
		{
			if (base.GetComponentInParent<Grid>() != null && positionProperty != null)
			{
				GridInformation.GridInformationKey gridInformationKey;
				gridInformationKey.position = position;
				gridInformationKey.name = name;
				GridInformation.GridInformationValue gridInformationValue;
				gridInformationValue.type = dataType;
				gridInformationValue.data = positionProperty;
				this.m_PositionProperties[gridInformationKey] = gridInformationValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000385C File Offset: 0x00001A5C
		public T GetPositionProperty<T>(Vector3Int position, string name, T defaultValue) where T : Object
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.UnityObject)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return gridInformationValue.data as T;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000038B0 File Offset: 0x00001AB0
		public int GetPositionProperty(Vector3Int position, string name, int defaultValue)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.Integer)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return (int)gridInformationValue.data;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003900 File Offset: 0x00001B00
		public string GetPositionProperty(Vector3Int position, string name, string defaultValue)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.String)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return (string)gridInformationValue.data;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003950 File Offset: 0x00001B50
		public float GetPositionProperty(Vector3Int position, string name, float defaultValue)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.Float)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return (float)gridInformationValue.data;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000039A0 File Offset: 0x00001BA0
		public double GetPositionProperty(Vector3Int position, string name, double defaultValue)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.Double)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return (double)gridInformationValue.data;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000039F0 File Offset: 0x00001BF0
		public Color GetPositionProperty(Vector3Int position, string name, Color defaultValue)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			GridInformation.GridInformationValue gridInformationValue;
			if (!this.m_PositionProperties.TryGetValue(gridInformationKey, out gridInformationValue))
			{
				return defaultValue;
			}
			if (gridInformationValue.type != GridInformationType.Color)
			{
				throw new InvalidCastException("Value stored in GridInformation is not of the right type");
			}
			return (Color)gridInformationValue.data;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003A40 File Offset: 0x00001C40
		public bool ErasePositionProperty(Vector3Int position, string name)
		{
			GridInformation.GridInformationKey gridInformationKey;
			gridInformationKey.position = position;
			gridInformationKey.name = name;
			return this.m_PositionProperties.Remove(gridInformationKey);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003A69 File Offset: 0x00001C69
		public virtual void Reset()
		{
			this.m_PositionProperties.Clear();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003A78 File Offset: 0x00001C78
		public Vector3Int[] GetAllPositions(string propertyName)
		{
			return (from x in this.m_PositionProperties.Keys.ToList<GridInformation.GridInformationKey>().FindAll((GridInformation.GridInformationKey x) => x.name == propertyName)
				select x.position).ToArray<Vector3Int>();
		}

		// Token: 0x04000015 RID: 21
		private Dictionary<GridInformation.GridInformationKey, GridInformation.GridInformationValue> m_PositionProperties = new Dictionary<GridInformation.GridInformationKey, GridInformation.GridInformationValue>();

		// Token: 0x04000016 RID: 22
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionIntKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x04000017 RID: 23
		[SerializeField]
		[HideInInspector]
		private List<int> m_PositionIntValues = new List<int>();

		// Token: 0x04000018 RID: 24
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionStringKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x04000019 RID: 25
		[SerializeField]
		[HideInInspector]
		private List<string> m_PositionStringValues = new List<string>();

		// Token: 0x0400001A RID: 26
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionFloatKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x0400001B RID: 27
		[SerializeField]
		[HideInInspector]
		private List<float> m_PositionFloatValues = new List<float>();

		// Token: 0x0400001C RID: 28
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionDoubleKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x0400001D RID: 29
		[SerializeField]
		[HideInInspector]
		private List<double> m_PositionDoubleValues = new List<double>();

		// Token: 0x0400001E RID: 30
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionObjectKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x0400001F RID: 31
		[SerializeField]
		[HideInInspector]
		private List<Object> m_PositionObjectValues = new List<Object>();

		// Token: 0x04000020 RID: 32
		[SerializeField]
		[HideInInspector]
		private List<GridInformation.GridInformationKey> m_PositionColorKeys = new List<GridInformation.GridInformationKey>();

		// Token: 0x04000021 RID: 33
		[SerializeField]
		[HideInInspector]
		private List<Color> m_PositionColorValues = new List<Color>();

		// Token: 0x02000012 RID: 18
		internal struct GridInformationValue
		{
			// Token: 0x04000041 RID: 65
			public GridInformationType type;

			// Token: 0x04000042 RID: 66
			public object data;
		}

		// Token: 0x02000013 RID: 19
		internal struct GridInformationKey : IEquatable<GridInformation.GridInformationKey>
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00004BE2 File Offset: 0x00002DE2
			public bool Equals(GridInformation.GridInformationKey key)
			{
				return this.position == key.position && this.name == key.name;
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00004C0A File Offset: 0x00002E0A
			public override int GetHashCode()
			{
				return (this.position.GetHashCode() * 317) ^ this.name.GetHashCode();
			}

			// Token: 0x04000043 RID: 67
			public Vector3Int position;

			// Token: 0x04000044 RID: 68
			public string name;
		}
	}
}
