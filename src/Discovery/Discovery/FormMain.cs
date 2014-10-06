using System;
using System.Configuration;
using System.Windows.Forms;

namespace Discovery
{
    public partial class FormMain : Form
    {
			private readonly string TemporaryPath;
			private readonly string TransformationsPath;
			private readonly string DataPath;

        public FormMain()
        {
            InitializeComponent();

						TemporaryPath = ConfigurationManager.AppSettings["TemporaryPath"];
						TransformationsPath = ConfigurationManager.AppSettings["TransformationsPath"];
						DataPath = ConfigurationManager.AppSettings["DataPath"];
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            //*****
            var url = new Uri(TextBoxAddress.Text);

						//*****
						var pipeline = new Discovery.Library.Pipeline(TransformationsPath, string.Format(@"{0}uri.xml", DataPath));
						var result = pipeline.Process(url);
						
						//*****
						var protocolFileName = string.Format(@"{0}{1}.xml", TemporaryPath, Guid.NewGuid());
						System.IO.File.WriteAllText(protocolFileName, result["Protocol"].Contents);
						var specifyFileName = string.Format(@"{0}{1}.xml", TemporaryPath, Guid.NewGuid());
						System.IO.File.WriteAllText(specifyFileName, result["Specify"].Contents);
						var displayFileName = string.Format(@"{0}{1}.html", TemporaryPath, Guid.NewGuid());
						System.IO.File.WriteAllText(displayFileName, result["Display"].Contents);

						//*****
						TextBoxResponseRaw.Text = result["Raw"].Contents;
						WebBrowserResponse.Navigate(string.Format("file:///{0}", protocolFileName.Replace(@"\", "/")));
						WebBrowserSpecify.Navigate(string.Format("file:///{0}", specifyFileName.Replace(@"\", "/")));
						WebBrowserDisplay.Navigate(string.Format("file:///{0}", displayFileName.Replace(@"\", "/")));

        }

        private void TextBoxAddress_Enter(object sender, EventArgs e)
        {
            TextBoxAddress.SelectAll();
        }

        private void TextBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ButtonGo_Click(sender, new EventArgs());
        }

        private void WebBrowserHTML_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowserDisplay.Tag = false;
            TextBoxResource.Text = e.Url.PathAndQuery;
        }
    }
}
