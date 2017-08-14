namespace Launchpad
{
    public class LButton
    {
        public const int UP = 0x68;
        public const int DOWN = 0x69;
        public const int LEFT = 0x6A;
        public const int RIGHT = 0x6B;
        public const int SESSION = 0x6C;
        public const int USER1 = 0x6D;
        public const int USER2 = 0x6E;
        public const int MIXER = 0x6F;
                     
        public const int SCENE_OFFSET = 1;
        public const int SCENE1 = SCENE_OFFSET + 0x08;
        public const int SCENE2 = SCENE_OFFSET + 0x18;
        public const int SCENE3 = SCENE_OFFSET + 0x28;
        public const int SCENE4 = SCENE_OFFSET + 0x38;
        public const int SCENE5 = SCENE_OFFSET + 0x48;
        public const int SCENE6 = SCENE_OFFSET + 0x58;
        public const int SCENE7 = SCENE_OFFSET + 0x68;
        public const int SCENE8 = SCENE_OFFSET + 0x78;

        public static bool IsButtonCode(int buttonCode)
        {
            if (buttonCode == UP) return true;
            if (buttonCode == DOWN) return true;
            if (buttonCode == LEFT) return true;
            if (buttonCode == RIGHT) return true;
            if (buttonCode == SESSION) return true;
            if (buttonCode == USER1) return true;
            if (buttonCode == USER2) return true;
            if (buttonCode == MIXER) return true;
            return false;
        }

        public static bool IsSceneButtonCode(int buttonCode)
        {
            if (buttonCode == SCENE1) return true;
            if (buttonCode == SCENE2) return true;
            if (buttonCode == SCENE3) return true;
            if (buttonCode == SCENE4) return true;
            if (buttonCode == SCENE5) return true;
            if (buttonCode == SCENE6) return true;
            if (buttonCode == SCENE7) return true;
            if (buttonCode == SCENE8) return true;
            return false;
        }

        public static int ButtonNumber(int button)
        {
            return (button <= 8) ? button : button - UP;
        }

        public static int ButtonCode(int button)
        {
            return (button <= 8) ? button + UP : button;
        }

        public static int SceneButtonNumber(int button)
        {
            return (button <= 8) ? button : (button - SCENE_OFFSET + 8) / 16;
        }

        public static int SceneButtonCode(int button)
        {
            return (button <= 8) ? (button * 16) - 8 + SCENE_OFFSET : button;
        }
    }
}
