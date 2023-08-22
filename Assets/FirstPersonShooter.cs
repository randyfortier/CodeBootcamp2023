using UnityEngine;

public class FirstPersonShooter : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private float range = 100f;
    [SerializeField] private GameObject bulletHolePrefab;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        // TODO Shooting sfx

        // TODO play gun anim

        // TODO spawn smoke paritcles

        // TODO spawn laser line

        // trace out the shot and see if we hit it
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, wallLayers)) {
            // Debug.Log("Hit this wall " + hit.collider.name);

            Instantiate(bulletHolePrefab, hit.point + (0.01f * hit.normal), Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
        }else if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, range, enemyLayers)) {
            Health health = hit.collider.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(35);
            }
        }




    }
}
