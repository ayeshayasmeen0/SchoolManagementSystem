using System;
using System.Data.SQLite;
using System.IO;

public class TestDatabase
{
    public static void CreateDatabase()
    {
        string dbPath = "SchoolDB.sqlite";

        if (!File.Exists(dbPath))
        {
            SQLiteConnection.CreateFile(dbPath);
            Console.WriteLine("Database file created: " + dbPath);

            // Tables create karo
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();

                string createStudentsTable = @"
                    CREATE TABLE Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentCode TEXT UNIQUE NOT NULL,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Phone TEXT,
                        DateOfBirth TEXT,
                        Gender TEXT,
                        Address TEXT,
                        EnrollmentDate TEXT DEFAULT CURRENT_DATE,
                        IsActive INTEGER DEFAULT 1
                    )";

                string createTeachersTable = @"
                    CREATE TABLE Teachers (
                        TeacherID INTEGER PRIMARY KEY AUTOINCREMENT,
                        TeacherCode TEXT UNIQUE NOT NULL,
                        FirstName TEXT NOT NULL,
                        LastName TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Phone TEXT,
                        Specialization TEXT,
                        HireDate TEXT DEFAULT CURRENT_DATE,
                        IsActive INTEGER DEFAULT 1
                    )";

                string createCoursesTable = @"
                    CREATE TABLE Courses (
                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseCode TEXT UNIQUE NOT NULL,
                        CourseName TEXT NOT NULL,
                        Credits INTEGER DEFAULT 3,
                        Description TEXT,
                        TeacherID INTEGER,
                        IsActive INTEGER DEFAULT 1
                    )";

                string createEnrollmentsTable = @"
                    CREATE TABLE Enrollments (
                        EnrollmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER,
                        CourseID INTEGER,
                        EnrollmentDate TEXT DEFAULT CURRENT_DATE,
                        Grade REAL,
                        Status TEXT DEFAULT 'Enrolled'
                    )";

                using (var cmd = new SQLiteCommand(createStudentsTable, connection))
                    cmd.ExecuteNonQuery();
                using (var cmd = new SQLiteCommand(createTeachersTable, connection))
                    cmd.ExecuteNonQuery();
                using (var cmd = new SQLiteCommand(createCoursesTable, connection))
                    cmd.ExecuteNonQuery();
                using (var cmd = new SQLiteCommand(createEnrollmentsTable, connection))
                    cmd.ExecuteNonQuery();

                Console.WriteLine("Tables created successfully!");
            }
        }
    }
}