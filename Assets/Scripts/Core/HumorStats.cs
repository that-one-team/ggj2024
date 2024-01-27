// author: @zsfer
using System.Collections.Generic;
using System.Configuration.Assemblies;

[System.Serializable]
public class HumorStats
{
    public float Dark;
    public float Aggressive;
    public float Slapstick;
    public float Satire;
    public float Ironic;

    public bool Add(HumorStats stats)
    {
        Dark += stats.Dark;
        Aggressive += stats.Aggressive;
        Slapstick += stats.Slapstick;
        Satire += stats.Satire;
        Ironic += stats.Ironic;

        return GetMaxStat(stats).StatName == GetMaxStat(this).StatName;
    }

    public float[] GetStatsInOrder()
    {
        List<float> finalVal = new()
        {
            Dark,
            Aggressive,
            Slapstick,
            Satire,
            Ironic,
        };

        return finalVal.ToArray();
    }

    public static (string StatName, float MaxValue) GetMaxStat(HumorStats stats)
    {
        float maxValue = float.MinValue;
        string maxStatName = "";

        if (stats.Dark > maxValue)
        {
            maxValue = stats.Dark;
            maxStatName = "Dark";
        }

        if (stats.Aggressive > maxValue)
        {
            maxValue = stats.Aggressive;
            maxStatName = "Aggressive";
        }

        if (stats.Slapstick > maxValue)
        {
            maxValue = stats.Slapstick;
            maxStatName = "Slapstick";
        }

        if (stats.Satire > maxValue)
        {
            maxValue = stats.Satire;
            maxStatName = "Satire";
        }

        if (stats.Ironic > maxValue)
        {
            maxValue = stats.Ironic;
            maxStatName = "Ironic";
        }

        return (maxStatName, maxValue);
    }

    public override string ToString()
    {
        return $"{Dark}|{Aggressive}|{Slapstick}|{Satire}|{Ironic}";
    }
}