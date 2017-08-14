using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launchpad
{
    public class LMidiCodes
    {
        public const int STATUS_NIL = 0x00;
        public const int STATUS_OFF = 0x80;
        public const int STATUS_ON = 0x90;
        public const int STATUS_MULTI = 0x92;
        public const int STATUS_CC = 0xB0;
                     
        public const int VELOCITY_TEST_LEDS = 0x7C;
                     
        public const int GRIDLAYOUT_XY = 0x01;
        public const int GRIDLAYOUT_DRUM_RACK = 0x02;
                     
        public const int BUFFER0 = 0;
        public const int BUFFER1 = 1;
        public const int MODE_FLASHING = 8;  //whether to start flashing by automatically switching between the two buffers for display
        public const int MODE_COPY = 16; //whether to copy the LEDs states from the new display_buffer over to the new update_buffer
    }
}
