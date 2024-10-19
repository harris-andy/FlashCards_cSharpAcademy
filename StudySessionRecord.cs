using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy
{
    public class StudySessionRecord
    {
        public DateTime Date { get; set; }
        public float Score { get; set; }
        public int StackID { get; set; }

        public StudySessionRecord(DateTime date, float score, int stackID)
        {
            Date = date;
            Score = score;
            StackID = stackID;
        }
    }
}