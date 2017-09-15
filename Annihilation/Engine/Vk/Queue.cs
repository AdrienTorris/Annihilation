namespace Engine.Vk
{
    public unsafe struct Queue
    {
        public QueueHandle Handle { get; }
        public Device Device { get; }

        private static QueueSubmitDelegate _queueSubmit;
        private static QueueWaitIdleDelegate _queueWaitIdle;
        private static QueueBindSparseDelegate _queueBindSparse;
        private static QueuePresentKHRDelegate _queuePresentKHR;

        public Queue(QueueHandle handle, Device device)
        {
            Handle = handle;
            Device = device;
        }

        public void Submit(uint submitCount, SubmitInfo* submits, FenceHandle fence)
        {
            _queueSubmit = _queueSubmit ?? Device.GetDeviceProcAddr<QueueSubmitDelegate>(FunctionName.QueueSubmit);

            _queueSubmit(Handle, submitCount, submits, fence).CheckError();
        }

        public void WaitIdle()
        {
            _queueWaitIdle = _queueWaitIdle ??
                             Device.GetDeviceProcAddr<QueueWaitIdleDelegate>(FunctionName.QueueWaitIdle);

            _queueWaitIdle(Handle).CheckError();
        }

        public void BindSparse(uint bindInfoCount, ref BindSparseInfo bindInfo, FenceHandle fence)
        {
            _queueBindSparse = _queueBindSparse ??
                               Device.GetDeviceProcAddr<QueueBindSparseDelegate>(FunctionName.QueueBindSparse);

            _queueBindSparse(Handle, bindInfoCount, ref bindInfo, fence).CheckError();
        }

        public void Present(ref PresentInfo presentInfo)
        {
            _queuePresentKHR = _queuePresentKHR ??
                               Device.GetDeviceProcAddr<QueuePresentKHRDelegate>(FunctionName.QueuePresentKHR);

            _queuePresentKHR(Handle, ref presentInfo).CheckError();
        }
    }
}