using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Graphs
{
	// Token: 0x02000029 RID: 41
	public class DialogueGraph : ScriptableObject, IGraphData
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000358D File Offset: 0x0000178D
		public INodeData Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003595 File Offset: 0x00001795
		public IReadOnlyList<INodeData> Nodes
		{
			get
			{
				return this._nodes;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000359D File Offset: 0x0000179D
		public void AddNode(NodeDataBase node)
		{
			node.SetIndex(this._nodes.Count);
			node.Setup();
			if (node is NodeDialogueData)
			{
				((NodeDialogueData)node).Setup();
			}
			this._nodes.Add(node);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000035D5 File Offset: 0x000017D5
		public void Awake()
		{
			this.RestoreKeyPrefix();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000035E0 File Offset: 0x000017E0
		public void DebugDisableKeys()
		{
			foreach (NodeDataBase nodeDataBase in this._nodes)
			{
				if (nodeDataBase is NodeDialogueData)
				{
					NodeDialogueData nodeDialogueData = (NodeDialogueData)nodeDataBase;
					nodeDialogueData.prefix = "nokey";
					using (List<ChoiceData>.Enumerator enumerator2 = nodeDialogueData.choices.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ChoiceData choiceData = enumerator2.Current;
							choiceData.prefix = "nokey";
						}
						continue;
					}
				}
				if (nodeDataBase is NodeChoiceHubData)
				{
					foreach (ChoiceData choiceData2 in ((NodeChoiceHubData)nodeDataBase).choices)
					{
						choiceData2.prefix = "nokey";
					}
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000036E0 File Offset: 0x000018E0
		public void RestoreKeyPrefix()
		{
			string text = base.name;
			text = DialogueGraph.RemoveFromEnd(text, "Dialogue") + "_";
			foreach (NodeDataBase nodeDataBase in this._nodes)
			{
				if (nodeDataBase is NodeDialogueData)
				{
					NodeDialogueData nodeDialogueData = (NodeDialogueData)nodeDataBase;
					nodeDialogueData.prefix = text;
					using (List<ChoiceData>.Enumerator enumerator2 = nodeDialogueData.choices.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ChoiceData choiceData = enumerator2.Current;
							choiceData.prefix = text;
						}
						continue;
					}
				}
				if (nodeDataBase is NodeChoiceHubData)
				{
					foreach (ChoiceData choiceData2 in ((NodeChoiceHubData)nodeDataBase).choices)
					{
						choiceData2.prefix = text;
					}
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000037F0 File Offset: 0x000019F0
		public void DeleteNode(NodeDataBase node)
		{
			this._nodes.Remove(node);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003800 File Offset: 0x00001A00
		public Dictionary<string, string> GetKeys()
		{
			string text = base.name;
			text = DialogueGraph.RemoveFromEnd(text, "Dialogue");
			if (text == "")
			{
				throw new SystemException(string.Concat(new string[] { "Please rename ", base.name, " at ", text, " graph to something more expressive." }));
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (NodeDataBase nodeDataBase in this._nodes)
			{
				if (nodeDataBase is NodeDialogueData)
				{
					NodeDialogueData nodeDialogueData = (NodeDialogueData)nodeDataBase;
					if (nodeDialogueData.key == "")
					{
						throw new SystemException("Some text in the " + base.name + " graph does not have a valid key");
					}
					if (!nodeDialogueData.externalKey)
					{
						dictionary.Add(text + "_" + nodeDialogueData.key, nodeDialogueData.dialogue);
					}
					using (List<ChoiceData>.Enumerator enumerator2 = nodeDialogueData.choices.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ChoiceData choiceData = enumerator2.Current;
							if (choiceData.key == "")
							{
								throw new SystemException("Some choice text in the " + base.name + " graph does not have a valid key");
							}
							if (!choiceData.externalKey)
							{
								dictionary.Add(text + "_" + choiceData.key, choiceData.text);
							}
						}
						continue;
					}
				}
				if (nodeDataBase is NodeChoiceHubData)
				{
					foreach (ChoiceData choiceData2 in ((NodeChoiceHubData)nodeDataBase).choices)
					{
						if (choiceData2.key == "")
						{
							throw new SystemException("Some choice text in the " + base.name + " graph does not have a valid key");
						}
						if (!choiceData2.externalKey)
						{
							dictionary.Add(text + "_" + choiceData2.key, choiceData2.text);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A78 File Offset: 0x00001C78
		public void LoadKeys(Dictionary<string, string> keys)
		{
			string text = base.name;
			text = DialogueGraph.RemoveFromEnd(text, "Dialogue");
			if (text == "")
			{
				throw new SystemException("Graph name " + base.name + " is not valid");
			}
			foreach (NodeDataBase nodeDataBase in this._nodes)
			{
				if (nodeDataBase is NodeDialogueData)
				{
					NodeDialogueData nodeDialogueData = (NodeDialogueData)nodeDataBase;
					if (nodeDialogueData.key == "")
					{
						throw new SystemException("Some text in the " + base.name + " graph does not have a valid key");
					}
					if (nodeDialogueData.externalKey)
					{
						if (keys.ContainsKey(nodeDialogueData.key.ToLower().Trim()))
						{
							nodeDialogueData.dialogue = keys[nodeDialogueData.key.ToLower().Trim()];
						}
						else
						{
							Debug.LogError(nodeDialogueData.key + " is not in language file");
						}
					}
					else if (keys.ContainsKey((text + "_" + nodeDialogueData.key).ToLower().Trim()))
					{
						nodeDialogueData.dialogue = keys[(text + "_" + nodeDialogueData.key).ToLower().Trim()];
					}
					else
					{
						Debug.LogError(text + "_" + nodeDialogueData.key + " is not in language file");
					}
					using (List<ChoiceData>.Enumerator enumerator2 = nodeDialogueData.choices.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ChoiceData choiceData = enumerator2.Current;
							if (choiceData.key == "")
							{
								throw new SystemException("Some choice text in the " + base.name + " graph does not have a valid key");
							}
							if (choiceData.externalKey)
							{
								if (keys.ContainsKey(choiceData.key.ToLower().Trim()))
								{
									choiceData.text = keys[choiceData.key.ToLower().Trim()];
								}
								else
								{
									Debug.LogError(choiceData.key + " is not in language file");
								}
							}
							else if (keys.ContainsKey((text + "_" + choiceData.key).ToLower().Trim()))
							{
								choiceData.text = keys[(text + "_" + choiceData.key).ToLower().Trim()];
							}
							else
							{
								Debug.LogError(text + "_" + choiceData.key + " is not in language file");
							}
						}
						continue;
					}
				}
				if (nodeDataBase is NodeChoiceHubData)
				{
					foreach (ChoiceData choiceData2 in ((NodeChoiceHubData)nodeDataBase).choices)
					{
						if (choiceData2.key == "")
						{
							throw new SystemException("Some choice text in the " + base.name + " graph does not have a valid key");
						}
						if (choiceData2.externalKey)
						{
							if (keys.ContainsKey(choiceData2.key.ToLower().Trim()))
							{
								choiceData2.text = keys[choiceData2.key.ToLower().Trim()];
							}
							else
							{
								Debug.LogError(choiceData2.key + " is not in language file");
							}
						}
						else if (keys.ContainsKey((text + "_" + choiceData2.key).ToLower().Trim()))
						{
							choiceData2.text = keys[(text + "_" + choiceData2.key).ToLower().Trim()];
						}
						else
						{
							Debug.LogError(text + "_" + choiceData2.key + " is not in language file");
						}
					}
				}
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003EAC File Offset: 0x000020AC
		public static string RemoveFromEnd(string s, string suffix)
		{
			if (s.EndsWith(suffix))
			{
				return s.Substring(0, s.Length - suffix.Length);
			}
			return s;
		}

		// Token: 0x0400004C RID: 76
		[HideInInspector]
		[SerializeField]
		private List<NodeDataBase> _nodes = new List<NodeDataBase>();

		// Token: 0x0400004D RID: 77
		[HideInInspector]
		public NodeRootData root;

		// Token: 0x0400004E RID: 78
		[HideInInspector]
		public Vector2 scrollPosition;
	}
}
