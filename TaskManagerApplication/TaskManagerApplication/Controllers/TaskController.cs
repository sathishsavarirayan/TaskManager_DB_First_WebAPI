using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagerApplication.Models;


namespace TaskManagerApplication.Controllers
{
    [RoutePrefix("Api/Task")]
    public class TaskController : ApiController
    {
        private TaskManagerEntities ObjTMEntities;
        public TaskController()
        {
            ObjTMEntities = new TaskManagerEntities();
        }


        [HttpGet]
        [Route("GetTask")]
        public IEnumerable<TaskDetail> GetAllTasks()
        {
            try
            {
                return ObjTMEntities.TaskDetails.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetTask/{TaskId}")]
        public IHttpActionResult GetTaskbyId(string TaskId)
        {
            TaskDetail objTaskdetail = new TaskDetail();
            int ID = Convert.ToInt32(TaskId);
            try
            {
                objTaskdetail = ObjTMEntities.TaskDetails.Find(ID);
                if (objTaskdetail == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(objTaskdetail);
        }

        [HttpPost]
        [Route("InsertTask")]
        public IHttpActionResult InsertTaskDetail(TaskDetail Taskdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ObjTMEntities.TaskDetails.Add(Taskdata);
                ObjTMEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(Taskdata);
        }


        [HttpPut]
        [Route("UpdateTask")]
        public IHttpActionResult UpdateTaskDetail(TaskDetail Taskdata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TaskDetail objtaskdtl = new TaskDetail();
                objtaskdtl = ObjTMEntities.TaskDetails.Find(Taskdata.Task_ID);
                if (objtaskdtl != null)
                {
                    objtaskdtl.TaskName = Taskdata.TaskName;
                    objtaskdtl.Priority = Taskdata.Priority;
                    objtaskdtl.Start_Date = Taskdata.Start_Date;
                    objtaskdtl.End_Date = Taskdata.End_Date;
                }
                int i = this.ObjTMEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(Taskdata);
        }



        [HttpDelete]
        [Route("DeleteTask")]
        public IHttpActionResult DeleteTask(int TaskId)
        {
            TaskDetail objTaskdtl = ObjTMEntities.TaskDetails.Find(TaskId);
            if (objTaskdtl == null)
            {
                return NotFound();
            }
            try
            {
                ObjTMEntities.TaskDetails.Remove(objTaskdtl);
                ObjTMEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(objTaskdtl);

        }



        [HttpGet]
        [Route("GetTask/{filter}/{value}")]
        public List<TaskDetail> GetFilteredTaskDetails(string filter, string value)
        {
            List<TaskDetail> lstTask = new List<TaskDetail>();
            try
            {
                switch (filter)
                {
                    case "TaskName":
                        lstTask = (from lst in ObjTMEntities.TaskDetails
                                   where lst.TaskName.StartsWith(value)
                                   select lst).ToList();
                        break;
                    case "ParentTaskName":
                        lstTask = (from lst in ObjTMEntities.TaskDetails
                                   where lst.TaskName.StartsWith(value)
                                   select lst).ToList();
                        break;
                    case "Priority":
                        lstTask = (from lst in ObjTMEntities.TaskDetails
                                   where lst.Priority.Equals(value)
                                   select lst).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstTask;
        }



    }
}
