using System;
using CannedBytes.Midi;

namespace Launchpad
{
    public class Launchpad : IMidiDataReceiver
    {
        private readonly MidiInPort _inPort;
        private readonly MidiOutPort _outPort;
        
        public string DeviceName { get; set; }
        public bool IsConnected { get; set; }

        public Launchpad() : this("Launchpad S")
        { }

        public Launchpad(string deviceName)
        {
            IsConnected = false;
            DeviceName = deviceName;

            for (int index = 0; index < new MidiInPortCapsCollection().Count; index++)
            {
                MidiInPortCaps inCaps = new MidiInPortCapsCollection()[index];
                if (inCaps.Name.Equals(deviceName))
                {
                    _inPort = new MidiInPort();
                    _inPort.Successor = this;
                    _inPort.Open(index);
                    _inPort.Start();
                }
            }

            for (int index = 0; index < new MidiOutPortCapsCollection().Count; index++)
            {
                MidiOutPortCaps outCaps = new MidiOutPortCapsCollection()[index];
                if (outCaps.Name.Equals(deviceName))
                {
                    _outPort = new MidiOutPort();
                    _outPort.Open(index);
                }
            }

            IsConnected = true;
        }

        public void Reset()
        {
            Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, LMidiCodes.STATUS_NIL);
        }

        public void TestLeds()
        {
            TestLeds(LColor.HIGH);
        }

        public void TestLeds(int brightness)
        {
            if (brightness == 0)
            {
                Reset();
            }
            else
            {
                Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, LMidiCodes.VELOCITY_TEST_LEDS + brightness);
            }
        }

        public void ChangeButton(int button, int color)
        {
            ChangeButton(button, new LColor(color));
        }

        public void ChangeButton(int button, LColor c)
        {
            Output(LMidiCodes.STATUS_CC, LButton.ButtonCode(button), c.Velocity());
        }

        public void ChangeSceneButton(int button, int color)
        {
            ChangeSceneButton(button, new LColor(color));
        }

        public void ChangeSceneButton(int button, LColor c)
        {
            Output(LMidiCodes.STATUS_ON, LButton.SceneButtonCode(button) - LButton.SCENE_OFFSET, c.Velocity());
        }

        public void ChangeGrid(int x, int y, int color)
        {
            ChangeGrid(x, y, new LColor(color));
        }

        public void ChangeGrid(int x, int y, LColor c)
        {
            Output(LMidiCodes.STATUS_ON, (y * 16 + x), c.Velocity());
        }

        public void ChangeAll(LColor[] colors)
        {
            // send normal MIDI message to reset rapid LED change pointer
            //  in this case, set mapping mode to x-y layout (the default)
            Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, LMidiCodes.GRIDLAYOUT_XY);

            int default_color = new LColor().Velocity();
            //  send colors in slices of 2
            for (int i = 0; i < 80; i = i + 2)
            {
                int param1;
                try
                {
                    param1 = colors[i].Velocity();
                }
                catch (IndexOutOfRangeException ex) {
                    param1 = default_color;
                }

                int param2;
                try
                {
                    param2 = colors[i + 1].Velocity();
                }
                catch (IndexOutOfRangeException) {
                    param2 = default_color;
                }
                Output(LMidiCodes.STATUS_MULTI, param1, param2);
            }
        }

        public void ChangeAll(int[] colors)
        {
            int param1, param2;

            // send normal MIDI message to reset rapid LED change pointer
            //  in this case, set mapping mode to x-y layout (the default)
            Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, LMidiCodes.GRIDLAYOUT_XY);

            int default_color = new LColor().Velocity();
            //  send colors in slices of 2
            for (int i = 0; i < 80; i = i + 2)
            {
                try
                {
                    param1 = colors[i];
                }
                catch (IndexOutOfRangeException ex) {
                    param1 = default_color;
                }

                try
                {
                    param2 = colors[i + 1];
                }
                catch (IndexOutOfRangeException ex) {
                    param2 = default_color;
                }
                Output(LMidiCodes.STATUS_MULTI, param1, param2);
            }
        }

        public void ChangeAll()
        {
            Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, LMidiCodes.GRIDLAYOUT_XY);
        }

        public void FlashingOn()
        {
            BufferingMode(LMidiCodes.BUFFER0, LMidiCodes.BUFFER0);
        }

        public void FlashingOff()
        {
            BufferingMode(LMidiCodes.BUFFER1, LMidiCodes.BUFFER0);
        }

        public void FlashingAuto()
        {
            BufferingMode(LMidiCodes.BUFFER0, LMidiCodes.BUFFER0, LMidiCodes.MODE_FLASHING);
        }

        public void BufferingMode(int display_buffer, int update_buffer)
        {
            BufferingMode(LMidiCodes.BUFFER0, LMidiCodes.BUFFER0, 0);
        }

        public void BufferingMode(int displayBuffer, int updateBuffer, int flags)
        {
            int data = displayBuffer + 4 * updateBuffer + 32 + flags;
            Output(LMidiCodes.STATUS_CC, LMidiCodes.STATUS_NIL, data);
        }

        private void Output(int status, int data1, int data2)
        {
            if (_outPort != null && _outPort.IsOpen)
            {
                MidiData midiData = new MidiData
                {
                    Status = Convert.ToByte(status),
                    Parameter1 = Convert.ToByte(data1),
                    Parameter2 = Convert.ToByte(data2)
                };

                _outPort.ShortData(midiData);
            }
        }

        public void ShortData(int data, long timestamp)
        {
        }

        public void LongData(MidiBufferStream buffer, long timestamp)
        {
        }
    }
}
