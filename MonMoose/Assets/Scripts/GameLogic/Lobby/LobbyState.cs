using MonMoose.Core;
using UnityEngine.SceneManagement;

public class LobbyState : State
{
    public override int stateIndex
    {
        get { return (int)EGameState.Lobby; }
    }

    public override void OnEnter()
    {
        SceneManager.LoadScene("LobbyScene");
        UIWindowManager.instance.OpenWindow((int)EWindowType.LobbyWindow);
    }

    public override void OnExit()
    {
        UIWindowManager.instance.DestroyAllWindow();
    }
}

