﻿using System.Text.RegularExpressions;

class Person
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public float[] HomeworkScores { get; set; }
    public float ExamScore { get; set; }
    public float FinalAverageGrade { get; set; }
    public float FinalMedianGrade { get; set; }

    public Person(string firstName, string surname, float[] homeworkScores, float examScore)
    {
        FirstName = firstName;
        Surname = surname;
        HomeworkScores = homeworkScores;
        ExamScore = examScore;
        CalculateFinalGrades();
    }

    public Person(Person other)
    {
        FirstName = other.FirstName;
        Surname = other.Surname;
        HomeworkScores = (float[])other.HomeworkScores.Clone();
        ExamScore = other.ExamScore;
        FinalAverageGrade = other.FinalAverageGrade;
        FinalMedianGrade = other.FinalMedianGrade;
    }

    public Person Assign(Person other)
    {
        if (this != other)
        {
            FirstName = other.FirstName;
            Surname = other.Surname;
            HomeworkScores = (float[])other.HomeworkScores.Clone();
            ExamScore = other.ExamScore;
            FinalAverageGrade = other.FinalAverageGrade;
            FinalMedianGrade = other.FinalMedianGrade;
        }
        return this;
    }

    ~Person() { }

    private void CalculateFinalGrades()
    {
        float average = HomeworkScores.Average();
        float median = GetMedian(HomeworkScores);
        FinalAverageGrade = (0.4f * average) + (0.6f * ExamScore);
        FinalMedianGrade = (0.4f * median) + (0.6f * ExamScore);
    }

    private float GetMedian(float[] numbers)
    {
        var sortedNumbers = numbers.OrderBy(n => n).ToArray();
        int middleIndex = sortedNumbers.Length / 2;

        if (sortedNumbers.Length % 2 == 0)
        {
            return (sortedNumbers[middleIndex - 1] + sortedNumbers[middleIndex]) / 2;
        }

        return sortedNumbers[middleIndex];
    }

    public static Person InputData(string inputLine)
    {
        string[] row = Regex.Split(inputLine, @"\s+");
        string firstName = row[0];
        string surname = row[1];
        float[] homeworkScores = row.Skip(2).Take(row.Length - 2).Select(float.Parse).ToArray();
        float examScore = float.Parse(row[row.Length - 1]);

        return new Person(firstName, surname, homeworkScores, examScore);
    }

    public void addToGrid(DataGridView grid)
    {
        grid.Rows.Add(FirstName, Surname, FinalAverageGrade.ToString("F2"), FinalMedianGrade.ToString("F2"));
    }
}
