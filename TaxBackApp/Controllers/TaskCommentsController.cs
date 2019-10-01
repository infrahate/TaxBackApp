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
    public class TaskCommentsController : Controller
    {
        private readonly TaxBackContext _context;

        public TaskCommentsController(TaxBackContext context)
        {
            _context = context;
        }

        // GET: TaskComments
        public async Task<IActionResult> Index()
        {
            var taxBackContext = _context.TaskComments.Include(t => t.TaskCommentType).Include(t => t.TaskEntry);
            return View(await taxBackContext.ToListAsync());
        }

        // GET: TaskComments/Create
        public IActionResult Create()
        {
            ViewData["TaskCommentTypeId"] = new SelectList(_context.TaskCommentTypes, "TaskCommentTypeId", "TaskCommentTypeDescription");
            ViewData["TaskEntryId"] = new SelectList(_context.TaskEntries, "TaskEntryId", "TaskEntryDescription");
            return View();
        }

        // POST: TaskComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskCommentId,TaskCommentDescription,TaskCommentTypeId,ReminderDate,TaskEntryId")] TaskComment taskComment)
        {
            ViewData["TaskCommentTypeId"] = new SelectList(_context.TaskCommentTypes, "TaskCommentTypeId", "TaskCommentTypeDescription", taskComment.TaskCommentTypeId);
            ViewData["TaskEntryId"] = new SelectList(_context.TaskEntries, "TaskEntryId", "TaskEntryDescription", taskComment.TaskEntryId);

            //If Reminder date later than Assigned taks date
            if ( taskComment.TaskEntryId!=null &&  taskComment.ReminderDate > _context.TaskEntries.Find(taskComment.TaskEntryId).DateDue) {
                ViewData["Error"] = "Comment reminder date cannot be later than "+ _context.TaskEntries.Find(taskComment.TaskEntryId).DateDue.ToString("yyyy/MM/dd");
                return View(taskComment);
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(taskComment);
                //if comment's reminder date is sooner than currently bound tasks's next action date
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskComment);
        }

        // GET: TaskComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskComment = await _context.TaskComments.FindAsync(id);
            if (taskComment == null)
            {
                return NotFound();
            }
            ViewData["TaskCommentTypeId"] = new SelectList(_context.TaskCommentTypes, "TaskCommentTypeId", "TaskCommentTypeDescription", taskComment.TaskCommentTypeId);
            ViewData["TaskEntryId"] = new SelectList(_context.TaskEntries, "TaskEntryId", "TaskEntryDescription", taskComment.TaskEntryId);
            return View(taskComment);
        }

        // POST: TaskComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskCommentId,TaskCommentDescription,TaskCommentTypeId,ReminderDate,TaskEntryId")] TaskComment taskComment)
        {
            if (id != taskComment.TaskCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskCommentExists(taskComment.TaskCommentId))
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
            ViewData["TaskCommentTypeId"] = new SelectList(_context.TaskCommentTypes, "TaskCommentTypeId", "TaskCommentTypeDescription", taskComment.TaskCommentTypeId);
            ViewData["TaskEntryId"] = new SelectList(_context.TaskEntries, "TaskEntryId", "TaskEntryDescription", taskComment.TaskEntryId);
            return View(taskComment);
        }

        // GET: TaskComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var taskComment = await _context.TaskComments.FindAsync(id);
            _context.TaskComments.Remove(taskComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskCommentExists(int id)
        {
            return _context.TaskComments.Any(e => e.TaskCommentId == id);
        }
    }
}
