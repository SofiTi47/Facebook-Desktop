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
    public class ProfileTabFacade
    {
        public string FetchCoverPhoto(User i_LoggedInUser)
        {
            foreach (Album album in i_LoggedInUser.Albums)
            {
                if (album.Name == "Cover photos")
                {
                    return album.Photos[0].PictureNormalURL;
                }
            }

            return null;
        }

        public string GetProfilePicture(User i_LoggedInUser)
        {
            return i_LoggedInUser.PictureNormalURL;
        }

        public PostComponent GetPosts(User i_LoggedInUser)
        {
            return new PostGroup(i_LoggedInUser, ePostLocation.User);
        }

    }
}
