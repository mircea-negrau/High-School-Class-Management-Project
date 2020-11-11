# High School Project for receiving Programming Diploma
## Overview

- Programming language: C# (WindowsFormsApp .NET Framework); 
- Database: MySql (XAMPP);
- The database was set up so that multiple teachers could connect to the program remotely.

## Short Description
The application aims to function as a school catalog, the teacher having the opportunity to grade students and add or motivate their absences. At one point, not satisfied with the accessibility of the application, I started implementing the idea as an ASP.NET Web Application (overall improved but not fully implemented), but failing to finish it, I will consider that the submitted application involves updating the database individually for each school semester, students do not have access to view their information, and any teacher can modify existing data in the database regardless of.

In the “Web” file I also attached the code for the web application, at the stage where I managed to bring it. The web application aims to provide the opportunity for students (and their parents) to strictly access their own information (grades, absences, environments). Also, teachers can change details (add / delete notes / absences) only to the classes they teach and only to the subjects they teach to that class (unless this is the assigned teacher to the class - thus having access to motivate absences to any student).

The two applications (WindowsFormsApp and WebApplication) were then to be centralized using the same database (MSSQL, so the windows application migrated to MSSQL from mySQL), and the Windows application to be updated accordingly to be compatible with the facilities implemented in the Web.

## Features
![Add Student](/images/login.PNG)
• Login System.

![Add Student](/images/main_menu.PNG)
• Main Menu:
  - Tooltip in the Main Menu (mouse hover-over);
  - Fully integrated error messages throughout the whole program.

![Add Student](/images/class_management.PNG)
• Class Management:
  - List all students selected class;
  - Add absence, grade, exam grade to selected student;
  - Delete absence, grade, exam grade of selected student;
  - Update absence status (un/motivateded), grade, exam grade of selected student;
  - Switch between a grade being the exam grade or a regular grade for the selected student;
  - Display of current average grade for selected subject of selected student;
  - Display number of grades / absences;
  - Display number of total/motivated/unmotivated absences for selected student overall/for selected subject; 
  - Confirmation before modifying anything.

• Add Student:
![Add Student](/images/add_student.PNG)
  

• The interface adds student (3):
o Select student details
• The student edit interface (4):
o Search for a student by ID
o Display for valid ID details
o Edit / delete details database
• Form interface (5):
o Display students in the database;
o Possibility to add / duplicate / edit / delete student;
o Student search based on any additional information (not just ID);
o Download student photo;
o Number of existing students in the database;
• Export interface (6):
o Export to the desktop .txt file of all existing students in the database
o Application of filters for students (gender / age);
![Add Student](/images/manage_student.PNG)
![Add Student](/images/print_filter_students_list.PNG)

