using System;

[AttributeUsage(AttributeTargets.Class)]
public class ScheduleClipAttribute : Attribute
{

}

[AttributeUsage(AttributeTargets.Field)]
public class ScheduleClipMemberAttribute : Attribute
{
    public string name;
    public MotionMemberType memberType = MotionMemberType.None;
    public ScheduleClipMemberAttribute(string InName, MotionMemberType InMemberType)
    {
        name = InName;
        memberType = InMemberType;
    }
}

public enum MotionMemberType
{
    None,
    Value,
    Action,
}
