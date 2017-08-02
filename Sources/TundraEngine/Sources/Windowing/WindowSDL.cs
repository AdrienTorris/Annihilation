using System;

using TundraEngine.Vulkan;
using TundraEngine.SDL;
using static TundraEngine.SDL.SDL;

namespace TundraEngine.Windowing
{
    internal class WindowSDL : IWindow
    {
        public WindowManagerInfo WindowManagerInfo { get; set; }
        
        // SDL
        private Window _window;
        private SysWMInfo _sysWMInfo;

        // Vulkan
        private Instance _instance;
        private PhysicalDevice _physicalDevice;
        private PhysicalDeviceFeatures _physicalDeviceFeatures;
        private Vulkan.Surface _surface;
        private SurfaceCapabilities _surfaceCapabilities;
        private Swapchain _swapchain;
        private uint _numSwapChainImages;
        private uint _currentCommandBuffer;
        private CommandPool _commandPool;
        private CommandPool _transientCommandPool;
        private CommandBuffer[] _commandBuffers;
        private Fence[] _commandBufferFences;
        private bool[] _commandBufferSubmitted;
        private Framebuffer[] _mainFramebuffers;
        private Semaphore[] _drawCompleteSemaphores;
        private Framebuffer[] _uiFramebuffers;
        private Image[] _swapchainImages;
        private ImageView[] _swapchainImageViews;
        private Semaphore[] _imageAcquiredSemaphores;
        private DeviceMemory[] _colorBuffersMemory;
        private ImageView[] _colorBuffersView;
        private Image _msaaColorBuffer;
        private DeviceMemory _msaaColorBufferMemory;
        private ImageView _msaaColorBufferView;
        private DescriptorSet _postprocessDescriptorSet;
#if USE_DEPTH
        private Image _depthBuffer;
        private DeviceMemory _depthBufferMemory;
        private ImageView _depthBufferView;
#endif

        public WindowSDL(ref WindowSettings settings)
        {
            int previousDisplay = -1;

            // Init video subsystem
            InitSubSystem(InitFlags.Video);
            
            // Get desktop display mode
            GetDesktopDisplayMode(settings.Monitor, out SDL.DisplayMode displayMode);

            // Create the window if needed, hidden
            if (_window.IsNull)
            {
                WindowFlags flags = WindowFlags.Hidden;
                if (settings.Mode == WindowMode.BorderlessWindow)
                {
                    flags |= WindowFlags.Borderless;
                }

                _window = new Window(settings.Name, Window.PositionUndefined, Window.PositionUndefined, settings.Width, settings.Height, flags);

                GetVersion(out _sysWMInfo.Version);
                _window.GetWMInfo(ref _sysWMInfo);
            }
            else
            {
                previousDisplay = _window.DisplayIndex;
            }

            // Ensure the window is not fullscreen
            if (_window.Flags.Has(WindowFlags.Fullscreen))
            {
                _window.SetFullscreen(0);
            }

            // Set window size and display mode
            _window.SetSize(settings.Width, settings.Height);
            if (previousDisplay >= 0)
            {
                _window.SetPosition((int)Window.PositionCenteredDisplay((uint)previousDisplay), (int)Window.PositionCenteredDisplay((uint)previousDisplay));
            }
            else
            {
                _window.SetPosition(Window.PositionCentered, Window.PositionCentered);
            }
            _window.SetDefaultDisplayMode();
            _window.SetBordered(false);
                
            // Window manager
            switch (_sysWMInfo.SubSystem)
            {
                case SysWMType.Windows:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Windows,
                        Windows = new WindowManagerInfo.WindowsInfo
                        {
                            HWindow = _sysWMInfo.Info.Windows.Window,
                            HInstance = _sysWMInfo.Info.Windows.HInstance
                        }
                    };
                    break;
                case SysWMType.X11:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.X11,
                        X11 = new WindowManagerInfo.X11Info
                        {
                            Window = _sysWMInfo.Info.X11.Window,
                            Connection = _sysWMInfo.Info.X11.Display
                        }
                    };
                    break;
                case SysWMType.Wayland:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Wayland,
                        Wayland = new WindowManagerInfo.WaylandInfo
                        {
                            Surface = _sysWMInfo.Info.Wayland.Surface,
                            Display = _sysWMInfo.Info.Wayland.Display
                        }
                    };
                    break;
                default:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.None
                    };
                    throw new Exception("Could not find a supported window manager.");
            }
        }

        private void SetWindowMode(int width, int height, bool fullscreen)
        {

        }

#region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void DisposeUnmanaged()
        {
            if (!disposedValue)
            {
                _window.Destroy();

                disposedValue = true;
            }
        }
        
        ~WindowSDL()
        {
            DisposeUnmanaged();
        }
        
        public void Dispose()
        {
            DisposeUnmanaged();
            GC.SuppressFinalize(this);
        }
#endregion
    }
}