using System;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField] public StatsType Type { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public Stat(StatsType statsType, float value)
        {
            Type = statsType;
            Value = value;
        }

        public void SetStatValue(float value)
        {
            Value = value;
        }

        public Stat GetCopy() => new Stat(Type, Value);

        public static implicit operator float(Stat stat)
        {
            return stat == null ? 0 : stat.Value;
        }
    }
}