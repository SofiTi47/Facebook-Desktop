using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookDesktopBackend
{
    internal class AlbumNameGuessGame : IGame
    {
        private int m_PlayerScore;
        private int m_NumberOfTries;
        private int m_TriesLeft;
        private int PhotosLeft;
        private List<Photo> m_GamePhotoList;
        private Photo m_CurrentPhoto;
        private User m_LoggedInUser;



        public AlbumNameGuessGame(User i_LoggedInUser, int i_NumberOfTries)
        {
            m_LoggedInUser = i_LoggedInUser;
            m_PlayerScore = 0;
            m_TriesLeft = i_NumberOfTries;
            m_NumberOfTries = i_NumberOfTries;
            m_GamePhotoList = new List<Photo>(getUserPhotos());
            PhotosLeft = m_GamePhotoList.Count;
        }

        private FacebookObjectCollection<Photo> getUserPhotos()
        {
            FacebookObjectCollection<Photo> photoList = new FacebookObjectCollection<Photo>();
            foreach (Album album in m_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    try
                    {
                        if (photo.Place != null)
                        {
                            if (!string.IsNullOrEmpty(photo.Place.Location.Country))
                            {
                                photoList.Add(photo);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            return photoList;
        }

        protected Photo GetRandomPhoto()
        {
            Random random = new Random();
            int index = random.Next(m_GamePhotoList.Count);
            return m_GamePhotoList[index];
        }


        public bool CheckIfCorrect(string i_UserGuess)
        {
            string plainAlbumName = GetRightAnswer().ToLower().Trim();
            bool answer = i_UserGuess.Equals(plainAlbumName);
            if (answer == false)
            {
                m_TriesLeft--;
            }
            else
            {
                m_PlayerScore++;
                m_GamePhotoList.Remove(m_CurrentPhoto);
            }

            return answer;
        }

        public string GetRightAnswer()
        {
            return m_CurrentPhoto.Album.Name;
        }

        public void NewGame()
        {
            m_PlayerScore = 0;
            m_TriesLeft = m_NumberOfTries;
            m_CurrentPhoto = GetRandomPhoto();
            m_GamePhotoList = new List<Photo>(getUserPhotos());
        }

        public void NextRound()
        {
            m_TriesLeft = m_NumberOfTries;
            m_CurrentPhoto = GetRandomPhoto();
        }

        public string GetCurrentLevelDataToShow()
        {
            return m_CurrentPhoto.PictureNormalURL;
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
            return m_GamePhotoList.Count <= 0;
        }
    }
}
