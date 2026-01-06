using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A6 RID: 422
	public class fsSerializer
	{
		// Token: 0x06000B19 RID: 2841 RVA: 0x0002EA88 File Offset: 0x0002CC88
		public fsSerializer()
		{
			this._cachedConverterTypeInstances = new Dictionary<Type, fsBaseConverter>();
			this._cachedConverters = new Dictionary<Type, fsBaseConverter>();
			this._cachedProcessors = new Dictionary<Type, List<fsObjectProcessor>>();
			this._references = new fsCyclicReferenceManager();
			this._lazyReferenceWriter = new fsSerializer.fsLazyCycleDefinitionWriter();
			this._availableConverters = new List<fsConverter>
			{
				new fsNullableConverter
				{
					Serializer = this
				},
				new fsGuidConverter
				{
					Serializer = this
				},
				new fsTypeConverter
				{
					Serializer = this
				},
				new fsDateConverter
				{
					Serializer = this
				},
				new fsEnumConverter
				{
					Serializer = this
				},
				new fsPrimitiveConverter
				{
					Serializer = this
				},
				new fsArrayConverter
				{
					Serializer = this
				},
				new fsDictionaryConverter
				{
					Serializer = this
				},
				new fsIEnumerableConverter
				{
					Serializer = this
				},
				new fsKeyValuePairConverter
				{
					Serializer = this
				},
				new fsWeakReferenceConverter
				{
					Serializer = this
				},
				new fsReflectedConverter
				{
					Serializer = this
				}
			};
			this._availableDirectConverters = new Dictionary<Type, fsDirectConverter>();
			this._processors = new List<fsObjectProcessor>
			{
				new fsSerializationCallbackProcessor()
			};
			this._processors.Add(new fsSerializationCallbackReceiverProcessor());
			this._abstractTypeRemap = new Dictionary<Type, Type>();
			this.SetDefaultStorageType(typeof(ICollection<>), typeof(List<>));
			this.SetDefaultStorageType(typeof(IList<>), typeof(List<>));
			this.SetDefaultStorageType(typeof(IDictionary<, >), typeof(Dictionary<, >));
			this.Context = new fsContext();
			this.Config = new fsConfig();
			foreach (Type type in fsConverterRegistrar.Converters)
			{
				this.AddConverter((fsBaseConverter)Activator.CreateInstance(type));
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
		private void RemapAbstractStorageTypeToDefaultType(ref Type storageType)
		{
			if (!storageType.Resolve().IsInterface && !storageType.Resolve().IsAbstract)
			{
				return;
			}
			Type type2;
			if (storageType.Resolve().IsGenericType)
			{
				Type type;
				if (this._abstractTypeRemap.TryGetValue(storageType.Resolve().GetGenericTypeDefinition(), out type))
				{
					Type[] genericArguments = storageType.GetGenericArguments();
					storageType = type.Resolve().MakeGenericType(genericArguments);
					return;
				}
			}
			else if (this._abstractTypeRemap.TryGetValue(storageType, out type2))
			{
				storageType = type2;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002ED22 File Offset: 0x0002CF22
		public void AddProcessor(fsObjectProcessor processor)
		{
			this._processors.Add(processor);
			this._cachedProcessors = new Dictionary<Type, List<fsObjectProcessor>>();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		public void RemoveProcessor<TProcessor>()
		{
			int i = 0;
			while (i < this._processors.Count)
			{
				if (this._processors[i] is TProcessor)
				{
					this._processors.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			this._cachedProcessors = new Dictionary<Type, List<fsObjectProcessor>>();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002ED8B File Offset: 0x0002CF8B
		public void SetDefaultStorageType(Type abstractType, Type defaultStorageType)
		{
			if (!abstractType.Resolve().IsInterface && !abstractType.Resolve().IsAbstract)
			{
				throw new ArgumentException("|abstractType| must be an interface or abstract type");
			}
			this._abstractTypeRemap[abstractType] = defaultStorageType;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
		private List<fsObjectProcessor> GetProcessors(Type type)
		{
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(type);
			List<fsObjectProcessor> list;
			if (attribute != null && attribute.Processor != null)
			{
				fsObjectProcessor fsObjectProcessor = (fsObjectProcessor)Activator.CreateInstance(attribute.Processor);
				list = new List<fsObjectProcessor>();
				list.Add(fsObjectProcessor);
				this._cachedProcessors[type] = list;
			}
			else if (!this._cachedProcessors.TryGetValue(type, out list))
			{
				list = new List<fsObjectProcessor>();
				for (int i = 0; i < this._processors.Count; i++)
				{
					fsObjectProcessor fsObjectProcessor2 = this._processors[i];
					if (fsObjectProcessor2.CanProcess(type))
					{
						list.Add(fsObjectProcessor2);
					}
				}
				this._cachedProcessors[type] = list;
			}
			return list;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002EE6C File Offset: 0x0002D06C
		public void AddConverter(fsBaseConverter converter)
		{
			if (converter.Serializer != null)
			{
				throw new InvalidOperationException("Cannot add a single converter instance to multiple fsConverters -- please construct a new instance for " + ((converter != null) ? converter.ToString() : null));
			}
			if (converter is fsDirectConverter)
			{
				fsDirectConverter fsDirectConverter = (fsDirectConverter)converter;
				this._availableDirectConverters[fsDirectConverter.ModelType] = fsDirectConverter;
			}
			else
			{
				if (!(converter is fsConverter))
				{
					throw new InvalidOperationException("Unable to add converter " + ((converter != null) ? converter.ToString() : null) + "; the type association strategy is unknown. Please use either fsDirectConverter or fsConverter as your base type.");
				}
				this._availableConverters.Insert(0, (fsConverter)converter);
			}
			converter.Serializer = this;
			this._cachedConverters = new Dictionary<Type, fsBaseConverter>();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002EF14 File Offset: 0x0002D114
		private fsBaseConverter GetConverter(Type type, Type overrideConverterType)
		{
			if (overrideConverterType != null)
			{
				fsBaseConverter fsBaseConverter;
				if (!this._cachedConverterTypeInstances.TryGetValue(overrideConverterType, out fsBaseConverter))
				{
					fsBaseConverter = (fsBaseConverter)Activator.CreateInstance(overrideConverterType);
					fsBaseConverter.Serializer = this;
					this._cachedConverterTypeInstances[overrideConverterType] = fsBaseConverter;
				}
				return fsBaseConverter;
			}
			fsBaseConverter fsBaseConverter2;
			if (this._cachedConverters.TryGetValue(type, out fsBaseConverter2))
			{
				return fsBaseConverter2;
			}
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(type);
			if (attribute != null && attribute.Converter != null)
			{
				fsBaseConverter2 = (fsBaseConverter)Activator.CreateInstance(attribute.Converter);
				fsBaseConverter2.Serializer = this;
				return this._cachedConverters[type] = fsBaseConverter2;
			}
			fsForwardAttribute attribute2 = fsPortableReflection.GetAttribute<fsForwardAttribute>(type);
			if (attribute2 != null)
			{
				fsBaseConverter2 = new fsForwardConverter(attribute2);
				fsBaseConverter2.Serializer = this;
				return this._cachedConverters[type] = fsBaseConverter2;
			}
			if (!this._cachedConverters.TryGetValue(type, out fsBaseConverter2))
			{
				if (this._availableDirectConverters.ContainsKey(type))
				{
					fsBaseConverter2 = this._availableDirectConverters[type];
					return this._cachedConverters[type] = fsBaseConverter2;
				}
				for (int i = 0; i < this._availableConverters.Count; i++)
				{
					if (this._availableConverters[i].CanProcess(type))
					{
						fsBaseConverter2 = this._availableConverters[i];
						return this._cachedConverters[type] = fsBaseConverter2;
					}
				}
			}
			throw new InvalidOperationException("Internal error -- could not find a converter for " + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002F081 File Offset: 0x0002D281
		public fsResult TrySerialize<T>(T instance, out fsData data)
		{
			return this.TrySerialize(typeof(T), instance, out data);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002F09C File Offset: 0x0002D29C
		public fsResult TryDeserialize<T>(fsData data, ref T instance)
		{
			object obj = instance;
			fsResult fsResult = this.TryDeserialize(data, typeof(T), ref obj);
			if (fsResult.Succeeded)
			{
				instance = (T)((object)obj);
			}
			return fsResult;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002F0DF File Offset: 0x0002D2DF
		public fsResult TrySerialize(Type storageType, object instance, out fsData data)
		{
			return this.TrySerialize(storageType, null, instance, out data);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		public fsResult TrySerialize(Type storageType, Type overrideConverterType, object instance, out fsData data)
		{
			List<fsObjectProcessor> processors = this.GetProcessors((instance == null) ? storageType : instance.GetType());
			try
			{
				fsSerializer.Invoke_OnBeforeSerialize(processors, storageType, instance);
			}
			catch (Exception ex)
			{
				data = new fsData();
				return fsResult.Fail(ex.ToString());
			}
			if (instance == null)
			{
				data = new fsData();
				fsSerializer.Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
				return fsResult.Success;
			}
			fsResult fsResult = this.InternalSerialize_1_ProcessCycles(storageType, overrideConverterType, instance, out data);
			try
			{
				fsSerializer.Invoke_OnAfterSerialize(processors, storageType, instance, ref data);
			}
			catch (Exception ex2)
			{
				fsResult += fsResult.Fail(ex2.ToString());
			}
			return fsResult;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002F194 File Offset: 0x0002D394
		private fsResult InternalSerialize_1_ProcessCycles(Type storageType, Type overrideConverterType, object instance, out fsData data)
		{
			fsResult fsResult;
			try
			{
				this._references.Enter();
				if (!this.GetConverter(instance.GetType(), overrideConverterType).RequestCycleSupport(instance.GetType()))
				{
					fsResult = this.InternalSerialize_2_Inheritance(storageType, overrideConverterType, instance, out data);
				}
				else if (this._references.IsReference(instance))
				{
					data = fsData.CreateDictionary();
					this._lazyReferenceWriter.WriteReference(this._references.GetReferenceId(instance), data.AsDictionary);
					fsResult = fsResult.Success;
				}
				else
				{
					this._references.MarkSerialized(instance);
					fsResult fsResult2 = this.InternalSerialize_2_Inheritance(storageType, overrideConverterType, instance, out data);
					if (fsResult2.Failed)
					{
						fsResult = fsResult2;
					}
					else
					{
						this._lazyReferenceWriter.WriteDefinition(this._references.GetReferenceId(instance), data);
						fsResult = fsResult2;
					}
				}
			}
			finally
			{
				if (this._references.Exit())
				{
					this._lazyReferenceWriter.Clear();
				}
			}
			return fsResult;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002F284 File Offset: 0x0002D484
		private fsResult InternalSerialize_2_Inheritance(Type storageType, Type overrideConverterType, object instance, out fsData data)
		{
			fsResult fsResult = this.InternalSerialize_3_ProcessVersioning(overrideConverterType, instance, out data);
			if (fsResult.Failed)
			{
				return fsResult;
			}
			if (storageType != instance.GetType() && this.GetConverter(storageType, overrideConverterType).RequestInheritanceSupport(storageType))
			{
				Type type = instance.GetType();
				if (instance is Object)
				{
					Type type2 = type;
					do
					{
						type = type2;
						type2 = type2.BaseType;
					}
					while (type2 != null && type != typeof(Object) && storageType.IsAssignableFrom(type2));
				}
				fsSerializer.EnsureDictionary(data);
				data.AsDictionary[fsSerializer.Key_InstanceType] = new fsData(RuntimeCodebase.SerializeType(type));
			}
			return fsResult;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002F32C File Offset: 0x0002D52C
		private fsResult InternalSerialize_3_ProcessVersioning(Type overrideConverterType, object instance, out fsData data)
		{
			fsOption<fsVersionedType> versionedType = fsVersionManager.GetVersionedType(instance.GetType());
			if (!versionedType.HasValue)
			{
				return this.InternalSerialize_4_Converter(overrideConverterType, instance, out data);
			}
			fsVersionedType value = versionedType.Value;
			fsResult fsResult = this.InternalSerialize_4_Converter(overrideConverterType, instance, out data);
			if (fsResult.Failed)
			{
				return fsResult;
			}
			fsSerializer.EnsureDictionary(data);
			data.AsDictionary[fsSerializer.Key_Version] = new fsData(value.VersionString);
			return fsResult;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002F39C File Offset: 0x0002D59C
		private fsResult InternalSerialize_4_Converter(Type overrideConverterType, object instance, out fsData data)
		{
			Type type = instance.GetType();
			return this.GetConverter(type, overrideConverterType).TrySerialize(instance, out data, type);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002F3C0 File Offset: 0x0002D5C0
		public fsResult TryDeserialize(fsData data, Type storageType, ref object result)
		{
			return this.TryDeserialize(data, storageType, null, ref result);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002F3CC File Offset: 0x0002D5CC
		public fsResult TryDeserialize(fsData data, Type storageType, Type overrideConverterType, ref object result)
		{
			if (data.IsNull)
			{
				result = null;
				List<fsObjectProcessor> processors = this.GetProcessors(storageType);
				fsSerializer.Invoke_OnBeforeDeserialize(processors, storageType, ref data);
				fsSerializer.Invoke_OnAfterDeserialize(processors, storageType, null);
				return fsResult.Success;
			}
			fsSerializer.ConvertLegacyData(ref data);
			fsResult fsResult2;
			try
			{
				this._references.Enter();
				List<fsObjectProcessor> list;
				fsResult fsResult = this.InternalDeserialize_1_CycleReference(overrideConverterType, data, storageType, ref result, out list);
				if (fsResult.Succeeded)
				{
					try
					{
						fsSerializer.Invoke_OnAfterDeserialize(list, storageType, result);
					}
					catch (Exception ex)
					{
						fsResult += fsResult.Fail(ex.ToString());
					}
				}
				fsResult2 = fsResult;
			}
			finally
			{
				this._references.Exit();
			}
			return fsResult2;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002F47C File Offset: 0x0002D67C
		private fsResult InternalDeserialize_1_CycleReference(Type overrideConverterType, fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			if (fsSerializer.IsObjectReference(data))
			{
				int num = int.Parse(data.AsDictionary[fsSerializer.Key_ObjectReference].AsString);
				result = this._references.GetReferenceObject(num);
				processors = this.GetProcessors(result.GetType());
				return fsResult.Success;
			}
			return this.InternalDeserialize_2_Version(overrideConverterType, data, storageType, ref result, out processors);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		private fsResult InternalDeserialize_2_Version(Type overrideConverterType, fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			if (fsSerializer.IsVersioned(data))
			{
				string asString = data.AsDictionary[fsSerializer.Key_Version].AsString;
				fsOption<fsVersionedType> versionedType = fsVersionManager.GetVersionedType(storageType);
				if (versionedType.HasValue && versionedType.Value.VersionString != asString)
				{
					fsResult fsResult = fsResult.Success;
					List<fsVersionedType> list;
					fsResult += fsVersionManager.GetVersionImportPath(asString, versionedType.Value, out list);
					if (fsResult.Failed)
					{
						processors = this.GetProcessors(storageType);
						return fsResult;
					}
					fsResult += this.InternalDeserialize_3_Inheritance(overrideConverterType, data, list[0].ModelType, ref result, out processors);
					if (fsResult.Failed)
					{
						return fsResult;
					}
					for (int i = 1; i < list.Count; i++)
					{
						result = list[i].Migrate(result);
					}
					if (fsSerializer.IsObjectDefinition(data))
					{
						int num = int.Parse(data.AsDictionary[fsSerializer.Key_ObjectDefinition].AsString);
						this._references.AddReferenceWithId(num, result);
					}
					processors = this.GetProcessors(fsResult.GetType());
					return fsResult;
				}
			}
			return this.InternalDeserialize_3_Inheritance(overrideConverterType, data, storageType, ref result, out processors);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002F618 File Offset: 0x0002D818
		private fsResult InternalDeserialize_3_Inheritance(Type overrideConverterType, fsData data, Type storageType, ref object result, out List<fsObjectProcessor> processors)
		{
			fsResult fsResult = fsResult.Success;
			Type type = storageType;
			if (fsSerializer.IsTypeSpecified(data))
			{
				type = fsSerializer.GetDataType(ref data, storageType, ref fsResult);
			}
			this.RemapAbstractStorageTypeToDefaultType(ref type);
			processors = this.GetProcessors(type);
			if (fsResult.Failed)
			{
				return fsResult;
			}
			try
			{
				fsSerializer.Invoke_OnBeforeDeserialize(processors, storageType, ref data);
			}
			catch (Exception ex)
			{
				fsResult += fsResult.Fail(ex.ToString());
				return fsResult;
			}
			if (result == null || result.GetType() != type)
			{
				result = this.GetConverter(type, overrideConverterType).CreateInstance(data, type);
			}
			try
			{
				fsSerializer.Invoke_OnBeforeDeserializeAfterInstanceCreation(processors, storageType, result, ref data);
			}
			catch (Exception ex2)
			{
				fsResult += fsResult.Fail(ex2.ToString());
				return fsResult;
			}
			fsResult += this.InternalDeserialize_4_Cycles(overrideConverterType, data, type, ref result);
			return fsResult;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002F704 File Offset: 0x0002D904
		private fsResult InternalDeserialize_4_Cycles(Type overrideConverterType, fsData data, Type resultType, ref object result)
		{
			if (fsSerializer.IsObjectDefinition(data))
			{
				int num = int.Parse(data.AsDictionary[fsSerializer.Key_ObjectDefinition].AsString);
				this._references.AddReferenceWithId(num, result);
			}
			return this.InternalDeserialize_5_Converter(overrideConverterType, data, resultType, ref result);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002F74E File Offset: 0x0002D94E
		private fsResult InternalDeserialize_5_Converter(Type overrideConverterType, fsData data, Type resultType, ref object result)
		{
			if (fsSerializer.IsWrappedData(data))
			{
				data = data.AsDictionary[fsSerializer.Key_Content];
			}
			return this.GetConverter(resultType, overrideConverterType).TryDeserialize(data, ref result, resultType);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002F77C File Offset: 0x0002D97C
		private static Type GetDataType(ref fsData data, Type defaultType, ref fsResult deserializeResult)
		{
			Dictionary<string, fsData> dictionary = data.AsDictionary;
			fsData fsData = dictionary[fsSerializer.Key_InstanceType];
			if (!fsData.IsString)
			{
				string key_InstanceType = fsSerializer.Key_InstanceType;
				string text = " value must be a string (in ";
				fsData fsData2 = data;
				deserializeResult.AddMessage(key_InstanceType + text + ((fsData2 != null) ? fsData2.ToString() : null) + ")");
				return defaultType;
			}
			string asString = fsData.AsString;
			Type type;
			if (!RuntimeCodebase.TryDeserializeType(asString, out type))
			{
				if (fsSerializer.IsVisualScriptingUnit(data))
				{
					dictionary[fsSerializer.Key_UnitFormerValue] = new fsData(data.ToString());
					dictionary[fsSerializer.Key_UnitFormerType] = fsData;
					dictionary[fsSerializer.Key_InstanceType] = new fsData(fsSerializer.TypeName_MissingType);
					deserializeResult += fsResult.Warn(string.Concat(new string[]
					{
						"Type definition for '",
						asString,
						"' is missing.\nConverted '",
						asString,
						"' unit to '",
						fsSerializer.TypeName_MissingType,
						"'. Did you delete the type's script file?"
					}));
					return fsSerializer.Type_MissingType;
				}
				deserializeResult += fsResult.Warn("Unable to find type: \"" + asString + "\"");
				return defaultType;
			}
			else
			{
				if (asString == fsSerializer.TypeName_MissingType)
				{
					if (dictionary.ContainsKey(fsSerializer.Key_UnitFormerType) && fsSerializer.IsVisualScriptingUnit(data))
					{
						string asString2 = dictionary[fsSerializer.Key_UnitFormerType].AsString;
						Type type2;
						if (RuntimeCodebase.TryDeserializeType(asString2, out type2))
						{
							if (defaultType.IsAssignableFrom(type2))
							{
								if (dictionary.ContainsKey(fsSerializer.Key_UnitFormerValue))
								{
									fsData fsData3 = dictionary[fsSerializer.Key_UnitPosition];
									data = fsJsonParser.Parse(dictionary[fsSerializer.Key_UnitFormerValue].AsString);
									dictionary = data.AsDictionary;
									dictionary[fsSerializer.Key_UnitPosition] = fsData3;
									deserializeResult += fsResult.Warn(string.Concat(new string[]
									{
										"Missing unit type '",
										asString2,
										"' was found.\nConverted '",
										fsSerializer.TypeName_MissingType,
										"' unit back to '",
										asString2,
										"'"
									}));
								}
								else
								{
									dictionary[fsSerializer.Key_InstanceType] = new fsData(asString2);
									fsResult fsResult = deserializeResult;
									string[] array = new string[8];
									array[0] = "Missing unit type '";
									array[1] = asString2;
									array[2] = "' was found.\nConverted '";
									array[3] = fsSerializer.TypeName_MissingType;
									array[4] = "' unit back to '";
									array[5] = asString2;
									array[6] = "'\nNo former state can be found. Reverting node to defaults.\n";
									int num = 7;
									fsData fsData4 = data;
									array[num] = ((fsData4 != null) ? fsData4.ToString() : null);
									deserializeResult = fsResult + fsResult.Warn(string.Concat(array));
								}
								return type2;
							}
							deserializeResult += fsResult.Warn(string.Concat(new string[]
							{
								"Missing unit type '",
								asString2,
								"' was found, but is not assignable to '",
								defaultType.FullName,
								"'. Did you forget to inherit from '",
								fsSerializer.TypeName_Unit,
								"'?"
							}));
						}
						else
						{
							deserializeResult += fsResult.Warn("Type definition for '" + asString2 + "' unit is missing. Did you remove its script file?");
						}
					}
					else
					{
						deserializeResult += fsResult.Warn("Serialized '" + fsSerializer.TypeName_MissingType + "' unit has an unrecognized format.");
					}
				}
				if (defaultType.IsAssignableFrom(type))
				{
					return type;
				}
				if (fsSerializer.IsVisualScriptingUnit(data))
				{
					dictionary[fsSerializer.Key_UnitFormerType] = fsData;
					dictionary[fsSerializer.Key_InstanceType] = new fsData(fsSerializer.TypeName_MissingType);
					deserializeResult += fsResult.Warn(string.Concat(new string[]
					{
						"Type '",
						asString,
						"' is no longer assignable to '",
						defaultType.FullName,
						"'. Did you remove inheritance from '",
						fsSerializer.TypeName_Unit,
						"'?\nConverted '",
						asString,
						"' unit to '",
						fsSerializer.TypeName_MissingType,
						"'."
					}));
					return fsSerializer.Type_MissingType;
				}
				string text2 = "Ignoring type specifier; a field/property of type ";
				string text3 = ((defaultType != null) ? defaultType.ToString() : null);
				string text4 = " cannot hold an instance of ";
				Type type3 = type;
				deserializeResult.AddMessage(text2 + text3 + text4 + ((type3 != null) ? type3.ToString() : null));
				return defaultType;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002FBB4 File Offset: 0x0002DDB4
		private static void EnsureDictionary(fsData data)
		{
			if (!data.IsDictionary)
			{
				fsData fsData = data.Clone();
				data.BecomeDictionary();
				data.AsDictionary[fsSerializer.Key_Content] = fsData;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002FD03 File Offset: 0x0002DF03
		public static bool IsReservedKeyword(string key)
		{
			return fsSerializer._reservedKeywords.Contains(key);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002FD10 File Offset: 0x0002DF10
		private static bool IsObjectReference(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_ObjectReference);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002FD2C File Offset: 0x0002DF2C
		private static bool IsObjectDefinition(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_ObjectDefinition);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002FD48 File Offset: 0x0002DF48
		private static bool IsVersioned(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_Version);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002FD64 File Offset: 0x0002DF64
		private static bool IsTypeSpecified(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_InstanceType);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002FD80 File Offset: 0x0002DF80
		private static bool IsWrappedData(fsData data)
		{
			return data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_Content);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002FD9C File Offset: 0x0002DF9C
		private static bool IsVisualScriptingUnit(fsData data)
		{
			if (!data.IsDictionary)
			{
				return false;
			}
			Dictionary<string, fsData> asDictionary = data.AsDictionary;
			return asDictionary.ContainsKey(fsSerializer.Key_UnitDefault) && asDictionary.ContainsKey(fsSerializer.Key_UnitPosition) && asDictionary.ContainsKey(fsSerializer.Key_UnitGuid) && asDictionary[fsSerializer.Key_UnitPosition].AsDictionary.ContainsKey("x") && asDictionary[fsSerializer.Key_UnitPosition].AsDictionary.ContainsKey("y");
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002FE1C File Offset: 0x0002E01C
		public static void StripDeserializationMetadata(ref fsData data)
		{
			if (data.IsDictionary && data.AsDictionary.ContainsKey(fsSerializer.Key_Content))
			{
				data = data.AsDictionary[fsSerializer.Key_Content];
			}
			if (data.IsDictionary)
			{
				Dictionary<string, fsData> asDictionary = data.AsDictionary;
				asDictionary.Remove(fsSerializer.Key_ObjectReference);
				asDictionary.Remove(fsSerializer.Key_ObjectDefinition);
				asDictionary.Remove(fsSerializer.Key_InstanceType);
				asDictionary.Remove(fsSerializer.Key_Version);
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002FE98 File Offset: 0x0002E098
		private static void ConvertLegacyData(ref fsData data)
		{
			if (!data.IsDictionary)
			{
				return;
			}
			Dictionary<string, fsData> asDictionary = data.AsDictionary;
			if (asDictionary.Count > 2)
			{
				return;
			}
			string text = "ReferenceId";
			string text2 = "SourceId";
			string text3 = "Data";
			string text4 = "Type";
			string text5 = "Data";
			if (asDictionary.Count == 2 && asDictionary.ContainsKey(text4) && asDictionary.ContainsKey(text5))
			{
				data = asDictionary[text5];
				fsSerializer.EnsureDictionary(data);
				fsSerializer.ConvertLegacyData(ref data);
				data.AsDictionary[fsSerializer.Key_InstanceType] = asDictionary[text4];
				return;
			}
			if (asDictionary.Count == 2 && asDictionary.ContainsKey(text2) && asDictionary.ContainsKey(text3))
			{
				data = asDictionary[text3];
				fsSerializer.EnsureDictionary(data);
				fsSerializer.ConvertLegacyData(ref data);
				data.AsDictionary[fsSerializer.Key_ObjectDefinition] = asDictionary[text2];
				return;
			}
			if (asDictionary.Count == 1 && asDictionary.ContainsKey(text))
			{
				data = fsData.CreateDictionary();
				data.AsDictionary[fsSerializer.Key_ObjectReference] = asDictionary[text];
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002FFAC File Offset: 0x0002E1AC
		private static void Invoke_OnBeforeSerialize(List<fsObjectProcessor> processors, Type storageType, object instance)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeSerialize(storageType, instance);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
		private static void Invoke_OnAfterSerialize(List<fsObjectProcessor> processors, Type storageType, object instance, ref fsData data)
		{
			for (int i = processors.Count - 1; i >= 0; i--)
			{
				processors[i].OnAfterSerialize(storageType, instance, ref data);
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00030008 File Offset: 0x0002E208
		private static void Invoke_OnBeforeDeserialize(List<fsObjectProcessor> processors, Type storageType, ref fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserialize(storageType, ref data);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00030034 File Offset: 0x0002E234
		private static void Invoke_OnBeforeDeserializeAfterInstanceCreation(List<fsObjectProcessor> processors, Type storageType, object instance, ref fsData data)
		{
			for (int i = 0; i < processors.Count; i++)
			{
				processors[i].OnBeforeDeserializeAfterInstanceCreation(storageType, instance, ref data);
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00030064 File Offset: 0x0002E264
		private static void Invoke_OnAfterDeserialize(List<fsObjectProcessor> processors, Type storageType, object instance)
		{
			for (int i = processors.Count - 1; i >= 0; i--)
			{
				processors[i].OnAfterDeserialize(storageType, instance);
			}
		}

		// Token: 0x04000298 RID: 664
		private readonly List<fsConverter> _availableConverters;

		// Token: 0x04000299 RID: 665
		private readonly Dictionary<Type, fsDirectConverter> _availableDirectConverters;

		// Token: 0x0400029A RID: 666
		private readonly List<fsObjectProcessor> _processors;

		// Token: 0x0400029B RID: 667
		private readonly fsCyclicReferenceManager _references;

		// Token: 0x0400029C RID: 668
		private readonly fsSerializer.fsLazyCycleDefinitionWriter _lazyReferenceWriter;

		// Token: 0x0400029D RID: 669
		private readonly Dictionary<Type, Type> _abstractTypeRemap;

		// Token: 0x0400029E RID: 670
		private Dictionary<Type, fsBaseConverter> _cachedConverterTypeInstances;

		// Token: 0x0400029F RID: 671
		private Dictionary<Type, fsBaseConverter> _cachedConverters;

		// Token: 0x040002A0 RID: 672
		private Dictionary<Type, List<fsObjectProcessor>> _cachedProcessors;

		// Token: 0x040002A1 RID: 673
		public fsContext Context;

		// Token: 0x040002A2 RID: 674
		public fsConfig Config;

		// Token: 0x040002A3 RID: 675
		private static HashSet<string> _reservedKeywords = new HashSet<string>
		{
			fsSerializer.Key_ObjectReference,
			fsSerializer.Key_ObjectDefinition,
			fsSerializer.Key_InstanceType,
			fsSerializer.Key_Version,
			fsSerializer.Key_Content
		};

		// Token: 0x040002A4 RID: 676
		private static readonly string Key_ObjectReference = fsGlobalConfig.InternalFieldPrefix + "ref";

		// Token: 0x040002A5 RID: 677
		private static readonly string Key_ObjectDefinition = fsGlobalConfig.InternalFieldPrefix + "id";

		// Token: 0x040002A6 RID: 678
		private static readonly string Key_InstanceType = fsGlobalConfig.InternalFieldPrefix + "type";

		// Token: 0x040002A7 RID: 679
		private static readonly string Key_Version = fsGlobalConfig.InternalFieldPrefix + "version";

		// Token: 0x040002A8 RID: 680
		private static readonly string Key_Content = fsGlobalConfig.InternalFieldPrefix + "content";

		// Token: 0x040002A9 RID: 681
		internal static readonly string Key_UnitDefault = "defaultValues";

		// Token: 0x040002AA RID: 682
		internal static readonly string Key_UnitPosition = "position";

		// Token: 0x040002AB RID: 683
		internal static readonly string Key_UnitGuid = "guid";

		// Token: 0x040002AC RID: 684
		internal static readonly string Key_UnitFormerType = "formerType";

		// Token: 0x040002AD RID: 685
		internal static readonly string Key_UnitFormerValue = "formerValue";

		// Token: 0x040002AE RID: 686
		internal static readonly string TypeName_Unit = "Unity.VisualScripting.Unit";

		// Token: 0x040002AF RID: 687
		private static readonly Type Type_Unit = RuntimeCodebase.DeserializeType(fsSerializer.TypeName_Unit);

		// Token: 0x040002B0 RID: 688
		internal static readonly string TypeName_MissingType = "Unity.VisualScripting.MissingType";

		// Token: 0x040002B1 RID: 689
		private static readonly Type Type_MissingType = RuntimeCodebase.DeserializeType(fsSerializer.TypeName_MissingType);

		// Token: 0x0200021C RID: 540
		internal class fsLazyCycleDefinitionWriter
		{
			// Token: 0x06001315 RID: 4885 RVA: 0x000390C0 File Offset: 0x000372C0
			public void WriteDefinition(int id, fsData data)
			{
				if (this._references.Contains(id))
				{
					fsSerializer.EnsureDictionary(data);
					data.AsDictionary[fsSerializer.Key_ObjectDefinition] = new fsData(id.ToString());
					return;
				}
				this._pendingDefinitions[id] = data;
			}

			// Token: 0x06001316 RID: 4886 RVA: 0x00039100 File Offset: 0x00037300
			public void WriteReference(int id, Dictionary<string, fsData> dict)
			{
				if (this._pendingDefinitions.ContainsKey(id))
				{
					fsData fsData = this._pendingDefinitions[id];
					fsSerializer.EnsureDictionary(fsData);
					fsData.AsDictionary[fsSerializer.Key_ObjectDefinition] = new fsData(id.ToString());
					this._pendingDefinitions.Remove(id);
				}
				else
				{
					this._references.Add(id);
				}
				dict[fsSerializer.Key_ObjectReference] = new fsData(id.ToString());
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x0003917B File Offset: 0x0003737B
			public void Clear()
			{
				this._pendingDefinitions.Clear();
				this._references.Clear();
			}

			// Token: 0x040009D8 RID: 2520
			private Dictionary<int, fsData> _pendingDefinitions = new Dictionary<int, fsData>();

			// Token: 0x040009D9 RID: 2521
			private HashSet<int> _references = new HashSet<int>();
		}
	}
}
