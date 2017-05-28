using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NFSScript
{
    /// <summary>
    /// Specifies key codes and modifiers.
    /// </summary>
    [ComVisible(true)]
    public enum Keys
    {
        /// <summary/>
        KeyCode = 65535,
        /// <summary/>
        Modifiers = -65536,
        /// <summary/>
        None = 0,
        /// <summary/>
        LButton = 1,
        /// <summary/>
        RButton = 2,
        /// <summary/>
        Cancel = RButton | LButton,
        /// <summary/>
        MButton = 4,
        /// <summary/>
        XButton1 = MButton | LButton,
        /// <summary/>
        XButton2 = MButton | RButton,
        /// <summary/>
        Back = 8,
        /// <summary/>
        Tab = Back | LButton,
        /// <summary/>
        LineFeed = Back | RButton,
        /// <summary/>
        Clear = Back | MButton,
        /// <summary/>
        Return = Clear | LButton,
        /// <summary/>
        Enter = Return,
        /// <summary/>
        ShiftKey = 16,
        /// <summary/>
        ControlKey = ShiftKey | LButton,
        /// <summary/>
        Menu = ShiftKey | RButton,
        /// <summary/>
        Pause = Menu | LButton,
        /// <summary/>
        Capital = ShiftKey | MButton,
        /// <summary/>
        CapsLock = Capital,
        /// <summary/>
        KanaMode = CapsLock | LButton,
        /// <summary/>
        HanguelMode = KanaMode,
        /// <summary/>
        HangulMode = HanguelMode,
        /// <summary/>
        JunjaMode = HangulMode | RButton,
        /// <summary/>
        FinalMode = ShiftKey | Back,
        /// <summary/>
        HanjaMode = FinalMode | LButton,
        /// <summary/>
        KanjiMode = HanjaMode,
        /// <summary/>
        Escape = KanjiMode | RButton,
        /// <summary/>
        IMEConvert = FinalMode | MButton,
        /// <summary/>
        IMENonconvert = IMEConvert | LButton,
        /// <summary/>
        IMEAccept = IMEConvert | RButton,
        /// <summary/>
        IMEAceept = IMEAccept,
        /// <summary/>
        IMEModeChange = IMEAceept | LButton,
        /// <summary/>
        Space = 32,
        /// <summary/>
        Prior = Space | LButton,
        /// <summary/>
        PageUp = Prior,
        /// <summary/>
        Next = Space | RButton,
        /// <summary/>
        PageDown = Next,
        /// <summary/>
        End = PageDown | LButton,
        /// <summary/>
        Home = Space | MButton,
        /// <summary/>
        Left = Home | LButton,
        /// <summary/>
        Up = Home | RButton,
        /// <summary/>
        Right = Up | LButton,
        /// <summary/>
        Down = Space | Back,
        /// <summary/>
        Select = Down | LButton,
        /// <summary/>
        Print = Down | RButton,
        /// <summary/>
        Execute = Print | LButton,
        /// <summary/>
        Snapshot = Down | MButton,
        /// <summary/>
        PrintScreen = Snapshot,
        /// <summary/>
        Insert = PrintScreen | LButton,
        /// <summary/>
        Delete = PrintScreen | RButton,
        /// <summary/>
        Help = Delete | LButton,
        /// <summary/>
        D0 = Space | ShiftKey,
        /// <summary/>
        D1 = D0 | LButton,
        /// <summary/>
        D2 = D0 | RButton,
        /// <summary/>
        D3 = D2 | LButton,
        /// <summary/>
        D4 = D0 | MButton,
        /// <summary/>
        D5 = D4 | LButton,
        /// <summary/>
        D6 = D4 | RButton,
        /// <summary/>
        D7 = D6 | LButton,
        /// <summary/>
        D8 = D0 | Back,
        /// <summary/>
        D9 = D8 | LButton,
        /// <summary/>
        A = 65,
        /// <summary/>
        B = 66,
        /// <summary/>
        C = B | LButton,
        /// <summary/>
        D = 68,
        /// <summary/>
        E = D | LButton,
        /// <summary/>
        F = D | RButton,
        /// <summary/>
        G = F | LButton,
        /// <summary/>
        H = 72,
        /// <summary/>
        I = H | LButton,
        /// <summary/>
        J = H | RButton,
        /// <summary/>
        K = J | LButton,
        /// <summary/>
        L = H | MButton,
        /// <summary/>
        M = L | LButton,
        /// <summary/>
        N = L | RButton,
        /// <summary/>
        O = N | LButton,
        /// <summary/>
        P = 80,
        /// <summary/>
        Q = P | LButton,
        /// <summary/>
        R = P | RButton,
        /// <summary/>
        S = R | LButton,
        /// <summary/>
        T = P | MButton,
        /// <summary/>
        U = T | LButton,
        /// <summary/>
        V = T | RButton,
        /// <summary/>
        W = V | LButton,
        /// <summary/>
        X = P | Back,
        /// <summary/>
        Y = X | LButton,
        /// <summary/>
        Z = X | RButton,
        /// <summary/>
        LWin = Z | LButton,
        /// <summary/>
        RWin = X | MButton,
        /// <summary/>
        Apps = RWin | LButton,
        /// <summary/>
        Sleep = Apps | RButton,
        /// <summary/>
        NumPad0 = 96,
        /// <summary/>
        NumPad1 = NumPad0 | LButton,
        /// <summary/>
        NumPad2 = NumPad0 | RButton,
        /// <summary/>
        NumPad3 = NumPad2 | LButton,
        /// <summary/>
        NumPad4 = NumPad0 | MButton,
        /// <summary/>
        NumPad5 = NumPad4 | LButton,
        /// <summary/>
        NumPad6 = NumPad4 | RButton,
        /// <summary/>
        NumPad7 = NumPad6 | LButton,
        /// <summary/>
        NumPad8 = NumPad0 | Back,
        /// <summary/>
        NumPad9 = NumPad8 | LButton,
        /// <summary/>
        Multiply = NumPad8 | RButton,
        /// <summary/>
        Add = Multiply | LButton,
        /// <summary/>
        Separator = NumPad8 | MButton,
        /// <summary/>
        Subtract = Separator | LButton,
        /// <summary/>
        Decimal = Separator | RButton,
        /// <summary/>
        Divide = Decimal | LButton,
        /// <summary/>
        F1 = NumPad0 | ShiftKey,
        /// <summary/>
        F2 = F1 | LButton,
        /// <summary/>
        F3 = F1 | RButton,
        /// <summary/>
        F4 = F3 | LButton,
        /// <summary/>
        F5 = F1 | MButton,
        /// <summary/>
        F6 = F5 | LButton,
        /// <summary/>
        F7 = F5 | RButton,
        /// <summary/>
        F8 = F7 | LButton,
        /// <summary/>
        F9 = F1 | Back,
        /// <summary/>
        F10 = F9 | LButton,
        /// <summary/>
        F11 = F9 | RButton,
        /// <summary/>
        F12 = F11 | LButton,
        /// <summary/>
        F13 = F9 | MButton,
        /// <summary/>
        F14 = F13 | LButton,
        /// <summary/>
        F15 = F13 | RButton,
        /// <summary/>
        F16 = F15 | LButton,
        /// <summary/>
        F17 = 128,
        /// <summary/>
        F18 = F17 | LButton,
        /// <summary/>
        F19 = F17 | RButton,
        /// <summary/>
        F20 = F19 | LButton,
        /// <summary/>
        F21 = F17 | MButton,
        /// <summary/>
        F22 = F21 | LButton,
        /// <summary/>
        F23 = F21 | RButton,
        /// <summary/>
        F24 = F23 | LButton,
        /// <summary/>
        NumLock = F17 | ShiftKey,
        /// <summary/>
        Scroll = NumLock | LButton,
        /// <summary/>
        LShiftKey = F17 | Space,
        /// <summary/>
        RShiftKey = LShiftKey | LButton,
        /// <summary/>
        LControlKey = LShiftKey | RButton,
        /// <summary/>
        RControlKey = LControlKey | LButton,
        /// <summary/>
        LMenu = LShiftKey | MButton,
        /// <summary/>
        RMenu = LMenu | LButton,
        /// <summary/>
        BrowserBack = LMenu | RButton,
        /// <summary/>
        BrowserForward = BrowserBack | LButton,
        /// <summary/>
        BrowserRefresh = LShiftKey | Back,
        /// <summary/>
        BrowserStop = BrowserRefresh | LButton,
        /// <summary/>
        BrowserSearch = BrowserRefresh | RButton,
        /// <summary/>
        BrowserFavorites = BrowserSearch | LButton,
        /// <summary/>
        BrowserHome = BrowserRefresh | MButton,
        /// <summary/>
        VolumeMute = BrowserHome | LButton,
        /// <summary/>
        VolumeDown = BrowserHome | RButton,
        /// <summary/>
        VolumeUp = VolumeDown | LButton,
        /// <summary/>
        MediaNextTrack = LShiftKey | ShiftKey,
        /// <summary/>
        MediaPreviousTrack = MediaNextTrack | LButton,
        /// <summary/>
        MediaStop = MediaNextTrack | RButton,
        /// <summary/>
        MediaPlayPause = MediaStop | LButton,
        /// <summary/>
        LaunchMail = MediaNextTrack | MButton,
        /// <summary/>
        SelectMedia = LaunchMail | LButton,
        /// <summary/>
        LaunchApplication1 = LaunchMail | RButton,
        /// <summary/>
        LaunchApplication2 = LaunchApplication1 | LButton,
        /// <summary/>
        OemSemicolon = MediaStop | Back,
        /// <summary/>
        Oem1 = OemSemicolon,
        /// <summary/>
        Oemplus = Oem1 | LButton,
        /// <summary/>
        Oemcomma = LaunchMail | Back,
        /// <summary/>
        OemMinus = Oemcomma | LButton,
        /// <summary/>
        OemPeriod = Oemcomma | RButton,
        /// <summary/>
        OemQuestion = OemPeriod | LButton,
        /// <summary/>
        Oem2 = OemQuestion,
        /// <summary/>
        Oemtilde = 192,
        /// <summary/>
        Oem3 = Oemtilde,
        /// <summary/>
        OemOpenBrackets = Oem3 | Escape,
        /// <summary/>
        Oem4 = OemOpenBrackets,
        /// <summary/>
        OemPipe = Oem3 | IMEConvert,
        /// <summary/>
        Oem5 = OemPipe,
        /// <summary/>
        OemCloseBrackets = Oem5 | LButton,
        /// <summary/>
        Oem6 = OemCloseBrackets,
        /// <summary/>
        OemQuotes = Oem5 | RButton,
        /// <summary/>
        Oem7 = OemQuotes,
        /// <summary/>
        Oem8 = Oem7 | LButton,
        /// <summary/>
        OemBackslash = Oem3 | PageDown,
        /// <summary/>
        Oem102 = OemBackslash,
        /// <summary/>
        ProcessKey = Oem3 | Left,
        /// <summary/>
        Packet = ProcessKey | RButton,
        /// <summary/>
        Attn = Oem102 | CapsLock,
        /// <summary/>
        Crsel = Attn | LButton,
        /// <summary/>
        Exsel = Oem3 | D8,
        /// <summary/>
        EraseEof = Exsel | LButton,
        /// <summary/>
        Play = Exsel | RButton,
        /// <summary/>
        Zoom = Play | LButton,
        /// <summary/>
        NoName = Exsel | MButton,
        /// <summary/>
        Pa1 = NoName | LButton,
        /// <summary/>
        OemClear = NoName | RButton,
        /// <summary/>
        Shift = 65536,
        /// <summary/>
        Control = 131072,
        /// <summary/>
        Alt = 262144,
    }
}
