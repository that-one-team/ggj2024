using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string humor;
    public string[] lines;
}

[CreateAssetMenu(fileName = "NPC Dialogue Data", menuName = "Dialogue Data", order = 0)]
public class NPCDialogueData : ScriptableObject
{
    public List<DialogueLine> Lines;

    public string[] GetMaxStatLines(HumorStats stats)
    {
        return Lines.Where((l) => l.humor == HumorStats.GetMaxStat(stats).StatName).SelectMany((l) => l.lines).ToArray();
    }
}