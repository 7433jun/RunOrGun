using UnityEngine;

// �÷��̾� �� ��ġ�ϴ� ������Ʈ
public class PlayerVisual : MonoBehaviour
{
    private GameObject currentModel;

    public void Initialize()
    {
        if (currentModel != null)
            Destroy(currentModel);
        
        //currentModel = Instantiate(resource.PlayerModel.Prefab, transform);
        //currentModel.transform.position = resource.PlayerModel.Pos;
        //currentModel.transform.rotation = Quaternion.Euler(resource.PlayerModel.Rot);
        //currentModel.transform.localScale = resource.PlayerModel.Scale;
    }
}
