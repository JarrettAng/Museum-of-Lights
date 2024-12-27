using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Terrain t;
    public int posX;
    public int posZ;
    public float[] textureValues;

    [Header("Volume")]
    [SerializeField]
    public float SnowFootstepVol; // 0.4f
    [SerializeField]
    public float GravelFootstepVol; // 4.0f
    [SerializeField]
    public float DirtFootstepVol; // 0.7f
    [SerializeField]
    public float WoodFootstepVol; // 1.0f
    [SerializeField]
    public float PresentFootstepVol; // 2.0f

    [Header ("Footsteps")]
    public List<AudioClip> dirtFS;
    public List<AudioClip> gravelFS;
    public List<AudioClip> snowFS;
    public List<AudioClip> darkStoneFS;
    public List<AudioClip> cliffFS;
    public List<AudioClip> woodFS;
    public List<AudioClip> presentFS;

    [Header("SFX")]
    public AudioClip solvedPuzzle;
    public AudioClip lockedIn;

    public List<AudioSource> source;
    AudioClip previousClip;

    enum FSSurface
    {
        Empty, // Terrain
        Wood,
        Present
    }

    enum AudioPlayers
    {
        SFX,
        Footsteps,
    }

    // Checks the surface where the player is standing on
    private FSSurface CheckSurface()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerTransform.position + Vector3.up * 0.5f, -Vector3.up);

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider == null)
                return FSSurface.Empty;

            if (hit.collider.CompareTag("Wood"))
                return FSSurface.Wood;
            else if (hit.collider.CompareTag("Present"))
                return FSSurface.Present;
        }

        return FSSurface.Empty;
    }

    void Start()
    {
        t = Terrain.activeTerrain;
        playerTransform = transform;
        textureValues = new float[5];
        Debug.Log(source.Count);
    }
    public void GetTerrainTexture()
    {
        ConvertPosition(playerTransform.position);
        CheckTexture();
    }
    
    // This function converts the player position into a position on the 
    // alpha map, setting the relative X and Z positions
    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - t.transform.position;
        Vector3 mapPosition = new Vector3(terrainPosition.x / t.terrainData.size.x, 
                                          0,
                                          terrainPosition.z / t.terrainData.size.z);

        float xCoord = mapPosition.x * t.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * t.terrainData.alphamapHeight;
        posX = (int)xCoord;
        posZ = (int)zCoord;
    }

    // Using the X and Z positions, we can use a sample of the area to find 
    // the different texture values of that sample, eg. mix of sand and dirt
    // we can get the percentages of each and determine how much of it is 
    // in that area
    void CheckTexture()
    {
        float[,,] aMap = t.terrainData.GetAlphamaps(posX, posZ, 1, 1);

        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        textureValues[2] = aMap[0, 0, 2];
        textureValues[3] = aMap[0, 0, 3];
        textureValues[4] = aMap[0, 0, 4];
    }
    
    // Depending on the surface, it will play the appropriate sound
    public void PlayFootstep()
    {
        if (source[(int)AudioPlayers.Footsteps].isPlaying)
            return;

        FSSurface surface = CheckSurface();
        source[(int)AudioPlayers.Footsteps].pitch = Random.Range(0.8f, 1.2f);

        switch(surface)
        {
            case FSSurface.Wood:
                source[(int)AudioPlayers.Footsteps].PlayOneShot(GetClip(woodFS), WoodFootstepVol);
                break;
            case FSSurface.Present:
                source[(int)AudioPlayers.Footsteps].PlayOneShot(GetClip(presentFS), PresentFootstepVol);
                break;
            // Terrain 
            case FSSurface.Empty:
                source[(int)AudioPlayers.Footsteps].clip = null;

                // If a certain texture has a value, it means that it is being stepped on
                // and the sound will be scaled based on the amount that is present, allowing
                // a blend of footstep sounds
                GetTerrainTexture();
                if (textureValues[0] > 0)
                {
                    source[(int)AudioPlayers.Footsteps].PlayOneShot(GetClip(dirtFS), textureValues[0] * DirtFootstepVol);
                }
                if (textureValues[1] > 0)
                {
                    source[(int)AudioPlayers.Footsteps].PlayOneShot(GetClip(gravelFS), textureValues[1] * GravelFootstepVol);
                }
                if (textureValues[2] > 0)
                {
                    source[(int)AudioPlayers.Footsteps].PlayOneShot(GetClip(snowFS), textureValues[2] * SnowFootstepVol);
                }
            break;
        }

    }
    AudioClip GetClip(List<AudioClip> clipArray)
    {
        int attempts = 3;
        AudioClip selectedClip =
        clipArray[Random.Range(0, clipArray.Count - 1)];

        // Semi prevents the same clip from being selected each time
        while (selectedClip == previousClip && attempts > 0)
        {
            selectedClip = clipArray[Random.Range(0, clipArray.Count - 1)];

            --attempts;
        }
        previousClip = selectedClip;
        return selectedClip;
    }
    public void PlaySolved()
    {
        source[(int)AudioPlayers.SFX].PlayOneShot(solvedPuzzle, 1.0f);
    }
    public void PlayLockedIn()
    {
        source[(int)AudioPlayers.SFX].PlayOneShot(lockedIn, 1.0f);
    }


}