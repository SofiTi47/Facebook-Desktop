using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookDesktopBackend;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookDesktopBackend
{
    public class GameData : IConcretePhotoGameIterator
    {
        private List<GameObjectPhotoData> m_PhotoDataList;

        public GameData(User i_LoggedInUser)
        {
            fetchDataFromUser(i_LoggedInUser);   
        }

        public GameObjectPhotoData this[int i_Index] { get { return m_PhotoDataList[i_Index]; } }

        public int Count { get { return m_PhotoDataList.Count; } }

        public void RemovePhotoData(GameObjectPhotoData i_DataToRemove)
        {
            try
            {
                m_PhotoDataList.Remove(i_DataToRemove);
            }
            catch
            {
                return;
            }
        }

        public PhotoGameIterator CreateIterator() { return new PhotoGameIterator(this); }

        private void fetchDataFromUser(User i_LoggedInUser)
        {
            m_PhotoDataList = new List<GameObjectPhotoData>();
            foreach (Album album in i_LoggedInUser.Albums)
            {
                foreach (Photo photo in album.Photos)
                {
                    try
                    {
                        if (photo.Place != null)
                        {
                            if (!string.IsNullOrEmpty(photo.Place.Location.Country))
                            {
                                m_PhotoDataList.Add(new GameObjectPhotoData(photo));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
        }
    }
}
