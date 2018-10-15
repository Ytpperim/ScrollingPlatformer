// *******************************************************
// Copyright 2013 Daikon Forge, all rights reserved under 
// US Copyright Law and international treaties
// 
// 2016 Updated by Ryan Leonard to comply with Unity 5.3.4f1
// *******************************************************
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class AutoSaveOnPlay {
	static public bool silent = true;
	static AutoSaveOnPlay() {
		EditorApplication.playmodeStateChanged = () => {
			if( EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying ) {
				Debug.Log("Auto-Saving open scenes before entering Play mode" );
				if (silent) {
					UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
				} else {
					UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
				}
				Debug.Log("Auto-Saving assets before entering Play mode" );
				AssetDatabase.SaveAssets();
			}
		};
	}
}