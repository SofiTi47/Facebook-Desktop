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
    public class NewsFeedTabFacade
    {
        public PostComponent GetPosts(User i_LoggedInUser)
        {
            return new PostGroup(i_LoggedInUser, ePostLocation.NewsFeed);
        }
    }
}
