using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.collider is CapsuleCollider) {
            SceneManager.LoadScene(0);
        }


    }

}
