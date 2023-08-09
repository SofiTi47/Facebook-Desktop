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
    public abstract class PostComponent
    {
        protected string m_NameOfPostsLocation;
        protected FacebookObject m_PostLocation;
        protected ePostLocation m_Location;

        public abstract void Add(PostComponent i_Component);
        public abstract void Remove(PostComponent i_Component);
        public abstract List<string[]> FetchData();
        public abstract List<string[]> FetchDataByDate(DateTime i_StartDate, DateTime i_EndDate);
        public abstract List<string> FetchLikes();
        public abstract List<string> FetchComments();
        public abstract List<PostComponent> GetComponentAsList();
    }
}
