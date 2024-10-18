using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy
{
    public class Stack
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public Stack(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}