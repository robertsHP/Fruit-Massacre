using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewBounds : MonoBehaviour {
    private Collider col;

    void Start() {
        col = GetComponent<Collider>();
    }
    public bool InBounds (Vector3 position) {
        return col.bounds.Contains(position);
    }
}
