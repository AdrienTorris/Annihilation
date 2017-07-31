using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace TundraEngine.SDL
{
    /// <summary>
    /// An enumeration of hint priorities
    /// </summary>
    public enum HintPriority
    {
        Default,
        Normal,
        Override
    }

    /// <summary> Pixel type. </summary>
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
        // Bitmap order
        BitmapNone = 0,
        Bitmap4321,
        Bitmap1234,
        // Packed order
        PackedNone = 0,
        PackedXRGB,
        PackedRGBX,
        PackedARGB,
        PackedRGBA,
        PackedXBGR,
        PackedBGRX,
        PackedABGR,
        PackedBGRA,
        // Array order
        ArrayNone = 0,
        ArrayRGB,
        ArrayRGBA,
        ArrayARGB,
        ArrayBGR,
        ArrayBGRA,
        ArrayABGR
    }

    /// <summary> Packed component layout. </summary>
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

    /// <summary>
    /// The predefined log categories
    /// <para /> By default the application category is enabled at the INFO level, the assert category is enabled at the WARN level, test is enabled at the VERBOSE level and all other categories are enabled at the CRITICAL level.
    /// </summary>
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

        /* Reserved for future SDL library use */
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

        /* Beyond this point is reserved for application use, e.g.
            enum {
                MYAPP_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
                MYAPP_CATEGORY_AWESOME2,
                MYAPP_CATEGORY_AWESOME3,
                ...
            };
        */
        Custom,
    }

    /// <summary>
    /// The predefined log priorities
    /// </summary>
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

    /// <summary>
    /// These are the flags which may be passed to SDL_Init(). You should specify the subsystems which you will be using in your application.
    /// </summary>
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

    /// <summary>
    /// Type definition of the hint callback function.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void HintCallback(IntPtr userData, IntPtr name, IntPtr oldValue, IntPtr newValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void LogOutputFunction(IntPtr userData, LogCategory category, LogPriority priority, IntPtr message);

    public static class SDL
    {
        [SuppressUnmanagedCodeSecurity]
        public class Native
        {
            //---------------------------------------------------------------------
            // SDL.h
            //---------------------------------------------------------------------

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

            //---------------------------------------------------------------------
            // SDL_clipboard.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_SetClipboardText(byte* text);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetClipboardText();

            [DllImport(LibraryName)]
            public extern static bool SDL_HasClipboardText();

            //---------------------------------------------------------------------
            // SDL_cpuinfo.h
            //---------------------------------------------------------------------

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

            //---------------------------------------------------------------------
            // SDL_error.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static unsafe int SDL_SetError(byte* fmt, params object[] objects);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetError();

            [DllImport(LibraryName)]
            public extern static void SDL_ClearError();

            //---------------------------------------------------------------------
            // SDL_events.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static void SDL_PumpEvents();

            [DllImport(LibraryName)]
            public extern static int SDL_PeepEvents(
                [Out(), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
            Event[] events,
                int numevents,
                EventAction action,
                EventType minType,
                EventType maxType);

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
            public extern static void SDL_SetEventFilter(EventFilter filter, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static bool SDL_GetEventFilter(out EventFilter filter, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_AddEventWatch(EventFilter filter, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_DelEventWatch(EventFilter filter, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_FilterEvents(EventFilter filter, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static byte SDL_EventState(EventType type, EventState state);

            [DllImport(LibraryName)]
            public extern static uint SDL_RegisterEvents(int numEvents);

            //---------------------------------------------------------------------
            // SDL_filesystem.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetBasePath();

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetPrefPath(byte* org, byte* app);

            //---------------------------------------------------------------------
            // SDL_gamecontroller.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static int SDL_GameControllerAddMappingsFromRW(IntPtr rwOps, int freeRW);

            [DllImport(LibraryName)]
            public static extern int SDL_GameControllerAddMapping(string mappingString);

            [DllImport(LibraryName)]
            public static extern int SDL_GameControllerNumMappings();

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerMappingForIndex(int mapping_index);

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerMappingForGUID(Guid guid);

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerMapping(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern bool SDL_IsGameController(int joystick_index);

            [DllImport(LibraryName)]
            public static extern string SDL_GameControllerNameForIndex(int joystick_index);

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerOpen(int joystick_index);

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerFromInstanceID(int joyid);

            [DllImport(LibraryName)]
            public static extern string SDL_GameControllerName(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern ushort SDL_GameControllerGetVendor(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern ushort SDL_GameControllerGetProduct(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern ushort SDL_GameControllerGetProductVersion(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern bool SDL_GameControllerGetAttached(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern IntPtr SDL_GameControllerGetJoystick(IntPtr gamecontroller);

            [DllImport(LibraryName)]
            public static extern int SDL_GameControllerEventState(int state);

            [DllImport(LibraryName)]
            public static extern void SDL_GameControllerUpdate();

            [DllImport(LibraryName)]
            public static extern GameControllerAxis SDL_GameControllerGetAxisFromString(string pchString);

            [DllImport(LibraryName)]
            public static extern string SDL_GameControllerGetStringForAxis(GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern GameControllerButtonBind SDL_GameControllerGetBindForAxis(IntPtr gamecontroller, GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern short SDL_GameControllerGetAxis(IntPtr gamecontroller, GameControllerAxis axis);

            [DllImport(LibraryName)]
            public static extern GameControllerButton SDL_GameControllerGetButtonFromString(string pchString);

            [DllImport(LibraryName)]
            public static extern string SDL_GameControllerGetStringForButton(GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern GameControllerButtonBind SDL_GameControllerGetBindForButton(IntPtr gamecontroller, GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern byte SDL_GameControllerGetButton(IntPtr gamecontroller, GameControllerButton button);

            [DllImport(LibraryName)]
            public static extern void SDL_GameControllerClose(IntPtr gamecontroller);

            //---------------------------------------------------------------------
            // SDL_hints.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static bool SDL_SetHintWithPriority(string name, string value, HintPriority priority);

            [DllImport(LibraryName)]
            public extern static bool SDL_SetHint(string name, string value);

            [DllImport(LibraryName)]
            private extern static IntPtr SDL_GetHint(string name);

            [DllImport(LibraryName)]
            public extern static bool SDL_GetHintBoolean(string name, bool defaultValue);

            [DllImport(LibraryName)]
            public extern static void SDL_AddHintCallback(string name, HintCallback callback, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_DelHintCallback(string name, HintCallback callback, IntPtr userData);

            [DllImport(LibraryName)]
            public extern static void SDL_ClearHints();

            //---------------------------------------------------------------------
            // SDL_keyboard.h
            //---------------------------------------------------------------------

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

            //---------------------------------------------------------------------
            // SDL_loadso.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_LoadObject(byte* file);

            [DllImport(LibraryName)]
            public extern static unsafe void* SDL_LoadFunction(void* handle, byte* name);

            [DllImport(LibraryName)]
            public extern static unsafe void SDL_UnloadObject(void* handle);

            //---------------------------------------------------------------------
            // SDL_log.h
            //---------------------------------------------------------------------

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

            //---------------------------------------------------------------------
            // SDL_mouse.h
            //---------------------------------------------------------------------

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
            public extern static void SDL_WarpMouseInWindow(IntPtr window, int x, int y);

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

            //---------------------------------------------------------------------
            // Rect.h
            //---------------------------------------------------------------------

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

            //---------------------------------------------------------------------
            // SDL_render.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static int SDL_GetNumRenderDrivers();

            [DllImport(LibraryName)]
            public extern static int SDL_GetRenderDriverInfo(int index, out RendererInfo info);

            [DllImport(LibraryName)]
            public extern static int SDL_CreateWindowAndRenderer(
                                            int width, int height, WindowFlags window_flags,
                                            out IntPtr window, out IntPtr renderer);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateRenderer(IntPtr window,
                                                           int index, RendererFlags flags);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_GetRenderer(IntPtr window);

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
                                                                SDL_BlendMode blendMode);

            [DllImport(LibraryName)]
            public extern static int SDL_GetTextureBlendMode(IntPtr texture,
                                                               out SDL_BlendMode blendMode);

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
                                                                   SDL_BlendMode blendMode);

            [DllImport(LibraryName)]
            public extern static int SDL_GetRenderDrawBlendMode(IntPtr renderer,
                                                                  out SDL_BlendMode blendMode);

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

            //---------------------------------------------------------------------
            // SDL_rwops.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static IntPtr SDL_RWFromFile(string file, string mode);

            [DllImport(LibraryName)]
            public extern static int SDL_RWclose(IntPtr context);

            [DllImport(LibraryName)]
            public extern static int SDL_RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

            [DllImport(LibraryName)]
            public extern static long SDL_RWsize(IntPtr context);

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
                out SDL_BlendMode blendMode
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
                SDL_BlendMode blendMode
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

            //---------------------------------------------------------------------
            // SDL_version.h
            //---------------------------------------------------------------------

            [DllImport(LibraryName)]
            public extern static void SDL_GetVersion(out Version version);

            [DllImport(LibraryName)]
            public extern static unsafe byte* SDL_GetRevision();

            [DllImport(LibraryName)]
            public extern static int SDL_GetRevisionNumber();
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

        public const string LibraryName = "SDL2.dll";
        public const string ImageLibraryName = "SDL2_image.dll";
        public const int ScanCodeMask = (1 << 30);
        /// <summary>
        /// A variable controlling how 3D acceleration is used to accelerate the SDL screen surface.
        /// <para /> SDL can try to accelerate the SDL screen surface by using streaming textures with a 3D rendering engine.This variable controls whether and how this is done.
        /// <para /> This variable can be set to the following values:
        /// "0"       - Disable 3D acceleration
        /// "1"       - Enable 3D acceleration, using the default renderer.
        /// "X"       - Enable 3D acceleration, using X where X is one of the valid rendering drivers.  (e.g. "direct3d", "opengl", etc.)
        /// <para /> By default SDL tries to make a best guess for each platform whether to use acceleration or not.
        /// </summary>
        public const string HintFrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
        /// <summary>
        /// A variable controlling the scaling policy for <see cref="RenderSetLogicalSize"/>.
        /// <para /> This variable can be set to the following values:
        /// "0" or "letterbox" - Uses letterbox/sidebars to fit the entire rendering on screen
        /// "1" or "overscan"  - Will zoom the rendering so it fills the entire screen, allowing edges to be drawn offscreen
        /// <para /> By default letterbox is used
        /// </summary>
        public const string HintRenderLogicalSizeMode = "SDL_HINT_RENDER_LOGICAL_SIZE_MODE";

        public static readonly uint PixelFormatUnknown = 0;
        public static readonly uint PixelFormatIndex1LSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap4321, 0, 1, 0);
        public static readonly uint PixelFormatIndex1MSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap1234, 0, 1, 0);
        public static readonly uint PixelFormatIndex4LSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap4321, 0, 4, 0);
        public static readonly uint PixelFormatIndex4MSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap1234, 0, 4, 0);
        public static readonly uint PixelFormatIndex8 = DefinePixelFormat(PixelType.Index8, 0, 0, 8, 1);
        public static readonly uint PixelFormatRGB332 = DefinePixelFormat(PixelType.Packed8, PixelOrder.PackedXRGB, PackedLayout.Layout332, 8, 1);
        public static readonly uint PixelFormatRGB444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout4444, 12, 2);
        public static readonly uint PixelFormatRGB555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout1555, 15, 2);
        public static readonly uint PixelFormatBGR555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout1555, 15, 2);
        public static readonly uint PixelFormatARGB4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatRGBA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatABGR4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatBGRA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatARGB1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout1555, 16, 2);
        public static readonly uint PixelFormatRGBA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout5551, 16, 2);
        public static readonly uint PixelFormatABGR1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout1555, 16, 2);
        public static readonly uint PixelFormatBGRA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout5551, 16, 2);
        public static readonly uint PixelFormatRGB565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout565, 16, 2);
        public static readonly uint PixelFormatBGR565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout565, 16, 2);
        public static readonly uint PixelFormatRGB24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayRGB, 0, 24, 3);
        public static readonly uint PixelFormatBGR24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayBGR, 0, 24, 3);
        public static readonly uint PixelFormatRGB888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXRGB, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatRGBX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedRGBX, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatBGR888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXBGR, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatBGRX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRX, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatARGB8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatRGBA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatABGR8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedABGR, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatBGRA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRA, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatARGB2101010 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, 0, 32, 4);
        public static readonly uint PixelFormatYV12 = FourCharacterCode((byte)'Y', (byte)'V', (byte)'1', (byte)'2');
        public static readonly uint PixelFormatIYUV = FourCharacterCode((byte)'I', (byte)'Y', (byte)'U', (byte)'V');
        public static readonly uint PixelFormatYUY2 = FourCharacterCode((byte)'Y', (byte)'U', (byte)'Y', (byte)'2');
        public static readonly uint PixelFormatUYVY = FourCharacterCode((byte)'U', (byte)'Y', (byte)'V', (byte)'Y');
        public static readonly uint PixelFormatYVYU = FourCharacterCode((byte)'Y', (byte)'V', (byte)'Y', (byte)'U');
        public static readonly uint PixelFormatNV12 = FourCharacterCode((byte)'N', (byte)'V', (byte)'1', (byte)'2');
        public static readonly uint PixelFormatNV21 = FourCharacterCode((byte)'N', (byte)'V', (byte)'2', (byte)'1');

        //---------------------------------------------------------------------
        // SDL.h
        //---------------------------------------------------------------------

        /// <summary>
        /// This function initializes the subsystems specified by <paramref name="flags"/>
        /// </summary>
        public static void Init(InitFlags flags)
        {
            Native.SDL_Init(flags).CheckError("Could not initialize SDL with " + flags);
        }

        /// <summary>
        /// This function initializes specific SDL subsystems
        /// <para /> Subsystem initialization is ref-counted, you must call <see cref="QuitSubSystem(InitFlags)"/> for each <see cref="InitSubSystem(InitFlags)"/> to correctly shutdown a subsystem manually (or call <see cref="Quit"/> to force shutdown).
        /// <para /> If a subsystem is already loaded then this call will increase the ref-count and return.
        /// </summary>
        public static void InitSubSystem(InitFlags flags)
        {
            Native.SDL_InitSubSystem(flags).CheckError("Could not initialize SDL subsystem " + flags);
        }

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        public static void QuitSubSystem(InitFlags flags)
        {
            Native.SDL_QuitSubSystem(flags);
        }

        /// <summary>
        /// This function returns a mask of the specified subsystems which have previously been initialized.
        /// <para /> If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        public static InitFlags WasInit(InitFlags flags)
        {
            return Native.SDL_WasInit(flags);
        }

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
        /// </summary>
        public static void Quit()
        {
            Native.SDL_Quit();
        }

        //---------------------------------------------------------------------
        // SDL_error.h
        //---------------------------------------------------------------------

        /// <summary>
        /// Use this function to retrieve a message about the last error that occurred.
        /// </summary>
        /// <returns> Returns a message with information about the specific error that occurred, or an empty string if there hasn't been an error message set since the last call to <see cref="ClearError"/>. The message is only applicable when an SDL function has signaled an error. You must check the return values of SDL function calls to determine when to appropriately call <see cref="Get"/>. </returns>
        /// <remarks> 
        /// It is possible for multiple errors to occur before calling <see cref="GetError"/>. Only the last error is returned. 
        /// <para /> The returned string is statically allocated and must not be freed by the application.
        /// </remarks>
        public static unsafe string GetError()
        {
            return GetString(Native.SDL_GetError());
        }

        /// <summary>
        /// Use this function to clear any previous error message.
        /// </summary>
        public static void ClearError()
        {
            Native.SDL_ClearError();
        }

        //---------------------------------------------------------------------
        // SDL_pixels.h
        //---------------------------------------------------------------------

        public static uint FourCharacterCode(byte a, byte b, byte c, byte d)
        {
            return (uint)(a | (b << 8) | (c << 16) | (d << 24));
        }

        public static uint DefinePixelFormat(PixelType type, PixelOrder order, PackedLayout layout, byte bits, byte bytes)
        {
            return (uint)((1 << 28) | ((byte)type << 24) | ((byte)order << 20) | ((byte)layout << 16) | (bits << 8) | bytes);
        }

        //---------------------------------------------------------------------
        // Utility methods
        //---------------------------------------------------------------------

        public static unsafe string GetString(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return string.Empty;

            var ptr = (byte*)handle;
            return GetString(ptr);
        }

        public static unsafe string GetString(byte* ptr)
        {
            byte* counter = ptr;
            while (*counter != 0)
            {
                counter++;
            }
            int count = (int)(counter - ptr);

            return Encoding.UTF8.GetString(ptr, count);
        }
    }

    internal static class IntExtensions
    {
        public static void CheckError(this int result, string message)
        {
            Assert.IsTrue(result == 0, "[SDL] " + message + ": " + SDL.GetError());
        }
    }
}