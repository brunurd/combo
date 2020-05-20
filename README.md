![Combo: Task Integration Tool][logo]

[![openupm]][oupm]  

A way to run automated tasks integrated in the **Unity Game Engine** Editor (similar to Grunt or Gulp in the web).  

---

## **1. Installation**

### **1.1. Install via OpenUPM**

  The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

  ```bash
  openupm add com.lavaleakgames.combo
  ```
### **1.2. Install via Git URL**

  Open *`Packages/manifest.json`* and add the following line to the dependencies block.

```json
"dependencies": {
  "com.lavaleakgames.combo": "https://github.com/lavaleak/combo.git"
}
```

---

## **2. Getting Started**
Combo is a task aggregator, those tasks run every time a file is created, updated or deleted into a specific file extension and in specific project directory.  


Let's see how works for the example Combo package **EnumFromJson**, we create a config like this:  

![picture01]


_**This task will run when:**_  
A file with the **.json** extension in the **Assets/Resources** directory was created, updated or deleted.  

---

### **2.1. Combo Purpose**
Combo can be used for tasks like convert files, save file into a specific folder, upload to web, anything what you want to do with those inputs:  


- A file **search pattern** (Combo uses the [System.IO.GetFiles][get-files] method to match this pattern, all rules used in this method are applied to Combo search pattern too), the search pattern is defined by the **IComboTask** implementation of the selected package.  
- A **path** (must be inside the Assets/ folder).  
- A **Combo package**, you can create your own or use one from community.  

---

## **3. Combo Usage**
Combo on be added to the project automatically creates a singleton **ComboConfig** asset file in the path **Assets/Editor/Combo/Config.asset**, this path can't be changed.  


This configuration can be edited in the asset inspector or in the Combo Config window, in **Tools > Combo Config**.  


In the very start will look like the following image, with a log level option and a "Add New Task" button.  

![picture02]

---

### **3.1. Log Level**
The log level is a flag enum, you can choose if the Combo Logger log normal Log, Warnings or Errors, as a flag those options can be exclusive or mixed.  

![picture03]

---

### **3.2. New Task**
On add a new task you have to choose first a Combo package to set the task type, and optionally you can set a name to task to be easy to find later.  


Any class or struct with a **ComboTaskAttribute** and with any type of **IComboTask** implementation appears in the list.  

![picture04]

---

## **4. Create A New Combo Task Package**
To create a Combo Task just need to create a class or a struct with a **ComboTaskAttribute** and with any type of **IComboTask**, like the following image:  

```C#
[ComboTask]
public class MyComboTask : IComboSingleFileTask
```

The **IComboTask** can be implemented with two interfaces, with the **IComboMultipleFilesTask** and with the **IComboSingleFileTask**.  

---

### **4.1. Interfaces**

- **IComboMultipleFilesTask:** Run one time only using all the matching files input.
- **IComboSingleFileTask:** Run one time for each file of the input.  


Each interface has a method for create or update event and other a method for the delete event.  


Besides that in the interface has the SearchPattern to define the task pattern and the Description to use in the ComboConfig as a tooltip.  

---

### **4.2. TaskFileInputData**

The representation of the input file passed as parameters for the tasks, it has the following fields:  

- **path:** The file relative path.
- **directoryName:** The directory name only without the file name.
- **fileName:** The file name only without the directory or the extension.
- **extension:** The extension, starting with the dot.
- **contents:** The full file contents as string.

---

### **5. License**

Source code: [MIT][license]  
Logo image: [CC-BY-ND][logo-license]  

[openupm]: https://img.shields.io/npm/v/com.lavaleakgames.combo?label=openupm&registry_uri=https://package.openupm.com
[logo]: Editor/Images/Logo/Combo-Logo-Banner_CC-BY-ND_by-Bruno-Araujo.png
[logo-license]: Editor/Images/Logo/LICENSE.md
[license]: LICENSE.md
[oupm]: https://openupm.com/packages/com.lavaleakgames.combo/
[pdf-doc]: documentation.pdf
[get-files]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=netcore-3.1
[picture01]: Editor/Images/picture01.png
[picture02]: Editor/Images/picture02.png
[picture03]: Editor/Images/picture03.png
[picture04]: Editor/Images/picture04.png