using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookDesktopBackend;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.ComponentModel;
namespace FacebookDesktopBackend
{
    public class GroupsTabFacade
    {
        public FacebookObjectCollection<Group> GetUserGroups(User i_User)
        {
            return i_User.Groups;
        }

        public string GetGroupDescription(Group i_Group)
        {
            DummyGroup dummyGroup = i_Group as DummyGroup;
            return dummyGroup.Description;
        }

        public string GetGroupImageUrl(Group i_Group)
        {
            DummyGroup dummyGroup = i_Group as DummyGroup;
            return dummyGroup.IconUrl;
        }

        public BindingList<DummyGroup> GetUserDummyGroups(User i_LoggedInUser)
        {
            return new BindingList<DummyGroup> {
                new DummyGroup("Gardening Advice", "All tips and tricks regarding gardening", "https://www.allaboutgardening.com/wp-content/uploads/2021/08/Child-Gardening-With-Shovel-1200x667.jpg"),
                new DummyGroup("Design Patterns", "Learn design patterns", "https://res.cloudinary.com/practicaldev/image/fetch/s--DUgpGKQh--/c_imagga_scale,f_auto,fl_progressive,h_900,q_auto,w_1600/https://dev-to-uploads.s3.amazonaws.com/uploads/articles/vvse11o7g3zewjjvu67j.jpeg"),
                new DummyGroup("C#", "Everything C#", "https://www.avenga.com/magazine/future-csharp-programming-language/")
            };
        }
    }
}
