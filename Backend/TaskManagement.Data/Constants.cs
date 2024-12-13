namespace TaskManagement.Data;

public static class Constants
{
    public static class TaskStatusName
    {
        public const string NotStarted = "Not Started";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
    }
    public static class TaskStatusId
    {
        public const int NotStarted = 0;
        public const int InProgress = 1;
        public const int Completed = 2;
    }
}