![Combo: Task Integration Tool][logo]

[![openupm]][oupm]  

A way to run automated tasks integrated in the **Unity Game Engine** Editor (similar to Grunt or Gulp in the web).  

|**Links:**|[Documentation][doc]|[Source Code License][license]|[Logo License][logo-license]|
|-|-|-|-|
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

[doc]: Documentation/README.md
[openupm]: https://img.shields.io/npm/v/com.lavaleakgames.combo?label=openupm&registry_uri=https://package.openupm.com
[logo]: Editor/Images/Logo/Combo-Logo-Banner_CC-BY-ND_by-Bruno-Araujo.png
[logo-license]: Editor/Images/Logo/LICENSE.md
[license]: LICENSE.md
[oupm]: https://openupm.com/packages/com.lavaleakgames.combo/
