using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordForWord.Logic
{
    class WordCheck
    {
        //private List<string> strings;
        private HashSet<string> strings;
        private Database db = new Database();

        public WordCheck()
        {
            strings = new HashSet<string>();
            //string[] vacab = new string[] { "село", "гол", "лаг", "стог", "вол", "гот", "сел", "сало", "селова", "лесть" };
            ////string[] vacab = new string[] { "село", "сел", "селова" };
            //foreach (var item in vacab)
            //{
            //    strings.Add(item); // загружать словарь из файла
            //}

            var lines = System.IO.File.ReadAllLines(@"c:\Users\max\Documents\Visual Studio 2015\Projects\WordForWord\WordForWord\bin\lop2v2.txt");
            foreach (var line in lines)
            {
                //if (line.Length > 1)
                {
                    strings.Add(line);
                }
            }
        }

        public enum SearchStatus
        {
            FoundAWord,
            FoundAWordAndParts,
            FoundParts,
            NotFound
        }

        public SearchStatus ContainsWordOrPart(string str)
        {
            int count = 0;
            bool hasFullWord = false;
            //count = db.GetParts(str);
            //hasFullWord = db.GetWord(str); // не очень быстро, надо автоматизировать

            foreach (var word in strings)
            {
                if (word.StartsWith(str))
                {
                    count++;
                }
                if (word.Equals(str))
                {
                    hasFullWord = true;
                }
            }

            if (hasFullWord && count == 1)
            {
                return SearchStatus.FoundAWord;
            }
            else if (hasFullWord && count > 1)
            {
                return SearchStatus.FoundAWordAndParts;
            }
            else if (!hasFullWord && count >= 1)
            {
                return SearchStatus.FoundParts;
            }

            return SearchStatus.NotFound; // есть ничего
        }
    }
}
