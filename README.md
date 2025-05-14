# File-Handling
CRUD using File Handling

C# Console Application Exercise – File handling
Objective:
Develop a console-based File Manager in C# that:
•	Reads a directory path from the application configuration file (App.config).
•	Takes a user-input file name and validates it.
•	Performs common file operations: create, read, append, and delete.
Program requirements:
a. Read Configuration
•	Read the value of DirectoryPath from App.config.
•	Check if the directory exists.
o	If it doesn't exist, display an error message and exit.
b. File Name Input and Validation
•	Prompt the user to enter a file name 
•	Validate the file name:
o	Ensure it does not contain invalid characters.
o	Do not allow empty or whitespace-only names.
c. Menu-Driven File Operations
•	Implement the following options in a loop:
--- File Manager ---
1. Create File
2. Read File
3. Append to File
4. Delete File
5. Exit
Enter your choice:

Option 1: Create File
•	If the file exists, inform the user.
•	If not, ask whether to create it.
•	If confirmed, create an empty file.
Option 2: Read File
•	Read and display the content of the file.
•	If the file is empty, display an appropriate message. 
Option 3: Append to File
•	Prompt the user to enter text.
•	Append the input to the file.
Option 4: Delete File
•	Ask for confirmation before deleting the file.
•	Delete the file if confirmed.
Check if file exists
•	If the file does not exist, display an appropriate message
Option 5: Exit
•	Exit the program gracefully.

Points to consider while coding
1.	Performing standard file operations
2.	Writing clean, modular code using methods
3.	Handling exceptions and user input

Additional exercises
After completing the above one, do the following
•	Use StreamReader and StreamWriter for the above functionalities
•	Use large files to know the benefits of Stream operations 
File and Directory Enumeration
•	Directory.GetFiles(), GetDirectories(), EnumerateFiles() (lazy loading)
•	Search using patterns (*.txt, *.log)
•	Recursive traversal of directories
File Metadata and Attributes
•	Access file info via FileInfo (size, created date, last accessed)
•	Set and read attributes like ReadOnly, Hidden using File.SetAttributes()
•	Retrieve file timestamps (LastWriteTime, CreationTime) 
