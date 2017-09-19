namespace Annihilation.Vk
{
    public unsafe struct CommandBuffer
    {
        public CommandBufferHandle Handle { get; private set; }
        public Device Device { get; private set; }

        private static BeginCommandBufferDelegate _beginCommandBuffer;
        private static EndCommandBufferDelegate _endCommandBuffer;
        private static ResetCommandBufferDelegate _resetCommandBuffer;
        private static CmdBindPipelineDelegate _cmdBindPipeline;
        private static CmdSetViewportDelegate _cmdSetViewport;
        private static CmdSetScissorDelegate _cmdSetScissor;
        private static CmdSetLineWidthDelegate _cmdSetLineWidth;
        private static CmdSetDepthBiasDelegate _cmdSetDepthBias;
        private static CmdSetBlendConstantsDelegate _cmdSetBlendConstants;
        private static CmdSetDepthBoundsDelegate _cmdSetDepthBounds;
        private static CmdSetStencilCompareMaskDelegate _cmdSetStencilCompareMask;
        private static CmdSetStencilWriteMaskDelegate _cmdSetStencilWriteMask;
        private static CmdSetStencilReferenceDelegate _cmdSetStencilReference;
        private static CmdBindDescriptorSetsDelegate _cmdBindDescriptorSets;
        private static CmdBindIndexBufferDelegate _cmdBindIndexBuffer;
        private static CmdBindVertexBuffersDelegate _cmdBindVertexBuffers;
        private static CmdDrawDelegate _cmdDraw;
        private static CmdDrawIndexedDelegate _cmdDrawIndexed;
        private static CmdDrawIndirectDelegate _cmdDrawIndirect;
        private static CmdDrawIndexedIndirectDelegate _cmdDrawIndexedIndirect;
        private static CmdDispatchDelegate _cmdDispatch;
        private static CmdDispatchIndirectDelegate _cmdDispatchIndirect;
        private static CmdCopyBufferDelegate _cmdCopyBuffer;
        private static CmdCopyImageDelegate _cmdCopyImage;
        private static CmdBlitImageDelegate _cmdBlitImage;
        private static CmdCopyBufferToImageDelegate _copyBufferToImage;
        private static CmdCopyImageToBufferDelegate _copyImageToBuffer;
        private static CmdUpdateBufferDelegate _cmdUpdateBuffer;
        private static CmdFillBufferDelegate _cmdFillBuffer;
        private static CmdClearColorImageDelegate _cmdClearColorImage;
        private static CmdClearDepthStencilImageDelegate _cmdClearDepthStencilImage;
        private static CmdClearAttachmentsDelegate _cmdClearAttachments;
        private static CmdResolveImageDelegate _cmdResolveImage;
        private static CmdSetEventDelegate _cmdSetEvent;
        private static CmdResetEventDelegate _cmdResetEvent;
        private static CmdWaitEventsDelegate _cmdWaitEvents;
        private static CmdPipelineBarrierDelegate _cmdPipelineBarrier;
        private static CmdBeginQueryDelegate _cmdBeginQuery;
        private static CmdEndQueryDelegate _cmdEndQuery;
        private static CmdResetQueryPoolDelegate _cmdResetQueryPool;
        private static CmdWriteTimestampDelegate _cmdWriteTimestamp;
        private static CmdCopyQueryPoolResultsDelegate _cmdCopyQueryPoolResults;
        private static CmdPushConstantsDelegate _cmdPushConstants;
        private static CmdBeginRenderPassDelegate _cmdBeginRenderPass;
        private static CmdNextSubpassDelegate _cmdNextSubpass;
        private static CmdEndRenderPassDelegate _cmdEndRenderPass;
        private static CmdExecuteCommandsDelegate _cmdExecuteCommands;

        public CommandBuffer(CommandBufferHandle handle, Device device)
        {
            Handle = handle;
            Device = device;
        }

        public void BeginCommandBuffer(ref CommandBufferBeginInfo beginInfo)
        {
            _beginCommandBuffer = _beginCommandBuffer ??
                                  Device.GetDeviceProcAddr<BeginCommandBufferDelegate>(FunctionName.BeginCommandBuffer);
        }

        public void EndCommandBuffer()
        {
            _endCommandBuffer = _endCommandBuffer ??
                                  Device.GetDeviceProcAddr<EndCommandBufferDelegate>(FunctionName.EndCommandBuffer);
        }

        public void ResetCommandBuffer(CommandBufferResetFlags flags)
        {
            _resetCommandBuffer = _resetCommandBuffer ??
                                  Device.GetDeviceProcAddr<ResetCommandBufferDelegate>(FunctionName.ResetCommandBuffer);
        }

        public void BindPipeline(PipelineBindPoint pipelineBindPoint, PipelineHandle pipeline)
        {
            _cmdBindPipeline = _cmdBindPipeline ??
                                  Device.GetDeviceProcAddr<CmdBindPipelineDelegate>(FunctionName.CmdBindPipeline);
        }

        public void SetViewport(uint firstViewport, uint viewportCount, Viewport* viewports)
        {
            _cmdSetViewport = _cmdSetViewport ??
                                  Device.GetDeviceProcAddr<CmdSetViewportDelegate>(FunctionName.CmdSetViewport);
        }

        public void SetScissor(uint firstScissor, uint scissorCount, Rect2D* scissors)
        {
            _cmdSetScissor = _cmdSetScissor ??
                                  Device.GetDeviceProcAddr<CmdSetScissorDelegate>(FunctionName.CmdSetScissor);
        }

        public void SetLineWidth(float lineWidth)
        {
            _cmdSetLineWidth = _cmdSetLineWidth ??
                                  Device.GetDeviceProcAddr<CmdSetLineWidthDelegate>(FunctionName.CmdSetLineWidth);
        }

        public void SetDepthBias(float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor)
        {
            _cmdSetDepthBias = _cmdSetDepthBias ??
                                  Device.GetDeviceProcAddr<CmdSetDepthBiasDelegate>(FunctionName.CmdSetDepthBias);
        }

        public void SetBlendConstants(float* blendConstants)
        {
            _cmdSetBlendConstants = _cmdSetBlendConstants ??
                                  Device.GetDeviceProcAddr<CmdSetBlendConstantsDelegate>(FunctionName.CmdSetBlendConstants);
        }

        public void SetDepthBounds(float minDepthBounds, float maxDepthBounds)
        {
            _cmdSetDepthBounds = _cmdSetDepthBounds ??
                                  Device.GetDeviceProcAddr<CmdSetDepthBoundsDelegate>(FunctionName.CmdSetDepthBounds);
        }

        public void SetStencilCompareMask(StencilFaceFlags faceMask, uint compareMask)
        {
            _cmdSetStencilCompareMask = _cmdSetStencilCompareMask ??
                                  Device.GetDeviceProcAddr<CmdSetStencilCompareMaskDelegate>(FunctionName.CmdSetStencilCompareMask);
        }

        public void SetStencilWriteMask(StencilFaceFlags faceMask, uint writeMask)
        {
            _cmdSetStencilWriteMask = _cmdSetStencilWriteMask ??
                                  Device.GetDeviceProcAddr<CmdSetStencilWriteMaskDelegate>(FunctionName.CmdSetStencilWriteMask);
        }

        public void SetStencilReference(StencilFaceFlags faceMask, uint reference)
        {
            _cmdSetStencilReference = _cmdSetStencilReference ??
                                  Device.GetDeviceProcAddr<CmdSetStencilReferenceDelegate>(FunctionName.CmdSetStencilReference);
        }

        public void BindDescriptorSets(PipelineBindPoint pipelineBindPoint, PipelineLayoutHandle layout, uint firstSet,
            uint descriptorSetCount, DescriptorSetHandle* descriptorSets, uint dynamicOffsetCount, uint* dynamicOffsets)
        {
            _cmdBindDescriptorSets = _cmdBindDescriptorSets ??
                                  Device.GetDeviceProcAddr<CmdBindDescriptorSetsDelegate>(FunctionName.CmdBindDescriptorSets);
        }

        public void BindIndexBuffer(BufferHandle buffer, DeviceSize offset, IndexType indexType)
        {
            _cmdBindIndexBuffer = _cmdBindIndexBuffer ??
                                  Device.GetDeviceProcAddr<CmdBindIndexBufferDelegate>(FunctionName.CmdBindIndexBuffer);
        }

        public void BindVertexBuffers(uint firstBinding, uint bindingCount, BufferHandle* buffers, DeviceSize* offsets)
        {
            _cmdBindVertexBuffers = _cmdBindVertexBuffers ??
                                  Device.GetDeviceProcAddr<CmdBindVertexBuffersDelegate>(FunctionName.CmdBindVertexBuffers);
        }

        public void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance)
        {
            _cmdDraw = _cmdDraw ??
                                  Device.GetDeviceProcAddr<CmdDrawDelegate>(FunctionName.CmdDraw);
        }

        public void DrawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset,
            uint firstInstance)
        {
            _cmdDrawIndexed = _cmdDrawIndexed ??
                                  Device.GetDeviceProcAddr<CmdDrawIndexedDelegate>(FunctionName.CmdDrawIndexed);
        }

        public void DrawIndirect(BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride)
        {
            _cmdDrawIndirect = _cmdDrawIndirect ??
                                  Device.GetDeviceProcAddr<CmdDrawIndirectDelegate>(FunctionName.CmdDrawIndirect);
        }

        public void DrawIndexedIndirect(BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride)
        {
            _cmdDrawIndexedIndirect = _cmdDrawIndexedIndirect ??
                                  Device.GetDeviceProcAddr<CmdDrawIndexedIndirectDelegate>(FunctionName.CmdDrawIndexedIndirect);
        }

        public void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ)
        {
            _cmdDispatch = _cmdDispatch ??
                                  Device.GetDeviceProcAddr<CmdDispatchDelegate>(FunctionName.CmdDispatch);
        }

        public void DispatchIndirect(BufferHandle buffer, DeviceSize offset)
        {
            _cmdDispatchIndirect = _cmdDispatchIndirect ??
                                  Device.GetDeviceProcAddr<CmdDispatchIndirectDelegate>(FunctionName.CmdDispatchIndirect);
        }

        public void CopyBuffer(BufferHandle srcBuffer, BufferHandle dstBuffer, uint regionCount, BufferCopy* regions)
        {
            _cmdCopyBuffer = _cmdCopyBuffer ??
                                  Device.GetDeviceProcAddr<CmdCopyBufferDelegate>(FunctionName.CmdCopyBuffer);
        }

        public void CopyImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageCopy* regions)
        {
            _cmdCopyImage = _cmdCopyImage ??
                                  Device.GetDeviceProcAddr<CmdCopyImageDelegate>(FunctionName.CmdCopyImage);
        }

        public void BlitImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageBlit* regions, Filter filter)
        {
            _cmdBlitImage = _cmdBlitImage ??
                                  Device.GetDeviceProcAddr<CmdBlitImageDelegate>(FunctionName.CmdBlitImage);
        }

        public void CopyBufferToImage(BufferHandle srcBuffer, ImageHandle dstImage, ImageLayout dstImageLayout,
            uint regionCount, BufferImageCopy* regions)
        {
            _copyBufferToImage = _copyBufferToImage ??
                                  Device.GetDeviceProcAddr<CmdCopyBufferToImageDelegate>(FunctionName.CmdCopyBufferToImage);
        }

        public void CopyImageToBuffer(ImageHandle srcImage, ImageLayout srcImageLayout, BufferHandle dstBuffer,
            uint regionCount, BufferImageCopy* regions)
        {
            _copyImageToBuffer = _copyImageToBuffer ??
                                  Device.GetDeviceProcAddr<CmdCopyImageToBufferDelegate>(FunctionName.CmdCopyImageToBuffer);
        }

        public void UpdateBuffer(BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data)
        {
            _cmdUpdateBuffer = _cmdUpdateBuffer ??
                                  Device.GetDeviceProcAddr<CmdUpdateBufferDelegate>(FunctionName.CmdUpdateBuffer);
        }

        public void FillBuffer(BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data)
        {
            _cmdFillBuffer = _cmdFillBuffer ??
                                  Device.GetDeviceProcAddr<CmdFillBufferDelegate>(FunctionName.CmdFillBuffer);
        }

        public void ClearColorImage(ImageHandle image, ImageLayout imageLayout, ref ClearColorValue color,
            uint rangeCount, ImageSubresourceRange* ranges)
        {
            _cmdClearColorImage = _cmdClearColorImage ??
                                  Device.GetDeviceProcAddr<CmdClearColorImageDelegate>(FunctionName.CmdClearColorImage);
        }

        public void ClearDepthStencilImage(ImageHandle image, ImageLayout imageLayout,
            ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange* ranges)
        {
            _cmdClearDepthStencilImage = _cmdClearDepthStencilImage ??
                                  Device.GetDeviceProcAddr<CmdClearDepthStencilImageDelegate>(FunctionName.CmdClearDepthStencilImage);
        }

        public void ClearAttachments(uint attachmentCount, ClearAttachment* attachments, uint rectCount,
            ClearRect* rects)
        {
            _cmdClearAttachments = _cmdClearAttachments ??
                                  Device.GetDeviceProcAddr<CmdClearAttachmentsDelegate>(FunctionName.CmdClearAttachments);
        }

        public void ResolveImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageResolve* regions)
        {
            _cmdResolveImage = _cmdResolveImage ??
                                  Device.GetDeviceProcAddr<CmdResolveImageDelegate>(FunctionName.CmdResolveImage);
        }

        public void SetEvent(EventHandle evt, PipelineStageFlags stageMask)
        {
            _cmdSetEvent = _cmdSetEvent ??
                                  Device.GetDeviceProcAddr<CmdSetEventDelegate>(FunctionName.CmdSetEvent);
        }

        public void ResetEvent(EventHandle evt, PipelineStageFlags stageMask)
        {
            _cmdResetEvent = _cmdResetEvent ??
                                  Device.GetDeviceProcAddr<CmdResetEventDelegate>(FunctionName.CmdResetEvent);
        }

        public void WaitEvents(uint eventCount, EventHandle* events, PipelineStageFlags srcStageMask,
            PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier* memoryBarriers,
            uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount,
            ImageMemoryBarrier* imageMemoryBarriers)
        {
            _cmdWaitEvents = _cmdWaitEvents ??
                                  Device.GetDeviceProcAddr<CmdWaitEventsDelegate>(FunctionName.CmdWaitEvents);
        }

        public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask,
            DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier* memoryBarriers,
            uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount,
            ImageMemoryBarrier* imageMemoryBarriers)
        {
            _cmdPipelineBarrier = _cmdPipelineBarrier ??
                                  Device.GetDeviceProcAddr<CmdPipelineBarrierDelegate>(FunctionName.CmdPipelineBarrier);
        }

        public void BeginQuery(QueryPoolHandle queryPool, uint query, QueryControlFlags flags)
        {
            _cmdBeginQuery = _cmdBeginQuery ??
                                  Device.GetDeviceProcAddr<CmdBeginQueryDelegate>(FunctionName.CmdBeginQuery);
        }

        public void EndQuery(QueryPoolHandle queryPool, uint query)
        {
            _cmdEndQuery = _cmdEndQuery ??
                                  Device.GetDeviceProcAddr<CmdEndQueryDelegate>(FunctionName.CmdEndQuery);
        }

        public void ResetQueryPool(QueryPoolHandle queryPool, uint firstQuery, uint queryCount)
        {
            _cmdResetQueryPool = _cmdResetQueryPool ??
                                  Device.GetDeviceProcAddr<CmdResetQueryPoolDelegate>(FunctionName.CmdResetQueryPool);
        }

        public void WriteTimestamp(PipelineStageFlags pipelineStage, QueryPoolHandle queryPool, uint query)
        {
            _cmdWriteTimestamp = _cmdWriteTimestamp ??
                                  Device.GetDeviceProcAddr<CmdWriteTimestampDelegate>(FunctionName.CmdWriteTimestamp);
        }

        public void CopyQueryPoolResults(QueryPoolHandle queryPool, uint firstQuery, uint queryCount,
            BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags)
        {
            _cmdCopyQueryPoolResults = _cmdCopyQueryPoolResults ??
                                  Device.GetDeviceProcAddr<CmdCopyQueryPoolResultsDelegate>(FunctionName.CmdCopyQueryPoolResults);
        }

        public void PushConstants(PipelineLayoutHandle layout, ShaderStageFlags stageFlags, uint offset, uint size,
            void* values)
        {
            _cmdPushConstants = _cmdPushConstants ??
                                  Device.GetDeviceProcAddr<CmdPushConstantsDelegate>(FunctionName.CmdPushConstants);
        }

        public void BeginRenderPass(ref RenderPassBeginInfo renderPassBegin, SubpassContents contents)
        {
            _cmdBeginRenderPass = _cmdBeginRenderPass ??
                                  Device.GetDeviceProcAddr<CmdBeginRenderPassDelegate>(FunctionName.CmdBeginRenderPass);
        }

        public void NextSubpass(SubpassContents contents)
        {
            _cmdNextSubpass = _cmdNextSubpass ??
                                  Device.GetDeviceProcAddr<CmdNextSubpassDelegate>(FunctionName.CmdNextSubpass);
        }

        public void EndRenderPassDelegate(CommandBufferHandle commandBuffer)
        {
            _cmdEndRenderPass = _cmdEndRenderPass ??
                                  Device.GetDeviceProcAddr<CmdEndRenderPassDelegate>(FunctionName.CmdEndRenderPass);
        }

        public void ExecuteCommands(uint commandBufferCount, CommandBufferHandle* commandBuffers)
        {
            _cmdExecuteCommands = _cmdExecuteCommands ??
                                  Device.GetDeviceProcAddr<CmdExecuteCommandsDelegate>(FunctionName.CmdExecuteCommands);
        }
    }
}