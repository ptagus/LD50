using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class TestAnim : MonoBehaviour
{
    void Start()
    {
        UnityFactory.factory.LoadDragonBonesData("NewProject_1_ske"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("NewProject_1_tex"); //Texture atlas file path (without suffix) 

        // Create armature.
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("SimpleGuy");
        // Input armature name

        // Play animation.
        armatureComponent.animation.Play("animtion0");

        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}

