using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FarmingManagementSystem.Utilities;
using FarmingManagementSystem.Models;

namespace FarmingManagementSystem.DL
{
    public class TaskDL
    {
        private List<TaskItem> tasks;

        public TaskDL()
        {
            tasks = new List<TaskItem>();
        }

        public List<TaskItem> GetAllTasks()
        {
            return tasks;
        }

        public void LoadTasksFromDatabase()
        {
            try
            {
                tasks.Clear();
                string query = "SELECT taskcropid, taskname, taskstatus, taskdeadline FROM tasks";

                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    while (reader.Read())
                    {
                        TaskItem task = new TaskItem();
                        task.TaskCropId = reader.GetInt32("taskcropid");
                        task.TaskName = reader.GetString("taskname");
                        task.TaskStatus = reader.GetString("taskstatus");
                        task.TaskDeadline = reader.GetString("taskdeadline");
                        tasks.Add(task);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while loading tasks: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading tasks: " + ex.Message);
            }
        }

        public void AddTask(TaskItem task)
        {
            try
            {
                if (task == null)
                {
                    throw new Exception("Task object cannot be null!");
                }

                task.TaskCropId = tasks.Count + 1;

                string query = "INSERT INTO tasks (taskcropid, taskname, taskstatus, taskdeadline) " +
                              "VALUES (@taskcropid, @taskname, @taskstatus, @taskdeadline)";

                DatabaseHelper.Instance.Update(query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@taskcropid", task.TaskCropId);
                    cmd.Parameters.AddWithValue("@taskname", task.TaskName);
                    cmd.Parameters.AddWithValue("@taskstatus", task.TaskStatus);
                    cmd.Parameters.AddWithValue("@taskdeadline", task.TaskDeadline);
                });

                tasks.Add(task);
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while adding task: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding task: " + ex.Message);
            }
        }

        public void UpdateTask(int taskId, string status, string deadline)
        {
            try
            {
                TaskItem task = null;
                foreach (TaskItem t in tasks)
                {
                    if (t.TaskCropId == taskId)
                    {
                        task = t;
                        break;
                    }
                }

                if (task != null)
                {
                    task.TaskStatus = status;
                    task.TaskDeadline = deadline;

                    string query = "UPDATE tasks SET taskstatus = @taskstatus, taskdeadline = @taskdeadline WHERE taskcropid = @taskcropid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@taskstatus", status);
                        cmd.Parameters.AddWithValue("@taskdeadline", deadline);
                        cmd.Parameters.AddWithValue("@taskcropid", taskId);
                    });
                }
                else
                {
                    throw new Exception("Task not found with ID: " + taskId);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while updating task: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating task: " + ex.Message);
            }
        }

        public bool DeleteTask(int taskId)
        {
            try
            {
                TaskItem task = null;
                foreach (TaskItem t in tasks)
                {
                    if (t.TaskCropId == taskId)
                    {
                        task = t;
                        break;
                    }
                }

                if (task != null)
                {
                    string query = "DELETE FROM tasks WHERE taskcropid = @taskcropid";

                    DatabaseHelper.Instance.Update(query, cmd =>
                    {
                        cmd.Parameters.AddWithValue("@taskcropid", taskId);
                    });

                    tasks.Remove(task);
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database error while deleting task: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting task: " + ex.Message);
            }
        }
    }
}