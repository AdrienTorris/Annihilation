using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL2
{
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class SDL
    {
        public const string LibraryName = "SDL2.dll";
        public const string ImageLibraryName = "SDL2_image.dll";
        public const int ScanCodeMask = (1 << 30);

        public const int AudioCVTMaxFilters = 9;

        //
        // SDL.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_Init")]
        public static extern int Init(InitFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_InitSubSystem")]
        public static extern int InitSubSystem(InitFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_QuitSubSystem")]
        public static extern void QuitSubSystem(InitFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_WasInit")]
        public static extern InitFlags WasInit(InitFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_Quit")]
        public static extern void Quit();

        //
        // SDL_clipboard.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetClipboardText")]
        public static extern int SetClipboardText(Text text);

        [DllImport(LibraryName, EntryPoint = "SDL_GetClipboardText")]
        public static extern Text GetClipboardText();

        [DllImport(LibraryName, EntryPoint = "SDL_HasClipboardText")]
        public static extern bool HasClipboardText();

        //
        // SDL_cpuinfo.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetCPUCount")]
        public static extern int GetCPUCount();

        [DllImport(LibraryName, EntryPoint = "SDL_GetCPUCacheLineSize")]
        public static extern int GetCPUCacheLineSize();

        [DllImport(LibraryName, EntryPoint = "SDL_HasRDTSC")]
        public static extern bool HasRDTSC();

        [DllImport(LibraryName, EntryPoint = "SDL_HasAltiVec")]
        public static extern bool HasAltiVec();

        [DllImport(LibraryName, EntryPoint = "SDL_HasMMX")]
        public static extern bool HasMMX();

        [DllImport(LibraryName, EntryPoint = "SDL_Has3DNow")]
        public static extern bool Has3DNow();

        [DllImport(LibraryName, EntryPoint = "SDL_HasSSE")]
        public static extern bool HasSSE();

        [DllImport(LibraryName, EntryPoint = "SDL_HasSSE2")]
        public static extern bool HasSSE2();

        [DllImport(LibraryName, EntryPoint = "SDL_HasSSE3")]
        public static extern bool HasSSE3();

        [DllImport(LibraryName, EntryPoint = "SDL_HasSSE41")]
        public static extern bool HasSSE41();

        [DllImport(LibraryName, EntryPoint = "SDL_HasSSE42")]
        public static extern bool HasSSE42();

        [DllImport(LibraryName, EntryPoint = "SDL_HasAVX")]
        public static extern bool HasAVX();

        [DllImport(LibraryName, EntryPoint = "SDL_HasAVX2")]
        public static extern bool HasAVX2();

        [DllImport(LibraryName, EntryPoint = "SDL_HasNEON")]
        public static extern bool HasNEON();

        [DllImport(LibraryName, EntryPoint = "SDL_GetSystemRAM")]
        public static extern int GetSystemRAM();

        //
        // SDL_error.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetError")]
        public static extern int SetError(Text format, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_GetError")]
        public static extern Text GetError();

        [DllImport(LibraryName, EntryPoint = "SDL_ClearError")]
        public static extern void ClearError();

        //
        // SDL_events.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_PumpEvents")]
        public static extern void PumpEvents();

        [DllImport(LibraryName, EntryPoint = "SDL_PeepEvents")]
        public static extern int PeepEvents(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_HasEvent")]
        public static extern bool HasEvent(EventType type);

        [DllImport(LibraryName, EntryPoint = "SDL_HasEvents")]
        public static extern bool HasEvents(EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvent")]
        public static extern void FlushEvent(EventType type);

        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvents")]
        public static extern void FlushEvents(EventType minType, EventType maxType);

        [DllImport(LibraryName, EntryPoint = "SDL_PollEvent")]
        public static extern int PollEvent(out Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_WaitEvent")]
        public static extern int WaitEvent(out Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_WaitEventTimeout")]
        public static extern int WaitEventTimeout(out Event sdlEvent, int timeout);

        [DllImport(LibraryName, EntryPoint = "SDL_PushEvent")]
        public static extern int PushEvent(ref Event sdlEvent);

        [DllImport(LibraryName, EntryPoint = "SDL_SetEventFilter")]
        public static extern void SetEventFilter(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_GetEventFilter")]
        public static extern bool GetEventFilter(out EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_AddEventWatch")]
        public static extern void AddEventWatch(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_DelEventWatch")]
        public static extern void DelEventWatch(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_FilterEvents")]
        public static extern void FilterEvents(EventFilter filter, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_EventState")]
        public static extern byte EventState(EventType type, State state);

        [DllImport(LibraryName, EntryPoint = "SDL_RegisterEvents")]
        public static extern uint RegisterEvents(int numEvents);

        //
        // SDL_filesystem.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetBasePath")]
        public static extern Text GetBasePath();

        [DllImport(LibraryName, EntryPoint = "SDL_GetPrefPath")]
        public static extern Text GetPrefPath(Text org, Text app);

        //
        // SDL_gamecontroller.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
        public static extern int GameControllerAddMappingsFromRW(RWops rwOps, int freeRW);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMapping")]
        public static extern int GameControllerAddMapping(Text mappingString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNumMappings")]
        public static extern int GameControllerNumMappings();

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForGUID")]
        public static extern Text GameControllerMappingForGUID(Guid guid);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMapping")]
        public static extern Text GameControllerMapping(GameController gameController);

        [DllImport(LibraryName, EntryPoint = "SDL_IsGameController")]
        public static extern bool IsGameController(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNameForIndex")]
        public static extern Text GameControllerNameForIndex(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerOpen")]
        public static extern GameController GameControllerOpen(int joystickIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerFromInstanceID")]
        public static extern GameController GameControllerFromInstanceID(JoystickID joyid);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerName")]
        public static extern Text GameControllerName(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAttached")]
        public static extern bool GameControllerGetAttached(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetJoystick")]
        public static extern Joystick GameControllerGetJoystick(GameController gamecontroller);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerEventState")]
        public static extern int GameControllerEventState(State state);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerUpdate")]
        public static extern void GameControllerUpdate();

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxisFromString")]
        public static extern GameControllerAxis GameControllerGetAxisFromString(Text pchString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetStringForAxis")]
        public static extern Text GameControllerGetStringForAxis(GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForAxis")]
        public static extern GameControllerButtonBind GameControllerGetBindForAxis(GameController gamecontroller, GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxis")]
        public static extern short GameControllerGetAxis(GameController gamecontroller, GameControllerAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButtonFromString")]
        public static extern GameControllerButton GameControllerGetButtonFromString(Text pchString);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetStringForButton")]
        public static extern Text GameControllerGetStringForButton(GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForButton")]
        public static extern GameControllerButtonBind GameControllerGetBindForButton(GameController gamecontroller, GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButton")]
        public static extern byte GameControllerGetButton(GameController gamecontroller, GameControllerButton button);

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerClose")]
        public static extern void GameControllerClose(GameController gamecontroller);

        //
        // SDL_hints.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetHintWithPriority")]
        public static extern bool SetHintWithPriority(Text name, Text value, HintPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_SetHint")]
        public static extern bool SetHint(Text name, Text value);

        [DllImport(LibraryName, EntryPoint = "SDL_GetHint")]
        private static extern Text GetHint(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_GetHintBoolean")]
        public static extern bool GetHintBoolean(Text name, bool defaultValue);

        [DllImport(LibraryName, EntryPoint = "SDL_AddHintCallback")]
        public static extern void AddHintCallback(Text name, HintCallback callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_DelHintCallback")]
        public static extern void DelHintCallback(Text name, HintCallback callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_ClearHints")]
        public static extern void ClearHints();

        //
        // SDL_joystick.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_NumJoysticks")]
        public static extern int NumJoysticks();

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNameForIndex")]
        public static extern Text JoystickNameForIndex(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickOpen")]
        public static extern Joystick JoystickOpen(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickFromInstanceID")]
        public static extern Joystick JoystickFromInstanceID(JoystickID joystickID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickName")]
        public static extern Text JoystickName(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceGUID")]
        public static extern Guid JoystickGetDeviceGUID(int deviceIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUID")]
        public static extern Guid JoystickGetGUID(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUIDString")]
        public static extern void JoystickGetGUIDString(Guid guid, Text pszGUID, int cbGUID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGUIDFromString")]
        public static extern Guid JoystickGetGUIDFromString(Text pchGUID);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAttached")]
        public static extern bool JoystickGetAttached(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickInstanceID")]
        public static extern JoystickID JoystickInstanceID(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumAxes")]
        public static extern int JoystickNumAxes(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumBalls")]
        public static extern int JoystickNumBalls(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumHats")]
        public static extern int JoystickNumHats(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumButtons")]
        public static extern int JoystickNumButtons(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickUpdate")]
        public static extern void JoystickUpdate();

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickEventState")]
        public static extern int JoystickEventState(State state);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAxis")]
        public static extern short JoystickGetAxis(SDL2.Joystick joystick, JoystickAxis axis);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetHat")]
        public static extern JoystickHat JoystickGetHat(SDL2.Joystick joystick, int hat);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetBall")]
        public static extern int JoystickGetBall(SDL2.Joystick joystick, int ball, out int dx, out int dy);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetButton")]
        public static extern byte JoystickGetButton(SDL2.Joystick joystick, int button);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickClose")]
        public static extern void JoystickClose(SDL2.Joystick joystick);

        [DllImport(LibraryName, EntryPoint = "SDL_JoystickCurrentPowerLevel")]
        public static extern JoystickPowerLevel JoystickCurrentPowerLevel(SDL2.Joystick joystick);

        //
        // SDL_keyboard.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardFocus")]
        public static extern Window GetKeyboardFocus();

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardState")]
        public static extern Text GetKeyboardState(out int numkeys);

        [DllImport(LibraryName, EntryPoint = "SDL_GetModState")]
        public static extern KeyMod GetModState();

        [DllImport(LibraryName, EntryPoint = "SDL_SetModState")]
        public static extern void SetModState(KeyMod modstate);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromScancode")]
        public static extern KeyCode GetKeyFromScancode(ScanCode scanCode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromKey")]
        public static extern ScanCode GetScancodeFromKey(KeyCode key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeName")]
        public static extern Text GetScancodeName(ScanCode scanCode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromName")]
        public static extern ScanCode GetScancodeFromName(string name);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyName")]
        public static extern Text GetKeyName(KeyCode key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromName")]
        public static extern KeyCode GetKeyFromName(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_StartTextInput")]
        public static extern void StartTextInput();

        [DllImport(LibraryName, EntryPoint = "SDL_IsTextInputActive")]
        public static extern bool IsTextInputActive();

        [DllImport(LibraryName, EntryPoint = "SDL_StopTextInput")]
        public static extern void StopTextInput();

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextInputRect")]
        public static extern void SetTextInputRect(ref Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_HasScreenKeyboardSupport")]
        public static extern bool HasScreenKeyboardSupport();

        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenKeyboardShown")]
        public static extern bool IsScreenKeyboardShown(SDL2.Window window);

        //
        // SDL_log.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_LogSetAllPriority")]
        public static extern void LogSetAllPriority(LogPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_LogSetPriority")]
        public static extern void LogSetPriority(LogCategory category, LogPriority priority);

        [DllImport(LibraryName, EntryPoint = "SDL_LogGetPriority")]
        public static extern LogPriority LogGetPriority(LogCategory category);

        [DllImport(LibraryName, EntryPoint = "SDL_LogResetPriorities")]
        public static extern void LogResetPriorities();

        [DllImport(LibraryName, EntryPoint = "SDL_Log")]
        public static extern void Log(Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogVerbose")]
        public static extern void LogVerbose(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogDebug")]
        public static extern void LogDebug(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogInfo")]
        public static extern void LogInfo(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogWarn")]
        public static extern void LogWarn(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogError")]
        public static extern void LogError(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogCritical")]
        public static extern void LogCritical(LogCategory category, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogMessage")]
        public static extern void LogMessage(LogCategory category, LogPriority priority, Text fmt, params object[] objects);

        [DllImport(LibraryName, EntryPoint = "SDL_LogGetOutputFunction")]
        public static extern void LogGetOutputFunction(LogOutputFunction callback, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_LogSetOutputFunction")]
        public static extern void LogSetOutputFunction(LogOutputFunction callback, void* userData);

        //
        // SDL_messagebox.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_ShowMessageBox")]
        public static extern int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowSimpleMessageBox")]
        public static extern int ShowSimpleMessageBox(MessageBoxFlags flags, Text title, Text message, SDL2.Window window);

        //
        // SDL_mouse.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseFocus")]
        public static extern Window GetMouseFocus();

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static extern MouseButtonState GetMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static extern MouseButtonState GetMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static extern MouseButtonState GetMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static extern MouseButtonState GetMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static extern MouseButtonState GetGlobalMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static extern MouseButtonState GetGlobalMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static extern MouseButtonState GetGlobalMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static extern MouseButtonState GetGlobalMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static extern MouseButtonState GetRelativeMouseState(out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static extern MouseButtonState GetRelativeMouseState(out int x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static extern MouseButtonState GetRelativeMouseState(int* x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static extern MouseButtonState GetRelativeMouseState(int* x, int* y);

        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseInWindow")]
        public static extern void WarpMouseInWindow(SDL2.Window window, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseGlobal")]
        public static extern int WarpMouseGlobal(int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRelativeMouseMode")]
        public static extern int SetRelativeMouseMode(bool enabled);

        [DllImport(LibraryName, EntryPoint = "SDL_CaptureMouse")]
        public static extern int CaptureMouse(bool enabled);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseMode")]
        public static extern bool GetRelativeMouseMode();

        [DllImport(LibraryName, EntryPoint = "SDL_CreateCursor")]
        public static extern Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateColorCursor")]
        public static extern Cursor CreateColorCursor(Surface surface, int hotX, int hotY);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateSystemCursor")]
        public static extern Cursor CreateSystemCursor(SystemCursor id);

        [DllImport(LibraryName, EntryPoint = "SDL_SetCursor")]
        public static extern void SetCursor(Cursor cursor);

        [DllImport(LibraryName, EntryPoint = "SDL_GetCursor")]
        public static extern Cursor GetCursor();

        [DllImport(LibraryName, EntryPoint = "SDL_GetDefaultCursor")]
        public static extern Cursor GetDefaultCursor();

        [DllImport(LibraryName, EntryPoint = "SDL_FreeCursor")]
        public static extern void FreeCursor(Cursor cursor);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowCursor")]
        public static extern State ShowCursor(State toggle);

        //
        // SDL_pixels.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPixelFormatName")]
        public static extern Text GetPixelFormatName(uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_PixelFormatEnumToMasks")]
        public static extern bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);

        [DllImport(LibraryName, EntryPoint = "SDL_MasksToPixelFormatEnum")]
        public static extern uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);

        [DllImport(LibraryName, EntryPoint = "SDL_AllocFormat")]
        public static extern PixelFormat* AllocFormat(uint pixelFormat);

        [DllImport(LibraryName, EntryPoint = "SDL_FreeFormat")]
        public static extern void FreeFormat(ref PixelFormat pixelFormat);

        [DllImport(LibraryName, EntryPoint = "SDL_AllocPalette")]
        public static extern Palette AllocPalette(int numColors);

        [DllImport(LibraryName, EntryPoint = "SDL_SetPixelFormatPalette")]
        public static extern int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette);

        [DllImport(LibraryName, EntryPoint = "SDL_SetPaletteColors")]
        public static extern int SetPaletteColors(Palette palette, Color[] colors, int firstColor, int numColors);

        [DllImport(LibraryName, EntryPoint = "SDL_FreePalette")]
        public static extern void FreePalette(Palette palette);

        [DllImport(LibraryName, EntryPoint = "SDL_MapRGB")]
        public static extern uint MapRGB(ref PixelFormat format, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_MapRGBA")]
        public static extern uint MapRGBA(ref PixelFormat format, byte r, byte g, byte b, byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRGB")]
        public static extern void GetRGB(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRGBA")]
        public static extern void GetRGBA(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_CalculateGammaRamp")]
        public static extern void CalculateGammaRamp(float gamma, out ushort[] ramp);

        //
        // SDL_platform.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPlatform")]
        public static extern Text GetPlatform();

        //
        // SDL_power.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPowerInfo")]
        public static extern PowerState GetPowerInfo(out int seconds, out int percentage);

        //
        // SDL_rect.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_HasIntersection")]
        public static extern bool HasIntersection(ref Rect a, ref Rect b);

        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRect")]
        public static extern bool IntersectRect(ref Rect a, ref Rect b, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_UnionRect")]
        public static extern void UnionRect(ref Rect a, ref Rect b, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_EnclosePoints")]
        public static extern bool EnclosePoints(Point[] points, int count, ref Rect clip, out Rect result);

        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine")]
        public static extern bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

        //
        // SDL_render.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetNumRenderDrivers")]
        public static extern int GetNumRenderDrivers();

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDriverInfo")]
        public static extern int GetRenderDriverInfo(int index, out RendererInfo info);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowAndRenderer")]
        public static extern int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out SDL2.Window window, out Renderer renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRenderer")]
        public static extern Renderer CreateRenderer(SDL2.Window window, int index, RendererFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateSoftwareRenderer")]
        public static extern IntPtr CreateSoftwareRenderer(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderer")]
        public static extern Renderer GetRenderer(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererInfo")]
        public static extern int GetRendererInfo(IntPtr renderer, out RendererInfo info);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererOutputSize")]
        public static extern int GetRendererOutputSize(IntPtr renderer, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateTexture")]
        public static extern IntPtr CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateTextureFromSurface")]
        public static extern IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_QueryTexture")]
        public static extern int QueryTexture(IntPtr texture, out uint format, out int access, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureColorMod")]
        public static extern int SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureColorMod")]
        public static extern int GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureAlphaMod")]
        public static extern int SetTextureAlphaMod(IntPtr texture, byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureAlphaMod")]
        public static extern int GetTextureAlphaMod(IntPtr texture, out byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureBlendMode")]
        public static extern int SetTextureBlendMode(IntPtr texture, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureBlendMode")]
        public static extern int GetTextureBlendMode(IntPtr texture, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateTexture")]
        public static extern int UpdateTexture(IntPtr texture, ref Rect rect, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateTexture")]
        public static extern int UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_LockTexture")]
        public static extern int LockTexture(IntPtr texture, ref Rect rect, out IntPtr pixels, out int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_LockTexture")]
        public static extern int LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_UnlockTexture")]
        public static extern void UnlockTexture(IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderTargetSupported")]
        public static extern bool RenderTargetSupported(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderTarget")]
        public static extern int SetRenderTarget(IntPtr renderer, IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderTarget")]
        public static extern IntPtr GetRenderTarget(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetLogicalSize")]
        public static extern int RenderSetLogicalSize(IntPtr renderer, int w, int h);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetLogicalSize")]
        public static extern void RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetIntegerScale")]
        public static extern int RenderSetIntegerScale(IntPtr renderer, bool enable);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetIntegerScale")]
        public static extern bool RenderGetIntegerScale(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetViewport")]
        public static extern int RenderSetViewport(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetViewport")]
        public static extern void RenderGetViewport(IntPtr renderer, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetClipRect")]
        public static extern int RenderSetClipRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetClipRect")]
        public static extern void RenderGetClipRect(IntPtr renderer, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderIsClipEnabled")]
        public static extern bool RenderIsClipEnabled(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetScale")]
        public static extern int RenderSetScale(IntPtr renderer, float scaleX, float scaleY);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetScale")]
        public static extern void RenderGetScale(IntPtr renderer, out float scaleX, out float scaleY);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawColor")]
        public static extern int SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawColor")]
        public static extern int GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawBlendMode")]
        public static extern int SetRenderDrawBlendMode(IntPtr renderer, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawBlendMode")]
        public static extern int GetRenderDrawBlendMode(IntPtr renderer, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderClear")]
        public static extern int RenderClear(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoint")]
        public static extern int RenderDrawPoint(IntPtr renderer, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoints")]
        public static extern int RenderDrawPoints(IntPtr renderer, Point[] points, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLine")]
        public static extern int RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLines")]
        public static extern int RenderDrawLines(IntPtr renderer, Point[] points, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRect")]
        public static extern int RenderDrawRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRect")]
        public static extern int RenderDrawRect(IntPtr renderer, IntPtr rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRects")]
        public static extern int RenderDrawRects(IntPtr renderer, Rect[] rects, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRect")]
        public static extern int RenderFillRect(IntPtr renderer, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRect")]
        public static extern int RenderFillRect(IntPtr renderer, IntPtr rect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRects")]
        public static extern int RenderFillRects(IntPtr renderer, Rect[] rects, int count);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static extern int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static extern int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopyEx")]
        public static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopyEx")]
        public static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderReadPixels")]
        public static extern int RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_RenderPresent")]
        public static extern void RenderPresent(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyTexture")]
        public static extern void DestroyTexture(IntPtr texture);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyRenderer")]
        public static extern void DestroyRenderer(IntPtr renderer);

        [DllImport(LibraryName, EntryPoint = "SDL_GL_BindTexture")]
        public static extern int GL_BindTexture(IntPtr texture, out float texw, out float texh);

        [DllImport(LibraryName, EntryPoint = "SDL_GL_UnbindTexture")]
        public static extern int GL_UnbindTexture(IntPtr texture);

        //
        // SDL_rwops.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_RWFromFile")]
        public static extern IntPtr RWFromFile(string file, string mode);

        [DllImport(LibraryName, EntryPoint = "SDL_RWclose")]
        public static extern int RWclose(IntPtr context);

        [DllImport(LibraryName, EntryPoint = "SDL_RWread")]
        public static extern int RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

        [DllImport(LibraryName, EntryPoint = "SDL_RWsize")]
        public static extern long RWsize(IntPtr context);

        //
        // SDL_shape.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_CreateShapedWindow")]
        public static extern Window CreateShapedWindow(byte title, uint x, uint y, uint w, uint h, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_IsShapedWindow")]
        public static extern bool IsShapedWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowShape")]
        public static extern int SetWindowShape(SDL2.Window window, Surface* shape, WindowShape* shapeMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetShapedWindowMode")]
        public static extern int GetShapedWindowMode(SDL2.Window window, out WindowShape shapeMode);

        //
        // SDL_stdinc.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_malloc")]
        public static extern void* Malloc(Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_calloc")]
        public static extern void* Calloc(Size nmemb, Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_realloc")]
        public static extern void* Realloc(void* mem, Size size);

        [DllImport(LibraryName, EntryPoint = "SDL_free")]
        public static extern void Free(void* mem);

        [DllImport(LibraryName, EntryPoint = "SDL_getenv")]
        public static extern Text Getenv(Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_setenv")]
        public static extern int Setenv(Text name, Text value, int overwrite);

        [DllImport(LibraryName, EntryPoint = "SDL_qsort")]
        public static extern void Qsort(void* buffer, Size nmemb, Size size, IntPtr compare);

        //
        // SDL_surface.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static extern int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static extern int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static extern int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlit")]
        public static extern int UpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static extern int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static extern int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static extern int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UpperBlitScaled")]
        public static extern int UpperBlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertPixels")]
        public static extern int ConvertPixels(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertSurface")]
        public static extern IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags);

        [DllImport(LibraryName, EntryPoint = "SDL_ConvertSurfaceFormat")]
        public static extern IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurface")]
        public static extern IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
        public static extern IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormat")]
        public static extern IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateRGBSurfaceWithFormatFrom")]
        public static extern IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRect")]
        public static extern int FillRect(IntPtr dst, ref Rect rect, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRect")]
        public static extern int FillRect(IntPtr dst, IntPtr rect, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FillRects")]
        public static extern int FillRects(IntPtr dst, Rect[] rects, int count, uint color);

        [DllImport(LibraryName, EntryPoint = "SDL_FreeSurface")]
        public static extern void FreeSurface(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_GetClipRect")]
        public static extern void GetClipRect(IntPtr surface, out Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_GetColorKey")]
        public static extern int GetColorKey(IntPtr surface, out uint key);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
        public static extern int GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceBlendMode")]
        public static extern int GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetSurfaceColorMod")]
        public static extern int GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_LoadBMP_RW")]
        private static extern IntPtr LoadBMP_RW(IntPtr src, int freesrc);

        [DllImport(LibraryName, EntryPoint = "SDL_LockSurface")]
        public static extern int LockSurface(IntPtr surface);

        [DllImport(LibraryName, EntryPoint = "SDL_LowerBlit")]
        public static extern int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_LowerBlitScaled")]
        public static extern int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_SaveBMP_RW")]
        private static extern int SaveBMP_RW(IntPtr surface, IntPtr src, int freesrc);

        [DllImport(LibraryName, EntryPoint = "SDL_SetClipRect")]
        public static extern bool SetClipRect(IntPtr surface, ref Rect rect);

        [DllImport(LibraryName, EntryPoint = "SDL_SetColorKey")]
        public static extern int SetColorKey(IntPtr surface, int flag, uint key);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
        public static extern int SetSurfaceAlphaMod(IntPtr surface, byte alpha);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceBlendMode")]
        public static extern int SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceColorMod")]
        public static extern int SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfacePalette")]
        public static extern int SetSurfacePalette(IntPtr surface, IntPtr palette);

        [DllImport(LibraryName, EntryPoint = "SDL_SetSurfaceRLE")]
        public static extern int SetSurfaceRLE(IntPtr surface, int flag);

        [DllImport(LibraryName, EntryPoint = "SDL_SoftStretch")]
        public static extern int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LibraryName, EntryPoint = "SDL_UnlockSurface")]
        public static extern void UnlockSurface(IntPtr surface);

        //
        // SDL_syswm.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowWMInfo")]
        public static extern bool GetWindowWMInfo(SDL2.Window window, ref SysWMInfo info);

        //
        // SDL_timer.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetTicks")]
        public static extern uint GetTicks();

        public static bool TicksPassed(uint a, uint b)
        {
            return ((int)(b - a) <= 0);
        }

        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceCounter")]
        public static extern ulong GetPerformanceCounter();

        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceFrequency")]
        public static extern ulong GetPerformanceFrequency();

        [DllImport(LibraryName, EntryPoint = "SDL_Delay")]
        public static extern void Delay(uint ms);

        [DllImport(LibraryName, EntryPoint = "SDL_AddTimer")]
        public static extern TimerID AddTimer(uint interval, TimerCallback callback, IntPtr param);

        [DllImport(LibraryName, EntryPoint = "SDL_RemoveTimer")]
        public static extern bool RemoveTimer(TimerID id);

        //
        // SDL_version.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetVersion")]
        public static extern void GetVersion(out Version version);

        [DllImport(LibraryName, EntryPoint = "SDL_GetRevision")]
        public static extern Text GetRevision();

        [DllImport(LibraryName, EntryPoint = "SDL_GetRevisionNumber")]
        public static extern int GetRevisionNumber();

        //
        // SDL_video.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDrivers")]
        public static extern int GetNumVideoDrivers();

        [DllImport(LibraryName, EntryPoint = "SDL_GetVideoDriver")]
        private static extern IntPtr GetVideoDriver(int index);

        [DllImport(LibraryName, EntryPoint = "SDL_VideoInit")]
        public static extern int VideoInit(IntPtr driver_name);

        [DllImport(LibraryName, EntryPoint = "SDL_VideoQuit")]
        public static extern void VideoQuit();

        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentVideoDriver")]
        public static extern IntPtr GetCurrentVideoDriver();

        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDisplays")]
        public static extern int GetNumVideoDisplays();

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayName")]
        public static extern IntPtr GetDisplayName(int displayIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayBounds")]
        public static extern int GetDisplayBounds(int displayIndex, out Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayDPI")]
        public static extern int GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayUsableBounds")]
        public static extern int GetDisplayUsableBounds(int displayIndex, out Rect rectangle);

        [DllImport(LibraryName, EntryPoint = "SDL_GetNumDisplayModes")]
        public static extern int GetNumDisplayModes(int displayIndex);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayMode")]
        public static extern int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetDeskTopDisplayMode")]
        public static extern int GetDeskTopDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentDisplayMode")]
        public static extern int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetClosestDisplayMode")]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern DisplayMode GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayIndex")]
        public static extern int GetWindowDisplayIndex(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
        public static extern int SetWindowDisplayMode(SDL2.Window window, ref DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
        public static extern int SetWindowDisplayMode(SDL2.Window window, IntPtr mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayMode")]
        public static extern int GetWindowDisplayMode(SDL2.Window window, out DisplayMode mode);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPixelFormat")]
        public static extern uint GetWindowPixelFormat(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindow")]
        public static extern Window CreateWindow(Text title, int x, int y, int width, int height, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowFrom")]
        public static extern Window CreateWindowFrom(void* data);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowID")]
        public static extern WindowID GetWindowID(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFromID")]
        public static extern Window GetWindowFromID(WindowID id);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFlags")]
        public static extern WindowFlags GetWindowFlags(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowTitle")]
        public static extern void SetWindowTitle(SDL2.Window window, Text title);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowTitle")]
        public static extern Text GetWindowTitle(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowIcon")]
        public static extern void SetWindowIcon(SDL2.Window window, Surface icon);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowData")]
        public static extern void* SetWindowData(SDL2.Window window, Text name, void* userData);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowData")]
        public static extern void* GetWindowData(SDL2.Window window, Text name);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowPosition")]
        public static extern void SetWindowPosition(SDL2.Window window, int x, int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static extern void GetWindowPosition(SDL2.Window window, out int x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static extern void GetWindowPosition(SDL2.Window window, out int x, IntPtr y);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static extern void GetWindowPosition(SDL2.Window window, IntPtr x, out int y);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowSize")]
        public static extern void SetWindowSize(SDL2.Window window, int width, int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static extern void GetWindowSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static extern void GetWindowSize(SDL2.Window window, out int width, IntPtr height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static extern void GetWindowSize(SDL2.Window window, IntPtr width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBordersSize")]
        public static extern int GetWindowBordersSize(SDL2.Window window, out int top, out int left, out int bottom, out int right);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMinimumSize")]
        public static extern void SetWindowMinimumSize(SDL2.Window window, int minwidth, int minHeight);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMinimumSize")]
        public static extern void GetWindowMinimumSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMaximumSize")]
        public static extern void SetWindowMaximumSize(SDL2.Window window, int maxWidth, int maxHeight);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMaximumSize")]
        public static extern void GetWindowMaximumSize(SDL2.Window window, out int width, out int height);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBordered")]
        public static extern void SetWindowBordered(SDL2.Window window, bool bordered);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowResizable")]
        public static extern void SetWindowResizable(SDL2.Window window, bool resizable);

        [DllImport(LibraryName, EntryPoint = "SDL_ShowWindow")]
        public static extern void ShowWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_HideWindow")]
        public static extern void HideWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_RaiseWindow")]
        public static extern void RaiseWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_MaximizeWindow")]
        public static extern void MaximizeWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_MinimizeWindow")]
        public static extern void MinimizeWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_RestoreWindow")]
        public static extern void RestoreWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowFullscreen")]
        public static extern int SetWindowFullscreen(SDL2.Window window, WindowFlags flags);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSurface")]
        public static extern Surface* GetWindowSurface(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurface")]
        public static extern int UpdateWindowSurface(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
        public static extern int UpdateWindowSurfaceRects(SDL2.Window window, Rect* rectangles, int numRectangles);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGrab")]
        public static extern void SetWindowGrab(SDL2.Window window, bool grabbed);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGrab")]
        public static extern bool GetWindowGrab(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_GetGrabbedWindow")]
        public static extern Window GetGrabbedWindow();

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBrightness")]
        public static extern int SetWindowBrightness(SDL2.Window window, float brightness);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBrightness")]
        public static extern float GetWindowBrightness(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowOpacity")]
        public static extern int SetWindowOpacity(SDL2.Window window, float opacity);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowOpacity")]
        public static extern int GetWindowOpacity(SDL2.Window window, out float outOpacity);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowModalFor")]
        public static extern int SetWindowModalFor(SDL2.Window modalWindow, SDL2.Window parentWindow);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowInputFocus")]
        public static extern int SetWindowInputFocus(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGammaRamp")]
        public static extern int SetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGammaRamp")]
        public static extern int GetWindowGammaRamp(SDL2.Window window, ushort* red, ushort* green, ushort* blue);

        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowHitTest")]
        public static extern int SetWindowHitTest(SDL2.Window window, HitTest callback, void* callbackData);

        [DllImport(LibraryName, EntryPoint = "SDL_DestroyWindow")]
        public static extern void DestroyWindow(SDL2.Window window);

        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenSaverEnabled")]
        public static extern bool IsScreenSaverEnabled();

        [DllImport(LibraryName, EntryPoint = "SDL_EnableScreenSaver")]
        public static extern void EnableScreenSaver();

        [DllImport(LibraryName, EntryPoint = "SDL_DisableScreenSaver")]
        public static extern void DisableScreenSaver();

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