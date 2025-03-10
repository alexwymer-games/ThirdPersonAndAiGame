using UnityEngine;

public class ManagerParent : MonoBehaviour
{
    public static ManagerParent managerParentInstance;

    private void Awake()
    {
        if (managerParentInstance == null)
        {
            managerParentInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
