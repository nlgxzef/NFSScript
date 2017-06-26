using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Linq;
using NFSScript;
using NFSScript.Core;
using NFSScript.Math;
using NFSScript.Carbon;

using Tick = System.Timers.Timer;
using RunningMemory = NFSScript.Core.VAMemory;
using NFSSKeys = NFSScript.Keys;

namespace NFSScriptLoader
{
    public class Program
    {
        static Tick timer;

        static int settingShowConsole = 0;
        static int settingDebug = 0;

        private static bool inGameplay = false;
        private static bool inRace = false;

        private static IntPtr GAMEPLAY_ACTIVE, IS_ACTIVITY_MODE;

        private const int UPDATE_TICK = 1;
        private const int WAIT_BEFORE_LOAD = 5000;

        static List<ModScript> scripts;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYUP = 256;
        private const int WM_KEYDOWN = 257;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;


        private const string NFS_SCRIPT_INI_FILE_NAME = "NFSScriptSettings.ini";

        private static int resetKey = (int)NFSSKeys.Insert;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = new LowLevelKeyboardProc(HookCallback);
        private static IntPtr _hookID = IntPtr.Zero;

        static NFSGame currentNFSGame;

        public static RunningMemory gameMemory;

        static Process gameProcess;

        static string gameProcessName = string.Empty;
        static string processNameTitle = string.Empty;

        static int EntryPoint(string pwzArgument)
        {
            Log.Print(NFSScriptLoader.INFO_TAG, "NFSScript by Dennis Stanistan");

            INIInit();
            GetNFSGame();
            Start();

            return 0;
        }

        static void Main(string[] args)
        {
            EntryPoint("NFSScript");
        }

        /// <summary>
        /// Call the OnExit method for any loaded scripts and shuts down the program.
        /// </summary>
        private static void Terminate()
        {
            CallScriptsOnExit();
            timer.Dispose();
            scripts = null;
            inGameplay = false;
            inRace = false;
            gameMemory = null;
            _hookID = IntPtr.Zero;
            Environment.Exit(0);
        }

        /// <summary>
        /// Loads data from the .ini file.
        /// </summary>
        private static void INIInit()
        {
            INIFile ini = new INIFile(NFS_SCRIPT_INI_FILE_NAME);
            if (File.Exists(ini.Path))
            {
                if (ini.KeyExists("ShowConsole", "NFSScript"))
                {
                    settingShowConsole = FlexiableParse(ini.Read("ShowConsole", "NFSScript"));                    
                }
                if (ini.KeyExists("Debug", "NFSScript"))
                {
                    settingDebug = FlexiableParse(ini.Read("Debug", "NFSScript"));
                }
            }

            if (settingShowConsole == 0)
                NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), 0);
        }

        /// <summary>
        /// Detects and applies process variables for the supported NFS games.
        /// </summary>
        private static void GetNFSGame()
        {
            Log.Print(NFSScriptLoader.INFO_TAG, "Getting executeable in directory.");
            currentNFSGame = NFS.DetectGameExecutableInDirectory;
            int wrldTick = 0;
            switch (currentNFSGame)
            {
                case NFSGame.None:
                    Log.Print(NFSScriptLoader.ERROR_TAG, "A valid NFS executeable was not found.");
                    Environment.Exit(0);
                    return;

                case NFSGame.Carbon:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: Carbon detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.NFSC_EXE)));
                    break;

                case NFSGame.MW:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: Most Wanted detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.SPEED_EXE)));
                    break;

                case NFSGame.Underground:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: Underground detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.SPEED_EXE)));
                    break;

                case NFSGame.Underground2:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: Underground 2 detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.SPEED2_EXE)));
                    break;

                case NFSGame.ProStreet:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: ProStreet detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.NFS_EXE)));
                    break;

                case NFSGame.Undercover:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: Undercover detected.");
                    SetProcessVariables(Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NFS.NFS_EXE)));
                    break;

                case NFSGame.World:
                    Log.Print(NFSScriptLoader.INFO_TAG, "Waiting for game's launch.");
                    while (Process.GetProcessesByName("nfsw").Length == 0 && (wrldTick < 120))
                    {
                        Thread.Sleep(1000);
                        wrldTick++;
                    }
                    if (Process.GetProcessesByName("nfsw").Length == 0)
                        Environment.Exit(0);
                    else
                    {
                        Log.Print(NFSScriptLoader.INFO_TAG, "Need for Speed: World detected.");
                        SetProcessVariables(Process.GetProcessesByName("nfsw")[0]);
                    }
                    break;

                default:
                    Log.Print(NFSScriptLoader.ERROR_TAG, "A valid NFS executeable was not found.");
                    Environment.Exit(0);
                    return;
            }
            NFSScript.NFSScript.CurrentLoadedNFSGame = currentNFSGame;
        }

        /// <summary>
        /// Get the game's memory by it's process name.
        /// </summary>
        /// <param name="processName"></param>
        private static void GetGameMemory(string processName)
        {
            gameMemory = new RunningMemory(gameProcessName);
            gameMemory.ReadInt32((IntPtr)0);

            GameMemory.memory = gameMemory;
            GameMemory.genericMemory = new GMemory(gameMemory.processName);
            if (settingDebug == 1)
                RunningMemory.debugMode = true;
        }

        /// <summary>
        /// Start loading scripts and applying keys & scripts events.
        /// </summary>
        private static void Start()
        {
            LoadScripts();

            GetGameMemory(gameProcessName);
            CallPreScriptMethod();
            Log.Print(NFSScriptLoader.INFO_TAG, string.Format("{0} {1} {2}", "Delaying the loader's thread for", (WAIT_BEFORE_LOAD / 1000), "seconds before initializing."));
            Thread.Sleep(WAIT_BEFORE_LOAD);
            Log.Print(NFSScriptLoader.INFO_TAG, "Delay is over, initializing.");
            // Step 1
            if (NFS.IsGameRunning())
            {
                CallInitScriptMethod();
                WaitForGameLoad();

                ApplyAndLoadIntPtrs();
                Log.Print(NFSScriptLoader.INFO_TAG, "Game is fully loaded.");

                CallMainScriptMethod();

                timer = new Tick();
                timer.Interval = UPDATE_TICK;
                timer.Elapsed += Update_Elapsed;
                timer.Start();

                SetKeyboardHook();
            }
            else Environment.Exit(0);
        }

        /// <summary>
        /// Wait till the game loads.
        /// </summary>
        private static void WaitForGameLoad()
        {
            Log.Print(NFSScriptLoader.INFO_TAG, "Waiting for the game to fully load. (Disabled in Most Wanted)");
            switch (currentNFSGame)
            {
                case NFSGame.None:
                    break;

                case NFSGame.Underground:
                    while (gameMemory.ReadByte((IntPtr)UGAddresses.GenericAddrs.STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;

                case NFSGame.Underground2:
                    while (gameMemory.ReadByte((IntPtr)UG2Addresses.GenericAddrs.STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;

                case NFSGame.MW:
                    break;

                case NFSGame.Carbon:
                    while (gameMemory.ReadByte((IntPtr)CarbonAddresses.GenericAddrs.STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;

                case NFSGame.ProStreet:
                    while(gameMemory.ReadByte((IntPtr)ProStreetAddresses.GenericAddrs.STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;

                case NFSGame.Undercover:
                    while (gameMemory.ReadByte((IntPtr)UndercoverAddresses.GenericAddrs.STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;

                case NFSGame.World:
                    while (gameMemory.ReadByte((IntPtr)gameMemory.getBaseAddress + WorldAddresses.GenericAddrs.NON_STATIC_IS_GAME_LOADED) != 0x01 && NFS.IsGameRunning())
                    {
                        Thread.Sleep(100);
                    }
                    break;
            }
        }

        /// <summary>
        /// Applies and loads necessary memory pointers.
        /// </summary>
        private static void ApplyAndLoadIntPtrs()
        {
            switch (currentNFSGame)
            {
                case NFSGame.None:
                    break;

                case NFSGame.Underground:
                    GAMEPLAY_ACTIVE = (IntPtr)UGAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = (IntPtr)UGAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    break;

                case NFSGame.Underground2:
                    GAMEPLAY_ACTIVE = (IntPtr)UG2Addresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = IntPtr.Zero;
                    break;

                case NFSGame.MW:
                    GAMEPLAY_ACTIVE = (IntPtr)MWAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = (IntPtr)MWAddresses.GameAddrs.STATIC_IS_ACTIVITY_MODE;
                    break;

                case NFSGame.Carbon:
                    GAMEPLAY_ACTIVE = (IntPtr)CarbonAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = (IntPtr)CarbonAddresses.GameAddrs.STATIC_IS_ACTIVITY_MODE;
                    break;

                case NFSGame.ProStreet:
                    GAMEPLAY_ACTIVE = (IntPtr)ProStreetAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = (IntPtr)ProStreetAddresses.GameAddrs.STATIC_IS_ACTIVITY_MODE;
                    break;

                case NFSGame.Undercover:
                    GAMEPLAY_ACTIVE = (IntPtr)UndercoverAddresses.GenericAddrs.STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = IntPtr.Zero;
                    break;

                case NFSGame.World:
                    GAMEPLAY_ACTIVE = (IntPtr)gameMemory.getBaseAddress + WorldAddresses.GenericAddrs.NON_STATIC_IS_GAMEPLAY_ACTIVE;
                    IS_ACTIVITY_MODE = (IntPtr)gameMemory.getBaseAddress + WorldAddresses.GameAddrs.NON_STATIC_IS_ACTIVITY_MODE;
                    break;
            }
        }

        /// <summary>
        /// Load supported .NET scripts from the /scripts directory.
        /// </summary>
        private static void LoadScripts()
        {
            scripts = new List<ModScript>();
            string[] dllFiles = Directory.GetFiles(NFSScriptLoader.SCRIPTS_FOLDER, "*.dll");
            string[] csFiles = Directory.GetFiles(NFSScriptLoader.SCRIPTS_FOLDER, "*.cs");
            string[] vbFiles = Directory.GetFiles(NFSScriptLoader.SCRIPTS_FOLDER, "*.vb");

            int compiledScripts = 0;

            for (int i = 0; i < dllFiles.Length; i++)
            {
                if (IsValidAssembly(dllFiles[i]))
                {
                    ModScript script = new ModScript(dllFiles[i]);
                    scripts.Add(script);
                    compiledScripts++;
                    Log.Print(NFSScriptLoader.INFO_TAG, string.Format("{0} {1}", "Loaded", script.File));
                }
            }
            for (int i = 0; i < csFiles.Length; i++)
            {
                CompileScript(csFiles[i]);
                compiledScripts++;
            }
            for (int i = 0; i < vbFiles.Length; i++)
            {
                CompileScript(vbFiles[i]);
                compiledScripts++;
            }

            Log.Print(NFSScriptLoader.INFO_TAG, string.Format("{0} scripts are loaded.", compiledScripts));
        }

        private static void CallPreScriptMethod()
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].HasPre)
                    scripts[i].CallModFunction(ModScript.ModMethod.Pre);
            }
        }

        /// <summary>
        /// Call scripts Init() method.
        /// </summary>
        private static void CallInitScriptMethod()
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].HasInitialize)
                    scripts[i].CallModFunction(ModScript.ModMethod.Initialize);
            }
        }

        /// <summary>
        /// Call scripts Main() method.
        /// </summary>
        private static void CallMainScriptMethod()
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].HasMain)
                    scripts[i].CallModFunction(ModScript.ModMethod.Main);
            }
        }

        /// <summary>
        /// Call scripts methods such as OnKeyUp, OnKeyDown & Update.
        /// </summary>
        private static void CallScriptsEvents()
        {
            bool isGameplayActive = false;
            if (currentNFSGame != NFSGame.Undercover && currentNFSGame != NFSGame.World)
                isGameplayActive = gameMemory.ReadByte(GAMEPLAY_ACTIVE) == 1;
            else if(currentNFSGame == NFSGame.Undercover)
                isGameplayActive = gameMemory.ReadInt32((IntPtr)UndercoverAddresses.GenericAddrs.STATIC_GAME_STATE) == 6;
            else if (currentNFSGame == NFSGame.World)
                isGameplayActive = gameMemory.ReadInt32((IntPtr)gameMemory.getBaseAddress + WorldAddresses.GenericAddrs.NON_STATIC_GAME_STATE) == 6;

            if (isGameplayActive && !inGameplay)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    if (scripts[i].HasOnGameplayStart && !scripts[i].IsInGameplay)
                    {
                        scripts[i].CallModFunction(ModScript.ModMethod.OnGameplayStart);
                        scripts[i].IsInGameplay = true;
                    }
                }
                inGameplay = true;
            }

            if (!isGameplayActive && inGameplay)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    if (scripts[i].HasOnGameplayExit && scripts[i].IsInGameplay)
                    {
                        scripts[i].CallModFunction(ModScript.ModMethod.OnGameplayExit);
                        scripts[i].IsInGameplay = false;
                    }
                }
                inGameplay = false;
            }

            if ((gameMemory.ReadByte(IS_ACTIVITY_MODE)) == 1 && !inRace)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    if (scripts[i].HasOnActivityEnter && !scripts[i].IsInActivity)
                    {
                        scripts[i].CallModFunction(ModScript.ModMethod.OnActivityEnter);
                        scripts[i].IsInActivity = true;
                    }
                }
                inRace = true;
            }

            if ((gameMemory.ReadByte(IS_ACTIVITY_MODE)) == 0 && inRace)
            {
                for (int i = 0; i < scripts.Count; i++)
                {
                    if (scripts[i].HasOnActivityExit && scripts[i].IsInActivity)
                    {
                        scripts[i].CallModFunction(ModScript.ModMethod.OnActivityExit);
                        scripts[i].IsInActivity = false;
                    }
                }
                inRace = false;
            }
        }

        /// <summary>
        /// Call scripts OnExit() method.
        /// </summary>
        private static void CallScriptsOnExit()
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].HasOnExit)
                    scripts[i].CallModFunction(ModScript.ModMethod.OnExit);
            }
        }

        /// <summary>
        /// Update elapsed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Update_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!NFS.IsGameRunning())
            {
                Log.Print(NFSScriptLoader.INFO_TAG, "Game is not running.");
                Terminate();                
            }

            CallScriptsEvents();
            for (int i = 0; i < scripts.Count; i++)
            {
                if (scripts[i].HasUpdate)
                    scripts[i].CallModFunction(ModScript.ModMethod.Update);
            }
        }

        /// <summary>
        /// Compile script by file.
        /// </summary>
        /// <param name="file"></param>
        private static bool CompileScript(string file)
        {
            FileInfo sourceFile = new FileInfo(file);
            CodeDomProvider provider = null;
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });

            var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll", "System.Windows.Forms.dll", "NFSScript.dll" });
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;

            if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".VB")
            {
                provider = CodeDomProvider.CreateProvider("VisualBasic");
            }

            if (provider != null)
            {
                CompilerResults results = provider.CompileAssemblyFromFile(parameters, file);
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError r in results.Errors)
                    {
                        Log.Print(NFSScriptLoader.ERROR_TAG, r.ToString());
                    }
                    return false;
                }
                else
                {
                    Assembly ass = results.CompiledAssembly;
                    ModScript script = new ModScript(ass, Path.GetFileName(file));
                    scripts.Add(script);
                    Log.Print(NFSScriptLoader.INFO_TAG, string.Format("{0} {1}", "Loaded", script.File));

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Restart the program.
        /// </summary>
        private static void Restart()
        {
            Log.Print(NFSScriptLoader.INFO_TAG, "Restarting...");
            CallScriptsOnExit();
            timer.Stop();
            scripts = null;
            inGameplay = false;
            inRace = false;
            LoadScripts();
            CallPreScriptMethod();
            CallInitScriptMethod();
            CallMainScriptMethod();            
            timer.Start();
        }

        /// <summary>
        /// A flexiable parse method for the NFSScriptLoader.ini file.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static int FlexiableParse(string s)
        {
            try {
                return int.Parse(s);
            }
            catch { return 0; }
        }

        /// <summary>
        /// Returns a value that indicates whether the specified .dll is a valid assembly file or not.
        /// </summary>
        /// <param name="dll"></param>
        /// <returns></returns>
        private static bool IsValidAssembly(string dll)
        {
            Assembly a = null;
            try
            {
                a = Assembly.LoadFrom(dll);
                return true;
            }
            catch { return false; }
        }

        public static bool IsRunning(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set keyboard's hook to get any pressed key when the game is maximized.
        /// </summary>
        private static void SetKeyboardHook()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            NativeMethods.UnhookWindowsHookEx(_hookID);
        }

        /// <summary>
        /// Print a debug message to the log if the debug value is set to 1 in the .ini file.
        /// </summary>
        /// <param name="msg"></param>
        private static void PrintDebugMsg(string msg)
        {
            if (settingDebug == 1)
                Log.Print(NFSScriptLoader.DEBUG_TAG, msg);
        }

        /// <summary>
        /// Get the hook callback.
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr mainWindowHandle = gameMemory.GetMainProcess().MainWindowHandle;

            if ((currentNFSGame != NFSGame.World && !NFS.IsGameMinimized()) || (currentNFSGame == NFSGame.World && NFS.IsGameFocused()))
            {
                if (nCode >= 0 && wParam == (IntPtr)257) // OnKeyUp
                {                    
                    int num = Marshal.ReadInt32(lParam);

                    if (num == resetKey)
                        Restart();

                    for (int i = 0; i < scripts.Count; i++)
                    {
                        if (scripts[i].HasOnKeyUp)
                        {
                            scripts[i].CallModFunction(ModScript.ModMethod.OnKeyUp, (NFSSKeys)num);
                        }
                    }
                }

                if (nCode >= 0 && wParam == (IntPtr)256) // OnKeyDown
                {
                    int num = Marshal.ReadInt32(lParam);
                    for (int i = 0; i < scripts.Count; i++)
                    {
                        if (scripts[i].HasOnKeyDown)
                        {
                            scripts[i].CallModFunction(ModScript.ModMethod.OnKeyDown, (NFSSKeys)num);
                        }
                    }
                }
            }

            return NativeMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// Set process variables.
        /// </summary>
        /// <param name="p"></param>
        private static void SetProcessVariables(Process p)
        {
            gameProcess = p;
            gameProcessName = p.ProcessName;
            processNameTitle = p.MainWindowTitle;

            Log.Print(NFSScriptLoader.INFO_TAG, "Game memory is loaded.");
        }

        /// <summary>
        /// Set hook.
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule mainModule = currentProcess.MainModule)
                    return NativeMethods.SetWindowsHookEx(13, proc, NativeMethods.GetModuleHandle(mainModule.ModuleName), 0U);
            }
        }

    }

    struct NFS
    {
        // A list of constant strings that contain the executable file name.
        public const string SPEED_EXE = "speed.exe";
        public const string SPEED2_EXE = "speed2.exe";
        public const string NFSC_EXE = "NFSC.exe";
        public const string NFS_EXE = "nfs.exe";
        public const string NFSW_EXE = "nfsw.exe";

        /// <summary>
        /// Detects the supported game executable in directory.
        /// </summary>
        public static NFSGame DetectGameExecutableInDirectory
        {
            // This is a really bad way of detecting executables
            // TODO: Get game versions by their entry points
            get
            {
                if (File.Exists(SPEED_EXE)) // speed.exe exists
                {
                    using (StreamReader sr = new StreamReader(SPEED_EXE))
                    {
                        string input;
                        while ((input = sr.ReadLine()) != null)
                        {
                            if (input.Contains("Most Wanted")) // speed.exe contains the string "Most Wanted"
                                return NFSGame.MW; // Return NFSGame.MW
                            else if (input.Contains("underground")) // speed.exe contains the string "underground"
                                return NFSGame.Underground; // Return NFSGame.Underground
                        }
                    }
                    return NFSGame.Undetermined;
                }
                else if (File.Exists(SPEED2_EXE)) // speed2.exe exists
                {
                    return NFSGame.Underground2; //Return NFSGame.Underground2
                }
                else if (File.Exists(NFSC_EXE)) // NFSC.exe exists
                {
                    return NFSGame.Carbon; // Return NFSGame.Carbon
                }
                else if (File.Exists(NFS_EXE)) // nfs.exe exists
                {
                    using (StreamReader sr = new StreamReader(NFS_EXE))
                    {
                        string input;
                        while ((input = sr.ReadLine()) != null)
                        {
                            if (input.Contains("Undercover")) // nfs.exe contains "undercover"
                                return NFSGame.Undercover; // Return NFSGame.Undercover
                        }
                    }
                    return NFSGame.ProStreet;
                }
                else if (File.Exists(NFSW_EXE))
                {
                    return NFSGame.World; // Return NFSGame.World
                }
                else return NFSGame.None;
            }
        }

        public static bool IsGameMinimized()
        {
            return CurrentGame.IsMinimized;
        }

        public static bool IsGameFocused()
        {
            return CurrentGame.IsGameInFocus;
        }

        public static bool IsGameRunning()
        {
            return (uint)Process.GetProcessesByName(Program.gameMemory.GetMainProcess().ProcessName).Length > 0U;
        }
    }

    public struct NFSScriptLoader
    {
        public const string LOADER_TAG = "NFSScriptLoader";
        public const string ERROR_TAG = "NFSScriptLoader ERROR";
        public const string INFO_TAG = "NFSScriptLoader INFO";
        public const string DEBUG_TAG = "NFSScriptLoader DEBUG";
        public const string SCRIPTS_FOLDER = "scripts\\";

        public static RunningMemory GetRunningGameMemory()
        {
            return Program.gameMemory;
        }
    }

    public static class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, Program.LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        public static extern IntPtr GetConsoleWindow();
    }
}
