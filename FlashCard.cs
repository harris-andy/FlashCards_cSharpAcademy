using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy
{
    public record FlashCard
    {
        public string Front { get; set; }
        public string Back { get; set; }

        public FlashCard(string front, string back)
        {
            Front = front;
            Back = back;
        }
    }
}