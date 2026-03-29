# Unity Advanced Audio System

A performant, modular audio management system for Unity. Built using **ScriptableObjects** for audio data configuration and the modern **UnityEngine.Pool** API to completely eliminate memory spikes and garbage collection drops when playing rapid sound effects.

---

## ✨ Features

* **ScriptableObject Configurations:** Treat your sound effects as data assets. Adjust volume, default pitch, and looping behaviors directly in the Editor without touching code.
* **Built-in Pitch Randomization:** Easily toggle pitch variance on sounds like footsteps or gunfire to prevent auditory fatigue and repetition.
* **Modern Object Pooling:** Utilizes Unity's native "UnityEngine.Pool.ObjectPool" to recycle "AudioSource" components, dramatically improving performance in busy scenes.
* **2D & 3D Audio Support:** Pass an optional "Vector3" position to the manager to automatically play spatial 3D audio.
* **Singleton Manager:** Accessible globally via "AudioManager.Instance", allowing any script to trigger sounds instantly.

---

## 🧠 Design Notes

Most beginner audio systems attach an "AudioSource" to every single bullet or enemy in the game, which is incredibly inefficient. 

This system acts as a central hub. When a script requests a sound effect, the "AudioManager" grabs an inactive "AudioSource" from its internal pool, assigns the "AudioData" properties to it, plays the sound, and then automatically recycles the source back into the pool when the clip finishes. The data-driven approach means audio designers can tweak the mix of the game without needing a programmer.

---

## 📂 Included Scripts

* "AudioData.cs" - The ScriptableObject blueprint that holds the AudioClip and its specific playback rules (volume, pitch variance, looping).
* "AudioManager.cs" - The persistent Singleton that handles music playback, manages the SFX Object Pool, and handles 3D spatial positioning.
* "AudioTrigger.cs" - A simple helper script you can attach to objects (like buttons or map hazards) to easily test and trigger sounds.

---

## 🧩 How To Use

1. **Create Audio Data:** Right-click in your Project window: "Create > Audio System > Audio Data". Assign your ".wav" or ".mp3" file and adjust the volume/pitch.
2. **Setup the Manager:** Create an empty GameObject in your first scene, name it "AudioManager", and attach the "AudioManager.cs" script. Give it a dedicated "AudioSource" component for the Music track.
3. **Playing Music:** Call this from any script to change the background track:
"
AudioManager.Instance.PlayMusic(myMusicDataAsset);
"
4. **Playing 2D Sound Effects:** Perfect for UI clicks or global sounds:
"
AudioManager.Instance.PlaySFX(myUiClickAsset);
"
5. **Playing 3D Sound Effects:** Pass the position of the object making the noise (like an explosion):
"
AudioManager.Instance.PlaySFX(myExplosionAsset, transform.position);
"

---

## 🚀 Possible Extensions

* **Music Crossfading:** Upgrade the "PlayMusic" method with a Coroutine that slowly turns down the volume of the old track while turning up the new one.
* **Audio Mixers:** Add a field for "UnityEngine.Audio.AudioMixerGroup" inside the "AudioData" script to route sounds to specific Master/SFX/Music buses.
* **Sound Playlists:** Create an "AudioPlaylist" ScriptableObject that holds an array of "AudioData" and randomly selects one to play (great for varied footstep types).

---

## 🛠 Unity Version

Tested in Unity6+ (should work without any problems in newer versions)

---

## 📜 License

MIT
