namespace Asset.Models
{
    public static class HistoryActionHelper
    {
        // Action Type IDs - Match these with your database ActionTypeId values
        public const int MAINTENANCE = 1;
        public const int USER_CHANGE = 2;
        public const int SPARE_PART = 3;
        public const int ASSIGNMENT = 4;
        public const int HARDWARE_ADDITION = 5;
        public const int HARDWARE_REMOVAL = 6;
        public const int RETURN_TO_IT = 7;
        public const int RETIRED = 8;
        public const int TRANSFER = 9;
        public const int REPAIR = 10;
        public const int UPGRADE = 11;
        public const int DEPLOYMENT = 12;
    }
}