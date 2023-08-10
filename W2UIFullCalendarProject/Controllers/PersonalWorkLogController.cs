using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using W2UIFullCalendarProject.Models;
using W2UIFullCalendarProject.Data;

namespace W2UIFullCalendarProject.Controllers
{
    public class PersonalWorkLogController : Controller
    {
        private readonly ILogger<PersonalWorkLogController> _logger;
        private readonly Context _context;

        public PersonalWorkLogController(Context context,ILogger<PersonalWorkLogController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult FullCalendarView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTodayPersonalData(int persid)
        {
            //var persd = 1; /*HttpContext.Session.GetInt32("Id");*/
            var allData = _context.RDWorklogTracking.Where(x => x.WL_CreatedById == persid).ToList();
            return Json(allData);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewLog([FromBody] RDWorklogTracking data)
        {
            if (data == null)
            {
                return BadRequest(new { success = false, WL_CreatedById = data?.WL_CreatedById, message = "GÖNDERİLEN DATA YANLIŞ" });
            }
            var username = "aa";//_context.KULLANICILAR.Where(x => x.ID == data.WL_CreatedById).Select(x => x.USERNAME).FirstOrDefault();
            int max_row = _context.RDWorklogTracking.Any() ? _context.RDWorklogTracking.Max(x => x.WL_Row) + 1 : 1;
            string logno = "PWL" + DateTime.Now.Year.ToString().Substring(2) + max_row.ToString().PadLeft(10, '0');
            RDWorklogTracking log = new RDWorklogTracking
            {
                WL_Row = max_row,
                WL_No = logno,
                WL_CreatedBy = username,
                WL_CreatedDate = DateTime.Now,
                WL_Project = data.WL_Project,
                WL_Title = data.WL_Title,
                WL_Description = data.WL_Description,
                WL_StartDate = data.WL_StartDate,
                WL_FinishDate = data.WL_FinishDate,
                WL_CreatedById = data.WL_CreatedById

            };

            _context.RDWorklogTracking.Add(log);
            await _context.SaveChangesAsync();

            return Ok(new { WL_CreatedById = data.WL_CreatedById, message = "Form data saved successfully." });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDatesOfDraggedResizedLog(int logid, string dragStartDate, string dragFinishDate)
        {

            string dateTimePartStart = dragStartDate.Substring(4, 20);
            DateTimeOffset dateTimeOffsetStart = DateTimeOffset.Parse(dateTimePartStart);

            string dateTimePartFinish = dragFinishDate.Substring(4, 20);
            DateTimeOffset dateTimeOffsetFinish = DateTimeOffset.Parse(dateTimePartFinish);



            var draggedLog = await _context.RDWorklogTracking.Where(x => x.ID == logid).ToListAsync();
            foreach (var log in draggedLog)
            {
                log.WL_StartDate = dateTimeOffsetStart.DateTime;
                log.WL_FinishDate = dateTimeOffsetFinish.DateTime;

                _context.RDWorklogTracking.Update(log);
                await _context.SaveChangesAsync();
            }
            return Ok(new { WL_CreatedById = draggedLog[0].WL_CreatedById, message = "Form data saved successfully." });
        }
        [HttpPost]
        public async Task<IActionResult> EditWorkLog([FromBody] RDWorklogTracking data)
        {

            if (data == null)
            {
                return BadRequest(new { success = false, WL_CreatedById = data?.WL_CreatedById, message = "GÖNDERİLEN DATA YANLIŞ" });
            }

            var draggedLog = await _context.RDWorklogTracking.Where(x => x.ID == data.ID).ToListAsync();
            foreach (var log in draggedLog)
            {
                log.WL_Title = data.WL_Title;
                log.WL_Project = data.WL_Project;
                log.WL_Description = data.WL_Description;
                log.WL_StartDate = data.WL_StartDate;
                log.WL_FinishDate = data.WL_FinishDate;

                _context.RDWorklogTracking.Update(log);
                await _context.SaveChangesAsync();
            }
            return Ok(new { WL_CreatedById = data.WL_CreatedById, message = "Form data saved successfully." });
        }
        [HttpPost]
        public async Task<IActionResult> deleteWorkLog(int logid)
        {

            var deletedrow = await _context.RDWorklogTracking.Where(x => x.ID == logid).ToListAsync();
            foreach (var log in deletedrow)
            {

                _context.RDWorklogTracking.Remove(log);
                await _context.SaveChangesAsync();
            }
            return Ok(new { WL_CreatedById = deletedrow[0].WL_CreatedById, message = "Form data saved successfully." });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
