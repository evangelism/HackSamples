using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmoRecognizer
{

    public class EmoCollectionRecord
    {
        public string Emotion { get; set; }
        public double Value { get; set; }

        public int Value500 { get { return (int)(Value * 500); } }
    }
    public class EmoCollection
    {
        public ObservableCollection<EmoCollectionRecord> Emotions { get; set; }
        public EmoCollection(Scores x)
        {
            Emotions = new ObservableCollection<EmoCollectionRecord>();
            Update(x);
        }

        public EmoCollection()
        {
            Emotions = new ObservableCollection<EmoCollectionRecord>();
        }

        public void Update(Scores x)
        {
            Emotions.Clear();
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Happiness", Value = x.Happiness });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Anger", Value = x.Anger });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Contempt", Value = x.Contempt });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Disgust", Value = x.Disgust });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Fear", Value = x.Fear, });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Sadness", Value = x.Sadness });
            Emotions.Add(new EmoCollectionRecord() { Emotion = "Surprise", Value = x.Surprise });
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            foreach(var x in Emotions)
            {
                b.Append($"{x.Emotion}: {x.Value,7:5N}");
            }
            return b.ToString();
        }

    }
}
