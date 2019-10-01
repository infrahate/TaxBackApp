using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxBackApp.Models;

namespace TaxBackApp.Controllers
{
    public class TaskEntriesController : Controller
    {
        private readonly TaxBackContext _context;

        public TaskEntriesController(TaxBackContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //Update next action required on tasks
            foreach (var item in _context.TaskEntries.Include(e => e.TaskComments))
            {
                List<DateTimeOffset> Dates = new List<DateTimeOffset>();
                Dates.Add(item.DateDue);
                foreach (var comm in item.TaskComments)
                {
                    Dates.Add(comm.ReminderDate);
                }
                var soonest = Dates.OrderBy(e => e.DateTime).First();
                if (item.DateNextAction > soonest)
                {
                    _context.TaskEntries.Find(item.TaskEntryId).DateNextAction = soonest;
                }
            }
            await _context.SaveChangesAsync();
            var taxBackContext = _context.TaskEntries.Include(t => t.TaskStatus).Include(t => t.TaskType).Include(t => t.UserList).Include(t => t.TaskComments);
            ViewData["TaskStatusId"] = new SelectList(_context.TaskStatuses, "TaskStatusId", "TaskStatusDescription");
            return View(await taxBackContext.OrderBy(e => e.DateDue).ToListAsync());
        }

        // GET: TaskEntries/Create
        public IActionResult Create()
        {
            ViewData["TaskStatusId"] = new SelectList(_context.TaskStatuses, "TaskStatusId", "TaskStatusDescription");
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "TaskTypeId", "TaskTypeDescription");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName");
            return View();
        }

        // POST: TaskEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskEntryId,DateCreated,DateDue,TaskEntryDescription,TaskStatusId,TaskTypeId,UserId,DateNextAction")] TaskEntry taskEntry)
        {
            taskEntry.DateNextAction = taskEntry.DateDue;

            //Set task status to pending if no user assigned
            if (taskEntry.UserId == null)
            {
                taskEntry.TaskStatusId = 3;
            }
            if (ModelState.IsValid)
            {
                _context.Add(taskEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskStatusId"] = new SelectList(_context.TaskStatuses, "TaskStatusId", "TaskStatusDescription", taskEntry.TaskStatusId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "TaskTypeId", "TaskTypeDescription", taskEntry.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", taskEntry.UserId);
            return View(taskEntry);
        }

        // GET: TaskEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntry = await _context.TaskEntries.FindAsync(id);
            if (taskEntry == null)
            {
                return NotFound();
            }
            ViewData["TaskStatusId"] = new SelectList(_context.TaskStatuses, "TaskStatusId", "TaskStatusDescription", taskEntry.TaskStatusId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "TaskTypeId", "TaskTypeDescription", taskEntry.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", taskEntry.UserId);
            return View(taskEntry);
        }

        // POST: TaskEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskEntryId,DateCreated,DateDue,TaskEntryDescription,TaskStatusId,TaskTypeId,UserId,DateNextAction")] TaskEntry taskEntry)
        {
            if (id != taskEntry.TaskEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskEntryExists(taskEntry.TaskEntryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskStatusId"] = new SelectList(_context.TaskStatuses, "TaskStatusId", "TaskStatusDescription", taskEntry.TaskStatusId);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskTypes, "TaskTypeId", "TaskTypeDescription", taskEntry.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName", taskEntry.UserId);
            return View(taskEntry);
        }

        // GET: TaskEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var taskEntry = await _context.TaskEntries.FindAsync(id);
            _context.TaskEntries.Remove(taskEntry);
            var comments = _context.TaskComments.Where(e => e.TaskEntryId == id);
            foreach (var item in comments)
            {
                _context.TaskComments.Remove(_context.TaskComments.Find(item.TaskCommentId));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskEntryExists(int id)
        {
            return _context.TaskEntries.Any(e => e.TaskEntryId == id);
        }
    }
}
