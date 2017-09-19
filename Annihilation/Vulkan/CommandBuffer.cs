namespace Annihilation.Vulkan
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

            _beginCommandBuffer(Handle, ref beginInfo).CheckError();
        }

        public void EndCommandBuffer()
        {
            _endCommandBuffer = _endCommandBuffer ??
                                  Device.GetDeviceProcAddr<EndCommandBufferDelegate>(FunctionName.EndCommandBuffer);

            _endCommandBuffer(Handle).CheckError();
        }

        public void ResetCommandBuffer(CommandBufferResetFlags flags)
        {
            _resetCommandBuffer = _resetCommandBuffer ??
                                  Device.GetDeviceProcAddr<ResetCommandBufferDelegate>(FunctionName.ResetCommandBuffer);

            _resetCommandBuffer(Handle, flags).CheckError();
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

            _cmdSetViewport(Handle, firstViewport, viewportCount, viewports);
        }

        public void SetScissor(uint firstScissor, uint scissorCount, Rect2D* scissors)
        {
            _cmdSetScissor = _cmdSetScissor ??
                                  Device.GetDeviceProcAddr<CmdSetScissorDelegate>(FunctionName.CmdSetScissor);

            _cmdSetScissor(Handle, firstScissor, scissorCount, scissors);
        }

        public void SetLineWidth(float lineWidth)
        {
            _cmdSetLineWidth = _cmdSetLineWidth ??
                                  Device.GetDeviceProcAddr<CmdSetLineWidthDelegate>(FunctionName.CmdSetLineWidth);

            _cmdSetLineWidth(Handle, lineWidth);
        }

        public void SetDepthBias(float depthBiasConstantFactor, float depthBiasClamp, float depthBiasSlopeFactor)
        {
            _cmdSetDepthBias = _cmdSetDepthBias ??
                                  Device.GetDeviceProcAddr<CmdSetDepthBiasDelegate>(FunctionName.CmdSetDepthBias);

            _cmdSetDepthBias(Handle, depthBiasConstantFactor, depthBiasClamp, depthBiasSlopeFactor);
        }

        public void SetBlendConstants(float* blendConstants)
        {
            _cmdSetBlendConstants = _cmdSetBlendConstants ??
                                  Device.GetDeviceProcAddr<CmdSetBlendConstantsDelegate>(FunctionName.CmdSetBlendConstants);

            _cmdSetBlendConstants(Handle, blendConstants);
        }

        public void SetDepthBounds(float minDepthBounds, float maxDepthBounds)
        {
            _cmdSetDepthBounds = _cmdSetDepthBounds ??
                                  Device.GetDeviceProcAddr<CmdSetDepthBoundsDelegate>(FunctionName.CmdSetDepthBounds);

            _cmdSetDepthBounds(Handle, minDepthBounds, maxDepthBounds);
        }

        public void SetStencilCompareMask(StencilFaceFlags faceMask, uint compareMask)
        {
            _cmdSetStencilCompareMask = _cmdSetStencilCompareMask ??
                                  Device.GetDeviceProcAddr<CmdSetStencilCompareMaskDelegate>(FunctionName.CmdSetStencilCompareMask);

            _cmdSetStencilCompareMask(Handle, faceMask, compareMask);
        }

        public void SetStencilWriteMask(StencilFaceFlags faceMask, uint writeMask)
        {
            _cmdSetStencilWriteMask = _cmdSetStencilWriteMask ??
                                  Device.GetDeviceProcAddr<CmdSetStencilWriteMaskDelegate>(FunctionName.CmdSetStencilWriteMask);

            _cmdSetStencilWriteMask(Handle, faceMask, writeMask);
        }

        public void SetStencilReference(StencilFaceFlags faceMask, uint reference)
        {
            _cmdSetStencilReference = _cmdSetStencilReference ??
                                  Device.GetDeviceProcAddr<CmdSetStencilReferenceDelegate>(FunctionName.CmdSetStencilReference);

            _cmdSetStencilReference(Handle, faceMask, reference);
        }

        public void BindDescriptorSets(PipelineBindPoint pipelineBindPoint, PipelineLayoutHandle layout, uint firstSet,
            uint descriptorSetCount, DescriptorSetHandle* descriptorSets, uint dynamicOffsetCount, uint* dynamicOffsets)
        {
            _cmdBindDescriptorSets = _cmdBindDescriptorSets ??
                                  Device.GetDeviceProcAddr<CmdBindDescriptorSetsDelegate>(FunctionName.CmdBindDescriptorSets);

            _cmdBindDescriptorSets(Handle, pipelineBindPoint, layout, firstSet, descriptorSetCount, descriptorSets,
                dynamicOffsetCount, dynamicOffsets);
        }

        public void BindIndexBuffer(BufferHandle buffer, DeviceSize offset, IndexType indexType)
        {
            _cmdBindIndexBuffer = _cmdBindIndexBuffer ??
                                  Device.GetDeviceProcAddr<CmdBindIndexBufferDelegate>(FunctionName.CmdBindIndexBuffer);

            _cmdBindIndexBuffer(Handle, buffer, offset, indexType);
        }

        public void BindVertexBuffers(uint firstBinding, uint bindingCount, BufferHandle* buffers, DeviceSize* offsets)
        {
            _cmdBindVertexBuffers = _cmdBindVertexBuffers ??
                                  Device.GetDeviceProcAddr<CmdBindVertexBuffersDelegate>(FunctionName.CmdBindVertexBuffers);

            _cmdBindVertexBuffers(Handle, firstBinding, bindingCount, buffers, offsets);
        }

        public void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance)
        {
            _cmdDraw = _cmdDraw ?? Device.GetDeviceProcAddr<CmdDrawDelegate>(FunctionName.CmdDraw);

            _cmdDraw(Handle, vertexCount, instanceCount, firstVertex, firstInstance);
        }

        public void DrawIndexed(uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset,
            uint firstInstance)
        {
            _cmdDrawIndexed = _cmdDrawIndexed ??
                                  Device.GetDeviceProcAddr<CmdDrawIndexedDelegate>(FunctionName.CmdDrawIndexed);

            _cmdDrawIndexed(Handle, indexCount, instanceCount, firstIndex, vertexOffset, firstInstance);
        }

        public void DrawIndirect(BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride)
        {
            _cmdDrawIndirect = _cmdDrawIndirect ??
                                  Device.GetDeviceProcAddr<CmdDrawIndirectDelegate>(FunctionName.CmdDrawIndirect);

            _cmdDrawIndirect(Handle, buffer, offset, drawCount, stride);
        }

        public void DrawIndexedIndirect(BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride)
        {
            _cmdDrawIndexedIndirect = _cmdDrawIndexedIndirect ??
                                  Device.GetDeviceProcAddr<CmdDrawIndexedIndirectDelegate>(FunctionName.CmdDrawIndexedIndirect);

            _cmdDrawIndexedIndirect(Handle, buffer, offset, drawCount, stride);
        }

        public void Dispatch(uint groupCountX, uint groupCountY, uint groupCountZ)
        {
            _cmdDispatch = _cmdDispatch ??
                                  Device.GetDeviceProcAddr<CmdDispatchDelegate>(FunctionName.CmdDispatch);

            _cmdDispatch(Handle, groupCountX, groupCountY, groupCountZ);
        }

        public void DispatchIndirect(BufferHandle buffer, DeviceSize offset)
        {
            _cmdDispatchIndirect = _cmdDispatchIndirect ??
                                  Device.GetDeviceProcAddr<CmdDispatchIndirectDelegate>(FunctionName.CmdDispatchIndirect);

            _cmdDispatchIndirect(Handle, buffer, offset);
        }

        public void CopyBuffer(BufferHandle srcBuffer, BufferHandle dstBuffer, uint regionCount, BufferCopy* regions)
        {
            _cmdCopyBuffer = _cmdCopyBuffer ??
                                  Device.GetDeviceProcAddr<CmdCopyBufferDelegate>(FunctionName.CmdCopyBuffer);

            _cmdCopyBuffer(Handle, srcBuffer, dstBuffer, regionCount, regions);
        }

        public void CopyImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageCopy* regions)
        {
            _cmdCopyImage = _cmdCopyImage ??
                                  Device.GetDeviceProcAddr<CmdCopyImageDelegate>(FunctionName.CmdCopyImage);

            _cmdCopyImage(Handle, srcImage, srcImageLayout, dstImage, dstImageLayout, regionCount, regions);
        }

        public void BlitImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageBlit* regions, Filter filter)
        {
            _cmdBlitImage = _cmdBlitImage ??
                                  Device.GetDeviceProcAddr<CmdBlitImageDelegate>(FunctionName.CmdBlitImage);

            _cmdBlitImage(Handle, srcImage, srcImageLayout, dstImage, dstImageLayout, regionCount, regions, filter);
        }

        public void CopyBufferToImage(BufferHandle srcBuffer, ImageHandle dstImage, ImageLayout dstImageLayout,
            uint regionCount, BufferImageCopy* regions)
        {
            _copyBufferToImage = _copyBufferToImage ??
                                  Device.GetDeviceProcAddr<CmdCopyBufferToImageDelegate>(FunctionName.CmdCopyBufferToImage);

            _copyBufferToImage(Handle, srcBuffer, dstImage, dstImageLayout, regionCount, regions);
        }

        public void CopyImageToBuffer(ImageHandle srcImage, ImageLayout srcImageLayout, BufferHandle dstBuffer,
            uint regionCount, BufferImageCopy* regions)
        {
            _copyImageToBuffer = _copyImageToBuffer ??
                                  Device.GetDeviceProcAddr<CmdCopyImageToBufferDelegate>(FunctionName.CmdCopyImageToBuffer);

            _copyImageToBuffer(Handle, srcImage, srcImageLayout, dstBuffer, regionCount, regions);
        }

        public void UpdateBuffer(BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data)
        {
            _cmdUpdateBuffer = _cmdUpdateBuffer ??
                                  Device.GetDeviceProcAddr<CmdUpdateBufferDelegate>(FunctionName.CmdUpdateBuffer);

            _cmdUpdateBuffer(Handle, dstBuffer, dstOffset, dataSize, data);
        }

        public void FillBuffer(BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data)
        {
            _cmdFillBuffer = _cmdFillBuffer ??
                                  Device.GetDeviceProcAddr<CmdFillBufferDelegate>(FunctionName.CmdFillBuffer);

            _cmdFillBuffer(Handle, dstBuffer, dstOffset, size, data);
        }

        public void ClearColorImage(ImageHandle image, ImageLayout imageLayout, ref ClearColorValue color,
            uint rangeCount, ImageSubresourceRange* ranges)
        {
            _cmdClearColorImage = _cmdClearColorImage ??
                                  Device.GetDeviceProcAddr<CmdClearColorImageDelegate>(FunctionName.CmdClearColorImage);

            _cmdClearColorImage(Handle, image, imageLayout, ref color, rangeCount, ranges);
        }

        public void ClearDepthStencilImage(ImageHandle image, ImageLayout imageLayout,
            ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange* ranges)
        {
            _cmdClearDepthStencilImage = _cmdClearDepthStencilImage ??
                                  Device.GetDeviceProcAddr<CmdClearDepthStencilImageDelegate>(FunctionName.CmdClearDepthStencilImage);

            _cmdClearDepthStencilImage(Handle, image, imageLayout, ref depthStencil, rangeCount, ranges);
        }

        public void ClearAttachments(uint attachmentCount, ClearAttachment* attachments, uint rectCount,
            ClearRect* rects)
        {
            _cmdClearAttachments = _cmdClearAttachments ??
                                  Device.GetDeviceProcAddr<CmdClearAttachmentsDelegate>(FunctionName.CmdClearAttachments);

            _cmdClearAttachments(Handle, attachmentCount, attachments, rectCount, rects);
        }

        public void ResolveImage(ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage,
            ImageLayout dstImageLayout, uint regionCount, ImageResolve* regions)
        {
            _cmdResolveImage = _cmdResolveImage ??
                                  Device.GetDeviceProcAddr<CmdResolveImageDelegate>(FunctionName.CmdResolveImage);

            _cmdResolveImage(Handle, srcImage, srcImageLayout, dstImage, dstImageLayout, regionCount, regions);
        }

        public void SetEvent(EventHandle evt, PipelineStageFlags stageMask)
        {
            _cmdSetEvent = _cmdSetEvent ??
                                  Device.GetDeviceProcAddr<CmdSetEventDelegate>(FunctionName.CmdSetEvent);

            _cmdSetEvent(Handle, evt, stageMask);
        }

        public void ResetEvent(EventHandle evt, PipelineStageFlags stageMask)
        {
            _cmdResetEvent = _cmdResetEvent ??
                                  Device.GetDeviceProcAddr<CmdResetEventDelegate>(FunctionName.CmdResetEvent);

            _cmdResetEvent(Handle, evt, stageMask);
        }

        public void WaitEvents(uint eventCount, EventHandle* events, PipelineStageFlags srcStageMask,
            PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier* memoryBarriers,
            uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount,
            ImageMemoryBarrier* imageMemoryBarriers)
        {
            _cmdWaitEvents = _cmdWaitEvents ??
                                  Device.GetDeviceProcAddr<CmdWaitEventsDelegate>(FunctionName.CmdWaitEvents);

            _cmdWaitEvents(Handle, eventCount, events, srcStageMask, dstStageMask, memoryBarrierCount, memoryBarriers,
                bufferMemoryBarrierCount, bufferMemoryBarriers, imageMemoryBarrierCount, imageMemoryBarriers);
        }

        public void PipelineBarrier(PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask,
            DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier* memoryBarriers,
            uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount,
            ImageMemoryBarrier* imageMemoryBarriers)
        {
            _cmdPipelineBarrier = _cmdPipelineBarrier ??
                                  Device.GetDeviceProcAddr<CmdPipelineBarrierDelegate>(FunctionName.CmdPipelineBarrier);

            _cmdPipelineBarrier(Handle, srcStageMask, dstStageMask, dependencyFlags, memoryBarrierCount, memoryBarriers,
                bufferMemoryBarrierCount, bufferMemoryBarriers, imageMemoryBarrierCount, imageMemoryBarriers);
        }

        public void BeginQuery(QueryPoolHandle queryPool, uint query, QueryControlFlags flags)
        {
            _cmdBeginQuery = _cmdBeginQuery ??
                                  Device.GetDeviceProcAddr<CmdBeginQueryDelegate>(FunctionName.CmdBeginQuery);

            _cmdBeginQuery(Handle, queryPool, query, flags);
        }

        public void EndQuery(QueryPoolHandle queryPool, uint query)
        {
            _cmdEndQuery = _cmdEndQuery ??
                                  Device.GetDeviceProcAddr<CmdEndQueryDelegate>(FunctionName.CmdEndQuery);

            _cmdEndQuery(Handle, queryPool, query);
        }

        public void ResetQueryPool(QueryPoolHandle queryPool, uint firstQuery, uint queryCount)
        {
            _cmdResetQueryPool = _cmdResetQueryPool ??
                                  Device.GetDeviceProcAddr<CmdResetQueryPoolDelegate>(FunctionName.CmdResetQueryPool);

            _cmdResetQueryPool(Handle, queryPool, firstQuery, queryCount);
        }

        public void WriteTimestamp(PipelineStageFlags pipelineStage, QueryPoolHandle queryPool, uint query)
        {
            _cmdWriteTimestamp = _cmdWriteTimestamp ??
                                  Device.GetDeviceProcAddr<CmdWriteTimestampDelegate>(FunctionName.CmdWriteTimestamp);

            _cmdWriteTimestamp(Handle, pipelineStage, queryPool, query);
        }

        public void CopyQueryPoolResults(QueryPoolHandle queryPool, uint firstQuery, uint queryCount,
            BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags)
        {
            _cmdCopyQueryPoolResults = _cmdCopyQueryPoolResults ??
                                  Device.GetDeviceProcAddr<CmdCopyQueryPoolResultsDelegate>(FunctionName.CmdCopyQueryPoolResults);

            _cmdCopyQueryPoolResults(Handle, queryPool, firstQuery, queryCount, dstBuffer, dstOffset, stride, flags);
        }

        public void PushConstants(PipelineLayoutHandle layout, ShaderStageFlags stageFlags, uint offset, uint size,
            void* values)
        {
            _cmdPushConstants = _cmdPushConstants ??
                                  Device.GetDeviceProcAddr<CmdPushConstantsDelegate>(FunctionName.CmdPushConstants);

            _cmdPushConstants(Handle, layout, stageFlags, offset, size, values);
        }

        public void BeginRenderPass(ref RenderPassBeginInfo renderPassBegin, SubpassContents contents)
        {
            _cmdBeginRenderPass = _cmdBeginRenderPass ??
                                  Device.GetDeviceProcAddr<CmdBeginRenderPassDelegate>(FunctionName.CmdBeginRenderPass);

            _cmdBeginRenderPass(Handle, ref renderPassBegin, contents);
        }

        public void NextSubpass(SubpassContents contents)
        {
            _cmdNextSubpass = _cmdNextSubpass ??
                                  Device.GetDeviceProcAddr<CmdNextSubpassDelegate>(FunctionName.CmdNextSubpass);

            _cmdNextSubpass(Handle, contents);
        }

        public void EndRenderPassDelegate(CommandBufferHandle commandBuffer)
        {
            _cmdEndRenderPass = _cmdEndRenderPass ??
                                  Device.GetDeviceProcAddr<CmdEndRenderPassDelegate>(FunctionName.CmdEndRenderPass);

            _cmdEndRenderPass(Handle);
        }

        public void ExecuteCommands(uint commandBufferCount, CommandBufferHandle* commandBuffers)
        {
            _cmdExecuteCommands = _cmdExecuteCommands ??
                                  Device.GetDeviceProcAddr<CmdExecuteCommandsDelegate>(FunctionName.CmdExecuteCommands);

            _cmdExecuteCommands(Handle, commandBufferCount, commandBuffers);
        }
    }
}