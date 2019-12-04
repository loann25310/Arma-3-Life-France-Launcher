; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F7E72F3D-DEA0-4B12-BBB3-B2188A8DD109}
AppName=Arma 3 Life France Launcher
AppVersion=0.0.1
;AppVerName=Arma 3 Life France Launcher 0.0.1
AppPublisher=ALF - Loann
AppPublisherURL=http://arma3lifefrance.fr/
AppSupportURL=http://arma3lifefrance.fr/
AppUpdatesURL=http://arma3lifefrance.fr/
DefaultDirName={pf}\Arma 3 Life France Launcher
DisableDirPage=yes
DefaultGroupName=Arma 3 Life France Launcher
AllowNoIcons=yes
OutputDir=C:\Users\Loann\source\repos\Arma Life France Launcher\Output
OutputBaseFilename=setup_launcher
SetupIconFile=C:\Users\Loann\Downloads\alf.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "finnish"; MessagesFile: "compiler:Languages\Finnish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\Arma Life France Launcher.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\ALF.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\MaterialSkin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\MetroFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\Microsoft.Win32.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\netstandard.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\SevenZip.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\steam_api.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\steam_appid.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\Steamworks.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.AppContext.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Collections.Concurrent.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Collections.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Collections.NonGeneric.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Collections.Specialized.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ComponentModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ComponentModel.EventBasedAsync.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ComponentModel.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ComponentModel.TypeConverter.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Console.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Data.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.Contracts.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.Debug.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.FileVersionInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.Process.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.StackTrace.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.TextWriterTraceListener.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.Tools.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.TraceSource.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Diagnostics.Tracing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Drawing.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Dynamic.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Globalization.Calendars.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Globalization.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Globalization.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.Compression.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.Compression.ZipFile.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.FileSystem.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.FileSystem.DriveInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.FileSystem.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.FileSystem.Watcher.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.IsolatedStorage.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.MemoryMappedFiles.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.Pipes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.IO.UnmanagedMemoryStream.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Linq.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Linq.Expressions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Linq.Parallel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Linq.Queryable.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Http.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.NameResolution.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.NetworkInformation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Ping.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Requests.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Security.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.Sockets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.WebHeaderCollection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.WebSockets.Client.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Net.WebSockets.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ObjectModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Reflection.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Reflection.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Reflection.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Resources.Reader.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Resources.ResourceManager.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Resources.Writer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.CompilerServices.VisualC.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Handles.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.InteropServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.InteropServices.RuntimeInformation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Numerics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Serialization.Formatters.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Serialization.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Serialization.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Runtime.Serialization.Xml.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Claims.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Cryptography.Algorithms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Cryptography.Csp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Cryptography.Encoding.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Cryptography.Primitives.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Cryptography.X509Certificates.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.Principal.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Security.SecureString.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Text.Encoding.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Text.Encoding.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Text.RegularExpressions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.Overlapped.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.Tasks.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.Tasks.Parallel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.Thread.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.ThreadPool.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Threading.Timer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.ValueTuple.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.ReaderWriter.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.XDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.XmlDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.XmlSerializer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.XPath.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Loann\source\repos\Arma Life France Launcher\Arma Life France Launcher\bin\Debug\System.Xml.XPath.XDocument.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\Arma 3 Life France Launcher"; Filename: "{app}\Arma Life France Launcher.exe"
Name: "{commondesktop}\Arma 3 Life France Launcher"; Filename: "{app}\Arma Life France Launcher.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Arma Life France Launcher.exe"; Description: "{cm:LaunchProgram,Arma 3 Life France Launcher}"; Flags: nowait postinstall skipifsilent

