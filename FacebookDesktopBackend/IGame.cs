using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookDesktopBackend
{
    public interface IGame
    {
        bool CheckIfCorrect(string i_UserGuess);

        string GetRightAnswer();

        string GetCurrentLevelDataToShow();

        void NewGame();

        void NextRound();

        int GetPlayerScore();

        int GetTriesLeft();

        int GetRoundsLeft();
        
    }
}
