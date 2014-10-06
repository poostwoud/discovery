using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Discovery
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            //*****
            var url = new Uri(TextBoxAddress.Text);

            //*****
            switch (url.Scheme.ToLower())
            {
                case "http":
                case "https":
                    HandleHTTPProtocol(url);
                    break;
                default:
                    MessageBox.Show(string.Format("Protocol {0} not supported", url.Scheme), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void TextBoxAddress_Enter(object sender, EventArgs e)
        {
            TextBoxAddress.SelectAll();
        }

        private void TextBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ButtonGo_Click(sender, new EventArgs());
        }

        private void HandleHTTPProtocol(Uri iri)
        {
            //***** Response;
            var http = new Library.Protocol.HTTPProtocol();
            var response = http.Read(iri);
            var fileName = string.Format(@"c:\temp\{0}.xml", Guid.NewGuid());
            System.IO.File.WriteAllText(fileName, response.Result);
            WebBrowserResponse.Navigate(string.Format(@"file:///{0}", fileName));
            TextBoxResponseRaw.Text = response.Raw;

            //***** Specify;
            //***** TODO:Use XSLT;
            //***** http://www.codeproject.com/Articles/24299/XML-String-Browser-just-like-Internet-Explorer-usi
            var transformation = System.IO.File.ReadAllText(@"D:\Github\discovery\src\Discovery\Discovery\Transformations\http\haljson\specify.xslt");
            Exception exception;
            string output;
            Library.Transformation.XSLHelper.Transform(response.Result, transformation, out output, out exception);
            fileName = string.Format(@"c:\temp\{0}.xml", Guid.NewGuid());
            System.IO.File.WriteAllText(fileName, output);
            WebBrowserSpecify.Navigate(string.Format(@"file:///{0}", fileName));

            //***** HTML;
            transformation = System.IO.File.ReadAllText(@"D:\Github\discovery\src\Discovery\Discovery\Transformations\http\haljson\html.xslt");
            Library.Transformation.XSLHelper.Transform(output, transformation, out output, out exception);
            fileName = string.Format(@"c:\temp\{0}.html", Guid.NewGuid());
            System.IO.File.WriteAllText(fileName, output);
            WebBrowserHTML.Tag = true;
            WebBrowserHTML.Navigate(string.Format(@"file:///{0}", fileName));
        }

        private void WebBrowserHTML_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (Convert.ToBoolean(WebBrowserHTML.Tag) == false)
            {
                e.Cancel = true;
                if (e.Url.Scheme == "file")
                {
                    var path = new string[e.Url.Segments.Length - 2];
                    for (var idx = 2; idx < e.Url.Segments.Length; idx++)
                        path[idx - 2] = e.Url.Segments[idx];
                    HandleHTTPProtocol(new Uri(string.Format("{0}{1}", TextBoxAddress.Text, string.Join("/", path)).ToLower()));
                }
            }
        }

        private void WebBrowserHTML_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowserHTML.Tag = false;
            label3.Text = string.Format("Presentation ({0})", DateTime.Now.ToString("HH:mm:ss.fff"));
            TextBoxResource.Text = e.Url.PathAndQuery;
        }

        private void WebBrowserResponse_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            label1.Text = string.Format("Response ({0})", DateTime.Now.ToString("HH:mm:ss.fff"));
        }

        private void WebBrowserSpecify_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            label2.Text = string.Format("Specify ({0})", DateTime.Now.ToString("HH:mm:ss.fff"));
        }
    }
}
