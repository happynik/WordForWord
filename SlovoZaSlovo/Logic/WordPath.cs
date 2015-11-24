using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordForWord.Logic
{
    class WordPath
    {
        private Stack<LetterInWord> _wordPath = new Stack<LetterInWord>();
        private Stack<CoOrd> _coordPath = new Stack<CoOrd>(); // get set

        public void PushLetter(LetterInWord letterClass)
        {
            _wordPath.Push(letterClass);
            _coordPath.Push(letterClass._letterCoord);
        }

        public void PopLetter()
        {
            _wordPath.Pop();
            _coordPath.Pop();
        }

        public int LastLetterIndex()
        {
            return _wordPath.Peek()._index;
        }

        public CoOrd GetAndPopNeighborhood()
        {
            // not null check
            var tempNeighborhoods = new List<CoOrd>(_wordPath.Peek()._neighborhoods);

            CoOrd neighborhood = tempNeighborhoods.First();
            _wordPath.Peek().PopNeighborhood(neighborhood);
            return neighborhood;
        }

        public bool HaveNeighborhood()
        {
            if (_wordPath.Peek()._neighborhoods.Count > 0)
            {
                return true;
            }
            return false;
        }

        public string GetWord()
        {
            string tempWord = "";
            foreach (var letter in _wordPath.Reverse())
            {
                tempWord += letter._letter;
            }
            return tempWord;
        }

        public bool ContainsCoord(CoOrd coord)
        {
            if (_coordPath.Contains(coord))
            {
                return true;
            }
            return false;
        }
    }

    class LetterInWord
    {
        public int _index { get; }
        public CoOrd _letterCoord { get; }
        public char _letter { get; }
        public List<CoOrd> _neighborhoods { get; }

        public LetterInWord(int index, CoOrd letterCoord, char letter, List<CoOrd> neighborhood)
        {
            _index = index;
            _letterCoord = letterCoord;
            _letter = letter;
            _neighborhoods = neighborhood;
        }

        public void PopNeighborhood(CoOrd coord)
        {
            _neighborhoods.Remove(coord);
        }
    }
}
