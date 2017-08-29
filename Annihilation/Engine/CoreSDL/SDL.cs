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

        public const string LibraryName = "dll";
        public const int ScanCodeMask = (1 << 30);
        public const int AudioCVTMaxFilters = 9;

        private static NativeLibrary LoadLibrary()
        {
            string name;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                name = "dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                name = "libSDL2-2.0.so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                name = "libdylib";
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

        // TODO: SDL_gesture.h
        // TODO: SDL_haptic.h

        //
        // SDL_hints.h
        //
        private delegate bool SetHintWithPriorityDelegate(string name, string value, HintPriority priority);
        private static SetHintWithPriorityDelegate _setHintWithPriority = LoadFunction<SetHintWithPriorityDelegate>("SDL_SetHintWithPriority");
        public static bool SetHintWithPriority(string name, string value, HintPriority priority) => _setHintWithPriority(name, value, priority);

        private delegate bool SetHintDelegate(string name, string value);
        private static SetHintDelegate _setHint = LoadFunction<SetHintDelegate>("SDL_SetHint");
        public static bool SetHint(string name, string value) => _setHint(name, value);

        private delegate string GetHintDelegate(string name);
        private static GetHintDelegate _getHint = LoadFunction<GetHintDelegate>("SDL_GetHint");
        private static string GetHint(string name) => _getHint(name);

        private delegate bool GetHintBooleanDelegate(string name, bool defaultValue);
        private static GetHintBooleanDelegate _getHintBoolean = LoadFunction<GetHintBooleanDelegate>("SDL_GetHintBoolean");
        public static bool GetHintBoolean(string name, bool defaultValue) => _getHintBoolean(name, defaultValue);

        private delegate void AddHintCallbackDelegate(string name, HintCallback callback, IntPtr userData);
        private static AddHintCallbackDelegate _addHintCallback = LoadFunction<AddHintCallbackDelegate>("SDL_AddHintCallback");
        public static void AddHintCallback(string name, HintCallback callback, IntPtr userData) => _addHintCallback(name, callback, userData);

        private delegate void DelHintCallbackDelegate(string name, HintCallback callback, IntPtr userData);
        private static DelHintCallbackDelegate _delHintCallback = LoadFunction<DelHintCallbackDelegate>("SDL_DelHintCallback");
        public static void DelHintCallback(string name, HintCallback callback, IntPtr userData) => _delHintCallback(name, callback, userData);

        private delegate void ClearHintsDelegate();
        private static ClearHintsDelegate _clearHints = LoadFunction<ClearHintsDelegate>("SDL_ClearHints");
        public static void ClearHints() => _clearHints();

        //
        // SDL_joystick.h
        //
        private delegate int NumJoysticksDelegate();
        private static NumJoysticksDelegate _numJoysticks = LoadFunction<NumJoysticksDelegate>("SDL_NumJoysticks");
        public static int NumJoysticks() => _numJoysticks();

        private delegate string JoystickNameForIndexDelegate(int deviceIndex);
        private static JoystickNameForIndexDelegate _joystickNameForIndex = LoadFunction<JoystickNameForIndexDelegate>("SDL_JoystickNameForIndex");
        public static string JoystickNameForIndex(int deviceIndex) => _joystickNameForIndex(deviceIndex);

        private delegate Guid JoystickGetDeviceGuidDelegate(int deviceIndex);
        private static JoystickGetDeviceGuidDelegate _joystickGetDeviceGuid = LoadFunction<JoystickGetDeviceGuidDelegate>("SDL_JoystickGetDeviceGUID");
        public static Guid JoystickGetDeviceGUID(int deviceIndex) => _joystickGetDeviceGuid(deviceIndex);

        private delegate ushort JoystickGetDeviceVendorDelegate(int deviceIndex);
        private static JoystickGetDeviceVendorDelegate _joystickGetDeviceVendor = LoadFunction<JoystickGetDeviceVendorDelegate>("SDL_JoystickGetDeviceVendor");
        public static ushort JoystickGetDeviceVendor(int deviceIndex) => _joystickGetDeviceVendor(deviceIndex);

        private delegate ushort JoystickGetDeviceProductDelegate(int deviceIndex);
        private static JoystickGetDeviceProductDelegate _joystickGetDeviceProduct = LoadFunction<JoystickGetDeviceProductDelegate>("SDL_JoystickGetDeviceProduct");
        public static ushort JoystickGetDeviceProduct(int deviceIndex) => _joystickGetDeviceProduct(deviceIndex);

        private delegate ushort JoystickGetDeviceProductVersionDelegate(int deviceIndex);
        private static JoystickGetDeviceProductVersionDelegate _joystickGetDeviceProductVersion = LoadFunction<JoystickGetDeviceProductVersionDelegate>("SDL_JoystickGetDeviceProductVersion");
        public static ushort JoystickGetDeviceProductVersion(int deviceIndex) => _joystickGetDeviceProductVersion(deviceIndex);

        private delegate JoystickType JoystickGetDeviceTypeDelegate(int deviceIndex);
        private static JoystickGetDeviceTypeDelegate _joystickGetDeviceType = LoadFunction<JoystickGetDeviceTypeDelegate>("SDL_JoystickGetDeviceType");
        public static JoystickType JoystickGetDeviceType(int deviceIndex) => _joystickGetDeviceType(deviceIndex);

        private delegate JoystickID JoystickGetDeviceInstanceIDDelegate(int deviceIndex);
        private static JoystickGetDeviceInstanceIDDelegate _joystickGetDeviceInstanceID = LoadFunction<JoystickGetDeviceInstanceIDDelegate>("SDL_JoystickGetDeviceInstanceID");
        public static JoystickID JoystickGetDeviceInstanceID(int deviceIndex) => _joystickGetDeviceInstanceID(deviceIndex);

        private delegate Joystick JoystickOpenDelegate(int deviceIndex);
        private static JoystickOpenDelegate _joystickOpen = LoadFunction<JoystickOpenDelegate>("SDL_JoystickOpen");
        public static Joystick JoystickOpen(int deviceIndex) => _joystickOpen(deviceIndex);

        private delegate Joystick JoystickFromInstanceIDDelegate(JoystickID joystickID);
        private static JoystickFromInstanceIDDelegate _joystickFromInstanceID = LoadFunction<JoystickFromInstanceIDDelegate>("SDL_JoystickFromInstanceID");
        public static Joystick JoystickFromInstanceID(JoystickID joystickID) => _joystickFromInstanceID(joystickID);

        private delegate string JoystickNameDelegate(Joystick joystick);
        private static JoystickNameDelegate _joystickName = LoadFunction<JoystickNameDelegate>("SDL_JoystickName");
        public static string JoystickName(Joystick joystick) => _joystickName(joystick);

        private delegate Guid JoystickGetGuidDelegate(Joystick joystick);
        private static JoystickGetGuidDelegate _joystickGetGuid = LoadFunction<JoystickGetGuidDelegate>("SDL_JoystickGetGUID");
        public static Guid JoystickGetGUID(Joystick joystick) => _joystickGetGuid(joystick);


        private delegate ushort JoystickGetVendorDelegate(Joystick joystick);
        private static JoystickGetVendorDelegate _joystickGetVendor = LoadFunction<JoystickGetVendorDelegate>("SDL_JoystickGetVendor");
        public static ushort JoystickGetVendor(Joystick joystick) => _joystickGetVendor(joystick);

        private delegate ushort JoystickGetProductDelegate(Joystick joystick);
        private static JoystickGetProductDelegate _joystickGetProduct = LoadFunction<JoystickGetProductDelegate>("SDL_JoystickGetProduct");
        public static ushort JoystickGetProduct(Joystick joystick) => _joystickGetProduct(joystick);

        private delegate ushort JoystickGetProductVersionDelegate(Joystick joystick);
        private static JoystickGetProductVersionDelegate _joystickGetProductVersion = LoadFunction<JoystickGetProductVersionDelegate>("SDL_JoystickGetProductVersion");
        public static ushort JoystickGetProductVersion(Joystick joystick) => _joystickGetProductVersion(joystick);

        private delegate JoystickType JoystickGetTypeDelegate(Joystick joystick);
        private static JoystickGetTypeDelegate _joystickGetType = LoadFunction<JoystickGetTypeDelegate>("SDL_JoystickGetType");
        public static JoystickType JoystickGetType(Joystick joystick) => _joystickGetType(joystick);

        private delegate void JoystickGetGuidStringDelegate(Guid guid, string pszGuid, int cbGuid);
        private static JoystickGetGuidStringDelegate _joystickGetGuidString = LoadFunction<JoystickGetGuidStringDelegate>("SDL_JoystickGetGUIDString");
        public static void JoystickGetGuidString(Guid guid, string pszGUID, int cbGUID) => _joystickGetGuidString(guid, pszGUID, cbGUID);

        private delegate Guid JoystickGetGuidFromStringDelegate(string pchGuid);
        private static JoystickGetGuidFromStringDelegate _joystickGetGuidFromString = LoadFunction<JoystickGetGuidFromStringDelegate>("SDL_JoystickGetGUIDFromString");
        public static Guid JoystickGetGUIDFromString(string pchGUID) => _joystickGetGuidFromString(pchGUID);

        private delegate bool JoystickGetAttachedDelegate(Joystick joystick);
        private static JoystickGetAttachedDelegate _joystickGetAttached = LoadFunction<JoystickGetAttachedDelegate>("SDL_JoystickGetAttached");
        public static bool JoystickGetAttached(Joystick joystick) => _joystickGetAttached(joystick);

        private delegate JoystickID JoystickInstanceIDDelegate(Joystick joystick);
        private static JoystickInstanceIDDelegate _joystickInstanceID = LoadFunction<JoystickInstanceIDDelegate>("SDL_JoystickInstanceID");
        public static JoystickID JoystickInstanceID(Joystick joystick) => _joystickInstanceID(joystick);

        private delegate int JoystickNumAxesDelegate(Joystick joystick);
        private static JoystickNumAxesDelegate _joystickNumAxes = LoadFunction<JoystickNumAxesDelegate>("SDL_JoystickNumAxes");
        public static int JoystickNumAxes(Joystick joystick) => _joystickNumAxes(joystick);

        private delegate int JoystickNumBallsDelegate(Joystick joystick);
        private static JoystickNumBallsDelegate _joystickNumBalls = LoadFunction<JoystickNumBallsDelegate>("SDL_JoystickNumBalls");
        public static int JoystickNumBalls(Joystick joystick) => _joystickNumBalls(joystick);

        private delegate int JoystickNumHatsDelegate(Joystick joystick);
        private static JoystickNumHatsDelegate _joystickNumHats = LoadFunction<JoystickNumHatsDelegate>("SDL_JoystickNumHats");
        public static int JoystickNumHats(Joystick joystick) => _joystickNumHats(joystick);

        private delegate int JoystickNumButtonsDelegate(Joystick joystick);
        private static JoystickNumButtonsDelegate _joystickNumButtons = LoadFunction<JoystickNumButtonsDelegate>("SDL_JoystickNumButtons");
        public static int JoystickNumButtons(Joystick joystick) => _joystickNumButtons(joystick);

        private delegate void JoystickUpdateDelegate();
        private static JoystickUpdateDelegate _joystickUpdate = LoadFunction<JoystickUpdateDelegate>("SDL_JoystickUpdate");
        public static void JoystickUpdate() => _joystickUpdate();

        private delegate State JoystickEventStateDelegate(State state);
        private static JoystickEventStateDelegate _joystickEventState = LoadFunction<JoystickEventStateDelegate>("SDL_JoystickEventState");
        public static State JoystickEventState(State state) => _joystickEventState(state);

        private delegate short JoystickGetAxisDelegate(Joystick joystick, JoystickAxis axis);
        private static JoystickGetAxisDelegate _joystickGetAxis = LoadFunction<JoystickGetAxisDelegate>("SDL_JoystickGetAxis");
        public static short JoystickGetAxis(Joystick joystick, JoystickAxis axis) => _joystickGetAxis(joystick, axis);

        private delegate bool JoystickGetAxisInitialStateDelegate(Joystick joystick, JoystickAxis axis, out short state);
        private static JoystickGetAxisInitialStateDelegate _joystickGetAxisInitialState = LoadFunction<JoystickGetAxisInitialStateDelegate>("SDL_JoystickGetAxisInitialState");
        public static bool JoystickGetAxisInitialState(Joystick joystick, JoystickAxis axis, out short state) => _joystickGetAxisInitialState(joystick, axis, out state);

        private delegate JoystickHat JoystickGetHatDelegate(Joystick joystick, int hat);
        private static JoystickGetHatDelegate _joystickGetHat = LoadFunction<JoystickGetHatDelegate>("SDL_JoystickGetHat");
        public static JoystickHat JoystickGetHat(Joystick joystick, int hat) => _joystickGetHat(joystick, hat);

        private delegate int JoystickGetBallDelegate(Joystick joystick, int ball, out int dx, out int dy);
        private static JoystickGetBallDelegate _joystickGetBall = LoadFunction<JoystickGetBallDelegate>("SDL_JoystickGetBallDelegate");
        public static int JoystickGetBall(Joystick joystick, int ball, out int dx, out int dy) => _joystickGetBall(joystick, ball, out dx, out dy);

        private delegate byte JoystickGetButtonDelegate(Joystick joystick, int button);
        private static JoystickGetButtonDelegate _joystickGetButton = LoadFunction<JoystickGetButtonDelegate>("SDL_JoystickGetButton");
        public static byte JoystickGetButton(Joystick joystick, int button) => _joystickGetButton(joystick, button);

        private delegate void JoystickCloseDelegte(Joystick joystick);
        private static JoystickCloseDelegte _joystickClose = LoadFunction<JoystickCloseDelegte>("SDL_JoystickClose");
        public static void JoystickClose(Joystick joystick) => _joystickClose(joystick);

        private delegate JoystickPowerLevel JoystickCurrentPowerLevelDelegate(Joystick joystick);
        private static JoystickCurrentPowerLevelDelegate _joystickCurrentPowerLevel = LoadFunction<JoystickCurrentPowerLevelDelegate>("SDL_JoystickCurrentPowerLevel");
        public static JoystickPowerLevel JoystickCurrentPowerLevel(Joystick joystick) => _joystickCurrentPowerLevel(joystick);

        //
        // SDL_keyboard.h
        //
        private delegate Window GetKeyboardFocusDelegate();
        private static GetKeyboardFocusDelegate _getKeyboardFocus = LoadFunction<GetKeyboardFocusDelegate>("SDL_GetKeyboardFocus");
        public static Window GetKeyboardFocus() => _getKeyboardFocus();

        private delegate string GetKeyboardStateDelegate(out int numKeys);
        private static GetKeyboardStateDelegate _getKeyboardState = LoadFunction<GetKeyboardStateDelegate>("SDL_GetKeyboardState");
        public static string GetKeyboardState(out int numkeys) => _getKeyboardState(out numkeys);

        private delegate KeyMod GetModStateDelegate();
        private static GetModStateDelegate _getModState = LoadFunction<GetModStateDelegate>("SDL_GetModState");
        public static KeyMod GetModState() => _getModState();

        private delegate void SetModStateDelegate(KeyMod modState);
        private static SetModStateDelegate _setModState = LoadFunction<SetModStateDelegate>("SDL_SetModState");
        public static void SetModState(KeyMod modState) => _setModState(modState);

        private delegate Keycode GetKeyFromScancodeDelegate(Scancode scancode);
        private static GetKeyFromScancodeDelegate _getKeyFromScancode = LoadFunction<GetKeyFromScancodeDelegate>("SDL_GetKeyFromScancode");
        public static Keycode GetKeyFromScancode(Scancode scancode) => _getKeyFromScancode(scancode);

        private delegate Scancode GetScancodeFromKeyDelegate(Keycode key);
        private static GetScancodeFromKeyDelegate _getScancodeFromKey = LoadFunction<GetScancodeFromKeyDelegate>("SDL_GetScancodeFromKey");
        public static Scancode GetScancodeFromKey(Keycode key) => _getScancodeFromKey(key);

        private delegate string GetScancodeNameDelegate(Scancode scancode);
        private static GetScancodeNameDelegate _getScancodeName = LoadFunction<GetScancodeNameDelegate>("SDL_GetScancodeName");
        public static string GetScancodeName(Scancode scancode) => _getScancodeName(scancode);

        private delegate Scancode GetScancodeFromNameDelegate(string name);
        private static GetScancodeFromNameDelegate _getScancodeFromName = LoadFunction<GetScancodeFromNameDelegate>("SDL_GetScancodeFromName");
        public static Scancode GetScancodeFromName(string name) => _getScancodeFromName(name);

        private delegate string GetKeyNameDelegate(Keycode key);
        private static GetKeyNameDelegate _getKeyName = LoadFunction<GetKeyNameDelegate>("SDL_GetKeyName");
        public static string GetKeyName(Keycode key) => _getKeyName(key);


        private delegate Keycode GetKeyFromNameDelegate(string name);
        private static GetKeyFromNameDelegate _getKeyFromName = LoadFunction<GetKeyFromNameDelegate>("SDL_GetKeyFromName");
        public static Keycode GetKeyFromName(string name) => _getKeyFromName(name);

        private delegate void StartTextInputDelegate();
        private static StartTextInputDelegate _startTextInput = LoadFunction<StartTextInputDelegate>("SDL_StartTextInput");
        public static void StartTextInput() => _startTextInput();

        private delegate bool IsTextInputActiveDelegate();
        private static IsTextInputActiveDelegate _isTextInputActive = LoadFunction<IsTextInputActiveDelegate>("SDL_IsTextInputActive");
        public static bool IsTextInputActive() => _isTextInputActive();

        private delegate void StopTextInputDelegate();
        private static StopTextInputDelegate _stopTextInput = LoadFunction<StopTextInputDelegate>("SDL_StopTextInput");
        public static void StopTextInput() => _stopTextInput();

        private delegate void SetTextInputRectDelegate(ref Rect rectangle);
        private static SetTextInputRectDelegate _setTextInputRect = LoadFunction<SetTextInputRectDelegate>("SDL_SetTextInputRect");
        public static void SetTextInputRect(ref Rect rectangle) => _setTextInputRect(ref rectangle);

        private delegate bool HasScreenKeyboardSupportDelegate();
        private static HasScreenKeyboardSupportDelegate _hasScreenKeyboardSupport = LoadFunction<HasScreenKeyboardSupportDelegate>("SDL_HasScreenKeyboardSupport");
        public static bool HasScreenKeyboardSupport() => _hasScreenKeyboardSupport();

        private delegate bool IsScreenKeyboardShownDelegate(Window window);
        private static IsScreenKeyboardShownDelegate _isScreenKeyboardShown = LoadFunction<IsScreenKeyboardShownDelegate>("SDL_IsScreenKeyboardShown");
        public static bool IsScreenKeyboardShown(Window window) => _isScreenKeyboardShown(window);

        //
        // SDL_loadso.h
        //
        private delegate IntPtr LoadObjectDelegate(string file);
        private static LoadObjectDelegate _loadObject = LoadFunction<LoadObjectDelegate>("SDL_LoadObject");
        public static IntPtr LoadObject(string file) => _loadObject(file);

        private delegate IntPtr LoadFunctionDelegate(IntPtr handle, string name);
        private static LoadFunctionDelegate _loadFunction = LoadFunction<LoadFunctionDelegate>("SDL_LoadFunction");
        public static IntPtr LoadFunction(IntPtr handle, string name) => _loadFunction(handle, name);

        private delegate void UnloadObjectDelegate(IntPtr handle);
        private static UnloadObjectDelegate _unloadObject = LoadFunction<UnloadObjectDelegate>("SDL_UnloadObject");
        public static void UnloadObject(IntPtr handle) => _unloadObject(handle);

        //
        // SDL_log.h
        //
        private delegate void LogSetAllPriorityDelegate(LogPriority priority);
        private static LogSetAllPriorityDelegate _logSetAllPriority = LoadFunction<LogSetAllPriorityDelegate>("SDL_LogSetAllPriority");
        public static void LogSetAllPriority(LogPriority priority) => _logSetAllPriority(priority);

        private delegate void LogSetPriorityDelegate(LogCategory category, LogPriority priority);
        private static LogSetPriorityDelegate _logSetPriority = LoadFunction<LogSetPriorityDelegate>("SDL_LogSetPriority");
        public static void LogSetPriority(LogCategory category, LogPriority priority) => _logSetPriority(category, priority);

        private delegate LogPriority LogGetPriorityDelegate(LogCategory category);
        private static LogGetPriorityDelegate _logGetPriority = LoadFunction<LogGetPriorityDelegate>("SDL_LogGetPriority");
        public static LogPriority LogGetPriority(LogCategory category) => _logGetPriority(category);

        private delegate void LogResetPrioritiesDelegate();
        private static LogResetPrioritiesDelegate _logResetPriorities = LoadFunction<LogResetPrioritiesDelegate>("SDL_LogResetPriorities");
        public static void LogResetPriorities() => _logResetPriorities();

        private delegate void LogDelegate(string fmt, params object[] objects);
        private static LogDelegate _log = LoadFunction<LogDelegate>("SDL_Log");
        public static void Log(string fmt, params object[] objects) => _log(fmt, objects);

        private delegate void LogVerboseDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogVerboseDelegate _logVerbose = LoadFunction<LogVerboseDelegate>("SDL_LogVerbose");
        public static void LogVerbose(LogCategory category, string fmt, params object[] objects) => _logVerbose(category, fmt, objects);

        private delegate void LogDebugDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogDebugDelegate _logDebug = LoadFunction<LogDebugDelegate>("SDL_LogDebug");
        public static void LogDebug(LogCategory category, string fmt, params object[] objects) => _logDebug(category, fmt, objects);

        private delegate void LogInfoDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogInfoDelegate _logInfo = LoadFunction<LogInfoDelegate>("SDL_LogInfo");
        public static void LogInfo(LogCategory category, string fmt, params object[] objects) => _logInfo(category, fmt, objects);

        private delegate void LogWarnDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogWarnDelegate _logWarn = LoadFunction<LogWarnDelegate>("SDL_LogWarn");
        public static void LogWarn(LogCategory category, string fmt, params object[] objects) => _logWarn(category, fmt, objects);

        private delegate void LogErrorDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogErrorDelegate _logError = LoadFunction<LogErrorDelegate>("SDL_LogError");
        public static void LogError(LogCategory category, string fmt, params object[] objects) => _logError(category, fmt, objects);

        private delegate void LogCriticalDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogCriticalDelegate _logCritical = LoadFunction<LogCriticalDelegate>("SDL_LogCritical");
        public static void LogCritical(LogCategory category, string fmt, params object[] objects) => _logCritical(category, fmt, objects);

        private delegate void LogMessageDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogMessageDelegate _logMessage = LoadFunction<LogMessageDelegate>("SDL_LogMessage");
        public static void LogMessage(LogCategory category, string fmt, params object[] objects) => _logMessage(category, fmt, objects);

        private delegate void LogGetOutputFunctionDelegate(LogOutputFunction callback, IntPtr userData);
        private static LogGetOutputFunctionDelegate _logGetOutputFunction = LoadFunction<LogGetOutputFunctionDelegate>("SDL_LogGetOutputFunction");
        public static void LogGetOutputFunction(LogOutputFunction callback, IntPtr userData) => _logGetOutputFunction(callback, userData);

        private delegate void LogSetOutputFunctionDelegate(LogOutputFunction callback, IntPtr userData);
        private static LogSetOutputFunctionDelegate _logSetOutputFunction = LoadFunction<LogSetOutputFunctionDelegate>("SDL_LogSetOutputFunction");
        public static void LogSetOutputFunction(LogOutputFunction callback, IntPtr userData) => _logSetOutputFunction(callback, userData);

        //
        // SDL_messagebox.h
        //
        private delegate int ShowMessageBoxDelegate(ref MessageBoxData messageBoxData, out int buttonID);
        private static ShowMessageBoxDelegate _showMessageBox = LoadFunction<ShowMessageBoxDelegate>("SDL_ShowMessageBox");
        public static int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID) => _showMessageBox(ref messageBoxData, out buttonID);

        private delegate int ShowSimpleMessageBoxDelegate(MessageBoxFlags flags, string title, string message, Window window);
        private static ShowSimpleMessageBoxDelegate _showSimpleMessageBox = LoadFunction<ShowSimpleMessageBoxDelegate>("SDL_ShowSimpleMessageBox");
        public static int ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message, Window window) => _showSimpleMessageBox(flags, title, message, window);

        //
        // SDL_mouse.h
        //
        private delegate Window GetMouseFocusDelegate();
        private static GetMouseFocusDelegate _getMouseFocus = LoadFunction<GetMouseFocusDelegate>("SDL_GetMouseFocus");
        public static Window GetMouseFocus() => _getMouseFocus();

        private delegate MouseButtonState GetMouseStateDelegate(out int? x, out int? y);
        private static GetMouseStateDelegate _getMouseState = LoadFunction<GetMouseStateDelegate>("SDL_GetMouseState");
        public static MouseButtonState GetMouseState(out int? x, out int? y) => _getMouseState(out x, out y);

        private delegate MouseButtonState GetGlobalMouseStateDelegate(out int? x, out int? y);
        private static GetGlobalMouseStateDelegate _getGlobalMouseState = LoadFunction<GetGlobalMouseStateDelegate>("SDL_GetGlobalMouseState");
        public static MouseButtonState GetGlobalMouseState(out int? x, out int? y) => _getGlobalMouseState(out x, out y);

        private delegate MouseButtonState GetRelativeMouseStateDelegate(out int? x, out int? y);
        private static GetRelativeMouseStateDelegate _getRelativeMouseState = LoadFunction<GetRelativeMouseStateDelegate>("SDL_GetRelativeMouseState");
        public static MouseButtonState GetRelativeMouseState(out int? x, out int? y) => _getRelativeMouseState(out x, out y);

        private delegate void WarpMouseInWindowDelegate(Window window, int x, int y);
        private static WarpMouseInWindowDelegate _warpMouseInWindow = LoadFunction<WarpMouseInWindowDelegate>("SDL_WarpMouseInWindow");
        public static void WarpMouseInWindow(Window window, int x, int y) => _warpMouseInWindow(window, x, y);

        private delegate int WarpMouseGlobalDelegate(int x, int y);
        private static WarpMouseGlobalDelegate _warpMouseGlobal = LoadFunction<WarpMouseGlobalDelegate>("SDL_WarpMouseGlobal");
        public static int WarpMouseGlobal(int x, int y) => _warpMouseGlobal(x, y);

        private delegate int SetRelativeMouseModeDelegate(bool enabled);
        private static SetRelativeMouseModeDelegate _setRelativeMouseMode = LoadFunction<SetRelativeMouseModeDelegate>("SDL_SetRelativeMouseMode");
        public static int SetRelativeMouseMode(bool enabled) => _setRelativeMouseMode(enabled);

        private delegate int CaptureMouseDelegate(bool enabled);
        private static CaptureMouseDelegate _captureMouse = LoadFunction<CaptureMouseDelegate>("SDL_CaptureMouse");
        public static int CaptureMouse(bool enabled) => _captureMouse(enabled);

        private delegate bool GetRelativeMouseModeDelegate();
        private static GetRelativeMouseModeDelegate _getRelativeMouseMode = LoadFunction<GetRelativeMouseModeDelegate>("SDL_GetRelativeMouseMode");
        public static bool GetRelativeMouseMode() => _getRelativeMouseMode();

        private delegate Cursor CreateCursorDelegate(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);
        private static CreateCursorDelegate _createCursor = LoadFunction<CreateCursorDelegate>("SDL_CreateCursor");
        public static Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY) => _createCursor(data, mask, w, h, hotX, hotY);

        private delegate Cursor CreateColorCursorDelegate(Surface surface, int hotX, int hotY);
        private static CreateColorCursorDelegate _createColorCursor = LoadFunction<CreateColorCursorDelegate>("SDL_CreateColorCursor");
        public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY) => _createColorCursor(surface, hotX, hotY);

        private delegate Cursor CreateSystemCursorDelegate(SystemCursor id);
        private static CreateSystemCursorDelegate _createSystemCursor = LoadFunction<CreateSystemCursorDelegate>("SDL_CreateSystemCursor");
        public static Cursor CreateSystemCursor(SystemCursor id) => _createSystemCursor(id);

        private delegate void SetCursorDelegate(Cursor cursor);
        private static SetCursorDelegate _setCursor = LoadFunction<SetCursorDelegate>("SDL_SetCursor");
        public static void SetCursor(Cursor cursor) => _setCursor(cursor);

        private delegate Cursor GetCursorDelegate();
        private static GetCursorDelegate _getCursor = LoadFunction<GetCursorDelegate>("SDL_GetCursor");
        public static Cursor GetCursor() => _getCursor();

        private delegate Cursor GetDefaultCursorDelegate();
        private static GetDefaultCursorDelegate _getDefaultCursor = LoadFunction<GetDefaultCursorDelegate>("SDL_GetDefaultCusror");
        public static Cursor GetDefaultCursor() => _getDefaultCursor();

        private delegate void FreeCursorDelegate(Cursor cursor);
        private static FreeCursorDelegate _freeCursor = LoadFunction<FreeCursorDelegate>("SDL_FreeCursor");
        public static void FreeCursor(Cursor cursor) => _freeCursor(cursor);

        private delegate State ShowCursorDelegate(State toggle);
        private static ShowCursorDelegate _showCursor = LoadFunction<ShowCursorDelegate>("SDL_ShowCursor");
        public static State ShowCursor(State toggle) => _showCursor(toggle);

        // TODO: SDL_mutex.h

        //
        // SDL_pixels.h
        //
        private delegate string GetPixelFormatNameDelegate(uint format);
        private static GetPixelFormatNameDelegate _getPixelFormatName = LoadFunction<GetPixelFormatNameDelegate>("SDL_GetPixelFormatName");
        public static string GetPixelFormatName(uint format) => _getPixelFormatName(format);

        private delegate bool PixelFormatEnumToMasksDelegate(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);
        private static PixelFormatEnumToMasksDelegate _pixelFormatEnumToMasks = LoadFunction<PixelFormatEnumToMasksDelegate>("SDL_PixelFormatEnumToMasks");
        public static bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask) => _pixelFormatEnumToMasks(format, out bpp, out rMask, out gMask, out bMask, out aMask);

        private delegate uint MasksToPixelFormatEnumDelegate(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);
        private static MasksToPixelFormatEnumDelegate _masksToPixelFormatEnum = LoadFunction<MasksToPixelFormatEnumDelegate>("SDL_MasksToPixelFormatEnum");
        public static uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask) => _masksToPixelFormatEnum(bpp, rMask, gMask, bMask, aMask);
        
        private delegate PixelFormat* AllocFormatDelegate(uint pixelFormat);
        private static AllocFormatDelegate _allocFormat = LoadFunction<AllocFormatDelegate>("SDL_AllocFormat");
        public static PixelFormat* AllocFormat(uint pixelFormat) => _allocFormat(pixelFormat);

        private delegate void FreeFormatDelegate(ref PixelFormat pixelFormat);
        private static FreeFormatDelegate _freeFormat = LoadFunction<FreeFormatDelegate>("SDL_FreeFormat");
        public static void FreeFormat(ref PixelFormat pixelFormat) => _freeFormat(ref pixelFormat);

        private delegate IntPtr AllocPaletteDelegate(int numColors);
        private static AllocPaletteDelegate _allocPalette = LoadFunction<AllocPaletteDelegate>("SDL_AllocPalette");
        public static IntPtr AllocPalette(int numColors) => _allocPalette(numColors);

        private delegate int SetPixelFormatPaletteDelegate(ref PixelFormat format, ref Palette palette);
        private static SetPixelFormatPaletteDelegate _setPixelFormatPalette = LoadFunction<SetPixelFormatPaletteDelegate>("SDL_SetPixelFormatPalette");
        public static int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette) => _setPixelFormatPalette(ref format, ref palette);

        private delegate int SetPaletteColorsDelegate(Palette palette, Color[] colors, int firstColor, int numColors);
        private static SetPaletteColorsDelegate _setPaletteColors = LoadFunction<SetPaletteColorsDelegate>("SDL_SetPaletteColors");
        public static int SetPaletteColors(Palette palette, Color[] colors, int firstColor, int numColors) => _setPaletteColors(palette, colors, firstColor, numColors);

        private delegate void FreePaletteDelegate(Palette palette);
        private static FreePaletteDelegate _freePalette = LoadFunction<FreePaletteDelegate>("SDL_FreePalette");
        public static void FreePalette(Palette palette) => _freePalette(palette);

        private delegate uint MapRgbDelegate(ref PixelFormat format, byte r, byte g, byte b);
        private static MapRgbDelegate _mapRgb = LoadFunction<MapRgbDelegate>("SDL_MapRGB");
        public static uint MapRgb(ref PixelFormat format, byte r, byte g, byte b) => _mapRgb(ref format, r, g, b);

        private delegate uint MapRgbaDelegate(ref PixelFormat format, byte r, byte g, byte b, byte a);
        private static MapRgbaDelegate _mapRgba = LoadFunction<MapRgbaDelegate>("SDL_MapRGBA");
        public static uint MapRgba(ref PixelFormat format, byte r, byte g, byte b, byte a) => _mapRgba(ref format, r, g, b, a);

        private delegate void GetRgbDelegate(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);
        private static GetRgbDelegate _getRgb = LoadFunction<GetRgbDelegate>("SDL_GetRGB");
        public static void GetRgb(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b) => _getRgb(pixel, ref format, out r, out g, out b);

        private delegate void GetRgbaDelegate(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);
        private static GetRgbaDelegate _getRgba = LoadFunction<GetRgbaDelegate>("SDL_GetRGBA");
        public static void GetRgba(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a) => _getRgba(pixel, ref format, out r, out g, out b, out a);

        private delegate void CalculateGammaRampDelegate(float gamma, out ushort[] ramp);
        private static CalculateGammaRampDelegate _calculateGammaRamp = LoadFunction<CalculateGammaRampDelegate>("SDL_CalculateGammaRamp");
        public static void CalculateGammaRamp(float gamma, out ushort[] ramp) => _calculateGammaRamp(gamma, out ramp);

        //
        // SDL_platform.h
        //
        private delegate string GetPlatformDelegate();
        private static GetPlatformDelegate _getPlatform = LoadFunction<GetPlatformDelegate>("SDL_GetPlatform");
        public static string GetPlatform() => _getPlatform();

        //
        // SDL_power.h
        //
        private delegate PowerState GetPowerInfoDelegate(out int seconds, out int percentage);
        private static GetPowerInfoDelegate _getPowerInfo = LoadFunction<GetPowerInfoDelegate>("SDL_GetPowerInfo");
        public static PowerState GetPowerInfo(out int seconds, out int percentage) => _getPowerInfo(out seconds, out percentage);

        //
        // SDL_rect.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInRect(ref Point p, ref Rect r)
        {
            return ((p.X >= r.X) && (p.X < (r.X + r.W)) &&
                    (p.Y >= r.Y) && (p.Y < (r.Y + r.H))) ? true : false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectEmpty(ref Rect r)
        {
            return ((r.W <= 0) || (r.H <= 0)) ? true : false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectEquals(ref Rect a, ref Rect b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) &&
                    (a.W == b.W) && (a.H == b.H)) ? true : false;
        }

        private delegate bool HasIntersectionDelegate(ref Rect a, ref Rect b);
        private static HasIntersectionDelegate _hasIntersection = LoadFunction<HasIntersectionDelegate>("SDL_HasIntersection");
        public static bool HasIntersection(ref Rect a, ref Rect b) => _hasIntersection(ref a, ref b);

        private delegate bool IntersectRectDelegate(ref Rect a, ref Rect b, out Rect result);
        private static IntersectRectDelegate _intersectRect = LoadFunction<IntersectRectDelegate>("SDL_IntersectRect");
        public static bool IntersectRect(ref Rect a, ref Rect b, out Rect result) => _intersectRect(ref a, ref b, out result);

        private delegate void UnionRectDelegate(ref Rect a, ref Rect b, out Rect result);
        private static UnionRectDelegate _unionRect = LoadFunction<UnionRectDelegate>("SDL_UnionRect");
        public static void UnionRect(ref Rect a, ref Rect b, out Rect result) => _unionRect(ref a, ref b, out result);

        private delegate bool EnclosePointsDelegate(Point[] points, int count, ref Rect clip, out Rect result);
        private static EnclosePointsDelegate _enclosePoints = LoadFunction<EnclosePointsDelegate>("SDL_EnclosePoints");
        public static bool EnclosePoints(Point[] points, int count, ref Rect clip, out Rect result) => _enclosePoints(points, count, ref clip, out result);

        private delegate bool IntersectRectAndLineDelegate(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);
        private static IntersectRectAndLineDelegate _intersectRectAndLine = LoadFunction<IntersectRectAndLineDelegate>("SDL_IntersectRectAndLine");
        public static bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2) => _intersectRectAndLine(ref rectangle, ref x1, ref y1, ref x2, ref y2);

        //
        // SDL_render.h
        //
        private delegate int GetNumRenderDriversDelegate();
        private static GetNumRenderDriversDelegate _getNumRenderDrivers = LoadFunction<GetNumRenderDriversDelegate>("SDL_GetNumRenderDrivers");
        public static int GetNumRenderDrivers() => _getNumRenderDrivers();

        private delegate int GetRenderDriverInfoDelegate(int index, out RendererInfo info);
        private static GetRenderDriverInfoDelegate _getRenderDriverInfo = LoadFunction<GetRenderDriverInfoDelegate>("SDL_GetRenderDriverInfo");
        public static int GetRenderDriverInfo(int index, out RendererInfo info) => _getRenderDriverInfo(index, out info);

        private delegate int CreateWindowAndRendererDelegate(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer);
        private static CreateWindowAndRendererDelegate _createWindowAndRenderer = LoadFunction<CreateWindowAndRendererDelegate>("SDL_CreateWindowAndRenderer");
        public static int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer) => _createWindowAndRenderer(width, height, windowFlags, out window, out renderer);

        
        public static Renderer CreateRenderer(Window window, int index, RendererFlags flags);

        
        public static IntPtr CreateSoftwareRenderer(IntPtr surface);

        
        public static Renderer GetRenderer(Window window);

        
        public static int GetRendererInfo(IntPtr renderer, out RendererInfo info);

        
        public static int GetRendererOutputSize(IntPtr renderer, out int w, out int h);

        
        public static IntPtr CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

        
        public static IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        
        public static int QueryTexture(IntPtr Texture, out uint format, out int access, out int w, out int h);

        
        public static int SetTextureColorMod(IntPtr Texture, byte r, byte g, byte b);

        
        public static int GetTextureColorMod(IntPtr Texture, out byte r, out byte g, out byte b);

        
        public static int SetTextureAlphaMod(IntPtr Texture, byte alpha);

        
        public static int GetTextureAlphaMod(IntPtr Texture, out byte alpha);

        
        public static int SetTextureBlendMode(IntPtr Texture, BlendMode blendMode);

        
        public static int GetTextureBlendMode(IntPtr Texture, out BlendMode blendMode);

        
        public static int UpdateTexture(IntPtr Texture, ref Rect? rect, IntPtr pixels, int pitch);

        SDL_UpdateYUVTexture

        public static int LockTexture(IntPtr Texture, ref Rect? rect, out IntPtr pixels, out int pitch);

        
        public static void UnlockTexture(IntPtr Texture);

        
        public static bool RenderTargetSupported(IntPtr renderer);

        
        public static int SetRenderTarget(IntPtr renderer, IntPtr Texture);

        
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

        
        public static int RenderCopy(IntPtr renderer, IntPtr Texture, ref Rect? srcrect, ref Rect? dstrect);
        

        public static int RenderCopyEx(IntPtr renderer, IntPtr Texture, ref Rect? srcrect, ref Rect? dstrect, double angle, ref Point center, RendererFlip flip);
        
        
        public static int RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

        
        public static void RenderPresent(IntPtr renderer);

        
        public static void DestroyTexture(IntPtr Texture);

        
        public static void DestroyRenderer(IntPtr renderer);

        // TODO: SDL_rwops.h

        //
        // SDL_shape.h
        //
        private delegate Window CreateShapedWindowDelegate(string title, uint x, uint y, uint w, uint h, WindowFlags flags);
        private static CreateShapedWindowDelegate _createShapedWindow = LoadFunction<CreateShapedWindowDelegate>("SDL_CreateShapedWindow");
        public static Window CreateShapedWindow(string title, uint x, uint y, uint w, uint h, WindowFlags flags) => _createShapedWindow(title, x, y, w, h, flags);

        private delegate bool IsShapedWindowDelegate(Window window);
        private static IsShapedWindowDelegate _isShapedWindow = LoadFunction<IsShapedWindowDelegate>("SDL_IsShapedWindow");
        public static bool IsShapedWindow(Window window) => _isShapedWindow(window);

        private delegate int SetWindowShapeDelegate(Window window, ref Surface shape, ref WindowShape shapeMode);
        private static SetWindowShapeDelegate _setWindowShape = LoadFunction<SetWindowShapeDelegate>("SDL_SetWindowShape");
        public static int SetWindowShape(Window window, ref Surface shape, ref WindowShape shapeMode) => _setWindowShape(window, ref shape, ref shapeMode);

        private delegate int GetShapedWindowModeDelegate(Window window, out WindowShape shapeMode);
        private static GetShapedWindowModeDelegate _getShapedWindowMode = LoadFunction<GetShapedWindowModeDelegate>("SDL_GetShapedWindowMode");
        public static int GetShapedWindowMode(Window window, out WindowShape shapeMode) => _getShapedWindowMode(window, out shapeMode);

        //
        // TODO: SDL_stdinc.h
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
        private delegate bool GetWindowWMInfoDelegate(Window window, out SysWMInfo info);
        private static GetWindowWMInfoDelegate _getWindowWMInfo = LoadFunction<GetWindowWMInfoDelegate>("SDL_GetWindowWMInfo");
        public static bool GetWindowWMInfo(Window window, out SysWMInfo info) => _getWindowWMInfo(window, out info);

        // TODO: SDL_thread.h

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

        // TODO: SDL_touch.h

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

        
        public static int GetWindowDisplayIndex(Window window);

        
        public static int SetWindowDisplayMode(Window window, ref DisplayMode mode);

        
        public static int SetWindowDisplayMode(Window window, IntPtr mode);

        
        public static int GetWindowDisplayMode(Window window, out DisplayMode mode);

        
        public static uint GetWindowPixelFormat(Window window);

        
        public static Window CreateWindow(string title, int x, int y, int width, int height, WindowFlags flags);

        
        public static Window CreateWindowFrom(IntPtr data);

        
        public static WindowID GetWindowID(Window window);

        
        public static Window GetWindowFromID(WindowID id);

        
        public static WindowFlags GetWindowFlags(Window window);

        
        public static void SetWindowTitle(Window window, string title);

        
        public static string GetWindowTitle(Window window);

        
        public static void SetWindowIcon(Window window, Surface icon);

        
        public static IntPtr SetWindowData(Window window, string name, IntPtr userData);

        
        public static IntPtr GetWindowData(Window window, string name);

        
        public static void SetWindowPosition(Window window, int x, int y);

        
        public static void GetWindowPosition(Window window, out int x, out int y);

        
        public static void GetWindowPosition(Window window, out int x, IntPtr y);

        
        public static void GetWindowPosition(Window window, IntPtr x, out int y);

        
        public static void SetWindowSize(Window window, int width, int height);

        
        public static void GetWindowSize(Window window, out int width, out int height);

        
        public static void GetWindowSize(Window window, out int width, IntPtr height);

        
        public static void GetWindowSize(Window window, IntPtr width, out int height);

        
        public static int GetWindowBordersSize(Window window, out int top, out int left, out int bottom, out int right);

        
        public static void SetWindowMinimumSize(Window window, int minwidth, int minHeight);

        
        public static void GetWindowMinimumSize(Window window, out int width, out int height);

        
        public static void SetWindowMaximumSize(Window window, int maxWidth, int maxHeight);

        
        public static void GetWindowMaximumSize(Window window, out int width, out int height);

        
        public static void SetWindowBordered(Window window, bool bordered);

        
        public static void SetWindowResizable(Window window, bool resizable);

        
        public static void ShowWindow(Window window);

        
        public static void HideWindow(Window window);

        
        public static void RaiseWindow(Window window);

        
        public static void MaximizeWindow(Window window);

        
        public static void MinimizeWindow(Window window);

        
        public static void RestoreWindow(Window window);

        
        public static int SetWindowFullscreen(Window window, WindowFlags flags);

        
        public static Surface* GetWindowSurface(Window window);

        
        public static int UpdateWindowSurface(Window window);

        
        public static int UpdateWindowSurfaceRects(Window window, Rect* rectangles, int numRectangles);

        
        public static void SetWindowGrab(Window window, bool grabbed);

        
        public static bool GetWindowGrab(Window window);

        
        public static Window GetGrabbedWindow();

        
        public static int SetWindowBrightness(Window window, float brightness);

        
        public static float GetWindowBrightness(Window window);

        
        public static int SetWindowOpacity(Window window, float opacity);

        
        public static int GetWindowOpacity(Window window, out float outOpacity);

        
        public static int SetWindowModalFor(Window modalWindow, Window parentWindow);

        
        public static int SetWindowInputFocus(Window window);

        
        public static int SetWindowGammaRamp(Window window, ushort* red, ushort* green, ushort* blue);

        
        public static int GetWindowGammaRamp(Window window, ushort* red, ushort* green, ushort* blue);

        
        public static int SetWindowHitTest(Window window, HitTest callback, IntPtr callbackData);

        
        public static void DestroyWindow(Window window);

        
        public static bool IsScreenSaverEnabled();

        
        public static void EnableScreenSaver();

        
        public static void DisableScreenSaver();

        // TODO: SDL_vulkan.h

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