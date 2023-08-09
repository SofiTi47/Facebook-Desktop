using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookDesktopBackend
{
    public class GameDecorator : IGame
    {
        public IGame Game { get; set; }
        
        public bool CheckIfCorrect(string i_UserGuess)
        {
            return Game.CheckIfCorrect(i_UserGuess);
        }

        public string GetRightAnswer()
        {
            return Game.GetRightAnswer();
        }

        public string GetCurrentLevelDataToShow()
        {
            return Game.GetCurrentLevelDataToShow();
        }

        public void NewGame()
        {
            Game.NewGame();
        }

        public void NextRound()
        {
            Game.NextRound();
        }

        public int GetPlayerScore()
        {
            return Game.GetPlayerScore();
        }

        public int GetTriesLeft()
        {
            return Game.GetPlayerScore();
        }

        public int GetRoundsLeft()
        {
            return Game.GetRoundsLeft();
        }

        public string GetGameTitle()
        {
            return Game.ToString();
        }

        public string GetGuessTitle()
        {
            return Game.ToString().Split(' ')[0];
        }
    }
}
