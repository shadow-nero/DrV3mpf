# DrV3mpf  

DrV3mpf is a tool developed to easily extract and reconstruct MPF files, making the process simple and accessible. Originally created to assist **somesillypenguin** in their modding projects.  

## Table of Contents  

- [Usage](#usage)  
  - [Extract MPF](#extract-mpf)  
  - [Repack MPF](#repack-mpf)  
- [Automation with BAT files](#automation-with-bat-files)  
- [Special Thanks](#special-thanks)  

---

## Usage  

### Compilation  

Download the source code and compile it using **Visual Studio 2022** or higher.  

### Extract MPF  

To extract the data from an MPF file, you need to specify the file type and the `-extract` command. See the examples below:  

```sh  
DrV3mpf -settings -extract "path/to/mpf_setting.dat" "path/to/output_folder"  
DrV3mpf -cursor -extract "path/to/mpf_cursor.dat" "path/to/output_folder"  
```  

> **Note:**  
> - Modify the paths as necessary.  
> - Avoid spaces or special characters in file and folder names.  
> - If the MPF file is outside the DrV3mpf folder, provide the full path.  

---

### Repack MPF  

After editing the extracted files, use the following command to repack the MPF:  

```sh  
DrV3mpf -settings -repack "path/to/mpf_setting.dat" "path/to/output_folder"  
DrV3mpf -cursor -repack "path/to/mpf_cursor.dat" "path/to/output_folder"  
```  

---

## Automation with BAT files  

To simplify the extraction and repacking process, you can create a **.bat** file to automate the commands.  

1. **Create the BAT file**  
   - Open **Notepad**.  
   - Insert the desired commands. Example:  

   ```bat  
   @echo off  
   DrV3mpf -settings -extract "path/to/mpf_setting.dat" "path/to/output_folder"  
   pause  
   ```  

2. **Save the file**  
   - Go to **File > Save As**.  
   - In the "File Name" field, type something like `extract_mpf.bat`.  
   - In "Save as type", select **All Files**.  
   - Click **Save**.  

Now, simply run the **.bat** file to execute the commands automatically.  

---

## Special Thanks  

- **[Morgana-X](https://github.com/morgana-x)** – I was inspired by the pull he made on another tool of mine.
- **[SomesillyPenguin](https://github.com/)** – This tool was made to help him in his analysis, so he needs to be here in the credits because without him I wouldn't even have created this tool. 
