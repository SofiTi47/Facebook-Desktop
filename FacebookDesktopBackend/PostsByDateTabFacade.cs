using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookDesktopBackend;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;


public class PostsByDateTabFacade
{
    public bool CheckPostValid(PostComponent i_Post, DateTime i_SelectedDateStart, DateTime i_SelectedDateEnd)
    {
        return i_Post.FetchDataByDate(i_SelectedDateStart, i_SelectedDateEnd) != null;
    }

    public List<PostComponent> GetUserPosts(User i_User)
    {
        return new PostGroup(i_User, ePostLocation.User).GetComponentAsList();
    }

    public List<PostComponent> GetPostsByDate(User i_User, DateTime i_SelectedDateStart, DateTime i_SelectedDateEnd)
    {
        List<PostComponent> validPosts = new List<PostComponent>();
        foreach(PostComponent post in GetUserPosts(i_User))
        {
            if (CheckPostValid(post,i_SelectedDateStart,i_SelectedDateEnd))
            {
                validPosts.Add(post);
            }
        }
        return validPosts;
    }

}
