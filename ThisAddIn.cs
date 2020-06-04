using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using InteropWord = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

namespace WordWorkProgressTracker
{
    public partial class ThisAddIn
    {
        private string diff = "";
        private InteropWord.Document _openDoc;
        private System.Threading.Timer _timer;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // Load content to produce diffs on
            // Set up timer to run every n minutes

            this.Application.DocumentBeforeSave += Application_DocumentBeforeSave;
            this.Application.DocumentOpen += Application_DocumentOpen;
            this.Application.DocumentBeforeClose += Application_DocumentBeforeClose;
            ((InteropWord.ApplicationEvents4_Event) this.Application).NewDocument += Application_NewDocument;

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Save our format to a file
        }

        private void Application_DocumentOpen(InteropWord.Document doc)
        {
            _openDoc = doc;
            _timer = new System.Threading.Timer((e) =>
            {
                SaveContent();
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        }

        private void Application_NewDocument(InteropWord.Document doc)
        {
            _openDoc = doc;
            _timer = new System.Threading.Timer((e) =>
            {
                SaveContent();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        }

        private string Diff(string a, string b)
        {
            return a;
        }

        private void Application_DocumentBeforeSave(InteropWord.Document doc, ref bool saveAsUi, ref bool cancel)
        {
            SaveContent();
        }

        private void Application_DocumentBeforeClose(InteropWord.Document doc, ref bool cancel)
        {
            _timer.Dispose();
            _openDoc = null;
        }

        private void SaveContent()
        {
            try
            {
                string content = _openDoc.Content.Text;
                diff = Diff(content, diff);

                byte[] data = System.Text.Encoding.ASCII.GetBytes(diff);
                string base64Encoded = $"<WWPTContent timestamp=\"{DateTime.UtcNow}\">" +
                                       System.Convert.ToBase64String(data) + "</WWPTContent>";

                _openDoc.CustomXMLParts.Add(base64Encoded);
            }
            catch (COMException)
            {

            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
