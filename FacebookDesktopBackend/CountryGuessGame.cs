using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookDesktopBackend
{
    public class CountryGuessGame : IGame
    {
        private int m_PlayerScore;
        private int m_NumberOfTries;
        private int m_TriesLeft; 
        private GameData m_GameData;
        private GameObjectPhotoData m_CurrentPhoto;
        private User m_LoggedInUser;
        private PhotoGameIterator m_GameDataIterator;



        public CountryGuessGame(User i_LoggedInUser, int i_NumberOfTries)
        {
            m_LoggedInUser = i_LoggedInUser;
            m_PlayerScore = 0;
            m_TriesLeft = i_NumberOfTries;
            m_NumberOfTries = i_NumberOfTries;
            m_GameData = new GameData(i_LoggedInUser);
            m_GameDataIterator = m_GameData.CreateIterator();
        }

        public bool CheckIfCorrect(string i_UserGuess)
        {
            string plainCountryName = GetRightAnswer().ToLower().Trim();
            bool answer = i_UserGuess.Equals(plainCountryName);
            if(answer == false)
            {
                m_TriesLeft--;
            }
            else
            {
                m_PlayerScore++;
            }
            
            return answer;
        }

        public string GetRightAnswer()
        {
            return m_CurrentPhoto.m_PhotoLocation;
        }

        public void NewGame()
        {
            m_GameData = new GameData(m_LoggedInUser);
            m_GameDataIterator = m_GameData.CreateIterator();
            m_PlayerScore = 0;
            m_TriesLeft = m_NumberOfTries;
            m_CurrentPhoto = m_GameDataIterator.First();
            
        }

        public void NextRound()
        {
            m_TriesLeft = m_NumberOfTries;
            m_CurrentPhoto = m_GameDataIterator.Next();
        }

        public string GetCurrentLevelDataToShow()
        {
            return m_CurrentPhoto.m_PhotoImageURL;
        }

        public int GetPlayerScore()
        {
            return m_PlayerScore;
        }

        public int GetTriesLeft()
        {
            return m_TriesLeft;
        }
        public bool IsGameWon()
        {
            return m_GameDataIterator.IsDone();
        }
    }
}
