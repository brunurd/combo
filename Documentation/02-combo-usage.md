## **02. Combo Usage**
Combo on be added to the project automatically creates a singleton **ComboConfig** asset file in the path **Assets/Editor/Combo/Config.asset**, this path can't be changed.  


This configuration can be edited in the asset inspector or in the Combo Config window, in **Tools > Combo Config**.  


In the very start will look like the following image, with a log level option and a "Add New Task" button.  

![picture02]

---

### **2.1. Log Level**
The log level is a flag enum, you can choose if the Combo Logger log normal Log, Warnings or Errors, as a flag those options can be exclusive or mixed.  

![picture03]

---

### **2.2. New Task**
On add a new task you have to choose first a Combo package to set the task type, and optionally you can set a name to task to be easy to find later.  


Any class or struct with a **ComboTaskAttribute** and with any type of **IComboTask** implementation appears in the list.  

![picture04]


|[<< 01. Getting Started][previous]|[03. Create A New Combo Task Package >>][next]|
|-|-|

[previous]: 01-getting-started.md
[next]: 03-create-a-new-combo-task-package.md
[picture02]: ../Editor/Images/picture02.png
[picture03]: ../Editor/Images/picture03.png
[picture04]: ../Editor/Images/picture04.png