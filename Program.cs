using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }

    public static void Main()
    {
        // Student collection
        IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };
        //a) Group by GPA and display the student's IDs
        Console.WriteLine("-------------\nGROUP BY GPA\n-------------");
        var groupedGPA = studentGPAList.GroupBy(g => g.GPA);
        foreach (var g in groupedGPA)
        {
            Console.WriteLine("GPA Group: " + g.Key);
            foreach (StudentGPA n in g)
            {
                Console.WriteLine("Student ID: " + n.StudentID);
            }
            Console.WriteLine();
        }

        //b) Sort by Club, then group by Club and display the student's IDs
        Console.WriteLine("-------------\nGROUP BY CLUB\n-------------");
        var clubSort = studentClubList.OrderBy(cn => cn.ClubName).GroupBy(c => c.ClubName);
        foreach(var club in clubSort)
        {
            Console.WriteLine("Club Name: " +  club.Key);
            foreach(StudentClubs s in club)
            {
                Console.WriteLine("Student ID: " + s.StudentID);
            }
            Console.WriteLine();
        }

        //c) Count the number of students with a GPA between 2.5 and 4.0
        Console.WriteLine();
        Console.WriteLine("-------------\nGPA COUNT\n-------------");
        var gpaCount = studentGPAList.Count(g => g.GPA > 2.5 && g.GPA < 4.0);
        Console.WriteLine("Number of students with GPA between 2.5 and 4.0 (exclusive): " + gpaCount);

        //d) Average all student's tuition
        Console.WriteLine();
        Console.WriteLine("-------------\nAVERAGE TUITION\n-------------");
        var average = studentList.Average(t=>t.Tuition);
        Console.WriteLine("The average of all students' tuition is: " + String.Format("{0:C}",average));

        //e) Find the student paying the most tuition and display their name, major and tuition.HINT: You will need to retrieve and store the highest tuition, then use a foreach loop to iterate through the studentList comparing each student's tuition to the highest. If they are equal, print out the data.
        Console.WriteLine();
        Console.WriteLine("-------------\nMAX TUITION\n-------------");
        var maxT = studentList.Max(m => m.Tuition);
        foreach( var t in studentList)
        {
            if(t.Tuition == maxT) Console.WriteLine($"Student Name: {t.StudentName}\nMajor: {t.Major}\nTuition: {t.Tuition}\n");
        }

        //f) Join the student list and student GPA list on student ID and display the student's name, major and gpa
        //outer sequence = studentList, inner sequence = studentGPAList, outerkey = student.studentID, innerkey = studentGPa.studentID
        Console.WriteLine();
        Console.WriteLine("-------------\nJOIN: STUDENT LIST WITH STUDENT GPA LIST\n-------------");
        var studentGPAJoin = studentList.Join(studentGPAList,
                                student=>student.StudentID,
                                studentGPA=>studentGPA.StudentID,
                                (student,studentGPA) => new
                                {
                                    StudentName = student.StudentName,
                                    Age = student.Age,
                                    Major = student.Major,
                                    GPA = studentGPA.GPA,
                                });
        Console.WriteLine("Combined student list with GPA list: ");
        Console.WriteLine();
        foreach (var st in studentGPAJoin)
        {
            Console.WriteLine($"Name: {st.StudentName}\nAge: {st.Age}\nMajor: {st.Major}\nGPA: {st.GPA}");
            Console.WriteLine();
        }
        Console.WriteLine();

        //g) Join the student list and student club list. Display the names of only those students who are in the Game club.
        Console.WriteLine();
        Console.WriteLine("-------------\nJOIN: STUDENT LIST WITH CLUB LIST\n-------------");
        var gameClub = studentList.Join(studentClubList,
                                student => student.StudentID,
                                studentClub => studentClub.StudentID,
                                (student, studentClub) => new
                                {
                                    StudentName = student.StudentName,
                                    Club=studentClub.ClubName,
                                });
        Console.WriteLine("List of students in the game club: ");
        Console.WriteLine();
        foreach (var st in gameClub)
        {
            if(st.Club == "Game")
            {
                Console.WriteLine($"Name: {st.StudentName}");
                Console.WriteLine();
            }

        }
        Console.WriteLine();

    }//end main
}//end program