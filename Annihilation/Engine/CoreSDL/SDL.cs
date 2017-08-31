using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Engine.System;

namespace SDL2
{
    public enum SDLModule
    {
        SDL,
        Audio,
        BlendMode,
        Clipboard,
        CpuInfo,
        Error,
        Events,
        FileSystem,
        GameController,
        Gesture,
        Haptic,
        Hints,
        Joystick,
        Keyboard,
        LoadSO,
        Log,
        MessageBox,
        Mouse,
        Pixels,
        Platform,
        Power,
        Rect,
        Render,
        Rwops,
        Shape,
        Surface,
        System,
        SysWm,
        Timer,
        Touch,
        Version,
        Video,
        Vulkan
    }

    [SuppressUnmanagedCodeSecurity]
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

        public static void LoadFunctions(SDLModule module)
        {
            switch (module)
            {
                case SDLModule.SDL:
                    _initSubSystem = LoadFunction<InitSubSystemDelegate>("SDL_InitSubSystem");
                    _quitSubSystem = LoadFunction<QuitSubSystemDelegate>("SDL_QuitSubSystem");
                    _wasInit = LoadFunction<WasInitDelegate>("SDL_WasInit");
                    _quit = LoadFunction<QuitDelegate>("SDL_Quit");
                    break;
                case SDLModule.Audio:
                    throw new NotImplementedException();
                case SDLModule.BlendMode:
                    _composeCustomBlendMode = LoadFunction<ComposeCustomBlendModeDelegate>("SDL_ComposeCustomBlendMode");
                    break;
                case SDLModule.Clipboard:
                    _setClipboardstring = LoadFunction<SetClipboardstringDelegate>("SDL_SetClipboardstring");
                    _getClipboardstring = LoadFunction<GetClipboardstringDelegate>("SDL_GetClipboardstring");
                    _hasClipboardstring = LoadFunction<HasClipboardstringDelegate>("SDL_HasClipboardstring");
                    break;
                case SDLModule.CpuInfo:
                    _getCpuCount = LoadFunction<GetCpuCountDelegate>("SDL_GetCPUCount");
                    _getCpuCacheLineSize = LoadFunction<GetCpuCacheLineSizeDelegate>("SDL_GetCPUCacheLineSize");
                    _hasRdtsc = LoadFunction<HasRdtscDelegate>("SDL_HasRDTSC");
                    _hasAltiVec = LoadFunction<HasAltiVecDelegate>("SDL_HasAltiVec");
                    _hasMmx = LoadFunction<HasMmxDelegate>("SDL_HasMMX");
                    _has3DNow = LoadFunction<Has3DNowDelegate>("SDL_Has3DNow");
                    _hasSse = LoadFunction<HasSseDelegate>("SDL_HasSSE");
                    _hasSse2 = LoadFunction<HasSse2Delegate>("SDL_HasSSE2");
                    _hasSse3 = LoadFunction<HasSse3Delegate>("SDL_HasSSE3");
                    _hasSse41 = LoadFunction<HasSse41Delegate>("SDL_HasSSE41");
                    _hasSse42 = LoadFunction<HasSse42Delegate>("SDL_HasSSE42");
                    _hasAvx = LoadFunction<HasAvxDelegate>("SDL_HasAVX");
                    _hasAvx2 = LoadFunction<HasAvx2Delegate>("SDL_HasAVX2");
                    _hasNeon = LoadFunction<HasNeonDelegate>("SDL_HasNEON");
                    _getSystemRam = LoadFunction<GetSystemRamDelegate>("SDL_GetSystemRAM");
                    break;
                case SDLModule.Error:
                    _setError = LoadFunction<SetErrorDelegate>("SDL_SetError");
                    _getError = LoadFunction<GetErrorDelegate>("SDL_GetError");
                    _clearError = LoadFunction<ClearErrorDelegate>("SDL_ClearError");
                    break;
                case SDLModule.Events:
                    _pumpEvents = LoadFunction<PumpEventsDelegate>("SDL_PumpEvents");
                    _peepEvents = LoadFunction<PeepEventsDelegate>("SDL_PeepEvents");
                    _hasEvent = LoadFunction<HasEventDelegate>("SDL_HasEvent");
                    _hasEvents = LoadFunction<HasEventsDelegate>("SDL_HasEvents");
                    _flushEvent = LoadFunction<FlushEventDelegate>("SDL_FlushEvent");
                    _flushEvents = LoadFunction<FlushEventsDelegate>("SDL_FlushEvents");
                    _pollEvent = LoadFunction<PollEventDelegate>("SDL_PollEvent");
                    _waitEvent = LoadFunction<WaitEventDelegate>("SDL_WaitEvent");
                    _waitEventTimeout = LoadFunction<WaitEventTimeoutDelegate>("SDL_WaitEventTimeout");
                    _pushEvent = LoadFunction<PushEventDelegate>("SDL_PushEvent");
                    _setEventFilter = LoadFunction<SetEventFilterDelegate>("SDL_SetEventFilter");
                    _getEventFilter = LoadFunction<GetEventFilterDelegate>("SDL_GetEventFilter");
                    _addEventWatch = LoadFunction<AddEventWatchDelegate>("SDL_AddEventWatch");
                    _delEventWatch = LoadFunction<DelEventWatchDelegate>("SDL_DelEventWatch");
                    _filterEvents = LoadFunction<FilterEventsDelegate>("SDL_FilterEvents");
                    _eventState = LoadFunction<EventStateDelegate>("SDL_EventState");
                    _registerEvents = LoadFunction<RegisterEventsDelegate>("SDL_RegisterEvents");
                    break;
                case SDLModule.FileSystem:
                    _getBasePath = LoadFunction<GetBasePathDelegate>("SDL_GetBasePath");
                    _getPrefPath = LoadFunction<GetPrefPathDelegate>("SDL_GetPrefPath");
                    break;
                case SDLModule.GameController:
                    _gameControllerAddMappingsFromRW = LoadFunction<GameControllerAddMappingsFromRWDelegate>("SDL_GameControllerAddMappingsFromRW");
                    _gameControllerAddMapping = LoadFunction<GameControllerAddMappingDelegate>("SDL_GameControllerAddMapping");
                    _gameControllerNumMappings = LoadFunction<GameControllerNumMappingsDelegate>("SDL_GameControllerNumMappings");
                    _gameControllerMappingForIndex = LoadFunction<GameControllerMappingForIndexDelegate>("SDL_GameControllerMappingForIndex");
                    _gameControllerMappingForGuid = LoadFunction<GameControllerMappingForGuidDelegate>("SDL_GameControllerMappingForGUID");
                    _gameControllerMapping = LoadFunction<GameControllerMappingDelegate>("SDL_GameControllerMapping");
                    _isGameController = LoadFunction<IsGameControllerDelegate>("SDL_IsGameController");
                    _gameControllerNameForIndex = LoadFunction<GameControllerNameForIndexDelegate>("SDL_GameControllerNameForIndex");
                    _gameControllerOpen = LoadFunction<GameControllerOpenDelegate>("SDL_GameControllerOpen");
                    _gameControllerFromInstanceID = LoadFunction<GameControllerFromInstanceIDDelegate>("SDL_GameControllerFromInstanceID");
                    _gameControllerName = LoadFunction<GameControllerNameDelegate>("SDL_GameControllerName");
                    _gameControllerGetVendor = LoadFunction<GameControllerGetVendorDelegate>("SDL_GameControllerGetVendor");
                    _gameControllerGetProduct = LoadFunction<GameControllerGetProductDelegate>("SDL_GameControllerGetProduct");
                    _gameControllerGetProductVersion = LoadFunction<GameControllerGetProductVersionDelegate>("SDL_GameControllerGetProductVersion");
                    _gameControllerGetAttached = LoadFunction<GameControllerGetAttachedDelegate>("SDL_GameControllerGetAttached");
                    _gameControllerGetJoystick = LoadFunction<GameControllerGetJoystickDelegate>("SDL_GameControllerGetJoystick");
                    _gameControllerEventState = LoadFunction<GameControllerEventStateDelegate>("SDL_GameControllerEventState");
                    _gameControllerUpdate = LoadFunction<GameControllerUpdateDelegate>("SDL_GameControllerUpdate");
                    _gameControllerGetAxisFromString = LoadFunction<GameControllerGetAxisFromStringDelegate>("SDL_GameControllerGetAxisFromString");
                    _gameControllerGetStringForAxis = LoadFunction<GameControllerGetStringForAxisDelegate>("SDL_GameControllerGetStringForAxis");
                    _gameControllerGetBindForAxis = LoadFunction<GameControllerGetBindForAxisDelegate>("SDL_GameControllerGetBindForAxis");
                    _gameControllerGetAxis = LoadFunction<GameControllerGetAxisDelegate>("SDL_GameControllerGetAxis");
                    _gameControllerGetButtonFromString = LoadFunction<GameControllerGetButtonFromStringDelegate>("SDL_GameControllerGetButtonFromString");
                    _gameControllerGetStringForButton = LoadFunction<GameControllerGetStringForButtonDelegate>("SDL_GameControllerGetStringForButton");
                    _gameControllerGetBindForButton = LoadFunction<GameControllerGetBindForButtonDelegate>("SDL_GameControllerGetBindForButton");
                    _gameControllerGetButton = LoadFunction<GameControllerGetButtonDelegate>("SDL_GameControllerGetButton");
                    _gameControllerClose = LoadFunction<GameControllerCloseDelegate>("SDL_GameControllerClose");
                    break;
                case SDLModule.Gesture:
                    throw new NotImplementedException();
                case SDLModule.Haptic:
                    throw new NotImplementedException();
                case SDLModule.Hints:
                    _setHintWithPriority = LoadFunction<SetHintWithPriorityDelegate>("SDL_SetHintWithPriority");
                    _setHint = LoadFunction<SetHintDelegate>("SDL_SetHint");
                    _getHint = LoadFunction<GetHintDelegate>("SDL_GetHint");
                    _getHintBoolean = LoadFunction<GetHintBooleanDelegate>("SDL_GetHintBoolean");                    _addHintCallback = LoadFunction<AddHintCallbackDelegate>("SDL_AddHintCallback");
                    _delHintCallback = LoadFunction<DelHintCallbackDelegate>("SDL_DelHintCallback");                    _clearHints = LoadFunction<ClearHintsDelegate>("SDL_ClearHints");
                    break;
                case SDLModule.Joystick:
                    _numJoysticks = LoadFunction<NumJoysticksDelegate>("SDL_NumJoysticks");
                    _joystickNameForIndex = LoadFunction<JoystickNameForIndexDelegate>("SDL_JoystickNameForIndex");
                    _joystickGetDeviceGuid = LoadFunction<JoystickGetDeviceGuidDelegate>("SDL_JoystickGetDeviceGUID");
                    _joystickGetDeviceVendor = LoadFunction<JoystickGetDeviceVendorDelegate>("SDL_JoystickGetDeviceVendor");
                    _joystickGetDeviceProduct = LoadFunction<JoystickGetDeviceProductDelegate>("SDL_JoystickGetDeviceProduct");
                    _joystickGetDeviceProductVersion = LoadFunction<JoystickGetDeviceProductVersionDelegate>("SDL_JoystickGetDeviceProductVersion");
                    _joystickGetDeviceType = LoadFunction<JoystickGetDeviceTypeDelegate>("SDL_JoystickGetDeviceType");
                    _joystickGetDeviceInstanceID = LoadFunction<JoystickGetDeviceInstanceIDDelegate>("SDL_JoystickGetDeviceInstanceID");
                    _joystickOpen = LoadFunction<JoystickOpenDelegate>("SDL_JoystickOpen");
                    _joystickFromInstanceID = LoadFunction<JoystickFromInstanceIDDelegate>("SDL_JoystickFromInstanceID");
                    _joystickName = LoadFunction<JoystickNameDelegate>("SDL_JoystickName");
                    _joystickGetGuid = LoadFunction<JoystickGetGuidDelegate>("SDL_JoystickGetGUID");
                    _joystickGetVendor = LoadFunction<JoystickGetVendorDelegate>("SDL_JoystickGetVendor");
                    _joystickGetProduct = LoadFunction<JoystickGetProductDelegate>("SDL_JoystickGetProduct");
                    _joystickGetProductVersion = LoadFunction<JoystickGetProductVersionDelegate>("SDL_JoystickGetProductVersion");
                    _joystickGetType = LoadFunction<JoystickGetTypeDelegate>("SDL_JoystickGetType");
                    _joystickGetGuidString = LoadFunction<JoystickGetGuidStringDelegate>("SDL_JoystickGetGUIDString");
                    _joystickGetGuidFromString = LoadFunction<JoystickGetGuidFromStringDelegate>("SDL_JoystickGetGUIDFromString");
                    _joystickGetAttached = LoadFunction<JoystickGetAttachedDelegate>("SDL_JoystickGetAttached");
                    _joystickInstanceID = LoadFunction<JoystickInstanceIDDelegate>("SDL_JoystickInstanceID");
                    _joystickNumAxes = LoadFunction<JoystickNumAxesDelegate>("SDL_JoystickNumAxes");
                    _joystickNumBalls = LoadFunction<JoystickNumBallsDelegate>("SDL_JoystickNumBalls");
                    _joystickNumHats = LoadFunction<JoystickNumHatsDelegate>("SDL_JoystickNumHats");
                    _joystickNumButtons = LoadFunction<JoystickNumButtonsDelegate>("SDL_JoystickNumButtons");
                    _joystickUpdate = LoadFunction<JoystickUpdateDelegate>("SDL_JoystickUpdate");
                    _joystickEventState = LoadFunction<JoystickEventStateDelegate>("SDL_JoystickEventState");
                    _joystickGetAxis = LoadFunction<JoystickGetAxisDelegate>("SDL_JoystickGetAxis");
                    _joystickGetAxisInitialState = LoadFunction<JoystickGetAxisInitialStateDelegate>("SDL_JoystickGetAxisInitialState");
                    _joystickGetHat = LoadFunction<JoystickGetHatDelegate>("SDL_JoystickGetHat");
                    _joystickGetBall = LoadFunction<JoystickGetBallDelegate>("SDL_JoystickGetBallDelegate");
                    _joystickGetButton = LoadFunction<JoystickGetButtonDelegate>("SDL_JoystickGetButton");
                    _joystickClose = LoadFunction<JoystickCloseDelegte>("SDL_JoystickClose");
                    _joystickCurrentPowerLevel = LoadFunction<JoystickCurrentPowerLevelDelegate>("SDL_JoystickCurrentPowerLevel");
                    break;
                case SDLModule.Keyboard:
                    _getKeyboardFocus = LoadFunction<GetKeyboardFocusDelegate>("SDL_GetKeyboardFocus");
                    _getKeyboardState = LoadFunction<GetKeyboardStateDelegate>("SDL_GetKeyboardState");
                    _getModState = LoadFunction<GetModStateDelegate>("SDL_GetModState");
                    _setModState = LoadFunction<SetModStateDelegate>("SDL_SetModState");
                    _getKeyFromScancode = LoadFunction<GetKeyFromScancodeDelegate>("SDL_GetKeyFromScancode");
                    _getScancodeFromKey = LoadFunction<GetScancodeFromKeyDelegate>("SDL_GetScancodeFromKey");
                    _getScancodeName = LoadFunction<GetScancodeNameDelegate>("SDL_GetScancodeName");
                    _getScancodeFromName = LoadFunction<GetScancodeFromNameDelegate>("SDL_GetScancodeFromName");
                    _getKeyName = LoadFunction<GetKeyNameDelegate>("SDL_GetKeyName");
                    _getKeyFromName = LoadFunction<GetKeyFromNameDelegate>("SDL_GetKeyFromName");
                    _startTextInput = LoadFunction<StartTextInputDelegate>("SDL_StartTextInput");
                    _isTextInputActive = LoadFunction<IsTextInputActiveDelegate>("SDL_IsTextInputActive");
                    _stopTextInput = LoadFunction<StopTextInputDelegate>("SDL_StopTextInput");
                    _setTextInputRect = LoadFunction<SetTextInputRectDelegate>("SDL_SetTextInputRect");
                    _hasScreenKeyboardSupport = LoadFunction<HasScreenKeyboardSupportDelegate>("SDL_HasScreenKeyboardSupport");
                    _isScreenKeyboardShown = LoadFunction<IsScreenKeyboardShownDelegate>("SDL_IsScreenKeyboardShown");
                    break;
                case SDLModule.LoadSO:
                    _loadObject = LoadFunction<LoadObjectDelegate>("SDL_LoadObject");
                    _loadFunction = LoadFunction<LoadFunctionDelegate>("SDL_LoadFunction");
                    _unloadObject = LoadFunction<UnloadObjectDelegate>("SDL_UnloadObject");
                    break;
                case SDLModule.Log:
                    _logSetAllPriority = LoadFunction<LogSetAllPriorityDelegate>("SDL_LogSetAllPriority");
                    _logSetPriority = LoadFunction<LogSetPriorityDelegate>("SDL_LogSetPriority");
                    _logGetPriority = LoadFunction<LogGetPriorityDelegate>("SDL_LogGetPriority");
                    _logResetPriorities = LoadFunction<LogResetPrioritiesDelegate>("SDL_LogResetPriorities");
                    _log = LoadFunction<LogDelegate>("SDL_Log");
                    _logVerbose = LoadFunction<LogVerboseDelegate>("SDL_LogVerbose");
                    _logDebug = LoadFunction<LogDebugDelegate>("SDL_LogDebug");
                    _logInfo = LoadFunction<LogInfoDelegate>("SDL_LogInfo");
                    _logWarn = LoadFunction<LogWarnDelegate>("SDL_LogWarn");
                    _logError = LoadFunction<LogErrorDelegate>("SDL_LogError");
                    _logCritical = LoadFunction<LogCriticalDelegate>("SDL_LogCritical");
                    _logMessage = LoadFunction<LogMessageDelegate>("SDL_LogMessage");
                    _logGetOutputFunction = LoadFunction<LogGetOutputFunctionDelegate>("SDL_LogGetOutputFunction");
                    _logSetOutputFunction = LoadFunction<LogSetOutputFunctionDelegate>("SDL_LogSetOutputFunction");
                    break;
                case SDLModule.MessageBox:
                    _showMessageBox = LoadFunction<ShowMessageBoxDelegate>("SDL_ShowMessageBox");
                    _showSimpleMessageBox = LoadFunction<ShowSimpleMessageBoxDelegate>("SDL_ShowSimpleMessageBox");
                    break;
                case SDLModule.Mouse:
                    _getMouseFocus = LoadFunction<GetMouseFocusDelegate>("SDL_GetMouseFocus");
                    _getMouseState = LoadFunction<GetMouseStateDelegate>("SDL_GetMouseState");
                    _getGlobalMouseState = LoadFunction<GetGlobalMouseStateDelegate>("SDL_GetGlobalMouseState");
                    _getRelativeMouseState = LoadFunction<GetRelativeMouseStateDelegate>("SDL_GetRelativeMouseState");
                    _warpMouseInWindow = LoadFunction<WarpMouseInWindowDelegate>("SDL_WarpMouseInWindow");
                    _warpMouseGlobal = LoadFunction<WarpMouseGlobalDelegate>("SDL_WarpMouseGlobal");
                    _setRelativeMouseMode = LoadFunction<SetRelativeMouseModeDelegate>("SDL_SetRelativeMouseMode");
                    _captureMouse = LoadFunction<CaptureMouseDelegate>("SDL_CaptureMouse");
                    _getRelativeMouseMode = LoadFunction<GetRelativeMouseModeDelegate>("SDL_GetRelativeMouseMode");
                    _createCursor = LoadFunction<CreateCursorDelegate>("SDL_CreateCursor");
                    _createColorCursor = LoadFunction<CreateColorCursorDelegate>("SDL_CreateColorCursor");
                    _createSystemCursor = LoadFunction<CreateSystemCursorDelegate>("SDL_CreateSystemCursor");
                    _setCursor = LoadFunction<SetCursorDelegate>("SDL_SetCursor");
                    _getCursor = LoadFunction<GetCursorDelegate>("SDL_GetCursor");
                    _getDefaultCursor = LoadFunction<GetDefaultCursorDelegate>("SDL_GetDefaultCusror");
                    _freeCursor = LoadFunction<FreeCursorDelegate>("SDL_FreeCursor");
                    _showCursor = LoadFunction<ShowCursorDelegate>("SDL_ShowCursor");
                    break;
                case SDLModule.Pixels:
                    _getPixelFormatName = LoadFunction<GetPixelFormatNameDelegate>("SDL_GetPixelFormatName");
                    _pixelFormatEnumToMasks = LoadFunction<PixelFormatEnumToMasksDelegate>("SDL_PixelFormatEnumToMasks");
                    _masksToPixelFormatEnum = LoadFunction<MasksToPixelFormatEnumDelegate>("SDL_MasksToPixelFormatEnum");
                    _allocFormat = LoadFunction<AllocFormatDelegate>("SDL_AllocFormat");
                    _freeFormat = LoadFunction<FreeFormatDelegate>("SDL_FreeFormat");
                    _allocPalette = LoadFunction<AllocPaletteDelegate>("SDL_AllocPalette");
                    _setPixelFormatPalette = LoadFunction<SetPixelFormatPaletteDelegate>("SDL_SetPixelFormatPalette");
                    _setPaletteColors = LoadFunction<SetPaletteColorsDelegate>("SDL_SetPaletteColors");
                    _freePalette = LoadFunction<FreePaletteDelegate>("SDL_FreePalette");
                    _mapRgb = LoadFunction<MapRgbDelegate>("SDL_MapRGB");
                    _mapRgba = LoadFunction<MapRgbaDelegate>("SDL_MapRGBA");
                    _getRgb = LoadFunction<GetRgbDelegate>("SDL_GetRGB");
                    _getRgba = LoadFunction<GetRgbaDelegate>("SDL_GetRGBA");
                    _calculateGammaRamp = LoadFunction<CalculateGammaRampDelegate>("SDL_CalculateGammaRamp");
                    break;
                case SDLModule.Platform:
                    _getPlatform = LoadFunction<GetPlatformDelegate>("SDL_GetPlatform");
                    break;
                case SDLModule.Power:
                    _getPowerInfo = LoadFunction<GetPowerInfoDelegate>("SDL_GetPowerInfo");
                    break;
                case SDLModule.Rect:
                    _hasIntersection = LoadFunction<HasIntersectionDelegate>("SDL_HasIntersection");
                    _intersectRect = LoadFunction<IntersectRectDelegate>("SDL_IntersectRect");
                    _unionRect = LoadFunction<UnionRectDelegate>("SDL_UnionRect");
                    _enclosePoints = LoadFunction<EnclosePointsDelegate>("SDL_EnclosePoints");
                    _intersectRectAndLine = LoadFunction<IntersectRectAndLineDelegate>("SDL_IntersectRectAndLine");
                    break;
                case SDLModule.Render:
                    _getNumRenderDrivers = LoadFunction<GetNumRenderDriversDelegate>("SDL_GetNumRenderDrivers");
                    _getRenderDriverInfo = LoadFunction<GetRenderDriverInfoDelegate>("SDL_GetRenderDriverInfo");
                    _createWindowAndRenderer = LoadFunction<CreateWindowAndRendererDelegate>("SDL_CreateWindowAndRenderer");
                    _createRenderer = LoadFunction<CreateRendererDelegate>("SDL_CreateRenderer");
                    _createSoftwareRenderer = LoadFunction<CreateSoftwareRendererDelegate>("SDL_CreateSoftwareRenderer");
                    _getRenderer = LoadFunction<GetRendererDelegate>("SDL_GetRenderer");
                    _getRendererInfo = LoadFunction<GetRendererInfoDelegate>("SDL_GetRendererInfo");
                    _getRendererOutputSize = LoadFunction<GetRendererOutputSizeDelegate>("SDL_GetRendererOutputSize");
                    _createTexture = LoadFunction<CreateTextureDelegate>("SDL_CreateTexture");
                    _createTextureFromSurface = LoadFunction<CreateTextureFromSurfaceDelegate>("SDL_CreateTextureFromSurface");
                    _queryTexture = LoadFunction<QueryTextureDelegate>("SDL_QueryTexture");
                    _setTextureColorMod = LoadFunction<SetTextureColorModDelegate>("SDL_SetTextureColorMod");
                    _getTextureColorMod = LoadFunction<GetTextureColorModDelegate>("SDL_GetTextureColorMod");
                    _setTextureAlphaMod = LoadFunction<SetTextureAlphaModDelegate>("SDL_SetTextureAlphaMod");
                    _getTextureAlphaMod = LoadFunction<GetTextureAlphaModDelegate>("SDL_GetTextureAlphaMod");
                    _setTextureBlendMode = LoadFunction<SetTextureBlendModeDelegate>("SDL_SetTextureBlendMode");
                    _getTextureBlendMode = LoadFunction<GetTextureBlendModeDelegate>("SDL_GetTextureBlendMode");
                    _updateTexture = LoadFunction<UpdateTextureDelegate>("SDL_UpdateTexture");
                    _updateYUVTexture = LoadFunction<UpdateYUVTextureDelegate>("SDL_UpdateYUVTexture");
                    _lockTexture = LoadFunction<LockTextureDelegate>("SDL_LockTexture");
                    _unlockTexture = LoadFunction<UnlockTextureDelegate>("SDL_UnlockTexture");
                    _renderTargetSupported = LoadFunction<RenderTargetSupportedDelegate>("SDL_RenderTargetSupported");
                    _setRenderTarget = LoadFunction<SetRenderTargetDelegate>("SDL_SetRenderTarget");
                    _getRenderTarget = LoadFunction<GetRenderTargetDelegate>("SDL_GetRenderTarget");
                    _renderSetLogicalSize = LoadFunction<RenderSetLogicalSizeDelegate>("SDL_RenderSetLogicalSize");
                    _renderGetLogicalSize = LoadFunction<RenderGetLogicalSizeDelegate>("SDL_RenderGetLogicalSize");
                    _renderSetIntegerScale = LoadFunction<RenderSetIntegerScaleDelegate>("SDL_RenderSetIntegerScale");
                    _renderGetIntegerScale = LoadFunction<RenderGetIntegerScaleDelegate>("SDL_RenderGetIntegerScale");
                    _renderSetViewport = LoadFunction<RenderSetViewportDelegate>("SDL_RenderSetViewport");
                    _renderGetViewport = LoadFunction<RenderGetViewportDelegate>("SDL_RenderGetViewport");
                    _renderSetClipRect = LoadFunction<RenderSetClipRectDelegate>("SDL_RenderSetClipRect");
                    _renderGetClipRect = LoadFunction<RenderGetClipRectDelegate>("SDL_RenderGetClipRect");
                    _renderIsClipEnabled = LoadFunction<RenderIsClipEnabledDelegate>("SDL_RenderIsClipEnabled");
                    _renderSetScale = LoadFunction<RenderSetScaleDelegate>("SDL_RenderSetScale");
                    _renderGetScale = LoadFunction<RenderGetScaleDelegate>("SDL_RenderGetScale");
                    _setRenderDrawColor = LoadFunction<SetRenderDrawColorDelegate>("SDL_SetRenderDrawColor");
                    _getRenderDrawColor = LoadFunction<GetRenderDrawColorDelegate>("SDL_GetRenderDrawColor");
                    _setRenderDrawBlendMode = LoadFunction<SetRenderDrawBlendModeDelegate>("SDL_SetRenderDrawBlendMode");
                    _getRenderDrawBlendMode = LoadFunction<GetRenderDrawBlendModeDelegate>("SDL_GetRenderDrawBlendMode");
                    _renderClear = LoadFunction<RenderClearDelegate>("SDL_RenderClear");
                    _renderDrawPoint = LoadFunction<RenderDrawPointDelegate>("SDL_RenderDrawPoint");
                    _renderDrawPoints = LoadFunction<RenderDrawPointsDelegate>("SDL_RenderDrawPoints");
                    _renderDrawLine = LoadFunction<RenderDrawLineDelegate>("SDL_RenderDrawLine");
                    _renderDrawLines = LoadFunction<RenderDrawLinesDelegate>("SDL_RenderDrawLines");
                    _renderDrawRect = LoadFunction<RenderDrawRectDelegate>("SDL_RenderDrawRect");
                    _renderDrawRects = LoadFunction<RenderDrawRectsDelegate>("SDL_RenderDrawRects");
                    _renderFillRect = LoadFunction<RenderFillRectDelegate>("SDL_RenderFillRect");
                    _renderFillRects = LoadFunction<RenderFillRectsDelegate>("SDL_RenderFillRects");
                    _renderCopy = LoadFunction<RenderCopyDelegate>("SDL_RenderCopy");
                    _renderCopyEx = LoadFunction<RenderCopyExDelegate>("SDL_RenderCopyEx");
                    _renderReadPixels = LoadFunction<RenderReadPixelsDelegate>("SDL_RenderReadPixels");
                    _renderPresent = LoadFunction<RenderPresentDelegate>("SDL_RenderPresent");
                    _destroyTexture = LoadFunction<DestroyTextureDelegate>("SDL_DestroyTexture");
                    _destroyRenderer = LoadFunction<DestroyRendererDelegate>("SDL_DestroyRenderer");
                    break;
                case SDLModule.Rwops:
                    _rwFromFile = LoadFunction<RWFromFileDelegate>("SDL_RWFromFile");
                    break;
                case SDLModule.Shape:
                    _createShapedWindow = LoadFunction<CreateShapedWindowDelegate>("SDL_CreateShapedWindow");
                    _isShapedWindow = LoadFunction<IsShapedWindowDelegate>("SDL_IsShapedWindow");
                    _setWindowShape = LoadFunction<SetWindowShapeDelegate>("SDL_SetWindowShape");
                    _getShapedWindowMode = LoadFunction<GetShapedWindowModeDelegate>("SDL_GetShapedWindowMode");
                    break;
                case SDLModule.Surface:

                    break;
                case SDLModule.System:
                    throw new NotImplementedException();
                case SDLModule.SysWm:
                    _getWindowWMInfo = LoadFunction<GetWindowWMInfoDelegate>("SDL_GetWindowWMInfo");
                    break;
                case SDLModule.Timer:
                    _getTicks = LoadFunction<GetTicksDelegate>("SDL_GetTicks");
                    _getPerformanceCounter = LoadFunction<GetPerformanceCounterDelegate>("SDL_GetPerformanceCounter");
                    _getPerformanceFrequency = LoadFunction<GetPerformanceFrequencyDelegate>("SDL_GetPerformanceFrequency");
                    _delay = LoadFunction<DelayDelegate>("SDL_Delay");
                    _addTimer = LoadFunction<AddTimerDelegate>("SDL_AddTimer");
                    _removeTimer = LoadFunction<RemoveTimerDelegate>("SDL_RemoveTimer");
                    break;
                case SDLModule.Touch:
                    throw new NotImplementedException();
                case SDLModule.Version:
                    _getVersion = LoadFunction<GetVersionDelegate>("SDL_GetVersion");
                    _getRevision = LoadFunction<GetRevisionDelegate>("SDL_GetRevision");
                    _getRevisionNumber = LoadFunction<GetRevisionNumberDelegate>("SDL_GetRevisionNumber");
                    break;
                case SDLModule.Video:

                    break;
                case SDLModule.Vulkan:
                    _vulkanLoadLibrary = LoadFunction<VulkanLoadLibraryDelegate>("SDL_Vulkan_LoadLibrary");
                    _vulkanGetVkGetInstanceProcAddr = LoadFunction<VulkanGetVkGetInstanceProcAddrDelegate>("SDL_Vulkan_GetVkGetInstanceProcAddr");
                    _vulkanUnloadLibrary = LoadFunction<VulkanUnloadLibraryDelegate>("SDL_Vulkan_UnloadLibrary");
                    _vulkanGetInstanceExtensions = LoadFunction<VulkanGetInstanceExtensionsDelegate>("SDL_Vulkan_GetInstanceExtensions");
                    _vulkanCreateSurface = LoadFunction<VulkanCreateSurfaceDelegate>("SDL_Vulkan_CreateSurface");
                    _vulkanGetDrawableSize = LoadFunction<VulkanGetDrawableSizeDelegate>("SDL_Vulkan_GetDrawableSize");
                    break;
            }
        }

        //
        // SDL.h
        //
        private delegate int InitDelegate(InitFlags flags);
        private static InitDelegate _init;
        public static int Init(InitFlags flags) => _init(flags);

        private delegate int InitSubSystemDelegate(InitFlags flags);
        private static InitSubSystemDelegate _initSubSystem;
        public static int InitSubSystem(InitFlags flags) => _initSubSystem(flags);

        private delegate void QuitSubSystemDelegate(InitFlags flags);
        private static QuitSubSystemDelegate _quitSubSystem;
        public static void QuitSubSystem(InitFlags flags) => _quitSubSystem(flags);

        private delegate InitFlags WasInitDelegate(InitFlags flags);
        private static WasInitDelegate _wasInit;
        public static InitFlags WasInit(InitFlags flags) => _wasInit(flags);

        private delegate void QuitDelegate();
        private static QuitDelegate _quit;
        public static void Quit() => _quit();

        // TODO: SDL_audio.h

        //
        // SDL_blendmode.h
        //
        private delegate BlendMode ComposeCustomBlendModeDelegate(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation);
        private static ComposeCustomBlendModeDelegate _composeCustomBlendMode;
        public static BlendMode ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation) => _composeCustomBlendMode(srcColorFactor, dstColorFactor, colorOperation, srcAlphaFactor, dstAlphaFactor, alphaOperation);

        //
        // SDL_clipboard.h
        //
        private delegate int SetClipboardstringDelegate(string text);
        private static SetClipboardstringDelegate _setClipboardstring;
        public static int SetClipboardstring(string text) => _setClipboardstring(text);

        private delegate string GetClipboardstringDelegate();
        private static GetClipboardstringDelegate _getClipboardstring;
        public static string GetClipboardstring() => _getClipboardstring();

        private delegate bool HasClipboardstringDelegate();
        private static HasClipboardstringDelegate _hasClipboardstring;
        public static bool HasClipboardstring() => _hasClipboardstring();

        //
        // SDL_cpuinfo.h
        //
        private delegate int GetCpuCountDelegate();
        private static GetCpuCountDelegate _getCpuCount;
        public static int GetCpuCount() => _getCpuCount();

        private delegate int GetCpuCacheLineSizeDelegate();
        private static GetCpuCacheLineSizeDelegate _getCpuCacheLineSize;
        public static int GetCpuCacheLineSize() => _getCpuCacheLineSize();

        private delegate bool HasRdtscDelegate();
        private static HasRdtscDelegate _hasRdtsc;
        public static bool HasRdtsc() => _hasRdtsc();

        private delegate bool HasAltiVecDelegate();
        private static HasAltiVecDelegate _hasAltiVec;
        public static bool HasAltiVec() => _hasAltiVec();

        private delegate bool HasMmxDelegate();
        private static HasMmxDelegate _hasMmx;
        public static bool HasMmx() => _hasMmx();

        private delegate bool Has3DNowDelegate();
        private static Has3DNowDelegate _has3DNow;
        public static bool Has3DNow() => _has3DNow();

        private delegate bool HasSseDelegate();
        private static HasSseDelegate _hasSse;
        public static bool HasSse() => _hasSse();

        private delegate bool HasSse2Delegate();
        private static HasSse2Delegate _hasSse2;
        public static bool HasSse2() => _hasSse2();

        private delegate bool HasSse3Delegate();
        private static HasSse3Delegate _hasSse3;
        public static bool HasSse3() => _hasSse3();

        private delegate bool HasSse41Delegate();
        private static HasSse41Delegate _hasSse41;
        public static bool HasSse41() => _hasSse41();

        private delegate bool HasSse42Delegate();
        private static HasSse42Delegate _hasSse42;
        public static bool HasSse42() => _hasSse42();

        private delegate bool HasAvxDelegate();
        private static HasAvxDelegate _hasAvx;
        public static bool HasAvx() => _hasAvx();

        private delegate bool HasAvx2Delegate();
        private static HasAvx2Delegate _hasAvx2;
        public static bool HasAvx2() => _hasAvx2();

        private delegate bool HasNeonDelegate();
        private static HasNeonDelegate _hasNeon;
        public static bool HasNeon() => _hasNeon();

        private delegate int GetSystemRamDelegate();
        private static GetSystemRamDelegate _getSystemRam;
        public static int GetSystemRam() => _getSystemRam();

        //
        // SDL_error.h
        //
        private delegate string GetErrorDelegate();
        private static GetErrorDelegate _getError;
        public static string GetError() => _getError();

        private delegate int SetErrorDelegate(string format, params object[] objects);
        private static SetErrorDelegate _setError;
        public static int SetError(string format, params object[] objects) => _setError(format, objects);

        private delegate void ClearErrorDelegate();
        private static ClearErrorDelegate _clearError;
        public static void ClearError() => _clearError();

        //
        // SDL_events.h
        //
        private delegate void PumpEventsDelegate();
        private static PumpEventsDelegate _pumpEvents;
        public static void PumpEvents() => _pumpEvents();

        private delegate int PeepEventsDelegate(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType);
        private static PeepEventsDelegate _peepEvents;
        public static int PeepEvents(Event[] events, int numEvents, EventAction action, EventType minType, EventType maxType) => _peepEvents(events, numEvents, action, minType, maxType);

        private delegate bool HasEventDelegate(EventType type);
        private static HasEventDelegate _hasEvent;
        public static bool HasEvent(EventType type) => _hasEvent(type);

        private delegate bool HasEventsDelegate(EventType minType, EventType maxType);
        private static HasEventsDelegate _hasEvents;
        public static bool HasEvents(EventType minType, EventType maxType) => _hasEvents(minType, maxType);

        private delegate void FlushEventDelegate(EventType type);
        private static FlushEventDelegate _flushEvent;
        public static void FlushEvent(EventType type) => _flushEvent(type);

        private delegate void FlushEventsDelegate(EventType minType, EventType maxType);
        private static FlushEventsDelegate _flushEvents;
        public static void FlushEvents(EventType minType, EventType maxType) => _flushEvents(minType, maxType);

        private delegate int PollEventDelegate(out Event sdlEvent);
        private static PollEventDelegate _pollEvent;
        public static int PollEvent(out Event sdlEvent) => _pollEvent(out sdlEvent);

        private delegate int WaitEventDelegate(out Event sdlEvent);
        private static WaitEventDelegate _waitEvent;
        public static int WaitEvent(out Event sdlEvent) => _waitEvent(out sdlEvent);

        private delegate int WaitEventTimeoutDelegate(out Event sdlEvent, int timeout);
        private static WaitEventTimeoutDelegate _waitEventTimeout;
        public static int WaitEventTimeout(out Event sdlEvent, int timeout) => _waitEventTimeout(out sdlEvent, timeout);

        private delegate int PushEventDelegate(ref Event sdlEvent);
        private static PushEventDelegate _pushEvent;
        public static int PushEvent(ref Event sdlEvent) => _pushEvent(ref sdlEvent);

        private delegate void SetEventFilterDelegate(EventFilter filter, IntPtr userData);
        private static SetEventFilterDelegate _setEventFilter;
        public static void SetEventFilter(EventFilter filter, IntPtr userData) => _setEventFilter(filter, userData);

        private delegate bool GetEventFilterDelegate(out EventFilter filter, IntPtr userData);
        private static GetEventFilterDelegate _getEventFilter;
        public static bool GetEventFilter(out EventFilter filter, IntPtr userData) => _getEventFilter(out filter, userData);

        private delegate void AddEventWatchDelegate(EventFilter filter, IntPtr userData);
        private static AddEventWatchDelegate _addEventWatch;
        public static void AddEventWatch(EventFilter filter, IntPtr userData) => _addEventWatch(filter, userData);

        private delegate void DelEventWatchDelegate(EventFilter filter, IntPtr userData);
        private static DelEventWatchDelegate _delEventWatch;
        public static void DelEventWatch(EventFilter filter, IntPtr userData) => _delEventWatch(filter, userData);

        private delegate void FilterEventsDelegate(EventFilter filter, IntPtr userData);
        private static FilterEventsDelegate _filterEvents;
        public static void FilterEvents(EventFilter filter, IntPtr userData) => _filterEvents(filter, userData);

        private delegate State EventStateDelegate(EventType type, State state);
        private static EventStateDelegate _eventState;
        public static State EventState(EventType type, State state) => _eventState(type, state);

        public static State GetEventState(EventType type) => _eventState(type, State.Query);

        private delegate uint RegisterEventsDelegate(int numEvents);
        private static RegisterEventsDelegate _registerEvents;
        public static uint RegisterEvents(int numEvents) => _registerEvents(numEvents);

        //
        // SDL_filesystem.h
        //
        private delegate string GetBasePathDelegate();
        private static GetBasePathDelegate _getBasePath;
        public static string GetBasePath() => _getBasePath();

        private delegate string GetPrefPathDelegate(string org, string app);
        private static GetPrefPathDelegate _getPrefPath;
        public static string GetPrefPath(string org, string app) => _getPrefPath(org, app);

        //
        // SDL_gamecontroller.h
        //
        private delegate int GameControllerAddMappingsFromRWDelegate(RWops rwOps, int freeRW);
        private static GameControllerAddMappingsFromRWDelegate _gameControllerAddMappingsFromRW;
        public static int GameControllerAddMappingsFromRW(RWops rwOps, int freeRW) => _gameControllerAddMappingsFromRW(rwOps, freeRW);

        public static int GameControllerAddMappingsFromFile(string file) => _gameControllerAddMappingsFromRW(RWFromFile(file, "rb"), 1);

        private delegate int GameControllerAddMappingDelegate(string mappginString);
        private static GameControllerAddMappingDelegate _gameControllerAddMapping;
        public static int GameControllerAddMapping(string mappingString) => _gameControllerAddMapping(mappingString);

        private delegate int GameControllerNumMappingsDelegate();
        private static GameControllerNumMappingsDelegate _gameControllerNumMappings;
        public static int GameControllerNumMappings() => _gameControllerNumMappings();

        private delegate string GameControllerMappingForIndexDelegate(int mappingIndex);
        private static GameControllerMappingForIndexDelegate _gameControllerMappingForIndex;
        public static string GameControllerMappingForIndex(int mappingIndex) => _gameControllerMappingForIndex(mappingIndex);

        private delegate string GameControllerMappingForGuidDelegate(Guid guid);
        private static GameControllerMappingForGuidDelegate _gameControllerMappingForGuid;
        public static string GameControllerMappingForGuid(Guid guid) => _gameControllerMappingForGuid(guid);

        private delegate string GameControllerMappingDelegate(GameController gameController);
        private static GameControllerMappingDelegate _gameControllerMapping;
        public static string GameControllerMapping(GameController gameController) => _gameControllerMapping(gameController);

        private delegate bool IsGameControllerDelegate(int joystickIndex);
        private static IsGameControllerDelegate _isGameController;
        public static bool IsGameController(int joystickIndex) => _isGameController(joystickIndex);

        private delegate string GameControllerNameForIndexDelegate(int joystickIndex);
        private static GameControllerNameForIndexDelegate _gameControllerNameForIndex;
        public static string GameControllerNameForIndex(int joystickIndex) => _gameControllerNameForIndex(joystickIndex);

        private delegate GameController GameControllerOpenDelegate(int joystickIndex);
        private static GameControllerOpenDelegate _gameControllerOpen;
        public static GameController GameControllerOpen(int joystickIndex) => _gameControllerOpen(joystickIndex);

        private delegate GameController GameControllerFromInstanceIDDelegate(JoystickID joystickID);
        private static GameControllerFromInstanceIDDelegate _gameControllerFromInstanceID;
        public static GameController GameControllerFromInstanceID(JoystickID joystickID) => _gameControllerFromInstanceID(joystickID);

        private delegate string GameControllerNameDelegate(GameController gameController);
        private static GameControllerNameDelegate _gameControllerName;
        public static string GameControllerName(GameController gameController) => _gameControllerName(gameController);

        private delegate ushort GameControllerGetVendorDelegate(GameController gameController);
        private static GameControllerGetVendorDelegate _gameControllerGetVendor;
        public static ushort GameControllerGetVendor(GameController gameController) => _gameControllerGetVendor(gameController);

        private delegate ushort GameControllerGetProductDelegate(GameController gameController);
        private static GameControllerGetProductDelegate _gameControllerGetProduct;
        public static ushort GameControllerGetProduct(GameController gameController) => _gameControllerGetProduct(gameController);

        private delegate ushort GameControllerGetProductVersionDelegate(GameController gameController);
        private static GameControllerGetProductVersionDelegate _gameControllerGetProductVersion;
        public static ushort GameControllerGetProductVersion(GameController gameController) => _gameControllerGetProductVersion(gameController);

        private delegate bool GameControllerGetAttachedDelegate(GameController gameController);
        private static GameControllerGetAttachedDelegate _gameControllerGetAttached;
        public static bool GameControllerGetAttached(GameController gameController) => _gameControllerGetAttached(gameController);

        private delegate Joystick GameControllerGetJoystickDelegate(GameController gameController);
        private static GameControllerGetJoystickDelegate _gameControllerGetJoystick;
        public static Joystick GameControllerGetJoystick(GameController gameController) => _gameControllerGetJoystick(gameController);

        private delegate State GameControllerEventStateDelegate(State state);
        private static GameControllerEventStateDelegate _gameControllerEventState;
        public static State GameControllerEventState(State state) => _gameControllerEventState(state);

        private delegate void GameControllerUpdateDelegate();
        private static GameControllerUpdateDelegate _gameControllerUpdate;
        public static void GameControllerUpdate() => _gameControllerUpdate();

        private delegate GameControllerAxis GameControllerGetAxisFromStringDelegate(string pchString);
        private static GameControllerGetAxisFromStringDelegate _gameControllerGetAxisFromString;
        public static GameControllerAxis GameControllerGetAxisFromString(string pchString) => _gameControllerGetAxisFromString(pchString);

        private delegate string GameControllerGetStringForAxisDelegate(GameControllerAxis axis);
        private static GameControllerGetStringForAxisDelegate _gameControllerGetStringForAxis;
        public static string GameControllerGetStringForAxis(GameControllerAxis axis) => _gameControllerGetStringForAxis(axis);

        private delegate GameControllerButtonBind GameControllerGetBindForAxisDelegate(GameController gameController, GameControllerAxis axis);
        private static GameControllerGetBindForAxisDelegate _gameControllerGetBindForAxis;
        public static GameControllerButtonBind GameControllerGetBindForAxis(GameController gameController, GameControllerAxis axis) => _gameControllerGetBindForAxis(gameController, axis);

        private delegate short GameControllerGetAxisDelegate(GameController gameController, GameControllerAxis axis);
        private static GameControllerGetAxisDelegate _gameControllerGetAxis;
        public static short GameControllerGetAxis(GameController gameController, GameControllerAxis axis) => _gameControllerGetAxis(gameController, axis);

        private delegate GameControllerButton GameControllerGetButtonFromStringDelegate(string pchString);
        private static GameControllerGetButtonFromStringDelegate _gameControllerGetButtonFromString;
        public static GameControllerButton GameControllerGetButtonFromString(string pchString) => _gameControllerGetButtonFromString(pchString);

        private delegate string GameControllerGetStringForButtonDelegate(GameControllerButton button);
        private static GameControllerGetStringForButtonDelegate _gameControllerGetStringForButton;
        public static string GameControllerGetStringForButton(GameControllerButton button) => _gameControllerGetStringForButton(button);

        private delegate GameControllerButtonBind GameControllerGetBindForButtonDelegate(GameController gameController, GameControllerButton button);
        private static GameControllerGetBindForButtonDelegate _gameControllerGetBindForButton;
        public static GameControllerButtonBind GameControllerGetBindForButton(GameController gameController, GameControllerButton button) => _gameControllerGetBindForButton(gameController, button);

        private delegate byte GameControllerGetButtonDelegate(GameController gameController, GameControllerButton button);
        private static GameControllerGetButtonDelegate _gameControllerGetButton;
        public static byte GameControllerGetButton(GameController gameController, GameControllerButton button) => _gameControllerGetButton(gameController, button);

        private delegate void GameControllerCloseDelegate(GameController gameController);
        private static GameControllerCloseDelegate _gameControllerClose;
        public static void GameControllerClose(GameController gameController) => _gameControllerClose(gameController);

        // TODO: SDL_gesture.h
        // TODO: SDL_haptic.h

        //
        // SDL_hints.h
        //
        private delegate bool SetHintWithPriorityDelegate(string name, string value, HintPriority priority);
        private static SetHintWithPriorityDelegate _setHintWithPriority;
        public static bool SetHintWithPriority(string name, string value, HintPriority priority) => _setHintWithPriority(name, value, priority);

        private delegate bool SetHintDelegate(string name, string value);
        private static SetHintDelegate _setHint;
        public static bool SetHint(string name, string value) => _setHint(name, value);

        private delegate string GetHintDelegate(string name);
        private static GetHintDelegate _getHint;
        private static string GetHint(string name) => _getHint(name);

        private delegate bool GetHintBooleanDelegate(string name, bool defaultValue);
        private static GetHintBooleanDelegate _getHintBoolean;
        public static bool GetHintBoolean(string name, bool defaultValue) => _getHintBoolean(name, defaultValue);

        private delegate void AddHintCallbackDelegate(string name, HintCallback callback, IntPtr userData);
        private static AddHintCallbackDelegate _addHintCallback;
        public static void AddHintCallback(string name, HintCallback callback, IntPtr userData) => _addHintCallback(name, callback, userData);

        private delegate void DelHintCallbackDelegate(string name, HintCallback callback, IntPtr userData);
        private static DelHintCallbackDelegate _delHintCallback;
        public static void DelHintCallback(string name, HintCallback callback, IntPtr userData) => _delHintCallback(name, callback, userData);

        private delegate void ClearHintsDelegate();
        private static ClearHintsDelegate _clearHints;
        public static void ClearHints() => _clearHints();

        //
        // SDL_joystick.h
        //
        private delegate int NumJoysticksDelegate();
        private static NumJoysticksDelegate _numJoysticks;
        public static int NumJoysticks() => _numJoysticks();

        private delegate string JoystickNameForIndexDelegate(int deviceIndex);
        private static JoystickNameForIndexDelegate _joystickNameForIndex;
        public static string JoystickNameForIndex(int deviceIndex) => _joystickNameForIndex(deviceIndex);

        private delegate Guid JoystickGetDeviceGuidDelegate(int deviceIndex);
        private static JoystickGetDeviceGuidDelegate _joystickGetDeviceGuid;
        public static Guid JoystickGetDeviceGUID(int deviceIndex) => _joystickGetDeviceGuid(deviceIndex);

        private delegate ushort JoystickGetDeviceVendorDelegate(int deviceIndex);
        private static JoystickGetDeviceVendorDelegate _joystickGetDeviceVendor;
        public static ushort JoystickGetDeviceVendor(int deviceIndex) => _joystickGetDeviceVendor(deviceIndex);

        private delegate ushort JoystickGetDeviceProductDelegate(int deviceIndex);
        private static JoystickGetDeviceProductDelegate _joystickGetDeviceProduct;
        public static ushort JoystickGetDeviceProduct(int deviceIndex) => _joystickGetDeviceProduct(deviceIndex);

        private delegate ushort JoystickGetDeviceProductVersionDelegate(int deviceIndex);
        private static JoystickGetDeviceProductVersionDelegate _joystickGetDeviceProductVersion;
        public static ushort JoystickGetDeviceProductVersion(int deviceIndex) => _joystickGetDeviceProductVersion(deviceIndex);

        private delegate JoystickType JoystickGetDeviceTypeDelegate(int deviceIndex);
        private static JoystickGetDeviceTypeDelegate _joystickGetDeviceType;
        public static JoystickType JoystickGetDeviceType(int deviceIndex) => _joystickGetDeviceType(deviceIndex);

        private delegate JoystickID JoystickGetDeviceInstanceIDDelegate(int deviceIndex);
        private static JoystickGetDeviceInstanceIDDelegate _joystickGetDeviceInstanceID;
        public static JoystickID JoystickGetDeviceInstanceID(int deviceIndex) => _joystickGetDeviceInstanceID(deviceIndex);

        private delegate Joystick JoystickOpenDelegate(int deviceIndex);
        private static JoystickOpenDelegate _joystickOpen;
        public static Joystick JoystickOpen(int deviceIndex) => _joystickOpen(deviceIndex);

        private delegate Joystick JoystickFromInstanceIDDelegate(JoystickID joystickID);
        private static JoystickFromInstanceIDDelegate _joystickFromInstanceID;
        public static Joystick JoystickFromInstanceID(JoystickID joystickID) => _joystickFromInstanceID(joystickID);

        private delegate string JoystickNameDelegate(Joystick joystick);
        private static JoystickNameDelegate _joystickName;
        public static string JoystickName(Joystick joystick) => _joystickName(joystick);

        private delegate Guid JoystickGetGuidDelegate(Joystick joystick);
        private static JoystickGetGuidDelegate _joystickGetGuid;
        public static Guid JoystickGetGUID(Joystick joystick) => _joystickGetGuid(joystick);


        private delegate ushort JoystickGetVendorDelegate(Joystick joystick);
        private static JoystickGetVendorDelegate _joystickGetVendor;
        public static ushort JoystickGetVendor(Joystick joystick) => _joystickGetVendor(joystick);

        private delegate ushort JoystickGetProductDelegate(Joystick joystick);
        private static JoystickGetProductDelegate _joystickGetProduct;
        public static ushort JoystickGetProduct(Joystick joystick) => _joystickGetProduct(joystick);

        private delegate ushort JoystickGetProductVersionDelegate(Joystick joystick);
        private static JoystickGetProductVersionDelegate _joystickGetProductVersion;
        public static ushort JoystickGetProductVersion(Joystick joystick) => _joystickGetProductVersion(joystick);

        private delegate JoystickType JoystickGetTypeDelegate(Joystick joystick);
        private static JoystickGetTypeDelegate _joystickGetType;
        public static JoystickType JoystickGetType(Joystick joystick) => _joystickGetType(joystick);

        private delegate void JoystickGetGuidStringDelegate(Guid guid, string pszGuid, int cbGuid);
        private static JoystickGetGuidStringDelegate _joystickGetGuidString;
        public static void JoystickGetGuidString(Guid guid, string pszGUID, int cbGUID) => _joystickGetGuidString(guid, pszGUID, cbGUID);

        private delegate Guid JoystickGetGuidFromStringDelegate(string pchGuid);
        private static JoystickGetGuidFromStringDelegate _joystickGetGuidFromString;
        public static Guid JoystickGetGUIDFromString(string pchGUID) => _joystickGetGuidFromString(pchGUID);

        private delegate bool JoystickGetAttachedDelegate(Joystick joystick);
        private static JoystickGetAttachedDelegate _joystickGetAttached;
        public static bool JoystickGetAttached(Joystick joystick) => _joystickGetAttached(joystick);

        private delegate JoystickID JoystickInstanceIDDelegate(Joystick joystick);
        private static JoystickInstanceIDDelegate _joystickInstanceID;
        public static JoystickID JoystickInstanceID(Joystick joystick) => _joystickInstanceID(joystick);

        private delegate int JoystickNumAxesDelegate(Joystick joystick);
        private static JoystickNumAxesDelegate _joystickNumAxes;
        public static int JoystickNumAxes(Joystick joystick) => _joystickNumAxes(joystick);

        private delegate int JoystickNumBallsDelegate(Joystick joystick);
        private static JoystickNumBallsDelegate _joystickNumBalls;
        public static int JoystickNumBalls(Joystick joystick) => _joystickNumBalls(joystick);

        private delegate int JoystickNumHatsDelegate(Joystick joystick);
        private static JoystickNumHatsDelegate _joystickNumHats;
        public static int JoystickNumHats(Joystick joystick) => _joystickNumHats(joystick);

        private delegate int JoystickNumButtonsDelegate(Joystick joystick);
        private static JoystickNumButtonsDelegate _joystickNumButtons;
        public static int JoystickNumButtons(Joystick joystick) => _joystickNumButtons(joystick);

        private delegate void JoystickUpdateDelegate();
        private static JoystickUpdateDelegate _joystickUpdate;
        public static void JoystickUpdate() => _joystickUpdate();

        private delegate State JoystickEventStateDelegate(State state);
        private static JoystickEventStateDelegate _joystickEventState;
        public static State JoystickEventState(State state) => _joystickEventState(state);

        private delegate short JoystickGetAxisDelegate(Joystick joystick, JoystickAxis axis);
        private static JoystickGetAxisDelegate _joystickGetAxis;
        public static short JoystickGetAxis(Joystick joystick, JoystickAxis axis) => _joystickGetAxis(joystick, axis);

        private delegate bool JoystickGetAxisInitialStateDelegate(Joystick joystick, JoystickAxis axis, out short state);
        private static JoystickGetAxisInitialStateDelegate _joystickGetAxisInitialState;
        public static bool JoystickGetAxisInitialState(Joystick joystick, JoystickAxis axis, out short state) => _joystickGetAxisInitialState(joystick, axis, out state);

        private delegate JoystickHat JoystickGetHatDelegate(Joystick joystick, int hat);
        private static JoystickGetHatDelegate _joystickGetHat;
        public static JoystickHat JoystickGetHat(Joystick joystick, int hat) => _joystickGetHat(joystick, hat);

        private delegate int JoystickGetBallDelegate(Joystick joystick, int ball, out int dx, out int dy);
        private static JoystickGetBallDelegate _joystickGetBall;
        public static int JoystickGetBall(Joystick joystick, int ball, out int dx, out int dy) => _joystickGetBall(joystick, ball, out dx, out dy);

        private delegate byte JoystickGetButtonDelegate(Joystick joystick, int button);
        private static JoystickGetButtonDelegate _joystickGetButton;
        public static byte JoystickGetButton(Joystick joystick, int button) => _joystickGetButton(joystick, button);

        private delegate void JoystickCloseDelegte(Joystick joystick);
        private static JoystickCloseDelegte _joystickClose;
        public static void JoystickClose(Joystick joystick) => _joystickClose(joystick);

        private delegate JoystickPowerLevel JoystickCurrentPowerLevelDelegate(Joystick joystick);
        private static JoystickCurrentPowerLevelDelegate _joystickCurrentPowerLevel;
        public static JoystickPowerLevel JoystickCurrentPowerLevel(Joystick joystick) => _joystickCurrentPowerLevel(joystick);

        //
        // SDL_keyboard.h
        //
        private delegate Window GetKeyboardFocusDelegate();
        private static GetKeyboardFocusDelegate _getKeyboardFocus;
        public static Window GetKeyboardFocus() => _getKeyboardFocus();

        private delegate string GetKeyboardStateDelegate(out int numKeys);
        private static GetKeyboardStateDelegate _getKeyboardState;
        public static string GetKeyboardState(out int numkeys) => _getKeyboardState(out numkeys);

        private delegate KeyMod GetModStateDelegate();
        private static GetModStateDelegate _getModState;
        public static KeyMod GetModState() => _getModState();

        private delegate void SetModStateDelegate(KeyMod modState);
        private static SetModStateDelegate _setModState;
        public static void SetModState(KeyMod modState) => _setModState(modState);

        private delegate Keycode GetKeyFromScancodeDelegate(Scancode scancode);
        private static GetKeyFromScancodeDelegate _getKeyFromScancode;
        public static Keycode GetKeyFromScancode(Scancode scancode) => _getKeyFromScancode(scancode);

        private delegate Scancode GetScancodeFromKeyDelegate(Keycode key);
        private static GetScancodeFromKeyDelegate _getScancodeFromKey;
        public static Scancode GetScancodeFromKey(Keycode key) => _getScancodeFromKey(key);

        private delegate string GetScancodeNameDelegate(Scancode scancode);
        private static GetScancodeNameDelegate _getScancodeName;
        public static string GetScancodeName(Scancode scancode) => _getScancodeName(scancode);

        private delegate Scancode GetScancodeFromNameDelegate(string name);
        private static GetScancodeFromNameDelegate _getScancodeFromName;
        public static Scancode GetScancodeFromName(string name) => _getScancodeFromName(name);

        private delegate string GetKeyNameDelegate(Keycode key);
        private static GetKeyNameDelegate _getKeyName;
        public static string GetKeyName(Keycode key) => _getKeyName(key);

        private delegate Keycode GetKeyFromNameDelegate(string name);
        private static GetKeyFromNameDelegate _getKeyFromName;
        public static Keycode GetKeyFromName(string name) => _getKeyFromName(name);

        private delegate void StartTextInputDelegate();
        private static StartTextInputDelegate _startTextInput;
        public static void StartTextInput() => _startTextInput();

        private delegate bool IsTextInputActiveDelegate();
        private static IsTextInputActiveDelegate _isTextInputActive;
        public static bool IsTextInputActive() => _isTextInputActive();

        private delegate void StopTextInputDelegate();
        private static StopTextInputDelegate _stopTextInput;
        public static void StopTextInput() => _stopTextInput();

        private delegate void SetTextInputRectDelegate(ref Rect rectangle);
        private static SetTextInputRectDelegate _setTextInputRect;
        public static void SetTextInputRect(ref Rect rectangle) => _setTextInputRect(ref rectangle);

        private delegate bool HasScreenKeyboardSupportDelegate();
        private static HasScreenKeyboardSupportDelegate _hasScreenKeyboardSupport;
        public static bool HasScreenKeyboardSupport() => _hasScreenKeyboardSupport();

        private delegate bool IsScreenKeyboardShownDelegate(Window window);
        private static IsScreenKeyboardShownDelegate _isScreenKeyboardShown;
        public static bool IsScreenKeyboardShown(Window window) => _isScreenKeyboardShown(window);

        //
        // SDL_loadso.h
        //
        private delegate IntPtr LoadObjectDelegate(string file);
        private static LoadObjectDelegate _loadObject;
        public static IntPtr LoadObject(string file) => _loadObject(file);

        private delegate IntPtr LoadFunctionDelegate(IntPtr handle, string name);
        private static LoadFunctionDelegate _loadFunction;
        public static IntPtr LoadFunction(IntPtr handle, string name) => _loadFunction(handle, name);

        private delegate void UnloadObjectDelegate(IntPtr handle);
        private static UnloadObjectDelegate _unloadObject;
        public static void UnloadObject(IntPtr handle) => _unloadObject(handle);

        //
        // SDL_log.h
        //
        private delegate void LogSetAllPriorityDelegate(LogPriority priority);
        private static LogSetAllPriorityDelegate _logSetAllPriority;
        public static void LogSetAllPriority(LogPriority priority) => _logSetAllPriority(priority);

        private delegate void LogSetPriorityDelegate(LogCategory category, LogPriority priority);
        private static LogSetPriorityDelegate _logSetPriority;
        public static void LogSetPriority(LogCategory category, LogPriority priority) => _logSetPriority(category, priority);

        private delegate LogPriority LogGetPriorityDelegate(LogCategory category);
        private static LogGetPriorityDelegate _logGetPriority;
        public static LogPriority LogGetPriority(LogCategory category) => _logGetPriority(category);

        private delegate void LogResetPrioritiesDelegate();
        private static LogResetPrioritiesDelegate _logResetPriorities;
        public static void LogResetPriorities() => _logResetPriorities();

        private delegate void LogDelegate(string fmt, params object[] objects);
        private static LogDelegate _log;
        public static void Log(string fmt, params object[] objects) => _log(fmt, objects);

        private delegate void LogVerboseDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogVerboseDelegate _logVerbose;
        public static void LogVerbose(LogCategory category, string fmt, params object[] objects) => _logVerbose(category, fmt, objects);

        private delegate void LogDebugDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogDebugDelegate _logDebug;
        public static void LogDebug(LogCategory category, string fmt, params object[] objects) => _logDebug(category, fmt, objects);

        private delegate void LogInfoDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogInfoDelegate _logInfo;
        public static void LogInfo(LogCategory category, string fmt, params object[] objects) => _logInfo(category, fmt, objects);

        private delegate void LogWarnDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogWarnDelegate _logWarn;
        public static void LogWarn(LogCategory category, string fmt, params object[] objects) => _logWarn(category, fmt, objects);

        private delegate void LogErrorDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogErrorDelegate _logError;
        public static void LogError(LogCategory category, string fmt, params object[] objects) => _logError(category, fmt, objects);

        private delegate void LogCriticalDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogCriticalDelegate _logCritical;
        public static void LogCritical(LogCategory category, string fmt, params object[] objects) => _logCritical(category, fmt, objects);

        private delegate void LogMessageDelegate(LogCategory category, string fmt, params object[] objects);
        private static LogMessageDelegate _logMessage;
        public static void LogMessage(LogCategory category, string fmt, params object[] objects) => _logMessage(category, fmt, objects);

        private delegate void LogGetOutputFunctionDelegate(LogOutputFunction callback, IntPtr userData);
        private static LogGetOutputFunctionDelegate _logGetOutputFunction;
        public static void LogGetOutputFunction(LogOutputFunction callback, IntPtr userData) => _logGetOutputFunction(callback, userData);

        private delegate void LogSetOutputFunctionDelegate(LogOutputFunction callback, IntPtr userData);
        private static LogSetOutputFunctionDelegate _logSetOutputFunction;
        public static void LogSetOutputFunction(LogOutputFunction callback, IntPtr userData) => _logSetOutputFunction(callback, userData);

        //
        // SDL_messagebox.h
        //
        private delegate int ShowMessageBoxDelegate(ref MessageBoxData messageBoxData, out int buttonID);
        private static ShowMessageBoxDelegate _showMessageBox;
        public static int ShowMessageBox(ref MessageBoxData messageBoxData, out int buttonID) => _showMessageBox(ref messageBoxData, out buttonID);

        private delegate int ShowSimpleMessageBoxDelegate(MessageBoxFlags flags, string title, string message, Window window);
        private static ShowSimpleMessageBoxDelegate _showSimpleMessageBox;
        public static int ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message, Window window) => _showSimpleMessageBox(flags, title, message, window);

        //
        // SDL_mouse.h
        //
        private delegate Window GetMouseFocusDelegate();
        private static GetMouseFocusDelegate _getMouseFocus;
        public static Window GetMouseFocus() => _getMouseFocus();

        private delegate MouseButtonState GetMouseStateDelegate(out int? x, out int? y);
        private static GetMouseStateDelegate _getMouseState;
        public static MouseButtonState GetMouseState(out int? x, out int? y) => _getMouseState(out x, out y);

        private delegate MouseButtonState GetGlobalMouseStateDelegate(out int? x, out int? y);
        private static GetGlobalMouseStateDelegate _getGlobalMouseState;
        public static MouseButtonState GetGlobalMouseState(out int? x, out int? y) => _getGlobalMouseState(out x, out y);

        private delegate MouseButtonState GetRelativeMouseStateDelegate(out int? x, out int? y);
        private static GetRelativeMouseStateDelegate _getRelativeMouseState;
        public static MouseButtonState GetRelativeMouseState(out int? x, out int? y) => _getRelativeMouseState(out x, out y);

        private delegate void WarpMouseInWindowDelegate(Window window, int x, int y);
        private static WarpMouseInWindowDelegate _warpMouseInWindow;
        public static void WarpMouseInWindow(Window window, int x, int y) => _warpMouseInWindow(window, x, y);

        private delegate int WarpMouseGlobalDelegate(int x, int y);
        private static WarpMouseGlobalDelegate _warpMouseGlobal;
        public static int WarpMouseGlobal(int x, int y) => _warpMouseGlobal(x, y);

        private delegate int SetRelativeMouseModeDelegate(bool enabled);
        private static SetRelativeMouseModeDelegate _setRelativeMouseMode;
        public static int SetRelativeMouseMode(bool enabled) => _setRelativeMouseMode(enabled);

        private delegate int CaptureMouseDelegate(bool enabled);
        private static CaptureMouseDelegate _captureMouse;
        public static int CaptureMouse(bool enabled) => _captureMouse(enabled);

        private delegate bool GetRelativeMouseModeDelegate();
        private static GetRelativeMouseModeDelegate _getRelativeMouseMode;
        public static bool GetRelativeMouseMode() => _getRelativeMouseMode();

        private delegate Cursor CreateCursorDelegate(byte[] data, byte[] mask, int w, int h, int hotX, int hotY);
        private static CreateCursorDelegate _createCursor;
        public static Cursor CreateCursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY) => _createCursor(data, mask, w, h, hotX, hotY);

        private delegate Cursor CreateColorCursorDelegate(Surface surface, int hotX, int hotY);
        private static CreateColorCursorDelegate _createColorCursor;
        public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY) => _createColorCursor(surface, hotX, hotY);

        private delegate Cursor CreateSystemCursorDelegate(SystemCursor id);
        private static CreateSystemCursorDelegate _createSystemCursor;
        public static Cursor CreateSystemCursor(SystemCursor id) => _createSystemCursor(id);

        private delegate void SetCursorDelegate(Cursor cursor);
        private static SetCursorDelegate _setCursor;
        public static void SetCursor(Cursor cursor) => _setCursor(cursor);

        private delegate Cursor GetCursorDelegate();
        private static GetCursorDelegate _getCursor;
        public static Cursor GetCursor() => _getCursor();

        private delegate Cursor GetDefaultCursorDelegate();
        private static GetDefaultCursorDelegate _getDefaultCursor;
        public static Cursor GetDefaultCursor() => _getDefaultCursor();

        private delegate void FreeCursorDelegate(Cursor cursor);
        private static FreeCursorDelegate _freeCursor;
        public static void FreeCursor(Cursor cursor) => _freeCursor(cursor);

        private delegate State ShowCursorDelegate(State toggle);
        private static ShowCursorDelegate _showCursor;
        public static State ShowCursor(State toggle) => _showCursor(toggle);

        //
        // SDL_pixels.h
        //
        private delegate string GetPixelFormatNameDelegate(uint format);
        private static GetPixelFormatNameDelegate _getPixelFormatName;
        public static string GetPixelFormatName(uint format) => _getPixelFormatName(format);

        private delegate bool PixelFormatEnumToMasksDelegate(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask);
        private static PixelFormatEnumToMasksDelegate _pixelFormatEnumToMasks;
        public static bool PixelFormatEnumToMasks(uint format, out int bpp, out uint rMask, out uint gMask, out uint bMask, out uint aMask) => _pixelFormatEnumToMasks(format, out bpp, out rMask, out gMask, out bMask, out aMask);

        private delegate uint MasksToPixelFormatEnumDelegate(int bpp, uint rMask, uint gMask, uint bMask, uint aMask);
        private static MasksToPixelFormatEnumDelegate _masksToPixelFormatEnum;
        public static uint MasksToPixelFormatEnum(int bpp, uint rMask, uint gMask, uint bMask, uint aMask) => _masksToPixelFormatEnum(bpp, rMask, gMask, bMask, aMask);

        private delegate PixelFormat* AllocFormatDelegate(uint pixelFormat);
        private static AllocFormatDelegate _allocFormat;
        public static PixelFormat* AllocFormat(uint pixelFormat) => _allocFormat(pixelFormat);

        private delegate void FreeFormatDelegate(ref PixelFormat pixelFormat);
        private static FreeFormatDelegate _freeFormat;
        public static void FreeFormat(ref PixelFormat pixelFormat) => _freeFormat(ref pixelFormat);

        private delegate IntPtr AllocPaletteDelegate(int numColors);
        private static AllocPaletteDelegate _allocPalette;
        public static IntPtr AllocPalette(int numColors) => _allocPalette(numColors);

        private delegate int SetPixelFormatPaletteDelegate(ref PixelFormat format, ref Palette palette);
        private static SetPixelFormatPaletteDelegate _setPixelFormatPalette;
        public static int SetPixelFormatPalette(ref PixelFormat format, ref Palette palette) => _setPixelFormatPalette(ref format, ref palette);

        private delegate int SetPaletteColorsDelegate(Palette palette, Color[] colors, int firstColor, int numColors);
        private static SetPaletteColorsDelegate _setPaletteColors;
        public static int SetPaletteColors(Palette palette, Color[] colors, int firstColor, int numColors) => _setPaletteColors(palette, colors, firstColor, numColors);

        private delegate void FreePaletteDelegate(Palette palette);
        private static FreePaletteDelegate _freePalette;
        public static void FreePalette(Palette palette) => _freePalette(palette);

        private delegate uint MapRgbDelegate(ref PixelFormat format, byte r, byte g, byte b);
        private static MapRgbDelegate _mapRgb;
        public static uint MapRgb(ref PixelFormat format, byte r, byte g, byte b) => _mapRgb(ref format, r, g, b);

        private delegate uint MapRgbaDelegate(ref PixelFormat format, byte r, byte g, byte b, byte a);
        private static MapRgbaDelegate _mapRgba;
        public static uint MapRgba(ref PixelFormat format, byte r, byte g, byte b, byte a) => _mapRgba(ref format, r, g, b, a);

        private delegate void GetRgbDelegate(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b);
        private static GetRgbDelegate _getRgb;
        public static void GetRgb(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b) => _getRgb(pixel, ref format, out r, out g, out b);

        private delegate void GetRgbaDelegate(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a);
        private static GetRgbaDelegate _getRgba;
        public static void GetRgba(uint pixel, ref PixelFormat format, out byte r, out byte g, out byte b, out byte a) => _getRgba(pixel, ref format, out r, out g, out b, out a);

        private delegate void CalculateGammaRampDelegate(float gamma, out ushort[] ramp);
        private static CalculateGammaRampDelegate _calculateGammaRamp;
        public static void CalculateGammaRamp(float gamma, out ushort[] ramp) => _calculateGammaRamp(gamma, out ramp);

        //
        // SDL_platform.h
        //
        private delegate string GetPlatformDelegate();
        private static GetPlatformDelegate _getPlatform;
        public static string GetPlatform() => _getPlatform();

        //
        // SDL_power.h
        //
        private delegate PowerState GetPowerInfoDelegate(out int seconds, out int percentage);
        private static GetPowerInfoDelegate _getPowerInfo;
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
        private static HasIntersectionDelegate _hasIntersection;
        public static bool HasIntersection(ref Rect a, ref Rect b) => _hasIntersection(ref a, ref b);

        private delegate bool IntersectRectDelegate(ref Rect a, ref Rect b, out Rect result);
        private static IntersectRectDelegate _intersectRect;
        public static bool IntersectRect(ref Rect a, ref Rect b, out Rect result) => _intersectRect(ref a, ref b, out result);

        private delegate void UnionRectDelegate(ref Rect a, ref Rect b, out Rect result);
        private static UnionRectDelegate _unionRect;
        public static void UnionRect(ref Rect a, ref Rect b, out Rect result) => _unionRect(ref a, ref b, out result);

        private delegate bool EnclosePointsDelegate(Point[] points, int count, ref Rect clip, out Rect result);
        private static EnclosePointsDelegate _enclosePoints;
        public static bool EnclosePoints(Point[] points, int count, ref Rect clip, out Rect result) => _enclosePoints(points, count, ref clip, out result);

        private delegate bool IntersectRectAndLineDelegate(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);
        private static IntersectRectAndLineDelegate _intersectRectAndLine;
        public static bool IntersectRectAndLine(ref Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2) => _intersectRectAndLine(ref rectangle, ref x1, ref y1, ref x2, ref y2);

        //
        // SDL_render.h
        //
        private delegate int GetNumRenderDriversDelegate();
        private static GetNumRenderDriversDelegate _getNumRenderDrivers;
        public static int GetNumRenderDrivers() => _getNumRenderDrivers();

        private delegate int GetRenderDriverInfoDelegate(int index, out RendererInfo info);
        private static GetRenderDriverInfoDelegate _getRenderDriverInfo;
        public static int GetRenderDriverInfo(int index, out RendererInfo info) => _getRenderDriverInfo(index, out info);

        private delegate int CreateWindowAndRendererDelegate(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer);
        private static CreateWindowAndRendererDelegate _createWindowAndRenderer;
        public static int CreateWindowAndRenderer(int width, int height, WindowFlags windowFlags, out Window window, out Renderer renderer) => _createWindowAndRenderer(width, height, windowFlags, out window, out renderer);

        private delegate Renderer CreateRendererDelegate(Window window, int index, RendererFlags flags);
        private static CreateRendererDelegate _createRenderer;
        public static Renderer CreateRenderer(Window window, int index, RendererFlags flags) => _createRenderer(window, index, flags);

        private delegate Renderer CreateSoftwareRendererDelegate(Surface surface);
        private static CreateSoftwareRendererDelegate _createSoftwareRenderer;
        public static Renderer CreateSoftwareRenderer(Surface surface) => _createSoftwareRenderer(surface);

        private delegate Renderer GetRendererDelegate(Window window);
        private static GetRendererDelegate _getRenderer;
        public static Renderer GetRenderer(Window window) => _getRenderer(window);

        private delegate int GetRendererInfoDelegate(Renderer renderer, out RendererInfo info);
        private static GetRendererInfoDelegate _getRendererInfo;
        public static int GetRendererInfo(Renderer renderer, out RendererInfo info) => _getRendererInfo(renderer, out info);

        private delegate int GetRendererOutputSizeDelegate(Renderer renderer, out int w, out int h);
        private static GetRendererOutputSizeDelegate _getRendererOutputSize;
        public static int GetRendererOutputSize(Renderer renderer, out int w, out int h) => _getRendererOutputSize(renderer, out w, out h);

        private delegate Texture CreateTextureDelegate(Renderer renderer, uint format, int access, int w, int h);
        private static CreateTextureDelegate _createTexture;
        public static Texture CreateTexture(Renderer renderer, uint format, int access, int w, int h) => _createTexture(renderer, format, access, w, h);

        private delegate Texture CreateTextureFromSurfaceDelegate(Renderer renderer, Surface surface);
        private static CreateTextureFromSurfaceDelegate _createTextureFromSurface;
        public static Texture CreateTextureFromSurface(Renderer renderer, Surface surface) => _createTextureFromSurface(renderer, surface);

        private delegate int QueryTextureDelegate(Texture texture, out uint format, out int access, out int w, out int h);
        private static QueryTextureDelegate _queryTexture;
        public static int QueryTexture(Texture texture, out uint format, out int access, out int w, out int h) => _queryTexture(texture, out format, out access, out w, out h);

        private delegate int SetTextureColorModDelegate(Texture texture, byte r, byte g, byte b);
        private static SetTextureColorModDelegate _setTextureColorMod;
        public static int SetTextureColorMod(Texture texture, byte r, byte g, byte b) => _setTextureColorMod(texture, r, g, b);

        private delegate int GetTextureColorModDelegate(Texture texture, out byte r, out byte g, out byte b);
        private static GetTextureColorModDelegate _getTextureColorMod;
        public static int GetTextureColorMod(Texture texture, out byte r, out byte g, out byte b) => _getTextureColorMod(texture, out r, out g, out b);

        private delegate int SetTextureAlphaModDelegate(Texture texture, byte alpha);
        private static SetTextureAlphaModDelegate _setTextureAlphaMod;
        public static int SetTextureAlphaMod(Texture texture, byte alpha) => _setTextureAlphaMod(texture, alpha);

        private delegate int GetTextureAlphaModDelegate(Texture texture, out byte alpha);
        private static GetTextureAlphaModDelegate _getTextureAlphaMod;
        public static int GetTextureAlphaMod(Texture texture, out byte alpha) => _getTextureAlphaMod(texture, out alpha);

        private delegate int SetTextureBlendModeDelegate(Texture texture, BlendMode blendMode);
        private static SetTextureBlendModeDelegate _setTextureBlendMode;
        public static int SetTextureBlendMode(Texture texture, BlendMode blendMode) => _setTextureBlendMode(texture, blendMode);

        private delegate int GetTextureBlendModeDelegate(Texture texture, out BlendMode blendMode);
        private static GetTextureBlendModeDelegate _getTextureBlendMode;
        public static int GetTextureBlendMode(Texture texture, out BlendMode blendMode) => _getTextureBlendMode(texture, out blendMode);

        private delegate int UpdateTextureDelegate(Texture texture, ref Rect? rect, IntPtr pixels, int pitch);
        private static UpdateTextureDelegate _updateTexture;
        public static int UpdateTexture(Texture texture, ref Rect? rect, IntPtr pixels, int pitch) => _updateTexture(texture, ref rect, pixels, pitch);

        private delegate int UpdateYUVTextureDelegate(Texture texture, ref Rect? rect, byte[] yPlane, int yPitch, byte[] uPlane, int uPitch, byte[] vPlane, int vPitch);
        private static UpdateYUVTextureDelegate _updateYUVTexture;
        public static int UpdateYUVTexture(Texture texture, ref Rect? rect, byte[] yPlane, int yPitch, byte[] uPlane, int uPitch, byte[] vPlane, int vPitch) => _updateYUVTexture(texture, ref rect, yPlane, yPitch, uPlane, uPitch, vPlane, vPitch);

        private delegate int LockTextureDelegate(Texture texture, ref Rect? rect, out IntPtr pixels, out int pitch);
        private static LockTextureDelegate _lockTexture;
        public static int LockTexture(Texture texture, ref Rect? rect, out IntPtr pixels, out int pitch) => _lockTexture(texture, ref rect, out pixels, out pitch);

        private delegate void UnlockTextureDelegate(Texture texture);
        private static UnlockTextureDelegate _unlockTexture;
        public static void UnlockTexture(Texture texture) => _unlockTexture(texture);

        private delegate bool RenderTargetSupportedDelegate(Renderer renderer);
        private static RenderTargetSupportedDelegate _renderTargetSupported;
        public static bool RenderTargetSupported(Renderer renderer) => _renderTargetSupported(renderer);

        private delegate int SetRenderTargetDelegate(Renderer renderer, Texture texture);
        private static SetRenderTargetDelegate _setRenderTarget;
        public static int SetRenderTarget(Renderer renderer, Texture texture) => _setRenderTarget(renderer, texture);

        private delegate Texture GetRenderTargetDelegate(Renderer renderer);
        private static GetRenderTargetDelegate _getRenderTarget;
        public static Texture GetRenderTarget(Renderer renderer) => _getRenderTarget(renderer);

        private delegate int RenderSetLogicalSizeDelegate(Renderer renderer, int w, int h);
        private static RenderSetLogicalSizeDelegate _renderSetLogicalSize;
        public static int RenderSetLogicalSize(Renderer renderer, int w, int h) => _renderSetLogicalSize(renderer, w, h);

        private delegate void RenderGetLogicalSizeDelegate(Renderer renderer, out int w, out int h);
        private static RenderGetLogicalSizeDelegate _renderGetLogicalSize;
        public static void RenderGetLogicalSize(Renderer renderer, out int w, out int h) => _renderGetLogicalSize(renderer, out w, out h);

        private delegate int RenderSetIntegerScaleDelegate(Renderer renderer, bool enable);
        private static RenderSetIntegerScaleDelegate _renderSetIntegerScale;
        public static int RenderSetIntegerScale(Renderer renderer, bool enable) => _renderSetIntegerScale(renderer, enable);

        private delegate bool RenderGetIntegerScaleDelegate(Renderer renderer);
        private static RenderGetIntegerScaleDelegate _renderGetIntegerScale;
        public static bool RenderGetIntegerScale(Renderer renderer) => _renderGetIntegerScale(renderer);

        private delegate int RenderSetViewportDelegate(Renderer renderer, ref Rect rect);
        private static RenderSetViewportDelegate _renderSetViewport;
        public static int RenderSetViewport(Renderer renderer, ref Rect rect) => _renderSetViewport(renderer, ref rect);

        private delegate void RenderGetViewportDelegate(Renderer renderer, out Rect rect);
        private static RenderGetViewportDelegate _renderGetViewport;
        public static void RenderGetViewport(Renderer renderer, out Rect rect) => _renderGetViewport(renderer, out rect);

        private delegate int RenderSetClipRectDelegate(Renderer renderer, ref Rect rect);
        private static RenderSetClipRectDelegate _renderSetClipRect;
        public static int RenderSetClipRect(Renderer renderer, ref Rect rect) => _renderSetClipRect(renderer, ref rect);

        private delegate void RenderGetClipRectDelegate(Renderer renderer, out Rect rect);
        private static RenderGetClipRectDelegate _renderGetClipRect;
        public static void RenderGetClipRect(Renderer renderer, out Rect rect) => _renderGetClipRect(renderer, out rect);

        private delegate bool RenderIsClipEnabledDelegate(Renderer renderer);
        private static RenderIsClipEnabledDelegate _renderIsClipEnabled;
        public static bool RenderIsClipEnabled(Renderer renderer) => _renderIsClipEnabled(renderer);

        private delegate int RenderSetScaleDelegate(Renderer renderer, float scaleX, float scaleY);
        private static RenderSetScaleDelegate _renderSetScale;
        public static int RenderSetScale(Renderer renderer, float scaleX, float scaleY) => _renderSetScale(renderer, scaleX, scaleY);

        private delegate void RenderGetScaleDelegate(Renderer renderer, out float scaleX, out float scaleY);
        private static RenderGetScaleDelegate _renderGetScale;
        public static void RenderGetScale(Renderer renderer, out float scaleX, out float scaleY) => _renderGetScale(renderer, out scaleX, out scaleY);

        private delegate int SetRenderDrawColorDelegate(Renderer renderer, byte r, byte g, byte b, byte a);
        private static SetRenderDrawColorDelegate _setRenderDrawColor;
        public static int SetRenderDrawColor(Renderer renderer, byte r, byte g, byte b, byte a) => _setRenderDrawColor(renderer, r, g, b, a);

        private delegate int GetRenderDrawColorDelegate(Renderer renderer, out byte r, out byte g, out byte b, out byte a);
        private static GetRenderDrawColorDelegate _getRenderDrawColor;
        public static int GetRenderDrawColor(Renderer renderer, out byte r, out byte g, out byte b, out byte a) => _getRenderDrawColor(renderer, out r, out g, out b, out a);

        private delegate int SetRenderDrawBlendModeDelegate(Renderer renderer, BlendMode blendMode);
        private static SetRenderDrawBlendModeDelegate _setRenderDrawBlendMode;
        public static int SetRenderDrawBlendMode(Renderer renderer, BlendMode blendMode) => _setRenderDrawBlendMode(renderer, blendMode);

        private delegate int GetRenderDrawBlendModeDelegate(Renderer renderer, out BlendMode blendMode);
        private static GetRenderDrawBlendModeDelegate _getRenderDrawBlendMode;
        public static int GetRenderDrawBlendMode(Renderer renderer, out BlendMode blendMode) => _getRenderDrawBlendMode(renderer, out blendMode);

        private delegate int RenderClearDelegate(Renderer renderer);
        private static RenderClearDelegate _renderClear;
        public static int RenderClear(Renderer renderer) => _renderClear(renderer);

        private delegate int RenderDrawPointDelegate(Renderer renderer, int x, int y);
        private static RenderDrawPointDelegate _renderDrawPoint;
        public static int RenderDrawPoint(Renderer renderer, int x, int y) => _renderDrawPoint(renderer, x, y);

        private delegate int RenderDrawPointsDelegate(Renderer renderer, Point[] points, int count);
        private static RenderDrawPointsDelegate _renderDrawPoints;
        public static int RenderDrawPoints(Renderer renderer, Point[] points, int count) => _renderDrawPoints(renderer, points, count);

        private delegate int RenderDrawLineDelegate(Renderer renderer, int x1, int y1, int x2, int y2);
        private static RenderDrawLineDelegate _renderDrawLine;
        public static int RenderDrawLine(Renderer renderer, int x1, int y1, int x2, int y2) => _renderDrawLine(renderer, x1, y1, x2, y2);

        private delegate int RenderDrawLinesDelegate(Renderer renderer, Point[] points, int count);
        private static RenderDrawLinesDelegate _renderDrawLines;
        public static int RenderDrawLines(Renderer renderer, Point[] points, int count) => _renderDrawLines(renderer, points, count);

        private delegate int RenderDrawRectDelegate(Renderer renderer, ref Rect rect);
        private static RenderDrawRectDelegate _renderDrawRect;
        public static int RenderDrawRect(Renderer renderer, ref Rect rect) => _renderDrawRect(renderer, ref rect);

        private delegate int RenderDrawRectsDelegate(Renderer renderer, Rect[] rects, int count);
        private static RenderDrawRectsDelegate _renderDrawRects;
        public static int RenderDrawRects(Renderer renderer, Rect[] rects, int count) => _renderDrawRects(renderer, rects, count);

        private delegate int RenderFillRectDelegate(Renderer renderer, ref Rect rect);
        private static RenderFillRectDelegate _renderFillRect;
        public static int RenderFillRect(Renderer renderer, ref Rect rect) => _renderFillRect(renderer, ref rect);

        private delegate int RenderFillRectsDelegate(Renderer renderer, Rect[] rects, int count);
        private static RenderFillRectsDelegate _renderFillRects;
        public static int RenderFillRects(Renderer renderer, Rect[] rects, int count) => _renderFillRects(renderer, rects, count);

        private delegate int RenderCopyDelegate(Renderer renderer, Texture texture, ref Rect? srcRect, ref Rect? dstRect);
        private static RenderCopyDelegate _renderCopy;
        public static int RenderCopy(Renderer renderer, Texture texture, ref Rect? srcRect, ref Rect? dstRect) => _renderCopy(renderer, texture, ref srcRect, ref dstRect);

        private delegate int RenderCopyExDelegate(Renderer renderer, Texture texture, ref Rect? srcrect, ref Rect? dstrect, double angle, ref Point center, RendererFlip flip);
        private static RenderCopyExDelegate _renderCopyEx;
        public static int RenderCopyEx(Renderer renderer, Texture texture, ref Rect? srcRect, ref Rect? dstRect, double angle, ref Point center, RendererFlip flip) => _renderCopyEx(renderer, texture, ref srcRect, ref dstRect, angle, ref center, flip);

        private delegate int RenderReadPixelsDelegate(Renderer renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);
        private static RenderReadPixelsDelegate _renderReadPixels;
        public static int RenderReadPixels(Renderer renderer, ref Rect rect, uint format, IntPtr pixels, int pitch) => _renderReadPixels(renderer, ref rect, format, pixels, pitch);

        private delegate void RenderPresentDelegate(Renderer renderer);
        private static RenderPresentDelegate _renderPresent;
        public static void RenderPresent(Renderer renderer) => _renderPresent(renderer);

        private delegate void DestroyTextureDelegate(Texture texture);
        private static DestroyTextureDelegate _destroyTexture;
        public static void DestroyTexture(Texture texture) => _destroyTexture(texture);

        private delegate void DestroyRendererDelegate(Renderer renderer);
        private static DestroyRendererDelegate _destroyRenderer;
        public static void DestroyRenderer(Renderer renderer) => _destroyRenderer(renderer);

        //
        // SDL_rwops.h
        //
        private delegate RWops RWFromFileDelegate(string file, string mode);
        private static RWFromFileDelegate _rwFromFile;
        public static RWops RWFromFile(string file, string mode) => _rwFromFile(file, mode);

        //
        // SDL_shape.h
        //
        private delegate Window CreateShapedWindowDelegate(string title, uint x, uint y, uint w, uint h, WindowFlags flags);
        private static CreateShapedWindowDelegate _createShapedWindow;
        public static Window CreateShapedWindow(string title, uint x, uint y, uint w, uint h, WindowFlags flags) => _createShapedWindow(title, x, y, w, h, flags);

        private delegate bool IsShapedWindowDelegate(Window window);
        private static IsShapedWindowDelegate _isShapedWindow;
        public static bool IsShapedWindow(Window window) => _isShapedWindow(window);

        private delegate int SetWindowShapeDelegate(Window window, ref Surface shape, ref WindowShape shapeMode);
        private static SetWindowShapeDelegate _setWindowShape;
        public static int SetWindowShape(Window window, ref Surface shape, ref WindowShape shapeMode) => _setWindowShape(window, ref shape, ref shapeMode);

        private delegate int GetShapedWindowModeDelegate(Window window, out WindowShape shapeMode);
        private static GetShapedWindowModeDelegate _getShapedWindowMode;
        public static int GetShapedWindowMode(Window window, out WindowShape shapeMode) => _getShapedWindowMode(window, out shapeMode);

        //
        // TODO: SDL_surface.h
        //

        //public static int UpperBlit(Surface src, ref Rect srcrect, Surface dst, ref Rect dstrect);
        

        //public static int UpperBlitScaled(Surface src, ref Rect srcrect, Surface dst, ref Rect dstrect);
        

        //public static int ConvertPixels(int width, int height, uint src_format, Surface src, int src_pitch, uint dst_format, Surface dst, int dst_pitch);


        //public static IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags);


        //public static IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);


        //public static IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);


        //public static IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);


        //public static IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);


        //public static IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);


        //public static int FillRect(IntPtr dst, ref Rect rect, uint color);
        

        //public static int FillRects(IntPtr dst, Rect[] rects, int count, uint color);


        //public static void FreeSurface(Surface surface);


        //public static void GetClipRect(Surface surface, out Rect rect);


        //public static int GetColorKey(Surface surface, out uint key);


        //public static int GetSurfaceAlphaMod(Surface surface, out byte alpha);


        //public static int GetSurfaceBlendMode(Surface surface, out BlendMode blendMode);


        //public static int GetSurfaceColorMod(Surface surface, out byte r, out byte g, out byte b);


        //private static IntPtr LoadBMP_RW(IntPtr src, int freesrc);


        //public static int LockSurface(Surface surface);


        //public static int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);


        //public static int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);


        //private static int SaveBMP_RW(Surface surface, IntPtr src, int freesrc);


        //public static bool SetClipRect(Surface surface, ref Rect rect);


        //public static int SetColorKey(Surface surface, int flag, uint key);


        //public static int SetSurfaceAlphaMod(Surface surface, byte alpha);


        //public static int SetSurfaceBlendMode(Surface surface, BlendMode blendMode);


        //public static int SetSurfaceColorMod(Surface surface, byte r, byte g, byte b);


        //public static int SetSurfacePalette(Surface surface, IntPtr palette);


        //public static int SetSurfaceRLE(Surface surface, int flag);


        //public static int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);


        //public static void UnlockSurface(Surface surface);

        //
        // SDL_syswm.h
        //
        private delegate bool GetWindowWMInfoDelegate(Window window, out SysWMInfo info);
        private static GetWindowWMInfoDelegate _getWindowWMInfo;
        public static bool GetWindowWMInfo(Window window, out SysWMInfo info) => _getWindowWMInfo(window, out info);

        //
        // SDL_timer.h
        //
        private delegate uint GetTicksDelegate();
        private static GetTicksDelegate _getTicks;
        public static uint GetTicks() => _getTicks();

        public static bool TicksPassed(uint a, uint b)
        {
            return ((int)(b - a) <= 0);
        }

        private delegate ulong GetPerformanceCounterDelegate();
        private static GetPerformanceCounterDelegate _getPerformanceCounter;
        public static ulong GetPerformanceCounter() => _getPerformanceCounter();

        private delegate ulong GetPerformanceFrequencyDelegate();
        private static GetPerformanceFrequencyDelegate _getPerformanceFrequency;
        public static ulong GetPerformanceFrequency() => _getPerformanceFrequency();

        private delegate void DelayDelegate(uint ms);
        private static DelayDelegate _delay;
        public static void Delay(uint ms) => _delay(ms);

        private delegate TimerID AddTimerDelegate(uint interval, TimerCallback callback, IntPtr param);
        private static AddTimerDelegate _addTimer;
        public static TimerID AddTimer(uint interval, TimerCallback callback, IntPtr param) => _addTimer(interval, callback, param);

        private delegate bool RemoveTimerDelegate(TimerID id);
        private static RemoveTimerDelegate _removeTimer;
        public static bool RemoveTimer(TimerID id) => _removeTimer(id);

        // TODO: SDL_touch.h

        //
        // SDL_version.h
        //
        public static int VersionNum(int x, int y, int z) => x * 1000 + y * 100 + z;
        public static int CompiledVersion() => VersionNum(Version.MajorVersion, Version.MinorVersion, Version.PatchLevel);
        public static bool VersionAtLeast(int x, int y, int z) => CompiledVersion() >= VersionNum(x, y, z);

        private delegate void GetVersionDelegate(out Version version);
        private static GetVersionDelegate _getVersion;
        public static void GetVersion(out Version version) => _getVersion(out version);

        private delegate string GetRevisionDelegate();
        private static GetRevisionDelegate _getRevision;
        public static string GetRevision() => _getRevision();

        private delegate int GetRevisionNumberDelegate();
        private static GetRevisionNumberDelegate _getRevisionNumber;
        public static int GetRevisionNumber() => _getRevisionNumber();

        //
        // SDL_video.h
        //
        public const int WindowPositionUndefinedMask = 0x1FFF0000;
        public const int WindowPositionCenteredMask = 0x2FFF0000;
        public const int WindowPositionUndefined = 0x1FFF0000;
        public const int WindowPositionCentered = 0x2FFF0000;

        public static int WindowPositionUndefinedDisplay(int x) => (WindowPositionUndefinedMask | x);

        public static bool WindowPositionIsUndefined(int x) => (x & 0xFFFF0000) == WindowPositionUndefinedMask;

        public static int WindowPositionCenteredDisplay(int x) => (WindowPositionCenteredMask | x);

        public static bool WindowPositionIsCentered(int x) => (x & 0xFFFF0000) == WindowPositionCenteredMask;

        private delegate int GetNumVideoDriversDelegate();
        private static GetNumVideoDriversDelegate _getNumVideoDrivers;
        public static int GetNumVideoDrivers() => _getNumVideoDrivers();

        private delegate string GetVideoDriverDelegate(int index);
        private static GetVideoDriverDelegate _getVideoDriver;
        private static string GetVideoDriver(int index) => _getVideoDriver(index);

        private delegate int VideoInitDelegate(string driverName);
        private static VideoInitDelegate _videoInit;
        public static int VideoInit(string driverName) => _videoInit(driverName);

        private delegate void VideoQuitDelegate();
        private static VideoQuitDelegate _videoQuit;
        public static void VideoQuit() => _videoQuit();

        private delegate string GetCurrentVideoDriverDelegate();
        private static GetCurrentVideoDriverDelegate _getCurrentVideoDriver;
        public static string GetCurrentVideoDriver() => _getCurrentVideoDriver();

        private delegate int GetNumVideoDisplaysDelegate();
        private static GetNumVideoDisplaysDelegate _getNumVideoDisplays;
        public static int GetNumVideoDisplays() => _getNumVideoDisplays();

        private delegate string GetDisplayNameDelegate(int displayIndex);
        private static GetDisplayNameDelegate _getDisplayName;
        public static string GetDisplayName(int displayIndex) => _getDisplayName(displayIndex);

        private delegate int GetDisplayBoundsDelegate(int displayIndex, out Rect rectangle);
        private static GetDisplayBoundsDelegate _getDisplayBounds;
        public static int GetDisplayBounds(int displayIndex, out Rect rectangle) => _getDisplayBounds(displayIndex, out rectangle);

        private delegate int GetDisplayDpiDelegate(int displayIndex, out float ddpi, out float hdpi, out float vdpi);
        private static GetDisplayDpiDelegate _getDisplayDpi;
        public static int GetDisplayDpi(int displayIndex, out float ddpi, out float hdpi, out float vdpi) => _getDisplayDpi(displayIndex, out ddpi, out hdpi, out vdpi);

        private delegate int GetDisplayUsableBoundsDelegate(int displayIndex, out Rect rectangle);
        private static GetDisplayUsableBoundsDelegate _getDisplayUsableBounds;
        public static int GetDisplayUsableBounds(int displayIndex, out Rect rectangle) => _getDisplayUsableBounds(displayIndex, out rectangle);

        private delegate int GetNumDisplayModesDelegate(int displayIndex);
        private static GetNumDisplayModesDelegate _getNumDisplayModes;
        public static int GetNumDisplayModes(int displayIndex) => _getNumDisplayModes(displayIndex);

        private delegate int GetDisplayModeDelegate(int displayIndex, int modeIndex, out DisplayMode mode);
        private static GetDisplayModeDelegate _getDisplayMode;
        public static int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode) => _getDisplayMode(displayIndex, modeIndex, out mode);

        private delegate int GetDesktopDisplayModeDelegate(int displayIndex, out DisplayMode mode);
        private static GetDesktopDisplayModeDelegate _getDesktopDisplayMode;
        public static int GetDesktopDisplayMode(int displayIndex, out DisplayMode mode) => _getDesktopDisplayMode(displayIndex, out mode);

        private delegate int GetCurrentDisplayModeDelegate(int displayIndex, out DisplayMode mode);
        private static GetCurrentDisplayModeDelegate _getCurrentDisplayMode;
        public static int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode) => _getCurrentDisplayMode(displayIndex, out mode);

        private delegate DisplayMode GetClosestDisplayModeDelegate(int displayIndex, ref DisplayMode mode, out DisplayMode closest);
        private static GetClosestDisplayModeDelegate _getClosestDisplayMode;
        public static DisplayMode GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest) => _getClosestDisplayMode(displayIndex, ref mode, out closest);

        private delegate int GetWindowDisplayIndexDelegate(Window window);
        private static GetWindowDisplayIndexDelegate _getWindowDisplayIndex;
        public static int GetWindowDisplayIndex(Window window) => _getWindowDisplayIndex(window);

        private delegate int SetWindowDisplayModeDelegate(Window window, ref DisplayMode mode);
        private static SetWindowDisplayModeDelegate _setWindowDisplayMode;
        public static int SetWindowDisplayMode(Window window, ref DisplayMode mode) => _setWindowDisplayMode(window, ref mode);


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

      
        public static void SetWindowSize(Window window, int width, int height);


        public static void GetWindowSize(Window window, out int width, out int height);
        

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

        //
        // SDL_vulkan.h
        //
        private delegate int VulkanLoadLibraryDelegate(string path);
        private static VulkanLoadLibraryDelegate _vulkanLoadLibrary;
        public static int VulkanLoadLibrary(string path) => _vulkanLoadLibrary(path);

        private delegate IntPtr VulkanGetVkGetInstanceProcAddrDelegate();
        private static VulkanGetVkGetInstanceProcAddrDelegate _vulkanGetVkGetInstanceProcAddr;
        public static IntPtr VulkanGetVkGetInstanceProcAddr() => _vulkanGetVkGetInstanceProcAddr();

        private delegate void VulkanUnloadLibraryDelegate();
        private static VulkanUnloadLibraryDelegate _vulkanUnloadLibrary;
        public static void VulkanUnloadLibrary() => _vulkanUnloadLibrary();

        private delegate bool VulkanGetInstanceExtensionsDelegate(Window window, ref uint count, string[] names);
        private static VulkanGetInstanceExtensionsDelegate _vulkanGetInstanceExtensions;
        public static bool VulkanGetInstanceExtensions(Window window, ref uint count, string[] names) => _vulkanGetInstanceExtensions(window, ref count, names);

        private delegate bool VulkanCreateSurfaceDelegate(Window window, Vulkan.Vk.Instance instance, out Vulkan.Vk.Surface surface);
        private static VulkanCreateSurfaceDelegate _vulkanCreateSurface;
        public static bool VulkanCreateSurface(Window window, Vulkan.Vk.Instance instance, out Vulkan.Vk.Surface surface) => _vulkanCreateSurface(window, instance, out surface);

        private delegate void VulkanGetDrawableSizeDelegate(Window window, out int? w, out int? h);
        private static VulkanGetDrawableSizeDelegate _vulkanGetDrawableSize;
        public static void VulkanGetDrawableSize(Window window, out int? w, out int? h) => _vulkanGetDrawableSize(window, out w, out h);

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