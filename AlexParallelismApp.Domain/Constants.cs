namespace AlexParallelismApp.Domain;

public static class Constants
{
    public static class ErrorMessages
    {
        public const string ObjectDeleted = "That element no longer exists!";

        public const string PessimisticVersionConflict = "There was a conflict while updating data. \n" +
                                                         "This entity is locked by the user : {0}. \n" +
                                                         "Try again later.";

        public const string OptimisticVersionConflict = "There was a conflicting version of the data. \n" +
                                                        "Your version : \n" +
                                                        "Name : {0} \n" +
                                                        "Description : {1} \n" +
                                                        "Last update time : {2} \n" +
                                                        "\n" +
                                                        "Version in database : \n" +
                                                        "Name : {3} \n" +
                                                        "Description : {4} \n" +
                                                        "Last update time : {5} \n";
    }
}