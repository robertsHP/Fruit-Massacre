using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCount : MonoBehaviour {
    TMPro.TMP_Text text;
    int count;

    void Awake () {
        text = GetComponent<TMPro.TMP_Text>();
    }
    void Start () => UpdateCount();
    void OnEnable () => Collectable.OnCollected += OnCollectableCollected;
    void OnDisable () => Collectable.OnCollected -= OnCollectableCollected;

    void OnCollectableCollected () {
        count++;
        UpdateCount();
    }
    void UpdateCount () {
        text.text = $"{count} / {Collectable.total}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
