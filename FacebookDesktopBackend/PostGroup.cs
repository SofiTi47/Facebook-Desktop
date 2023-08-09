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
    public class PostGroup : PostComponent
    {
        private List<PostComponent> r_PostComponents = new List<PostComponent>();

        public PostGroup(FacebookObject i_PostLocation, ePostLocation i_Location)
        {
            m_Location = i_Location;
            m_PostLocation = i_PostLocation;
            switch (m_Location)
            {
                case ePostLocation.User:
                    m_NameOfPostsLocation = (i_PostLocation as User).Name;
                    foreach (Post post in (i_PostLocation as User).Posts)
                    {
                        if(post.Message != null)
                        {
                            r_PostComponents.Add(new PostData(post, i_PostLocation, i_Location));
                        }
                    }

                    break;
                case ePostLocation.Page:
                    m_NameOfPostsLocation = (i_PostLocation as Page).Name;
                    foreach (Post post in (i_PostLocation as Page).Posts)
                    {
                        if(post.Message != null)
                        {
                            r_PostComponents.Add(new PostData(post, i_PostLocation, i_Location));
                        }
                    }
                    break;
                case ePostLocation.NewsFeed:
                    m_NameOfPostsLocation = "NewsFeed";
                    foreach (Post post in (i_PostLocation as User).NewsFeed)
                    {
                        if(post.Message != null)
                        {
                            r_PostComponents.Add(new PostData(post, i_PostLocation, i_Location));
                        }
                    }
                    break;
            }
            
        }
        public PostComponent this[int i_Index] { get { return r_PostComponents[i_Index]; } }
        public override void Add(PostComponent i_Component)
        {
            r_PostComponents.Add(i_Component);
        }
        public override void Remove(PostComponent i_Component)
        {
            if(r_PostComponents.Contains(i_Component))
            {
                r_PostComponents.Remove(i_Component);
            }
        }

        public override List<string> FetchComments()
        {
            List<string> comments = new List<string>();
            foreach(PostComponent componnent in r_PostComponents)
            {
                comments.AddRange(componnent.FetchComments());
            }
            return comments;
        }

        public override List<string> FetchLikes()
        {
            List<string> likes = new List<string>();
            foreach (PostComponent componnent in r_PostComponents)
            {
                likes.AddRange(componnent.FetchLikes());
            }
            return likes;
        }

        public override List<string[]> FetchData()
        {
            List<string[]> dataList = new List<string[]>();
            foreach(PostComponent component in r_PostComponents)
            {
                List<string[]> componentData = component.FetchData();
                if (componentData != null)
                {
                    dataList.AddRange(componentData);
                }
            }

            return dataList;
        }

        public override List<string[]> FetchDataByDate(DateTime i_StartDate, DateTime i_EndDate)
        {
            List<string[]> dataList = new List<string[]>();
            foreach (PostComponent component in r_PostComponents)
            {
                List<string[]> componentData = component.FetchDataByDate(i_StartDate, i_EndDate);
                if (componentData != null)
                {
                    dataList.AddRange(componentData);
                }
            }

            return dataList;
        }

        public override List<PostComponent> GetComponentAsList()
        {
            return r_PostComponents;
        }

    }
}
