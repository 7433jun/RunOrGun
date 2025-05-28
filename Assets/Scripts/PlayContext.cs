using UnityEngine;

public class PlayContext : MonoBehaviour
{
    public CharacterRegistry CharacterRegistry { get; private set; }

    public void Initialize()
    {
        CharacterRegistry = new CharacterRegistry();
    }
}
