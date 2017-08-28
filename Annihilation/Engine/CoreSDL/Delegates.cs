using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        //
        // SDL_audio.h
        //
        public unsafe delegate void AudioCallback(IntPtr userData, byte[] stream, int length);

        public unsafe delegate void AudioFilter(ref AudioCVT cvt, AudioFormat format);

        //
        // SDL_events.h
        //
        public unsafe delegate int EventFilter(void* userdata, Event* sdlEvent);

        //
        // SDL_hints.h
        //
        public unsafe delegate void HintCallback(void* userData, Text name, Text oldValue, Text newValue);

        //
        // SDL_log.h
        //
        public unsafe delegate void LogOutputFunction(void* userData, LogCategory category, LogPriority priority, Text message);

        //
        // SDL_timer.h
        //
        public delegate TimerID TimerCallback(uint interval, IntPtr param);

        //
        // SDL_video.h
        //
        public unsafe delegate HitTestResult HitTest(Window window, Point* area, void* data);
    }
}