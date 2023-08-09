using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookDesktopBackend
{
    public class PhotoGameIterator : IPhotoGameIterator
    {
        private GameData m_GameData;
        private int m_CurrentPhotoData;
        private int m_PhotosLeft;
        public PhotoGameIterator(GameData i_GameData)
        {
            m_GameData = i_GameData;
            m_CurrentPhotoData = 0;
            m_PhotosLeft = i_GameData.Count;
        }
        private int getRandomIndex() { return new Random().Next(0, m_GameData.Count); }

        public GameObjectPhotoData First()
        {
            m_CurrentPhotoData = getRandomIndex();
            return m_GameData[m_CurrentPhotoData];
        }

        public GameObjectPhotoData Next()
        {
            m_PhotosLeft--;
            GameObjectPhotoData photoData = null;
            if (!IsDone())
            {
                m_GameData.RemovePhotoData(m_GameData[m_CurrentPhotoData]);
                m_CurrentPhotoData = getRandomIndex();
                photoData = m_GameData[m_CurrentPhotoData];
            }
            return photoData;

        }

        public bool IsDone() { return m_PhotosLeft <= 0; }

        public GameObjectPhotoData CurrentItem()
        {
            return m_GameData[m_CurrentPhotoData];
        }
    }
}