using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    [SerializeField] GameObject swordHitBox;
    public void EnableSwordHitBox()
    {
        swordHitBox.SetActive(true);
    }

    public void DisableHitBox()
    {
        swordHitBox.SetActive(false);
    }
}
