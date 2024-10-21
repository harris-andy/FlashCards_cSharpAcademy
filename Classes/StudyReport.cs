using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy.Classes
{
    public class StudyReport<T> where T : struct
    {
        public string StackName { get; set; }
        public T January { get; set; }
        public T February { get; set; }
        public T March { get; set; }
        public T April { get; set; }
        public T May { get; set; }
        public T June { get; set; }
        public T July { get; set; }
        public T August { get; set; }
        public T September { get; set; }
        public T October { get; set; }
        public T November { get; set; }
        public T December { get; set; }

        public StudyReport(
            string stackName,
            T january,
            T february,
            T march,
            T april,
            T may,
            T june,
            T july,
            T august,
            T september,
            T october,
            T november,
            T december)
        {
            StackName = stackName;
            January = january;
            February = february;
            March = march;
            April = april;
            May = may;
            June = june;
            July = july;
            August = august;
            September = september;
            October = october;
            November = november;
            December = december;
        }
    }
}