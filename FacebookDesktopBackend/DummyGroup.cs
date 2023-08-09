using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookDesktopBackend
{
    public class DummyGroup : Group
    {
        public new string Name {get;}
        public new string Description { get; set; }
        public new string IconUrl { get; }

        
        
        public DummyGroup()
        {
            Name = "Indoor Plant Tips & Tricks";
            Description =
                "**Before you post your questions, please use the search bar at the top of our page using keywords of what you need help with**\r\n"
                + "Australia is going totally bonkers for indoor plants! And the more we all collect, the more questions we have about how to keep them alive, how to propagate, "
                + "where to buy from and how much we should be paying.\r\n"
                + "This group is open to all indoor plant lovers who want to hear "
                + "plant care tips and hacks from experts as well as share their "
                + "own ideas and questions.\r\nNo selling. Sales posts will be "
                + "deleted and repeat offenders blocked.\r\nKeep it clean, kind and only indoor plant related.";

            IconUrl = "https://scontent.ftlv5-1.fna.fbcdn.net/v/t1.6435-9/104830239_590845714900546_346748834745194646_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=8631f5&_nc_ohc=DQFyvjzQv4gAX8Es_Gr&_nc_ht=scontent.ftlv5-1.fna&oh=00_AT8s2pbZ31JZv6nEpexhIQtKSiPLSF4x_AoXS6wremx7ug&oe=6315E38A";
        }

        public DummyGroup(string i_Name, string i_Description, string i_IconURL)
        {
            Name = i_Name;
            Description = i_Description;
            IconUrl = i_IconURL;
        }
    }
}
