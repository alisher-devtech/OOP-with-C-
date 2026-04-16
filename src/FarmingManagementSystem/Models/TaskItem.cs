namespace FarmingManagementSystem.Models
{
    public class TaskItem
    {
        private int taskCropId;
        private string taskName;
        private string taskStatus;
        private string taskDeadline;

        public int TaskCropId
        {
            get { return taskCropId; }
            set { taskCropId = value; }
        }

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        public string TaskStatus
        {
            get { return taskStatus; }
            set { taskStatus = value; }
        }

        public string TaskDeadline
        {
            get { return taskDeadline; }
            set { taskDeadline = value; }
        }

        public TaskItem()
        {
            taskCropId = 0;
            taskName = "";
            taskStatus = "";
            taskDeadline = "";
        }

        public TaskItem(int taskCropId, string taskName, string taskStatus, string taskDeadline)
        {
            this.taskCropId = taskCropId;
            this.taskName = taskName;
            this.taskStatus = taskStatus;
            this.taskDeadline = taskDeadline;
        }
    }
}