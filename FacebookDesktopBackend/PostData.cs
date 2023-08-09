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
    public class PostData : PostComponent
    {
        private Post m_Post;
        private ILikedByStrategy m_LikedByStrategy;
        public PostData(Post i_Post, FacebookObject i_PostLocation, ePostLocation i_Location)
        {
            m_LikedByStrategy = new DemoPostLikedByStrategy();
            m_Post = i_Post;
            m_Location = i_Location;
            m_PostLocation = i_PostLocation;
            switch (m_Location)
            {
                case ePostLocation.User:
                    m_NameOfPostsLocation = (i_PostLocation as User).Name;
                    break;
                case ePostLocation.Page:
                    m_NameOfPostsLocation = (i_PostLocation as Page).Name;
                    break;
            }
        }

        public void SetLikedByStrategy(ILikedByStrategy i_Strategy) => m_LikedByStrategy = i_Strategy;
        public override void Add(PostComponent i_Component)
        {
            throw new NotSupportedException();
        }
        public override void Remove(PostComponent i_Component)
        {
            throw new NotSupportedException();
        }

        public override List<PostComponent> GetComponentAsList()
        {
            throw new NotSupportedException();

        }
        public override List<string[]> FetchDataByDate(DateTime i_StartDate, DateTime i_EndDate)
        {
            List<string[]> dataList = new List<string[]>();
            string[] data = new string[4];
            if (m_Post.CreatedTime.Value.Date >= i_StartDate
               && m_Post.CreatedTime.Value.Date <= i_EndDate && m_Post.Message != null)
            {
                data[0] = m_Post.Message;
                data[1] = "1";
                data[2] = m_Post.Comments.Count.ToString();
                data[3] = m_Post.CreatedTime.Value.ToShortDateString();
                dataList.Add(data);
                return dataList;
            }
            else
            {
                return null;
            }

        }
        public override List<string[]> FetchData()
        {
            List<string[]> dataList = new List<string[]>();
            string[] data = new string[3];
            if (m_Post.Message != null)
            {
                data[0] = m_Post.Message;
                data[1] = "1";
                data[2] = m_Post.Comments.Count.ToString();
                dataList.Add(data);
                return dataList;
            }
            else
            {
                return null;
            }
        }

        public override List<string> FetchComments()
        {
            List<string> comments = new List<string>();

            foreach (Comment comment in m_Post.Comments)
            {
                comments.Add(comment.From.Name + " : " + comment.Message);
            }

            if (comments.Count == 0)
            {
                comments.Add("No comments");
            }

            return comments;
        }

        public override List<string> FetchLikes()
        {
            return m_LikedByStrategy.GetLikedBy(m_Post) as List<string>;
        }


    }
}
