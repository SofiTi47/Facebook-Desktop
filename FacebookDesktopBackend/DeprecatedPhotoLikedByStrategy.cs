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
    public class DeprecatedPhotoLikedByStrategy : ILikedByStrategy
    {
        public object GetLikedBy(object i_Item)
        {
            Photo photo = i_Item as Photo;
            List<string> likesList = new List<string>();
            foreach (User user in photo.LikedBy)
            {
                likesList.Add(user.Name);
            }
            return likesList;
        }
    }
}
