using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlates.Tx.Configuration
{
    class Suggestion : IComparable
    {
        private string _suggestedText;
        private string _originalText;

        internal string SuggestedText
        {
            get { return _suggestedText; }
            set { _suggestedText = value; }
        }

        internal string OriginalText
        {
            get { return _originalText; }
            set { _originalText = value; }
        }

        internal int DifferentChars
        {
            get
            {
                int num = 0;
                for (int i = 0; i < _originalText.Length; i++)
                {
                    if (!_originalText[i].ToString().Equals(_suggestedText[i].ToString()))
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public Suggestion(string originalText, string suggestedText)
        {
            _originalText = originalText;
            _suggestedText = suggestedText;
        }

        int IComparable.CompareTo(object obj)
        {
            Suggestion sug = (Suggestion)obj;

            if (this.DifferentChars > sug.DifferentChars)
                return 1;
            if (this.DifferentChars < sug.DifferentChars)
                return -1;
            else
                return 0;
        }
    }
}
