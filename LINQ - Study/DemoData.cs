namespace LINQ___Study;
internal static class DemoData
{
    public static List<Student> Students = new List<Student>
    {
        new Student { Id = 1, Name = "Ahmed", Age = 21, Grade = 85.5, DepartmentId = 1 },
        new Student { Id = 2, Name = "Ramy", Age = 22, Grade = 91.0, DepartmentId = 2 },
        new Student { Id = 3, Name = "Maged", Age = 20, Grade = 78.0, DepartmentId = 1 },
        new Student { Id = 4, Name = "Omar", Age = 23, Grade = 95.5, DepartmentId = 3 },
        new Student { Id = 5, Name = "Laila", Age = 21, Grade = 89.0, DepartmentId = 2 },
        new Student { Id = 6, Name = "Sara", Age = 20, Grade = 72.0, DepartmentId = 1 },
        new Student { Id = 7, Name = "Mohamed", Age = 22, Grade = 80.5, DepartmentId = 3 },
        new Student { Id = 8, Name = "Youssef", Age = 23, Grade = 85.0, DepartmentId = 1 },
        new Student { Id = 9, Name = "Fatma", Age = 21, Grade = 94.5, DepartmentId = 2 },
        new Student { Id = 10, Name = "Ali", Age = 22, Grade = 77.0, DepartmentId = 3 }
    };

    public static List<Course> Courses = new List<Course>
    {
        new Course { Id = 101, Name = "Physics I", Credits = 3 },
        new Course { Id = 102, Name = "Calculus", Credits = 4 },
        new Course { Id = 103, Name = "C# Programming", Credits = 4 },
        new Course { Id = 104, Name = "Data Structures", Credits = 3 },
        new Course { Id = 105, Name = "Database Systems", Credits = 3 },
        new Course { Id = 106, Name = "Artificial Intelligence", Credits = 4 }
    };

    public static List<Department> Departments = new List<Department>
    {
        new Department { Id = 1, Name = "Computer Science" },
        new Department { Id = 2, Name = "Information Technology" },
        new Department { Id = 3, Name = "Software Engineering" }
    };

    public static List<Enrollment> Enrollments = new List<Enrollment>
    {
        new Enrollment { StudentId = 1, CourseId = 101, Semester = "Fall" },
        new Enrollment { StudentId = 1, CourseId = 102, Semester = "Fall" },
        new Enrollment { StudentId = 2, CourseId = 103, Semester = "Fall" },
        new Enrollment { StudentId = 3, CourseId = 101, Semester = "Spring" },
        new Enrollment { StudentId = 4, CourseId = 102, Semester = "Spring" },
        new Enrollment { StudentId = 5, CourseId = 103, Semester = "Fall" },
        new Enrollment { StudentId = 6, CourseId = 105, Semester = "Fall" },
        new Enrollment { StudentId = 7, CourseId = 105, Semester = "Spring" },
        new Enrollment { StudentId = 8, CourseId = 104, Semester = "Fall" },
        new Enrollment { StudentId = 9, CourseId = 106, Semester = "Fall" },
        new Enrollment { StudentId = 10, CourseId = 104, Semester = "Spring" }
    };
}
