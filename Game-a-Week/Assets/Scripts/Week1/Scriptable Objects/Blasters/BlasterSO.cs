using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBlaster", menuName = "SpaceshipRachet/SpacePlayerBlaster")]
public class BlasterSO : ScriptableObject
{
    public GameObject[] fireSpots; //And and all fire spots

    public GameObject bulletPrefab; //Prefab of this blaster projectile

    public float fireDelay; //Delay for this form of blaster
}
