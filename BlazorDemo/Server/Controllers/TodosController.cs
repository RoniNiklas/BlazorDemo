using BlazorDemo.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using BlazorDemo.Server.Persistence;
using BlazorDemo.Server.Domain;

namespace BlazorDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoStore _todoStore { get; set; } 

        public TodosController(ITodoStore todoStore)
        {
            _todoStore = todoStore;
        }

        [HttpGet]
        public IEnumerable<TodoTableVM> GetTableVMs()
        {
            var vms = _todoStore.GetAll().Select(item => new TodoTableVM
            {
                Id = item.Id.GetValueOrDefault(),
                Assignee = item.Assignee,
                Summary = item.Summary,
                DueDate = item.DueDate,
                Finished = item.DateFinished != null
            });

            return vms;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoDetailsVM?> GetDetailsVM(int id)
        {
            var entity = _todoStore.GetById(id);

            if (entity != null)
            {
                return new TodoDetailsVM()
                {
                    Id = entity.Id,
                    Assignee = entity.Assignee,
                    Summary = entity.Summary,
                    DueDate = entity.DueDate,
                    DateAssigned = entity.DateAssigned,
                    DateFinished = entity.DateFinished,
                    Details = entity.Details
                };
            }

            return NotFound();
        }

        [HttpPost]
        public StatusCodeResult Upsert(TodoDetailsVM todo)
        {
            if (todo.Validate())
            {
                var vmAsEntity = new TodoEntity()
                {
                    Id = todo.Id,
                    Assignee = todo.Assignee,
                    Summary = todo.Summary,
                    DueDate = todo.DueDate,
                    DateAssigned = todo.DateAssigned,
                    DateFinished = todo.DateFinished,
                    Details = todo.Details
                };

                _todoStore.Upsert(vmAsEntity);
                return Ok();
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            _todoStore.Delete(id);
            return Ok();
        }
    }
}
