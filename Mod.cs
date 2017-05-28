using System;
using System.Threading.Tasks;

namespace NFSScript
{
    /// <summary>
    /// Mod is the base class from which every NFSScript mod derives.
    /// </summary>
    public class Mod
    {
        /// <summary>
        /// The initialize method name.
        /// </summary>
        public const string INITIALIZE_METHOD = "Initialize";

        /// <summary>
        /// The pre method name.
        /// </summary>
        public const string PRE_METHOD = "Pre";

        /// <summary>
        /// The main method name.
        /// </summary>
        public const string MAIN_METHOD = "Main";

        /// <summary>
        /// The update method name.
        /// </summary>
        public const string UPDATE_METHOD = "Update";

        /// <summary>
        /// The OnKeyDown method name.
        /// </summary>
        public const string ONKEYDOWN_METHOD = "OnKeyDown";

        /// <summary>
        /// The OnKeyUp method name.
        /// </summary>
        public const string ONKEYUP_METHOD = "OnKeyUp";

        /// <summary>
        /// The OnGameplayStart method name.
        /// </summary>
        public const string ONGAMEPLAYSTART_METHOD = "OnGameplayStart";

        /// <summary>
        /// The OnGameplayExit method name.
        /// </summary>
        public const string ONGAMEPLAYEXIT_METHOD = "OnGameplayExit";

        /// <summary>
        /// The OnActivityEnter method name.
        /// </summary>
        public const string ONACTIVITYENTER_METHOD = "OnActivityEnter";

        /// <summary>
        /// The OnActivityExit method name.
        /// </summary>
        public const string ONACTIVITYEXIT_METHOD = "OnActivityExit";

        /// <summary>
        /// The OnExit method name.
        /// </summary>
        public const string ONEXIT_METHOD = "OnExit";

        /// <summary>
        /// Pre is called as soon as the the game memory loads.
        /// </summary>
        public virtual void Pre()
        { }

        /// <summary>
        /// Initialize is called when the script is ready and enabled just before any of the Main and Update methods are called.
        /// </summary>
        public virtual void Initialize()
        { }

        /// <summary>
        /// Main is called after the Initialize method is called and before the Update method is called.
        /// </summary>
        public virtual void Main()
        { }

        /// <summary>
        /// Update method is called every 1ms.
        /// </summary>
        public virtual void Update()
        { }

        /// <summary>
        /// This method is called when the gameplay starts.
        /// </summary>
        public virtual void OnGameplayStart()
        { }

        /// <summary>
        /// This method is called when the gameplay is stopped (When the player enters the garage/car lot/etc...).
        /// </summary>
        public virtual void OnGameplayExit()
        { }

        /// <summary>
        /// This method is called when the player has entered an activity (Doesn't work in Most Wanted and Undercover, yet).
        /// </summary>
        public virtual void OnActivityEnter()
        { }

        /// <summary>
        /// This method is called when the player has exited from an activity (Doesn't work in Most Wanted and Undercover, yet).
        /// </summary>
        public virtual void OnActivityExit()
        { }

        /// <summary>
        /// This method is called when a key is being held in-game.
        /// </summary>
        public virtual void OnKeyDown(Keys key)
        { }

        /// <summary>
        /// This method is called when a key has been released in-game.
        /// </summary>
        public virtual void OnKeyUp(Keys key)
        { }
        
        /// <summary>
        /// This method is called when the script is being terminated.
        /// </summary>
        public virtual void OnExit()
        { }

        /// <summary>
        /// Peforms a task.
        /// </summary>
        /// <param name="action"></param>
        public static void DoTask(Action action)
        {
            action();
        }

        /// <summary>
        /// Peforms an asynchronous task.
        /// </summary>
        /// <param name="action"></param>
        public static void DoAsyncTask(Action action)
        {
            var task = new Task(() => { action(); });
            task.Start();
        }

        /// <summary>
        /// Peforms a task after a number of defined milliseconds without blocking the thread.
        /// </summary>
        /// <param name="action">The action to peform.</param>
        /// <param name="secondsBeforeExecuting">The amount of time to wait in milliseconds before executing the task.</param>
        public static void DoTimedTask(Action action, int secondsBeforeExecuting)
        {
            var task = new Task(() =>
            {
                System.Threading.Thread.Sleep(secondsBeforeExecuting);
                action();
            });
            task.Start();
        }
    }
}
