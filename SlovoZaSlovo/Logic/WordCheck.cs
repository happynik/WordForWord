using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlovoZaSlovo.Logic
{
    class WordCheck
    {
        List<string> strings;

        public WordCheck()
        {
            strings = new List<string>();
            string[] vacab = new string[] { "село", "гол", "лаг", "стог", "вол", "гот", "сел", "сало", "селова", "лесть" };
            //string[] vacab = new string[] { "село", "сел", "селова" };
            foreach (var item in vacab)
            {
                strings.Add(item);
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
