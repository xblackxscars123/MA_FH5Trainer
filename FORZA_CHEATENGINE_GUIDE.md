# Forza Horizon Offline Memory Extraction Guide with Cheat Engine

This guide provides a basic walkthrough on how to use Cheat Engine to find memory addresses, pointers, and AOBs (Array of Bytes) for Forza Horizon while playing in offline mode. This is intended for educational purposes and for use in a private, offline context.

**Disclaimer:** Modifying game memory can lead to unexpected crashes, corrupted save files, or other issues. Always back up your save data before proceeding. Using cheats online may result in a ban from the game's online services. **Proceed at your own risk.**

## 1. Prerequisites

*   **Forza Horizon (PC Version):** This guide assumes you are using a legitimate copy of the game on PC.
*   **Cheat Engine:** Download and install the latest version of Cheat Engine from [cheatengine.org](https://cheatengine.org).
*   **Offline Mode:** Ensure you are playing in a completely offline session to avoid any potential network-related issues or bans.

## 2. Setting up Cheat Engine

1.  **Launch Forza Horizon:** Start the game and load into a driving session.
2.  **Launch Cheat Engine:** Run Cheat Engine as an administrator.
3.  **Attach to Process:**
    *   Click the "Select a process to open" button (the computer icon in the top-left).
    *   From the process list, find and select the Forza Horizon process (e.g., `ForzaHorizon5.exe`).
    *   Click "Open".

## 3. Finding Values (Example: Credits)

The easiest way to find a value is to search for it, change it in the game, and then search for the new value.

1.  **Initial Scan:**
    *   In Cheat Engine, enter your current number of credits in the "Value" box.
    *   Click "First Scan". A list of memory addresses will appear on the left.

2.  **Change the Value in-game:**
    *   Go back to the game and do something to change your credits (e.g., buy a car, complete a race).

3.  **Next Scan:**
    *   Go back to Cheat Engine, enter your new credit amount in the "Value" box.
    *   Click "Next Scan". The list of addresses should be much smaller.

4.  **Isolate the Address:**
    *   Repeat steps 2 and 3 until you have only a few addresses left (ideally one).
    *   Double-click the correct address to add it to the address list at the bottom.
    *   You can now change the value in Cheat Engine to see if it changes in the game.

## 4. Finding Pointers

Static addresses (like the one you just found) can change each time you restart the game. A pointer is a static address that points to the dynamic address of your value.

1.  **Find the Address:** Follow the steps in section 3 to find the address of the value you want.
2.  **Find what accesses this address:** Right-click on the address in your address list and select "Find out what accesses this address".
3.  **Analyze the instructions:** A new window will open showing the assembly instructions that are accessing the address. Go back to the game and do something that would cause the value to be read or written. The window will populate with instructions.
4.  **Look for a base address:** Look for an instruction that uses a register with an offset, like `[rax+10]`. The register (`rax` in this case) holds a base address.
5.  **Pointer Scan:**
    *   Copy the address from the instruction.
    *   Go back to the main Cheat Engine window and click "New Scan", then "Pointer Scan".
    *   Paste the address into the "Address to find" box.
    *   Set the "Max Level" to a reasonable number (e.g., 4).
    *   Click "OK". This will generate a list of pointer paths.

## 5. Finding AOBs (Array of Bytes)

An AOB is a unique sequence of bytes for a specific function in the game's code. This is useful for creating detours and patches.

1.  **Find a function:** Use the "Find out what accesses this address" method from the previous section to find an instruction that is part of the function you want to target.
2.  **Go to Disassembler:** Right-click the instruction and select "Show in disassembler".
3.  **Identify the AOB:** In the disassembler view, you'll see the bytes for each instruction. Select a sequence of bytes that looks unique. Wildcards (`?`) can be used for parts of the signature that might change.
4.  **AOB Scan:**
    *   Go back to the main Cheat Engine window.
    *   Change the "Value Type" to "Array of Byte".
    *   Enter your AOB signature in the "Array of Byte" box.
    *   Click "First Scan".

## 6. Tips for Offline Use

*   **Disconnect from the Internet:** The most reliable way to ensure you are offline is to disconnect your PC from the internet before launching the game.
*   **Use a separate account:** If possible, use a separate, non-primary account for any memory editing activities.
*   **Backup your saves:** Before you start, navigate to the game's save location and create a backup.

This guide provides a starting point. Reverse engineering can be a complex process, and finding the right values and signatures often requires patience and experimentation.

## 7. Troubleshooting

### Game Crashes Immediately (Process Detection)
If the game closes instantly when you open Cheat Engine, it is detecting the process name.

1.  **Rename Cheat Engine:**
    *   Close Cheat Engine.
    *   Navigate to your Cheat Engine installation folder.
    *   Rename `cheatengine-x86_64.exe` to something random (e.g., `notepad_plus.exe`).

2.  **Configure Before Attaching:**
    *   Open your renamed Cheat Engine *before* attaching to the game.
    *   Go to **Edit** -> **Settings** -> **Debugger Options**.
    *   Change "Debugger method" to **Use VEH Debugger**.
    *   *(Optional)* Under "Extra", select "Query memory region routines" and ensure it is **unchecked** if it causes issues.

### Correct Launch Order
The tool (xpaint) applies a bypass for memory integrity checks. You must launch it in this specific order:

1.  Launch **Forza Horizon 5** and wait until you are driving.
2.  Launch **xpaint** (RemixedTrnr).
3.  Wait for xpaint to show **"Attached: On"** (Green text). This injects the bypass code.
4.  Launch your **renamed Cheat Engine**.
5.  Attach Cheat Engine to the game process.

*Note: If you attach Cheat Engine before xpaint is attached, the game will likely crash.*
