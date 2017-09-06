using System;
using System.Security;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;using Engine;

namespace SDL2
{
    public static unsafe class SDLExtensions
    {
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this bool value)
        {
            if (value == false)
            {
                Log.Error(SDL.GetError());
            }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this int value)
        {
            if (value != 0)
            {
                Log.Error(SDL.GetError());
            }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this IntPtr value)
        {
            if (value == IntPtr.Zero)
            {
                Log.Error(SDL.GetError());
            }
        }

        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this SDL.Window value)
        {
            if (value == IntPtr.Zero)
            {
                Log.Error(SDL.GetError());
            }
        }
    }

    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class SDL
    {
#if PLATFORM_WINDOWS
        private const string LibraryName = "SDL2.dll";
#elif PLATFORM_LINUX
        private const string LibraryName = "libSDL2-2.0.so";
#elif PLATFORM_MACOS
        private const string LibraryName = "libdylib";
#endif

        public const int ScanCodeMask = (1 << 30);
        public const int AudioCVTMaxFilters = 9;

        public static class Hint
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

        //
        // SDL.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_Init")]
        public static extern int Init(InitFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_InitSubSystem")]
        public static extern int InitSubSystem(InitFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_QuitSubSystem")]
        public static extern void QuitSubSystem(InitFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_WasInit")]
        public static extern InitFlags WasInit(InitFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_Quit")]
        public static extern void Quit();

        // TODO: SDL_audio.h

        //
        // SDL_blendmode.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_ComposeCustomBlendMode")]
        public static extern BlendMode ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation);

        //
        // SDL_clipboard.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetClipboardText")]
        public static extern int SetClipboardText(byte* text);        [DllImport(LibraryName, EntryPoint = "SDL_GetClipboardText")]
        public static extern byte* GetClipboardText();        [DllImport(LibraryName, EntryPoint = "SDL_HasClipboardText")]
        public static extern bool HasClipboardText();

        //
        // SDL_cpuinfo.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetCpuCount")]
        public static extern int GetCpuCount();        [DllImport(LibraryName, EntryPoint = "SDL_GetCpuCacheLineSize")]
        public static extern int GetCpuCacheLineSize();        [DllImport(LibraryName, EntryPoint = "SDL_HasRdtsc")]
        public static extern bool HasRdtsc();        [DllImport(LibraryName, EntryPoint = "SDL_HasAltiVec")]
        public static extern bool HasAltiVec();        [DllImport(LibraryName, EntryPoint = "SDL_HasMmx")]
        public static extern bool HasMmx();        [DllImport(LibraryName, EntryPoint = "SDL_Has3DNow")]
        public static extern bool Has3DNow();        [DllImport(LibraryName, EntryPoint = "SDL_HasSse")]
        public static extern bool HasSse();        [DllImport(LibraryName, EntryPoint = "SDL_HasSse2")]
        public static extern bool HasSse2();        [DllImport(LibraryName, EntryPoint = "SDL_HasSse3")]
        public static extern bool HasSse3();        [DllImport(LibraryName, EntryPoint = "SDL_HasSse41")]
        public static extern bool HasSse41();        [DllImport(LibraryName, EntryPoint = "SDL_HasSse42")]
        public static extern bool HasSse42();        [DllImport(LibraryName, EntryPoint = "SDL_HasAvx")]
        public static extern bool HasAvx();        [DllImport(LibraryName, EntryPoint = "SDL_HasAvx2")]
        public static extern bool HasAvx2();        [DllImport(LibraryName, EntryPoint = "SDL_HasNeon")]
        public static extern bool HasNeon();        [DllImport(LibraryName, EntryPoint = "SDL_GetSystemRam")]
        public static extern int GetSystemRam();

        //
        // SDL_error.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetError")]
        public static extern byte* GetError();        [DllImport(LibraryName, EntryPoint = "SDL_SetError")]
        public static extern int SetError(byte* format, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_ClearError")]
        public static extern void ClearError();

        //
        // SDL_events.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_PumpEvents")]
        public static extern void PumpEvents();        [DllImport(LibraryName, EntryPoint = "SDL_PeepEvents")]
        public static extern int PeepEvents(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType);        [DllImport(LibraryName, EntryPoint = "SDL_HasEvent")]
        public static extern bool HasEvent(EventType type);        [DllImport(LibraryName, EntryPoint = "SDL_HasEvents")]
        public static extern bool HasEvents(EventType minType, EventType maxType);        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvent")]
        public static extern void FlushEvent(EventType type);        [DllImport(LibraryName, EntryPoint = "SDL_FlushEvents")]
        public static extern void FlushEvents(EventType minType, EventType maxType);        [DllImport(LibraryName, EntryPoint = "SDL_PollEvent")]
        public static extern int PollEvent(out Event @event);        [DllImport(LibraryName, EntryPoint = "SDL_WaitEvent")]
        public static extern int WaitEvent(out Event @event);        [DllImport(LibraryName, EntryPoint = "SDL_WaitEventTimeout")]
        public static extern int WaitEventTimeout(out Event @event, int timeout);        [DllImport(LibraryName, EntryPoint = "SDL_PushEvent")]
        public static extern int PushEvent(ref Event @event);        [DllImport(LibraryName, EntryPoint = "SDL_SetEventFilter")]
        public static extern void SetEventFilter(EventFilter filter, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_GetEventFilter")]
        public static extern bool GetEventFilter(out EventFilter filter, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_AddEventWatch")]
        public static extern void AddEventWatch(EventFilter filter, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_DelEventWatch")]
        public static extern void DelEventWatch(EventFilter filter, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_FilterEvents")]
        public static extern void FilterEvents(EventFilter filter, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_EventState")]
        public static extern State EventState(EventType type, State state);
        public static State GetEventState(EventType type) => EventState(type, State.Query);        [DllImport(LibraryName, EntryPoint = "SDL_RegisterEvents")]
        public static extern uint RegisterEvents(int numEvents);

        //
        // SDL_filesystem.h
        //

        [DllImport(LibraryName, EntryPoint = "SDL_GetBasePath")]
        public static extern byte* GetBasePath();        [DllImport(LibraryName, EntryPoint = "SDL_GetPrefPath")]
        public static extern byte* GetPrefPath(byte* org, byte* app);

        //
        // SDL_gamecontroller.h
        //

        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
        public static extern int GameControllerAddMappingsFromRW(RWops rwOps, int freeRW);
        public static int GameControllerAddMappingsFromFile(byte* file) => GameControllerAddMappingsFromRW(RWFromFile(file, "rb".ToUtf8()), 1);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerAddMapping")]
        public static extern int GameControllerAddMapping(byte* mappingText);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNumMappings")]
        public static extern int GameControllerNumMappings();        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForIndex")]
        public static extern byte* GameControllerMappingForIndex(int mappingIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMappingForGuid")]
        public static extern byte* GameControllerMappingForGuid(Guid guid);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerMapping")]
        public static extern byte* GameControllerMapping(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_IsGameController")]
        public static extern bool IsGameController(int joystickIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerNameForIndex")]
        public static extern byte* GameControllerNameForIndex(int joystickIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerOpen")]
        public static extern GameController GameControllerOpen(int joystickIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerFromInstanceID")]
        public static extern GameController GameControllerFromInstanceID(JoystickID joystickID);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerName")]
        public static extern byte* GameControllerName(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetVendor")]
        public static extern ushort GameControllerGetVendor(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetProduct")]
        public static extern ushort GameControllerGetProduct(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetProductVersion")]
        public static extern ushort GameControllerGetProductVersion(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAttached")]
        public static extern bool GameControllerGetAttached(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetJoystick")]
        public static extern Joystick GameControllerGetJoystick(GameController gameController);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerEventState")]
        public static extern State GameControllerEventState(State state);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerUpdate")]
        public static extern void GameControllerUpdate();        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxisFromText")]
        public static extern GameControllerAxis GameControllerGetAxisFromText(byte* pchText);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetTextForAxis")]
        public static extern byte* GameControllerGetTextForAxis(GameControllerAxis axis);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForAxis")]
        public static extern GameControllerButtonBind GameControllerGetBindForAxis(GameController gameController, GameControllerAxis axis);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetAxis")]
        public static extern short GameControllerGetAxis(GameController gameController, GameControllerAxis axis);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButtonFromText")]
        public static extern GameControllerButton GameControllerGetButtonFromText(byte* pchText);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetTextForButton")]
        public static extern byte* GameControllerGetTextForButton(GameControllerButton button);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetBindForButton")]
        public static extern GameControllerButtonBind GameControllerGetBindForButton(GameController gameController, GameControllerButton button);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerGetButton")]
        public static extern byte GameControllerGetButton(GameController gameController, GameControllerButton button);        [DllImport(LibraryName, EntryPoint = "SDL_GameControllerClose")]
        public static extern void GameControllerClose(GameController gameController);

        // TODO: SDL_gesture.h
        // TODO: SDL_haptic.h

        //
        // SDL_hints.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_SetHintWithPriority")]
        public static extern bool SetHintWithPriority(byte* name, byte* value, HintPriority priority);        [DllImport(LibraryName, EntryPoint = "SDL_SetHint")]
        public static extern bool SetHint(byte* name, byte* value);        [DllImport(LibraryName, EntryPoint = "SDL_GetHint")]
        public static extern byte* GetHint(byte* name);        [DllImport(LibraryName, EntryPoint = "SDL_GetHintBoolean")]
        public static extern bool GetHintBoolean(byte* name, bool defaultValue);        [DllImport(LibraryName, EntryPoint = "SDL_AddHintCallback")]
        public static extern void AddHintCallback(byte* name, HintCallback callback, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_DelHintCallback")]
        public static extern void DelHintCallback(byte* name, HintCallback callback, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_ClearHints")]
        public static extern void ClearHints();

        //
        // SDL_joystick.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_NumJoysticks")]
        public static extern int NumJoysticks();        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNameForIndex")]
        public static extern byte* JoystickNameForIndex(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceGuid")]
        public static extern Guid JoystickGetDeviceGUID(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceVendor")]
        public static extern ushort JoystickGetDeviceVendor(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceProduct")]
        public static extern ushort JoystickGetDeviceProduct(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceProductVersion")]
        public static extern ushort JoystickGetDeviceProductVersion(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceType")]
        public static extern JoystickType JoystickGetDeviceType(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetDeviceInstanceID")]
        public static extern JoystickID JoystickGetDeviceInstanceID(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickOpen")]
        public static extern Joystick JoystickOpen(int deviceIndex);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickFromInstanceID")]
        public static extern Joystick JoystickFromInstanceID(JoystickID joystickID);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickName")]
        public static extern byte* JoystickName(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGuid")]
        public static extern Guid JoystickGetGUID(Joystick joystick);
        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetVendor")]
        public static extern ushort JoystickGetVendor(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetProduct")]
        public static extern ushort JoystickGetProduct(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetProductVersion")]
        public static extern ushort JoystickGetProductVersion(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetType")]
        public static extern JoystickType JoystickGetType(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGuidText")]
        public static extern void JoystickGetGuidText(Guid guid, byte* pszGUID, int cbGUID);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetGuidFromText")]
        public static extern Guid JoystickGetGUIDFromText(byte* pchGUID);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAttached")]
        public static extern bool JoystickGetAttached(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickInstanceID")]
        public static extern JoystickID JoystickInstanceID(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumAxes")]
        public static extern int JoystickNumAxes(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumBalls")]
        public static extern int JoystickNumBalls(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumHats")]
        public static extern int JoystickNumHats(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickNumButtons")]
        public static extern int JoystickNumButtons(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickUpdate")]
        public static extern void JoystickUpdate();        [DllImport(LibraryName, EntryPoint = "SDL_JoystickEventState")]
        public static extern State JoystickEventState(State state);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAxis")]
        public static extern short JoystickGetAxis(Joystick joystick, JoystickAxis axis);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetAxisInitialState")]
        public static extern bool JoystickGetAxisInitialState(Joystick joystick, JoystickAxis axis, out short state);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetHat")]
        public static extern JoystickHat JoystickGetHat(Joystick joystick, int hat);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetBall")]
        public static extern int JoystickGetBall(Joystick joystick, int ball, out int dx, out int dy);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickGetButton")]
        public static extern byte JoystickGetButton(Joystick joystick, int button);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickClose")]
        public static extern void JoystickClose(Joystick joystick);        [DllImport(LibraryName, EntryPoint = "SDL_JoystickCurrentPowerLevel")]
        public static extern JoystickPowerLevel JoystickCurrentPowerLevel(Joystick joystick);

        //
        // SDL_keyboard.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardFocus")]
        public static extern Window GetKeyboardFocus();        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyboardState")]
        public static extern byte* GetKeyboardState(out int numkeys);        [DllImport(LibraryName, EntryPoint = "SDL_GetModState")]
        public static extern KeyMod GetModState();        [DllImport(LibraryName, EntryPoint = "SDL_SetModState")]
        public static extern void SetModState(KeyMod modState);        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromScancode")]
        public static extern KeyCode GetKeyFromScancode(Scancode scancode);        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromKey")]
        public static extern Scancode GetScancodeFromKey(KeyCode key);        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeName")]
        public static extern byte* GetScancodeName(Scancode scancode);        [DllImport(LibraryName, EntryPoint = "SDL_GetScancodeFromName")]
        public static extern Scancode GetScancodeFromName(byte* name);        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyName")]
        public static extern byte* GetKeyName(KeyCode key);        [DllImport(LibraryName, EntryPoint = "SDL_GetKeyFromName")]
        public static extern KeyCode GetKeyFromName(byte* name);        [DllImport(LibraryName, EntryPoint = "SDL_StartTextInput")]
        public static extern void StartTextInput();        [DllImport(LibraryName, EntryPoint = "SDL_IsTextInputActive")]
        public static extern bool IsTextInputActive();        [DllImport(LibraryName, EntryPoint = "SDL_StopTextInput")]
        public static extern void StopTextInput();        [DllImport(LibraryName, EntryPoint = "SDL_SetTextInputRect")]
        public static extern void SetTextInputRect(ref Rect rectangle);        [DllImport(LibraryName, EntryPoint = "SDL_HasScreenKeyboardSupport")]
        public static extern bool HasScreenKeyboardSupport();        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenKeyboardShown")]
        public static extern bool IsScreenKeyboardShown(Window window);

        //
        // SDL_loadso.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_LoadObject")]
        public static extern IntPtr LoadObject(byte* file);        [DllImport(LibraryName, EntryPoint = "SDL_LoadFunction")]
        public static extern IntPtr LoadFunction(IntPtr handle, byte* name);        [DllImport(LibraryName, EntryPoint = "SDL_UnloadObject")]
        public static extern void UnloadObject(IntPtr handle);

        //
        // SDL_log.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_LogSetAllPriority")]
        public static extern void LogSetAllPriority(LogPriority priority);        [DllImport(LibraryName, EntryPoint = "SDL_LogSetPriority")]
        public static extern void LogSetPriority(LogCategory category, LogPriority priority);        [DllImport(LibraryName, EntryPoint = "SDL_LogGetPriority")]
        public static extern LogPriority LogGetPriority(LogCategory category);        [DllImport(LibraryName, EntryPoint = "SDL_LogResetPriorities")]
        public static extern void LogResetPriorities();        [DllImport(LibraryName, EntryPoint = "SDL_Log")]
        public static extern void Log(byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogVerbose")]
        public static extern void LogVerbose(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogDebug")]
        public static extern void LogDebug(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogInfo")]
        public static extern void LogInfo(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogWarn")]
        public static extern void LogWarn(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogError")]
        public static extern void LogError(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogCritical")]
        public static extern void LogCritical(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogMessage")]
        public static extern void LogMessage(LogCategory category, byte* fmt, params object[] objects);        [DllImport(LibraryName, EntryPoint = "SDL_LogGetOutputFunction")]
        public static extern void LogGetOutputFunction(LogOutputFunction callback, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_LogSetOutputFunction")]
        public static extern void LogSetOutputFunction(LogOutputFunction callback, IntPtr userData);

        //
        // SDL_messagebox.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_ShowMessageBox")]
        public static extern int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID);        [DllImport(LibraryName, EntryPoint = "SDL_ShowSimpleMessageBox")]
        public static extern int ShowSimpleMessageBox(MessageBoxFlags flags, byte* title, byte* message, Window window);

        //
        // SDL_mouse.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseFocus")]
        public static extern Window GetMouseFocus();        [DllImport(LibraryName, EntryPoint = "SDL_GetMouseState")]
        public static extern MouseButtonState GetMouseState(out int x, out int y);        [DllImport(LibraryName, EntryPoint = "SDL_GetGlobalMouseState")]
        public static extern MouseButtonState GetGlobalMouseState(out int x, out int y);        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseState")]
        public static extern MouseButtonState GetRelativeMouseState(out int x, out int y);        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseInWindow")]
        public static extern void WarpMouseInWindow(Window window, int x, int y);        [DllImport(LibraryName, EntryPoint = "SDL_WarpMouseGlobal")]
        public static extern int WarpMouseGlobal(int x, int y);        [DllImport(LibraryName, EntryPoint = "SDL_SetRelativeMouseMode")]
        public static extern int SetRelativeMouseMode(bool enabled);        [DllImport(LibraryName, EntryPoint = "SDL_CaptureMouse")]
        public static extern int CaptureMouse(bool enabled);        [DllImport(LibraryName, EntryPoint = "SDL_GetRelativeMouseMode")]
        public static extern bool GetRelativeMouseMode();        [DllImport(LibraryName, EntryPoint = "SDL_CreateCursor")]
        public static extern Cursor CreateCursor(byte* data, byte* mask, int w, int h, int hotX, int hotY);        [DllImport(LibraryName, EntryPoint = "SDL_CreateColorCursor")]
        public static extern Cursor CreateColorCursor(Surface surface, int hotX, int hotY);        [DllImport(LibraryName, EntryPoint = "SDL_CreateSystemCursor")]
        public static extern Cursor CreateSystemCursor(SystemCursor id);        [DllImport(LibraryName, EntryPoint = "SDL_SetCursor")]
        public static extern void SetCursor(Cursor cursor);        [DllImport(LibraryName, EntryPoint = "SDL_GetCursor")]
        public static extern Cursor GetCursor();        [DllImport(LibraryName, EntryPoint = "SDL_GetDefaultCursor")]
        public static extern Cursor GetDefaultCursor();        [DllImport(LibraryName, EntryPoint = "SDL_FreeCursor")]
        public static extern void FreeCursor(Cursor cursor);        [DllImport(LibraryName, EntryPoint = "SDL_ShowCursor")]
        public static extern State ShowCursor(State toggle);

        //
        // SDL_pixels.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPixelFormatName")]
        public static extern byte* GetPixelFormatName(uint format);        [DllImport(LibraryName, EntryPoint = "SDL_PixelFormatEnumToMasks")]
        public static extern bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);        [DllImport(LibraryName, EntryPoint = "SDL_MasksToPixelFormatEnum")]
        public static extern uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);        [DllImport(LibraryName, EntryPoint = "SDL_AllocFormat")]
        public static extern PixelFormat* AllocFormat(uint pixelFormat);        [DllImport(LibraryName, EntryPoint = "SDL_FreeFormat")]
        public static extern void FreeFormat(ref PixelFormat pixelFormat);        [DllImport(LibraryName, EntryPoint = "SDL_AllocPalette")]
        public static extern IntPtr AllocPalette(int numColors);        [DllImport(LibraryName, EntryPoint = "SDL_SetPixelFormatPalette")]
        public static extern int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette);        [DllImport(LibraryName, EntryPoint = "SDL_SetPaletteColors")]
        public static extern int SetPaletteColors(Palette palette, Color* colors, int firstColor, int numColors);        [DllImport(LibraryName, EntryPoint = "SDL_FreePalette")]
        public static extern void FreePalette(Palette palette);        [DllImport(LibraryName, EntryPoint = "SDL_MapRgb")]
        public static extern uint MapRgb(ref PixelFormat format, byte r, byte g, byte b);        [DllImport(LibraryName, EntryPoint = "SDL_MapRgba")]
        public static extern uint MapRgba(ref PixelFormat format, byte r, byte g, byte b, byte a);        [DllImport(LibraryName, EntryPoint = "SDL_GetRgb")]
        public static extern void GetRgb(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);        [DllImport(LibraryName, EntryPoint = "SDL_GetRgba")]
        public static extern void GetRgba(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);        [DllImport(LibraryName, EntryPoint = "SDL_CalculateGammaRamp")]
        public static extern void CalculateGammaRamp(float gamma, out ushort* ramp);

        //
        // SDL_platform.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPlatform")]
        public static extern byte* GetPlatform();

        //
        // SDL_power.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetPowerInfo")]
        public static extern PowerState GetPowerInfo(out int seconds, out int percentage);

        //
        // SDL_rect.h
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInRect(ref Point p, ref Rect r) => ((p.X >= r.X) && (p.X < (r.X + r.W)) && (p.Y >= r.Y) && (p.Y < (r.Y + r.H))) ? true : false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectEmpty(ref Rect r) => ((r.W <= 0) || (r.H <= 0)) ? true : false;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectEquals(ref Rect a, ref Rect b) => ((a.X == b.X) && (a.Y == b.Y) && (a.W == b.W) && (a.H == b.H)) ? true : false;        [DllImport(LibraryName, EntryPoint = "SDL_HasIntersection")]
        public static extern bool HasIntersection(ref Rect a, ref Rect b);        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRect")]
        public static extern bool IntersectRect(ref Rect a, ref Rect b, out Rect result);        [DllImport(LibraryName, EntryPoint = "SDL_UnionRect")]
        public static extern void UnionRect(ref Rect a, ref Rect b, out Rect result);        [DllImport(LibraryName, EntryPoint = "SDL_EnclosePoints")]
        public static extern bool EnclosePoints(Point* points, int count, ref Rect clip, out Rect result);        [DllImport(LibraryName, EntryPoint = "SDL_IntersectRectAndLine")]
        public static extern bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

        //
        // SDL_render.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetNumRenderDrivers")]
        public static extern int GetNumRenderDrivers();        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDriverInfo")]
        public static extern int GetRenderDriverInfo(int index, out RendererInfo info);        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowAndRenderer")]
        public static extern int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_CreateRenderer")]
        public static extern Renderer CreateRenderer(Window window, int index, RendererFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_CreateSoftwareRenderer")]
        public static extern Renderer CreateSoftwareRenderer(Surface surface);        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderer")]
        public static extern Renderer GetRenderer(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererInfo")]
        public static extern int GetRendererInfo(Renderer renderer, out RendererInfo info);        [DllImport(LibraryName, EntryPoint = "SDL_GetRendererOutputSize")]
        public static extern int GetRendererOutputSize(Renderer renderer, out int w, out int h);        [DllImport(LibraryName, EntryPoint = "SDL_CreateTexture")]
        public static extern Texture CreateTexture(Renderer renderer, uint format, int access, int w, int h);        [DllImport(LibraryName, EntryPoint = "SDL_CreateTextureFromSurface")]
        public static extern Texture CreateTextureFromSurface(Renderer renderer, Surface surface);        [DllImport(LibraryName, EntryPoint = "SDL_QueryTexture")]
        public static extern int QueryTexture(Texture texture, out uint format, out int access, out int w, out int h);        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureColorMod")]
        public static extern int SetTextureColorMod(Texture texture, byte r, byte g, byte b);        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureColorMod")]
        public static extern int GetTextureColorMod(Texture texture, out byte r, out byte g, out byte b);        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureAlphaMod")]
        public static extern int SetTextureAlphaMod(Texture texture, byte alpha);        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureAlphaMod")]
        public static extern int GetTextureAlphaMod(Texture texture, out byte alpha);        [DllImport(LibraryName, EntryPoint = "SDL_SetTextureBlendMode")]
        public static extern int SetTextureBlendMode(Texture texture, BlendMode blendMode);        [DllImport(LibraryName, EntryPoint = "SDL_GetTextureBlendMode")]
        public static extern int GetTextureBlendMode(Texture texture, out BlendMode blendMode);        [DllImport(LibraryName, EntryPoint = "SDL_UpdateTexture")]
        public static extern int UpdateTexture(Texture texture, ref Rect rect, IntPtr pixels, int pitch);        [DllImport(LibraryName, EntryPoint = "SDL_UpdateYUVTexture")]
        public static extern int UpdateYUVTexture(Texture texture, ref Rect rect, byte* yPlane, int yPitch, byte* uPlane, int uPitch, byte* vPlane, int vPitch);        [DllImport(LibraryName, EntryPoint = "SDL_LockTexture")]
        public static extern int LockTexture(Texture texture, ref Rect rect, out IntPtr pixels, out int pitch);        [DllImport(LibraryName, EntryPoint = "SDL_UnlockTexture")]
        public static extern void UnlockTexture(Texture texture);        [DllImport(LibraryName, EntryPoint = "SDL_RenderTargetSupported")]
        public static extern bool RenderTargetSupported(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderTarget")]
        public static extern int SetRenderTarget(Renderer renderer, Texture texture);        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderTarget")]
        public static extern Texture GetRenderTarget(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetLogicalSize")]
        public static extern int RenderSetLogicalSize(Renderer renderer, int w, int h);        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetLogicalSize")]
        public static extern void RenderGetLogicalSize(Renderer renderer, out int w, out int h);        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetIntegerScale")]
        public static extern int RenderSetIntegerScale(Renderer renderer, bool enable);        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetIntegerScale")]
        public static extern bool RenderGetIntegerScale(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetViewport")]
        public static extern int RenderSetViewport(Renderer renderer, ref Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetViewport")]
        public static extern void RenderGetViewport(Renderer renderer, out Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetClipRect")]
        public static extern int RenderSetClipRect(Renderer renderer, ref Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetClipRect")]
        public static extern void RenderGetClipRect(Renderer renderer, out Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderIsClipEnabled")]
        public static extern bool RenderIsClipEnabled(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_RenderSetScale")]
        public static extern int RenderSetScale(Renderer renderer, float scaleX, float scaleY);        [DllImport(LibraryName, EntryPoint = "SDL_RenderGetScale")]
        public static extern void RenderGetScale(Renderer renderer, out float scaleX, out float scaleY);        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawColor")]
        public static extern int SetRenderDrawColor(Renderer renderer, byte r, byte g, byte b, byte a);        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawColor")]
        public static extern int GetRenderDrawColor(Renderer renderer, out byte r, out byte g, out byte b, out byte a);        [DllImport(LibraryName, EntryPoint = "SDL_SetRenderDrawBlendMode")]
        public static extern int SetRenderDrawBlendMode(Renderer renderer, BlendMode blendMode);        [DllImport(LibraryName, EntryPoint = "SDL_GetRenderDrawBlendMode")]
        public static extern int GetRenderDrawBlendMode(Renderer renderer, out BlendMode blendMode);        [DllImport(LibraryName, EntryPoint = "SDL_RenderClear")]
        public static extern int RenderClear(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoint")]
        public static extern int RenderDrawPoint(Renderer renderer, int x, int y);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawPoints")]
        public static extern int RenderDrawPoints(Renderer renderer, Point* points, int count);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLine")]
        public static extern int RenderDrawLine(Renderer renderer, int x1, int y1, int x2, int y2);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawLines")]
        public static extern int RenderDrawLines(Renderer renderer, Point* points, int count);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRect")]
        public static extern int RenderDrawRect(Renderer renderer, ref Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderDrawRects")]
        public static extern int RenderDrawRects(Renderer renderer, Rect* rects, int count);        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRect")]
        public static extern int RenderFillRect(Renderer renderer, ref Rect rect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderFillRects")]
        public static extern int RenderFillRects(Renderer renderer, Rect* rects, int count);        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopy")]
        public static extern int RenderCopy(Renderer renderer, Texture texture, ref Rect srcRect, ref Rect dstRect);        [DllImport(LibraryName, EntryPoint = "SDL_RenderCopyEx")]
        public static extern int RenderCopyEx(Renderer renderer, Texture texture, ref Rect srcRect, ref Rect dstRect, double angle, ref Point center, RendererFlip flip);        [DllImport(LibraryName, EntryPoint = "SDL_RenderReadPixels")]
        public static extern int RenderReadPixels(Renderer renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);        [DllImport(LibraryName, EntryPoint = "SDL_RenderPresent")]
        public static extern void RenderPresent(Renderer renderer);        [DllImport(LibraryName, EntryPoint = "SDL_DestroyTexture")]
        public static extern void DestroyTexture(Texture texture);        [DllImport(LibraryName, EntryPoint = "SDL_DestroyRenderer")]
        public static extern void DestroyRenderer(Renderer renderer);

        //
        // SDL_rwops.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_RwFromFile")]
        public static extern RWops RWFromFile(byte* file, byte* mode);

        //
        // SDL_shape.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_CreateShapedWindow")]
        public static extern Window CreateShapedWindow(byte* title, uint x, uint y, uint w, uint h, WindowFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_IsShapedWindow")]
        public static extern bool IsShapedWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowShape")]
        public static extern int SetWindowShape(Window window, ref Surface shape, ref WindowShape shapeMode);        [DllImport(LibraryName, EntryPoint = "SDL_GetShapedWindowMode")]
        public static extern int GetShapedWindowMode(Window window, out WindowShape shapeMode);

        //
        // TODO: SDL_surface.h
        //
        //public static extern int UpperBlit(Surface src, ref Rect srcrect, Surface dst, ref Rect dstrect)")]        //public static extern int UpperBlitScaled(Surface src, ref Rect srcrect, Surface dst, ref Rect dstrect)")]        //public static extern int ConvertPixels(int width, int height, uint src_format, Surface src, int src_pitch, uint dst_format, Surface dst, int dst_pitch)")]        //public static extern IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags)")]        //public static extern IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags)")]        //public static extern IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask)")]        //public static extern IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask)")]        //public static extern IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format)")]        //public static extern IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format)")]        //public static extern int FillRect(IntPtr dst, ref Rect rect, uint color)")]        //public static extern int FillRects(IntPtr dst, Rect* rects, int count, uint color)")]        //public static extern void FreeSurface(Surface surface)")]        //public static extern void GetClipRect(Surface surface, out Rect rect)")]        //public static extern int GetColorKey(Surface surface, out uint key)")]        //public static extern int GetSurfaceAlphaMod(Surface surface, out byte alpha)")]        //public static extern int GetSurfaceBlendMode(Surface surface, out BlendMode blendMode)")]        //public static extern int GetSurfaceColorMod(Surface surface, out byte r, out byte g, out byte b)")]        //[DllImport(LibraryName, EntryPoint = "SDL_RW(IntPtr src, int freesrc)")]        //public static extern int LockSurface(Surface surface)")]        //public static extern int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect)")]        //public static extern int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect)")]        //[DllImport(LibraryName, EntryPoint = "SDL_RW(Surface surface, IntPtr src, int freesrc)")]        //public static extern bool SetClipRect(Surface surface, ref Rect rect)")]        //public static extern int SetColorKey(Surface surface, int flag, uint key)")]        //public static extern int SetSurfaceAlphaMod(Surface surface, byte alpha)")]        //public static extern int SetSurfaceBlendMode(Surface surface, BlendMode blendMode)")]        //public static extern int SetSurfaceColorMod(Surface surface, byte r, byte g, byte b)")]        //public static extern int SetSurfacePalette(Surface surface, IntPtr palette)")]        //public static extern int SetSurfaceRLE(Surface surface, int flag)")]        //public static extern int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect)")]        //public static extern void UnlockSurface(Surface surface)")]

        //
        // SDL_syswm.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowWMInfo")]
        public static extern bool GetWindowWMInfo(Window window, ref SysWMInfo info);

        //
        // SDL_timer.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_GetTicks")]
        public static extern uint GetTicks();

        public static bool TicksPassed(uint a, uint b) => ((int)(b - a) <= 0);        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceCounter")]
        public static extern ulong GetPerformanceCounter();        [DllImport(LibraryName, EntryPoint = "SDL_GetPerformanceFrequency")]
        public static extern ulong GetPerformanceFrequency();        [DllImport(LibraryName, EntryPoint = "SDL_Delay")]
        public static extern void Delay(uint ms);        [DllImport(LibraryName, EntryPoint = "SDL_AddTimer")]
        public static extern TimerID AddTimer(uint interval, TimerCallback callback, IntPtr param);        [DllImport(LibraryName, EntryPoint = "SDL_RemoveTimer")]
        public static extern bool RemoveTimer(TimerID id);

        // TODO: SDL_touch.h

        //
        // SDL_version.h
        //
        public static int VersionNum(int x, int y, int z) => x * 1000 + y * 100 + z;
        public static int CompiledVersion() => VersionNum(Version.MajorVersion, Version.MinorVersion, Version.PatchLevel);
        public static bool VersionAtLeast(int x, int y, int z) => CompiledVersion() >= VersionNum(x, y, z);        [DllImport(LibraryName, EntryPoint = "SDL_GetVersion")]
        public static extern void GetVersion(out Version version);        [DllImport(LibraryName, EntryPoint = "SDL_GetRevision")]
        public static extern byte* GetRevision();        [DllImport(LibraryName, EntryPoint = "SDL_GetRevisionNumber")]
        public static extern int GetRevisionNumber();

        //
        // SDL_video.h
        //
        public const int WindowPositionUndefinedMask = 0x1FFF0000;
        public const int WindowPositionCenteredMask = 0x2FFF0000;
        public const int WindowPositionUndefined = 0x1FFF0000;
        public const int WindowPositionCentered = 0x2FFF0000;
        public static int WindowPositionUndefinedDisplay(int x) => WindowPositionUndefinedMask | x;
        public static bool WindowPositionIsUndefined(int x) => (x & 0xFFFF0000) == WindowPositionUndefinedMask;
        public static int WindowPositionCenteredDisplay(int x) => WindowPositionCenteredMask | x;
        public static bool WindowPositionIsCentered(int x) => (x & 0xFFFF0000) == WindowPositionCenteredMask;        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDrivers")]
        public static extern int GetNumVideoDrivers();        [DllImport(LibraryName, EntryPoint = "SDL_GetVideoDriver")]
        public static extern byte* GetVideoDriver(int index);        [DllImport(LibraryName, EntryPoint = "SDL_VideoInit")]
        public static extern int VideoInit(byte* driverName);        [DllImport(LibraryName, EntryPoint = "SDL_VideoQuit")]
        public static extern void VideoQuit();        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentVideoDriver")]
        public static extern byte* GetCurrentVideoDriver();        [DllImport(LibraryName, EntryPoint = "SDL_GetNumVideoDisplays")]
        public static extern int GetNumVideoDisplays();        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayName")]
        public static extern byte* GetDisplayName(int displayIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayBounds")]
        public static extern int GetDisplayBounds(int displayIndex, out Rect rectangle);        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayDpi")]
        public static extern int GetDisplayDpi(int displayIndex, out float ddpi, out float hdpi, out float vdpi);        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayUsableBounds")]
        public static extern int GetDisplayUsableBounds(int displayIndex, out Rect rectangle);        [DllImport(LibraryName, EntryPoint = "SDL_GetNumDisplayModes")]
        public static extern int GetNumDisplayModes(int displayIndex);        [DllImport(LibraryName, EntryPoint = "SDL_GetDisplayMode")]
        public static extern int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);        [DllImport(LibraryName, EntryPoint = "SDL_GetDesktopDisplayMode")]
        public static extern int GetDesktopDisplayMode(int displayIndex, out DisplayMode mode);        [DllImport(LibraryName, EntryPoint = "SDL_GetCurrentDisplayMode")]
        public static extern int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);        [DllImport(LibraryName, EntryPoint = "SDL_GetClosestDisplayMode")]
        public static extern DisplayMode GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayIndex")]
        public static extern int GetWindowDisplayIndex(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowDisplayMode")]
        public static extern int SetWindowDisplayMode(Window window, ref DisplayMode mode);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowDisplayMode")]
        public static extern int GetWindowDisplayMode(Window window, out DisplayMode mode);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPixelFormat")]
        public static extern uint GetWindowPixelFormat(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindow")]
        public static extern Window CreateWindow(byte* title, int x, int y, int width, int height, WindowFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_CreateWindowFrom")]
        public static extern Window CreateWindowFrom(IntPtr data);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowID")]
        public static extern WindowID GetWindowID(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFromID")]
        public static extern Window GetWindowFromID(WindowID id);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowFlags")]
        public static extern WindowFlags GetWindowFlags(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowTitle")]
        public static extern void SetWindowTitle(Window window, byte* title);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowTitle")]
        public static extern byte* GetWindowTitle(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowIcon")]
        public static extern void SetWindowIcon(Window window, Surface icon);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowData")]
        public static extern IntPtr SetWindowData(Window window, byte* name, IntPtr userData);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowData")]
        public static extern IntPtr GetWindowData(Window window, byte* name);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowPosition")]
        public static extern void SetWindowPosition(Window window, int x, int y);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowPosition")]
        public static extern void GetWindowPosition(Window window, out int x, out int y);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowSize")]
        public static extern void SetWindowSize(Window window, int width, int height);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSize")]
        public static extern void GetWindowSize(Window window, out int width, out int height);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBordersSize")]
        public static extern int GetWindowBordersSize(Window window, out int top, out int left, out int bottom, out int right);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMinimumSize")]
        public static extern void SetWindowMinimumSize(Window window, int minWidth, int minHeight);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMinimumSize")]
        public static extern void GetWindowMinimumSize(Window window, out int width, out int height);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowMaximumSize")]
        public static extern void SetWindowMaximumSize(Window window, int maxWidth, int maxHeight);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowMaximumSize")]
        public static extern void GetWindowMaximumSize(Window window, out int width, out int height);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBordered")]
        public static extern void SetWindowBordered(Window window, bool bordered);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowResizable")]
        public static extern void SetWindowResizable(Window window, bool resizable);        [DllImport(LibraryName, EntryPoint = "SDL_ShowWindow")]
        public static extern void ShowWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_HideWindow")]
        public static extern void HideWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_RaiseWindow")]
        public static extern void RaiseWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_MaximizeWindow")]
        public static extern void MaximizeWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_MinimizeWindow")]
        public static extern void MinimizeWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_RestoreWindow")]
        public static extern void RestoreWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowFullscreen")]
        public static extern int SetWindowFullscreen(Window window, WindowFlags flags);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowSurface")]
        public static extern Surface GetWindowSurface(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurface")]
        public static extern int UpdateWindowSurface(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
        public static extern int UpdateWindowSurfaceRects(Window window, Rect* rectangles, int numRectangles);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGrab")]
        public static extern void SetWindowGrab(Window window, bool grabbed);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGrab")]
        public static extern bool GetWindowGrab(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_GetGrabbedWindow")]
        public static extern Window GetGrabbedWindow();        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowBrightness")]
        public static extern int SetWindowBrightness(Window window, float brightness);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowBrightness")]
        public static extern float GetWindowBrightness(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowOpacity")]
        public static extern int SetWindowOpacity(Window window, float opacity);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowOpacity")]
        public static extern int GetWindowOpacity(Window window, out float opacity);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowModalFor")]
        public static extern int SetWindowModalFor(Window modalWindow, Window parentWindow);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowInputFocus")]
        public static extern int SetWindowInputFocus(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowGammaRamp")]
        public static extern int SetWindowGammaRamp(Window window, ushort* red, ushort* green, ushort* blue);        [DllImport(LibraryName, EntryPoint = "SDL_GetWindowGammaRamp")]
        public static extern int GetWindowGammaRamp(Window window, ushort* red, ushort* green, ushort* blue);        [DllImport(LibraryName, EntryPoint = "SDL_SetWindowHitTest")]
        public static extern int SetWindowHitTest(Window window, HitTest callback, IntPtr callbackData);        [DllImport(LibraryName, EntryPoint = "SDL_DestroyWindow")]
        public static extern void DestroyWindow(Window window);        [DllImport(LibraryName, EntryPoint = "SDL_IsScreenSaverEnabled")]
        public static extern bool IsScreenSaverEnabled();        [DllImport(LibraryName, EntryPoint = "SDL_EnableScreenSaver")]
        public static extern void EnableScreenSaver();        [DllImport(LibraryName, EntryPoint = "SDL_DisableScreenSaver")]
        public static extern void DisableScreenSaver();

        //
        // SDL_vulkan.h
        //
        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_LoadLibrary")]
        public static extern int VulkanLoadLibrary(byte* path);        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_GetVkGetInstanceProcAddr")]
        public static extern IntPtr VulkanGetVkGetInstanceProcAddr();        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_UnloadLibrary")]
        public static extern void VulkanUnloadLibrary();        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_GetInstanceExtensions")]
        public static extern bool VulkanGetInstanceExtensions(Window window, ref uint count, byte** names);        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_CreateSurface")]
        public static extern bool VulkanCreateSurface(Window window, Vulkan.Vk.Instance instance, out Vulkan.Vk.Surface surface);        [DllImport(LibraryName, EntryPoint = "SDL_Vulkan_GetDrawableSize")]
        public static extern void VulkanGetDrawableSize(Window window, out int w, out int h);
    }
}