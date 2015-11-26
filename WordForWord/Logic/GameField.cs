using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordForWord.Logic
{
    class GameField
    {
        private MainWindow _MW;
        private char[][] _field;
        //private int[][] _fieldUsed;
        private int _x;
        private int _y;

        private int _index = 0;

        //private Stack<CoOrd> _path = new Stack<CoOrd>();
        private WordPath _pathOfword;// = new WordPath();
        private WordCheck _wordCheck = new WordCheck();
        private List<string> _allWords = new List<string>();

        public GameField(char[][] inputArray, MainWindow MW)
        {
            _MW = MW;

            _field = inputArray;
            _x = _field.Length;
            _y = _field[0].Length;

            //_fieldUsed = new int[_x][];
            //for (int i = 0; i < _y; i++)
            //{
            //    _fieldUsed[i] = new int[_x];
            //}
        }

        public void ArrayDetour()
        {
            List<Thread> thread = new List<Thread>(); ;
            for (int x = 0; x < _x; x++)
            {
                
                for (int y = 0; y < _y; y++)
                {
                    //_wordPath = new WordPath();
                    //GraphDetour(new CoOrd(x, y));
                    object coord = new CoOrd(x, y);
                    //Thread t = new Thread(new ThreadStart(GraphDetour(coord));
                    thread.Add(new Thread(GraphDetour));
                    thread.Last().Start(coord);

                    //int status = GraphDetour(new CoOrd(0, 0));

                    //_MW.FieldUpdate(_path);
                    //return;
                }
            }
        }


        public void GraphDetour(object a_charCoOrd)
        {
            _pathOfword = new WordPath();
            var charCoOrd = (CoOrd)a_charCoOrd;
            var allNeighborgoods = GetCellNeighborhoods((CoOrd)charCoOrd);
            
            _pathOfword.PushLetter(new LetterInWord(0, charCoOrd, _field[charCoOrd.x][charCoOrd.y], allNeighborgoods));
            Thread.Sleep(0);

            var status = _wordCheck.ContainsWordOrPart(_pathOfword.GetWord());
            if (status == WordCheck.SearchStatus.NotFound)
            {
                return;
            }

            var flag = true;
            int level = 0;
            while (flag)
            {
                if (_pathOfword.HaveNeighborhood())
                {
                    _index++;
                    var nextStep = _pathOfword.GetAndPopNeighborhood();

                    allNeighborgoods = GetCellNeighborhoods(nextStep);

                    level++;
                    _pathOfword.PushLetter(new LetterInWord(level, nextStep, _field[nextStep.x][nextStep.y], allNeighborgoods));

                    //Console.WriteLine("Step > " + nextStep + " and word > " + _wordPath.GetWord());
                    var checkStatus = _wordCheck.ContainsWordOrPart(_pathOfword.GetWord());
                    if (checkStatus == WordCheck.SearchStatus.NotFound)  // нужна ограничение, если слово уже найдено. Данная проверка работает плохо...
                    {
                        while (_pathOfword.HaveNeighborhood())
                        {
                            nextStep = _pathOfword.GetAndPopNeighborhood();
                        }
                    }else if (checkStatus == WordCheck.SearchStatus.FoundAWordAndParts 
                        || (checkStatus == WordCheck.SearchStatus.FoundAWord))
                    {
                        var tempWord = _pathOfword.GetWord();
                        if (!_allWords.Contains(tempWord))
                        {
                            _allWords.Add(tempWord);
                            Console.WriteLine(">>> Have a word: " + _allWords.Last());
                        }
                    }

                } else
                {
                    _pathOfword.PopLetter();
                    level--;
                    if (level == -1)
                    {
                        flag = false;
                    }
                }
            }

            Console.WriteLine(">>> Count of iterations: " + _index);

            return;
        }

        public List<CoOrd> GetCellNeighborhoods(CoOrd coOrd)
        {
            List<CoOrd> allNeighborhoods = new List<CoOrd>();

            int x;
            int y;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    x = coOrd.x;
                    y = coOrd.y;
                    x += i;
                    y += j;

                    if (_pathOfword.ContainsCoord(new CoOrd(x, y)))
                    {
                        continue;
                    }

                    if ((x >= 0 && x < _x) && (y >= 0 && y < _y))
                    {
                        //Console.WriteLine("GetCellNeighborhoods: ({0};{1})", x, y);
                        allNeighborhoods.Add(new CoOrd(x, y));
                    }

                }
            }
            allNeighborhoods.Remove(new CoOrd(coOrd.x, coOrd.y));
            return allNeighborhoods;
        }

    }
}

