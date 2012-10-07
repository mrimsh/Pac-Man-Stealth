using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class LightEffects : MonoBehaviour
{
	public float effectsDuration = 2f;
	public float blinkDuration = 0.3f;
	public List<LightBlink> blinks;
	private int currentEffect;
	
	private enum LightEffect
	{
		none = 0,
		brightBlink = 1,
		darkBlink = 2,
		redBlink = 3
	}
	
	private float lastEffectChangeTime;
	private float lastBlinkTime;
	private bool isBlinkOn;
	private int summaryChance;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < blinks.Count; i++) {
			summaryChance += blinks [i].chance;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > lastEffectChangeTime + blinks [currentEffect].effectDuration) {
			int chance = Random.Range (0, summaryChance + 1);
			int lastMinimumChance = 0;
			for (int i = 0; i < blinks.Count; i++) {
				if ((chance > lastMinimumChance) && (chance < (lastMinimumChance + blinks [i].chance))) {
					currentEffect = i;
					isBlinkOn = false;
					break;
				}
				lastMinimumChance += blinks [i].chance;
			}
			lastEffectChangeTime = Time.time;
		} else {
			if (Time.time > lastBlinkTime + blinks [currentEffect].blinkDuration) {
				if (isBlinkOn) {
					light.intensity = blinks [currentEffect].lightIntensity;
					light.color = blinks [currentEffect].lightColor;
				} else {
					light.intensity = 0.5f;
					light.color = Color.gray;
				}
				isBlinkOn = !isBlinkOn;
				lastBlinkTime = Time.time;
			}
		}
	}
}

[System.Serializable]
public class LightBlink
{
	[XmlAttribute("name")]
	public string name = "New Effect";
	public float effectDuration = 2f;
	public float blinkDuration = 0.3f;
	public int chance = 1;
	public Color lightColor = Color.gray;
	public float lightIntensity = 0.5f;
}