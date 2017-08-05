using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine.SDL
{
    public enum HintPriority
    {
        Default,
        Normal,
        Override
    }

    public enum PixelType : uint
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        ArrayU8,
        ArrayU16,
        ArrayU32,
        ArrayF16,
        ArrayF32
    }

    public enum PixelOrder : uint
    {
        BitmapNone = 0,
        Bitmap4321,
        Bitmap1234,

        PackedNone = 0,
        PackedXRGB,
        PackedRGBX,
        PackedARGB,
        PackedRGBA,
        PackedXBGR,
        PackedBGRX,
        PackedABGR,
        PackedBGRA,

        ArrayNone = 0,
        ArrayRGB,
        ArrayRGBA,
        ArrayARGB,
        ArrayBGR,
        ArrayBGRA,
        ArrayABGR
    }

    public enum PackedLayout : uint
    {
        None,
        Layout332,
        Layout4444,
        Layout1555,
        Layout5551,
        Layout565,
        Layout8888,
        Layout2101010,
        Layout1010102
    }

    public enum LogCategory
    {
        Application,
        Error,
        Assert,
        System,
        Audio,
        Video,
        Render,
        Input,
        Test,

        Reserved1,
        Reserved2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,

        Custom,
    }

    public enum LogPriority
    {
        Verbose = 1,
        Debug,
        Info,
        Warn,
        Error,
        Critical,

        NumLogPriorities
    }

    [Flags]
    public enum InitFlags : uint
    {
        Timer = 0x00000001,
        Audio = 0x00000010,
        Video = 0x00000020,
        Joystick = 0x00000200,
        Haptic = 0x00001000,
        GameController = 0x00002000,
        Events = 0x00004000,
        NoParachute = 0x00100000,
        Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events
    }

    [Flags]
    public enum ImageInitFlags
    {
        Jpg = 0x00000001,
        Png = 0x00000002,
        Tif = 0x00000004,
        Webp = 0x00000008
    }

    public enum WindowEventID
    {
        None,
        Shown,
        Hidden,
        Exposed,
        Moved,
        Resized,
        SizeChanged,
        Minimized,
        Maximized,
        Restored,
        Enter,
        Leave,
        FocusGained,
        FocusLost,
        Close,
        TakeFocus,
        HitTest
    }

    [Flags]
    public enum BlendMode
    {
        None = 0,
        Blend = 1 << 0,
        Add = 1 << 1,
        Mod = 1 << 2
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void HintCallback(IntPtr userData, IntPtr name, IntPtr oldValue, IntPtr newValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void LogOutputFunction(IntPtr userData, LogCategory category, LogPriority priority, IntPtr message);

    public static class SDL
    {
        [SuppressUnmanagedCodeSecurity]
        public class Native
        {
            //
            // SDL.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_Init(InitFlags flags);

            [DllImport(LibraryName)]
            public extern static int SDL_InitSubSystem(InitFlags flags);

            [DllImport(LibraryName)]
            public extern static void SDL_QuitSubSystem(InitFlags flags);

            [DllImport(LibraryName)]
            public extern static InitFlags SDL_WasInit(InitFlags flags);

            [DllImport(LibraryName)]
            public extern static void SDL_Quit();

            //
            // SDL_atomics.h
            //
            [DllImport(LibraryName)]
            public extern static bool SDL_AtomicTryLock(Spinlock spinlock);

            [DllImport(LibraryName)]
            public extern static void SDL_AtomicLock(Spinlock spinlock);

            [DllImport(LibraryName)]
            public extern static void SDL_AtomicUnlock(Spinlock spinlock);

            [DllImport(LibraryName)]
            public extern static bool SDL_AtomicCAS(Atomic atomic, int oldVal, int newVal);

            [DllImport(LibraryName)]
            public extern static int SDL_AtomicSet(Atomic atomic, int value);

            [DllImport(LibraryName)]
            public extern static int SDL_AtomicGet(Atomic atomic);

            [DllImport(LibraryName)]
            public extern static int SDL_AtomicAdd(Atomic atomic, int value);

            [DllImport(LibraryName)]
            public extern static unsafe bool SDL_AtomicCASPtr(void** atomic, void* oldVal, void* newVal);

            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_AtomicSetPtr(void** atomic, void* value);

            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_AtomicGetPtr(void** atomic);

            //
            // SDL_audio.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_GetNumAudioDrivers();

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetAudioDriver(int index);

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_AudioInit(byte* driverName);

            [DllImport(LibraryName)]
            public extern static void SDL_AudioQuit();

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetCurrentAudioDriver();

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_OpenAudio(AudioSpec* desired, AudioSpec* obtained);

            [DllImport(LibraryName)]
            public extern static int SDL_GetNumAudioDevices(int isCapture);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetAudioDeviceName(int index, int isCapture);

            [DllImport(LibraryName)]
            public extern static unsafe AudioDeviceID SDL_OpenAudioDevice(byte* device, int isCapture, AudioSpec* desired, AudioSpec* obtained, int allowedChanges);

            [DllImport(LibraryName)]
            public extern static void SDL_PauseAudio(int pauseOn);

            [DllImport(LibraryName)]
            public extern static void SDL_PauseAudioDevice(AudioDeviceID device, int pauseOn);

            [DllImport(LibraryName)]
            public extern static unsafe AudioSpec* SDL_LoadWAV_RW(RWops* src, int freeSrc, AudioSpec* spec, byte** audioBuffer, uint* audioLength);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_FreeWAV(byte* audioBuffer);

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_BuildAudioCVT(AudioCVT* cvt, AudioFormat srcFormat, byte srcChannels, int srcRate, AudioFormat dstFormat, byte dstChannels, int dstRate);

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_ConvertAudio(AudioCVT* cvt);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_MixAudio(byte* dst, byte* src, uint length, int volume);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_MixAudioFormat(byte* dst, byte* src, AudioFormat format, uint length, int volume);

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_QueueAudio(AudioDeviceID device, void* data, uint length);

            [DllImport(LibraryName)]
            public extern static unsafe uint SDL_DequeueAudio(AudioDeviceID device, void* data, uint length);

            [DllImport(LibraryName)]
            public extern static uint SDL_GetQueueAudioSize(AudioDeviceID device);

            [DllImport(LibraryName)]
            public extern static void SDL_ClearQueueAudio(AudioDeviceID device);

            [DllImport(LibraryName)]
            public extern static void SDL_LockAudio();

            [DllImport(LibraryName)]
            public extern static void SDL_LockAudioDevice(AudioDeviceID device);

            [DllImport(LibraryName)]
            public extern static void SDL_UnlockAudio();

            [DllImport(LibraryName)]
            public extern static void SDL_UnlockAudioDevice(AudioDeviceID device);

            [DllImport(LibraryName)]
            public extern static void SDL_CloseAudio();

            [DllImport(LibraryName)]
            public extern static void SDL_CloseAudioDevice(AudioDeviceID device);

            //
            // SDL_clipboard.h
            //
            [DllImport(LibraryName)]
            public extern static unsafe int SDL_SetClipboardText(byte* text);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetClipboardText();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasClipboardText();

            //
            // SDL_cpuinfo.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_GetCPUCount();

            [DllImport(LibraryName)]
            public extern static int SDL_GetCPUCacheLineSize();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasRDTSC();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasAltiVec();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasMMX();

            [DllImport(LibraryName)]
            public extern static bool SDL_Has3DNow();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasSSE();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasSSE2();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasSSE3();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasSSE41();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasSSE42();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasAVX();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasAVX2();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasNEON();

            [DllImport(LibraryName)]
            public extern static int SDL_GetSystemRAM();

            //
            // SDL_error.h
            //
            [DllImport(LibraryName)]
            public extern static unsafe int SDL_SetError(byte* format, params object[] objects);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetError();

            [DllImport(LibraryName)]
            public extern static void SDL_ClearError();

            //
            // SDL_events.h
            //
            [DllImport(LibraryName)]
            public extern static void SDL_PumpEvents();

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_PeepEvents(Event* events, int numEvents, EventAction action, EventType minType, EventType maxType);

            [DllImport(LibraryName)]
            public extern static bool SDL_HasEvent(EventType type);

            [DllImport(LibraryName)]
            public extern static bool SDL_HasEvents(EventType minType, EventType maxType);

            [DllImport(LibraryName)]
            public extern static void SDL_FlushEvent(EventType type);

            [DllImport(LibraryName)]
            public extern static void SDL_FlushEvents(EventType minType, EventType maxType);

            [DllImport(LibraryName)]
            public extern static int SDL_PollEvent(out Event sdlEvent);

            [DllImport(LibraryName)]
            public extern static int SDL_WaitEvent(out Event sdlEvent);

            [DllImport(LibraryName)]
            public extern static int SDL_WaitEventTimeout(out Event sdlEvent, int timeout);

            [DllImport(LibraryName)]
            public extern static int SDL_PushEvent(ref Event sdlEvent);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_SetEventFilter(EventFilter filter, void* userData);

            [DllImport(LibraryName)]
            public extern static unsafe bool SDL_GetEventFilter(out EventFilter filter, void* userData);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_AddEventWatch(EventFilter filter, void* userData);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_DelEventWatch(EventFilter filter, void* userData);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_FilterEvents(EventFilter filter, void* userData);

            [DllImport(LibraryName)]
            public extern static byte SDL_EventState(EventType type, EventState state);

            [DllImport(LibraryName)]
            public extern static uint SDL_RegisterEvents(int numEvents);

            //
            // SDL_filesystem.h
            //
            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetBasePath();

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetPrefPath(byte* org, byte* app);

            //
            // SDL_gamecontroller.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_GameControllerAddMappingsFromRW(RWops rwOps, int freeRW);

            [DllImport(LibraryName)]
            public static extern unsafe int SDL_GameControllerAddMapping(byte* mappingString);

            [DllImport(LibraryName)]
            public static extern int SDL_GameControllerNumMappings();
            
            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerMappingForGUID(Guid guid);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerMapping(GameController gameController);

            [DllImport(LibraryName)]
            public static extern bool SDL_IsGameController(int joystickIndex);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerNameForIndex(int joystickIndex);

            [DllImport(LibraryName)]
            public static extern GameController SDL_GameControllerOpen(int joystickIndex);

            [DllImport(LibraryName)]
            public static extern GameController SDL_GameControllerFromInstanceID(JoystickID joyid);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerName(GameController gamecontroller);
            
            [DllImport(LibraryName)]
            public static extern bool SDL_GameControllerGetAttached(GameController gamecontroller);

            [DllImport(LibraryName)]
            public static extern Joystick SDL_GameControllerGetJoystick(GameController gamecontroller);

            [DllImport(LibraryName)]
            public static extern int SDL_GameControllerEventState(EventState state);

            [DllImport(LibraryName)]
            public static extern void SDL_GameControllerUpdate();

            [DllImport(LibraryName)]
            public static extern unsafe GameControllerAxis SDL_GameControllerGetAxisFromString(byte* pchString);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerGetStringForAxis(GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern GameControllerButtonBind SDL_GameControllerGetBindForAxis(GameController gamecontroller, GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern short SDL_GameControllerGetAxis(GameController gamecontroller, GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern unsafe GameControllerButton SDL_GameControllerGetButtonFromString(byte* pchString);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_GameControllerGetStringForButton(GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern GameControllerButtonBind SDL_GameControllerGetBindForButton(GameController gamecontroller, GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern byte SDL_GameControllerGetButton(GameController gamecontroller, GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern void SDL_GameControllerClose(GameController gamecontroller);

            //
            // SDL_hints.h
            //
            [DllImport(LibraryName)]
            public extern static unsafe bool SDL_SetHintWithPriority(byte* name, byte* value, HintPriority priority);

            [DllImport(LibraryName)]
            public extern static unsafe bool SDL_SetHint(byte* name, byte* value);

            [DllImport(LibraryName)]
            private extern static unsafe IntPtr SDL_GetHint(byte* name);

            [DllImport(LibraryName)]
            public extern static unsafe bool SDL_GetHintBoolean(byte* name, bool defaultValue);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_AddHintCallback(byte* name, HintCallback callback, void* userData);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_DelHintCallback(byte* name, HintCallback callback, void* userData);

            [DllImport(LibraryName)]
            public extern static void SDL_ClearHints();

            //
            // SDL_joystick.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_NumJoysticks();

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_JoystickNameForIndex(int deviceIndex);

            [DllImport(LibraryName)]
            public static extern Joystick SDL_JoystickOpen(int deviceIndex);

            [DllImport(LibraryName)]
            public static extern Joystick SDL_JoystickFromInstanceID(JoystickID joystickID);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_JoystickName(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern Guid SDL_JoystickGetDeviceGUID(int deviceIndex);

            [DllImport(LibraryName)]
            public static extern Guid SDL_JoystickGetGUID(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern unsafe void SDL_JoystickGetGUIDString(Guid guid, byte* pszGUID, int cbGUID);

            [DllImport(LibraryName)]
            public static extern unsafe Guid SDL_JoystickGetGUIDFromString(byte* pchGUID);

            [DllImport(LibraryName)]
            public static extern bool SDL_JoystickGetAttached(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern JoystickID SDL_JoystickInstanceID(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern int SDL_JoystickNumAxes(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern int SDL_JoystickNumBalls(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern int SDL_JoystickNumHats(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern int SDL_JoystickNumButtons(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern void SDL_JoystickUpdate();

            [DllImport(LibraryName)]
            public static extern int SDL_JoystickEventState(EventState state);

            [DllImport(LibraryName)]
            public static extern short SDL_JoystickGetAxis(Joystick joystick, JoystickAxis axis);

            [DllImport(LibraryName)]
            public static extern JoystickHat SDL_JoystickGetHat(Joystick joystick, int hat);

            [DllImport(LibraryName)]
            public static extern unsafe int SDL_JoystickGetBall(Joystick joystick, int ball, int* dx, int* dy);

            [DllImport(LibraryName)]
            public static extern byte SDL_JoystickGetButton(Joystick joystick, int button);

            [DllImport(LibraryName)]
            public static extern void SDL_JoystickClose(Joystick joystick);

            [DllImport(LibraryName)]
            public static extern JoystickPowerLevel SDL_JoystickCurrentPowerLevel(Joystick joystick);

            //
            // SDL_keyboard.h
            //
            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetKeyboardFocus();

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetKeyboardState(out int numkeys);

            [DllImport(LibraryName)]
            public extern static KeyMod SDL_GetModState();

            [DllImport(LibraryName)]
            public extern static void SDL_SetModState(KeyMod modstate);

            [DllImport(LibraryName)]
            public extern static KeyCode SDL_GetKeyFromScancode(ScanCode scanCode);

            [DllImport(LibraryName)]
            public extern static ScanCode SDL_GetScancodeFromKey(KeyCode key);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetScancodeName(ScanCode scanCode);

            [DllImport(LibraryName)]
            public extern static ScanCode SDL_GetScancodeFromName(string name);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetKeyName(KeyCode key);

            [DllImport(LibraryName)]
            public extern static KeyCode SDL_GetKeyFromName(string name);

            [DllImport(LibraryName)]
            public extern static void SDL_StartTextInput();

            [DllImport(LibraryName)]
            public extern static bool SDL_IsTextInputActive();

            [DllImport(LibraryName)]
            public extern static void SDL_StopTextInput();

            [DllImport(LibraryName)]
            public extern static void SDL_SetTextInputRect(out Rect rectangle);

            [DllImport(LibraryName)]
            public extern static bool SDL_HasScreenKeyboardSupport();

            [DllImport(LibraryName)]
            public extern static bool SDL_IsScreenKeyboardShown(Window window);

            //
            // SDL_loadso.h
            //
            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_LoadObject(byte* file);

            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_LoadFunction(void* handle, byte* name);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_UnloadObject(void* handle);

            //
            // SDL_log.h
            //
            [DllImport(LibraryName)]
            public extern static void SDL_LogSetAllPriority(LogPriority priority);

            [DllImport(LibraryName)]
            public extern static void SDL_LogSetPriority(LogCategory category, LogPriority priority);

            [DllImport(LibraryName)]
            public extern static LogPriority SDL_LogGetPriority(LogCategory category);

            [DllImport(LibraryName)]
            public extern static void SDL_LogResetPriorities();

            [DllImport(LibraryName)]
            public extern static void SDL_Log(string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogVerbose(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogDebug(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogInfo(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogWarn(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogError(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogCritical(LogCategory category, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogMessage(LogCategory category, LogPriority priority, string fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static void SDL_LogGetOutputFunction(LogOutputFunction callback, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_LogSetOutputFunction(LogOutputFunction callback, IntPtr userData);

            //
            // SDL_mouse.h
            //
            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetMouseFocus();

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetMouseState(out int x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetMouseState(out int x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetMouseState(IntPtr x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetMouseState(IntPtr x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetGlobalMouseState(out int x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetGlobalMouseState(out int x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetGlobalMouseState(IntPtr x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetGlobalMouseState(IntPtr x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetRelativeMouseState(out int x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetRelativeMouseState(out int x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetRelativeMouseState(IntPtr x, out int y);

            [DllImport(LibraryName)]
            public extern static MouseButtonState SDL_GetRelativeMouseState(IntPtr x, IntPtr y);

            [DllImport(LibraryName)]
            public extern static void SDL_WarpMouseInWindow(Window window, int x, int y);

            [DllImport(LibraryName)]
            public extern static int SDL_WarpMouseGlobal(int x, int y);

            [DllImport(LibraryName)]
            public extern static int SDL_SetRelativeMouseMode(bool enabled);

            [DllImport(LibraryName)]
            public extern static int SDL_CaptureMouse(bool enabled);

            [DllImport(LibraryName)]
            public extern static bool SDL_GetRelativeMouseMode();

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateCursor(byte[] data, byte[] mask, int w, int h, int hot_x, int hot_y);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateSystemCursor(SystemCursor id);

            [DllImport(LibraryName)]
            public extern static void SDL_SetCursor(IntPtr cursor);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetCursor();

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetDefaultCursor();

            [DllImport(LibraryName)]
            public extern static void SDL_FreeCursor(IntPtr cursor);

            [DllImport(LibraryName)]
            public extern static int SDL_ShowCursor(int toggle);

            //
            // Rect.h
            //
            [DllImport(LibraryName)]
            public extern static bool PointInRect(ref Point point, ref Rect rectangle);

            [DllImport(LibraryName)]
            public static extern bool RectEmpty(ref Rect rectangle);

            [DllImport(LibraryName)]
            public static extern bool SDL_HasIntersection(ref Rect a, ref Rect b);

            [DllImport(LibraryName)]
            public static extern bool SDL_IntersectRect(ref Rect a, ref Rect b, out Rect result);

            [DllImport(LibraryName)]
            public static extern void SDL_UnionRect(ref Rect a, ref Rect b, out Rect result);

            [DllImport(LibraryName)]
            public static extern bool SDL_EnclosePoints(
                [In (), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
                Point[] points,
                int count,
                ref Rect clip,
                out Rect result);

            [DllImport(LibraryName)]
            public static extern bool SDL_EnclosePoints(
                [In (), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
                Point[] points,
                int count,
                IntPtr clip,
                out Rect result);

            [DllImport(LibraryName)]
            public static extern bool SDL_IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

            //
            // SDL_render.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_GetNumRenderDrivers();

            [DllImport(LibraryName)]
            public extern static int SDL_GetRenderDriverInfo(int index, out RendererInfo info);

            [DllImport(LibraryName)]
            public extern static int SDL_CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer);

            [DllImport(LibraryName)]
            public extern static Renderer SDL_CreateRenderer(Window window, int index, RendererFlags flags);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

            [DllImport(LibraryName)]
            public extern static Renderer SDL_GetRenderer(Window window);

            [DllImport(LibraryName)]
            public extern static int SDL_GetRendererInfo(IntPtr renderer,
                                                            out RendererInfo info);

            [DllImport(LibraryName)]
            public extern static int SDL_GetRendererOutputSize(IntPtr renderer,
                                                                 out int w, out int h);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateTexture(IntPtr renderer,
                                                                    uint format,
                                                                    int access, int w,
                                                                    int h);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

            [DllImport(LibraryName)]
            public extern static int SDL_QueryTexture(IntPtr texture,
                                                        out uint format, out int access,
                                                        out int w, out int h);

            [DllImport(LibraryName)]
            public extern static int SDL_SetTextureColorMod(IntPtr texture,
                                                               byte r, byte g, byte b);

            [DllImport(LibraryName)]
            public extern static int SDL_GetTextureColorMod(IntPtr texture,
                                                              out byte r, out byte g,
                                                              out byte b);

            [DllImport(LibraryName)]
            public extern static int SDL_SetTextureAlphaMod(IntPtr texture,
                                                               byte alpha);

            [DllImport(LibraryName)]
            public extern static int SDL_GetTextureAlphaMod(IntPtr texture,
                                                              out byte alpha);

            [DllImport(LibraryName)]
            public extern static int SDL_SetTextureBlendMode(IntPtr texture,
                                                                BlendMode blendMode);

            [DllImport(LibraryName)]
            public extern static int SDL_GetTextureBlendMode(IntPtr texture,
                                                               out BlendMode blendMode);

            [DllImport(LibraryName)]
            public static extern int SDL_UpdateTexture(
                IntPtr texture,
                ref Rect rect,
                IntPtr pixels,
                int pitch
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpdateTexture(
                IntPtr texture,
                IntPtr rect,
                IntPtr pixels,
                int pitch
            );

            [DllImport(LibraryName)]
            public static extern int SDL_LockTexture(
                IntPtr texture,
                ref Rect rect,
                out IntPtr pixels,
                out int pitch
            );

            [DllImport(LibraryName)]
            public static extern int SDL_LockTexture(
                IntPtr texture,
                IntPtr rect,
                out IntPtr pixels,
                out int pitch
            );

            [DllImport(LibraryName)]
            public extern static void SDL_UnlockTexture(IntPtr texture);

            [DllImport(LibraryName)]
            public extern static bool SDL_RenderTargetSupported(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_SetRenderTarget(IntPtr renderer,
                                                            IntPtr texture);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetRenderTarget(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderSetLogicalSize(IntPtr renderer, int w, int h);

            [DllImport(LibraryName)]
            public extern static void SDL_RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderSetIntegerScale(IntPtr renderer,
                                                                  bool enable);

            [DllImport(LibraryName)]
            public extern static bool SDL_RenderGetIntegerScale(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderSetViewport(IntPtr renderer,
                                                      ref Rect rect);

            [DllImport(LibraryName)]
            public extern static void SDL_RenderGetViewport(IntPtr renderer,
                                                              out Rect rect);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderSetClipRect(IntPtr renderer,
                                                      ref Rect rect);

            [DllImport(LibraryName)]
            public extern static void SDL_RenderGetClipRect(IntPtr renderer,
                                                             out Rect rect);

            [DllImport(LibraryName)]
            public extern static bool SDL_RenderIsClipEnabled(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderSetScale(IntPtr renderer,
                                                           float scaleX, float scaleY);

            [DllImport(LibraryName)]
            public extern static void SDL_RenderGetScale(IntPtr renderer,
                                                          out float scaleX, out float scaleY);

            [DllImport(LibraryName)]
            public extern static int SDL_SetRenderDrawColor(IntPtr renderer,
                                                       byte r, byte g, byte b,
                                                       byte a);

            [DllImport(LibraryName)]
            public extern static int SDL_GetRenderDrawColor(IntPtr renderer,
                                                      out byte r, out byte g, out byte b,
                                                      out byte a);

            [DllImport(LibraryName)]
            public extern static int SDL_SetRenderDrawBlendMode(IntPtr renderer,
                                                                   BlendMode blendMode);

            [DllImport(LibraryName)]
            public extern static int SDL_GetRenderDrawBlendMode(IntPtr renderer,
                                                                  out BlendMode blendMode);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderClear(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_RenderDrawPoint(IntPtr renderer,
                                                            int x, int y);

            [DllImport(LibraryName)]
            public static extern int SDL_RenderDrawPoints(
                IntPtr renderer,
                [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            Point[] points,
                int count
            );

            [DllImport(LibraryName)]
            public extern static int SDL_RenderDrawLine(IntPtr renderer,
                                                           int x1, int y1, int x2, int y2);

            [DllImport(LibraryName)]
            public static extern int SDL_RenderDrawLines(
                IntPtr renderer,
                [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            Point[] points,
                int count
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderDrawRect(
                IntPtr renderer,
                ref Rect rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderDrawRect(
                IntPtr renderer,
                IntPtr rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderDrawRects(
                IntPtr renderer,
                [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            Rect[] rects,
                int count
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderFillRect(
                IntPtr renderer,
                ref Rect rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderFillRect(
                IntPtr renderer,
                IntPtr rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderFillRects(
                IntPtr renderer,
                [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
                Rect[] rects,
                int count
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopy(
                IntPtr renderer,
                IntPtr texture,
                ref Rect srcrect,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopy(
                IntPtr renderer,
                IntPtr texture,
                IntPtr srcrect,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopy(
                IntPtr renderer,
                IntPtr texture,
                ref Rect srcrect,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopy(
                IntPtr renderer,
                IntPtr texture,
                IntPtr srcrect,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopyEx(
                IntPtr renderer,
                IntPtr texture,
                ref Rect srcrect,
                ref Rect dstrect,
                double angle,
                ref Point center,
                RendererFlip flip
            );

            [DllImport(LibraryName)]
            public static extern int SDL_RenderCopyEx(
                IntPtr renderer,
                IntPtr texture,
                IntPtr srcrect,
                ref Rect dstrect,
                double angle,
                ref Point center,
                RendererFlip flip
            );

            [DllImport(LibraryName)]
            public extern static int SDL_RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

            [DllImport(LibraryName)]
            public extern static void SDL_RenderPresent(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static void SDL_DestroyTexture(IntPtr texture);

            [DllImport(LibraryName)]
            public extern static void SDL_DestroyRenderer(IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static int SDL_GL_BindTexture(IntPtr texture, out float texw, out float texh);

            [DllImport(LibraryName)]
            public extern static int SDL_GL_UnbindTexture(IntPtr texture);

            //
            // SDL_rwops.h
            //
            [DllImport(LibraryName)]
            public extern static IntPtr SDL_RWFromFile(string file, string mode);

            [DllImport(LibraryName)]
            public extern static int SDL_RWclose(IntPtr context);

            [DllImport(LibraryName)]
            public extern static int SDL_RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

            [DllImport(LibraryName)]
            public extern static long SDL_RWsize(IntPtr context);

            //
            // SDL_shape.h
            //
            [DllImport(LibraryName)]
            public static extern unsafe Window SDL_CreateShapedWindow(byte title, uint x, uint y, uint w, uint h, WindowFlags flags);

            [DllImport(LibraryName)]
            public static extern bool IsShapedWindow(Window window);

            [DllImport(LibraryName)]
            public static extern unsafe int SDL_SetWindowShape(Window window, Surface* shape, WindowShape* shapeMode);

            [DllImport(LibraryName)]
            public static extern unsafe int SDL_GetShapedWindowMode(Window window, out WindowShape shapeMode);

            //
            // SDL_stdinc.h
            //
            [DllImport(LibraryName)]
            public static extern unsafe void* SDL_malloc(Size size);

            [DllImport(LibraryName)]
            public static extern unsafe void* SDL_calloc(Size nmemb, Size size);

            [DllImport(LibraryName)]
            public static extern unsafe void* SDL_realloc(void* mem, Size size);

            [DllImport(LibraryName)]
            public static extern unsafe void SDL_free(void* mem);

            [DllImport(LibraryName)]
            public static extern unsafe byte* SDL_getenv(byte* name);

            [DllImport(LibraryName)]
            public static extern unsafe int SDL_setenv(byte* name, byte* value, int overwrite);

            [DllImport(LibraryName)]
            public static extern unsafe void SDL_qsort(void* buffer, Size nmemb, Size size, IntPtr compare);

            //
            // SDL_surface.h
            //
            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlit(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlit(
                IntPtr src,
                IntPtr srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlit(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlit(
                IntPtr src,
                IntPtr srcrect,
                IntPtr dst,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlitScaled(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlitScaled(
                IntPtr src,
                IntPtr srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlitScaled(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_UpperBlitScaled(
                IntPtr src,
                IntPtr srcrect,
                IntPtr dst,
                IntPtr dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_ConvertPixels(
                int width,
                int height,
                uint src_format,
                IntPtr src,
                int src_pitch,
                uint dst_format,
                IntPtr dst,
                int dst_pitch
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_ConvertSurface(
                IntPtr src,
                IntPtr fmt,
                uint flags
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_ConvertSurfaceFormat(
                IntPtr src,
                uint pixel_format,
                uint flags
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_CreateRGBSurface(
                uint flags,
                int width,
                int height,
                int depth,
                uint Rmask,
                uint Gmask,
                uint Bmask,
                uint Amask
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_CreateRGBSurfaceFrom(
                IntPtr pixels,
                int width,
                int height,
                int depth,
                int pitch,
                uint Rmask,
                uint Gmask,
                uint Bmask,
                uint Amask
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_CreateRGBSurfaceWithFormat(
                uint flags,
                int width,
                int height,
                int depth,
                uint format
            );

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_CreateRGBSurfaceWithFormatFrom(
                IntPtr pixels,
                int width,
                int height,
                int depth,
                int pitch,
                uint format
            );

            [DllImport(LibraryName)]
            public static extern int SDL_FillRect(
                IntPtr dst,
                ref Rect rect,
                uint color
            );

            [DllImport(LibraryName)]
            public static extern int SDL_FillRect(
                IntPtr dst,
                IntPtr rect,
                uint color
            );

            [DllImport(LibraryName)]
            public static extern int SDL_FillRects(
                IntPtr dst,
                [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
                Rect[] rects,
                int count,
                uint color
            );

            [DllImport(LibraryName)]
            public static extern void SDL_FreeSurface(IntPtr surface);

            [DllImport(LibraryName)]
            public static extern void SDL_GetClipRect(
                IntPtr surface,
                out Rect rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_GetColorKey(
                IntPtr surface,
                out uint key
            );

            [DllImport(LibraryName)]
            public static extern int SDL_GetSurfaceAlphaMod(
                IntPtr surface,
                out byte alpha
            );

            [DllImport(LibraryName)]
            public static extern int SDL_GetSurfaceBlendMode(
                IntPtr surface,
                out BlendMode blendMode
            );

            [DllImport(LibraryName)]
            public static extern int SDL_GetSurfaceColorMod(
                IntPtr surface,
                out byte r,
                out byte g,
                out byte b
            );

            [DllImport(LibraryName)]
            private static extern IntPtr SDL_LoadBMP_RW(
                IntPtr src,
                int freesrc
            );

            [DllImport(LibraryName)]
            public static extern int SDL_LockSurface(IntPtr surface);

            [DllImport(LibraryName)]
            public static extern int SDL_LowerBlit(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_LowerBlitScaled(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            private static extern int SDL_SaveBMP_RW(
                IntPtr surface,
                IntPtr src,
                int freesrc
            );

            [DllImport(LibraryName)]
            public static extern bool SDL_SetClipRect(
                IntPtr surface,
                ref Rect rect
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetColorKey(
                IntPtr surface,
                int flag,
                uint key
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetSurfaceAlphaMod(
                IntPtr surface,
                byte alpha
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetSurfaceBlendMode(
                IntPtr surface,
                BlendMode blendMode
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetSurfaceColorMod(
                IntPtr surface,
                byte r,
                byte g,
                byte b
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetSurfacePalette(
                IntPtr surface,
                IntPtr palette
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SetSurfaceRLE(
                IntPtr surface,
                int flag
            );

            [DllImport(LibraryName)]
            public static extern int SDL_SoftStretch(
                IntPtr src,
                ref Rect srcrect,
                IntPtr dst,
                ref Rect dstrect
            );

            [DllImport(LibraryName)]
            public static extern void SDL_UnlockSurface(IntPtr surface);

            //
            // SDL_syswm.h
            //
            [DllImport(LibraryName)]
            public extern static bool SDL_GetWindowWMInfo(Window window, ref SysWMInfo info);

            //
            // SDL_timer.h
            //
            [DllImport(LibraryName)]
            public extern static uint SDL_GetTicks();

            public static bool SDL_TicksPassed(uint a, uint b)
            {
                return ((int)(b - a) <= 0);
            }

            [DllImport(LibraryName)]
            public extern static ulong SDL_GetPerformanceCounter();

            [DllImport(LibraryName)]
            public extern static ulong SDL_GetPerformanceFrequency();

            [DllImport(LibraryName)]
            public extern static void SDL_Delay(uint ms);

            [DllImport(LibraryName)]
            public extern static TimerID SDL_AddTimer(uint interval, TimerCallback callback, IntPtr param);

            [DllImport(LibraryName)]
            public extern static bool SDL_RemoveTimer(TimerID id);

            //
            // SDL_version.h
            //
            [DllImport(LibraryName)]
            public extern static void SDL_GetVersion(out Version version);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetRevision();

            [DllImport(LibraryName)]
            public extern static int SDL_GetRevisionNumber();

            //
            // SDL_video.h
            //
            [DllImport(LibraryName)]
            public extern static int SDL_GetNumVideoDrivers();

            [DllImport(LibraryName)]
            private extern static IntPtr SDL_GetVideoDriver(int index);

            [DllImport(LibraryName)]
            public static extern int SDL_VideoInit(IntPtr driver_name);

            [DllImport(LibraryName)]
            public static extern void SDL_VideoQuit();

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GetCurrentVideoDriver();

            [DllImport(LibraryName)]
            public static extern int SDL_GetNumVideoDisplays();

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GetDisplayName(int displayIndex);

            [DllImport(LibraryName)]
            public static extern int SDL_GetDisplayBounds(int displayIndex, out Rect rectangle);

            [DllImport(LibraryName)]
            public static extern int SDL_GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

            [DllImport(LibraryName)]
            public static extern int SDL_GetDisplayUsableBounds(int displayIndex, out Rect rectangle);

            [DllImport(LibraryName)]
            public static extern int SDL_GetNumDisplayModes(int displayIndex);

            [DllImport(LibraryName)]
            public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

            [DllImport(LibraryName)]
            public static extern int SDL_GetDeskTopDisplayMode(int displayIndex, out DisplayMode mode);

            [DllImport(LibraryName)]
            public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

            [DllImport(LibraryName)]
            [return: MarshalAs(UnmanagedType.LPStruct)]
            public static extern DisplayMode SDL_GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

            [DllImport(LibraryName)]
            public static extern int SDL_GetWindowDisplayIndex(Window window);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowDisplayMode(Window window, ref DisplayMode mode);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowDisplayMode(Window window, IntPtr mode);

            [DllImport(LibraryName)]
            public static extern int SDL_GetWindowDisplayMode(Window window, out DisplayMode mode);

            [DllImport(LibraryName)]
            public static extern uint SDL_GetWindowPixelFormat(Window window);

            [DllImport(LibraryName)]
            public static unsafe extern Window SDL_CreateWindow(byte* title, int x, int y, int width, int height, WindowFlags flags);

            [DllImport(LibraryName)]
            public static unsafe extern Window SDL_CreateWindowFrom(void* data);

            [DllImport(LibraryName)]
            public static extern WindowID SDL_GetWindowID(Window window);

            [DllImport(LibraryName)]
            public static extern Window SDL_GetWindowFromID(WindowID id);

            [DllImport(LibraryName)]
            public static extern WindowFlags SDL_GetWindowFlags(Window window);

            [DllImport(LibraryName)]
            public static unsafe extern void SDL_SetWindowTitle(Window window, byte* title);

            [DllImport(LibraryName)]
            public static unsafe extern byte* SDL_GetWindowTitle(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowIcon(Window window, Surface icon);

            [DllImport(LibraryName)]
            public static unsafe extern void* SDL_SetWindowData(Window window, byte* name, void* userData);

            [DllImport(LibraryName)]
            public static unsafe extern void* SDL_GetWindowData(Window window, byte* name);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowPosition(Window window, int x, int y);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowPosition(Window window, out int x, out int y);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowPosition(Window window, out int x, IntPtr y);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowPosition(Window window, IntPtr x, out int y);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowSize(Window window, int width, int height);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowSize(Window window, out int width, out int height);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowSize(Window window, out int width, IntPtr height);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowSize(Window window, IntPtr width, out int height);

            [DllImport(LibraryName)]
            public static extern int SDL_GetWindowBordersSize(Window window, out int top, out int left, out int bottom, out int right);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowMinimumSize(Window window, int minwidth, int minHeight);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowMinimumSize(Window window, out int width, out int height);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowMaximumSize(Window window, int maxWidth, int maxHeight);

            [DllImport(LibraryName)]
            public static extern void SDL_GetWindowMaximumSize(Window window, out int width, out int height);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowBordered(Window window, bool bordered);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowResizable(Window window, bool resizable);

            [DllImport(LibraryName)]
            public static extern void SDL_ShowWindow(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_HideWindow(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_RaiseWindow(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_MaximizeWindow(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_MinimizeWindow(Window window);

            [DllImport(LibraryName)]
            public static extern void SDL_RestoreWindow(Window window);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowFullscreen(Window window, WindowFlags flags);

            [DllImport(LibraryName)]
            public static unsafe extern Surface* SDL_GetWindowSurface(Window window);

            [DllImport(LibraryName)]
            public static extern int SDL_UpdateWindowSurface(Window window);

            [DllImport(LibraryName)]
            public static unsafe extern int SDL_UpdateWindowSurfaceRects(
                Window window,
                Rect* rectangles,
                int numRectangles);

            [DllImport(LibraryName)]
            public static extern void SDL_SetWindowGrab(Window window, bool grabbed);

            [DllImport(LibraryName)]
            public static extern bool SDL_GetWindowGrab(Window window);

            [DllImport(LibraryName)]
            public static extern Window SDL_GetGrabbedWindow();

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowBrightness(Window window, float brightness);

            [DllImport(LibraryName)]
            public static extern float SDL_GetWindowBrightness(Window window);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowOpacity(Window window, float opacity);

            [DllImport(LibraryName)]
            public static extern int SDL_GetWindowOpacity(Window window, out float outOpacity);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowModalFor(Window modalWindow, Window parentWindow);

            [DllImport(LibraryName)]
            public static extern int SDL_SetWindowInputFocus(Window window);

            [DllImport(LibraryName)]
            public static unsafe extern int SDL_SetWindowGammaRamp(
                Window window,
                ushort* red,
                ushort* green,
                ushort* blue);

            [DllImport(LibraryName)]
            public static unsafe extern int SDL_GetWindowGammaRamp(
                Window window,
                ushort* red,
                ushort* green,
                ushort* blue);

            [DllImport(LibraryName)]
            public static unsafe extern int SDL_SetWindowHitTest(Window window, HitTest callback, void* callbackData);

            [DllImport(LibraryName)]
            public static extern void SDL_DestroyWindow(Window window);

            [DllImport(LibraryName)]
            public static extern bool SDL_IsScreenSaverEnabled();

            [DllImport(LibraryName)]
            public static extern void SDL_EnableScreenSaver();

            [DllImport(LibraryName)]
            public static extern void SDL_DisableScreenSaver();
        }

        [SuppressUnmanagedCodeSecurity]
        public class ImageNative
        {
            [DllImport(ImageLibraryName)]
            public static extern int IMG_Init(ImageInitFlags flags);

            [DllImport(ImageLibraryName)]
            public static extern void IMG_Quit();

            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTyped_RW(IntPtr src, int freesrc, string type);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_Load(string file);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_Load_RW(IntPtr src, int freesrc);

            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTexture(IntPtr renderer, string file);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTexture_RW(IntPtr renderer, IntPtr src, int freesrc);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTextureTyped_RW(IntPtr renderer, IntPtr src, int freesrc, string type);

            [DllImport(ImageLibraryName)]
            public static extern int IMG_isICO(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isCUR(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isBMP(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isGIF(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isJPG(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isLBM(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isPCX(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isPNG(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isPNM(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isTIF(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isXCF(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isXPM(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isXV(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_isWEBP(IntPtr src);

            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadICO_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadCUR_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadBMP_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadGIF_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadJPG_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadLBM_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadPCX_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadPNG_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadPNM_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTGA_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadTIF_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadXCF_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadXPM_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadXV_RW(IntPtr src);
            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_LoadWEBP_RW(IntPtr src);

            [DllImport(ImageLibraryName)]
            public static extern IntPtr IMG_ReadXPMFromArray(string[] xpm);

            [DllImport(ImageLibraryName)]
            public static extern int IMG_SavePNG(IntPtr surface, string file);
            [DllImport(ImageLibraryName)]
            public static extern int IMG_SavePNG_RW(IntPtr surface, IntPtr dst, int freedst);
        }

        public static class Hints
        {
            public const string FrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
            public const string RenderDriver = "SDL_RENDER_DRIVER";
            public const string RenderOpenglShaders = "SDL_RENDER_OPENGL_SHADERS";
            public const string RenderDirect3DThreadsafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
            public const string RenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
            public const string RenderScaleQuality = "SDL_RENDER_SCALE_QUALITY";
            public const string RenderVsync = "SDL_RENDER_VSYNC";
            public const string VideoAllowScreensave = "SDL_VIDEO_ALLOW_SCREENSAVER";
            public const string VideoX11XvidMode = "SDL_VIDEO_X11_XVIDMODE";
            public const string VideoX11Xinerama = "SDL_VIDEO_X11_XINERAMA";
            public const string VideoX11XRandR = "SDL_VIDEO_X11_XRANDR";
            public const string VideoX11NetWMPing = "SDL_VIDEO_X11_NET_WM_PING";
            public const string WindowFrameUsableWhileCursorHidden = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
            public const string WindowsEnableMessageLoop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
            public const string GrabKeyboard = "SDL_GRAB_KEYBOARD";
            public const string MouseRelativeModeWarp = "SDL_MOUSE_RELATIVE_MODE_WARP";
            public const string MouseFocusClickThrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
            public const string VideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
            public const string IdleTimerDisabled = "SDL_IOS_IDLE_TIMER_DISABLED";
            public const string Orientations = "SDL_IOS_ORIENTATIONS";
            public const string AppleTVControllerUIEvents = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";
            public const string AppleTVRemoteAllowRotation = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
            public const string AccelerometerAsJoystick = "SDL_ACCELEROMETER_AS_JOYSTICK";
            public const string XInputEnabled = "SDL_XINPUT_ENABLED";
            public const string XInputUseOldJoystickMapping = "SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING";
            public const string GameControllerConfig = "SDL_GAMECONTROLLERCONFIG";
            public const string JoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
            public const string AllowTopmost = "SDL_ALLOW_TOPMOST";
            public const string TimerResolution = "SDL_TIMER_RESOLUTION";
            public const string ThreadStackSize = "SDL_THREAD_STACK_SIZE";
            public const string VideoHighDPIDisabled = "SDL_VIDEO_HIGHDPI_DISABLED";
            public const string MacCtrlClickEmulateRightClick = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
            public const string VideoWinD3DCompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
            public const string VideoWindowSharePixelFormat = "SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT";
            public const string WinRTPrivacyPolicyURL = "SDL_WINRT_PRIVACY_POLICY_URL";
            public const string WinRTPrivacyPolicyLabel = "SDL_WINRT_PRIVACY_POLICY_LABEL";
            public const string WinRTHandleBackButton = "SDL_WINRT_HANDLE_BACK_BUTTON";
            public const string VideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
            public const string MacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
            public const string AndroidAPKExpansionMainFileVersion = "SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";
            public const string AndroidAPKExpansionPatchFileVersion = "SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";
            public const string IMEInternalEditing = "SDL_IME_INTERNAL_EDITING";
            public const string AndroidSeparateMouseAndTouch = "SDL_ANDROID_SEPARATE_MOUSE_AND_TOUCH";
            public const string EmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
            public const string NoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
            public const string WindowsNoCloseOnAltF4 = "SDL_WINDOWS_NO_CLOSE_ON_ALT_F4";
            public const string BMPSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";
            public const string WindowsDisableThreadWarning = "SDL_WINDOWS_DISABLE_THREAD_NAMING";
            public const string RPIVideoLayer = "SDL_RPI_VIDEO_LAYER";
        }

        public static class PixelFormats
        {
            public static readonly uint Unknown = 0;
            public static readonly uint Index1LSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap4321, 0, 1, 0);
            public static readonly uint Index1MSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap1234, 0, 1, 0);
            public static readonly uint Index4LSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap4321, 0, 4, 0);
            public static readonly uint Index4MSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap1234, 0, 4, 0);
            public static readonly uint Index8 = DefinePixelFormat(PixelType.Index8, 0, 0, 8, 1);
            public static readonly uint RGB332 = DefinePixelFormat(PixelType.Packed8, PixelOrder.PackedXRGB, PackedLayout.Layout332, 8, 1);
            public static readonly uint RGB444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout4444, 12, 2);
            public static readonly uint RGB555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout1555, 15, 2);
            public static readonly uint BGR555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout1555, 15, 2);
            public static readonly uint ARGB4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout4444, 16, 2);
            public static readonly uint RGBA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout4444, 16, 2);
            public static readonly uint ABGR4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout4444, 16, 2);
            public static readonly uint BGRA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout4444, 16, 2);
            public static readonly uint ARGB1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout1555, 16, 2);
            public static readonly uint RGBA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout5551, 16, 2);
            public static readonly uint ABGR1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout1555, 16, 2);
            public static readonly uint BGRA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout5551, 16, 2);
            public static readonly uint RGB565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout565, 16, 2);
            public static readonly uint BGR565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout565, 16, 2);
            public static readonly uint RGB24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayRGB, 0, 24, 3);
            public static readonly uint BGR24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayBGR, 0, 24, 3);
            public static readonly uint RGB888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXRGB, PackedLayout.Layout8888, 24, 4);
            public static readonly uint RGBX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedRGBX, PackedLayout.Layout8888, 24, 4);
            public static readonly uint BGR888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXBGR, PackedLayout.Layout8888, 24, 4);
            public static readonly uint BGRX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRX, PackedLayout.Layout8888, 24, 4);
            public static readonly uint ARGB8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
            public static readonly uint RGBA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
            public static readonly uint ABGR8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedABGR, PackedLayout.Layout8888, 32, 4);
            public static readonly uint BGRA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRA, PackedLayout.Layout8888, 32, 4);
            public static readonly uint ARGB2101010 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, 0, 32, 4);
            public static readonly uint YV12 = FourCharacterCode((byte)'Y', (byte)'V', (byte)'1', (byte)'2');
            public static readonly uint IYUV = FourCharacterCode((byte)'I', (byte)'Y', (byte)'U', (byte)'V');
            public static readonly uint YUY2 = FourCharacterCode((byte)'Y', (byte)'U', (byte)'Y', (byte)'2');
            public static readonly uint UYVY = FourCharacterCode((byte)'U', (byte)'Y', (byte)'V', (byte)'Y');
            public static readonly uint YVYU = FourCharacterCode((byte)'Y', (byte)'V', (byte)'Y', (byte)'U');
            public static readonly uint NV12 = FourCharacterCode((byte)'N', (byte)'V', (byte)'1', (byte)'2');
            public static readonly uint NV21 = FourCharacterCode((byte)'N', (byte)'V', (byte)'2', (byte)'1');

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint FourCharacterCode(byte a, byte b, byte c, byte d) => (uint)(a | (b << 8) | (c << 16) | (d << 24));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static uint DefinePixelFormat(PixelType type, PixelOrder order, PackedLayout layout, byte bits, byte bytes) => (uint)((1 << 28) | ((byte)type << 24) | ((byte)order << 20) | ((byte)layout << 16) | (bits << 8) | bytes);
        }

        public const string LibraryName = "SDL2.dll";
        public const string ImageLibraryName = "SDL2_image.dll";
        public const int ScanCodeMask = (1 << 30);

        //
        // SDL.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Init(InitFlags flags) => Native.SDL_Init(flags).CheckError("Could not initialize SDL with " + flags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitSubSystem(InitFlags flags) => Native.SDL_InitSubSystem(flags).CheckError("Could not initialize SDL subsystem " + flags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void QuitSubSystem(InitFlags flags) => Native.SDL_QuitSubSystem(flags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InitFlags WasInit(InitFlags flags) => Native.SDL_WasInit(flags);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Quit() => Native.SDL_Quit();
        
        //
        // SDL_error.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe string GetError() => GetString(Native.SDL_GetError());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearError() => Native.SDL_ClearError();
        
        //
        // SDL_version.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetVersion(out Version version) => Native.SDL_GetVersion(out version);

        //
        // SDL_video.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetDesktopDisplayMode(int displayIndex, out DisplayMode displayMode) => Native.SDL_GetDeskTopDisplayMode(displayIndex, out displayMode).CheckError("Could not get dekstop display mode");

        //
        // Utility methods
        //
        public static unsafe string GetString(IntPtr handle)
        {
            Assert.IsTrue(handle != IntPtr.Zero, "[SDL] String is null: " + GetError());

            byte* ptr = (byte*)handle;
            return GetString(ptr);
        }

        public static unsafe string GetString(byte* ptr)
        {
            Assert.IsTrue(ptr != null, "[SDL] String is null: " + GetError());

            byte* counter = ptr;
            while (*counter != 0)
            {
                counter++;
            }
            int count = (int)(counter - ptr);

            return Encoding.UTF8.GetString(ptr, count);
        }
    }
}