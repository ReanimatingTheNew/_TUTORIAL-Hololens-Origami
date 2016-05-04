using UnityEngine;
using System.Collections;

public class SphereSounds : MonoBehaviour
{
    AudioSource audioSource = null;
    AudioClip impactClip = null;
    AudioClip rollingClip = null;
    bool rolling = false;

	// Use this for initialization
	void Start ()
    {
        //Add an audio source component and set up some defaults
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Custom;

        //Load the sphere sounds from the Resources Folder
        impactClip = Resources.Load<AudioClip>("Impact");
        rollingClip = Resources.Load<AudioClip>("Rolling");
	}
	
    //Occurs each frame that this object continues to collide with another object 
    void OnCollisionStay(Collision collision)
    {
        Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();

        //Play a rolling sound if the sphere is rolling fast enough
        if(!rolling && rigid.velocity.magnitude >= 0.0f)
        {
            rolling = true;
            audioSource.clip = rollingClip;
            audioSource.Play();
        }
        //Stop the rolling sound if rolling slows down
        else if(rolling && rigid.velocity.magnitude < 0.01f)
        {
            rolling = false;
            audioSource.Stop();
        }

    }

    //Occurs when this object collides with another object
    void OnCollisionExit(Collision collision)
    {
        //Stop the rolling sound if the object falls off and stops colliding
        if(rolling)
        {
            rolling = false;
            audioSource.Stop();
        }
    }

}
