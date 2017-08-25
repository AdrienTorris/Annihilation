using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    //
    // SDL_events.h
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate int EventFilter(void* userdata, Event* sdlEvent);
    
    //
    // SDL_hints.h
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void HintCallback(void* userData, Text name, Text oldValue, Text newValue);

    //
    // SDL_log.h
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void LogOutputFunction(void* userData, LogCategory category, LogPriority priority, Text message);

    //
    // SDL_timer.h
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate TimerID TimerCallback(uint interval, IntPtr param);

    //
    // SDL_video.h
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate HitTestResult HitTest(Window window, Point* area, void* data);
}