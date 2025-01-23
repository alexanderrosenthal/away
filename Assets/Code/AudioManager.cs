using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sourceA; // Erste Audioquelle
    private AudioSource sourceB; // Zweite Audioquelle
    public float crossfadeDuration = 1.0f; // Dauer des Crossfades

    public void CrossfadeTo(AudioSource newSourceA, AudioSource newSourceB)
    {
        sourceA = newSourceA;
        sourceB = newSourceB;
        sourceB.volume = 0;
        sourceB.Play();
        StartCoroutine(CrossfadeRoutine());
    }

    private IEnumerator CrossfadeRoutine()
    {
        float timer = 0f;

        // Lautstärke von der aktiven Quelle (z.B. sourceA) reduzieren
        while (timer < crossfadeDuration)
        {
            //Wenn SourceA noch gar nicht läuft muss auch nichts runtergefahren werden.
            if (sourceA.isPlaying)
            {
                sourceA.volume = Mathf.Lerp(1, 0, timer / crossfadeDuration);
            }
            sourceB.volume = Mathf.Lerp(0, 1, timer / crossfadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Den Wechsel komplettieren
        sourceA.Stop();
        sourceA.volume = 0;

        sourceB.volume = 1;
    }
}
