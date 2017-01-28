using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LevelService
    {
        LevelCRUD levelTableActions;

        public LevelService()
        {
            levelTableActions = new LevelCRUD();
        }

        public void SaveLevel(Level levelData)
        {
            levelTableActions.SaveLevel(levelData);
        }

        public Level GetLevelById(int LevelID)
        {
            return levelTableActions.GetLevelById(LevelID);
        }
        public Level GetLevelBySemester(string Semester)
        {
            return levelTableActions.GetLevelBySemester(Semester);
        }
        public void UpdateLevel(Level dataToBeUpdated)
        {
            levelTableActions.UpdateLevel(dataToBeUpdated);
        }
        public void RemoveLevel(Level dataToBeDeleted)
        {
            levelTableActions.RemoveLevel(dataToBeDeleted);
        }

        public void DeleteLevel(int LevelID)
        {
            Level levelToBeDeleted = levelTableActions.GetLevelById(LevelID);
            levelTableActions.RemoveLevel(levelToBeDeleted);
        }

        public IList<Level> GetAllLevels(string sortOrder, string searchString)
        {
            return levelTableActions.GetAllLevels(sortOrder, searchString);
        }
    }
}
