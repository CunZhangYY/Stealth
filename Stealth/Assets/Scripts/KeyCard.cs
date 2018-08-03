using UnityEngine;
using System.Collections;

public class KeyCard : MonoBehaviour {
    public AudioClip musicKey;
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player)
        {
            other.GetComponent<Player>().hasKey = true;//获得钥匙
            AudioSource.PlayClipAtPoint(musicKey, transform.position);
           
            Destroy(this.gameObject);
        }
    }
}
