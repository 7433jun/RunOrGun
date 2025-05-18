using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private GameObject currentModel;

    public void Initialize(WeaponDefinition weaponData)
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(weaponData.weaponPrefab, transform);
    }
}
