using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GlobalScore : NetworkBehaviour
{
    public TextMeshProUGUI scoreText;
    public NetworkVariable<int> score = new(0);
    void Update()
    {
        scoreText.text = "Global score : " + score.Value;
    }

    [Rpc(SendTo.Server)]
    public void IncrementScoreRpc()
    {
        Debug.Log("Score incrementation");
        score.Value += 1;
    }
}
