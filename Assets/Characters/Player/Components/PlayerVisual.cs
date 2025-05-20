using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private GameObject currentModel;

    public void Initialize(WeaponDefinition weaponDefinition)
    {
        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(weaponDefinition.weaponModel.prefab, transform);
        currentModel.transform.position = weaponDefinition.weaponModel.pos;
        currentModel.transform.rotation = Quaternion.Euler(weaponDefinition.weaponModel.rot);
        currentModel.transform.localScale = weaponDefinition.weaponModel.scale;
    }
}
