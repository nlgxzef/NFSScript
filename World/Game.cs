using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NFSScript.Core;
using static NFSScript.Core.GameMemory;
using static NFSScript.Core.WorldAddresses;
using static NFSScript.WorldFunctions;
using static NFSScript.World.EASharpBindings;
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
                return memory.ReadFloat((IntPtr)memory.getBaseAddress + GenericAddrs.NON_STATIC_LAST_FRAME_TIME);
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
            get { return memory.ReadFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_EVENT_MUSIC_VOLUME); }
            set { memory.WriteFloat((IntPtr)memory.getBaseAddress + GameAddrs.NON_STATIC_EVENT_MUSIC_VOLUME, value); }
        }

        /// <summary>
        /// Shakes the camera.
        /// </summary>
        public static void CameraShake()
        {
            Function.Call(BASE_CAMERA_SHAKE + MemoryBase.FunctionBase);
        }

        /// <summary>
        /// Audio class.
        /// </summary>
        public static class Audio
        {
            /// <summary>
            /// Plays a sound.
            /// </summary>
            /// <param name="pSoundString">Sound string.</param>
            public static void PlaySound(string pSoundString)
            {
                CallBinding(_EASharpBinding_16, pSoundString);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="pMixTriggerString"></param>
            /// <param name="value"></param>
            public static unsafe void SendMixTrigger(string pMixTriggerString, int value)
            {               
                CallBinding(_EASharpBinding_17, pMixTriggerString, value);
            }
        }

        /// <summary>
        /// Cameras class.
        /// </summary>
        public static class Cameras
        {
            /// <summary>
            /// 
            /// </summary>
            public static void ClearAllBounds()
            {
                CallBinding(_EASharpBinding_27);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="posX"></param>
            /// <param name="posY"></param>
            /// <param name="posZ"></param>
            /// <param name="height"></param>
            /// <param name="radius"></param>
            public static void AssignCylinderBounds(float posX, float posY, float posZ, float height, float radius)
            {
                CallBinding(_EASharpBinding_28, posX, posY, posZ, height, radius);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cameraName"></param>
            public static unsafe void Set(string cameraName)
            {
                CallBinding(_EASharpBinding_33, cameraName);
            }
        }

        /// <summary>
        /// A class for cops.
        /// </summary>
        public static class Cops
        {
            /// <summary>
            /// Launches a pursuit.
            /// </summary>
            /// <param name="heat">Heat level.</param>
            /// <param name="spawnpursuitcar">Whether to spawn a pursuit car or not.</param>
            public static void LaunchPursuit(int heat, bool spawnpursuitcar)
            {
                CallBinding(_EASharpBinding_39, heat, spawnpursuitcar);
            }

            /// <summary>
            /// Reinitiates a pursuit.
            /// </summary>
            /// <param name="spawnpursuitcar">Whether to spawn a pursuit car or not.</param>
            public static void ReinitiatePursuit(bool spawnpursuitcar)
            {
                CallBinding(_EASharpBinding_40, spawnpursuitcar);
            }

            /// <summary>
            /// 
            /// </summary>
            public static void ResetPursuitStats()
            {
                CallBinding(_EASharpBinding_41);
            }

            /// <summary>
            /// Kills the pursuit.
            /// </summary>
            public static void KillPursuit()
            {
                CallBinding(_EASharpBinding_42);
            }

            /// <summary>
            /// 
            /// </summary>
            public static void FlushCops()
            {
                CallBinding(_EASharpBinding_43);
            }

            /// <summary>
            /// Set's the pursuit heat.
            /// </summary>
            /// <param name="heat">Heat level</param>
            public static void SetPursuitHeat(float heat)
            {
                CallBinding(_EASharpBinding_44, heat);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="vehicleKey"></param>
            /// <param name="count"></param>
            public static void PrimeCopVehicleCache(uint vehicleKey, int count)
            {
                CallBinding(_EASharpBinding_48, vehicleKey, count);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="enable"></param>
            public static void EnablePursuitSynchronization(bool enable)
            {
                CallBinding(_EASharpBinding_49, enable);
            }
        }

        /// <summary>
        /// Game's world class.
        /// </summary>
        public static class World
        {
            /// <summary>
            /// Disables barriers.
            /// </summary>
            public static void DisableBarriers()
            {
                CallBinding(_EASharpBinding_543);
            }

            /// <summary>
            /// Enables a barrier.
            /// </summary>
            /// <param name="barrier">Barrier string.</param>
            /// <param name="isFlipped">Whether the barrier be flipped or not.</param>
            public static void EnableBarrier(string barrier, bool isFlipped)
            {
                CallBinding(_EASharpBinding_544, barrier, isFlipped);
            }

            /// <summary>
            /// Disables a barrier.
            /// </summary>
            /// <param name="barrier">Barrier string.</param>
            public static void DisableBarrier(string barrier)
            {
                CallBinding(_EASharpBinding_545, barrier);
            }

            /// <summary>
            /// Reset props.
            /// </summary>
            public static void ResetProps()
            {
                CallBinding(_EASharpBinding_546);
            }
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
        public static IntPtr address { get { return (IntPtr)memory.getBaseAddress + GenericAddrs.NON_STATIC_GAME_STATE; } }

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
