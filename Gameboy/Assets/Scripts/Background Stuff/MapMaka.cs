using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapMaka : MonoBehaviour {
    public Tilemap floor;
    public Tilemap foliage;
    public Tilemap shadows;
    public Tilemap water;
    public Tile[] floor_tiles;
    public Tile[] wall_tiles;
    public Tile[] ceiling_tiles;
    public Tile[] curb_tiles;
    public GameObject[] obstacles;

    public int width = 100;
    public int length = 100;
    public int height = 2;
    int[,] map;
    int[,] walls;
    private float percent_wall;


    public void Start() {
        percent_wall = .5f;
        map = new int[width, length];
        walls = new int[width, length];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                map[x, y] = Random.value < percent_wall ? 1 : 0;
                walls[x, y] = 0;
                if (x == 100 || y == 100)
                    map[x, y] = 3;
            }
        }
        SmoothMap();
        SmoothMap();
        configureWalls();
        MarchingBoxes();
        AddDetails();
        DrawMap();
    }


    void DrawMap() {
        int tileID;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                switch (map[i, j]) {
                    case 0: // Floor
                        tileID = (int)Random.Range(0, floor_tiles.Length);
                        floor.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), floor_tiles[tileID]);
                        break;
                    case 1: // Wall
                        if (getHeightBelow(i, j) < height) {
                            tileID = (int)Random.Range(0, wall_tiles.Length);
                            floor.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), wall_tiles[tileID]);
                        }
                        else {
                            tileID = walls[i, j];
                            floor.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), ceiling_tiles[tileID]);
                        }
                        break;
                    case 2: // Wall - Floor transition
                        tileID = (int)Random.Range(0, wall_tiles.Length);
                        floor.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), wall_tiles[tileID]);
                        tileID = (int)Random.Range(0, curb_tiles.Length);
                        shadows.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), curb_tiles[tileID]);
                        break;
                    case 3: // Top of wall
                        tileID = walls[i, j];
                        floor.SetTile(new Vector3Int(i - width / 2, j - length / 2, 0), ceiling_tiles[tileID]);
                        break;
                }
            }
        }
    }
    void SmoothMap() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                int numwalls = GetAdjacentWallCount(x, y);
                if (numwalls > 4)
                    map[x, y] = 1;
                else if (numwalls < 4)
                    map[x, y] = 0;
            }
        }
    }

    int GetAdjacentWallCount(int xcoor, int ycoor) {
        int count = 0;
        for (int x = xcoor - 1; x <= xcoor + 1; x++) {
            for (int y = ycoor - 1; y <= ycoor + 1; y++) {
                if (x >= 0 && x < width && y > 0 && y < length) {
                    if (x != xcoor || y != ycoor) {
                        count += (map[x, y] > .5 ? 1 : 0);
                    }
                }
                else {
                    count++;
                }
            }
        }
        return count;
    }

    //Makes all walls at least 3 blocks high
    void configureWalls() { 
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                if (map[i, j] >= 1)
                    configure(i, j);
            }
        }
    }

    void configure(int xcoor, int ycoor) {
        int wallheight = getHeightAbove(xcoor, ycoor) + getHeightBelow(xcoor, ycoor);

        if (wallheight < height && ycoor + 1 < length && ycoor > 1) {
            map[xcoor, ycoor + 1] = 1;
            map[xcoor, ycoor - 1] = 1;
        }
    }

    int getHeightAbove(int xcoor, int ycoor) {
        int count = 0;
        while (ycoor + 1 < length && map[xcoor, ycoor+1] >= 1) {
            ycoor++;
            count++;
        }
        return count;
    }

    int getHeightBelow(int xcoor, int ycoor) {
        int count = 0;
        while (ycoor - 1 > 0 && map[xcoor, ycoor-1] >= 1) {
            ycoor--;
            count++;
        }
        return count;
    }

    void MarchingBoxes() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                if (getHeightBelow(i, j) >= height && i < width - 1 && i > 0 && j < length - 1 && j > 0) {
                    int ID = 0;
                    ID += (map[i, j + 1] == 1) ? 1 : 0;
                    ID += (getHeightBelow(i + 1, j) >= height && (map[i + 1, j] == 1)) ? 2 : 0;
                    ID += (getHeightBelow(i - 1, j) >= height && (map[i - 1, j] == 1)) ? 4 : 0;
                    ID += (map[i, j - 3] == 1) ? 8 : 0;
                    walls[i, j] = ID;
                }
            }
        }
    }

    void AddDetails() {
        for (int i = -width / 2; i < width / 2; i++) {
            for (int j = -width / 2; j < length / 2; j++) {
                if (GetAdjacentWallCount(i + width / 2, j + width / 2) < 1 && map[i + width / 2, j + length / 2] == 0) {
                    if (Random.value < .02) {
                        int obsID = (int)Random.Range(0, 2);
                        Instantiate(obstacles[obsID], new Vector3(i, j, 0), Quaternion.identity);
                    }
                    else if (Random.value < .02) {
                        int obsID = (int)Random.Range(2, 5);
                        Instantiate(obstacles[obsID], new Vector3(i + Random.Range(-.35f, .35f), j + Random.Range(-.15f, .15f), 0), Quaternion.identity);
                    }
                }
                else if (GetAdjacentWallCount(i + width / 2, j + width / 2) > 2 && map[i + width / 2, j + length / 2] == 0) {
                    if (Random.value < .02) {
                        int obsID = (int)Random.Range(5, 6);
                        Instantiate(obstacles[obsID], new Vector3(i + .5f, j + .5f, 0), Quaternion.identity);
                    }
                }
                
            }
        }
    }

}