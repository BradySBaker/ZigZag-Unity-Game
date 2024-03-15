using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public GameObject block;
    public GameObject fruit;

    private Vector3 prevBlockPos;
    int chunkCount = 6; //Should be even number
    int blockPerChunk = 3;
    int generateDistance;
    int blockCount = 0;
    public float fruitChance = .1f;

    private void Start() {
        generateDistance = ((chunkCount * blockPerChunk)/2);
        SpawnBlocks(blockPerChunk, true);
    }

    void SpawnFruit(Vector3 blockPos) {
        float xOffset = Random.Range(-.5f, .5f);
        Vector3 offset = new Vector3(xOffset, 0f, 0f);
        GameObject newFruit = Instantiate(fruit, blockPos + transform.up/2 + offset, Quaternion.identity);
    }

    void SpawnBlocks(int amount, bool first = false) {
        if (prevBlockPos == null) {
            prevBlockPos = new Vector3(14.72f, 7.49f, -1.77f);
        }
        for (int l = 1; l <= chunkCount; l++) {
            bool forward = Random.Range(0, 2) == 0 ? true : false;
            if (first && l == 1) {
                forward = true;
            }
            Vector3 curBlockPos = prevBlockPos;
            for (int i = 1; i <= amount; i++) {
                blockCount++;
                GameObject newBlock = Instantiate(block, prevBlockPos, Quaternion.Euler(0, 45, 0));
                if (forward) {
                    newBlock.transform.Translate(Vector3.forward * i);
                } else {
                    newBlock.transform.Translate(Vector3.left * i);
                }


                curBlockPos = newBlock.transform.position;
                newBlock.name = blockCount.ToString();

                if (Random.Range(0, (int)(1 / fruitChance)) == 0) {
                    SpawnFruit(curBlockPos);
                }
            }
            prevBlockPos = curBlockPos;
        }
    }

    public void CheckBlockAndHandleSpawnAndDestroy(string blockName) {
        Debug.Log(blockName + " Count: " + blockCount);
        if ((blockCount - generateDistance).ToString() == blockName) {
            SpawnBlocks(blockPerChunk);
        }
        GameObject lastBlock = GameObject.Find((int.Parse(blockName) - generateDistance).ToString());
        if (lastBlock) {
            Destroy(lastBlock);
        }
    }
}
