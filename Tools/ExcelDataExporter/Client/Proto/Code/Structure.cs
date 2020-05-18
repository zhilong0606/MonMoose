// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Client/Proto/IL/Structure.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MonMoose.StaticData {

  /// <summary>Holder for reflection information generated from Client/Proto/IL/Structure.proto</summary>
  public static partial class StructureReflection {

    #region Descriptor
    /// <summary>File descriptor for Client/Proto/IL/Structure.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static StructureReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch9DbGllbnQvUHJvdG8vSUwvU3RydWN0dXJlLnByb3RvEhNNb25Nb29zZS5T",
            "dGF0aWNEYXRhIqgBCg9BY3RvclN0YXRpY0luZm8SCgoCSWQYASABKAUSDAoE",
            "TmFtZRgCIAEoCRIyCgRUeXBlGAMgASgOMiQuTW9uTW9vc2UuU3RhdGljRGF0",
            "YS5FQWN0b3JDbGFzc1R5cGUSEQoJTW92ZVNwZWVkGAQgASgFEg4KBkF0dGFj",
            "axgFIAEoBRIQCghEZWZmZW5jZRgGIAEoBRISCgpQcmVmYWJQYXRoGAcgASgJ",
            "IkkKE0FjdG9yU3RhdGljSW5mb0xpc3QSMgoEbGlzdBgBIAMoCzIkLk1vbk1v",
            "b3NlLlN0YXRpY0RhdGEuQWN0b3JTdGF0aWNJbmZvKnkKD0VBY3RvckNsYXNz",
            "VHlwZRIICgROb25lEAASCwoHV2FycmlvchABEg0KCVRlc3RUeXBlMhACEg0K",
            "CVRlc3RUeXBlMxADEg4KClRlc3QzVHlwZTEQBBIPCgtUZXMyMXRUeXBlMhAF",
            "EhAKDFRlc3RkZGRUeXBlMxAGYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::MonMoose.StaticData.EActorClassType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MonMoose.StaticData.ActorStaticInfo), global::MonMoose.StaticData.ActorStaticInfo.Parser, new[]{ "Id", "Name", "Type", "MoveSpeed", "Attack", "Deffence", "PrefabPath" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::MonMoose.StaticData.ActorStaticInfoList), global::MonMoose.StaticData.ActorStaticInfoList.Parser, new[]{ "List" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum EActorClassType {
    [pbr::OriginalName("None")] None = 0,
    [pbr::OriginalName("Warrior")] Warrior = 1,
    [pbr::OriginalName("TestType2")] TestType2 = 2,
    [pbr::OriginalName("TestType3")] TestType3 = 3,
    [pbr::OriginalName("Test3Type1")] Test3Type1 = 4,
    [pbr::OriginalName("Tes21tType2")] Tes21TType2 = 5,
    [pbr::OriginalName("TestdddType3")] TestdddType3 = 6,
  }

  #endregion

  #region Messages
  public sealed partial class ActorStaticInfo : pb::IMessage<ActorStaticInfo> {
    private static readonly pb::MessageParser<ActorStaticInfo> _parser = new pb::MessageParser<ActorStaticInfo>(() => new ActorStaticInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ActorStaticInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MonMoose.StaticData.StructureReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfo(ActorStaticInfo other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      type_ = other.type_;
      moveSpeed_ = other.moveSpeed_;
      attack_ = other.attack_;
      deffence_ = other.deffence_;
      prefabPath_ = other.prefabPath_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfo Clone() {
      return new ActorStaticInfo(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Type" field.</summary>
    public const int TypeFieldNumber = 3;
    private global::MonMoose.StaticData.EActorClassType type_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::MonMoose.StaticData.EActorClassType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "MoveSpeed" field.</summary>
    public const int MoveSpeedFieldNumber = 4;
    private int moveSpeed_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MoveSpeed {
      get { return moveSpeed_; }
      set {
        moveSpeed_ = value;
      }
    }

    /// <summary>Field number for the "Attack" field.</summary>
    public const int AttackFieldNumber = 5;
    private int attack_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Attack {
      get { return attack_; }
      set {
        attack_ = value;
      }
    }

    /// <summary>Field number for the "Deffence" field.</summary>
    public const int DeffenceFieldNumber = 6;
    private int deffence_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Deffence {
      get { return deffence_; }
      set {
        deffence_ = value;
      }
    }

    /// <summary>Field number for the "PrefabPath" field.</summary>
    public const int PrefabPathFieldNumber = 7;
    private string prefabPath_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PrefabPath {
      get { return prefabPath_; }
      set {
        prefabPath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ActorStaticInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ActorStaticInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Type != other.Type) return false;
      if (MoveSpeed != other.MoveSpeed) return false;
      if (Attack != other.Attack) return false;
      if (Deffence != other.Deffence) return false;
      if (PrefabPath != other.PrefabPath) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Type != 0) hash ^= Type.GetHashCode();
      if (MoveSpeed != 0) hash ^= MoveSpeed.GetHashCode();
      if (Attack != 0) hash ^= Attack.GetHashCode();
      if (Deffence != 0) hash ^= Deffence.GetHashCode();
      if (PrefabPath.Length != 0) hash ^= PrefabPath.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Type != 0) {
        output.WriteRawTag(24);
        output.WriteEnum((int) Type);
      }
      if (MoveSpeed != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(MoveSpeed);
      }
      if (Attack != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(Attack);
      }
      if (Deffence != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Deffence);
      }
      if (PrefabPath.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(PrefabPath);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (MoveSpeed != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MoveSpeed);
      }
      if (Attack != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Attack);
      }
      if (Deffence != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Deffence);
      }
      if (PrefabPath.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PrefabPath);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ActorStaticInfo other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.MoveSpeed != 0) {
        MoveSpeed = other.MoveSpeed;
      }
      if (other.Attack != 0) {
        Attack = other.Attack;
      }
      if (other.Deffence != 0) {
        Deffence = other.Deffence;
      }
      if (other.PrefabPath.Length != 0) {
        PrefabPath = other.PrefabPath;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            type_ = (global::MonMoose.StaticData.EActorClassType) input.ReadEnum();
            break;
          }
          case 32: {
            MoveSpeed = input.ReadInt32();
            break;
          }
          case 40: {
            Attack = input.ReadInt32();
            break;
          }
          case 48: {
            Deffence = input.ReadInt32();
            break;
          }
          case 58: {
            PrefabPath = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ActorStaticInfoList : pb::IMessage<ActorStaticInfoList> {
    private static readonly pb::MessageParser<ActorStaticInfoList> _parser = new pb::MessageParser<ActorStaticInfoList>(() => new ActorStaticInfoList());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ActorStaticInfoList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MonMoose.StaticData.StructureReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfoList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfoList(ActorStaticInfoList other) : this() {
      list_ = other.list_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ActorStaticInfoList Clone() {
      return new ActorStaticInfoList(this);
    }

    /// <summary>Field number for the "list" field.</summary>
    public const int ListFieldNumber = 1;
    private static readonly pb::FieldCodec<global::MonMoose.StaticData.ActorStaticInfo> _repeated_list_codec
        = pb::FieldCodec.ForMessage(10, global::MonMoose.StaticData.ActorStaticInfo.Parser);
    private readonly pbc::RepeatedField<global::MonMoose.StaticData.ActorStaticInfo> list_ = new pbc::RepeatedField<global::MonMoose.StaticData.ActorStaticInfo>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::MonMoose.StaticData.ActorStaticInfo> List {
      get { return list_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ActorStaticInfoList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ActorStaticInfoList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!list_.Equals(other.list_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= list_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      list_.WriteTo(output, _repeated_list_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += list_.CalculateSize(_repeated_list_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ActorStaticInfoList other) {
      if (other == null) {
        return;
      }
      list_.Add(other.list_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            list_.AddEntriesFrom(input, _repeated_list_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
