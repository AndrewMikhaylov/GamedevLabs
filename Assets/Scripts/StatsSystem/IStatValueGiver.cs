using StatsSystem.Enum;

namespace StatsSystem
{
    public interface IStatValueGiver
    {
        float GetStatValue(StatsType statsType);
    }
}