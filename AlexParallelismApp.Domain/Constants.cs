namespace AlexParallelismApp.Domain;

public static class Constants
{
    public static class ErrorMessages
    {
        public const string ObjectDeleted = "That element no longer exists!";

        public const string PessimisticVersionConflict = @"There was a conflict while updating data.
                               This entity is locked by the user : {0}. Try again later.";

        public const string OptimisticVersionConflict = @"There was a conflicting version of the data.
                               Your version : 
                               Name : {0}
                               Description : {1}
                               Last update time : {2}

                               Version in database : 
                               Name : {3}
                               Description : {4}
                               Last update time : {5}";
    }
}