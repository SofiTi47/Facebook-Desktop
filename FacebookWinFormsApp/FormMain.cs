using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasicFacebookFeatures;
using FacebookDesktopBackend;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.Threading;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        private User m_LoggedInUser;
        private LoginResult m_LoginResult;
        private ProgramSettings m_ProgramSettings;
        private IGame m_countryGuessGame;
        private ProfileTabFacade m_ProfileTabFacade;
        private PostsByDateTabFacade m_PostsByDateTabFacade;
        private GroupsTabFacade m_GroupTabFacade;
        private AlbumsTabFacade m_AlbumTabFacade;
        private NewsFeedTabFacade m_NewsFeedTabFacade;
        private readonly List<ActionCommand> r_TabCommands = new List<ActionCommand>();
        public FormMain()
        {
            InitializeComponent();
            FacebookService.s_CollectionLimit = 100;
            initProgramSettings();
        }


        private void initProgramSettings()
        {
            initTabCommands();
            m_ProfileTabFacade = new ProfileTabFacade();
            m_PostsByDateTabFacade = new PostsByDateTabFacade();
            m_GroupTabFacade = new GroupsTabFacade();
            m_AlbumTabFacade = new AlbumsTabFacade();
            m_NewsFeedTabFacade = new NewsFeedTabFacade();
            m_ProgramSettings = ProgramSettings.LoadFromFile();
            this.Location = m_ProgramSettings.LastWindowLocation;
            this.Size = m_ProgramSettings.LastWindowsSize;
            this.WindowState = m_ProgramSettings.LastWindowState;
            this.checkBoxRememberUser.Checked = m_ProgramSettings.RememberUser;
            if (m_ProgramSettings.RememberUser && m_ProgramSettings.LastAccessToken != string.Empty)
            {
                authLogIn();
            }
        }

        private void initTabCommands()
        {
            r_TabCommands.Add(new ActionCommand { Action = loadProfileTab });
            r_TabCommands.Add(new ActionCommand { Action = loadAlbumTab });
            r_TabCommands.Add(new ActionCommand { Action = loadGroupTab });
            r_TabCommands.Add(new ActionCommand { Action = loadFeedTab });
            r_TabCommands.Add(new ActionCommand { Action = loadPostsByDateTab });
            r_TabCommands.Add(new ActionCommand { Action = loadGuessCountryGame });
        }

        private void authLogIn()
        {
            m_LoginResult = FacebookService.Connect(m_ProgramSettings.LastAccessToken);
            m_LoggedInUser = m_LoginResult.LoggedInUser;
            m_countryGuessGame = new CountryGuessGame(m_LoggedInUser, 3);
        }
        private void LogIn()
        {
            m_LoginResult = FacebookService.Login(
                            "762896271622707",
                                 /// requested permissions:
                            "email",
                            "public_profile",
                            "user_birthday",
                            "user_events",
                            "user_friends",
                            "user_gender",
                            "user_hometown",
                            "user_likes",
                            "user_link",
                            "user_location",
                            "user_photos",
                            "user_posts");
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken))
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                m_countryGuessGame = new CountryGuessGame(m_LoggedInUser, 3);
                initForm();
            }
            else
            {
                MessageBox.Show(m_LoginResult.ErrorMessage, "Login Failed");
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            new Thread(LogIn).Start();
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if(m_LoggedInUser != null)
            {
                initForm();
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            m_ProgramSettings.LastWindowsSize = this.Size;
            m_ProgramSettings.LastWindowLocation = this.Location;
            m_ProgramSettings.RememberUser = checkBoxRememberUser.Checked;
            m_ProgramSettings.LastAccessToken = m_ProgramSettings.RememberUser ? m_LoginResult.AccessToken : string.Empty;
            m_ProgramSettings.LastWindowState = this.WindowState;
            m_ProgramSettings.SaveToFile();
        }


        private void initForm()
        {
            tabControl1.Invoke(new Action(() => tabControl1.Show()));
            new Thread(loadProfileTab).Start();
            buttonLogin.Invoke(new Action(()=>buttonLogin.Text = $"Logged in as {m_LoggedInUser.Name}"));
            richTextBoxPost.Invoke(new Action(()=>richTextBoxPost.Text = "Type here a post!"));
            this.Invoke(new Action(() => this.AutoSize = true));
            this.Invoke(new Action(() => this.richTextBoxPost.Visible = true));
            this.Invoke(new Action(() => this.buttonPost.Visible = true));
            this.Invoke(new Action(() => this.buttonCancelPost.Visible = true));
            this.Invoke(new Action(() => this.checkBoxRememberUser.Visible = true));
            DummyGroup dummyGroup = new DummyGroup();
            m_LoggedInUser.Groups.Add(dummyGroup);
            dummyGroupBindingSource.DataSource = m_GroupTabFacade.GetUserDummyGroups(m_LoggedInUser);
        }


        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            pictureBoxProfile.Image = null;
            richTextBoxPost.Text = string.Empty;
        }


        private void loadProfileTab()
        {
            pictureBoxProfile.Invoke(new Action(()=> pictureBoxProfile.LoadAsync(m_ProfileTabFacade.GetProfilePicture(m_LoggedInUser))));
            pictureBoxProfileSmall.Invoke(new Action(()=> pictureBoxProfileSmall.LoadAsync(m_ProfileTabFacade.GetProfilePicture(m_LoggedInUser))));
            if(m_ProfileTabFacade.FetchCoverPhoto(m_LoggedInUser) != null)
            {
                pictureBoxCover.Invoke(new Action(()=> pictureBoxCover.LoadAsync(m_ProfileTabFacade.FetchCoverPhoto(m_LoggedInUser))));
            }
            fetchPosts(m_ProfileTabFacade.GetPosts(m_LoggedInUser), listViewPosts);
        }

        private void buttonPhotoUpload_Click(object sender, EventArgs e)
        {
            if(openFileDialogPhoto.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Uploaded successfully!");
            }
        }


        private void buttonCreateAlbum_Click(object sender, EventArgs e)
        {
            AlbumDialogBox dialogBox = new AlbumDialogBox();
            if(dialogBox.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("New album " + dialogBox.textBox1.Text + " was created!");
            }
        }


        private void loadAlbumTab()
        {
            foreach (Album album in m_AlbumTabFacade.GetUserAlbums(m_LoggedInUser))
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.LoadAsync(m_AlbumTabFacade.GetAlbumUrl(album));
                pictureBox.Text = m_AlbumTabFacade.GetAlbumName(album);
                pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.Size = new Size(200, flowLayoutPanelAlbums.Height);
                pictureBox.Click += pictureBoxAlbum_Click;
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
                pictureBox.Tag = album;
                this.Invoke(new Action(() => flowLayoutPanelAlbums.Controls.Add(pictureBox)));
            }
        }


        private void pictureBoxAlbum_Click(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = sender as PictureBox;
            resetAlbumBorders();
            selectedPictureBox.BorderStyle = BorderStyle.Fixed3D;
            Album selectedAlbum = selectedPictureBox.Tag as Album;
            buttonAddToAlbum.Enabled = true;
            resetPhotoPane();
            fetchAlbumPhotos(selectedAlbum);
        }


        private void resetPhotoPane()
        {
            flowLayoutPanelPhotos.Controls.Clear();
            labelPhotoCommentCount.Text = "0";
            labelPhotoLikeCount.Text = "0";
            listBoxPhotoComment.Items.Clear();
            listBoxPhotoLiked.Items.Clear();
        }


        private void fetchAlbumPhotos(Album i_SelectedAlbum)
        {
            foreach (Photo photo in i_SelectedAlbum.Photos)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.LoadAsync(m_AlbumTabFacade.GetPhotoUrl(photo));
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Size = new Size(200, flowLayoutPanelPhotos.Height);
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.Tag = photo;
                pictureBox.Click += pictureBoxPhoto_Click;
                if (m_AlbumTabFacade.GetPhotoName(photo) != null)
                {
                    pictureBox.Text = m_AlbumTabFacade.GetPhotoName(photo);
                }

                flowLayoutPanelPhotos.Controls.Add(pictureBox);
            }
        }


        private void pictureBoxPhoto_Click(object sender, EventArgs e)
        {
            PictureBox selectedPhoto = sender as PictureBox;
            resetPhotoBorders();
            selectedPhoto.BorderStyle = BorderStyle.Fixed3D;
            Photo photo = selectedPhoto.Tag as Photo;
            listBoxPhotoComment.Items.Clear();
            listBoxPhotoLiked.Items.Clear();

            fetchPhotoComments(m_AlbumTabFacade.GetPhotoComments(photo));
            fetchPhotoLikes(m_AlbumTabFacade.GetAmmountOfLikesOfPhoto(photo));
        }


        private void fetchPhotoComments(List<string> i_Comments)
        {
            labelPhotoCommentCount.Text = i_Comments.Count.ToString();
            foreach (string photoComment in i_Comments)
            {
                listBoxPhotoComment.Items.Add(photoComment);
            }
            if (listBoxPhotoComment.Items.Count == 0)
            {
                listBoxPhotoComment.Items.Add("No comments");
            }
        }


        private void fetchPhotoLikes(int i_PhotoLikes)
        {
            labelPhotoLikeCount.Text = i_PhotoLikes.ToString();
            listBoxPhotoLiked.Items.Add(m_LoggedInUser.Name);
        }


        private void resetAlbumBorders()
        {
            foreach(PictureBox pictureBox in flowLayoutPanelAlbums.Controls)
            {
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }


        private void resetPhotoBorders()
        {
            foreach (PictureBox pictureBox in flowLayoutPanelPhotos.Controls)
            {
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }


        private void fetchPosts(PostComponent i_PostComponent, ListView i_PostDestination)
        {
            i_PostDestination.Invoke(new Action(() => i_PostDestination.Items.Clear()));
            foreach (PostComponent component in m_ProfileTabFacade.GetPosts(m_LoggedInUser).GetComponentAsList())
            {
                List<string[]> postData = component.FetchData();
                ListViewItem item = new ListViewItem(postData[0]);
                item.Tag = component;
                i_PostDestination.Invoke(new Action(() => i_PostDestination.Items.Add(item)));
            }

            if(i_PostDestination.Items.Count == 0)
            {
                i_PostDestination.Invoke(new Action(()=> i_PostDestination.Items.Add("No posts!")));
            }
        }


        private void buttonPost_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Posted successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonPostCancel_Click(object sender, EventArgs e)
        {
            richTextBoxPost.Text = string.Empty;
        }

        private void richTextBoxPost_FocusEnter(object sender, EventArgs e)
        {
            richTextBoxPost.Text = string.Empty;
        }

        private void richTextBoxPost_FocusLeave(object sender, EventArgs e)
        {
            richTextBoxPost.Text = "Type here a post!";
        }


        private void listViewPosts_ItemActive(object sender, EventArgs e)
        {
            listBoxComments.Invoke(new Action(() => listBoxFeedComments.Items.Clear()));
            listBoxFeedLikes.Invoke(new Action(() => listBoxFeedLikes.Items.Clear()));
            PostComponent currentPost = getListViewActivePost((ListView)sender);
            fetchListItemPost(currentPost, listBoxComments, listBoxLikes);
        }


        private PostComponent getListViewActivePost(ListView i_ListView)
        {
            ListViewItem selectedItem = i_ListView.SelectedItems[0];
            PostComponent selectedPost = selectedItem.Tag as PostComponent;
            return selectedPost;
        }

        private void listViewFeed_ItemActive(object sender, EventArgs e)
        {
            listBoxFeedComments.Invoke(new Action(() => listBoxFeedComments.Items.Clear()));
            listBoxFeedLikes.Invoke(new Action(() => listBoxFeedLikes.Items.Clear()));
            PostComponent currentPost = getListViewActivePost((ListView)sender);
            fetchListItemPost(currentPost, listBoxFeedComments, listBoxFeedLikes);
        }


        private void loadFeedTab()
        {
            listViewFeed.Invoke(new Action(()=> listViewFeed.Items.Clear()));
            fetchPosts(m_NewsFeedTabFacade.GetPosts(m_LoggedInUser), listViewFeed);
        }


        private void loadGroupTab()
        {
            
        }
        

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            new Thread(() => r_TabCommands[index].Execute()).Start();
        }


        private void loadPostsByDateTab()
        {
            listViewPostsByDate.Invoke(new Action(() => listViewPostsByDate.Items.Clear()));
            listBoxCommentsByDate.Invoke(new Action(() => listBoxCommentsByDate.Items.Clear()));
        }
        

        private void buttonPostsByDateOK_Click(object sender, EventArgs e)
        {
            listBoxCommentsByDate.Invoke(new Action(() => listBoxCommentsByDate.Items.Clear()));
            listViewPostsByDate.Invoke(new Action(() => listViewPostsByDate.Items.Clear()));
            foreach (PostComponent component in m_PostsByDateTabFacade.GetPostsByDate(m_LoggedInUser,monthCalendarDatePost.SelectionStart.Date, monthCalendarDatePost.SelectionEnd.Date))
            {
                List<string[]> data = component.FetchDataByDate(monthCalendarDatePost.SelectionStart.Date, monthCalendarDatePost.SelectionEnd.Date);
                ListViewItem item = listViewPostsByDate.Items.Add(data[0][0]);
                item.SubItems.Add(data[0][1]);
                item.SubItems.Add(data[0][2]);
                item.SubItems.Add(data[0][3]);
                item.Tag = component;
            }

            if (listViewPostsByDate.Items.Count == 0)
            {
                listViewPostsByDate.Invoke(new Action(() => listViewPostsByDate.Items.Add("No posts!")));
            }
        }

        private void listViewPostsByDate_ItemActive(object sender, EventArgs e)
        {
            listBoxCommentsByDate.Invoke(new Action(() => listBoxCommentsByDate.Items.Clear()));
            listBoxLikesByDate.Invoke(new Action(() => listBoxLikesByDate.Items.Clear()));
            PostComponent currentPost = getListViewActivePost((ListView)sender);
            fetchListItemPost(currentPost, listBoxCommentsByDate, listBoxLikesByDate);
        }


        private void fetchListItemPost(PostComponent i_SelectedPost, ListBox i_Comments, ListBox i_Likes)
        {
            List<string> comments = i_SelectedPost.FetchComments();
            foreach (string comment in comments)
            {
                i_Comments.Items.Add(comment);
            }

            List<string> likes = i_SelectedPost.FetchLikes();
            foreach (string like in likes)
            {
                i_Likes.Items.Add(like);
            }
        }


        private void buttonCheckCountryGame_Click(object sender, EventArgs e)
        {
            if (m_countryGuessGame.CheckIfCorrect(textBoxCountryGuess.Text.ToLower().Trim()))
            {
                if(m_countryGuessGame.IsGameWon())
                {
                    labelGameMessages.Text = "You have won!";
                    buttonNextCountryGame.Visible = false;
                    buttonNewGame.Visible = true;
                    buttonCheckCountryGame.Enabled = false;
                }
                else
                {
                    labelGameMessages.Text = "Correct!";
                    buttonNextCountryGame.Visible = true;
                    buttonCheckCountryGame.Enabled = false;
                }
                labelScore.Text = m_countryGuessGame.GetPlayerScore().ToString();
            }
            else
            {
                if (m_countryGuessGame.GetTriesLeft() > 0)
                {
                    labelGameMessages.Text = "Wrong guess, Try again";
                }
                else
                {
                    labelGameMessages.Text = "Game Over, The country was: " + m_countryGuessGame.GetRightAnswer();
                    buttonNewGame.Visible = true;
                    buttonCheckCountryGame.Enabled = false;
                    textBoxCountryGuess.Enabled = false;
                }
                labelTriesLeft.Text = m_countryGuessGame.GetTriesLeft().ToString();
            }
        }

        private void buttonNextCountryGame_Click(object sender, EventArgs e)
        {
            buttonCheckCountryGame.Enabled = true;
            m_countryGuessGame.NextRound();
            newGameRound();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            buttonNewGame.Visible = false;
            buttonCheckCountryGame.Enabled = true;
            m_countryGuessGame.NewGame();
            setGame();
            newGameRound();
        }


        private void newGameRound()
        {
            if(m_countryGuessGame.IsGameWon())
            {
                labelGameMessages.Text = "You have won!";
                buttonNextCountryGame.Visible = false;
                buttonNewGame.Visible = true;
                buttonCheckCountryGame.Enabled = false;
            }
            else
            {
                pictureBoxGamePicture.LoadAsync(m_countryGuessGame.GetCurrentLevelDataToShow());
                buttonNextCountryGame.Visible = false;
                labelTriesLeft.Text = m_countryGuessGame.GetTriesLeft().ToString();
                textBoxCountryGuess.Text = "";
            }
        }


        private void setGame()
        {
            labelTriesLeft.Text = m_countryGuessGame.GetTriesLeft().ToString();
            labelScore.Text = m_countryGuessGame.GetPlayerScore().ToString();
            labelGameMessages.Text = "";
            textBoxCountryGuess.Text = "";
            buttonNextCountryGame.Visible = false;
            pictureBoxGamePicture.Image = null;
            textBoxCountryGuess.Enabled = true;
            buttonCheckCountryGame.Enabled = true;
        }

        private void loadGuessCountryGame()
        {
            if(m_countryGuessGame != null)
            {
                m_countryGuessGame.NewGame();
            }
            else
            {
                m_countryGuessGame = new CountryGuessGame(m_LoggedInUser, 3);
            }
        }

    }
}
