using OnlineToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineToDoList.Controllers
{
    public class ToDoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetToDoList()
        {
            using (OnlineToDoListDBEntities Obj = new OnlineToDoListDBEntities())
            {
                List<ToDoList> taskList = Obj.ToDoLists.ToList();
                return Json(taskList, JsonRequestBehavior.AllowGet);
            }
        }
        
        public string InsertTask(ToDoList task)
        {
            if (task != null)
            {
                using (OnlineToDoListDBEntities Obj = new OnlineToDoListDBEntities())
                {
                    ToDoList taskObj = new ToDoList();
                    taskObj.Content = task.Content;
                    taskObj.CreatedDateTime = DateTime.Now;
                    taskObj.Status = "Active";
                    Obj.ToDoLists.Add(taskObj);
                    Obj.SaveChanges();
                    return "Task Added Successfully";
                }
            }
            else
            {
                return "Task Not Inserted! Try Again";
            }
        }

        public string DeleteTask(ToDoList task)
        {
            if (task != null)
            {
                using (OnlineToDoListDBEntities Obj = new OnlineToDoListDBEntities())
                {
                    var taskObj = Obj.Entry(task);
                    if (taskObj.State == System.Data.Entity.EntityState.Detached)
                    {
                        Obj.ToDoLists.Attach(task);
                        Obj.ToDoLists.Remove(task);
                    }
                    Obj.SaveChanges();
                    return "Task Deleted Successfully";
                }
            }
            else
            {
                return "Task Not Deleted! Try Again";
            }
        }

        public string CompleteTask(ToDoList task)
        {
            if (task != null)
            {
                using (OnlineToDoListDBEntities Obj = new OnlineToDoListDBEntities())
                {
                    var taskObj = Obj.Entry(task);
                    ToDoList taskObj2 = Obj.ToDoLists.Where(x => x.ID == task.ID).FirstOrDefault();
                    taskObj2.Status = "Completed";
                    Obj.SaveChanges();
                    return "Task Updated Successfully";
                }
            }
            else
            {
                return "Task Not Updated! Try Again";
            }
        }
    }
}