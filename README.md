# Anti-Virus
Anti-Virus final project in Advanced Topics in Cyber security
Operating Instructions :
We will run the Func_Cyber engine file for the first time with administrator privileges.
Then every time the computer boots up, the engine will run in the background.
For UI it is necessary to manually open the Gui_Cyber file
The antivirus supports the following actions:
• Scanning a file - the user selects the file he wants to scan to check if it is a malicious file
• Scanning a folder - the user selects the folder he wants to scan to check if it or its subfolders have
Malicious files (the selected folder will be the root folder, so the antivirus will scan all its contents)
• Expert mode - the user gets a deeper look at the software (statistical data)
• file Log – a text file containing detailed documentation about the operation of the software
• History - shows the user the discovery history of the software - allows him to view all the discoveries made with
The timestamp of the discovery, how it was discovered, and where the malicious file is located.
• Networks Check - a new process that checks software that tries to communicate from the computer to the outside and checks if they are malicious
• Satertup Check - the process that checks if there are any malicious programs in the registry
• Monitor File – a process that monitors changes in folders on the computer.


![image](https://github.com/natielkayam/Anti-Virus/assets/80591263/dcee18b2-5b2a-42fb-a885-1e9a10d40cdb)

Classes explantaion : 

# Singelton Design Malicious
Department of testing whether the file is malicious.
There is an xml.whitelist file that contains the good files and their 5md.
The first test is to check the 5md of the file we received, is it present in xml.whitelist.
If it does not exist, we will return an answer that it is malicious and add it to list_detected.
If it exists, we will perform another check and check its similarity to all our existing files in list_detected if
There is a match of 80% or more, we will return an answer that is malicious and add it.
We will perform the similarity test by breaking the file into bits and comparing them to the file that is already in the detected list
(heuristic)

# Singelton Design FileManager
Department of monitoring files on the computer in the folders we defined: SYSTEM32, all folders under USER\USER
This class uses the MALICIOUS class.
Monitors the system in the folders we defined according to the following situations:
Created, deleted, changed, renamed
For each of the situations, a check is made with the help of malicious whether the file that was changed is suspicious and if so we will remove it
Notice to the user.

# Singelton Design StartupRegistry
Provides an examination of suspicious files in the registry of the machine, with the help of malicious

# Singelton Design NetworkManager
A department that monitors the ports coming out of the machine.
uses netstat, checks for each record the file by PID and uses malicious to check if
The same file that tries to connect out is malicious or not.
If so, a message is issued to the user, and the process is killed.
If not, it continues to the next record, and works in a loop until we stop it.

# Singelton Design Log
A class that creates a log file closed in a mutex that will not be accessible except from the project.

# Singelton Design White_Item
A class that creates an object of each file from the xml.whitelist.
The department enables more convenient work for the software.

# GuiHelper
This department's role is to link between the user interface and the engine.
Allows the GUI to use all functionality available in the engine.


![image](https://github.com/natielkayam/Anti-Virus/assets/80591263/af4c9c86-251a-4775-8800-4c55c5281b3f)


# Scan File, Directory
Gives the possibility to scan a file, a folder on the computer and then uses the Malicious class of the engine in order to
determine if malicious or not.

# Open Log File
Opens an exact copy of the latest log file.

# Check Startup Registry
Makes use of the engine's StartupRegestry class

# Monitor Files
Makes use of the engine's FileManager class

# Start check Networks
Makes use of the engine's NetworkManager class

