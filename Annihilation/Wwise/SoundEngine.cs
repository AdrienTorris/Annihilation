using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Annihilation.Wwise
{
    public enum Result
    {
        NotImplemented = 0,
        Success = 1,
        Fail = 2,
        PartialSuccess = 3,
        NotCompatible = 4,
        AlreadyConnected = 5,
        NameNotSet = 6,
        InvalidFile = 7,
        AudioFileHeaderTooLarge = 8,
        MaxReached = 9,
        InputsInUsed = 10,
        OutputsInUsed = 11,
        InvalidName = 12,
        NameAlreadyInUse = 13,
        InvalidID = 14,
        IDNotFound = 15,
        InvalidInstanceID = 16,
        NoMoreData = 17,
        NoSourceAvailable = 18,
        StateGroupAlreadyExists = 19,
        InvalidStateGroup = 20,
        ChildAlreadyHasAParent = 21,
        InvalidLanguage = 22,
        CannotAddItseflAsAChild = 23,
        //TransitionNotFound		= 24,
        //TransitionNotStartable	= 25,
        //TransitionNotRemovable	= 26,
        //UsersListFull			= 27,
        //UserAlreadyInList		= 28,
        UserNotInList = 29,
        NoTransitionPoint = 30,
        InvalidParameter = 31,
        ParameterAdjusted = 32,
        IsA3DSound = 33,
        NotA3DSound = 34,
        ElementAlreadyInList = 35,
        PathNotFound = 36,
        PathNoVertices = 37,
        PathNotRunning = 38,
        PathNotPaused = 39,
        PathNodeAlreadyInList = 40,
        PathNodeNotInList = 41,
        VoiceNotFound = 42,
        DataNeeded = 43,
        NoDataNeeded = 44,
        DataReady = 45,
        NoDataReady = 46,
        NoMoreSlotAvailable = 47,
        SlotNotFound = 48,
        ProcessingOnly = 49,
        MemoryLeak = 50,
        CorruptedBlockList = 51,
        InsufficientMemory = 52,
        Cancelled = 53,
        UnknownBankID = 54,
        IsProcessing = 55,
        BankReadError = 56,
        InvalidSwitchType = 57,
        VoiceDone = 58,
        UnknownEnvironment = 59,
        EnvironmentInUse = 60,
        UnknownObject = 61,
        NoConversionNeeded = 62,
        FormatNotReady = 63,
        WrongBankVersion = 64,
        DataReadyNoProcess = 65,
        FileNotFound = 66,
        DeviceNotReady = 67,
        CouldNotCreateSecBuffer = 68,
        BankAlreadyLoaded = 69,
        RenderedFX = 71,
        ProcessNeeded = 72,
        ProcessDone = 73,
        MemManagerNotInitialized = 74,
        StreamMgrNotInitialized = 75,
        SSEInstructionsNotSupported = 76,
        Busy = 77,
        UnsupportedChannelConfig = 78,
        PluginMediaNotAvailable = 79,
        MustBeVirtualized = 80,
        CommandTooLarge = 81,
        RejectedByFilter = 82,
        InvalidCustomPlatformName = 83,
        DLLCannotLoad
    }

    public enum AudioOutputType
    {
        None = 0,
        Dummy,
        Main,
        Secondary,
        NumBuiltInOutputs,
        Plugin
    }

    public enum PanningRule
    {
        Speakers = 0,
        Headphones = 1
    }

    public enum PluginType
    {
        None = 0,
        Codex = 1,
        Source = 2,
        Effect = 3,
        MotionDevice = 4,
        MotionSource = 5,
        Mixer = 6,
        Sink = 7,
        Mask = 0xF
    }

    [Flags]
    public enum GlobalCallbackLocation : uint
    {
        Register = 1 << 0,
        Begin = 1 << 1,
        PreProcessMessageQueueForRender = 1 << 2,
        PostMessagesProcessed = 1 << 3,
        BeginRender = 1 << 4,
        EndRender = 1 << 5,
        End = 1 << 6,
        Term = 1 << 7,
        Num = 8
    }

    [Flags]
    public enum CallbackType : uint
    {
        None = 0,

        EndOfEvent = 1 << 0,
        EnfOfDynamicSequenceItem = 1 << 1,
        Marker = 1 << 2,
        Duration = 1 << 3,

        SpeakerVolumeMatrix = 1 << 4,

        Starvation = 1 << 5,

        MusicPlaylistSelect = 1 << 6,
        MusicPlayStarted = 1 << 7,

        MusicSyncBeat = 1 << 8,
        MusicSyncBar = 1 << 9,
        MusicSyncEntry = 1 << 10,
        MusicSyncExit = 1 << 11,
        MusicSyncGrid = 1 << 12,
        MusicSyncUserCue = 1 << 13,
        MusicSyncPoint = 1 << 14,
        MusicSyncAll = 0x7F00,

        MidiEvent = 1 << 16,

        CallbackBits = 0xFFFFF,

        EnableGetSourcePlayPosition = 1 << 24,
        EnableGetMusicPlayPosition = 1 << 25,
        EnableGetSourceStreamBuffering = 1 << 26
    }

    public enum ActionOnEventType
    {
        Stop = 0,
        Pause = 1,
        Resume = 2,
        Break = 3,
        ReleaseEnvelope = 4
    }

    public enum CurveInterpolation : byte
    {
        Log3 = 0,
        Sine = 1,
        Log1 = 2,
        InvSCurve = 3,
        Linear = 4,
        SCurve = 5,
        Exp1 = 6,
        SineRecip = 7,
        Exp3 = 8,
        LastFadeCurve = 8,
        Constant = 9
    }

    public enum MultiPositionType
    {
        SingleSource,
        MultiSources,
        MultiDirections
    }

    public enum PreparationType
    {
        Load,
        Unload,
        LoadAndDecode
    }

    public enum BankContent
    {
        StructureOnly,
        All
    }

    public enum GroupType
    {
        Switch = 0,
        State = 1
    }

    [Flags]
    public enum MeteringFlags
    {
        NoMetering = 0,
        Peak = 1 << 0,
        TruePeak = 1 << 1,
        RMS = 1 << 2,
        KPower = 1 << 4
    }

    public unsafe struct InitSettings
    {

    }

    public unsafe struct PlatformInitSettings
    {

    }

    public unsafe struct AudioSettings
    {

    }

    public unsafe struct ChannelConfig
    {

    }

    public unsafe struct ExternalSourceInfo
    {

    }

    public unsafe struct SourcePosition
    {

    }

    public unsafe struct SoundPosition
    {

    }

    public unsafe struct ChannelEmitter
    {

    }

    public unsafe struct SourceSettings
    {

    }

    public unsafe struct AuxSendValue
    {

    }

    public unsafe struct ObstructionOcclusionValues
    {

    }
    
    public unsafe struct MidiPost
    {

    }

    public struct PlayingID : IEquatable<PlayingID>
    {
        public static readonly PlayingID Invalid;

        private uint _value;

        public PlayingID(uint value)
        {
            _value = value;
        }

        public bool Equals(PlayingID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(PlayingID playingID) => playingID._value;
        public static implicit operator PlayingID(uint value) => new PlayingID(value);
    }

    public struct UniqueID : IEquatable<UniqueID>
    {
        private uint _value;

        public bool Equals(UniqueID other)
        {
            throw new NotImplementedException();
        }
    }

    public struct GameObjectID : IEquatable<GameObjectID>
    {
        private ulong _value;

        public bool Equals(GameObjectID other)
        {
            throw new NotImplementedException();
        }
    }

    public struct TimeMs : IEquatable<TimeMs>
    {
        private int _value;

        public TimeMs(int value)
        {
            _value = value;
        }

        public bool Equals(TimeMs other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator int(TimeMs timeMs) => timeMs._value;
        public static implicit operator TimeMs(int value) => new TimeMs(value);
    }

    public struct Priority : IEquatable<Priority>
    {
        private sbyte _value;

        public Priority(sbyte value)
        {
            _value = value;
        }

        public bool Equals(Priority other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator sbyte(Priority priority) => priority._value;
        public static implicit operator Priority(sbyte value) => new Priority(value);
    }

    public struct MemPoolID : IEquatable<MemPoolID>
    {
        private int _value;

        public MemPoolID(int value)
        {
            _value = value;
        }

        public bool Equals(MemPoolID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator int(MemPoolID memPoolID) => memPoolID._value;
        public static implicit operator MemPoolID(int value) => new MemPoolID(value);
    }

    public struct BankID : IEquatable<BankID>
    {
        private uint _value;

        public BankID(uint value)
        {
            _value = value;
        }

        public bool Equals(BankID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(BankID bankID) => bankID._value;
        public static implicit operator BankID(uint value) => new BankID(value);
    }

    public struct RtpcID : IEquatable<RtpcID>
    {
        private uint _value;

        public RtpcID(uint value)
        {
            _value = value;
        }

        public bool Equals(RtpcID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(RtpcID rtpcID) => rtpcID._value;
        public static implicit operator RtpcID(uint value) => new RtpcID(value);
    }

    public struct RtpcValue : IEquatable<RtpcValue>
    {
        private float _value;

        public RtpcValue(float value)
        {
            _value = value;
        }

        public bool Equals(RtpcValue other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator float(RtpcValue rtpcValue) => rtpcValue._value;
        public static implicit operator RtpcValue(float value) => new RtpcValue(value);
    }

    public struct SwitchGroupID : IEquatable<SwitchGroupID>
    {
        private uint _value;

        public SwitchGroupID(uint value)
        {
            _value = value;
        }

        public bool Equals(SwitchGroupID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(SwitchGroupID switchGroupID) => switchGroupID._value;
        public static implicit operator SwitchGroupID(uint value) => new SwitchGroupID(value);
    }

    public struct SwitchStateID : IEquatable<SwitchStateID>
    {
        private uint _value;

        public SwitchStateID(uint value)
        {
            _value = value;
        }

        public bool Equals(SwitchStateID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(SwitchStateID switchStateID) => switchStateID._value;
        public static implicit operator SwitchStateID(uint value) => new SwitchStateID(value);
    }

    public struct TriggerID : IEquatable<TriggerID>
    {
        private uint _value;

        public TriggerID(uint value)
        {
            _value = value;
        }

        public bool Equals(TriggerID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(TriggerID triggerID) => triggerID._value;
        public static implicit operator TriggerID(uint value) => new TriggerID(value);
    }

    public struct StateGroupID : IEquatable<StateGroupID>
    {
        private uint _value;

        public StateGroupID(uint value)
        {
            _value = value;
        }

        public bool Equals(StateGroupID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(StateGroupID stateGroupID) => stateGroupID._value;
        public static implicit operator StateGroupID(uint value) => new StateGroupID(value);
    }

    public struct StateID : IEquatable<StateID>
    {
        private uint _value;

        public StateID(uint value)
        {
            _value = value;
        }

        public bool Equals(StateID other)
        {
            throw new NotImplementedException();
        }

        public static implicit operator uint(StateID stateID) => stateID._value;
        public static implicit operator StateID(uint value) => new StateID(value);
    }

    [SuppressUnmanagedCodeSecurity]
    public static unsafe class SoundEngine
    {
#if PLATFORM_WINDOWS
        public const string LibName = "AkSoundEngine.lib";
#elif PLATFORM_LINUX

#elif PLATFORM_MACOS

#else
#error Unsupported platform
#endif

        [DllImport(LibName)]
        public static extern bool IsInitialized();

        [DllImport(LibName)]
        public static extern Result Init(InitSettings* settings, PlatformInitSettings* platformSettings);

        [DllImport(LibName)]
        public static extern void GetDefaultInitSettings(out InitSettings settings);

        [DllImport(LibName)]
        public static extern void GetDefaultPlatformInitSettings(out PlatformInitSettings platformSettings);

        [DllImport(LibName)]
        public static extern void Term();

        [DllImport(LibName)]
        public static extern Result GetAudioSettings(out AudioSettings audioSettings);

        [DllImport(LibName)]
        public static extern ChannelConfig GetSpeakerConfiguration(AudioOutputType outputType = AudioOutputType.Main, uint outputID = 0);

        [DllImport(LibName)]
        public static extern Result GetPanningRule(out PanningRule panningRule, AudioOutputType outputType = AudioOutputType.Main, uint outputID = 0);

        [DllImport(LibName)]
        public static extern Result SetPanningRule(PanningRule panningRule, AudioOutputType outputType = AudioOutputType.Main, uint outputID = 0);

        [DllImport(LibName)]
        public static extern Result GetSpeakerAngles(float* speakerAngles, ref uint numAngles, out float heightAngle, AudioOutputType outputType = AudioOutputType.Main, uint outputID = 0);

        [DllImport(LibName)]
        public static extern Result SetSpeakerAngles(float* speakerAngles, uint numAngles, float heightAngle, AudioOutputType outputType = AudioOutputType.Main, uint outputID = 0);

        [DllImport(LibName)]
        public static extern Result SetVolumeThreshold(float volumeThresholdDB);

        [DllImport(LibName)]
        public static extern Result SetMaxNumVoicesLimit(ushort maxNumberVoices);

        [DllImport(LibName)]
        public static extern Result RenderAudio(bool allowSyncRender = true);

        [DllImport(LibName)]
        public static extern IntPtr GetGlobalPluginContext();

        [DllImport(LibName)]
        public static extern Result RegisterPlugin(PluginType type, uint companyID, uint pluginID, IntPtr createFunc, IntPtr createParamFunc);

        [DllImport(LibName)]
        public static extern Result RegisterPluginDLL(char* dllName);

        [DllImport(LibName)]
        public static extern Result RegisterCodec(uint companyID, uint codecID, IntPtr fileCreateFunc, IntPtr bankCreateFunc);

        [DllImport(LibName)]
        public static extern Result RegisterGlobalCallback(IntPtr callback, GlobalCallbackLocation location = GlobalCallbackLocation.BeginRender, void* cookie = null);

        [DllImport(LibName)]
        public static extern Result UnregisterGlobalCallback(IntPtr callback, GlobalCallbackLocation location = GlobalCallbackLocation.BeginRender);

        [DllImport(LibName)]
        public static extern uint GetIDFromString(char* @string);

        [DllImport(LibName)]
        public static extern uint GetIDFromString(byte* @string);

        [DllImport(LibName)]
        public static extern PlayingID PostEvent(UniqueID eventID, GameObjectID gameObjectID, CallbackType flags = CallbackType.None, IntPtr callback = default(IntPtr), void* cookie = null, uint externals = 0,
             ExternalSourceInfo* externalSources = null, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern PlayingID PostEvent(char* eventName, GameObjectID gameObjectID, CallbackType flags = CallbackType.None, IntPtr callback = default(IntPtr), void* cookie = null, uint externals = 0,
             ExternalSourceInfo* externalSources = null, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern PlayingID PostEvent(byte* eventName, GameObjectID gameObjectID, CallbackType flags = CallbackType.None, IntPtr callback = default(IntPtr), void* cookie = null, uint externals = 0,
             ExternalSourceInfo* externalSources = null, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result ExecuteActionOnEvent(UniqueID eventID, ActionOnEventType actionType, GameObjectID gameObjectID, TimeMs transitionDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result ExecuteActionOnEvent(char* eventName, ActionOnEventType actionType, GameObjectID gameObjectID, TimeMs transitionDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result ExecuteActionOnEvent(byte* eventName, ActionOnEventType actionType, GameObjectID gameObjectID, TimeMs transitionDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result PostMIDIOnEvent(UniqueID eventID, GameObjectID gameObjectID, MidiPost* posts, ushort numPosts);

        [DllImport(LibName)]
        public static extern Result StopMIDIOnEvent(UniqueID eventID = default(UniqueID), GameObjectID gameObjectID = (ulong)-1);

        [DllImport(LibName)]
        public static extern Result PinEventInStreamCache(UniqueID eventID, Priority activePriority, Priority inactivePriority);

        [DllImport(LibName)]
        public static extern Result PinEventInStreamCache(char* eventName, Priority activePriority, Priority inactivePriority);

        [DllImport(LibName)]
        public static extern Result PinEventInStreamCache(byte* eventName, Priority activePriority, Priority inactivePriority);

        [DllImport(LibName)]
        public static extern Result UnpinEventInStreamCache(UniqueID eventID);

        [DllImport(LibName)]
        public static extern Result UnpinEventInStreamCache(char* eventName);

        [DllImport(LibName)]
        public static extern Result UnpinEventInStreamCache(byte* eventName);

        [DllImport(LibName)]
        public static extern Result GetBufferStatusForPinnedEvent(UniqueID eventID, out float percentBuffered, out bool cachePinnedMemoryFull);

        [DllImport(LibName)]
        public static extern Result GetBufferStatusForPinnedEvent(char* eventName, out float percentBuffered, out bool cachePinnedMemoryFull);

        [DllImport(LibName)]
        public static extern Result GetBufferStatusForPinnedEvent(byte* eventName, out float percentBuffered, out bool cachePinnedMemoryFull);

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(UniqueID eventID, GameObjectID gameObjectID, TimeMs position, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(char* eventName, GameObjectID gameObjectID, TimeMs position, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(byte* eventName, GameObjectID gameObjectID, TimeMs position, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(UniqueID eventID, GameObjectID gameObjectID, float percent, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(char* eventName, GameObjectID gameObjectID, float percent, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern Result SeekOnEvent(byte* eventName, GameObjectID gameObjectID, float percent, bool seekToNearestMarker = false, PlayingID playingID = default(PlayingID));

        [DllImport(LibName)]
        public static extern void CancelEventCallbackCookie(void* cookie);

        [DllImport(LibName)]
        public static extern void CancelEventCallbackGameObject(GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern void CancelEventCallback(PlayingID playingID);

        [DllImport(LibName)]
        public static extern Result GetSourcePlayPosition(PlayingID playingID, out TimeMs position, bool extrapolate = true);

        [DllImport(LibName)]
        public static extern Result GetSourcePlayPositions(PlayingID playingID, SourcePosition* positions, ref uint numPositions, bool extrapolate = true);

        [DllImport(LibName)]
        public static extern Result GetSourceStreamBuffering(PlayingID playingID, out TimeMs buffering, out bool isBuffering);

        [DllImport(LibName)]
        public static extern void StopAll(GameObjectID gameObjectID = (ulong)-1);

        [DllImport(LibName)]
        public static extern void StopPlayingID(PlayingID playingID, TimeMs transitionDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear);

        [DllImport(LibName)]
        public static extern void SetRandomSeed(uint seed);

        [DllImport(LibName)]
        public static extern void MuteBackgroundMusic(bool mute);

        [DllImport(LibName)]
        public static extern bool GetBackgroundMusicMute();

        [DllImport(LibName)]
        public static extern Result SendPluginCustomGameData(UniqueID busID, GameObjectID busObjectID, PluginType type, uint companyID, uint pluginID, void* data, uint sizeInBytes);

        [DllImport(LibName)]
        public static extern Result RegisterGameObj(GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result RegisterGameObj(GameObjectID gameObjectID, byte* objName);

        [DllImport(LibName)]
        public static extern Result UnregisterGameObj(GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result UnregisterAllGameObj();

        [DllImport(LibName)]
        public static extern Result SetPosition(GameObjectID gameObjectID, SoundPosition position);

        [DllImport(LibName)]
        public static extern Result SetMultiplePositions(GameObjectID gameObjectID, SoundPosition* positions, ushort numPositions, MultiPositionType multiPositionType = MultiPositionType.MultiDirections);

        [DllImport(LibName)]
        public static extern Result SetMultiplePositions(GameObjectID gameObjectID, ChannelEmitter* positions, ushort numPositions, MultiPositionType multiPositionType = MultiPositionType.MultiDirections);

        [DllImport(LibName)]
        public static extern Result SetScalingFactor(GameObjectID gameObjectID, float attenuationScalingFactor);

        [DllImport(LibName)]
        public static extern Result ClearBanks();

        [DllImport(LibName)]
        public static extern Result SetBankLoadIOSettings(float throughput, Priority priority);

        [DllImport(LibName)]
        public static extern Result LoadBank(char* name, MemPoolID memPoolID, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(byte* name, MemPoolID memPoolID, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(BankID bankID, MemPoolID memPoolID);

        [DllImport(LibName)]
        public static extern Result LoadBank(void* memoryBankPtr, uint memoryBankSize, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(void* memoryBankPtr, uint memoryBankSize, MemPoolID poolForBankMedia, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result DecodeBank(void* memoryBankPtr, uint memoryBankSize, MemPoolID poolForDecodedBank, void* decodedBankPtr, out uint decodedBankSize);

        [DllImport(LibName)]
        public static extern Result LoadBank(char* name, IntPtr bankCallback, void* cookie, MemPoolID memPoolID, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(byte* name, IntPtr bankCallback, void* cookie, MemPoolID memPoolID, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(BankID bankID, IntPtr bankCallback, void* cookie, MemPoolID memPoolID);

        [DllImport(LibName)]
        public static extern Result LoadBank(void* memoryBankPtr, uint memoryBankSize, IntPtr bankCallback, void* cookie, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result LoadBank(void* memoryBankPtr, uint memoryBankSize, IntPtr bankCallback, void* cookie, MemPoolID poolForBankMedia, out BankID bankID);

        [DllImport(LibName)]
        public static extern Result UnloadBank(char* name, void* memoryBankPtr, out MemPoolID memPoolID);

        [DllImport(LibName)]
        public static extern Result UnloadBank(byte* name, void* memoryBankPtr, out MemPoolID memPoolID);

        [DllImport(LibName)]
        public static extern Result UnloadBank(BankID bankID, void* memoryBankPtr, MemPoolID* memPoolID = null);

        [DllImport(LibName)]
        public static extern Result UnloadBank(char* name, void* memoryBankPtr, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result UnloadBank(byte* name, void* memoryBankPtr, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result UnloadBank(BankID bankID, void* memoryBankPtr, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern void CancelBankCallbackCookie(void* cookie);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, char* name, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, byte* name, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, BankID bankID, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, char* name, IntPtr bankCallback, void* cookie, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, byte* name, IntPtr bankCallback, void* cookie, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result PrepareBank(PreparationType preparationType, BankID bankID, IntPtr bankCallback, void* cookie, BankContent flags = BankContent.All);

        [DllImport(LibName)]
        public static extern Result ClearPreparedEvents();

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, char** names, uint numEvent);

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, byte** names, uint numEvent);

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, UniqueID* eventIDs, uint numEvent);

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, char** names, uint numEvent, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, byte** names, uint numEvent, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result PrepareEvent(PreparationType preparationType, UniqueID* eventIDs, uint numEvent, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result SetMedia(SourceSettings* sourceSettings, uint numSourceSettings);

        [DllImport(LibName)]
        public static extern Result UnsetMedia(SourceSettings* sourceSettings, uint numSourceSettings);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, char* groupName, char** gameSyncName, uint numGameSyncs);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, byte* groupName, byte** gameSyncName, uint numGameSyncs);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, uint groupID, uint* gameSyncID, uint numGameSyncs);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, char* groupName, char** gameSyncName, uint numGameSyncs, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, byte* groupName, byte** gameSyncName, uint numGameSyncs, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result PrepareGameSyncs(PreparationType preparationType, GroupType gameSyncType, uint groupID, uint* gameSyncID, uint numGameSyncs, IntPtr bankCallback, void* cookie);

        [DllImport(LibName)]
        public static extern Result SetListeners(GameObjectID emitterGameObj, GameObjectID* listenerGameObjs, uint numListeners);

        [DllImport(LibName)]
        public static extern Result SetDefaultListeners(GameObjectID* listenerGameObjs, uint numListeners);

        [DllImport(LibName)]
        public static extern Result ResetListenersToDefault(GameObjectID emitterGameObj);

        [DllImport(LibName)]
        public static extern Result SetListenerSpatialization(GameObjectID listenerID, bool spatialized, ChannelConfig channelConfig, float* volumeOffsets = null);

        [DllImport(LibName)]
        public static extern Result SetListenerPipeline(GameObjectID listenerID, bool audio, bool motion);

        [DllImport(LibName)]
        public static extern Result SetRTPCValue(RtpcID rtpcID, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetRTPCValue(char* rtpcName, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetRTPCValue(byte* rtpcName, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetRTPCValueByPlayingID(RtpcID rtpcID, RtpcValue value, PlayingID playingID, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetRTPCValueByPlayingID(char* rtpcName, RtpcValue value, PlayingID playingID, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetRTPCValueByPlayingID(byte* rtpcName, RtpcValue value, PlayingID playingID, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result ResetRTPCValue(RtpcID rtpcID, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result ResetRTPCValue(char* rtpcName, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result ResetRTPCValue(byte* rtpcName, RtpcValue value, GameObjectID gameObjectID = (ulong)-1, TimeMs valueChangeDuration = default(TimeMs), CurveInterpolation fadeCurve = CurveInterpolation.Linear, bool bypassInternalValueInterpolation = false);

        [DllImport(LibName)]
        public static extern Result SetSwitch(SwitchGroupID switchGroup, SwitchStateID switchState, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result SetSwitch(char* switchGroup, char* switchState, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result SetSwitch(byte* switchGroup, byte* switchState, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result PostTrigger(TriggerID triggerID, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result PostTrigger(char* trigger, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result PostTrigger(byte* trigger, GameObjectID gameObjectID);

        [DllImport(LibName)]
        public static extern Result SetState(StateGroupID stateGroup, StateID state);

        [DllImport(LibName)]
        public static extern Result SetState(char* stateGroup, char* state);

        [DllImport(LibName)]
        public static extern Result SetState(byte* stateGroup, byte* state);

        [DllImport(LibName)]
        public static extern Result SetGameObjectAuxSendValues(GameObjectID gameObjectID, AuxSendValue* auxSendValues, uint numSendValues);

        [DllImport(LibName)]
        public static extern Result RegisterBusVolumeCallback(UniqueID busID, IntPtr callback);

        [DllImport(LibName)]
        public static extern Result RegisterBusMeteringCallback(UniqueID busID, IntPtr callback, MeteringFlags meteringFlags);

        [DllImport(LibName)]
        public static extern Result SetGameObjectOutputBusVolume(GameObjectID emitterObjID, GameObjectID listenerObjID, float controlValue);

        [DllImport(LibName)]
        public static extern Result SetActorMixerEffect(UniqueID audioNodeID, uint fxIndex, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetBusEffect(UniqueID audioNodeID, uint fxIndex, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetBusEffect(char* busName, uint fxIndex, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetBusEffect(byte* busName, uint fxIndex, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetMixer(UniqueID audioNodeID, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetMixer(char* busName, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetMixer(byte* busName, UniqueID shareSetID);

        [DllImport(LibName)]
        public static extern Result SetBusConfig(UniqueID audioNodeID, ChannelConfig channelConfig);

        [DllImport(LibName)]
        public static extern Result SetBusConfig(char* busName, ChannelConfig channelConfig);

        [DllImport(LibName)]
        public static extern Result SetBusConfig(byte* busName, ChannelConfig channelConfig);

        [DllImport(LibName)]
        public static extern Result SetObjectObstructionAndOcclusion(GameObjectID emitterID, GameObjectID listenerID, float obstructionLevel, float occlusionLevel);

        [DllImport(LibName)]
        public static extern Result SetMultipleObstructionAndOcclusion(GameObjectID emitterID, GameObjectID listenerID, ObstructionOcclusionValues* obstructionOcclusionValues, uint numOcclusionObstruction);

        [DllImport(LibName)]
        public static extern Result GetContainerHistory(IntPtr bytes);

        [DllImport(LibName)]
        public static extern Result SetContainerHistory(IntPtr bytes);

        [DllImport(LibName)]
        public static extern Result StartOutputCapture(char* captureFileName);

        [DllImport(LibName)]
        public static extern Result StopOutputCapture();

        [DllImport(LibName)]
        public static extern Result AddOutputCaptureMarker(char* markerText);

        [DllImport(LibName)]
        public static extern Result StartProfilerCapture(char* captureFileName);

        [DllImport(LibName)]
        public static extern Result StopProfilerCapture();

        [DllImport(LibName)]
        public static extern Result AddSecondaryOutput(uint ouptutID, AudioOutputType deviceType, GameObjectID* listenerIDs, uint numListeners, uint outputFlags = 0, UniqueID audioDeviceShareSet = default(UniqueID));

        [DllImport(LibName)]
        public static extern Result RemoveSecondaryOutput(uint outputID, AudioOutputType deviceType);

        [DllImport(LibName)]
        public static extern Result SetSecondaryOutputVolume(uint outputID, AudioOutputType deviceType, float volume);

        [DllImport(LibName)]
        public static extern Result Suspend(bool renderAnyway = false);

        [DllImport(LibName)]
        public static extern Result WakeupFromSuspend();

        [DllImport(LibName)]
        public static extern Result GetBufferTick();
    }
}