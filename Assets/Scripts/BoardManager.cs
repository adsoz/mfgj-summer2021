using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

	private int rows = 16;
	private int columns = 20;
	public Player player;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] blockTiles;
	public GameObject spikes;
	public GameObject startTile;
	public GameObject exitTile;
	private Vector3 spawnPoint;

	public int[,] levelOneSetup = new int[,] {{ 1,1,1,1,1,1,1,1,1,8,1,1,1,1,1,1,1,1,1,1 },
										 	  { 1,1,1,1,1,1,1,1,1,2,0,1,1,1,1,1,1,1,1,1 },
										 	  { 1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1 },
										 	  { 1,2,0,0,0,0,1,1,1,0,1,1,1,1,1,1,1,1,1,1 },
										 	  { 1,0,0,0,2,0,1,0,0,2,1,1,1,0,0,0,0,0,1,1 },
										 	  { 1,0,0,0,0,2,1,0,1,0,0,0,1,0,0,2,0,0,1,1 },
										 	  { 1,0,0,0,0,0,1,2,0,2,0,0,0,2,0,0,0,0,1,1 },
										 	  { 1,0,2,0,0,3,1,0,1,1,1,1,1,1,1,1,0,1,1,1 },
										 	  { 1,0,0,2,0,0,0,0,0,0,0,0,0,0,0,2,0,1,1,1 },
										 	  { 1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,3,2,0,0,1 },
										 	  { 1,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,1 },
										 	  { 1,0,0,2,0,3,0,0,0,0,0,1,1,1,2,0,0,0,0,1 },
										 	  { 1,0,2,0,0,1,1,1,1,1,0,1,1,1,2,0,0,0,0,1 },
										 	  { 1,1,1,1,1,1,1,1,1,1,0,1,1,1,0,0,0,0,0,1 },
										 	  { 1,1,1,1,1,1,1,1,1,1,9,1,1,1,1,1,1,1,1,1 },
										 	  { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 } };

	public int[,] levelTwoSetup = new int[,] {{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
										 	  { 1,0,0,0,0,0,1,1,0,0,0,0,2,0,1,1,1,1,1,1 },
										 	  { 1,0,2,0,8,0,1,1,0,1,0,0,0,0,1,1,1,1,1,1 },
										 	  { 1,0,0,0,2,2,0,2,0,1,0,0,2,2,1,1,1,1,1,1 },
										 	  { 1,1,1,0,0,2,0,0,2,0,2,0,0,0,0,0,1,1,1,1 },
										 	  { 1,1,1,1,2,1,0,2,0,1,1,0,0,2,2,0,1,1,1,1 },
										 	  { 1,0,2,0,0,0,0,2,0,0,2,2,1,0,0,0,0,0,0,1 },
										 	  { 1,0,1,1,1,1,1,0,1,0,0,0,1,2,0,0,2,2,0,1 },
										 	  { 1,2,0,0,0,0,0,2,0,1,2,2,1,0,0,2,0,0,0,1 },
										 	  { 1,0,0,2,0,0,0,2,0,1,0,1,1,0,2,0,0,0,0,1 },
										 	  { 1,0,2,0,0,0,0,0,0,0,0,2,0,2,0,1,1,1,1,1 },
										 	  { 1,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1 },
										 	  { 1,0,0,0,0,0,0,0,2,0,0,2,1,1,2,1,1,1,1,1 },
										 	  { 1,1,1,1,1,1,0,0,2,0,0,0,1,1,0,0,0,0,1,1 },
										 	  { 1,1,1,1,1,1,1,0,0,0,2,0,0,0,0,9,2,0,1,1 },
										 	  { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 } };

	private Transform boardHolder;

	void BoardSetup() {
		boardHolder = new GameObject("Board").transform;
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
				// if (x == -1 || x == columns || y == -1 || y == rows) {
				// 	toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.length)];
				// }

				GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	void LevelSetup(int level) {
		int[,] levelGrid = levelOneSetup;

		if (level == 1) {
			levelGrid = levelOneSetup;
		} else if (level == 2) {
			levelGrid = levelTwoSetup;
		}

		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {

				GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
				
				if (levelGrid[y,x] == 2) {
					toInstantiate = blockTiles[Random.Range(0, blockTiles.Length)];

				} else if (levelGrid[y,x] == 3) {
					toInstantiate = spikes;

				} else if (levelGrid[y,x] == 8) {
					toInstantiate = startTile;
					spawnPoint = new Vector3 (x,rows-y-1,0f);
					

				} else if (levelGrid[y,x] == 9) {
					toInstantiate = exitTile;
				}

				if (levelGrid[y,x] > 0) {
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x,rows-y-1,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}

			}
		}

        // 

	}

	public void SetupScene (int level) {
		BoardSetup();
		LevelSetup(level);
		
		player = FindObjectOfType<Player>();
		// GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
		
		player.transform.position = spawnPoint;

	}

}
