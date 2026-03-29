using System.Runtime.InteropServices;
using System.Windows;
using System.IO;
using System;

namespace XPaint.Resources.Keybinds;

public static class GamepadPoller
{
    private static CancellationTokenSource? s_cts;
    private static Task? s_pollTask;
    private static readonly object s_lock = new();
    private const int PollIntervalMs = 16; // ~60 Hz

    [StructLayout(LayoutKind.Sequential)]
    private struct XInputGamepad
    {
        public ushort Buttons;
        public byte LeftTrigger;
        public byte RightTrigger;
        public short ThumbLX;
        public short ThumbLY;
        public short ThumbRX;
        public short ThumbRY;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct XInputState
    {
        public uint PacketNumber;
        public XInputGamepad Gamepad;
    }

    [DllImport("xinput1_4.dll", EntryPoint = "XInputGetState")]
    private static extern uint XInputGetState(uint dwUserIndex, ref XInputState pState);

    private const uint ErrorSuccess = 0;
    private const byte TriggerThreshold = 128;
    // Temporary logger for debugging gamepad inputs. Remove when finished.
    private const bool s_enableLogging = true;
    private static readonly string s_logPath = Path.Combine(Path.GetTempPath(), "GamepadPollerLog.txt");

    public static bool IsAvailable()
    {
        try
        {
            var state = new XInputState();
            return XInputGetState(0, ref state) == ErrorSuccess;
        }
        catch
        {
            return false;
        }
    }

    public static void Start()
    {
        lock (s_lock)
        {
            if (s_cts != null) return;
            s_cts = new CancellationTokenSource();
            var token = s_cts.Token;
            s_pollTask = Task.Run(() => PollLoop(token), token);
        }
    }

    public static void Stop()
    {
        lock (s_lock)
        {
            if (s_cts == null) return;
            s_cts.Cancel();
            try { s_pollTask?.Wait(500); } catch { /* ignored */ }
            s_cts.Dispose();
            s_cts = null;
            s_pollTask = null;
        }
    }

    private static async Task PollLoop(CancellationToken token)
    {
        var prevButtons = GamepadButton.None;
        var prevLeftTrigger = false;
        var prevRightTrigger = false;

        while (!token.IsCancellationRequested)
        {
            var state = new XInputState();
            if (XInputGetState(0, ref state) == ErrorSuccess)
            {
                var current = (GamepadButton)state.Gamepad.Buttons;
                var leftTrigger = state.Gamepad.LeftTrigger >= TriggerThreshold;
                var rightTrigger = state.Gamepad.RightTrigger >= TriggerThreshold;

                // Detect newly pressed buttons (rising edge)
                var justPressed = current & ~prevButtons;

                // Detect trigger rising edges
                var leftTriggerJustPressed = leftTrigger && !prevLeftTrigger;
                var rightTriggerJustPressed = rightTrigger && !prevRightTrigger;

                if (justPressed != GamepadButton.None || leftTriggerJustPressed || rightTriggerJustPressed)
                {
                    if (s_enableLogging)
                    {
                        try
                        {
                            var hexButtons = ((ushort)state.Gamepad.Buttons).ToString("X4");
                            var line = $"{DateTime.UtcNow:o}\tButtons=0x{hexButtons}\tCurrent={current}\tJustPressed={justPressed}\tLTrig={leftTriggerJustPressed}\tRTrig={rightTriggerJustPressed}";
                            File.AppendAllText(s_logPath, line + Environment.NewLine);
                        }
                        catch
                        {
                            // ignore logging errors
                        }
                    }

                    FireHotkeys(justPressed, leftTriggerJustPressed, rightTriggerJustPressed);
                }

                prevButtons = current;
                prevLeftTrigger = leftTrigger;
                prevRightTrigger = rightTrigger;
            }

            try
            {
                await Task.Delay(PollIntervalMs, token);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }

    private static void FireHotkeys(GamepadButton justPressed, bool leftTrigger, bool rightTrigger)
    {
        try
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                HotkeysManager.CheckGamepadHotkeys(justPressed, leftTrigger, rightTrigger);
            });
        }
        catch
        {
            // App shutting down
        }
    }
}
