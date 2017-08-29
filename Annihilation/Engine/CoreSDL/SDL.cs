using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Engine.System;

namespace SDL2
{
    public static unsafe partial class SDL
    {   
        private static readonly NativeLibrary _library = LoadLibrary();

        public const string LibraryName = "SDL2.dll";
        public const int ScanCodeMask = (1 << 30);
        public const int AudioCVTMaxFilters = 9;

        private static NativeLibrary LoadLibrary()
        {
            string name;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                name = "SDL2.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                name = "libSDL2-2.0.so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                name = "libsdl2.dylib";
            }
            else
            {
                throw new InvalidOperationException("Unknown SDL platform.");
            }

            NativeLibrary lib = new NativeLibrary(name);
            return lib;
        }

        private static T LoadFunction<T>(string name)
        {
            return _library.LoadFunction<T>(name);
        }

        //
        // SDL.h
        //
        private delegate int InitDelegate(InitFlags flags);
        private static InitDelegate _init = LoadFunction<InitDelegate>("SDL_Init");
        public static int Init(InitFlags flags) => _init(flags);

        private delegate int InitSubSystemDelegate(InitFlags flags);
        private static InitSubSystemDelegate _initSubSystem = LoadFunction<InitSubSystemDelegate>("SDL_InitSubSystem");
        public static int InitSubSystem(InitFlags flags) => _initSubSystem(flags);

        private delegate void QuitSubSystemDelegate(InitFlags flags);
        private static QuitSubSystemDelegate _quitSubSystem = LoadFunction<QuitSubSystemDelegate>("SDL_QuitSubSystem");
        public static void QuitSubSystem(InitFlags flags) => _quitSubSystem(flags);

        private delegate InitFlags WasInitDelegate(InitFlags flags);
        private static WasInitDelegate _wasInit = LoadFunction<WasInitDelegate>("SDL_WasInit");
        public static InitFlags WasInit(InitFlags flags) => _wasInit(flags);

        private delegate void QuitDelegate();
        private static QuitDelegate _quit = LoadFunction<QuitDelegate>("SDL_Quit");
        public static void Quit() => _quit();

        //
        // SDL_clipboard.h
        //
        private delegate int SetClipboardTextDelegate(string text);
        private static SetClipboardTextDelegate _setClipboardText = LoadFunction<SetClipboardTextDelegate>("SDL_SetClipboardText");
        public static int SetClipboardText(string text) => _setClipboardText(text);

        private delegate string GetClipboardTextDelegate();
        private static GetClipboardTextDelegate _getClipboardText = LoadFunction<GetClipboardTextDelegate>("SDL_GetClipboardText");
        public static string GetClipboardText() => _getClipboardText();

        private delegate bool HasClipboardTextDelegate();
        private static HasClipboardTextDelegate _hasClipboardText = LoadFunction<HasClipboardTextDelegate>("SDL_HasClipboardText");
        public static bool HasClipboardText() => _hasClipboardText();

        //
        // SDL_cpuinfo.h
        //
        private delegate int GetCpuCountDelegate();
        private static GetCpuCountDelegate _getCpuCount = LoadFunction<GetCpuCountDelegate>("SDL_GetCPUCount");
        public static int GetCpuCount() => _getCpuCount();

        private delegate int GetCpuCacheLineSizeDelegate();
        private static GetCpuCacheLineSizeDelegate _getCpuCacheLineSize = LoadFunction<GetCpuCacheLineSizeDelegate>("SDL_GetCPUCacheLineSize");
        public static int GetCpuCacheLineSize() => _getCpuCacheLineSize();

        private delegate bool HasRdtscDelegate();
        private static HasRdtscDelegate _hasRdtsc = LoadFunction<HasRdtscDelegate>("SDL_HasRDTSC");
        public static bool HasRdtsc() => _hasRdtsc();

        private delegate bool HasAltiVecDelegate();
        private static HasAltiVecDelegate _hasAltiVec = LoadFunction<HasAltiVecDelegate>("SDL_HasAltiVec");
        public static bool HasAltiVec() => _hasAltiVec();

        private delegate bool HasMmxDelegate();
        private static HasMmxDelegate _hasMmx = LoadFunction<HasMmxDelegate>("SDL_HasMMX");
        public static bool HasMmx() => _hasMmx();

        private delegate bool Has3DNowDelegate();
        private static Has3DNowDelegate _has3DNow = LoadFunction<Has3DNowDelegate>("SDL_Has3DNow");
        public static bool Has3DNow() => _has3DNow();

        private delegate bool HasSseDelegate();
        private static HasSseDelegate _hasSse = LoadFunction<HasSseDelegate>("SDL_HasSSE");
        public static bool HasSse() => _hasSse();

        private delegate bool HasSse2Delegate();
        private static HasSse2Delegate _hasSse2 = LoadFunction<HasSse2Delegate>("SDL_HasSSE2");
        public static bool HasSse2() => _hasSse2();

        private delegate bool HasSse3Delegate();
        private static HasSse3Delegate _hasSse3 = LoadFunction<HasSse3Delegate>("SDL_HasSSE3");
        public static bool HasSse3() => _hasSse3();

        private delegate bool HasSse41Delegate();
        private static HasSse41Delegate _hasSse41 = LoadFunction<HasSse41Delegate>("SDL_HasSSE41");
        public static bool HasSse41() => _hasSse41();

        private delegate bool HasSse42Delegate();
        private static HasSse42Delegate _hasSse42 = LoadFunction<HasSse42Delegate>("SDL_HasSSE42");
        public static bool HasSse42() => _hasSse42();

        private delegate bool HasAvxDelegate();
        private static HasAvxDelegate _hasAvx = LoadFunction<HasAvxDelegate>("SDL_HasAVX");
        public static bool HasAvx() => _hasAvx();

        private delegate bool HasAvx2Delegate();
        private static HasAvx2Delegate _hasAvx2 = LoadFunction<HasAvx2Delegate>("SDL_HasAVX2");
        public static bool HasAvx2() => _hasAvx2();

        private delegate bool HasNeonDelegate();
        private static HasNeonDelegate _hasNeon = LoadFunction<HasNeonDelegate>("SDL_HasNEON");
        public static bool HasNeon() => _hasNeon();

        private delegate int GetSystemRamDelegate();
        private static GetSystemRamDelegate _getSystemRam = LoadFunction<GetSystemRamDelegate>("SDL_GetSystemRAM");
        public static int GetSystemRam() => _getSystemRam();

        //
        // SDL_error.h
        //
        private delegate string GetErrorDelegate();
        private static GetErrorDelegate _getError = LoadFunction<GetErrorDelegate>("SDL_GetError");
        public static string GetError() => _getError();

        private delegate int SetErrorDelegate(string format, params object[] objects);
        private static SetErrorDelegate _setError = LoadFunction<SetErrorDelegate>("SDL_SetError");
        public static int SetError(string format, params object[] objects) => _setError(format, objects);

        private delegate void ClearErrorDelegate();
        private static ClearErrorDelegate _clearError = LoadFunction<ClearErrorDelegate>("SDL_ClearError");
        public static void ClearError() => _clearError();

        //
        // SDL_events.h
        //
        private delegate void PumpEventsDelegate();
        private static PumpEventsDelegate _pumpEvents = LoadFunction<PumpEventsDelegate>("SDL_PumpEvents");
        public static void PumpEvents() => _pumpEvents();

        [DllImport(LibraryName, EntryPoint = "SDL_PeepEvents")]
        public static int PeepEvents(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_HasEvent")]
        public static bool HasEvent(EventType type);

        [DllImport(LibraryName, EntryPoint = "SDL_HasEvents")]
        public static bool HasEvents(EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvent")]
        public static void FlushEvent(EventType type);

        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvents")]
        public static void FlushEvents(EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_PollEvent")]
        public static int PollEvent(out Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_WaitEvent")]
        public static int WaitEvent(out Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_WaitEventTimeout")]
        public static int WaitEventTimeout(out Event sdlEvent, int timeout);

        [DllImport(LibraryName, EntryPoint = "SDL_PushEvent")]
        public static int PushEvent(ref Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_SetEventFilter")]
        public static void SetEventFilter(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_GetEventFilter")]
        public static bool GetEventFilter(out EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_AddEventWatch")]
        public static void AddEventWatch(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_DelEventWatch")]
        public static void DelEventWatch(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_FilterEvents")]
        public static void FilterEvents(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_EventState")]
        public static byte EventState(EventType type, State state);

        [DllImport(LibraryName, EntryPoint = "SDL_RegisterEvents")]
        public static uint RegisterEvents(int numEvents);

        //
        // SDL_filesystem.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetBasePath")]
        public static Text GetBasePath();

        [DllImport(LibraryName, EntryPoint = "SDL_GetPrefPath")]
        public static Text GetPrefPath(Text org, Text app);

        //
        // SDL_gamecontroller.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
        public static int GameControllerAddMappingsFromRW(RWops rwOps, int freeRW);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMapping")]
        public static int GameControllerAddMapping(Text mappingString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNumMappings")]
        public static int GameControllerNumMappings();

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForGUID")]
        public static Text GameControllerMappingForGUID(Guid guid);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMapping")]
        public static Text GameControllerMapping(GameController gameController);

        [DllImport(LibraryName, EntryPoint = "SDL_IsGameController")]
        public static bool IsGameController(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNameForIndex")]
        public static Text GameControllerNameForIndex(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerOpen")]
        public static GameController GameControllerOpen(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerFromInstanceID")]
        public static GameController GameControllerFromInstanceID(JoystickID joyid);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerName")]
        public static Text GameControllerName(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAttached")]
        public static bool GameControllerGetAttached(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetJoystick")]
        public static Joystick GameControllerGetJoystick(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerEventState")]
        public static int GameControllerEventState(State state);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerUpdate")]
        public static void GameControllerUpdate();

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxisFromString")]
        public static GameControllerAxis GameControllerGetAxisFromString(Text pchString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetStringForAxis")]
        public static Text GameControllerGetStringForAxis(GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForAxis")]
        public static GameControllerButtonBind GameControllerGetBindForAxis(GameController gamecontroller, GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxis")]
        public static short GameControllerGetAxis(GameController gamecontroller, GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButtonFromString")]
        public static GameControllerButton GameControllerGetButtonFromString(Text pchString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetStringForButton")]
        public static Text GameControllerGetStringForButton(GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForButton")]
        public static GameControllerButtonBind GameControllerGetBindForButton(GameController gamecontroller, GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButton")]
        public static byte GameControllerGetButton(GameController gamecontroller, GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerClose")]
        public static void GameControllerClose(GameController gamecontroller);

        //
        // SDL_hints.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetHintWithPriority")]
        public static bool SetHintWithPriority(Text name, Text value, HintPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_SetHint")]
        public static bool SetHint(Text name, Text value);

        [DllImport(LibraryName, EntryPoint = "SDL_GetHint")]
        private static Text GetHint(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_GetHintBoolean")]
        public static bool GetHintBoolean(Text name, bool defaultValue);

        [DllImport(LibraryName, EntryPoint = "SDL_AddHintCallback")]
        public static void AddHintCallback(Text name, HintCallback callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_DelHintCallback")]
        public static void DelHintCallback(Text name, HintCallback callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_ClearHints")]
        public static void ClearHints();

        //
        // SDL_joystick.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_NumJoysticks")]
        public static int NumJoysticks();

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNameForIndex")]
        public static Text JoystickNameForIndex(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickOpen")]
        public static Joystick JoystickOpen(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickFromInstanceID")]
        public static Joystick JoystickFromInstanceID(JoystickID joystickID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickName")]
        public static Text JoystickName(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceGUID")]
        public static Guid JoystickGetDeviceGUID(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUID")]
        public static Guid JoystickGetGUID(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUIDString")]
        public static void JoystickGetGUIDString(Guid guid, Text pszGUID, int cbGUID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUIDFromString")]
        public static Guid JoystickGetGUIDFromString(Text pchGUID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAttached")]
        public static bool JoystickGetAttached(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickInstanceID")]
        public static JoystickID JoystickInstanceID(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumAxes")]
        public static int JoystickNumAxes(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumBalls")]
        public static int JoystickNumBalls(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumHats")]
        public static int JoystickNumHats(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumButtons")]
        public static int JoystickNumButtons(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickUpdate")]
        public static void JoystickUpdate();

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickEventState")]
        public static int JoystickEventState(State state);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAxis")]
        public static short JoystickGetAxis(SDL2.Joystick joystick, JoystickAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetHat")]
        public static JoystickHat JoystickGetHat(SDL2.Joystick joystick, int hat);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetBall")]
        public static int JoystickGetBall(SDL2.Joystick joystick, int ball, out int dx, out int dy);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetButton")]
        public static byte JoystickGetButton(SDL2.Joystick joystick, int button);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickClose")]
        public static void JoystickClose(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickCurrentPowerLevel")]
        public static JoystickPowerLevel JoystickCurrentPowerLevel(SDL2.Joystick joystick);

        //
        // SDL_keyboard.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardFocus")]
        public static Window GetKeyboardFocus();

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardState")]
        public static Text GetKeyboardState(out int numkeys);

        [DllImport(LibraryName, EntryPoint = "SDL_GetModState")]
        public static KeyMod GetModState();

        [DllImport(LibraryName, EntryPoint = "SDL_SetModState")]
        public static void SetModState(KeyMod modstate);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromScancode")]
        public static KeyCode GetKeyFromScancode(ScanCode scanCode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromKey")]
        public static ScanCode GetScancodeFromKey(KeyCode key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeName")]
        public static Text GetScancodeName(ScanCode scanCode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromName")]
        public static ScanCode GetScancodeFromName(string name);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyName")]
        public static Text GetKeyName(KeyCode key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromName")]
        public static KeyCode GetKeyFromName(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_StartTextInput")]
        public static void StartTextInput();

        [DllImport(LibraryName, EntryPoint = "SDL_IsTextInputActive")]
        public static bool IsTextInputActive();

        [DllImport(LibraryName, EntryPoint = "SDL_StopTextInput")]
        public static void StopTextInput();

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextInputRect")]
        public static void SetTextInputRect(ref Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_HasScreenKeyboardSupport")]
        public static bool HasScreenKeyboardSupport();

        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenKeyboardShown")]
        public static bool IsScreenKeyboardShown(SDL2.Window window);

        //
        // SDL_log.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_LogSetAllPriority")]
        public static void LogSetAllPriority(LogPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_LogSetPriority")]
        public static void LogSetPriority(LogCategory category, LogPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_LogGetPriority")]
        public static LogPriority LogGetPriority(LogCategory category);

        [DllImport(LibraryName, EntryPoint = "SDL_LogResetPriorities")]
        public static void LogResetPriorities();

        [DllImport(LibraryName, EntryPoint = "SDL_Log")]
        public static void Log(Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogVerbose")]
        public static void LogVerbose(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogDebug")]
        public static void LogDebug(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogInfo")]
        public static void LogInfo(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogWarn")]
        public static void LogWarn(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogError")]
        public static void LogError(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogCritical")]
        public static void LogCritical(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogMessage")]
        public static void LogMessage(LogCategory category, LogPriority priority, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogGetOutputFunction")]
        public static void LogGetOutputFunction(LogOutputFunction callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_LogSetOutputFunction")]
        public static void LogSetOutputFunction(LogOutputFunction callback, void* userData);

        //
        // SDL_messagebox.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_ShowMessageBox")]
        public static int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowSimpleMessageBox")]
        public static int ShowSimpleMessageBox(MessageBoxFlags flags, Text title, Text message, SDL2.Window window);

        //
        // SDL_mouse.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseFocus")]
        public static Window GetMouseFocus();

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static MouseButtonState GetMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static MouseButtonState GetMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static MouseButtonState GetMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static MouseButtonState GetMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static MouseButtonState GetGlobalMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static MouseButtonState GetGlobalMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static MouseButtonState GetGlobalMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static MouseButtonState GetGlobalMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static MouseButtonState GetRelativeMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static MouseButtonState GetRelativeMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static MouseButtonState GetRelativeMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static MouseButtonState GetRelativeMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseInWindow")]
        public static void WarpMouseInWindow(SDL2.Window window, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseGlobal")]
        public static int WarpMouseGlobal(int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRelativeMouseMode")]
        public static int SetRelativeMouseMode(bool enabled);

        [DllImport(LibraryName, EntryPoint = "SDL_CaptureMouse")]
        public static int CaptureMouse(bool enabled);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseMode")]
        public static bool GetRelativeMouseMode();

        [DllImport(LibraryName, EntryPoint = "SDL_CreateCursor")]
        public static Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateColorCursor")]
        public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateSystemCursor")]
        public static Cursor CreateSystemCursor(SystemCursor id);

        [DllImport(LibraryName, EntryPoint = "SDL_SetCursor")]
        public static void SetCursor(Cursor cursor);

        [DllImport(LibraryName, EntryPoint = "SDL_GetCursor")]
        public static Cursor GetCursor();

        [DllImport(LibraryName, EntryPoint = "SDL_GetDefaultCursor")]
        public static Cursor GetDefaultCursor();

        [DllImport(LibraryName, EntryPoint = "SDL_FreeCursor")]
        public static void FreeCursor(Cursor cursor);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowCursor")]
        public static State ShowCursor(State toggle);

        //
        // SDL_pixels.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPixelFormatName")]
        public static Text GetPixelFormatName(uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_PixelFormatEnumToMasks")]
        public static bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);

        [DllImport(LibraryName, EntryPoint = "SDL_MasksToPixelFormatEnum")]
        public static uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);

        [DllImport(LibraryName, EntryPoint = "SDL_AllocFormat")]
        public static PixelFormat* AllocFormat(uint pixelFormat);

        [DllImport(LibraryName, EntryPoint = "SDL_FreeFormat")]
        public static void FreeFormat(ref PixelFormat pixelFormat);

        [DllImport(LibraryName, EntryPoint = "SDL_AllocPalette")]
        public static Palette AllocPalette(int numColors);

        [DllImport(LibraryName, EntryPoint = "SDL_SetPixelFormatPalette")]
        public static int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette);

        [DllImport(LibraryName, EntryPoint = "SDL_SetPaletteColors")]
        public static int SetPaletteColors(Palette palette, Color[] colors, int firstColor, int numColors);

        [DllImport(LibraryName, EntryPoint = "SDL_FreePalette")]
        public static void FreePalette(Palette palette);

        [DllImport(LibraryName, EntryPoint = "SDL_MapRGB")]
        public static uint MapRGB(ref PixelFormat format, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_MapRGBA")]
        public static uint MapRGBA(ref PixelFormat format, byte r, byte g, byte b, byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRGB")]
        public static void GetRGB(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRGBA")]
        public static void GetRGBA(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_CalculateGammaRamp")]
        public static void CalculateGammaRamp(float gamma, out ushort[] ramp);

        //
        // SDL_platform.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPlatform")]
        public static Text GetPlatform();

        //
        // SDL_power.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPowerInfo")]
        public static PowerState GetPowerInfo(out int seconds, out int percentage);

        //
        // SDL_rect.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_HasIntersection")]
        public static bool HasIntersection(ref Rect a, ref Rect b);

        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRect")]
        public static bool IntersectRect(ref Rect a, ref Rect b, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_UnionRect")]
        public static void UnionRect(ref Rect a, ref Rect b, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_EnclosePoints")]
        public static bool EnclosePoints(Point[] points, int count, ref Rect clip, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine")]
        public static bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

        //
        // SDL_render.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetNumRenderDrivers")]
        public static int GetNumRenderDrivers();

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDriverInfo")]
        public static int GetRenderDriverInfo(int index, out RendererInfo info);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowAndRenderer")]
        public static int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out SDL2.Window window, out Renderer renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRenderer")]
        public static Renderer CreateRenderer(SDL2.Window window, int index, RendererFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateSoftwareRenderer")]
        public static IntPtr CreateSoftwareRenderer(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderer")]
        public static Renderer GetRenderer(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererInfo")]
        public static int GetRendererInfo(IntPtr renderer, out RendererInfo info);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererOutputSize")]
        public static int GetRendererOutputSize(IntPtr renderer, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateTexture")]
        public static IntPtr CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateTextureFromSurface")]
        public static IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_QueryTexture")]
        public static int QueryTexture(IntPtr texture, out uint format, out int access, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureColorMod")]
        public static int SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureColorMod")]
        public static int GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureAlphaMod")]
        public static int SetTextureAlphaMod(IntPtr texture, byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureAlphaMod")]
        public static int GetTextureAlphaMod(IntPtr texture, out byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureBlendMode")]
        public static int SetTextureBlendMode(IntPtr texture, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureBlendMode")]
        public static int GetTextureBlendMode(IntPtr texture, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateTexture")]
        public static int UpdateTexture(IntPtr texture, ref Rect rect, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateTexture")]
        public static int UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_LockTexture")]
        public static int LockTexture(IntPtr texture, ref Rect rect, out IntPtr pixels, out int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_LockTexture")]
        public static int LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_UnlockTexture")]
        public static void UnlockTexture(IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderTargetSupported")]
        public static bool RenderTargetSupported(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderTarget")]
        public static int SetRenderTarget(IntPtr renderer, IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderTarget")]
        public static IntPtr GetRenderTarget(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetLogicalSize")]
        public static int RenderSetLogicalSize(IntPtr renderer, int w, int h);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetLogicalSize")]
        public static void RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetIntegerScale")]
        public static int RenderSetIntegerScale(IntPtr renderer, bool enable);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetIntegerScale")]
        public static bool RenderGetIntegerScale(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetViewport")]
        public static int RenderSetViewport(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetViewport")]
        public static void RenderGetViewport(IntPtr renderer, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetClipRect")]
        public static int RenderSetClipRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetClipRect")]
        public static void RenderGetClipRect(IntPtr renderer, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderIsClipEnabled")]
        public static bool RenderIsClipEnabled(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetScale")]
        public static int RenderSetScale(IntPtr renderer, float scaleX, float scaleY);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetScale")]
        public static void RenderGetScale(IntPtr renderer, out float scaleX, out float scaleY);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawColor")]
        public static int SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawColor")]
        public static int GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawBlendMode")]
        public static int SetRenderDrawBlendMode(IntPtr renderer, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawBlendMode")]
        public static int GetRenderDrawBlendMode(IntPtr renderer, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderClear")]
        public static int RenderClear(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoint")]
        public static int RenderDrawPoint(IntPtr renderer, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoints")]
        public static int RenderDrawPoints(IntPtr renderer, Point[] points, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLine")]
        public static int RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLines")]
        public static int RenderDrawLines(IntPtr renderer, Point[] points, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRect")]
        public static int RenderDrawRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRect")]
        public static int RenderDrawRect(IntPtr renderer, IntPtr rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRects")]
        public static int RenderDrawRects(IntPtr renderer, Rect[] rects, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRect")]
        public static int RenderFillRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRect")]
        public static int RenderFillRect(IntPtr renderer, IntPtr rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRects")]
        public static int RenderFillRects(IntPtr renderer, Rect[] rects, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopyEx")]
        public static int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopyEx")]
        public static int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderReadPixels")]
        public static int RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderPresent")]
        public static void RenderPresent(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyTexture")]
        public static void DestroyTexture(IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyRenderer")]
        public static void DestroyRenderer(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_GL_BindTexture")]
        public static int GL_BindTexture(IntPtr texture, out float texw, out float texh);

        [DllImport(LibraryName, EntryPoint = "SDL_GL_UnbindTexture")]
        public static int GL_UnbindTexture(IntPtr texture);

        //
        // SDL_rwops.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_RWFromFile")]
        public static IntPtr RWFromFile(string file, string mode);

        [DllImport(LibraryName, EntryPoint = "SDL_RWclose")]
        public static int RWclose(IntPtr context);

        [DllImport(LibraryName, EntryPoint = "SDL_RWread")]
        public static int RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

        [DllImport(LibraryName, EntryPoint = "SDL_RWsize")]
        public static long RWsize(IntPtr context);

        //
        // SDL_shape.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_CreateShapedWindow")]
        public static Window CreateShapedWindow(byte title, uint x, uint y, uint w, uint h, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_IsShapedWindow")]
        public static bool IsShapedWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowShape")]
        public static int SetWindowShape(SDL2.Window window, Surface* shape, WindowShape* shapeMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetShapedWindowMode")]
        public static int GetShapedWindowMode(SDL2.Window window, out WindowShape shapeMode);

        //
        // SDL_stdinc.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_malloc")]
        public static void* Malloc(Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_calloc")]
        public static void* Calloc(Size nmemb, Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_realloc")]
        public static void* Realloc(void* mem, Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_free")]
        public static void Free(void* mem);

        [DllImport(LibraryName, EntryPoint = "SDL_getenv")]
        public static Text Getenv(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_setenv")]
        public static int Setenv(Text name, Text value, int overwrite);

        [DllImport(LibraryName, EntryPoint = "SDL_qsort")]
        public static void Qsort(void* buffer, Size nmemb, Size size, IntPtr compare);

        //
        // SDL_surface.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertPixels")]
        public static int ConvertPixels(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertSurface")]
        public static IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertSurfaceFormat")]
        public static IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurface")]
        public static IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
        public static IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormat")]
        public static IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormatFrom")]
        public static IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRect")]
        public static int FillRect(IntPtr dst, ref Rect rect, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRect")]
        public static int FillRect(IntPtr dst, IntPtr rect, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRects")]
        public static int FillRects(IntPtr dst, Rect[] rects, int count, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FreeSurface")]
        public static void FreeSurface(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_GetClipRect")]
        public static void GetClipRect(IntPtr surface, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_GetColorKey")]
        public static int GetColorKey(IntPtr surface, out uint key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
        public static int GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceBlendMode")]
        public static int GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceColorMod")]
        public static int GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_LoadBMP_RW")]
        private static IntPtr LoadBMP_RW(IntPtr src, int freesrc);

        [DllImport(LibraryName, EntryPoint = "SDL_LockSurface")]
        public static int LockSurface(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_LowerBlit")]
        public static int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_LowerBlitScaled")]
        public static int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_SaveBMP_RW")]
        private static int SaveBMP_RW(IntPtr surface, IntPtr src, int freesrc);

        [DllImport(LibraryName, EntryPoint = "SDL_SetClipRect")]
        public static bool SetClipRect(IntPtr surface, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_SetColorKey")]
        public static int SetColorKey(IntPtr surface, int flag, uint key);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
        public static int SetSurfaceAlphaMod(IntPtr surface, byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceBlendMode")]
        public static int SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceColorMod")]
        public static int SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfacePalette")]
        public static int SetSurfacePalette(IntPtr surface, IntPtr palette);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceRLE")]
        public static int SetSurfaceRLE(IntPtr surface, int flag);

        [DllImport(LibraryName, EntryPoint = "SDL_SoftStretch")]
        public static int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UnlockSurface")]
        public static void UnlockSurface(IntPtr surface);

        //
        // SDL_syswm.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowWMInfo")]
        public static bool GetWindowWMInfo(SDL2.Window window, ref SysWMInfo info);

        //
        // SDL_timer.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetTicks")]
        public static uint GetTicks();

        public static bool TicksPassed(uint a, uint b)
        {
            return ((int)(b - a) <= 0);
        }

        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceCounter")]
        public static ulong GetPerformanceCounter();

        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceFrequency")]
        public static ulong GetPerformanceFrequency();

        [DllImport(LibraryName, EntryPoint = "SDL_Delay")]
        public static void Delay(uint ms);

        [DllImport(LibraryName, EntryPoint = "SDL_AddTimer")]
        public static TimerID AddTimer(uint interval, TimerCallback callback, IntPtr param);

        [DllImport(LibraryName, EntryPoint = "SDL_RemoveTimer")]
        public static bool RemoveTimer(TimerID id);

        //
        // SDL_version.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetVersion")]
        public static void GetVersion(out Version version);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRevision")]
        public static Text GetRevision();

        [DllImport(LibraryName, EntryPoint = "SDL_GetRevisionNumber")]
        public static int GetRevisionNumber();

        //
        // SDL_video.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDrivers")]
        public static int GetNumVideoDrivers();

        [DllImport(LibraryName, EntryPoint = "SDL_GetVideoDriver")]
        private static IntPtr GetVideoDriver(int index);

        [DllImport(LibraryName, EntryPoint = "SDL_VideoInit")]
        public static int VideoInit(IntPtr driver_name);

        [DllImport(LibraryName, EntryPoint = "SDL_VideoQuit")]
        public static void VideoQuit();

        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentVideoDriver")]
        public static IntPtr GetCurrentVideoDriver();

        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDisplays")]
        public static int GetNumVideoDisplays();

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayName")]
        public static IntPtr GetDisplayName(int displayIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayBounds")]
        public static int GetDisplayBounds(int displayIndex, out Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayDPI")]
        public static int GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayUsableBounds")]
        public static int GetDisplayUsableBounds(int displayIndex, out Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_GetNumDisplayModes")]
        public static int GetNumDisplayModes(int displayIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayMode")]
        public static int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDeskTopDisplayMode")]
        public static int GetDeskTopDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentDisplayMode")]
        public static int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetClosestDisplayMode")]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static DisplayMode GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayIndex")]
        public static int GetWindowDisplayIndex(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
        public static int SetWindowDisplayMode(SDL2.Window window, ref DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
        public static int SetWindowDisplayMode(SDL2.Window window, IntPtr mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayMode")]
        public static int GetWindowDisplayMode(SDL2.Window window, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPixelFormat")]
        public static uint GetWindowPixelFormat(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindow")]
        public static Window CreateWindow(Text title, int x, int y, int width, int height, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowFrom")]
        public static Window CreateWindowFrom(void* data);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowID")]
        public static WindowID GetWindowID(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFromID")]
        public static Window GetWindowFromID(WindowID id);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFlags")]
        public static WindowFlags GetWindowFlags(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowTitle")]
        public static void SetWindowTitle(SDL2.Window window, Text title);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowTitle")]
        public static Text GetWindowTitle(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowIcon")]
        public static void SetWindowIcon(SDL2.Window window, Surface icon);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowData")]
        public static void* SetWindowData(SDL2.Window window, Text name, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowData")]
        public static void* GetWindowData(SDL2.Window window, Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowPosition")]
        public static void SetWindowPosition(SDL2.Window window, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static void GetWindowPosition(SDL2.Window window, out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static void GetWindowPosition(SDL2.Window window, out int x, IntPtr y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static void GetWindowPosition(SDL2.Window window, IntPtr x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowSize")]
        public static void SetWindowSize(SDL2.Window window, int width, int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static void GetWindowSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static void GetWindowSize(SDL2.Window window, out int width, IntPtr height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static void GetWindowSize(SDL2.Window window, IntPtr width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBordersSize")]
        public static int GetWindowBordersSize(SDL2.Window window, out int top, out int left, out int bottom, out int right);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMinimumSize")]
        public static void SetWindowMinimumSize(SDL2.Window window, int minwidth, int minHeight);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMinimumSize")]
        public static void GetWindowMinimumSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMaximumSize")]
        public static void SetWindowMaximumSize(SDL2.Window window, int maxWidth, int maxHeight);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMaximumSize")]
        public static void GetWindowMaximumSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBordered")]
        public static void SetWindowBordered(SDL2.Window window, bool bordered);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowResizable")]
        public static void SetWindowResizable(SDL2.Window window, bool resizable);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowWindow")]
        public static void ShowWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_HideWindow")]
        public static void HideWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_RaiseWindow")]
        public static void RaiseWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_MaximizeWindow")]
        public static void MaximizeWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_MinimizeWindow")]
        public static void MinimizeWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_RestoreWindow")]
        public static void RestoreWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowFullscreen")]
        public static int SetWindowFullscreen(SDL2.Window window, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSurface")]
        public static Surface* GetWindowSurface(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurface")]
        public static int UpdateWindowSurface(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
        public static int UpdateWindowSurfaceRects(SDL2.Window window, Rect* rectangles, int numRectangles);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGrab")]
        public static void SetWindowGrab(SDL2.Window window, bool grabbed);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGrab")]
        public static bool GetWindowGrab(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGrabbedWindow")]
        public static Window GetGrabbedWindow();

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBrightness")]
        public static int SetWindowBrightness(SDL2.Window window, float brightness);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBrightness")]
        public static float GetWindowBrightness(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowOpacity")]
        public static int SetWindowOpacity(SDL2.Window window, float opacity);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowOpacity")]
        public static int GetWindowOpacity(SDL2.Window window, out float outOpacity);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowModalFor")]
        public static int SetWindowModalFor(SDL2.Window modalWindow, SDL2.Window parentWindow);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowInputFocus")]
        public static int SetWindowInputFocus(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGammaRamp")]
        public static int SetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGammaRamp")]
        public static int GetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowHitTest")]
        public static int SetWindowHitTest(SDL2.Window window, HitTest callback, void* callbackData);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyWindow")]
        public static void DestroyWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenSaverEnabled")]
        public static bool IsScreenSaverEnabled();

        [DllImport(LibraryName, EntryPoint = "SDL_EnableScreenSaver")]
        public static void EnableScreenSaver();

        [DllImport(LibraryName, EntryPoint = "SDL_DisableScreenSaver")]
        public static void DisableScreenSaver();

        public static class Hints
        {
            public const string FrameBufferAcceleration = "FRAMEBUFFER_ACCELERATION";
            public const string RenderDriver = "RENDER_DRIVER";
            public const string RenderOpenglShaders = "RENDER_OPENGL_SHADERS";
            public const string RenderDirect3DThreadsafe = "RENDER_DIRECT3D_THREADSAFE";
            public const string RenderDirect3D11Debug = "RENDER_DIRECT3D11_DEBUG";
            public const string RenderScaleQuality = "RENDER_SCALE_QUALITY";
            public const string RenderVsync = "RENDER_VSYNC";
            public const string VideoAllowScreensave = "VIDEO_ALLOW_SCREENSAVER";
            public const string VideoX11XvidMode = "VIDEO_X11_XVIDMODE";
            public const string VideoX11Xinerama = "VIDEO_X11_XINERAMA";
            public const string VideoX11XRandR = "VIDEO_X11_XRANDR";
            public const string VideoX11NetWMPing = "VIDEO_X11_NET_WM_PING";
            public const string WindowFrameUsableWhileCursorHidden = "WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
            public const string WindowsEnableMessageLoop = "WINDOWS_ENABLE_MESSAGELOOP";
            public const string GrabKeyboard = "GRAB_KEYBOARD";
            public const string MouseRelativeModeWarp = "MOUSE_RELATIVE_MODE_WARP";
            public const string MouseFocusClickThrough = "MOUSE_FOCUS_CLICKTHROUGH";
            public const string VideoMinimizeOnFocusLoss = "VIDEO_MINIMIZE_ON_FOCUS_LOSS";
            public const string IdleTimerDisabled = "IOS_IDLE_TIMER_DISABLED";
            public const string Orientations = "IOS_ORIENTATIONS";
            public const string AppleTVControllerUIEvents = "APPLE_TV_CONTROLLER_UI_EVENTS";
            public const string AppleTVRemoteAllowRotation = "APPLE_TV_REMOTE_ALLOW_ROTATION";
            public const string AccelerometerAsJoystick = "ACCELEROMETER_AS_JOYSTICK";
            public const string XInputEnabled = "XINPUT_ENABLED";
            public const string XInputUseOldJoystickMapping = "XINPUT_USE_OLD_JOYSTICK_MAPPING";
            public const string GameControllerConfig = "GAMECONTROLLERCONFIG";
            public const string JoystickAllowBackgroundEvents = "JOYSTICK_ALLOW_BACKGROUND_EVENTS";
            public const string AllowTopmost = "ALLOW_TOPMOST";
            public const string TimerResolution = "TIMER_RESOLUTION";
            public const string ThreadStackSize = "THREAD_STACK_SIZE";
            public const string VideoHighDPIDisabled = "VIDEO_HIGHDPI_DISABLED";
            public const string MacCtrlClickEmulateRightClick = "MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
            public const string VideoWinD3DCompiler = "VIDEO_WIN_D3DCOMPILER";
            public const string VideoWindowSharePixelFormat = "VIDEO_WINDOW_SHARE_PIXEL_FORMAT";
            public const string WinRTPrivacyPolicyURL = "WINRT_PRIVACY_POLICY_URL";
            public const string WinRTPrivacyPolicyLabel = "WINRT_PRIVACY_POLICY_LABEL";
            public const string WinRTHandleBackButton = "WINRT_HANDLE_BACK_BUTTON";
            public const string VideoMacFullscreenSpaces = "VIDEO_MAC_FULLSCREEN_SPACES";
            public const string MacBackgroundApp = "MAC_BACKGROUND_APP";
            public const string AndroidAPKExpansionMainFileVersion = "ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";
            public const string AndroidAPKExpansionPatchFileVersion = "ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";
            public const string IMEInternalEditing = "IME_INTERNAL_EDITING";
            public const string AndroidSeparateMouseAndTouch = "ANDROID_SEPARATE_MOUSE_AND_TOUCH";
            public const string EmscriptenKeyboardElement = "EMSCRIPTEN_KEYBOARD_ELEMENT";
            public const string NoSignalHandlers = "NO_SIGNAL_HANDLERS";
            public const string WindowsNoCloseOnAltF4 = "WINDOWS_NO_CLOSE_ON_ALT_F4";
            public const string BMPSaveLegacyFormat = "BMP_SAVE_LEGACY_FORMAT";
            public const string WindowsDisableThreadWarning = "WINDOWS_DISABLE_THREAD_NAMING";
            public const string RPIVideoLayer = "RPI_VIDEO_LAYER";
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
    }
}