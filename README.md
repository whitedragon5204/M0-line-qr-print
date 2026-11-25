# M0线二维码打印程序

M0 线 PACK / 客户码纸条打印（WinForms，.NET Framework 4.6.1）。  
用 Visual Studio 打开 `PackLabelPrint.sln`，生成后运行 `PackLabelPrint.exe`。

## 以后怎么提交

**只提交源码改动**，不要把编译结果、现场文件、驱动一起推上去。

可以提交：

- `.cs`、`.csproj`、`.sln`、`App.config`
- `README.md`、`使用说明.txt`、`.gitignore`

不要提交：

- `bin/`、`obj/`、`.vs/`、`.pdb`、`.user`
- `exe`、`dll`、运行日志
- TSC 驱动、现场打印机配置、压缩包（这些在上一级目录 `PackLabelPrint相关文件`）

本仓库已用 `.gitignore` 挡住上面这些。提交前看一眼 `git status`，列表里应只有你改过的代码文件。

## 现场拷贝

生成后把 `bin\Debug\net461` 里这几个放同一目录拷到现场：

- `PackLabelPrint.exe`
- `PackLabelPrint.exe.config`
- `Newtonsoft.Json.dll`
- `zxing.dll`
- `zxing.presentation.dll`
