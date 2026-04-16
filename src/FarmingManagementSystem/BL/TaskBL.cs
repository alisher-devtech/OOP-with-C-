using System;
using System.Collections.Generic;
using FarmingManagementSystem.DL;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.BL
{
    public class TaskBL
    {
        private TaskDL taskDL;

        public TaskBL()
        {
            taskDL = new TaskDL();
        }

        public void LoadTasks()
        {
            try
            {
                taskDL.LoadTasksFromDatabase();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load tasks: " + ex.Message);
            }
        }

        public List<TaskItem> GetAllTasks()
        {
            try
            {
                return taskDL.GetAllTasks();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get tasks: " + ex.Message);
            }
        }

        public bool AddTask(string taskName, string status, string deadline)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taskName))
                {
                    throw new Exception("Task name cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Task status cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(deadline))
                {
                    throw new Exception("Deadline cannot be empty!");
                }

                TaskItem task = new TaskItem(0, taskName, status, deadline);
                taskDL.AddTask(task);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add task: " + ex.Message);
            }
        }

        public bool UpdateTask(int taskId, string status, string deadline)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new Exception("Invalid task ID!");
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    throw new Exception("Status cannot be empty!");
                }

                if (string.IsNullOrWhiteSpace(deadline))
                {
                    throw new Exception("Deadline cannot be empty!");
                }

                taskDL.UpdateTask(taskId, status, deadline);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update task: " + ex.Message);
            }
        }

        public bool DeleteTask(int taskId)
        {
            try
            {
                if (taskId <= 0)
                {
                    throw new Exception("Invalid task ID!");
                }

                return taskDL.DeleteTask(taskId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete task: " + ex.Message);
            }
        }
    }
}