using LINQ___Study;

namespace LINQStudy
{
    internal class Program
    {
        // Constants
        private const string TargetCourse = "C# Programming";
        private const int ComputerScienceDeptId = 1;
        private const int ITDeptId = 2;

        static void Main ( string[] args )
        {
            RunFilteringAndOrderingExamples();
            RunProjectionExamples();
            RunJoiningExamples();
            RunGroupingExamples();
            RunAggregationExamples();
            RunQuantifierExamples();
            RunElementExamples();
            RunPartitioningExamples();
            RunSetOperatorExamples();
            RunConversionExamples();
        }

        #region Filtering and Ordering
        private static void RunFilteringAndOrderingExamples ()
        {
            // Students in CS department
            var csStudents = DemoData.Students.Where(s => s.DepartmentId == ComputerScienceDeptId);
            Console.WriteLine("Students in Computer Science Department:");
            foreach (var student in csStudents)
                Console.WriteLine($"Name: {student.Name}, Department: {student.DepartmentId}");
            PrintSeparator();

            // Students with Grade > 80
            var highGradeStudents = DemoData.Students.Where(s => s.Grade > 80).OrderBy(s => s.Name);
            Console.WriteLine("Students with grade > 80:");
            foreach (var student in highGradeStudents)
                Console.WriteLine($"Name: {student.Name}, Grade: {student.Grade}");
            PrintSeparator();

            // Order students by Age then Grade desc
            var orderedStudents = DemoData.Students.OrderBy(s => s.Age).ThenByDescending(s => s.Grade);
            Console.WriteLine("Students ordered by Age then Grade:");
            foreach (var student in orderedStudents)
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            PrintSeparator();
        }
        #endregion

        #region Projection
        private static void RunProjectionExamples ()
        {
            var topStudents = DemoData.Students
                .Where(s => s.Grade > 85)
                .OrderByDescending(s => s.Grade)
                .Select(s => new { s.Name, s.Grade });

            Console.WriteLine("Students with grade > 85 (ordered desc):");
            foreach (var student in topStudents)
                Console.WriteLine($"Name: {student.Name}, Grade: {student.Grade}");
            PrintSeparator();

            var coursesWithHighCredits = DemoData.Courses.Where(c => c.Credits > 3);
            Console.WriteLine("Courses with credits > 3:");
            foreach (var course in coursesWithHighCredits)
                Console.WriteLine($"Course: {course.Name}, Credits: {course.Credits}");
            PrintSeparator();

            var studentStatuses = DemoData.Students.Select(s => new
            {
                s.Name,
                s.Age,
                Status = s.Grade >= 90 ? "Excellent" : (s.Grade >= 80 ? "Very Good" : "Good")
            });

            Console.WriteLine("Student Statuses:");
            foreach (var student in studentStatuses)
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Status: {student.Status}");
            PrintSeparator();
        }
        #endregion

        #region Joining
        private static void RunJoiningExamples ()
        {
            var studentsWithDepartments = DemoData.Students
                .Join(DemoData.Departments,
                    student => student.DepartmentId,
                    department => department.Id,
                    ( student, department ) => new { student.Name, DepartmentName = department.Name });

            Console.WriteLine("Students with their Departments:");
            foreach (var student in studentsWithDepartments)
                Console.WriteLine($"Student: {student.Name}, Department: {student.DepartmentName}");
            PrintSeparator();

            var studentsInCSharp = DemoData.Students
                .Join(DemoData.Enrollments,
                    student => student.Id,
                    enrollment => enrollment.StudentId,
                    ( student, enrollment ) => new { student, enrollment })
                .Join(DemoData.Courses,
                    enrollmentCourse => enrollmentCourse.enrollment.CourseId,
                    course => course.Id,
                    ( enrollmentCourse, course ) => new { enrollmentCourse.student.Name, CourseName = course.Name })
                .Where(sc => sc.CourseName == TargetCourse);

            Console.WriteLine($"Students enrolled in {TargetCourse}:");
            foreach (var student in studentsInCSharp)
                Console.WriteLine($"Student: {student.Name}, Course: {student.CourseName}");
            PrintSeparator();

            var studentsAbove21 = DemoData.Students.Where(s => s.Age > 21)
                .Join(DemoData.Enrollments,
                    student => student.Id,
                    enrollment => enrollment.StudentId,
                    ( student, enrollment ) => new { student, enrollment })
                .Join(DemoData.Courses,
                    se => se.enrollment.CourseId,
                    course => course.Id,
                    ( se, course ) => new { se.student.Name, se.student.Age, CourseName = course.Name })
                .OrderBy(s => s.Name);

            Console.WriteLine("Students above 21 with their courses:");
            foreach (var student in studentsAbove21)
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Course: {student.CourseName}");
            PrintSeparator();
        }
        #endregion

        #region Grouping
        private static void RunGroupingExamples ()
        {
            var studentsGroupedByAge = DemoData.Students.GroupBy(s => s.Age);
            Console.WriteLine("Students grouped by Age:");
            foreach (var group in studentsGroupedByAge)
                Console.WriteLine($"Age {group.Key}: {group.Count()} students");
            PrintSeparator();

            var studentsAbove85ByDept = DemoData.Students.Where(s => s.Grade > 85).GroupBy(s => s.DepartmentId);
            Console.WriteLine("Students with grade > 85 grouped by Department:");
            foreach (var group in studentsAbove85ByDept)
                Console.WriteLine($"Department {group.Key}: {group.Count()} students");
            PrintSeparator();

            var studentsGroupedByDeptName = DemoData.Students
                .Join(DemoData.Departments,
                    student => student.DepartmentId,
                    department => department.Id,
                    ( student, department ) => new { student.Name, DepartmentName = department.Name })
                .GroupBy(s => s.DepartmentName);

            Console.WriteLine("Students grouped by Department Name:");
            foreach (var group in studentsGroupedByDeptName)
                Console.WriteLine($"Department {group.Key}: {group.Count()} students");
            PrintSeparator();
        }
        #endregion

        #region Aggregation
        private static void RunAggregationExamples ()
        {
            var youngestStudent = DemoData.Students.MinBy(s => s.Age);
            var oldestStudent = DemoData.Students.MaxBy(s => s.Age);
            Console.WriteLine($"Youngest: {youngestStudent.Name}, Age: {youngestStudent.Age}");
            Console.WriteLine($"Oldest: {oldestStudent.Name}, Age: {oldestStudent.Age}");
            PrintSeparator();

            var topCourse = DemoData.Courses.MaxBy(c => c.Credits);
            Console.WriteLine($"Course with max credits: {topCourse.Name}, Credits: {topCourse.Credits}");
            PrintSeparator();

            var avgGradesByDept = DemoData.Students
                .GroupBy(s => s.DepartmentId)
                .Select(g => new { DepartmentId = g.Key, Average = g.Average(s => s.Grade) });
            Console.WriteLine("Average grades per Department:");
            foreach (var dept in avgGradesByDept)
                Console.WriteLine($"Department {dept.DepartmentId}: Avg Grade {dept.Average:F2}");
            PrintSeparator();

            var studentsInCSharp = DemoData.Enrollments
                .Join(DemoData.Courses,
                    e => e.CourseId,
                    c => c.Id,
                    ( e, c ) => new { e, c })
                .Count(x => x.c.Name == TargetCourse);
            Console.WriteLine($"Number of students in {TargetCourse}: {studentsInCSharp}");
            PrintSeparator();
        }
        #endregion

        #region Quantifiers
        private static void RunQuantifierExamples ()
        {
            Console.WriteLine("Any student with grade > 95? " + DemoData.Students.Any(s => s.Grade > 95));
            Console.WriteLine("All courses credits > 2? " + DemoData.Courses.All(c => c.Credits > 2));

            bool isStudentInSpring = DemoData.Students.Where(s => s.Id == 1)
                .Join(DemoData.Enrollments,
                    student => student.Id,
                    enrollment => enrollment.StudentId,
                    ( student, enrollment ) => enrollment.Semester)
                .Any(sem => sem == "Spring");

            Console.WriteLine("Student with ID=1 enrolled in Spring? " + isStudentInSpring);
            PrintSeparator();
        }
        #endregion

        #region Elements
        private static void RunElementExamples ()
        {
            var studentRamy = DemoData.Students.FirstOrDefault(s => s.Name == "Ramy");
            Console.WriteLine(studentRamy != null ? $"Found: {studentRamy.Name}" : "Not Found");

            var studentWithId10 = DemoData.Students.SingleOrDefault(s => s.Id == 10);
            if (studentWithId10 != null)
                Console.WriteLine($"Found student with ID=10: {studentWithId10.Name}");
            PrintSeparator();
        }
        #endregion

        #region Partitioning
        private static void RunPartitioningExamples ()
        {
            var firstTwoAfterSkip = DemoData.Students.Skip(3).Take(2);
            Console.WriteLine("Skip 3 and Take 2:");
            foreach (var student in firstTwoAfterSkip)
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}");
            PrintSeparator();

            var lastTwoHighGrades = DemoData.Students.Where(s => s.Grade > 80).TakeLast(2);
            Console.WriteLine("Last 2 students with grade > 80:");
            foreach (var student in lastTwoHighGrades)
                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Grade: {student.Grade}");
            PrintSeparator();
        }
        #endregion

        #region Set Operators
        private static void RunSetOperatorExamples ()
        {
            var csStudents = DemoData.Students.Where(s => s.DepartmentId == ComputerScienceDeptId);
            var itStudents = DemoData.Students.Where(s => s.DepartmentId == ITDeptId);

            var combined = csStudents.Union(itStudents);
            Console.WriteLine($"Total CS + IT students: {combined.Count()}");
            PrintSeparator();

            var distinctNames = DemoData.Students.Select(s => s.Name).Distinct();
            Console.WriteLine("Distinct student names:");
            foreach (var name in distinctNames)
                Console.WriteLine(name);
            PrintSeparator();
        }
        #endregion

        #region Conversion
        private static void RunConversionExamples ()
        {
            var studentsList = DemoData.Students.Where(s => s.Name == "Ahmed" || s.Name == "Sara").ToList();
            Console.WriteLine("Ahmed and Sara:");
            foreach (var student in studentsList)
                Console.WriteLine(student.Name);
            PrintSeparator();

            var coursesDict = DemoData.Courses.ToDictionary(c => c.Name);
            if (coursesDict.TryGetValue(TargetCourse, out var csharpCourse))
                Console.WriteLine($"{TargetCourse} credits: {csharpCourse.Credits}");
            PrintSeparator();

            var studentsLookup = DemoData.Students.ToLookup(s => s.DepartmentId);
            Console.WriteLine("Students in CS Department from Lookup:");
            foreach (var student in studentsLookup[ComputerScienceDeptId])
                Console.WriteLine(student.Name);
            PrintSeparator();
        }
        #endregion

        private static void PrintSeparator () =>
            Console.WriteLine(new string('-', 50));
    }
}
