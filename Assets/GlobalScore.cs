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
        score.Value += 1;
    }

    [Rpc(SendTo.Server)]
    public void DecrementScoreRpc()
    {
        if (score.Value > 0)
        {
            score.Value -= 1;
        }
    }
}
