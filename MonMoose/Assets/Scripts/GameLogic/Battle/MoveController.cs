using MonMoose.Core;
using UnityEngine;

public class MoveController
{
    public void Init()
    {
        RegisterListener();
    }

    private void RegisterListener()
    {
        JoystickManager.instance.RegisterActionDragUpdate((int)EJoystickType.Move, OnJoystickDragUpdate);
        JoystickManager.instance.RegisterActionValidChanged((int)EJoystickType.Move, OnJoystickValidChanged);
        //KeyInputManager.instance.RegisterListener(new[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }, EKeyState.Down, OnKeyChange);
        //KeyInputManager.instance.RegisterListener(new[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }, EKeyState.Up, OnKeyChange);
    }

    private void RemoveListener()
    {
        JoystickManager.instance.UnregisterActionDragUpdate((int)EJoystickType.Move, OnJoystickDragUpdate);
        JoystickManager.instance.UnregisterActionValidChanged((int)EJoystickType.Move, OnJoystickValidChanged);
        //KeyInputManager.instance.RegisterListener(new[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }, EKeyState.Down, OnKeyChange);
        //KeyInputManager.instance.RegisterListener(new[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }, EKeyState.Up, OnKeyChange);
    }

    private void OnJoystickValidChanged(JoystickEvent e)
    {
        if (e.isValid)
        {
        }
        else
        {
            StopMoveCommand command = ClassPoolManager.instance.Fetch<StopMoveCommand>();
            command.playerID = PlayerManager.instance.HostPlayer.playerID;
            FrameSyncManager.instance.Send(command);
        }
    }

    private void OnJoystickDragUpdate(JoystickEvent e)
    {
        Vector3 pos = new Vector3(e.normal.x, 0, e.normal.y);
        Vector3 dir = Camera.main.transform.TransformDirection(pos);
        dir.y = 0;
        MoveDirectionCommand command = ClassPoolManager.instance.Fetch<MoveDirectionCommand>();
        command.playerID = PlayerManager.instance.HostPlayer.playerID;
        command.direction = dir;
        FrameSyncManager.instance.Send(command);
    }

    //private void OnKeyChange()
    //{
    //    Vector3 pos = Vector3.zero;
    //    bool isKeyDown = false;
    //    if (KeyInputManager.instance.IsKeyDown(KeyCode.W))
    //    {
    //        pos += new Vector3(0, 0, 1);
    //        isKeyDown = true;
    //    }
    //    if (KeyInputManager.instance.IsKeyDown(KeyCode.S))
    //    {
    //        pos += new Vector3(0, 0, -1);
    //        isKeyDown = true;
    //    }
    //    if (KeyInputManager.instance.IsKeyDown(KeyCode.A))
    //    {
    //        pos += new Vector3(-1, 0, 0);
    //        isKeyDown = true;
    //    }
    //    if (KeyInputManager.instance.IsKeyDown(KeyCode.D))
    //    {
    //        pos += new Vector3(1, 0, 0);
    //        isKeyDown = true;
    //    }
    //    if (isKeyDown && pos != Vector3.zero)
    //    {
    //        Vector3 dir = Camera.main.transform.TransformDirection(pos).normalized;
    //        dir.y = 0;
    //        MoveDirectionCommand command = ClassPool<MoveDirectionCommand>.Get();
    //        command.playerID = PlayerManager.instance.HostPlayer.playerID;
    //        command.direction = dir.ToIntVector3();
    //        FrameSyncManager.instance.Send(command);
    //    }
    //    else
    //    {
    //        StopMoveCommand command = ClassPool<StopMoveCommand>.Get();
    //        command.playerID = PlayerManager.instance.HostPlayer.playerID;
    //        FrameSyncManager.instance.Send(command);
    //    }
    //}

    public void Clear()
    {
        RemoveListener();
    }
}
