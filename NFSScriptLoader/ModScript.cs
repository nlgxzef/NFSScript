using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using NFSScript;

namespace NFSScriptLoader
{
    public class ModScript
    {
        public string File { get; private set; }
        Type[] t;
        MethodInfo[] methods;

        public bool HasInitialize { get; private set; }
        public bool HasPre { get; private set; }
        public bool HasMain { get; private set; }
        public bool HasUpdate { get; private set; }
        public bool HasOnKeyUp { get; private set; }
        public bool HasOnKeyDown { get; private set; }
        public bool HasOnGameplayStart { get; private set; }
        public bool HasOnGameplayExit { get; private set; }
        public bool HasOnActivityEnter { get; private set; }
        public bool HasOnActivityExit { get; private set; }
        public bool HasOnExit { get; private set; }

        public bool IsInGameplay { get; set; }
        public bool IsInActivity { get; set; }

        public ModScript(string dllFile)
        {
            IsInGameplay = false;
            IsInActivity = false;

            File = dllFile;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, File);
            t = NReflec.GetTypesFromDLL(path);
            methods = NReflec.GetMethodsFromDLL(path);
            CheckMethods();
        }

        public ModScript(Assembly ass, string fileName)
        {
            IsInGameplay = false;
            IsInActivity = false;

            File = fileName;
            t = NReflec.GetTypesFromAssembly(ass);
            methods = NReflec.GetMethodsFromAssembly(ass);
            CheckMethods();
        }

        private void CheckMethods()
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name.Equals(Mod.PRE_METHOD))
                    HasPre = true;

                if (methods[i].Name.Equals(Mod.INITIALIZE_METHOD))
                    HasInitialize = true;

                if (methods[i].Name.Equals(Mod.MAIN_METHOD))
                    HasMain = true;

                if (methods[i].Name.Equals(Mod.UPDATE_METHOD))
                    HasUpdate = true;

                if (methods[i].Name.Equals(Mod.ONKEYUP_METHOD))
                    HasOnKeyUp = true;

                if (methods[i].Name.Equals(Mod.ONKEYDOWN_METHOD))
                    HasOnKeyDown = true;

                if (methods[i].Name.Equals(Mod.ONGAMEPLAYSTART_METHOD))
                    HasOnGameplayStart = true;

                if (methods[i].Name.Equals(Mod.ONGAMEPLAYEXIT_METHOD))
                    HasOnGameplayExit = true;

                if (methods[i].Name.Equals(Mod.ONACTIVITYENTER_METHOD))
                    HasOnActivityEnter = true;

                if (methods[i].Name.Equals(Mod.ONACTIVITYEXIT_METHOD))
                    HasOnActivityExit = true;

                if (methods[i].Name.Equals(Mod.ONEXIT_METHOD))
                    HasOnExit = true;
            }
        }

        public void CallModFunction(ModMethod modMethod, params object[] o)
        {
            string method = string.Empty;
            switch (modMethod)
            {
                case ModMethod.None:
                    method = string.Empty;
                    break;

                case ModMethod.Pre:
                    method = Mod.PRE_METHOD;
                    break;

                case ModMethod.Initialize:
                    method = Mod.INITIALIZE_METHOD;
                    break;

                case ModMethod.Main:
                    method = Mod.MAIN_METHOD;
                    break;

                case ModMethod.Update:
                    method = Mod.UPDATE_METHOD;
                    break;

                case ModMethod.OnKeyDown:
                    method = Mod.ONKEYDOWN_METHOD;
                    break;

                case ModMethod.OnKeyUp:
                    method = Mod.ONKEYUP_METHOD;
                    break;

                case ModMethod.OnGameplayStart:
                    method = Mod.ONGAMEPLAYSTART_METHOD;
                    break;

                case ModMethod.OnGameplayExit:
                    method = Mod.ONGAMEPLAYEXIT_METHOD;
                    break;

                case ModMethod.OnActivityEnter:
                    method = Mod.ONACTIVITYENTER_METHOD;
                    break;

                case ModMethod.OnActivityExit:
                    method = Mod.ONACTIVITYEXIT_METHOD;
                    break;

                case ModMethod.OnExit:
                    method = Mod.ONEXIT_METHOD;
                    break;
            }
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i].IsSubclassOf(typeof(Mod)))
                {
                    try
                    {
                        NReflec.CallMethodFromType(t[i], method, o);
                    }
                    catch (Exception e) {
                        Log.Print("EXCEPTION", e.ToString());
                    }
                }
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ModScript: File = ");
            sb.Append(File);
            sb.Append(" Types: ");
            sb.Append(t.Length);
            sb.Append(" Methods: ");
            for (int i = 0; i < methods.Length; i++)
            {
                sb.AppendLine(string.Format("{0} {1}: {2} ", "Method", i, methods[i].Name));
            }
            return sb.ToString();
        }

        public enum ModMethod : byte
        {
            None = 0,
            Initialize = 1,
            Main = 2,
            Update = 3,
            OnKeyDown = 4,
            OnKeyUp = 5,
            OnGameplayStart = 6,
            OnGameplayExit = 7,
            OnActivityEnter = 8,
            OnActivityExit = 9,
            OnExit = 10,
            Pre = 11
        }
    }
}
