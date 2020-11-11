# High School Project for receiving Programming Diploma
## Overview
- 
- Deadline: 4 weeks;
- Programming language: C# (WindowsFormsApp .NET Framework); 
- Database: MySql (XAMPP);
- *No previous knowledge of either C# or databases*

## Short Description
- The database was set up so that multiple teachers could connect to the program remotely.
- The application aims to function as a school gradebook, the teacher having the opportunity to grade students and add or motivate their absences. 
- At one point, not satisfied with the accessibility of the application, I started implementing the idea as an ASP.NET Web Application (overall improved although not fully implemented) but not managing to migrate it fully within the given deadline, I will consider that the submitted application involves updating the database individually for each school semester, students do not have access to view their information, and any teacher can modify existing data in the database regardless of their taught subject.

In the “Web” file I also attached the code for the web application, at the stage where I managed to bring it. The web application aims to provide the opportunity for students (and their parents) to strictly access their own information (grades, absences, environments). Also, teachers can change details (add / delete notes / absences) only to the classes they teach and only to the subjects they teach to that class (unless this is the assigned teacher to the class - thus having access to motivate absences to any student).

## Features
### Login System.
![Login](/images/login.PNG)

### Main Menu:
![Main Menu](/images/main_menu.PNG)
  - Tooltip in the Main Menu (mouse hover-over);
  - Fully integrated error messages throughout the whole program.

### Class Management:
![Class Management](/images/class_management.PNG)
  - List all students selected class;
  - Add absence, grade, exam grade to selected student;
  - Delete absence, grade, exam grade of selected student;
  - Update absence status (un/motivateded), grade, exam grade of selected student;
  - Switch between a grade being the exam grade or a regular grade for the selected student;
  - Display of current average grade for selected subject of selected student;
  - Display number of grades / absences;
  - Display number of total/motivated/unmotivated absences for selected student overall/for selected subject; 
  - Confirmation before modifying anything.

### Student Managemenent:
![Student Managemenent](/images/add_student.PNG)
  - Upload photo of student;
  - Add details (Surname, Forename, Phone number, Home Address, etc.);
  - Set date of birth and gender;
  - Assign student to a classroom;
  
![Student Managemenent](/images/manage_student.PNG)
  - Edit photo of student;
  - Edit details (Surname, Forename, Phone number, Home Address, etc.);
  - Edit date of birth and gender;
  - Assign student to a classroom;

![Student Managemenent](/images/search_edit_delete_students.PNG)
  - List all the students of the school;
  - Search function to filter students;
  - Add new student;
  - Get ID of selected student;
  - Download picture of selected student;
  - Change details about selected student;
  - Remove selected student;
  - Reset all fields;
