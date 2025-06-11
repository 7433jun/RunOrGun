using UnityEngine;

public class PlayContext : MonoBehaviour
{
    public CharacterRegistry CharacterRegistry { get; private set; }

    public PlayEvent playEvent;

    public void Initialize()
    {
        CharacterRegistry = new CharacterRegistry();
        playEvent = new PlayEvent();
    }
}
