namespace Flashcards.harris_andy.Classes;

public class StudyReportCounts
{
    public string StackName { get; set; }
    public int January { get; set; }
    public int February { get; set; }
    public int March { get; set; }
    public int April { get; set; }
    public int May { get; set; }
    public int June { get; set; }
    public int July { get; set; }
    public int August { get; set; }
    public int September { get; set; }
    public int October { get; set; }
    public int November { get; set; }
    public int December { get; set; }

    public StudyReportCounts(
        string stackName,
        int january,
        int february,
        int march,
        int april,
        int may,
        int june,
        int july,
        int august,
        int september,
        int october,
        int november,
        int december)
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