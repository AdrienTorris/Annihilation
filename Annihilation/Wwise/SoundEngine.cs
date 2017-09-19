using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Annihilation.Wwise
{
    public enum Result
    {
        NotImplemented = 0,  ///< This feature is not implemented.
        Success = 1, ///< The operation was successful.
        Fail = 2,    ///< The operation failed.
        PartialSuccess = 3,  ///< The operation succeeded partially.
        NotCompatible = 4,   ///< Incompatible formats
        AlreadyConnected = 5,    ///< The stream is already connected to another node.
        NameNotSet = 6,  ///< Trying to open a file when its name was not set
        InvalidFile = 7, ///< An unexpected value causes the file to be invalid.
        AudioFileHeaderTooLarge = 8, ///< The file header is too large.
        MaxReached = 9,  ///< The maximum was reached.
        InputsInUsed = 10,   ///< Inputs are currently used.
        OutputsInUsed = 11,  ///< Outputs are currently used.
        InvalidName = 12,    ///< The name is invalid.
        NameAlreadyInUse = 13,   ///< The name is already in use.
        InvalidID = 14,  ///< The ID is invalid.
        IDNotFound = 15, ///< The ID was not found.
        InvalidInstanceID = 16,  ///< The InstanceID is invalid.
        NoMoreData = 17, ///< No more data is available from the source.
        NoSourceAvailable = 18,  ///< There is no child (source) associated with the node.
        StateGroupAlreadyExists = 19,    ///< The StateGroup already exists.
        InvalidStateGroup = 20,  ///< The StateGroup is not a valid channel.
        ChildAlreadyHasAParent = 21, ///< The child already has a parent.
        InvalidLanguage = 22,    ///< The language is invalid (applies to the Low-Level I/O).
        CannotAddItseflAsAChild = 23,    ///< It is not possible to add itself as its own child.
        //TransitionNotFound		= 24,	///< The transition is not in the list.
        //TransitionNotStartable	= 25,	///< Start allowed in the Running and Done states.
        //TransitionNotRemovable	= 26,	///< Must not be in the Computing state.
        //UsersListFull			= 27,	///< No one can be added any more, could be MaxReached.
        //UserAlreadyInList		= 28,	///< This user is already there.
        UserNotInList = 29,  ///< This user is not there.
        NoTransitionPoint = 30,  ///< Not in use.
        InvalidParameter = 31,   ///< Something is not within bounds.
        ParameterAdjusted = 32,  ///< Something was not within bounds and was relocated to the nearest OK value.
        IsA3DSound = 33, ///< The sound has 3D parameters.
        NotA3DSound = 34,    ///< The sound does not have 3D parameters.
        ElementAlreadyInList = 35,   ///< The item could not be added because it was already in the list.
        PathNotFound = 36,   ///< This path is not known.
        PathNoVertices = 37, ///< Stuff in vertices before trying to start it
        PathNotRunning = 38, ///< Only a running path can be paused.
        PathNotPaused = 39,  ///< Only a paused path can be resumed.
        PathNodeAlreadyInList = 40,  ///< This path is already there.
        PathNodeNotInList = 41,  ///< This path is not there.
        VoiceNotFound = 42,  ///< Unknown in our voices list
        DataNeeded = 43, ///< The consumer needs more.
        NoDataNeeded = 44,   ///< The consumer does not need more.
        DataReady = 45,  ///< The provider has available data.
        NoDataReady = 46,    ///< The provider does not have available data.
        NoMoreSlotAvailable = 47,    ///< Not enough space to load bank.
        SlotNotFound = 48,   ///< Bank error.
        ProcessingOnly = 49, ///< No need to fetch new data.
        MemoryLeak = 50, ///< Debug mode only.
        CorruptedBlockList = 51, ///< The memory manager's block list has been corrupted.
        InsufficientMemory = 52, ///< Memory error.
        Cancelled = 53,  ///< The requested action was cancelled (not an error).
        UnknownBankID = 54,  ///< Trying to load a bank using an ID which is not defined.
        IsProcessing = 55,   ///< Asynchronous pipeline component is processing.
        BankReadError = 56,  ///< Error while reading a bank.
        InvalidSwitchType = 57,  ///< Invalid switch type (used with the switch container)
        VoiceDone = 58,  ///< Internal use only.
        UnknownEnvironment = 59, ///< This environment is not defined.
        EnvironmentInUse = 60,   ///< This environment is used by an object.
        UnknownObject = 61,  ///< This object is not defined.
        NoConversionNeeded = 62, ///< Audio data already in target format, no conversion to perform.
        FormatNotReady = 63,   ///< Source format not known yet.
        WrongBankVersion = 64,   ///< The bank version is not compatible with the current bank reader.
        DataReadyNoProcess = 65, ///< The provider has some data but does not process it (virtual voices).
        FileNotFound = 66,   ///< File not found.
        DeviceNotReady = 67,   ///< IO device not ready (may be because the tray is open)
        CouldNotCreateSecBuffer = 68,   ///< The direct sound secondary buffer creation failed.
        BankAlreadyLoaded = 69,  ///< The bank load failed because the bank is already loaded.
        RenderedFX = 71, ///< The effect on the node is rendered.
        ProcessNeeded = 72,  ///< A routine needs to be executed on some CPU.
        ProcessDone = 73,    ///< The executed routine has finished its execution.
        MemManagerNotInitialized = 74,   ///< The memory manager should have been initialized at this point.
        StreamMgrNotInitialized = 75,    ///< The stream manager should have been initialized at this point.
        SSEInstructionsNotSupported = 76,///< The machine does not support SSE instructions (required on PC).
        Busy = 77,   ///< The system is busy and could not process the request.
        UnsupportedChannelConfig = 78,   ///< Channel configuration is not supported in the current execution context.
        PluginMediaNotAvailable = 79,    ///< Plugin media is not available for effect.
        MustBeVirtualized = 80,  ///< Sound was Not Allowed to play.
        CommandTooLarge = 81,    ///< SDK command is too large to fit in the command queue.
        RejectedByFilter = 82,   ///< A play request was rejected due to the MIDI filter parameters.
        InvalidCustomPlatformName = 83,  ///< Detecting incompatibility between Custom platform of banks and custom platform of connected application
        DLLCannotLoad = 84	///< DLL could not be loaded, either because it is not found or one dependency is missing.
    }

    public static class SoundEngine
    {
        public const string LibraryName = "AkSoundEngine";

        [SuppressUnmanagedCodeSecurity]
        public unsafe class Native
        {
            [DllImport(LibraryName)]
            public static extern bool IsInitialized();

            [DllImport(LibraryName)]
            public static extern Result Init();
        }
    }
}