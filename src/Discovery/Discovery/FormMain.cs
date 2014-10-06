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
			private readonly Library.Pipeline Pipeline;
        
				public FormMain()
        {
            InitializeComponent();

						TemporaryPath = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["TemporaryPath"]);
						TransformationsPath = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["TransformationsPath"]);
						DataPath = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["DataPath"]);
						Pipeline = new Discovery.Library.Pipeline(TransformationsPath, string.Format(@"{0}uri.xml", DataPath));
        }

				private void Process(Uri uri)
				{
					//*****
					var result = Pipeline.Process(uri);

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

					//*****
					WebBrowserDisplay.Tag = true;
					WebBrowserDisplay.Navigate(string.Format("file:///{0}", displayFileName.Replace(@"\", "/")));
				}

        private void ButtonGo_Click(object sender, EventArgs e)
        {
           //*****
           Process(new Uri(TextBoxAddress.Text));
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

				private void WebBrowserDisplay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
				{
					if (Convert.ToBoolean(WebBrowserDisplay.Tag) == false)
					{
						e.Cancel = true;
						if (e.Url.Scheme == "file")
						{
							var path = new string[e.Url.Segments.Length - 2];
							for (var idx = 2; idx < e.Url.Segments.Length; idx++)
								path[idx - 2] = e.Url.Segments[idx];
							Process(new Uri(string.Format("{0}{1}", TextBoxAddress.Text, string.Join("/", path)).ToLower()));
						}
					}
				}
    }
}
