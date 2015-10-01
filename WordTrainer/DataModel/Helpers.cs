using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTrainer.DataModel
{
    public class Helpers
    {

        protected static Random rnd = new Random();

        public static T Pick<T>(T[] Choices)
        {
            var n = rnd.Next(0, Choices.Length);
            return Choices[n];
        }

        public static T PickAndReplace<T>(T[] Choices, T value)
        {
            var n = rnd.Next(0, Choices.Length);
            var x = Choices[n];
            Choices[n] = value;
            return x;
        }

    }
}
