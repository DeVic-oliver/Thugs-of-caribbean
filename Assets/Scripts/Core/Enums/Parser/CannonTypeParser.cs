namespace Assets.Scripts.Core.Enums.Parser
{
    public static class CannonTypeParser
    {
           
        public static bool IsSingleCannon(CannonTypes type)
        {
            return (CannonTypes.Single == type);
        }

        public static bool IsMultipleCannon(CannonTypes type)
        {
            return (CannonTypes.Multiple == type);
        }

    }
}