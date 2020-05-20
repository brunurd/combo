![Combo: Task Integration Tool][logo]

[![openupm]][oupm]  

A way to run automated tasks integrated in the **Unity Game Engine** Editor (similar to Grunt or Gulp in the web).  

---

## **Installation**

### **Install via OpenUPM**

  The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

  ```bash
  openupm add com.lavaleakgames.combo
  ```
### **Install via Git URL**

  Open *`Packages/manifest.json`* and add the following line to the dependencies block.

```json
"dependencies": {
  "com.lavaleakgames.combo": "https://github.com/lavaleak/combo.git"
}
```

---

## **Documentation**

1. **[Getting Started][page01]**
2. **[Combo Usage][page02]**
3. **[Create A New Combo Task Package][page03]**

---

## **Licenses**

Source code: [MIT][license]  
Logo image: [CC-BY-ND][logo-license]  

[openupm]: https://img.shields.io/npm/v/com.lavaleakgames.combo?label=openupm&registry_uri=https://package.openupm.com
[logo]: Editor/Images/Logo/Combo-Logo-Banner_CC-BY-ND_by-Bruno-Araujo.png
[logo-license]: Editor/Images/Logo/LICENSE.md
[license]: LICENSE.md
[oupm]: https://openupm.com/packages/com.lavaleakgames.combo/
[page01]: Documentation/01-getting-started.md
[page02]: Documentation/02-combo-usage.md
[page03]: Documentation/03-create-a-new-combo-task-package.md