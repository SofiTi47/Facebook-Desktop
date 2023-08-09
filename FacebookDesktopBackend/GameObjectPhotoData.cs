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
    public class GameObjectPhotoData
    {
        public string m_PhotoLocation { get; }
        public string m_PhotoImageURL { get; }

        public GameObjectPhotoData(Photo i_Photo)
        {
            m_PhotoLocation = i_Photo.Place.Location.Country.Trim().ToLower();
            m_PhotoImageURL = i_Photo.PictureNormalURL;
        }

    }
}
