using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000137 RID: 311
	public static class Serialization
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0002592A File Offset: 0x00023B2A
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00025931 File Offset: 0x00023B31
		public static bool isUnitySerializing { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00025939 File Offset: 0x00023B39
		public static bool isCustomSerializing
		{
			get
			{
				return Serialization.busyOperations.Count > 0;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00025948 File Offset: 0x00023B48
		public static bool isSerializing
		{
			get
			{
				return Serialization.isUnitySerializing || Serialization.isCustomSerializing;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00025958 File Offset: 0x00023B58
		private static SerializationOperation StartOperation()
		{
			object obj = Serialization.@lock;
			SerializationOperation serializationOperation2;
			lock (obj)
			{
				if (Serialization.freeOperations.Count == 0)
				{
					Serialization.freeOperations.Add(new SerializationOperation());
				}
				SerializationOperation serializationOperation = Serialization.freeOperations.First<SerializationOperation>();
				Serialization.freeOperations.Remove(serializationOperation);
				Serialization.busyOperations.Add(serializationOperation);
				serializationOperation2 = serializationOperation;
			}
			return serializationOperation2;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000259D4 File Offset: 0x00023BD4
		private static void EndOperation(SerializationOperation operation)
		{
			object obj = Serialization.@lock;
			lock (obj)
			{
				if (!Serialization.busyOperations.Contains(operation))
				{
					throw new InvalidOperationException("Trying to finish an operation that isn't started.");
				}
				operation.Reset();
				Serialization.busyOperations.Remove(operation);
				Serialization.freeOperations.Add(operation);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00025A44 File Offset: 0x00023C44
		public static T CloneViaSerialization<T>(this T value, bool forceReflected = false)
		{
			return (T)((object)value.Serialize(forceReflected).Deserialize(forceReflected));
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00025A60 File Offset: 0x00023C60
		public static void CloneViaSerializationInto<TSource, TDestination>(this TSource value, ref TDestination instance, bool forceReflected = false) where TDestination : TSource
		{
			object obj = instance;
			value.Serialize(forceReflected).DeserializeInto(ref obj, forceReflected);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00025A90 File Offset: 0x00023C90
		public static SerializationData Serialize(this object value, bool forceReflected = false)
		{
			SerializationOperation serializationOperation = Serialization.StartOperation();
			SerializationData serializationData;
			try
			{
				string text = Serialization.SerializeJson(serializationOperation.serializer, value, forceReflected);
				Object[] array = serializationOperation.objectReferences.ToArray();
				serializationData = new SerializationData(text, array);
			}
			catch (Exception ex)
			{
				throw new SerializationException("Serialization of '" + (((value != null) ? value.GetType().ToString() : null) ?? "null") + "' failed.", ex);
			}
			finally
			{
				Serialization.EndOperation(serializationOperation);
			}
			return serializationData;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00025B20 File Offset: 0x00023D20
		public static void DeserializeInto(this SerializationData data, ref object instance, bool forceReflected = false)
		{
			try
			{
				if (string.IsNullOrEmpty(data.json))
				{
					instance = null;
				}
				else
				{
					SerializationOperation serializationOperation = Serialization.StartOperation();
					try
					{
						serializationOperation.objectReferences.AddRange(data.objectReferences);
						Serialization.DeserializeJson(serializationOperation.serializer, data.json, ref instance, forceReflected);
					}
					finally
					{
						Serialization.EndOperation(serializationOperation);
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					Debug.LogWarning(data.ToString("Deserialization Failure Data"), instance as Object);
				}
				catch (Exception ex2)
				{
					string text = "Failed to log deserialization failure data:\n";
					Exception ex3 = ex2;
					Debug.LogWarning(text + ((ex3 != null) ? ex3.ToString() : null), instance as Object);
				}
				string text2 = "Deserialization into '";
				object obj = instance;
				throw new SerializationException(text2 + (((obj != null) ? obj.GetType().ToString() : null) ?? "null") + "' failed.", ex);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00025C18 File Offset: 0x00023E18
		public static object Deserialize(this SerializationData data, bool forceReflected = false)
		{
			object obj = null;
			data.DeserializeInto(ref obj, forceReflected);
			return obj;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00025C34 File Offset: 0x00023E34
		private static string SerializeJson(fsSerializer serializer, object instance, bool forceReflected)
		{
			string text;
			using (ProfilingUtility.SampleBlock("SerializeJson"))
			{
				fsData fsData;
				fsResult fsResult;
				if (forceReflected)
				{
					fsResult = serializer.TrySerialize(instance.GetType(), typeof(fsReflectedConverter), instance, out fsData);
				}
				else
				{
					fsResult = serializer.TrySerialize<object>(instance, out fsData);
				}
				Serialization.HandleResult("Serialization", fsResult, instance as Object);
				text = fsJsonPrinter.CompressedJson(fsData);
			}
			return text;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00025CB0 File Offset: 0x00023EB0
		private static fsResult DeserializeJsonUtil(fsSerializer serializer, string json, ref object instance, bool forceReflected)
		{
			fsData fsData = fsJsonParser.Parse(json);
			fsResult fsResult;
			if (forceReflected)
			{
				fsResult = serializer.TryDeserialize(fsData, instance.GetType(), typeof(fsReflectedConverter), ref instance);
			}
			else
			{
				fsResult = serializer.TryDeserialize<object>(fsData, ref instance);
			}
			return fsResult;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00025CF0 File Offset: 0x00023EF0
		private static void DeserializeJson(fsSerializer serializer, string json, ref object instance, bool forceReflected)
		{
			using (ProfilingUtility.SampleBlock("DeserializeJson"))
			{
				fsResult fsResult = Serialization.DeserializeJsonUtil(serializer, json, ref instance, forceReflected);
				Serialization.HandleResult("Deserialization", fsResult, instance as Object);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00025D44 File Offset: 0x00023F44
		private static void HandleResult(string label, fsResult result, Object context = null)
		{
			result.AssertSuccess();
			if (result.HasWarnings)
			{
				foreach (string text in result.RawMessages)
				{
					Debug.LogWarning(string.Concat(new string[] { "[", label, "] ", text, "\n" }), context);
				}
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00025DD0 File Offset: 0x00023FD0
		public static string PrettyPrint(string json)
		{
			return fsJsonPrinter.PrettyJson(fsJsonParser.Parse(json));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00025DDD File Offset: 0x00023FDD
		public static void AwaitDependencies(ISerializationDepender depender)
		{
			Serialization.awaitingDependers.Add(depender);
			Serialization.CheckIfDependenciesMet(depender);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00025DF1 File Offset: 0x00023FF1
		public static void NotifyDependencyDeserializing(ISerializationDependency dependency)
		{
			Serialization.NotifyDependencyUnavailable(dependency);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00025DF9 File Offset: 0x00023FF9
		public static void NotifyDependencyDeserialized(ISerializationDependency dependency)
		{
			Serialization.NotifyDependencyAvailable(dependency);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00025E01 File Offset: 0x00024001
		public static void NotifyDependencyUnavailable(ISerializationDependency dependency)
		{
			dependency.IsDeserialized = false;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00025E0C File Offset: 0x0002400C
		public static void NotifyDependencyAvailable(ISerializationDependency dependency)
		{
			dependency.IsDeserialized = true;
			foreach (ISerializationDepender serializationDepender in Serialization.awaitingDependers.ToArray<ISerializationDepender>())
			{
				if (Serialization.awaitingDependers.Contains(serializationDepender))
				{
					Serialization.CheckIfDependenciesMet(serializationDepender);
				}
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00025E50 File Offset: 0x00024050
		private static void CheckIfDependenciesMet(ISerializationDepender depender)
		{
			bool flag = true;
			using (IEnumerator<ISerializationDependency> enumerator = depender.deserializationDependencies.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsDeserialized)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				Serialization.awaitingDependers.Remove(depender);
				depender.OnAfterDependenciesDeserialized();
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00025EB8 File Offset: 0x000240B8
		public static void LogStuckDependers()
		{
			if (Serialization.awaitingDependers.Any<ISerializationDepender>())
			{
				string text = Serialization.awaitingDependers.Count.ToString() + " awaiting dependers: \n";
				foreach (ISerializationDepender serializationDepender in Serialization.awaitingDependers)
				{
					HashSet<object> hashSet = new HashSet<object>();
					foreach (ISerializationDependency serializationDependency in serializationDepender.deserializationDependencies)
					{
						if (!serializationDependency.IsDeserialized)
						{
							hashSet.Add(serializationDependency);
							break;
						}
					}
					text += string.Format("{0} is missing {1}\n", serializationDepender, hashSet.ToCommaSeparatedString());
				}
				Debug.LogWarning(text);
				return;
			}
			Debug.Log("No stuck awaiting depender.");
		}

		// Token: 0x04000205 RID: 517
		public const string ConstructorWarning = "This parameterless constructor is only made public for serialization. Use another constructor instead.";

		// Token: 0x04000206 RID: 518
		private static readonly HashSet<SerializationOperation> freeOperations = new HashSet<SerializationOperation>();

		// Token: 0x04000207 RID: 519
		private static readonly HashSet<SerializationOperation> busyOperations = new HashSet<SerializationOperation>();

		// Token: 0x04000208 RID: 520
		private static readonly object @lock = new object();

		// Token: 0x0400020A RID: 522
		private static readonly HashSet<ISerializationDepender> awaitingDependers = new HashSet<ISerializationDepender>();
	}
}
