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
    public class DeprecatedPostLikedByStrategy : ILikedByStrategy
    {
        public object GetLikedBy(object i_Item)
        {
            Post post = i_Item as Post;
            List<string> likesList = new List<string>();
            foreach(User user in post.LikedBy)
            {
                likesList.Add(user.Name);
            }
            return likesList;
        }
    }
}
