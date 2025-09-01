-- ==========================
-- University System Database
-- ==========================

CREATE TABLE University (
    university_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL
);
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(100) NOT NULL UNIQUE,
    password_hash NVARCHAR(255) NOT NULL,
    role NVARCHAR(50) CHECK (role IN ('Admin', 'Lecturer', 'Student'))
);

-- تعديل جدول الطالب لإضافة الربط مع اليوزر
ALTER TABLE Student
ADD user_id INT UNIQUE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id);

-- تعديل جدول المحاضر لإضافة الربط مع اليوزر
ALTER TABLE Lecturer
ADD user_id INT UNIQUE,
    FOREIGN KEY (user_id) REFERENCES Users(user_id);
CREATE TABLE Faculty (
    faculty_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    dean NVARCHAR(255),
    university_id INT,
    FOREIGN KEY (university_id) REFERENCES University(university_id)
);

CREATE TABLE Year (
    year_id INT PRIMARY KEY IDENTITY(1,1),
    year_label NVARCHAR(50) NOT NULL
);

CREATE TABLE Department (
    department_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    degree_level NVARCHAR(100),
    faculty_id INT,
    FOREIGN KEY (faculty_id) REFERENCES Faculty(faculty_id)
);

CREATE TABLE Lecturer (
    lecturer_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    rank NVARCHAR(100),
    department_id INT,
    FOREIGN KEY (department_id) REFERENCES Department(department_id)
);

CREATE TABLE Program (
    program_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    faculty_id INT,
    FOREIGN KEY (faculty_id) REFERENCES Faculty(faculty_id)
);

CREATE TABLE Course_Semester (
    course_id INT PRIMARY KEY IDENTITY(1,1),
    code NVARCHAR(50) NOT NULL,
    title NVARCHAR(255) NOT NULL,
    email NVARCHAR(255),
    program_id INT,
    advisor_id INT,
    FOREIGN KEY (program_id) REFERENCES Program(program_id),
    FOREIGN KEY (advisor_id) REFERENCES Lecturer(lecturer_id)
);

CREATE TABLE Student (
    student_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    dob DATE,
    email NVARCHAR(255),
    program_id INT,
    advisor_id INT,
    FOREIGN KEY (program_id) REFERENCES Program(program_id),
    FOREIGN KEY (advisor_id) REFERENCES Lecturer(lecturer_id)
);

CREATE TABLE Student_Course (
    student_id INT,
    course_id INT,
    semester_id INT,
    grade NVARCHAR(5),
    PRIMARY KEY (student_id, course_id, semester_id),
    FOREIGN KEY (student_id) REFERENCES Student(student_id),
    FOREIGN KEY (course_id) REFERENCES Course_Semester(course_id),
    FOREIGN KEY (semester_id) REFERENCES Year(year_id)
);