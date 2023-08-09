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
    public class AlbumsTabFacade
    {
        private ILikedByStrategy m_PhotoLikedByStrategy;

        public AlbumsTabFacade()
        {
            m_PhotoLikedByStrategy = new DemoPhotoLikedByStrategy();
        }
        public void SetLikedByStrategy(ILikedByStrategy i_Strategy) => m_PhotoLikedByStrategy = i_Strategy;

        public FacebookObjectCollection<Album> GetUserAlbums(User i_LoggedInUser)
        {
            return i_LoggedInUser.Albums;
        }

        public string GetAlbumUrl(Album i_Album)
        {
            return i_Album.PictureAlbumURL;
        }

        public string GetAlbumName(Album i_Album)
        {
            return i_Album.Name;
        }

        public FacebookObjectCollection<Photo> GetAlbumPhotos(Album i_Album)
        {
            return i_Album.Photos;
        }

        public string GetPhotoUrl(Photo i_Photo)
        {
            return i_Photo.PictureNormalURL;
        }

        public string GetPhotoName(Photo i_Photo)
        {
            return i_Photo.Name;
        }

        public List<String> GetPhotoComments(Photo i_Photo)
        {
            List<String> comments = new List<string>();
            foreach (Comment photoComment in i_Photo.Comments)
            {
                comments.Add(photoComment.From.Name + " : " + photoComment.Message);
            }
            if (comments.Count == 0)
            {
                comments.Add("No Comments");
            }

            return comments;
        }

        public int GetAmmountOfLikesOfPhoto(Photo i_Photo)
        {
            return (m_PhotoLikedByStrategy.GetLikedBy(i_Photo) as List<string>).Count;
        }

    }
}
