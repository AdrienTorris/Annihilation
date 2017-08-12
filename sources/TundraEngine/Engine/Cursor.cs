using System.Runtime.CompilerServices; 
    
using Engine.SDL;
using static Engine.SDL.SDL;

namespace Engine
{
    public class Cursor
    {
        public CursorHandle Handle;

        public static readonly Cursor Null = new Cursor();

        //
        // Constructors
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Cursor() => Handle = new CursorHandle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Cursor(CursorHandle handle) => Handle = handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Cursor(byte[] data, byte[] mask, int w, int h, int hotX, int hotY) => Handle = Native.SDL_CreateCursor(data, mask, w, h, hotX, hotY);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Cursor(Surface surface, int hotX, int hotY) => Handle = Native.SDL_CreateColorCursor(surface, hotX, hotY);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Cursor(SystemCursor systemCursor) => Handle = Native.SDL_CreateSystemCursor(systemCursor);

        //
        // Methods
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Show() => Native.SDL_ShowCursor(State.Enable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Hide() => Native.SDL_ShowCursor(State.Disable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public State GetState() => Native.SDL_ShowCursor(State.Query);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free() => Native.SDL_FreeCursor(Handle);

        //
        // Static methods
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetActive(Cursor cursor) => Native.SDL_SetCursor(cursor.Handle);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Cursor GetActive() => new Cursor(Native.SDL_GetCursor().CheckErrorAndReturn());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Cursor GetDefault() => new Cursor(Native.SDL_GetDefaultCursor().CheckErrorAndReturn());
    }
}