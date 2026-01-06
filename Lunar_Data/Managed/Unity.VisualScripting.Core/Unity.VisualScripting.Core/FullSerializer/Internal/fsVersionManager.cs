using System;
using System.Collections.Generic;
using System.Reflection;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001B1 RID: 433
	public static class fsVersionManager
	{
		// Token: 0x06000B93 RID: 2963 RVA: 0x00031284 File Offset: 0x0002F484
		public static fsResult GetVersionImportPath(string currentVersion, fsVersionedType targetVersion, out List<fsVersionedType> path)
		{
			path = new List<fsVersionedType>();
			if (!fsVersionManager.GetVersionImportPathRecursive(path, currentVersion, targetVersion))
			{
				return fsResult.Fail(string.Concat(new string[] { "There is no migration path from \"", currentVersion, "\" to \"", targetVersion.VersionString, "\"" }));
			}
			path.Add(targetVersion);
			return fsResult.Success;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000312E8 File Offset: 0x0002F4E8
		private static bool GetVersionImportPathRecursive(List<fsVersionedType> path, string currentVersion, fsVersionedType current)
		{
			for (int i = 0; i < current.Ancestors.Length; i++)
			{
				fsVersionedType fsVersionedType = current.Ancestors[i];
				if (fsVersionedType.VersionString == currentVersion || fsVersionManager.GetVersionImportPathRecursive(path, currentVersion, fsVersionedType))
				{
					path.Add(fsVersionedType);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00031338 File Offset: 0x0002F538
		public static fsOption<fsVersionedType> GetVersionedType(Type type)
		{
			fsOption<fsVersionedType> fsOption;
			if (!fsVersionManager._cache.TryGetValue(type, out fsOption))
			{
				fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(type);
				if (attribute != null && (!string.IsNullOrEmpty(attribute.VersionString) || attribute.PreviousModels != null))
				{
					if (attribute.PreviousModels != null && string.IsNullOrEmpty(attribute.VersionString))
					{
						throw new Exception("fsObject attribute on " + ((type != null) ? type.ToString() : null) + " contains a PreviousModels specifier - it must also include a VersionString modifier");
					}
					fsVersionedType[] array = new fsVersionedType[(attribute.PreviousModels != null) ? attribute.PreviousModels.Length : 0];
					for (int i = 0; i < array.Length; i++)
					{
						fsOption<fsVersionedType> versionedType = fsVersionManager.GetVersionedType(attribute.PreviousModels[i]);
						if (versionedType.IsEmpty)
						{
							throw new Exception("Unable to create versioned type for ancestor " + versionedType.ToString() + "; please add an [fsObject(VersionString=\"...\")] attribute");
						}
						array[i] = versionedType.Value;
					}
					fsVersionedType fsVersionedType = new fsVersionedType
					{
						Ancestors = array,
						VersionString = attribute.VersionString,
						ModelType = type
					};
					fsVersionManager.VerifyUniqueVersionStrings(fsVersionedType);
					fsVersionManager.VerifyConstructors(fsVersionedType);
					fsOption = fsOption.Just<fsVersionedType>(fsVersionedType);
				}
				fsVersionManager._cache[type] = fsOption;
			}
			return fsOption;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00031468 File Offset: 0x0002F668
		private static void VerifyConstructors(fsVersionedType type)
		{
			ConstructorInfo[] declaredConstructors = type.ModelType.GetDeclaredConstructors();
			for (int i = 0; i < type.Ancestors.Length; i++)
			{
				Type modelType = type.Ancestors[i].ModelType;
				bool flag = false;
				for (int j = 0; j < declaredConstructors.Length; j++)
				{
					ParameterInfo[] parameters = declaredConstructors[j].GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType == modelType)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new fsMissingVersionConstructorException(type.ModelType, modelType);
				}
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000314F4 File Offset: 0x0002F6F4
		private static void VerifyUniqueVersionStrings(fsVersionedType type)
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			Queue<fsVersionedType> queue = new Queue<fsVersionedType>();
			queue.Enqueue(type);
			while (queue.Count > 0)
			{
				fsVersionedType fsVersionedType = queue.Dequeue();
				if (dictionary.ContainsKey(fsVersionedType.VersionString) && dictionary[fsVersionedType.VersionString] != fsVersionedType.ModelType)
				{
					throw new fsDuplicateVersionNameException(dictionary[fsVersionedType.VersionString], fsVersionedType.ModelType, fsVersionedType.VersionString);
				}
				dictionary[fsVersionedType.VersionString] = fsVersionedType.ModelType;
				foreach (fsVersionedType fsVersionedType2 in fsVersionedType.Ancestors)
				{
					queue.Enqueue(fsVersionedType2);
				}
			}
		}

		// Token: 0x040002CE RID: 718
		private static readonly Dictionary<Type, fsOption<fsVersionedType>> _cache = new Dictionary<Type, fsOption<fsVersionedType>>();
	}
}
