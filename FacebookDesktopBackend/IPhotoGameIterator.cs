using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookDesktopBackend
{
    public interface IPhotoGameIterator
    {
        GameObjectPhotoData First();
        GameObjectPhotoData Next();
        bool IsDone();
        GameObjectPhotoData CurrentItem();
    }
}
