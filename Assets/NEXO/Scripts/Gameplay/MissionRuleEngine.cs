using Nexo.Progression;

namespace Nexo.Gameplay
{
    public static class MissionRuleEngine
    {
        public const int MaxWater = 6;
        public const int AllocationCost = 2;
        public const int ReservoirValveIndex = 1;

        public static bool CanAllocate(int water, WaterAllocation allocation)
        {
            return allocation != WaterAllocation.None && water >= AllocationCost;
        }

        public static int ApplyAllocation(int water, WaterAllocation allocation)
        {
            return CanAllocate(water, allocation) ? water - AllocationCost : water;
        }

        public static bool IsValveAligned(int valveIndex)
        {
            return valveIndex == ReservoirValveIndex;
        }

        public static bool CanCompleteReservoir(int water, WaterAllocation allocation, int valveIndex)
        {
            return allocation == WaterAllocation.Reservoir &&
                   water >= 0 && IsValveAligned(valveIndex);
        }
    }
}
