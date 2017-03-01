using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NFSScript.Core;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using static NFSScript.WorldFunctions;
using NFSScript.Math;

namespace NFSScript.World
{
    /// <summary>
    /// A class that represents the main game manager.
    /// </summary>
    public static class Game
    {        
        /// <summary>
        /// Returns the amount of seconds it takes to render a frame.
        /// </summary>
        public static float lastFrameTime
        {
            get
            {
                return memory.ReadFloat((IntPtr)memory.getBaseAddress + GenericAddrs.STATIC_LAST_FRAME_TIME);
            }
        }

        /// <summary>
        /// Returns true if the gameplay is currently active.
        /// </summary>
        public static bool isGameplayActive
        {
            get
            {
                return memory.ReadByte((IntPtr)memory.getBaseAddress + GenericAddrs.NON_STATIC_IS_GAMEPLAY_ACTIVE) == 1;
            }
        }

        /// <summary>
        /// The master sound volume.
        /// </summary>
        public static float masterVolume
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_MASTER_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_MASTER_VOLUME, value); }
        }

        /// <summary>
        /// The speech sound volume.
        /// </summary>
        public static float speechVolume
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_SPEECH_VOLUME); }
            set { memory.WriteFloat((IntPtr)GameAddrs.NON_STATIC_SPEECH_VOLUME, value); }
        }

        /// <summary>
        /// The sound effects sound volume.
        /// </summary>
        public static float soundEffectsVolume
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_SOUND_EFFECTS_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_SOUND_EFFECTS_VOLUME, value); }
        }

        /// <summary>
        /// The car sound volume.
        /// </summary>
        public static float carVolume
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_CAR_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_CAR_VOLUME, value); }
        }

        /// <summary>
        /// The free roam music volume.
        /// </summary>
        public static float freeRoamMusicVolune
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_FREE_ROAM_MUSIC_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_FREE_ROAM_MUSIC_VOLUME, value); }
        }

        /// <summary>
        /// The event music volume.
        /// </summary>
        public static float eventMusicVolume
        {
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.STATIC_EVENT_MUSIC_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.STATIC_EVENT_MUSIC_VOLUME, value); }
        }

        /// <summary>
        /// Shakes the camera.
        /// </summary>
        public static void CameraShake()
        {
            Function.Call(BASE_CAMERA_SHAKE + MemoryBase.FunctionBase);
        }
    }

    /// <summary>
    /// The game's flow manager class.
    /// </summary>
    public class GameFlowManager
    {
        /// <summary>
        /// The address where the main GameFlowManager is located at.
        /// </summary>
        public static IntPtr address { get { return (IntPtr)GenericAddrs.NON_STATIC_GAME_STATE; } }

        private int gameStateValue;

        /// <summary>
        /// The main GameFlowManager.
        /// </summary>
        public static GameFlowManager TheGameFlowManager { get { return new GameFlowManager(memory.ReadInt32(address)); } }

        /// <summary>
        /// Instantiate a GameFlowManager class.
        /// </summary>
        /// <param name="gameStateValue"></param>
        private GameFlowManager(int gameStateValue)
        {
            this.gameStateValue = gameStateValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public static implicit operator int(GameFlowManager instance)
        {
            if (instance == null)
            {
                return -1;
            }
            return instance.gameStateValue;
        }

        /// <summary>
        /// Returns the game state value string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return gameStateValue.ToString();
        }
    }
}
