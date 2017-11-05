using System.Linq;
using Assignment1;

namespace Assignment4
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            string[] result = intArray
                .GroupBy(n => n)
                .OrderBy(group => group.Key)
                .Select(group => "Broj " + group.Key + " ponavlja se " + group.Count() + " puta")
                .ToArray();
            return result;
        }

        public static University[] Linq2_1(University[] universityArray)
        {
            University[] result = universityArray
                .Where(univ => univ.Students.Length == univ.Students
                .Count(stud => stud.Gender == Gender.Male))
                .ToArray();
            return result;
        }

        public static University[] Linq2_2(University[] universityArray)
        {
            University[] result = universityArray
                .Where(univ =>
                univ.Students.Length < universityArray
                                .Select(un => un.Students.Length)
                                .Average())
                .ToArray();
            return result;
        }

        public static Student[] Linq2_3(University[] universityArray)
        {
            Student[] result = universityArray
                .SelectMany(univ => univ.Students)
                .Distinct()
                .ToArray();
            return result;
        }

        public static Student[] Linq2_4(University[] universityArray)
        {
            Student[] result = universityArray
                .Where(univ => univ.Students.Length == univ.Students
                                   .Count(stud => stud.Gender == Gender.Male) ||
                               univ.Students.Length == univ.Students
                                   .Count(stud => stud.Gender == Gender.Female))
                .SelectMany(univ => univ.Students)
                .Distinct()
                .ToArray();
            return result;
        }

        public static Student[] Linq2_5(University[] universityArray)
        {
            Student[] result = universityArray
                .SelectMany(univ => univ.Students)
                .GroupBy(stud => stud)
                .Where(group => group.Count() >= 2)
                .Select(group => group.Key)
                .ToArray();
            return result;
        }
    }
}