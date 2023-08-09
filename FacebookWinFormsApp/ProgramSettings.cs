using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BasicFacebookFeatures
{
    public class ProgramSettings
    {
        public Point LastWindowLocation { get; set; }

        public Size LastWindowsSize { get; set; }

        public bool RememberUser { get; set; }

        public string LastAccessToken { get; set; }

        public FormWindowState LastWindowState { get; set; }

        public static string m_FileName = "ProgramSettings.xml";

        private ProgramSettings()
        {
            LastAccessToken = string.Empty;
            RememberUser = false;
            LastWindowsSize = new Size(858, 696);
            LastWindowLocation = new Point(254, 36);
            LastWindowState = FormWindowState.Normal;
        }

        public void SaveToFile()
        {
            FileMode fileMode = FileMode.Create;
            if(File.Exists(m_FileName))
            {
                fileMode = FileMode.Truncate;
            }

            using(Stream fileStream = new FileStream(m_FileName, fileMode))
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(fileStream, this);
            }
        }

        public static ProgramSettings LoadFromFile()
        {
            ProgramSettings programSettings;
            try
            {
                Stream fileStream = new FileStream(m_FileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
                programSettings = serializer.Deserialize(fileStream) as ProgramSettings;
            }
            catch(Exception e)
            {
                programSettings = new ProgramSettings();
            }

            return programSettings;
        }
    }
}
