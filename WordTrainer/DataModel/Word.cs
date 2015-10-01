using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace WordTrainer.DataModel
{
    public class Word
    {
        protected BitmapImage _pic;

        public string Name { get; set; }
        public string Category { get; set; }
        public BitmapImage Pic { get { return _pic; }  }
        public string PicUrl
        {
            set
            {
                _pic = new BitmapImage(new Uri(String.Format("ms-appx:/Pics/{0}", value)));
            }
        }

        public Word(XElement x)
        {
            Name = x.Attribute("Name").Value;
            Category = x.Attribute("Category").Value;
            PicUrl = x.Attribute("Pic").Value;
        }

        public static XDocument doc;
        public static Word[] GetWords(string cat)
        {
            if (doc == null) doc = XDocument.Load(@"Data\PictDict.xml");
            var res = cat == "*" ?
                from x in doc.Descendants("Word")
                select new Word(x)
                :
                from x in doc.Descendants("Word")
                where x.Attribute("Category").Value == cat
                select new Word(x);
            return res.ToArray<Word>();
        }


        public static Word[] GetWordSample(string cat, int p)
        {
            var words = GetWords(cat);
            List<Word> l = new List<Word>();
            Word w;
            for (int i = 0; i < p; i++)
            {
                w = null;
                while (w == null) w = Helpers.PickAndReplace(words, null);
                l.Add(w);
            }
            return l.ToArray<Word>();
        }
    }

}
