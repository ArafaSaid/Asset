namespace Asset.Models
{
    public static class HistoryActionHelper
    {
        // Action Type Constants
        public const int MAINTENANCE = 1;
        public const int USER_CHANGE = 2;
        public const int SPARE_PART_REPLACEMENT = 3;
        public const int ASSIGNMENT = 4;
        public const int HARDWARE_ADDITION = 5;
        public const int HARDWARE_REMOVAL = 6;
        public const int RETURN_TO_IT = 7;
        public const int RETIRED = 8;
        public const int TRANSFER = 9;
        public const int REPAIR = 10;
        public const int UPGRADE = 11;
        public const int DEPLOYMENT = 12;

        // Field usage guide per action type
        public static Dictionary<int, string[]> GetRequiredFields(int actionTypeId)
        {
            var fieldMapping = new Dictionary<int, string[]>
            {
                { USER_CHANGE, new[] { "AssignedToUser", "Description", "PerformedBy" } },
                { HARDWARE_ADDITION, new[] { "SparePart", "Description", "PerformedBy" } },
                { HARDWARE_REMOVAL, new[] { "SparePart", "Description", "PerformedBy" } },
                { RETURN_TO_IT, new[] { "Description", "PerformedBy", "AssignedToUser" } },
                { RETIRED, new[] { "Description", "PerformedBy" } },
                { SPARE_PART_REPLACEMENT, new[] { "SparePart", "Description", "PerformedBy" } },
                { MAINTENANCE, new[] { "Description", "PerformedBy" } },
                { TRANSFER, new[] { "Description", "PerformedBy", "AssignedToUser" } },
                { REPAIR, new[] { "Description", "PerformedBy", "SparePart" } },
                { UPGRADE, new[] { "SparePart", "Description", "PerformedBy" } },
                { DEPLOYMENT, new[] { "AssignedToUser", "Description", "PerformedBy" } }
            };

            return fieldMapping.ContainsKey(actionTypeId) 
                ? new Dictionary<int, string[]> { { actionTypeId, fieldMapping[actionTypeId] } }
                : new Dictionary<int, string[]>();
        }
    }
}