using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace Lab7_service
{
    public class FeedService : IFeedService
    {
        public SyndicationFeedFormatter GetStudentNotes(string studentId)
        {
            SyndicationFeed feed = new SyndicationFeed("Subjects & Notes", "Get list of notes by all subjects for this student", null);
            List<SyndicationItem> items = new List<SyndicationItem>();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://localhost:44353/WcfDataService1.svc/Note?$filter=(StudentId%20eq%20" + studentId + ")&$format=json");
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseString = reader.ReadToEnd();
            var notesResp = JsonConvert.DeserializeObject<NoteResponse>(responseString);
            var notes = notesResp.Value;
            foreach (var note in notes)
            {
                items.Add(new SyndicationItem(note.Subj, note.Note1.ToString(), null));
            }
            feed.Items = items;

            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom")
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                formatter = new Rss20FeedFormatter(feed);
            }
            return formatter;
        }
    }
}
