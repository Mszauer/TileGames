﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework;
using System.Drawing;

namespace MarioWorld1_1 {
    class Game {
        public static readonly int TILE_SIZE = 16;
        protected Map currentMap = null;
        PointF offsetPosition = new PointF();
        public bool GameOver = false;
        protected PlayerCharacter hero = null;
        protected string heroSheet = "Assets/Mario.png";

        protected string startingMap = "Assets/world1-1.txt";
        
        //rows before columns, map[y][x]
        public Tile GetTile(PointF pixelPoint) {

            return currentMap[(int)pixelPoint.Y / TILE_SIZE][(int)pixelPoint.X / TILE_SIZE]; //y returns 31? y=496 x=44
        }
        public Rectangle GetTileRect(PointF pixelPoint) {
            int xTile = (int)pixelPoint.X / TILE_SIZE; //integer math
            int yTile = (int)pixelPoint.Y / TILE_SIZE;
            Rectangle result = new Rectangle(xTile * TILE_SIZE, yTile * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            return result;
        }
        protected static Game instance = null;
        public static Game Instance {
            get {
                if (instance == null) {
                    instance = new Game();
                }
                return instance;
            }
        }

        protected Game() {

        }
        public void Initialize() {
            TextureManager.Instance.UseNearestFiltering = true;
            hero = new PlayerCharacter(heroSheet);
            currentMap = new Map(startingMap,hero);
        }
        public void Update(float dt) {
            //currentMap = currentMap.ResolveDoors(hero);
            //currentMap.Update(dt, hero, projectiles);
            hero.Update(1/30.0f);
        }
        public void Render() {
            PointF offsetPosition = new PointF();
            offsetPosition.X = hero.Position.X - (float)(1 * Game.TILE_SIZE);
            //if hero is less than half of camea close to left or top corner
            if (hero.Position.X < 6 * Game.TILE_SIZE) {
                offsetPosition.X = 0;
            }
            //bottom corner camera logic
            if (hero.Position.X > (currentMap[0].Length - 6) * Game.TILE_SIZE) {
                offsetPosition.X = (currentMap[0].Length - 12) * Game.TILE_SIZE;
            }
            currentMap.Render(offsetPosition,hero.Center);
            hero.Render();
        }
        public void Shutdown() {
            currentMap.Destroy();
            hero.Destroy();
        }
    }
}
