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
    public class DemoPhotoLikedByStrategy : ILikedByStrategy
    {
        public object GetLikedBy(object i_Item)
        {
            List<string> likesList = new List<string>();
            likesList.Add("Desig Pater");
            return likesList;
        }
    }
}
