using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        [Flags]
        public enum SDL_RendererFlags : uint
        {
            Software = 1 << 0,
            Accelerated = 1 << 1,
            PresentVSync = 1 << 2,
            TargetTexture = 1 << 3
        }

        [Flags]
        public enum SDL_RendererFlip
        {
            None = 0,
            Horizontal = 1 << 0,
            Vertical = 1 << 1
        }

        public enum SDL_TextureAccess
        {
            Static,
            Streaming,
            Target
        }

        [Flags]
        public enum SDL_TextureModulate
        {
            None = 0,
            Horizontal = 1 << 0,
            Vertical = 1 << 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SDL_RendererInfo
        {
            public IntPtr Name; // const char*
            public SDL_RendererFlags Flags;
            public uint NumTextureFormats;
            public fixed uint TextureFormats[16];
            public int MaxTextureWidth;
            public int MaxTextureHeight;
        }

        /**
         *  \brief Get the number of 2D rendering drivers available for the current
         *         display.
         *
         *  A render driver is a set of code that handles rendering and texture
         *  management on a particular display.  Normally there is only one, but
         *  some drivers may have several available with different capabilities.
         *
         *  \sa SDL_GetRenderDriverInfo()
         *  \sa SDL_CreateRenderer()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetNumRenderDrivers();

        /**
         *  \brief Get information about a specific 2D rendering driver for the current
         *         display.
         *
         *  \param index The index of the driver to query information about.
         *  \param info  A pointer to an SDL_RendererInfo struct to be filled with
         *               information on the rendering driver.
         *
         *  \return 0 on success, -1 if the index was out of range.
         *
         *  \sa SDL_CreateRenderer()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetRenderDriverInfo(int index, out SDL_RendererInfo info);

        /**
         *  \brief Create a window and default renderer
         *
         *  \param width    The width of the window
         *  \param height   The height of the window
         *  \param window_flags The flags used to create the window
         *  \param window   A pointer filled with the window, or NULL on error
         *  \param renderer A pointer filled with the renderer, or NULL on error
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_CreateWindowAndRenderer(
                                        int width, int height, SDL_WindowFlags window_flags,
                                        out IntPtr window, out IntPtr renderer);


        /**
         *  \brief Create a 2D rendering context for a window.
         *
         *  \param window The window where rendering is displayed.
         *  \param index    The index of the rendering driver to initialize, or -1 to
         *                  initialize the first one supporting the requested flags.
         *  \param flags    ::SDL_RendererFlags.
         *
         *  \return A valid rendering context or NULL if there was an error.
         *
         *  \sa SDL_CreateSoftwareRenderer()
         *  \sa SDL_GetRendererInfo()
         *  \sa SDL_DestroyRenderer()
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateRenderer(IntPtr window,
                                                       int index, SDL_RendererFlags flags);

        /**
         *  \brief Create a 2D software rendering context for a surface.
         *
         *  \param surface The surface where rendering is done.
         *
         *  \return A valid rendering context or NULL if there was an error.
         *
         *  \sa SDL_CreateRenderer()
         *  \sa SDL_DestroyRenderer()
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

        /**
         *  \brief Get the renderer associated with a window.
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_GetRenderer(IntPtr window);

        /**
         *  \brief Get information about a rendering context.
         */
        [DllImport(LibName)]
        public extern static int SDL_GetRendererInfo(IntPtr renderer,
                                                        out SDL_RendererInfo info);

        /**
         *  \brief Get the output size in pixels of a rendering context.
         */
        [DllImport(LibName)]
        public extern static int SDL_GetRendererOutputSize(IntPtr renderer,
                                                             out int w, out int h);

        /**
         *  \brief Create a texture for a rendering context.
         *
         *  \param renderer The renderer.
         *  \param format The format of the texture.
         *  \param access One of the enumerated values in ::SDL_TextureAccess.
         *  \param w      The width of the texture in pixels.
         *  \param h      The height of the texture in pixels.
         *
         *  \return The created texture is returned, or NULL if no rendering context was
         *          active,  the format was unsupported, or the width or height were out
         *          of range.
         *
         *  \sa SDL_QueryTexture()
         *  \sa SDL_UpdateTexture()
         *  \sa SDL_DestroyTexture()
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateTexture(IntPtr renderer,
                                                                uint format,
                                                                int access, int w,
                                                                int h);

        /**
         *  \brief Create a texture from an existing surface.
         *
         *  \param renderer The renderer.
         *  \param surface The surface containing pixel data used to fill the texture.
         *
         *  \return The created texture is returned, or NULL on error.
         *
         *  \note The surface is not modified or freed by this function.
         *
         *  \sa SDL_QueryTexture()
         *  \sa SDL_DestroyTexture()
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        /**
         *  \brief Query the attributes of a texture
         *
         *  \param texture A texture to be queried.
         *  \param format  A pointer filled in with the raw format of the texture.  The
         *                 actual format may differ, but pixel transfers will use this
         *                 format.
         *  \param access  A pointer filled in with the actual access to the texture.
         *  \param w       A pointer filled in with the width of the texture in pixels.
         *  \param h       A pointer filled in with the height of the texture in pixels.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         */
        [DllImport(LibName)]
        public extern static int SDL_QueryTexture(IntPtr texture,
                                                    out uint format, out int access,
                                                    out int w, out int h);

        /**
         *  \brief Set an additional color value used in render copy operations.
         *
         *  \param texture The texture to update.
         *  \param r       The red color value multiplied into copy operations.
         *  \param g       The green color value multiplied into copy operations.
         *  \param b       The blue color value multiplied into copy operations.
         *
         *  \return 0 on success, or -1 if the texture is not valid or color modulation
         *          is not supported.
         *
         *  \sa SDL_GetTextureColorMod()
         */
        [DllImport(LibName)]
        public extern static int SDL_SetTextureColorMod(IntPtr texture,
                                                           byte r, byte g, byte b);


        /**
         *  \brief Get the additional color value used in render copy operations.
         *
         *  \param texture The texture to query.
         *  \param r         A pointer filled in with the current red color value.
         *  \param g         A pointer filled in with the current green color value.
         *  \param b         A pointer filled in with the current blue color value.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         *
         *  \sa SDL_SetTextureColorMod()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetTextureColorMod(IntPtr texture,
                                                          out byte r, out byte g,
                                                          out byte b);

        /**
         *  \brief Set an additional alpha value used in render copy operations.
         *
         *  \param texture The texture to update.
         *  \param alpha     The alpha value multiplied into copy operations.
         *
         *  \return 0 on success, or -1 if the texture is not valid or alpha modulation
         *          is not supported.
         *
         *  \sa SDL_GetTextureAlphaMod()
         */
        [DllImport(LibName)]
        public extern static int SDL_SetTextureAlphaMod(IntPtr texture,
                                                           byte alpha);

        /**
         *  \brief Get the additional alpha value used in render copy operations.
         *
         *  \param texture The texture to query.
         *  \param alpha     A pointer filled in with the current alpha value.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         *
         *  \sa SDL_SetTextureAlphaMod()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetTextureAlphaMod(IntPtr texture,
                                                          out byte alpha);

        /**
         *  \brief Set the blend mode used for texture copy operations.
         *
         *  \param texture The texture to update.
         *  \param blendMode ::SDL_BlendMode to use for texture blending.
         *
         *  \return 0 on success, or -1 if the texture is not valid or the blend mode is
         *          not supported.
         *
         *  \note If the blend mode is not supported, the closest supported mode is
         *        chosen.
         *
         *  \sa SDL_GetTextureBlendMode()
         */
        [DllImport(LibName)]
        public extern static int SDL_SetTextureBlendMode(IntPtr texture,
                                                            SDL_BlendMode blendMode);

        /**
         *  \brief Get the blend mode used for texture copy operations.
         *
         *  \param texture   The texture to query.
         *  \param blendMode A pointer filled in with the current blend mode.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         *
         *  \sa SDL_SetTextureBlendMode()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetTextureBlendMode(IntPtr texture,
                                                           out SDL_BlendMode blendMode);

        /**
         *  \brief Update the given texture rectangle with new pixel data.
         *
         *  \param texture   The texture to update
         *  \param rect      A pointer to the rectangle of pixels to update, or NULL to
         *                   update the entire texture.
         *  \param pixels    The raw pixel data.
         *  \param pitch     The number of bytes in a row of pixel data, including padding between lines.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         *
         *  \note This is a fairly slow function.
         */
        [DllImport(LibName)]
        public static extern int SDL_UpdateTexture(
            IntPtr texture,
            ref SDL_Rect rect,
            IntPtr pixels,
            int pitch
        );

        /* texture refers to an SDL_Texture* */
        [DllImport(LibName)]
        public static extern int SDL_UpdateTexture(
            IntPtr texture,
            IntPtr rect,
            IntPtr pixels,
            int pitch
        );

        /**
         *  \brief Update a rectangle within a planar YV12 or IYUV texture with new pixel data.
         *
         *  \param texture   The texture to update
         *  \param rect      A pointer to the rectangle of pixels to update, or NULL to
         *                   update the entire texture.
         *  \param Yplane    The raw pixel data for the Y plane.
         *  \param Ypitch    The number of bytes between rows of pixel data for the Y plane.
         *  \param Uplane    The raw pixel data for the U plane.
         *  \param Upitch    The number of bytes between rows of pixel data for the U plane.
         *  \param Vplane    The raw pixel data for the V plane.
         *  \param Vpitch    The number of bytes between rows of pixel data for the V plane.
         *
         *  \return 0 on success, or -1 if the texture is not valid.
         *
         *  \note You can use SDL_UpdateTexture() as long as your pixel data is
         *        a contiguous block of Y and U/V planes in the proper order, but
         *        this function is available if your pixel data is not contiguous.
         */
        /*[DllImport(LibName)]
        public extern static int SDL_UpdateYUVTexture(IntPtr texture,
                                                 ref SDL_Rect rect,
                                                 const byte* Yplane, int Ypitch,
                                                 const byte* Uplane, int Upitch,
                                                 const byte* Vplane, int Vpitch);*/

        /**
         *  \brief Lock a portion of the texture for write-only pixel access.
         *
         *  \param texture   The texture to lock for access, which was created with
         *                   ::SDL_TEXTUREACCESS_STREAMING.
         *  \param rect      A pointer to the rectangle to lock for access. If the rect
         *                   is NULL, the entire texture will be locked.
         *  \param pixels    This is filled in with a pointer to the locked pixels,
         *                   appropriately offset by the locked area.
         *  \param pitch     This is filled in with the pitch of the locked pixels.
         *
         *  \return 0 on success, or -1 if the texture is not valid or was not created with ::SDL_TEXTUREACCESS_STREAMING.
         *
         *  \sa SDL_UnlockTexture()
         */
        [DllImport(LibName)]
        public static extern int SDL_LockTexture(
            IntPtr texture,
            ref SDL_Rect rect,
            out IntPtr pixels,
            out int pitch
        );

        /// <summary>
        /// Use this function to lock a portion of the texture for write-only pixel access. This overload
        /// allows for passing an IntPtr.Zero (null) rect value to lock the entire texture.
        /// </summary>
        /// <param name="texture">the texture to lock for access, which was created with
        /// SDL_TEXTUREACCESS_STREAMING (refers to a SDL_Texture*)</param>
        /// <param name="rect">an SDL_Rect structure representing the area to lock for access;
        /// NULL to lock the entire texture </param>
        /// <param name="pixels">this is filled in with a pointer to the locked pixels, appropriately
        /// offset by the locked area (refers to a void*)</param>
        /// <param name="pitch">this is filled in with the pitch of the locked pixels </param>
        /// <returns>Returns 0 on success or a negative error code if the texture is not valid or
        /// was not created with SDL_TEXTUREACCESS_STREAMING; call <see cref="SDL_GetError()"/> for more information. </returns>
        [DllImport(LibName)]
        public static extern int SDL_LockTexture(
            IntPtr texture,
            IntPtr rect,
            out IntPtr pixels,
            out int pitch
        );

        /**
         *  \brief Unlock a texture, uploading the changes to video memory, if needed.
         *
         *  \sa SDL_LockTexture()
         */
        [DllImport(LibName)]
        public extern static void SDL_UnlockTexture(IntPtr texture);

        /**
         * \brief Determines whether a window supports the use of render targets
         *
         * \param renderer The renderer that will be checked
         *
         * \return SDL_TRUE if supported, SDL_FALSE if not.
         */
        [DllImport(LibName)]
        public extern static bool SDL_RenderTargetSupported(IntPtr renderer);

        /**
         * \brief Set a texture as the current rendering target.
         *
         * \param renderer The renderer.
         * \param texture The targeted texture, which must be created with the SDL_TEXTUREACCESS_TARGET flag, or NULL for the default render target
         *
         * \return 0 on success, or -1 on error
         *
         *  \sa SDL_GetRenderTarget()
         */
        [DllImport(LibName)]
        public extern static int SDL_SetRenderTarget(IntPtr renderer,
                                                        IntPtr texture);

        /**
         * \brief Get the current render target or NULL for the default render target.
         *
         * \return The current render target
         *
         *  \sa SDL_SetRenderTarget()
         */
        [DllImport(LibName)]
        public extern static IntPtr SDL_GetRenderTarget(IntPtr renderer);

        /**
         *  \brief Set device independent resolution for rendering
         *
         *  \param renderer The renderer for which resolution should be set.
         *  \param w      The width of the logical resolution
         *  \param h      The height of the logical resolution
         *
         *  This function uses the viewport and scaling functionality to allow a fixed logical
         *  resolution for rendering, regardless of the actual output resolution.  If the actual
         *  output resolution doesn't have the same aspect ratio the output rendering will be
         *  centered within the output display.
         *
         *  If the output display is a window, mouse events in the window will be filtered
         *  and scaled so they seem to arrive within the logical resolution.
         *
         *  \note If this function results in scaling or subpixel drawing by the
         *        rendering backend, it will be handled using the appropriate
         *        quality hints.
         *
         *  \sa SDL_RenderGetLogicalSize()
         *  \sa SDL_RenderSetScale()
         *  \sa SDL_RenderSetViewport()
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderSetLogicalSize(IntPtr renderer, int w, int h);

        /**
         *  \brief Get device independent resolution for rendering
         *
         *  \param renderer The renderer from which resolution should be queried.
         *  \param w      A pointer filled with the width of the logical resolution
         *  \param h      A pointer filled with the height of the logical resolution
         *
         *  \sa SDL_RenderSetLogicalSize()
         */
        [DllImport(LibName)]
        public extern static void SDL_RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

        /**
         *  \brief Set whether to force integer scales for resolution-independent rendering
         *
         *  \param renderer The renderer for which integer scaling should be set.
         *  \param enable   Enable or disable integer scaling
         *
         *  This function restricts the logical viewport to integer values - that is, when
         *  a resolution is between two multiples of a logical size, the viewport size is
         *  rounded down to the lower multiple.
         *
         *  \sa SDL_RenderSetLogicalSize()
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderSetIntegerScale(IntPtr renderer,
                                                              bool enable);

        /**
         *  \brief Get whether integer scales are forced for resolution-independent rendering
         *
         *  \param renderer The renderer from which integer scaling should be queried.
         *
         *  \sa SDL_RenderSetIntegerScale()
         */
        [DllImport(LibName)]
        public extern static bool SDL_RenderGetIntegerScale(IntPtr renderer);

        /**
         *  \brief Set the drawing area for rendering on the current target.
         *
         *  \param renderer The renderer for which the drawing area should be set.
         *  \param rect The rectangle representing the drawing area, or NULL to set the viewport to the entire target.
         *
         *  The x,y of the viewport rect represents the origin for rendering.
         *
         *  \return 0 on success, or -1 on error
         *
         *  \note If the window associated with the renderer is resized, the viewport is automatically reset.
         *
         *  \sa SDL_RenderGetViewport()
         *  \sa SDL_RenderSetLogicalSize()
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderSetViewport(IntPtr renderer,
                                                  ref SDL_Rect rect);

        /**
         *  \brief Get the drawing area for the current target.
         *
         *  \sa SDL_RenderSetViewport()
         */
        [DllImport(LibName)]
        public extern static void SDL_RenderGetViewport(IntPtr renderer,
                                                          out SDL_Rect rect);

        /**
         *  \brief Set the clip rectangle for the current target.
         *
         *  \param renderer The renderer for which clip rectangle should be set.
         *  \param rect   A pointer to the rectangle to set as the clip rectangle, or
         *                NULL to disable clipping.
         *
         *  \return 0 on success, or -1 on error
         *
         *  \sa SDL_RenderGetClipRect()
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderSetClipRect(IntPtr renderer,
                                                  ref SDL_Rect rect);

        /**
         *  \brief Get the clip rectangle for the current target.
         *
         *  \param renderer The renderer from which clip rectangle should be queried.
         *  \param rect   A pointer filled in with the current clip rectangle, or
         *                an empty rectangle if clipping is disabled.
         *
         *  \sa SDL_RenderSetClipRect()
         */
        [DllImport(LibName)]
        public extern static void SDL_RenderGetClipRect(IntPtr renderer,
                                                         out SDL_Rect rect);

        /**
         *  \brief Get whether clipping is enabled on the given renderer.
         *
         *  \param renderer The renderer from which clip state should be queried.
         *
         *  \sa SDL_RenderGetClipRect()
         */
        [DllImport(LibName)]
        public extern static bool SDL_RenderIsClipEnabled(IntPtr renderer);


        /**
         *  \brief Set the drawing scale for rendering on the current target.
         *
         *  \param renderer The renderer for which the drawing scale should be set.
         *  \param scaleX The horizontal scaling factor
         *  \param scaleY The vertical scaling factor
         *
         *  The drawing coordinates are scaled by the x/y scaling factors
         *  before they are used by the renderer.  This allows resolution
         *  independent drawing with a single coordinate system.
         *
         *  \note If this results in scaling or subpixel drawing by the
         *        rendering backend, it will be handled using the appropriate
         *        quality hints.  For best results use integer scaling factors.
         *
         *  \sa SDL_RenderGetScale()
         *  \sa SDL_RenderSetLogicalSize()
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderSetScale(IntPtr renderer,
                                                       float scaleX, float scaleY);

        /**
         *  \brief Get the drawing scale for the current target.
         *
         *  \param renderer The renderer from which drawing scale should be queried.
         *  \param scaleX A pointer filled in with the horizontal scaling factor
         *  \param scaleY A pointer filled in with the vertical scaling factor
         *
         *  \sa SDL_RenderSetScale()
         */
        [DllImport(LibName)]
        public extern static void SDL_RenderGetScale(IntPtr renderer,
                                                      out float scaleX, out float scaleY);

        /**
         *  \brief Set the color used for drawing operations (Rect, Line and Clear).
         *
         *  \param renderer The renderer for which drawing color should be set.
         *  \param r The red value used to draw on the rendering target.
         *  \param g The green value used to draw on the rendering target.
         *  \param b The blue value used to draw on the rendering target.
         *  \param a The alpha value used to draw on the rendering target, usually
         *           ::SDL_ALPHA_OPAQUE (255).
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_SetRenderDrawColor(IntPtr renderer,
                                                   byte r, byte g, byte b,
                                                   byte a);

        /**
         *  \brief Get the color used for drawing operations (Rect, Line and Clear).
         *
         *  \param renderer The renderer from which drawing color should be queried.
         *  \param r A pointer to the red value used to draw on the rendering target.
         *  \param g A pointer to the green value used to draw on the rendering target.
         *  \param b A pointer to the blue value used to draw on the rendering target.
         *  \param a A pointer to the alpha value used to draw on the rendering target,
         *           usually ::SDL_ALPHA_OPAQUE (255).
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_GetRenderDrawColor(IntPtr renderer,
                                                  out byte r, out byte g, out byte b,
                                                  out byte a);

        /**
         *  \brief Set the blend mode used for drawing operations (Fill and Line).
         *
         *  \param renderer The renderer for which blend mode should be set.
         *  \param blendMode ::SDL_BlendMode to use for blending.
         *
         *  \return 0 on success, or -1 on error
         *
         *  \note If the blend mode is not supported, the closest supported mode is
         *        chosen.
         *
         *  \sa SDL_GetRenderDrawBlendMode()
         */
        [DllImport(LibName)]
        public extern static int SDL_SetRenderDrawBlendMode(IntPtr renderer,
                                                               SDL_BlendMode blendMode);

        /**
         *  \brief Get the blend mode used for drawing operations.
         *
         *  \param renderer The renderer from which blend mode should be queried.
         *  \param blendMode A pointer filled in with the current blend mode.
         *
         *  \return 0 on success, or -1 on error
         *
         *  \sa SDL_SetRenderDrawBlendMode()
         */
        [DllImport(LibName)]
        public extern static int SDL_GetRenderDrawBlendMode(IntPtr renderer,
                                                              out SDL_BlendMode blendMode);

        /**
         *  \brief Clear the current rendering target with the drawing color
         *
         *  This function clears the entire rendering target, ignoring the viewport and
         *  the clip rectangle.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderClear(IntPtr renderer);

        /**
         *  \brief Draw a point on the current rendering target.
         *
         *  \param renderer The renderer which should draw a point.
         *  \param x The x coordinate of the point.
         *  \param y The y coordinate of the point.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderDrawPoint(IntPtr renderer,
                                                        int x, int y);

        /**
         *  \brief Draw multiple points on the current rendering target.
         *
         *  \param renderer The renderer which should draw multiple points.
         *  \param points The points to draw
         *  \param count The number of points to draw
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderDrawPoints(
            IntPtr renderer,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            SDL_Point[] points,
            int count
        );

        /**
         *  \brief Draw a line on the current rendering target.
         *
         *  \param renderer The renderer which should draw a line.
         *  \param x1 The x coordinate of the start point.
         *  \param y1 The y coordinate of the start point.
         *  \param x2 The x coordinate of the end point.
         *  \param y2 The y coordinate of the end point.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderDrawLine(IntPtr renderer,
                                                       int x1, int y1, int x2, int y2);

        /**
         *  \brief Draw a series of connected lines on the current rendering target.
         *
         *  \param renderer The renderer which should draw multiple lines.
         *  \param points The points along the lines
         *  \param count The number of points, drawing count-1 lines
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderDrawLines(
            IntPtr renderer,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            SDL_Point[] points,
            int count
        );

        /**
         *  \brief Draw a rectangle on the current rendering target.
         *
         *  \param renderer The renderer which should draw a rectangle.
         *  \param rect A pointer to the destination rectangle, or NULL to outline the entire rendering target.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderDrawRect(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
		 * This overload allows for IntPtr.Zero (null) to be passed for rect.
		 */
        [DllImport(LibName)]
        public static extern int SDL_RenderDrawRect(
            IntPtr renderer,
            IntPtr rect
        );

        /**
         *  \brief Draw some number of rectangles on the current rendering target.
         *
         *  \param renderer The renderer which should draw multiple rectangles.
         *  \param rects A pointer to an array of destination rectangles.
         *  \param count The number of rectangles.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderDrawRects(
            IntPtr renderer,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
            SDL_Rect[] rects,
            int count
        );

        /**
         *  \brief Fill a rectangle on the current rendering target with the drawing color.
         *
         *  \param renderer The renderer which should fill a rectangle.
         *  \param rect A pointer to the destination rectangle, or NULL for the entire
         *              rendering target.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderFillRect(
            IntPtr renderer,
            ref SDL_Rect rect
        );

        /* renderer refers to an SDL_Renderer*, rect to an SDL_Rect*.
		 * This overload allows for IntPtr.Zero (null) to be passed for rect.
		 */
        [DllImport(LibName)]
        public static extern int SDL_RenderFillRect(
            IntPtr renderer,
            IntPtr rect
        );

        /**
         *  \brief Fill some number of rectangles on the current rendering target with the drawing color.
         *
         *  \param renderer The renderer which should fill multiple rectangles.
         *  \param rects A pointer to an array of destination rectangles.
         *  \param count The number of rectangles.
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderFillRects(
            IntPtr renderer,
            [In()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 2)]
                SDL_Rect[] rects,
            int count
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
        [DllImport(LibName)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_Rect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
		 * Internally, this function contains logic to use default values when
		 * source and destination rectangles are passed as NULL.
		 * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
		 */
        [DllImport(LibName)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_Rect dstrect
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
		 * Internally, this function contains logic to use default values when
		 * source and destination rectangles are passed as NULL.
		 * This overload allows for IntPtr.Zero (null) to be passed for dstrect.
		 */
        [DllImport(LibName)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            IntPtr dstrect
        );

        [DllImport(LibName)]
        public static extern int SDL_RenderCopy(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            IntPtr dstrect
        );

        /**
         *  \brief Copy a portion of the source texture to the current rendering target, rotating it by angle around the given center
         *
         *  \param renderer The renderer which should copy parts of a texture.
         *  \param texture The source texture.
         *  \param srcrect   A pointer to the source rectangle, or NULL for the entire
         *                   texture.
         *  \param dstrect   A pointer to the destination rectangle, or NULL for the
         *                   entire rendering target.
         *  \param angle    An angle in degrees that indicates the rotation that will be applied to dstrect
         *  \param center   A pointer to a point indicating the point around which dstrect will be rotated (if NULL, rotation will be done around dstrect.w/2, dstrect.h/2).
         *  \param flip     An SDL_RendererFlip value stating which flipping actions should be performed on the texture
         *
         *  \return 0 on success, or -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            ref SDL_Rect srcrect,
            ref SDL_Rect dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /* renderer refers to an SDL_Renderer*, texture to an SDL_Texture*.
		 * Internally, this function contains logic to use default values when
		 * source, destination, and/or center are passed as NULL.
		 * This overload allows for IntPtr.Zero (null) to be passed for srcrect.
		 */
        [DllImport(LibName)]
        public static extern int SDL_RenderCopyEx(
            IntPtr renderer,
            IntPtr texture,
            IntPtr srcrect,
            ref SDL_Rect dstrect,
            double angle,
            ref SDL_Point center,
            SDL_RendererFlip flip
        );

        /**
         *  \brief Read pixels from the current rendering target.
         *
         *  \param renderer The renderer from which pixels should be read.
         *  \param rect   A pointer to the rectangle to read, or NULL for the entire
         *                render target.
         *  \param format The desired format of the pixel data, or 0 to use the format
         *                of the rendering target
         *  \param pixels A pointer to be filled in with the pixel data
         *  \param pitch  The pitch of the pixels parameter.
         *
         *  \return 0 on success, or -1 if pixel reading is not supported.
         *
         *  \warning This is a very slow operation, and should not be used frequently.
         */
        [DllImport(LibName)]
        public extern static int SDL_RenderReadPixels(IntPtr renderer, ref SDL_Rect rect, uint format, IntPtr pixels, int pitch);

        /**
         *  \brief Update the screen with rendering performed.
         */
        [DllImport(LibName)]
        public extern static void SDL_RenderPresent(IntPtr renderer);

        /**
         *  \brief Destroy the specified texture.
         *
         *  \sa SDL_CreateTexture()
         *  \sa SDL_CreateTextureFromSurface()
         */
        [DllImport(LibName)]
        public extern static void SDL_DestroyTexture(IntPtr texture);

        /**
         *  \brief Destroy the rendering context for a window and free associated
         *         textures.
         *
         *  \sa SDL_CreateRenderer()
         */
        [DllImport(LibName)]
        public extern static void SDL_DestroyRenderer(IntPtr renderer);


        /**
         *  \brief Bind the texture to the current OpenGL/ES/ES2 context for use with
         *         OpenGL instructions.
         *
         *  \param texture  The SDL texture to bind
         *  \param texw     A pointer to a float that will be filled with the texture width
         *  \param texh     A pointer to a float that will be filled with the texture height
         *
         *  \return 0 on success, or -1 if the operation is not supported
         */
        [DllImport(LibName)]
        public extern static int SDL_GL_BindTexture(IntPtr texture, out float texw, out float texh);

        /**
         *  \brief Unbind a texture from the current OpenGL/ES/ES2 context.
         *
         *  \param texture  The SDL texture to unbind
         *
         *  \return 0 on success, or -1 if the operation is not supported
         */
        [DllImport(LibName)]
        public extern static int SDL_GL_UnbindTexture(IntPtr texture);
    }
}