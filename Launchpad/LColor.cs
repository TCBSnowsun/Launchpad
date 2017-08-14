namespace Launchpad
{
    public class LColor
    {
        public const int OFF = 0;
        public const int LOW = 1;
        public const int MEDIUM = 2;
        public const int HIGH = 3;
                     
        public const int RED_OFF = OFF;
        public const int RED_LOW = LOW;
        public const int RED_MEDIUM = MEDIUM;
        public const int RED_HIGH = HIGH;
                     
        public const int GREEN_OFFSET = 16;
        public const int GREEN_OFF = OFF * GREEN_OFFSET;
        public const int GREEN_LOW = LOW * GREEN_OFFSET;
        public const int GREEN_MEDIUM = MEDIUM * GREEN_OFFSET;
        public const int GREEN_HIGH = HIGH * GREEN_OFFSET;
                     
        public const int YELLOW_OFF = RED_OFF + GREEN_OFF;
        public const int YELLOW_LOW = RED_LOW + GREEN_LOW;
        public const int YELLOW_MEDIUM = RED_MEDIUM + GREEN_MEDIUM;
        public const int YELLOW_HIGH = RED_HIGH + GREEN_HIGH;
                     
        public const int BUFFERED = 12; // updates the LED for the current update_buffer only
        public const int FLASHING = 8; // flashing   updates the LED for flashing (the new value will be written to buffer 0 while the LED will be off in buffer 1, see buffering_mode)
        public const int NORMAL = 0; // updates the LED for all circumstances (the new value will be written to both buffers)

        private int red;
        private int green;
        private int mode;

        public LColor() : this(OFF)
        { }

        public LColor(int red, int green) : this(red)
        {
            SetGreen(green);
        }

        public LColor(int red, int green, int mode) : this(red + mode)
        {
            SetGreen(green);
        }

        public LColor(int _color)
        {
            int red_and_mode = _color % GREEN_OFFSET;
            if (red_and_mode < FLASHING)
                SetMode(NORMAL);
            else if (red_and_mode < BUFFERED)
                SetMode(FLASHING);
            else
                SetMode(BUFFERED);

            SetRed(red_and_mode - GetMode());
            SetGreen(_color - red_and_mode);
        }

        public int Velocity()
        {
            //Hack switch values, as CONST have switch values for default reasons
            int mode = this.mode;
            if (mode == NORMAL)
            {
                mode = BUFFERED;
            }
            else if (mode == BUFFERED)
            {
                mode = NORMAL;
            }
            return this.green + this.red + mode;
        }

        public int GetRed()
        {
            return red;
        }

        public void SetRed(int red)
        {
            this.red = red;
        }

        public int GetGreen()
        {
            return green;
        }

        public void SetGreen(int green)
        {
            if (green < GREEN_OFFSET) green *= GREEN_OFFSET;
            this.green = green;
        }

        public int GetMode()
        {
            return mode;
        }

        public void SetMode(int mode)
        {
            this.mode = mode;
        }

        public bool IsColor(int color)
        {
            return Velocity() == new LColor(color).Velocity();
        }
        
        public bool IsColor(LColor color)
        {
            return Velocity() == color.Velocity();
        }
        
        public bool IsRed(int red)
        {
            return GetRed() == new LColor(red).GetRed();
        }
        
        public bool IsGreen(int green)
        {
            return GetGreen() == new LColor(green).GetGreen();
        }

        public bool IsMode(int mode)
        {
            return GetMode() == new LColor(mode).GetMode();
        }
    }
}
