## **01. Getting Started**
Combo is a task aggregator, those tasks run every time a file is created, updated or deleted into a specific file extension and in specific project directory.  


Let's see how works for the example Combo package **EnumFromJson**, we create a config like this:  

![picture01]


_**This task will run when:**_  
A file with the **.json** extension in the **Assets/Resources** directory was created, updated or deleted.  

---

### **1.1. Combo Purpose**
Combo can be used for tasks like convert files, save file into a specific folder, upload to web, anything what you want to do with those inputs:  


- A file **search pattern** (Combo uses the [System.IO.GetFiles][get-files] method to match this pattern, all rules used in this method are applied to Combo search pattern too), the search pattern is defined by the **IComboTask** implementation of the selected package.  
- A **path** (must be inside the Assets/ folder).  
- A **Combo package**, you can create your own or use one from community.  


[picture01]: ../Editor/Images/picture01.png
[get-files]: https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=netcore-3.1