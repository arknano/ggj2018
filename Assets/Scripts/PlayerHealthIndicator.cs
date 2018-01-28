using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthIndicator : MonoBehaviour {

    public GameObject[] HealthParts;
    public GameObject HealthCenter;
    public float MaxRotationSpeed;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
	void Update() {
        if (playerHealth.isDead)
        {
            foreach (GameObject part in HealthParts)
            {
                part.SetActive(false);
            }
            if (HealthCenter != null) {
                HealthCenter.SetActive(false);
            }
            return;
        }

        float hp = playerHealth.PlayerHP.Value;
        for (int i = 0; i < HealthParts.Length; i++)
        {
            float speed;
            float hpThreshold = i * 25;
            if (hp < hpThreshold)
            {
                speed = 0;
            }
            else
            {
                speed = (hp - hpThreshold) / 25 * MaxRotationSpeed;
            }

            // Slow down spinning
            GameObject healthPart = HealthParts[i];
            RectTransform rectTransform = healthPart.GetComponent<RectTransform>();
            Quaternion rotate = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.forward);
            rectTransform.localRotation *= rotate;

            // Fade alpha
            float alpha = speed / MaxRotationSpeed;
            Image image = healthPart.GetComponent<Image>();
            Color color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            image.color = color;
        }
	}
}
