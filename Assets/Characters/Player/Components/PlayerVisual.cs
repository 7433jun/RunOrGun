using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private GameObject currentModel;

    public void Initialize(WeaponDefinition weaponDefinition)
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(weaponDefinition.weaponData.prefab, transform);
        currentModel.transform.position = weaponDefinition.weaponData.pos;
        currentModel.transform.rotation = Quaternion.Euler(weaponDefinition.weaponData.rot);
        currentModel.transform.localScale = weaponDefinition.weaponData.scale;
    }
}
