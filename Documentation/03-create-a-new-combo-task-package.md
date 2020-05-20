
## **03. Create A New Combo Task Package**
To create a Combo Task just need to create a class or a struct with a **ComboTaskAttribute** and with any type of **IComboTask**, like the following image:  

```C#
[ComboTask]
public class MyComboTask : IComboSingleFileTask
```

The **IComboTask** can be implemented with two interfaces, with the **IComboMultipleFilesTask** and with the **IComboSingleFileTask**.  

---

### **3.1. Interfaces**

- **IComboMultipleFilesTask:** Run one time only using all the matching files input.
- **IComboSingleFileTask:** Run one time for each file of the input.  


Each interface has a method for create or update event and other a method for the delete event.  


Besides that in the interface has the SearchPattern to define the task pattern and the Description to use in the ComboConfig as a tooltip.  

---

### **3.2. TaskFileInputData**

The representation of the input file passed as parameters for the tasks, it has the following fields:  

- **path:** The file relative path.
- **directoryName:** The directory name only without the file name.
- **fileName:** The file name only without the directory or the extension.
- **extension:** The extension, starting with the dot.
- **contents:** The full file contents as string.  
