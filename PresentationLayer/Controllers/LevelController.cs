using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreLayer;
using BusinessLayer;
using PagedList;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class LevelController : Controller
    {
        //
        // GET: /Level/

        LevelService levelService = new LevelService();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.YearSortParm = sortOrder == "year_asc" ? "year_dsc" : "year_asc";
            ViewBag.SemesterSortParm = sortOrder == "semester_asc" ? "semester_dsc" : "semester_asc";
            ViewBag.DescriptionSortParm = sortOrder == "desc_asc" ? "desc_dsc" : "desc_asc";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var Levels = levelService.GetAllLevels(sortOrder, searchString);
            return View(Levels.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveLevel(LevelViewModel levelData)
        {

            levelService = new LevelService();
            Level Level = new Level()
            {
                Year = levelData.Year,
                Semester = levelData.Semester,
                Description = levelData.Description,
                InsertedDate = DateTime.Now,
            };
            levelService.SaveLevel(Level);
            return View(levelData);
        }


        public ActionResult UpdateLevel(LevelViewModel newLevelData)
        {
            Level level = new Level()
            {
                Year = newLevelData.Year,
                Semester = newLevelData.Semester,
                Description = newLevelData.Description,
                InsertedDate = DateTime.Now
            };
            levelService.UpdateLevel(level);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteLevel(int LevelID)
        {
            try
            {
                levelService.DeleteLevel(LevelID);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int LevelID)
        {
            Level level = levelService.GetLevelById(LevelID);
            LevelViewModel levelViewModel = new LevelViewModel()
            {
                LevelID = level.LevelID,
                Year = level.Year,
                Semester = level.Semester,
                Description = level.Description,
            };
            return View(levelViewModel);
        }
    }
}
