using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy.Classes
{
    public class StudySessionReport
    {
        public string StackName { get; set; }
        public float January { get; set; }
        public float February { get; set; }
        public float March { get; set; }
        public float April { get; set; }
        public float May { get; set; }
        public float June { get; set; }
        public float July { get; set; }
        public float August { get; set; }
        public float September { get; set; }
        public float October { get; set; }
        public float November { get; set; }
        public float December { get; set; }

        public StudySessionReport(
            string stackName,
            float january,
            float february,
            float march,
            float april,
            float may,
            float june,
            float july,
            float august,
            float september,
            float october,
            float november,
            float december)
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