using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookDesktopBackend
{
    public interface ILikedByStrategy
    {
        object GetLikedBy(object i_Item);
    }
}
