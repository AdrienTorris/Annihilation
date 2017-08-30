using System;
using System.Security;

namespace SDL2
{
    [SuppressUnmanagedCodeSecurity]
    public unsafe static partial class SDL
    {
        // SDL_audio.h
        public delegate void AudioCallback(IntPtr userData, byte[] stream, int length);
        public delegate void AudioFilter(ref AudioCVT cvt, AudioFormat format);
        
        // SDL_events.h
        public delegate int EventFilter(void* userdata, Event* sdlEvent);
        
        // SDL_hints.h
        public delegate void HintCallback(void* userData, Text name, Text oldValue, Text newValue);
        
        // SDL_log.h
        public delegate void LogOutputFunction(void* userData, LogCategory category, LogPriority priority, Text message);
        
        // SDL_timer.h
        public delegate TimerID TimerCallback(uint interval, IntPtr param);
        
        // SDL_video.h
        public delegate HitTestResult HitTest(Window window, Point* area, void* data);
    }
}