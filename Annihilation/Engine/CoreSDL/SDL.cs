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
        private delegate int SetClipboardstringDelegate(string text);
        private static SetClipboardstringDelegate _setClipboardstring = LoadFunction<SetClipboardstringDelegate>("SDL_SetClipboardstring");
        public static int SetClipboardstring(string text) => _setClipboardstring(text);

        private delegate string GetClipboardstringDelegate();
        private static GetClipboardstringDelegate _getClipboardstring = LoadFunction<GetClipboardstringDelegate>("SDL_GetClipboardstring");
        public static string GetClipboardstring() => _getClipboardstring();

        private delegate bool HasClipboardstringDelegate();
        private static HasClipboardstringDelegate _hasClipboardstring = LoadFunction<HasClipboardstringDelegate>("SDL_HasClipboardstring");
        public static bool HasClipboardstring() => _hasClipboardstring();

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

        private delegate int PeepEventsDelegate(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType);
        private static PeepEventsDelegate _peepEvents = LoadFunction<PeepEventsDelegate>("SDL_PeepEvents");
        public static int PeepEvents(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType) => _peepEvents(events, numEvents, action, minType, maxType);

        private delegate bool HasEventDelegate(EventType type);
        private static HasEventDelegate _hasEvent = LoadFunction<HasEventDelegate>("SDL_HasEvent");
        public static bool HasEvent(EventType type) => _hasEvent(type);

        private delegate bool HasEventsDelegate(EventType minType, EventType maxType);
        private static HasEventsDelegate _hasEvents = LoadFunction<HasEventsDelegate>("SDL_HasEvents");
        public static bool HasEvents(EventType minType, EventType maxType) => _hasEvents(minType, maxType);

        private delegate void FlushEventDelegate(EventType type);
        private static FlushEventDelegate _flushEvent = LoadFunction<FlushEventDelegate>("SDL_FlushEvent");
        public static void FlushEvent(EventType type) => _flushEvent(type);

        private delegate void FlushEventsDelegate(EventType minType, EventType maxType);
        private static FlushEventsDelegate _flushEvents = LoadFunction<FlushEventsDelegate>("SDL_FlushEvents");
        public static void FlushEvents(EventType minType, EventType maxType) => _flushEvents(minType, maxType);

        private delegate int PollEventDelegate(out Event sdlEvent);
        private static PollEventDelegate _pollEvent = LoadFunction<PollEventDelegate>("SDL_PollEvent");
        public static int PollEvent(out Event sdlEvent) => _pollEvent(out sdlEvent);

        private delegate int WaitEventDelegate(out Event sdlEvent);
        private static WaitEventDelegate _waitEvent = LoadFunction<WaitEventDelegate>("SDL_WaitEvent");
        public static int WaitEvent(out Event sdlEvent) => _waitEvent(out sdlEvent);

        private delegate int WaitEventTimeoutDelegate(out Event sdlEvent, int timeout);
        private static WaitEventTimeoutDelegate _waitEventTimeout = LoadFunction<WaitEventTimeoutDelegate>("SDL_WaitEventTimeout");
        public static int WaitEventTimeout(out Event sdlEvent, int timeout) => _waitEventTimeout(out sdlEvent, timeout);

        private delegate int PushEventDelegate(ref Event sdlEvent);
        private static PushEventDelegate _pushEvent = LoadFunction<PushEventDelegate>("SDL_PushEvent");
        public static int PushEvent(ref Event sdlEvent) => _pushEvent(ref sdlEvent);

        private delegate void SetEventFilterDelegate(EventFilter filter, IntPtr userData);
        private static SetEventFilterDelegate _setEventFilter = LoadFunction<SetEventFilterDelegate>("SDL_SetEventFilter");
        public static void SetEventFilter(EventFilter filter, IntPtr userData) => _setEventFilter(filter, userData);

        private delegate bool GetEventFilterDelegate(out EventFilter filter, IntPtr userData);
        private static GetEventFilterDelegate _getEventFilter = LoadFunction<GetEventFilterDelegate>("SDL_GetEventFilter");
        public static bool GetEventFilter(out EventFilter filter, IntPtr userData) => _getEventFilter(out filter, userData);

        private delegate void AddEventWatchDelegate(EventFilter filter, IntPtr userData);
        private static AddEventWatchDelegate _addEventWatch = LoadFunction<AddEventWatchDelegate>("SDL_AddEventWatch");
        public static void AddEventWatch(EventFilter filter, IntPtr userData) => _addEventWatch(filter, userData);

        private delegate void DelEventWatchDelegate(EventFilter filter, IntPtr userData);
        private static DelEventWatchDelegate _delEventWatch = LoadFunction<DelEventWatchDelegate>("SDL_DelEventWatch");
        public static void DelEventWatch(EventFilter filter, IntPtr userData) => _delEventWatch(filter, userData);

        private delegate void FilterEventsDelegate(EventFilter filter, IntPtr userData);
        private static FilterEventsDelegate _filterEvents = LoadFunction<FilterEventsDelegate>("SDL_FilterEvents");
        public static void FilterEvents(EventFilter filter, IntPtr userData) => _filterEvents(filter, userData);

        private delegate State EventStateDelegate(EventType type, State state);
        private static EventStateDelegate _eventState = LoadFunction<EventStateDelegate>("SDL_EventState");
        public static State EventState(EventType type, State state) => _eventState(type, state);

        public static State GetEventState(EventType type) => _eventState(type, State.Query);

        private delegate uint RegisterEventsDelegate(int numEvents);
        private static RegisterEventsDelegate _registerEvents = LoadFunction<RegisterEventsDelegate>("SDL_RegisterEvents");
        public static uint RegisterEvents(int numEvents) => _registerEvents(numEvents);

        //
        // SDL_filesystem.h
        //
        private delegate string GetBasePathDelegate();
        private static GetBasePathDelegate _getBasePath = LoadFunction<GetBasePathDelegate>("SDL_GetBasePath");
        public static string GetBasePath() => _getBasePath();

        private delegate string GetPrefPathDelegate(string org, string app);
        private static GetPrefPathDelegate _getPrefPath = LoadFunction<GetPrefPathDelegate>("SDL_GetPrefPath");
        public static string GetPrefPath(string org, string app) => _getPrefPath(org, app);

        //
        // SDL_gamecontroller.h
        //
        private delegate int GameControllerAddMappingsFromRWDelegate(RWops rwOps, int freeRW);
        private static GameControllerAddMappingsFromRWDelegate _gameControllerAddMappingsFromRW = LoadFunction<GameControllerAddMappingsFromRWDelegate>("SDL_GameControllerAddMappingsFromRW");
        public static int GameControllerAddMappingsFromRW(RWops rwOps, int freeRW) => _gameControllerAddMappingsFromRW(rwOps, freeRW);

        public static int GameControllerAddMappingsFromFile(string file) => _gameControllerAddMappingsFromRW(RWFromFile(file, "rb"), 1);

        private delegate int GameControllerAddMappingDelegate(string mappginString);
        private static GameControllerAddMappingDelegate _gameControllerAddMapping = LoadFunction<GameControllerAddMappingDelegate>("SDL_GameControllerAddMapping");
        public static int GameControllerAddMapping(string mappingString) => _gameControllerAddMapping(mappingString);

        private delegate int GameControllerNumMappingsDelegate();
        private static GameControllerNumMappingsDelegate _gameControllerNumMappings = LoadFunction<GameControllerNumMappingsDelegate>("SDL_GameControllerNumMappings");
        public static int GameControllerNumMappings() => _gameControllerNumMappings();

        private delegate string GameControllerMappingForIndexDelegate(int mappingIndex);
        private static GameControllerMappingForIndexDelegate _gameControllerMappingForIndex = LoadFunction<GameControllerMappingForIndexDelegate>("SDL_GameControllerMappingForIndex");
        public static string GameControllerMappingForIndex(int mappingIndex) => _gameControllerMappingForIndex(mappingIndex);

        private delegate string GameControllerMappingForGuidDelegate(Guid guid);
        private static GameControllerMappingForGuidDelegate _gameControllerMappingForGuid = LoadFunction<GameControllerMappingForGuidDelegate>("SDL_GameControllerMappingForGUID");
        public static string GameControllerMappingForGuid(Guid guid) => _gameControllerMappingForGuid(guid);

        private delegate string GameControllerMappingDelegate(GameController gameController);
        private static GameControllerMappingDelegate _gameControllerMapping = LoadFunction<GameControllerMappingDelegate>("SDL_GameControllerMapping");
        public static string GameControllerMapping(GameController gameController) => _gameControllerMapping(gameController);

        private delegate bool IsGameControllerDelegate(int joystickIndex);
        private static IsGameControllerDelegate _isGameController = LoadFunction<IsGameControllerDelegate>("SDL_IsGameController");
        public static bool IsGameController(int joystickIndex) => _isGameController(joystickIndex);

        private delegate string GameControllerNameForIndexDelegate(int joystickIndex);
        private static GameControllerNameForIndexDelegate _gameControllerNameForIndex = LoadFunction<GameControllerNameForIndexDelegate>("SDL_GameControllerNameForIndex");
        public static string GameControllerNameForIndex(int joystickIndex) => _gameControllerNameForIndex(joystickIndex);

        private delegate GameController GameControllerOpenDelegate(int joystickIndex);
        private static GameControllerOpenDelegate _gameControllerOpen = LoadFunction<GameControllerOpenDelegate>("SDL_GameControllerOpen");
        public static GameController GameControllerOpen(int joystickIndex) => _gameControllerOpen(joystickIndex);

        private delegate GameController GameControllerFromInstanceIDDelegate(JoystickID joystickID);
        private static GameControllerFromInstanceIDDelegate _gameControllerFromInstanceID = LoadFunction<GameControllerFromInstanceIDDelegate>("SDL_GameControllerFromInstanceID");
        public static GameController GameControllerFromInstanceID(JoystickID joystickID) => _gameControllerFromInstanceID(joystickID);

        private delegate string GameControllerNameDelegate(GameController gameController);
        private static GameControllerNameDelegate _gameControllerName = LoadFunction<GameControllerNameDelegate>("SDL_GameControllerName");
        public static string GameControllerName(GameController gameController) => _gameControllerName(gameController);

        private delegate ushort GameControllerGetVendorDelegate(GameController gameController);
        private static GameControllerGetVendorDelegate _gameControllerGetVendor = LoadFunction<GameControllerGetVendorDelegate>("SDL_GameControllerGetVendor");
        public static ushort GameControllerGetVendor(GameController gameController) => _gameControllerGetVendor(gameController);

        private delegate ushort GameControllerGetProductDelegate(GameController gameController);
        private static GameControllerGetProductDelegate _gameControllerGetProduct = LoadFunction<GameControllerGetProductDelegate>("SDL_GameControllerGetProduct");
        public static ushort GameControllerGetProduct(GameController gameController) => _gameControllerGetProduct(gameController);

        private delegate ushort GameControllerGetProductVersionDelegate(GameController gameController);
        private static GameControllerGetProductVersionDelegate _gameControllerGetProductVersion = LoadFunction<GameControllerGetProductVersionDelegate>("SDL_GameControllerGetProductVersion");
        public static ushort GameControllerGetProductVersion(GameController gameController) => _gameControllerGetProductVersion(gameController);

        private delegate bool GameControllerGetAttachedDelegate(GameController gameController);
        private static GameControllerGetAttachedDelegate _gameControllerGetAttached = LoadFunction<GameControllerGetAttachedDelegate>("SDL_GameControllerGetAttached");
        public static bool GameControllerGetAttached(GameController gameController) => _gameControllerGetAttached(gameController);

        private delegate Joystick GameControllerGetJoystickDelegate(GameController gameController);
        private static GameControllerGetJoystickDelegate _gameControllerGetJoystick = LoadFunction<GameControllerGetJoystickDelegate>("SDL_GameControllerGetJoystick");
        public static Joystick GameControllerGetJoystick(GameController gameController) => _gameControllerGetJoystick(gameController);

        private delegate State GameControllerEventStateDelegate(State state);
        private static GameControllerEventStateDelegate _gameControllerEventState = LoadFunction<GameControllerEventStateDelegate>("SDL_GameControllerEventState");
        public static State GameControllerEventState(State state) => _gameControllerEventState(state);

        private delegate void GameControllerUpdateDelegate();
        private static GameControllerUpdateDelegate _gameControllerUpdate = LoadFunction<GameControllerUpdateDelegate>("SDL_GameControllerUpdate");
        public static void GameControllerUpdate() => _gameControllerUpdate();

        private delegate GameControllerAxis GameControllerGetAxisFromStringDelegate(string pchString);
        private static GameControllerGetAxisFromStringDelegate _gameControllerGetAxisFromString = LoadFunction<GameControllerGetAxisFromStringDelegate>("SDL_GameControllerGetAxisFromString");
        public static GameControllerAxis GameControllerGetAxisFromString(string pchString) => _gameControllerGetAxisFromString(pchString);

        private delegate string GameControllerGetStringForAxisDelegate(GameControllerAxis axis);
        private static GameControllerGetStringForAxisDelegate _gameControllerGetStringForAxis = LoadFunction<GameControllerGetStringForAxisDelegate>("SDL_GameControllerGetStringForAxis");
        public static string GameControllerGetStringForAxis(GameControllerAxis axis) => _gameControllerGetStringForAxis(axis);

        private delegate GameControllerButtonBind GameControllerGetBindForAxisDelegate(GameController gameController, GameControllerAxis axis);
        private static GameControllerGetBindForAxisDelegate _gameControllerGetBindForAxis = LoadFunction<GameControllerGetBindForAxisDelegate>("SDL_GameControllerGetBindForAxis");
        public static GameControllerButtonBind GameControllerGetBindForAxis(GameController gameController, GameControllerAxis axis) => _gameControllerGetBindForAxis(gameController, axis);

        private delegate short GameControllerGetAxisDelegate(GameController gameController, GameControllerAxis axis);
        private static GameControllerGetAxisDelegate _gameControllerGetAxis = LoadFunction<GameControllerGetAxisDelegate>("SDL_GameControllerGetAxis");
        public static short GameControllerGetAxis(GameController gameController, GameControllerAxis axis) => _gameControllerGetAxis(gameController, axis);

        private delegate GameControllerButton GameControllerGetButtonFromStringDelegate(string pchString);
        private static GameControllerGetButtonFromStringDelegate _gameControllerGetButtonFromString = LoadFunction<GameControllerGetButtonFromStringDelegate>("SDL_GameControllerGetButtonFromString");
        public static GameControllerButton GameControllerGetButtonFromString(string pchString) => _gameControllerGetButtonFromString(pchString);

        private delegate string GameControllerGetStringForButtonDelegate(GameControllerButton button);
        private static GameControllerGetStringForButtonDelegate _gameControllerGetStringForButton = LoadFunction<GameControllerGetStringForButtonDelegate>("SDL_GameControllerGetStringForButton");
        public static string GameControllerGetStringForButton(GameControllerButton button) => _gameControllerGetStringForButton(button);

        private delegate GameControllerButtonBind GameControllerGetBindForButtonDelegate(GameController gameController, GameControllerButton button);
        private static GameControllerGetBindForButtonDelegate _gameControllerGetBindForButton = LoadFunction<GameControllerGetBindForButtonDelegate>("SDL_GameControllerGetBindForButton");
        public static GameControllerButtonBind GameControllerGetBindForButton(GameController gameController, GameControllerButton button) => _gameControllerGetBindForButton(gameController, button);

        private delegate byte GameControllerGetButtonDelegate(GameController gameController, GameControllerButton button);
        private static GameControllerGetButtonDelegate _gameControllerGetButton = LoadFunction<GameControllerGetButtonDelegate>("SDL_GameControllerGetButton");
        public static byte GameControllerGetButton(GameController gameController, GameControllerButton button) => _gameControllerGetButton(gameController, button);

        private delegate void GameControllerCloseDelegate(GameController gameController);
        private static GameControllerCloseDelegate _gameControllerClose = LoadFunction<GameControllerCloseDelegate>("SDL_GameControllerClose");
        public static void GameControllerClose(GameController gameController) => _gameControllerClose(gameController);

        //
        // SDL_hints.h
        //
        
        public static bool SetHintWithPriority(string name, string value, HintPriority priority);

        
        public static bool SetHint(string name, string value);

        
        private static string GetHint(string name);

        
        public static bool GetHintBoolean(string name, bool defaultValue);

        
        public static void AddHintCallback(string name, HintCallback callback, IntPtr userData);

        
        public static void DelHintCallback(string name, HintCallback callback, IntPtr userData);

        
        public static void ClearHints();

        //
        // SDL_joystick.h
        //
        
        public static int NumJoysticks();

        
        public static string JoystickNameForIndex(int deviceIndex);

        
        public static Joystick JoystickOpen(int deviceIndex);

        
        public static Joystick JoystickFromInstanceID(JoystickID joystickID);

        
        public static string JoystickName(SDL2.Joystick joystick);

        
        public static Guid JoystickGetDeviceGUID(int deviceIndex);

        
        public static Guid JoystickGetGUID(SDL2.Joystick joystick);

        
        public static void JoystickGetGUIDString(Guid guid, string pszGUID, int cbGUID);

        
        public static Guid JoystickGetGUIDFromString(string pchGUID);

        
        public static bool JoystickGetAttached(SDL2.Joystick joystick);

        
        public static JoystickID JoystickInstanceID(SDL2.Joystick joystick);

        
        public static int JoystickNumAxes(SDL2.Joystick joystick);

        
        public static int JoystickNumBalls(SDL2.Joystick joystick);

        
        public static int JoystickNumHats(SDL2.Joystick joystick);

        
        public static int JoystickNumButtons(SDL2.Joystick joystick);

        
        public static void JoystickUpdate();

        
        public static int JoystickEventState(State state);

        
        public static short JoystickGetAxis(SDL2.Joystick joystick, JoystickAxis axis);

        
        public static JoystickHat JoystickGetHat(SDL2.Joystick joystick, int hat);

        
        public static int JoystickGetBall(SDL2.Joystick joystick, int ball, out int dx, out int dy);

        
        public static byte JoystickGetButton(SDL2.Joystick joystick, int button);

        
        public static void JoystickClose(SDL2.Joystick joystick);

        
        public static JoystickPowerLevel JoystickCurrentPowerLevel(SDL2.Joystick joystick);

        //
        // SDL_keyboard.h
        //
        
        public static Window GetKeyboardFocus();

        
        public static string GetKeyboardState(out int numkeys);

        
        public static KeyMod GetModState();

        
        public static void SetModState(KeyMod modstate);

        
        public static KeyCode GetKeyFromScancode(ScanCode scanCode);

        
        public static ScanCode GetScancodeFromKey(KeyCode key);

        
        public static string GetScancodeName(ScanCode scanCode);

        
        public static ScanCode GetScancodeFromName(string name);

        
        public static string GetKeyName(KeyCode key);

        
        public static KeyCode GetKeyFromName(string name);

        
        public static void StartstringInput();

        
        public static bool IsstringInputActive();

        
        public static void StopstringInput();

        
        public static void SetstringInputRect(ref Rect rectangle);

        
        public static bool HasScreenKeyboardSupport();

        
        public static bool IsScreenKeyboardShown(SDL2.Window window);

        //
        // SDL_log.h
        //
        
        public static void LogSetAllPriority(LogPriority priority);

        
        public static void LogSetPriority(LogCategory category, LogPriority priority);

        
        public static LogPriority LogGetPriority(LogCategory category);

        
        public static void LogResetPriorities();

        
        public static void Log(string fmt, params object[] objects);

        
        public static void LogVerbose(LogCategory category, string fmt, params object[] objects);

        
        public static void LogDebug(LogCategory category, string fmt, params object[] objects);

        
        public static void LogInfo(LogCategory category, string fmt, params object[] objects);

        
        public static void LogWarn(LogCategory category, string fmt, params object[] objects);

        
        public static void LogError(LogCategory category, string fmt, params object[] objects);

        
        public static void LogCritical(LogCategory category, string fmt, params object[] objects);

        
        public static void LogMessage(LogCategory category, LogPriority priority, string fmt, params object[] objects);

        
        public static void LogGetOutputFunction(LogOutputFunction callback, IntPtr userData);

        
        public static void LogSetOutputFunction(LogOutputFunction callback, IntPtr userData);

        //
        // SDL_messagebox.h
        //
        
        public static int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID);

        
        public static int ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message, SDL2.Window window);

        //
        // SDL_mouse.h
        //
        
        public static Window GetMouseFocus();

        
        public static MouseButtonState GetMouseState(out int x, out int y);

        
        public static MouseButtonState GetMouseState(out int x, int* y);

        
        public static MouseButtonState GetMouseState(int* x, out int y);

        
        public static MouseButtonState GetMouseState(int* x, int* y);

        
        public static MouseButtonState GetGlobalMouseState(out int x, out int y);

        
        public static MouseButtonState GetGlobalMouseState(out int x, int* y);

        
        public static MouseButtonState GetGlobalMouseState(int* x, out int y);

        
        public static MouseButtonState GetGlobalMouseState(int* x, int* y);

        
        public static MouseButtonState GetRelativeMouseState(out int x, out int y);

        
        public static MouseButtonState GetRelativeMouseState(out int x, int* y);

        
        public static MouseButtonState GetRelativeMouseState(int* x, out int y);

        
        public static MouseButtonState GetRelativeMouseState(int* x, int* y);

        
        public static void WarpMouseInWindow(SDL2.Window window, int x, int y);

        
        public static int WarpMouseGlobal(int x, int y);

        
        public static int SetRelativeMouseMode(bool enabled);

        
        public static int CaptureMouse(bool enabled);

        
        public static bool GetRelativeMouseMode();

        
        public static Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);

        
        public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY);

        
        public static Cursor CreateSystemCursor(SystemCursor id);

        
        public static void SetCursor(Cursor cursor);

        
        public static Cursor GetCursor();

        
        public static Cursor GetDefaultCursor();

        
        public static void FreeCursor(Cursor cursor);

        
        public static State ShowCursor(State toggle);

        //
        // SDL_pixels.h
        //
        
        public static string GetPixelFormatName(uint format);

        
        public static bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);

        
        public static uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);

        
        public static PixelFormat* AllocFormat(uint pixelFormat);

        
        public static void FreeFormat(ref PixelFormat pixelFormat);

        
        public static Palette AllocPalette(int numColors);

        
        public static int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette);

        
        public static int SetPaletteColors(Palette palette, Color[] colors, int firstColor, int numColors);

        
        public static void FreePalette(Palette palette);

        
        public static uint MapRGB(ref PixelFormat format, byte r, byte g, byte b);

        
        public static uint MapRGBA(ref PixelFormat format, byte r, byte g, byte b, byte a);

        
        public static void GetRGB(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);

        
        public static void GetRGBA(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);

        
        public static void CalculateGammaRamp(float gamma, out ushort[] ramp);

        //
        // SDL_platform.h
        //
        
        public static string GetPlatform();

        //
        // SDL_power.h
        //
        
        public static PowerState GetPowerInfo(out int seconds, out int percentage);

        //
        // SDL_rect.h
        //
        
        public static bool HasIntersection(ref Rect a, ref Rect b);

        
        public static bool IntersectRect(ref Rect a, ref Rect b, out Rect result);

        
        public static void UnionRect(ref Rect a, ref Rect b, out Rect result);

        
        public static bool EnclosePoints(Point[] points, int count, ref Rect clip, out Rect result);

        
        public static bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

        //
        // SDL_render.h
        //
        
        public static int GetNumRenderDrivers();

        
        public static int GetRenderDriverInfo(int index, out RendererInfo info);

        
        public static int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out SDL2.Window window, out Renderer renderer);

        
        public static Renderer CreateRenderer(SDL2.Window window, int index, RendererFlags flags);

        
        public static IntPtr CreateSoftwareRenderer(IntPtr surface);

        
        public static Renderer GetRenderer(SDL2.Window window);

        
        public static int GetRendererInfo(IntPtr renderer, out RendererInfo info);

        
        public static int GetRendererOutputSize(IntPtr renderer, out int w, out int h);

        
        public static IntPtr Createstringure(IntPtr renderer, uint format, int access, int w, int h);

        
        public static IntPtr CreatestringureFromSurface(IntPtr renderer, IntPtr surface);

        
        public static int Querystringure(IntPtr stringure, out uint format, out int access, out int w, out int h);

        
        public static int SetstringureColorMod(IntPtr stringure, byte r, byte g, byte b);

        
        public static int GetstringureColorMod(IntPtr stringure, out byte r, out byte g, out byte b);

        
        public static int SetstringureAlphaMod(IntPtr stringure, byte alpha);

        
        public static int GetstringureAlphaMod(IntPtr stringure, out byte alpha);

        
        public static int SetstringureBlendMode(IntPtr stringure, BlendMode blendMode);

        
        public static int GetstringureBlendMode(IntPtr stringure, out BlendMode blendMode);

        
        public static int Updatestringure(IntPtr stringure, ref Rect rect, IntPtr pixels, int pitch);

        
        public static int Updatestringure(IntPtr stringure, IntPtr rect, IntPtr pixels, int pitch);

        
        public static int Lockstringure(IntPtr stringure, ref Rect rect, out IntPtr pixels, out int pitch);

        
        public static int Lockstringure(IntPtr stringure, IntPtr rect, out IntPtr pixels, out int pitch);

        
        public static void Unlockstringure(IntPtr stringure);

        
        public static bool RenderTargetSupported(IntPtr renderer);

        
        public static int SetRenderTarget(IntPtr renderer, IntPtr stringure);

        
        public static IntPtr GetRenderTarget(IntPtr renderer);

        
        public static int RenderSetLogicalSize(IntPtr renderer, int w, int h);

        
        public static void RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

        
        public static int RenderSetIntegerScale(IntPtr renderer, bool enable);

        
        public static bool RenderGetIntegerScale(IntPtr renderer);

        
        public static int RenderSetViewport(IntPtr renderer, ref Rect rect);

        
        public static void RenderGetViewport(IntPtr renderer, out Rect rect);

        
        public static int RenderSetClipRect(IntPtr renderer, ref Rect rect);

        
        public static void RenderGetClipRect(IntPtr renderer, out Rect rect);

        
        public static bool RenderIsClipEnabled(IntPtr renderer);

        
        public static int RenderSetScale(IntPtr renderer, float scaleX, float scaleY);

        
        public static void RenderGetScale(IntPtr renderer, out float scaleX, out float scaleY);

        
        public static int SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        
        public static int GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

        
        public static int SetRenderDrawBlendMode(IntPtr renderer, BlendMode blendMode);

        
        public static int GetRenderDrawBlendMode(IntPtr renderer, out BlendMode blendMode);

        
        public static int RenderClear(IntPtr renderer);

        
        public static int RenderDrawPoint(IntPtr renderer, int x, int y);

        
        public static int RenderDrawPoints(IntPtr renderer, Point[] points, int count);

        
        public static int RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

        
        public static int RenderDrawLines(IntPtr renderer, Point[] points, int count);

        
        public static int RenderDrawRect(IntPtr renderer, ref Rect rect);

        
        public static int RenderDrawRect(IntPtr renderer, IntPtr rect);

        
        public static int RenderDrawRects(IntPtr renderer, Rect[] rects, int count);

        
        public static int RenderFillRect(IntPtr renderer, ref Rect rect);

        
        public static int RenderFillRect(IntPtr renderer, IntPtr rect);

        
        public static int RenderFillRects(IntPtr renderer, Rect[] rects, int count);

        
        public static int RenderCopy(IntPtr renderer, IntPtr stringure, ref Rect srcrect, ref Rect dstrect);

        
        public static int RenderCopy(IntPtr renderer, IntPtr stringure, IntPtr srcrect, ref Rect dstrect);

        
        public static int RenderCopy(IntPtr renderer, IntPtr stringure, ref Rect srcrect, IntPtr dstrect);

        
        public static int RenderCopy(IntPtr renderer, IntPtr stringure, IntPtr srcrect, IntPtr dstrect);

        
        public static int RenderCopyEx(IntPtr renderer, IntPtr stringure, ref Rect srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        
        public static int RenderCopyEx(IntPtr renderer, IntPtr stringure, IntPtr srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        
        public static int RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

        
        public static void RenderPresent(IntPtr renderer);

        
        public static void Destroystringure(IntPtr stringure);

        
        public static void DestroyRenderer(IntPtr renderer);

        
        public static int GL_Bindstringure(IntPtr stringure, out float texw, out float texh);

        
        public static int GL_Unbindstringure(IntPtr stringure);

        //
        // SDL_rwops.h
        //
        
        public static RWops RWFromFile(string file, string mode);

        
        public static int RWclose(IntPtr constring);

        
        public static int RWread(IntPtr constring, IntPtr ptr, int size, int maxNum);

        
        public static long RWsize(IntPtr constring);

        //
        // SDL_shape.h
        //
        
        public static Window CreateShapedWindow(byte title, uint x, uint y, uint w, uint h, WindowFlags flags);

        
        public static bool IsShapedWindow(SDL2.Window window);

        
        public static int SetWindowShape(SDL2.Window window, Surface* shape, WindowShape* shapeMode);

        
        public static int GetShapedWindowMode(SDL2.Window window, out WindowShape shapeMode);

        //
        // SDL_stdinc.h
        //
        
        public static IntPtr Malloc(Size size);

        
        public static IntPtr Calloc(Size nmemb, Size size);

        
        public static IntPtr Realloc(IntPtr mem, Size size);

        
        public static void Free(IntPtr mem);

        
        public static string Getenv(string name);

        
        public static int Setenv(string name, string value, int overwrite);

        
        public static void Qsort(IntPtr buffer, Size nmemb, Size size, IntPtr compare);

        //
        // SDL_surface.h
        //
        
        public static int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        
        public static int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        
        public static int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        
        public static int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        
        public static int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        
        public static int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        
        public static int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        
        public static int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        
        public static int ConvertPixels(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

        
        public static IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags);

        
        public static IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);

        
        public static IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        
        public static IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        
        public static IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

        
        public static IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);

        
        public static int FillRect(IntPtr dst, ref Rect rect, uint color);

        
        public static int FillRect(IntPtr dst, IntPtr rect, uint color);

        
        public static int FillRects(IntPtr dst, Rect[] rects, int count, uint color);

        
        public static void FreeSurface(IntPtr surface);

        
        public static void GetClipRect(IntPtr surface, out Rect rect);

        
        public static int GetColorKey(IntPtr surface, out uint key);

        
        public static int GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

        
        public static int GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);

        
        public static int GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

        
        private static IntPtr LoadBMP_RW(IntPtr src, int freesrc);

        
        public static int LockSurface(IntPtr surface);

        
        public static int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        
        public static int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        
        private static int SaveBMP_RW(IntPtr surface, IntPtr src, int freesrc);

        
        public static bool SetClipRect(IntPtr surface, ref Rect rect);

        
        public static int SetColorKey(IntPtr surface, int flag, uint key);

        
        public static int SetSurfaceAlphaMod(IntPtr surface, byte alpha);

        
        public static int SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);

        
        public static int SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

        
        public static int SetSurfacePalette(IntPtr surface, IntPtr palette);

        
        public static int SetSurfaceRLE(IntPtr surface, int flag);

        
        public static int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        
        public static void UnlockSurface(IntPtr surface);

        //
        // SDL_syswm.h
        //
        
        public static bool GetWindowWMInfo(SDL2.Window window, ref SysWMInfo info);

        //
        // SDL_timer.h
        //
        
        public static uint GetTicks();

        public static bool TicksPassed(uint a, uint b)
        {
            return ((int)(b - a) <= 0);
        }

        
        public static ulong GetPerformanceCounter();

        
        public static ulong GetPerformanceFrequency();

        
        public static void Delay(uint ms);

        
        public static TimerID AddTimer(uint interval, TimerCallback callback, IntPtr param);

        
        public static bool RemoveTimer(TimerID id);

        //
        // SDL_version.h
        //
        
        public static void GetVersion(out Version version);

        
        public static string GetRevision();

        
        public static int GetRevisionNumber();

        //
        // SDL_video.h
        //
        
        public static int GetNumVideoDrivers();

        
        private static IntPtr GetVideoDriver(int index);

        
        public static int VideoInit(IntPtr driver_name);

        
        public static void VideoQuit();

        
        public static IntPtr GetCurrentVideoDriver();

        
        public static int GetNumVideoDisplays();

        
        public static IntPtr GetDisplayName(int displayIndex);

        
        public static int GetDisplayBounds(int displayIndex, out Rect rectangle);

        
        public static int GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        
        public static int GetDisplayUsableBounds(int displayIndex, out Rect rectangle);

        
        public static int GetNumDisplayModes(int displayIndex);

        
        public static int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        
        public static int GetDeskTopDisplayMode(int displayIndex, out DisplayMode mode);

        
        public static int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static DisplayMode GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        
        public static int GetWindowDisplayIndex(SDL2.Window window);

        
        public static int SetWindowDisplayMode(SDL2.Window window, ref DisplayMode mode);

        
        public static int SetWindowDisplayMode(SDL2.Window window, IntPtr mode);

        
        public static int GetWindowDisplayMode(SDL2.Window window, out DisplayMode mode);

        
        public static uint GetWindowPixelFormat(SDL2.Window window);

        
        public static Window CreateWindow(string title, int x, int y, int width, int height, WindowFlags flags);

        
        public static Window CreateWindowFrom(IntPtr data);

        
        public static WindowID GetWindowID(SDL2.Window window);

        
        public static Window GetWindowFromID(WindowID id);

        
        public static WindowFlags GetWindowFlags(SDL2.Window window);

        
        public static void SetWindowTitle(SDL2.Window window, string title);

        
        public static string GetWindowTitle(SDL2.Window window);

        
        public static void SetWindowIcon(SDL2.Window window, Surface icon);

        
        public static IntPtr SetWindowData(SDL2.Window window, string name, IntPtr userData);

        
        public static IntPtr GetWindowData(SDL2.Window window, string name);

        
        public static void SetWindowPosition(SDL2.Window window, int x, int y);

        
        public static void GetWindowPosition(SDL2.Window window, out int x, out int y);

        
        public static void GetWindowPosition(SDL2.Window window, out int x, IntPtr y);

        
        public static void GetWindowPosition(SDL2.Window window, IntPtr x, out int y);

        
        public static void SetWindowSize(SDL2.Window window, int width, int height);

        
        public static void GetWindowSize(SDL2.Window window, out int width, out int height);

        
        public static void GetWindowSize(SDL2.Window window, out int width, IntPtr height);

        
        public static void GetWindowSize(SDL2.Window window, IntPtr width, out int height);

        
        public static int GetWindowBordersSize(SDL2.Window window, out int top, out int left, out int bottom, out int right);

        
        public static void SetWindowMinimumSize(SDL2.Window window, int minwidth, int minHeight);

        
        public static void GetWindowMinimumSize(SDL2.Window window, out int width, out int height);

        
        public static void SetWindowMaximumSize(SDL2.Window window, int maxWidth, int maxHeight);

        
        public static void GetWindowMaximumSize(SDL2.Window window, out int width, out int height);

        
        public static void SetWindowBordered(SDL2.Window window, bool bordered);

        
        public static void SetWindowResizable(SDL2.Window window, bool resizable);

        
        public static void ShowWindow(SDL2.Window window);

        
        public static void HideWindow(SDL2.Window window);

        
        public static void RaiseWindow(SDL2.Window window);

        
        public static void MaximizeWindow(SDL2.Window window);

        
        public static void MinimizeWindow(SDL2.Window window);

        
        public static void RestoreWindow(SDL2.Window window);

        
        public static int SetWindowFullscreen(SDL2.Window window, WindowFlags flags);

        
        public static Surface* GetWindowSurface(SDL2.Window window);

        
        public static int UpdateWindowSurface(SDL2.Window window);

        
        public static int UpdateWindowSurfaceRects(SDL2.Window window, Rect* rectangles, int numRectangles);

        
        public static void SetWindowGrab(SDL2.Window window, bool grabbed);

        
        public static bool GetWindowGrab(SDL2.Window window);

        
        public static Window GetGrabbedWindow();

        
        public static int SetWindowBrightness(SDL2.Window window, float brightness);

        
        public static float GetWindowBrightness(SDL2.Window window);

        
        public static int SetWindowOpacity(SDL2.Window window, float opacity);

        
        public static int GetWindowOpacity(SDL2.Window window, out float outOpacity);

        
        public static int SetWindowModalFor(SDL2.Window modalWindow, SDL2.Window parentWindow);

        
        public static int SetWindowInputFocus(SDL2.Window window);

        
        public static int SetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        
        public static int GetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        
        public static int SetWindowHitTest(SDL2.Window window, HitTest callback, IntPtr callbackData);

        
        public static void DestroyWindow(SDL2.Window window);

        
        public static bool IsScreenSaverEnabled();

        
        public static void EnableScreenSaver();

        
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