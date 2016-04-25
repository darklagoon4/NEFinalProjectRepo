using UnityEngine;
using System.Collections;

namespace RTS {
    public static class ResourceManagerScript {

        public static int scrollDist { get { return 100; } }
        public static float scrollSpeed { get { return 2.0f; } }
        private static Vector3 invalPos = new Vector3(-10000, -10000, -10000);
        public static Vector3 invalidPosition {  get { return invalPos; } }

        private static Bounds invalBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
        public static Bounds invalidBounds { get { return invalBounds; } }

        public static int buildSpeed { get { return 2; } }

        public static GameObjectTrackerScript gameObjTracker;


        public static void setGameObjectTracker(GameObjectTrackerScript objList)
        {
            gameObjTracker = objList;
        }

        public static GameObject getUnit(string name)
        {
            return gameObjTracker.getUnit(name);
        }

        public static GameObject getBuilding(string name)
        {
            return gameObjTracker.getBuilding(name);
        }

        public static GameObject getWorldObj(string name)
        {
            return gameObjTracker.getWorldObj(name);
        }

        public static GameObject getPlayer()
        {
            return gameObjTracker.getPlayer();
        }

        public static Texture2D getImage(string name)
        {
            return gameObjTracker.getImage(name);
        }



    }


}