using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flashcards.harris_andy.Classes
{
    public class StudyReport
    {
        public string StackName { get; set; }
        public object January { get; set; }
        public object February { get; set; }
        public object March { get; set; }
        public object April { get; set; }
        public object May { get; set; }
        public object June { get; set; }
        public object July { get; set; }
        public object August { get; set; }
        public object September { get; set; }
        public object October { get; set; }
        public object November { get; set; }
        public object December { get; set; }

        // public StudyReport() { }

        public StudyReport(
            string stackName,
            object january,
            object february,
            object march,
            object april,
            object may,
            object june,
            object july,
            object august,
            object september,
            object october,
            object november,
            object december)
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